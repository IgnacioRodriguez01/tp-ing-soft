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
                DialogResult res = MessageBox.Show(
                    ex.Message + "\n\n¿Desea restaurar/recalcular los dígitos verificadores (DVH/DVV) ahora?", 
                    "Error de Integridad", 
                    MessageBoxButtons.YesNo, 
                    MessageBoxIcon.Warning
                );

                if (res == DialogResult.Yes)
                {
                    try
                    {
                        new BLL.IntegridadBLL().RepararIntegridad();
                        MessageBox.Show(
                            "Dígitos verificadores recalculados y restaurados con éxito. Vuelva a iniciar la aplicación.", 
                            "Restauración Exitosa", 
                            MessageBoxButtons.OK, 
                            MessageBoxIcon.Information
                        );
                    }
                    catch (Exception exReparar)
                    {
                        MessageBox.Show(
                            "Error al restaurar integridad: " + exReparar.Message, 
                            "Error de Restauración", 
                            MessageBoxButtons.OK, 
                            MessageBoxIcon.Error
                        );
                    }
                }
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
