using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace TpIngSoft.Traduccion
{
    public class MenuStripTraducible : IControlTraducible
    {
        public string NombreControl => _nombreControl;
        private readonly string _nombreControl;
        private readonly List<(ToolStripItem Item, string Key, string Fallback)> _items = new List<(ToolStripItem, string, string)>();

        public MenuStripTraducible(string nombreControl)
        {
            _nombreControl = nombreControl;
        }

        public MenuStripTraducible ConItem(ToolStripItem item, string key, string fallback)
        {
            if (item != null)
            {
                _items.Add((item, key, fallback));
            }
            return this;
        }

        public void Traducir(Dictionary<string, string> traducciones)
        {
            foreach (var mapping in _items)
            {
                if (traducciones.TryGetValue(mapping.Key, out string texto))
                {
                    mapping.Item.Text = texto;
                }
                else
                {
                    mapping.Item.Text = $"<{mapping.Key}>";
                }
            }
        }
    }
}
