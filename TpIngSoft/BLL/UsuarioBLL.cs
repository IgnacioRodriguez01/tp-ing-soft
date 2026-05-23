using System;
using System.Collections.Generic;
using BE;
using DAL;
using SERVICIOS;
using System.IO;

namespace BLL
{
    public class UsuarioBLL
    {
        private MapperUsuario mapperUsuario = new MapperUsuario();
        private MapperSeguridad mapperSeguridad = new MapperSeguridad();
        private MapperSesion mapperSesion = new MapperSesion();
        private const string SESSION_FILE = "session.dat";

        public bool Login(string nombre, string pass)
        {
            BE.Usuario user = mapperUsuario.BuscarPorNombre(nombre);

            if (user != null)
            {
                if (user.BloqueadoHasta.HasValue && user.BloqueadoHasta.Value > DateTime.Now)
                    throw new Exception("Usuario bloqueado temporalmente.");

                if (user.Password == Encriptador.Hash(pass))
                {
                    mapperUsuario.ActualizarIntentos(nombre, true);
                    CargarDatosUsuario(user);

                    int sessionId = mapperSesion.AbrirSesion(user.Id);
                    if (sessionId != -1)
                    {
                        SessionManager.Instance.Login(user, sessionId);
                        GuardarSesionLocal(sessionId);
                        GestorBitacora.Instance.RegistrarEvento(user, "Login", "Inicio de sesión exitoso");
                        return true;
                    }
                }
                else
                {
                    mapperUsuario.ActualizarIntentos(nombre, false);
                }
            }
            
            GestorBitacora.Instance.RegistrarEvento(null, "Login Fallido", "Intento de acceso: " + nombre);
            return false;
        }

        private void CargarDatosUsuario(Usuario user)
        {
            user.Roles = mapperSeguridad.LeerRolesPorUsuario(user.Id);
            foreach (var rol in user.Roles)
            {
                rol.Permisos = mapperSeguridad.LeerPermisosPorRol(rol.Id);
            }
        }

        public void Logout()
        {
            if (SessionManager.Instance.IsLoggedIn())
            {
                Usuario user = SessionManager.Instance.CurrentUser;
                int sessionId = SessionManager.Instance.SessionId.Value;
                
                mapperSesion.CerrarSesion(sessionId);
                BorrarSesionLocal();
                GestorBitacora.Instance.RegistrarEvento(user, "Logout", "Cierre de sesión");
                SessionManager.Instance.Logout();
            }
        }

        public int Registrar(BE.Usuario user, int idRol = 2)
        {
            if (string.IsNullOrEmpty(user.Nombre) || string.IsNullOrEmpty(user.Password))
                return -1;

            if (mapperUsuario.BuscarPorNombre(user.Nombre) != null)
            {
                Usuario editor = SessionManager.Instance.IsLoggedIn() ? SessionManager.Instance.CurrentUser : null;
                GestorBitacora.Instance.RegistrarEvento(editor, "Alta Fallida", "Usuario existente: " + user.Nombre);
                return -2;
            }

            user.Password = Encriptador.Hash(user.Password);
            int result = mapperUsuario.Crear(user);
            if (result != -1)
            {
                mapperSeguridad.AsignarRol(result, idRol);
                Usuario editor = SessionManager.Instance.IsLoggedIn() ? SessionManager.Instance.CurrentUser : null;
                GestorBitacora.Instance.RegistrarEvento(editor, "Alta", "Usuario creado: " + user.Nombre);
            }
            return result;
        }

        public List<Rol> ObtenerRoles() => mapperSeguridad.LeerRoles();

        public void GuardarSesionLocal(int id) => File.WriteAllText(SESSION_FILE, id.ToString());

        public void BorrarSesionLocal() { if (File.Exists(SESSION_FILE)) File.Delete(SESSION_FILE); }

        public bool ValidarSesionLocal()
        {
            if (File.Exists(SESSION_FILE))
            {
                if (int.TryParse(File.ReadAllText(SESSION_FILE), out int id))
                {
                    int idUsuario = mapperSesion.ValidarYRefrescarSesion(id);
                    if (idUsuario != -1)
                    {
                        Usuario user = mapperUsuario.BuscarPorId(idUsuario);
                        if (user != null)
                        {
                            CargarDatosUsuario(user);
                            SessionManager.Instance.Login(user, id);
                            return true;
                        }
                    }
                }
                BorrarSesionLocal();
            }
            return false;
        }
    }
}
