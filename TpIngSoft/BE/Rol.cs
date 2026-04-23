using System;
using System.Collections.Generic;

namespace BE
{
    public class Rol
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public List<Permiso> Permisos { get; set; } = new List<Permiso>();

        public override string ToString()
        {
            return Nombre;
        }
    }
}
