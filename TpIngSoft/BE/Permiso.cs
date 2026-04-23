using System;
using System.Collections.Generic;

namespace BE
{
    public class Permiso
    {
        public int Id { get; set; }
        public string Nombre { get; set; }

        public override string ToString()
        {
            return Nombre;
        }
    }
}
