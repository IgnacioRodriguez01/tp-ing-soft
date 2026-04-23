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
                    return true;
                }
            }
            return false;
        }

        public void Logout()
        {
            if (SessionManager.Instance.IsLoggedIn())
            {
                int sessionId = SessionManager.Instance.SessionId.Value;
                mapperSesion.CerrarSesion(sessionId);
                SessionManager.Instance.Logout();
            }
        }

        public int Registrar(BE.Usuario user)
        {
            // Validaciones de negocio podrían ir aquí
            if (string.IsNullOrEmpty(user.Nombre) || string.IsNullOrEmpty(user.Password))
                return -1;
                
            return mapperUsuario.Crear(user);
        }
    }
}
