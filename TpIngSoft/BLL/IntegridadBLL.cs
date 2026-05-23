using System;
using System.Collections.Generic;
using System.Linq;
using DAL;
using SERVICIOS;

namespace BLL
{
    public class IntegridadBLL
    {
        private MapperUsuario mapperUsuario = new MapperUsuario();
        private MapperDVV mapperDVV = new MapperDVV();

        public void VerificarIntegridad()
        {
            var usuarios = mapperUsuario.LeerTodos();
            List<long> dvhs = new List<long>();

            foreach (var u in usuarios)
            {
                long dvhCalculado = GestorDV.CalcularDVH(u);
                if (u.DVH != dvhCalculado)
                {
                    throw new Exception($"ERROR DE INTEGRIDAD: El usuario '{u.Nombre}' (ID: {u.Id}) ha sido alterado externamente.");
                }
                dvhs.Add(u.DVH);
            }

            long dvvCalculado = GestorDV.CalcularDVV(dvhs);
            long dvvAlmacenado = mapperDVV.LeerDVV("Usuario");

            if (dvvCalculado != dvvAlmacenado)
            {
                throw new Exception("ERROR DE INTEGRIDAD: La tabla Usuario ha sufrido cambios estructurales (inserciones o eliminaciones externas).");
            }
        }

        public void RepararIntegridad()
        {
            // Solo para uso administrativo en caso de desincronización controlada
            var usuarios = mapperUsuario.LeerTodos();
            List<long> dvhs = new List<long>();

            foreach (var u in usuarios)
            {
                u.DVH = GestorDV.CalcularDVH(u);
                mapperUsuario.Actualizar(u);
                dvhs.Add(u.DVH);
            }

            long dvvCalculado = GestorDV.CalcularDVV(dvhs);
            mapperDVV.ActualizarDVV("Usuario", dvvCalculado);
        }
    }
}
