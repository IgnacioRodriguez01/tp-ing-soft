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
    public class MapperSesion : Mapper<BE.Sesion>
    {
        public int Crear(BE.Usuario usuario)
        {
            Acceso acceso = new Acceso();

            acceso.Abrir();
            string sql = "CrearSesion";

            SqlParameter sId = acceso.CrearParametroOut("@SesionId");
            List<SqlParameter> parametros = new List<SqlParameter> {
                    sId,
                    acceso.CrearParametro("@UsuarioNombre", usuario.Nombre),
                };

            acceso.Escribir(sql, parametros);
            acceso.Cerrar();

            if (sId.Value == DBNull.Value || sId.Value == null)
                throw new Exception("Error al crear sesión.");

            return Convert.ToInt32(sId.Value);
        }

        public bool Cerrar(BE.Sesion sesion)
        {
            Acceso acceso = new Acceso();

            acceso.Abrir();
            string sql = "CerrarSesion";

            acceso.Escribir(sql, new List<SqlParameter> {
                acceso.CrearParametro("@SesionId", sesion.ID)
            });
            
            acceso.Cerrar();

            return true;
        }

        public bool CerrarTodas()
        {
            Acceso acceso = new Acceso();

            acceso.Abrir();
            string sql = "CerrarTodasLasSesiones";

            acceso.Escribir(sql);

            acceso.Cerrar();

            return true;
        }
    }
}
