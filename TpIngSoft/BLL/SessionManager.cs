using System;
using System.Linq;
using BE;

namespace BLL
{
    public class SessionManager
    {
        private static SessionManager _instance;
        private static readonly object _lock = new object();

        public BE.Usuario CurrentUser { get; private set; }
        public int? SessionId { get; private set; }

        private SessionManager() { }

        public static SessionManager Instance
        {
            get
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new SessionManager();
                    }
                    return _instance;
                }
            }
        }

        public void Login(BE.Usuario user, int sessionId)
        {
            CurrentUser = user;
            SessionId = sessionId;
        }

        public void Logout()
        {
            CurrentUser = null;
            SessionId = null;
        }

        public bool IsLoggedIn()
        {
            return CurrentUser != null;
        }

        public bool HasPermission(string permissionName)
        {
            if (CurrentUser == null) return false;

            return CurrentUser.Roles.Any(r => 
                r.Permisos.Any(p => p.Nombre.Equals(permissionName, StringComparison.OrdinalIgnoreCase))
            );
        }
    }
}
