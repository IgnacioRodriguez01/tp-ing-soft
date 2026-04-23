using System;
using System.Windows.Forms;
using BLL;

namespace TpIngSoft
{
    public partial class FormMain : Form
    {
        private UsuarioBLL usuarioBLL = new UsuarioBLL();

        public FormMain()
        {
            InitializeComponent();
            this.IsMdiContainer = true;
            this.WindowState = FormWindowState.Maximized;
            this.Text = "TpIngSoft - Sistema de Gestión";
            ConfigurarMenu();
        }

        private void ConfigurarMenu()
        {
            // Ocultar o mostrar opciones según permisos
            if (SessionManager.Instance.IsLoggedIn())
            {
                lblSesionInfo.Text = $"Usuario: {SessionManager.Instance.CurrentUser.Nombre}";
                
                // Ejemplo de restricción por permisos
                adminToolStripMenuItem.Visible = SessionManager.Instance.HasPermission("AccesoAdmin");
                gestionUsuariosToolStripMenuItem.Visible = SessionManager.Instance.HasPermission("GestionUsuarios");
            }
            else
            {
                lblSesionInfo.Text = "Usuario no autenticado";
                adminToolStripMenuItem.Visible = false;
                gestionUsuariosToolStripMenuItem.Visible = false;
            }
        }

        private void logoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            usuarioBLL.Logout();
            this.Close(); // O re-mostrar login
        }

        private void gestionUsuariosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Abrir Form de Gestión de Usuarios
            FormGestionUsuarios frm = new FormGestionUsuarios();
            frm.MdiParent = this;
            frm.Show();
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
