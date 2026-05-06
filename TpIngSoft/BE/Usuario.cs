using System;
using System.Collections.Generic;

namespace BE
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Password { get; set; }
        public bool Activo { get; set; }
        public int IntentosFallidos { get; set; }
        public DateTime? BloqueadoHasta { get; set; }
        public List<Rol> Roles { get; set; } = new List<Rol>();

        public override string ToString()
        {
            return Nombre;
        }
    }
}