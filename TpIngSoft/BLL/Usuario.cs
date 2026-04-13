using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public class Usuario
    {
        public BE.Usuario CrearUsuario(int numero)
        {
            BE.Usuario usuario = new BE.Usuario();
            usuario.Numero = numero;
            return usuario;
        }
        public BE.Usuario Login(string nombre, string pass)
        {
            DAL.MapperUsuario mapper = new DAL.MapperUsuario();
            BE.Usuario usuario = mapper.BuscarPorNombre(nombre) ?? throw new Exception("El nombre de usuario no existe.");

            if (pass == null || pass != usuario.Pass)
            {
                throw new Exception("La contraseña es incorrecta.");
            }

            return usuario;
        }
    }
}