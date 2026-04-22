using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using BE;

namespace DAL
{
    public class MapperUsuario
    {
        private Acceso acceso = new Acceso();

        public Usuario BuscarPorNombre(string nombre)
        {
            acceso.Abrir();
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>
                {
                    acceso.CrearParametro("@Nombre", nombre)
                };

                DataTable dt = acceso.Leer("BuscarUsuarioPorNombre", parameters);

                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    return new Usuario
                    {
                        Id = Convert.ToInt32(row["id"]),
                        Nombre = row["nombre"].ToString(),
                        Password = row["pass"].ToString(),
                        Activo = Convert.ToBoolean(row["activo"])
                    };
                }
                return null;
            }
            finally
            {
                acceso.Cerrar();
            }
        }

        public int Crear(Usuario user)
        {
            acceso.Abrir();
            try
            {
                SqlParameter outParam = acceso.CrearParametroOut("@NuevoId");
                List<SqlParameter> parameters = new List<SqlParameter>
                {
                    acceso.CrearParametro("@Nombre", user.Nombre),
                    acceso.CrearParametro("@Pass", user.Password),
                    outParam
                };

                int result = acceso.Escribir("CrearUsuario", parameters);
                if (result != -1)
                {
                    user.Id = (int)outParam.Value;
                    return user.Id;
                }
                return -1;
            }
            finally
            {
                acceso.Cerrar();
            }
        }
    }
}
