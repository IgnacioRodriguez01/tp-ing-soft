using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Sesion
    {
		private int id;

		public int ID
		{
			get { return id; }
			set { id = value; }
		}

		private Usuario usuario;

		public Usuario Usuario
		{
			get { return usuario; }
			set { usuario = value; }
		}
		private DateTime fechaLogin;

		public DateTime FechaLogin
		{
			get { return fechaLogin; }
			set { fechaLogin = value; }
		}
		private DateTime? fechaLogout;

		public DateTime? FechaLogout
		{
			get { return fechaLogout; }
			set { fechaLogout = value; }
		}
	}
}
