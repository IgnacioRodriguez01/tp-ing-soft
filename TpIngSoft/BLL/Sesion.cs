using BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class Sesion
    {
        public BE.Sesion CrearSesion(BE.Usuario usuario)
        {
            BE.Sesion sesion = new BE.Sesion();
            sesion.Usuario = usuario;
            sesion.FechaLogin = DateTime.Now;

            return sesion;
        }

        public void Persistir(BE.Sesion sesion, BE.Usuario usuario)
        {
            DAL.MapperSesion mapper = new DAL.MapperSesion();
            sesion.ID = mapper.Crear(usuario);
        }

        public void Cerrar(BE.Sesion sesion)
        {
            DAL.MapperSesion mapper = new DAL.MapperSesion();
            if (sesion != null) mapper.Cerrar(sesion);
        }

        public void CerrarTodas()
        {
            DAL.MapperSesion mapper = new DAL.MapperSesion();
            mapper.CerrarTodas();
        }
    }
}
