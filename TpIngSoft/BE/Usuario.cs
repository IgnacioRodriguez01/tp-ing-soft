using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BE
{
    public class Usuario
    {

        private int numero;

		public int Numero
		{
			get { return numero; }
			set { numero = value; }
		}

		private string nombre;

		public string Nombre
		{
			get { return nombre; }
			set { nombre = value; }
		}

		private string pass;

		public string Pass
		{
			get { return pass; }
			set { pass = value; }
		}

		private Sesion sesionActual;

		public Sesion SesionActual
		{
			get { return sesionActual; }
			set { sesionActual = value; }
		}

	}
}