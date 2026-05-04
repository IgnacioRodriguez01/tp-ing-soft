using System;
using System.Collections.Generic;
using BE;
using DAL;

namespace BLL
{
    public class UsuarioBLL
    {
        private MapperUsuario mapperUsuario = new MapperUsuario();
        private MapperSeguridad mapperSeguridad = new MapperSeguridad();
        private MapperSesion mapperSesion = new MapperSesion();

        public bool Login(string nombre, string pass)
        {
            BE.Usuario user = mapperUsuario.BuscarPorNombre(nombre);

            if (user != null && user.Password == pass)
            {
                // Cargar Roles y Permisos
                user.Roles = mapperSeguridad.LeerRolesPorUsuario(user.Id);
                foreach (var rol in user.Roles)
                {
                    rol.Permisos = mapperSeguridad.LeerPermisosPorRol(rol.Id);
                }

                // Crear Sesión en DB
                int sessionId = mapperSesion.AbrirSesion(user.Id);

                if (sessionId != -1)
                {
                    // Inicializar Singleton
                    SessionManager.Instance.Login(user, sessionId);
                    
                    // Registro en Bitácora
                    GestorBitacora.Instance.RegistrarEvento(user, "Login", "Inicio de sesión exitoso");
                    
                    return true;
                }
            }
            
            // Registro de Intento Fallido
            GestorBitacora.Instance.RegistrarEvento(null, "Login Fallido", "Intento de acceso con usuario: " + nombre);
            
            return false;
        }


        public void Logout()
        {
            if (SessionManager.Instance.IsLoggedIn())
            {
                Usuario user = SessionManager.Instance.CurrentUser;
                int sessionId = SessionManager.Instance.SessionId.Value;
                
                mapperSesion.CerrarSesion(sessionId);
                
                // Registro en Bitácora
                GestorBitacora.Instance.RegistrarEvento(user, "Logout", "Cierre de sesión de usuario");
                
                SessionManager.Instance.Logout();
            }
        }


        public int Registrar(BE.Usuario user)
        {
            // Validaciones de negocio podrían ir aquí
            if (string.IsNullOrEmpty(user.Nombre) || string.IsNullOrEmpty(user.Password))
                return -1;
                
            int result = mapperUsuario.Crear(user);
            if (result != -1)
            {
                // Registro en Bitácora
                Usuario editor = SessionManager.Instance.IsLoggedIn() ? SessionManager.Instance.CurrentUser : null;
                GestorBitacora.Instance.RegistrarEvento(editor, "Alta de Usuario", "Se creó el usuario: " + user.Nombre);
            }
            return result;
        }
    }
}

