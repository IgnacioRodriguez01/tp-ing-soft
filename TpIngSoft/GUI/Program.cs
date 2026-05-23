using System;
using System.Windows.Forms;

namespace TpIngSoft
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            BLL.UsuarioBLL usuarioBLL = new BLL.UsuarioBLL();
            if (usuarioBLL.ValidarSesionLocal() || new FormLogin().ShowDialog() == DialogResult.OK)
            {
                Application.Run(new FormMain());
            }
        }
    }
}
