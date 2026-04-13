using BE;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class MapperUsuario : Mapper<BE.Usuario>
    {
        public BE.Usuario BuscarPorNombre(string nombre)
        {
            Acceso acceso = new Acceso();
            acceso.Abrir();

            string sql = "BuscarUsuarioPorNombre";

            DataTable dt = acceso.Leer(sql, new List<SqlParameter> {
                acceso.CrearParametro("@Nombre", nombre)
            });

            acceso.Cerrar();

            if (dt.Rows.Count == 0)
                return null;

            DataRow dr = dt.Rows[0];

            BE.Usuario usuario = new BE.Usuario();
            usuario.Nombre = dr["nombre"].ToString();
            usuario.Pass = dr["pass"].ToString();

            return usuario;
        }
    }
}
