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

            try
            {
                new BLL.IntegridadBLL().VerificarIntegridad();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error de Integridad", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
                return;
            }

            BLL.UsuarioBLL usuarioBLL = new BLL.UsuarioBLL();
            if (usuarioBLL.ValidarSesionLocal() || new FormLogin().ShowDialog() == DialogResult.OK)
            {
                Application.Run(new FormMain());
            }
        }
    }
}
