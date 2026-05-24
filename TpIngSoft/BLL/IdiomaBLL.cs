using System;
using System.Collections.Generic;
using BE;
using DAL;

namespace BLL
{
    public class IdiomaBLL
    {
        private readonly MapperIdioma mapper = new MapperIdioma();
        private readonly Dictionary<int, Dictionary<string, string>> cache = new Dictionary<int, Dictionary<string, string>>();
        private readonly object lockObj = new object();

        public List<Idioma> ObtenerIdiomasActivos()
        {
            return mapper.LeerIdiomasActivos();
        }

        public Dictionary<string, string> ObtenerTraduccionesCacheadas(int idIdioma)
        {
            lock (lockObj)
            {
                if (!cache.ContainsKey(idIdioma))
                {
                    List<Traduccion> traducciones = mapper.LeerTraduccionesPorIdioma(idIdioma);
                    Dictionary<string, string> dict = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                    foreach (var t in traducciones)
                    {
                        string key = (t.NombreControl == "(form)" || string.IsNullOrEmpty(t.NombreControl))
                            ? t.Formulario
                            : $"{t.Formulario}.{t.NombreControl}";
                        dict[key] = t.Texto;
                    }
                    cache[idIdioma] = dict;
                }
                return new Dictionary<string, string>(cache[idIdioma]);
            }
        }

        public void InvalidarCache()
        {
            lock (lockObj)
            {
                cache.Clear();
            }
        }

        public void GuardarTraduccion(int idControl, int idIdioma, string texto)
        {
            mapper.ActualizarTraduccion(idControl, idIdioma, texto);
            InvalidarCache();
        }

        public List<Traduccion> ObtenerControles()
        {
            return mapper.LeerControles();
        }

        public int RegistrarControl(string nombre, string formulario)
        {
            return mapper.CrearControl(nombre, formulario);
        }

        public void GuardarIdiomaUsuario(int idUsuario, int idIdioma)
        {
            mapper.GuardarIdiomaUsuario(idUsuario, idIdioma);
        }

        public Idioma LeerIdiomaUsuario(int idUsuario)
        {
            return mapper.LeerIdiomaUsuario(idUsuario);
        }

        public int CrearIdioma(string nombre)
        {
            Idioma nuevo = new Idioma { Nombre = nombre };
            int id = mapper.CrearIdioma(nuevo);
            if (id != -1)
            {
                InvalidarCache();
            }
            return id;
        }
    }
}
