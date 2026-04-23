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

            FormLogin login = new FormLogin();
            if (login.ShowDialog() == DialogResult.OK)
            {
                Application.Run(new FormMain());
            }
            else
            {
                Application.Exit();
            }
        }
    }
}
