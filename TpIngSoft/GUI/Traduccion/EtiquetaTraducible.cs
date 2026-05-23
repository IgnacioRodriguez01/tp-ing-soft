using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace TpIngSoft.Traduccion
{
    public class EtiquetaTraducible : IControlTraducible
    {
        public string NombreControl { get; }
        private readonly Control _control;
        private readonly string _fallback;

        public EtiquetaTraducible(Control control, string nombreControl, string fallback)
        {
            _control = control ?? throw new ArgumentNullException(nameof(control));
            NombreControl = nombreControl;
            _fallback = fallback;
        }

        public void Traducir(Dictionary<string, string> traducciones)
        {
            if (traducciones.TryGetValue(NombreControl, out string texto))
            {
                _control.Text = texto;
            }
            else
            {
                _control.Text = _fallback;
            }
        }
    }
}
