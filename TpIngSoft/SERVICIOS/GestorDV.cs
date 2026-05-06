using System;
using System.Collections.Generic;
using System.Linq;
using BE;

namespace SERVICIOS
{
    public static class GestorDV
    {
        /// <summary>
        /// Calcula el Dígito Verificador Horizontal para un Usuario.
        /// Algoritmo: suma de (ASCII * posicion_caracter * posicion_atributo)
        /// </summary>
        public static long CalcularDVH(Usuario user)
        {
            long dvh = 0;
            dvh += CalcularValorString(user.Id.ToString(), 1);
            dvh += CalcularValorString(user.Nombre, 2);
            dvh += CalcularValorString(user.Password, 3);
            dvh += CalcularValorString(user.Activo ? "1" : "0", 4);
            return dvh;
        }

        private static long CalcularValorString(string valor, int posicionAtributo)
        {
            if (string.IsNullOrEmpty(valor)) return 0;
            long suma = 0;
            for (int i = 0; i < valor.Length; i++)
            {
                // Carácter * (posición 1-based) * posición del atributo
                suma += (long)valor[i] * (i + 1) * posicionAtributo;
            }
            return suma;
        }

        /// <summary>
        /// Calcula el Dígito Verificador Vertical como la suma de los DVHs.
        /// </summary>
        public static long CalcularDVV(IEnumerable<long> dvhs)
        {
            return dvhs.Sum();
        }
    }
}
