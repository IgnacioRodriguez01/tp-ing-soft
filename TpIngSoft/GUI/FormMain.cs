using System;
using System.Collections.Generic;
using System.Windows.Forms;
using BE;
using BLL;
using SERVICIOS;
using TpIngSoft.Traduccion;

namespace TpIngSoft
{
    public partial class FormMain : Form, IObservador
    {
        private UsuarioBLL usuarioBLL = new UsuarioBLL();
        private ToolStripComboBox cmbIdioma;
        private ToolStripMenuItem gestionIdiomasToolStripMenuItem;
        private List<IControlTraducible> _controlesTraducibles = new List<IControlTraducible>();
        private bool _isChangingLanguage = false;

        public FormMain()
        {
            InitializeComponent();
            this.IsMdiContainer = true;
            this.WindowState = FormWindowState.Maximized;
            this.Text = "TpIngSoft - Sistema de Gestión";

            // Initialize language dropdown
            cmbIdioma = new ToolStripComboBox();
            cmbIdioma.Alignment = ToolStripItemAlignment.Right;
            cmbIdioma.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbIdioma.Size = new System.Drawing.Size(120, 25);
            cmbIdioma.SelectedIndexChanged += CmbIdioma_SelectedIndexChanged;
            menuStrip1.Items.Add(cmbIdioma);

            // Initialize admin option for language management
            gestionIdiomasToolStripMenuItem = new ToolStripMenuItem();
            gestionIdiomasToolStripMenuItem.Name = "gestionIdiomasToolStripMenuItem";
            gestionIdiomasToolStripMenuItem.Click += GestionIdiomasToolStripMenuItem_Click;
            adminToolStripMenuItem.DropDownItems.Add(gestionIdiomasToolStripMenuItem);

            RegistrarControlesTraducibles();

            CargarIdiomas();

            // Subscribe to language changes
            GestorIdioma.Instancia.Adjuntar(this);

            ConfigurarMenu();
        }

        private void RegistrarControlesTraducibles()
        {
            _controlesTraducibles.Clear();
            _controlesTraducibles.Add(new EtiquetaTraducible(this, "FormMain", "TpIngSoft - Sistema de Gestión"));

            var menuTraducible = new MenuStripTraducible("FormMain.menuStrip1")
                .ConItem(archivoToolStripMenuItem, BE.NombreControl.FormMain_archivoToolStripMenuItem, "Archivo")
                .ConItem(logoutToolStripMenuItem, BE.NombreControl.FormMain_logoutToolStripMenuItem, "Cerrar Sesión")
                .ConItem(salirToolStripMenuItem, BE.NombreControl.FormMain_salirToolStripMenuItem, "Salir")
                .ConItem(adminToolStripMenuItem, BE.NombreControl.FormMain_adminToolStripMenuItem, "Admin")
                .ConItem(gestionUsuariosToolStripMenuItem, BE.NombreControl.FormMain_gestionUsuariosToolStripMenuItem, "Gestión de Usuarios")
                .ConItem(bitacoraToolStripMenuItem, BE.NombreControl.FormMain_bitacoraToolStripMenuItem, "Bitácora")
                .ConItem(controlCambiosToolStripMenuItem, BE.NombreControl.FormMain_controlCambiosToolStripMenuItem, "Control de Cambios")
                .ConItem(gestionPerfilesToolStripMenuItem, BE.NombreControl.FormMain_gestionPerfilesToolStripMenuItem, "Gestión de Perfiles")
                .ConItem(gestionIdiomasToolStripMenuItem, BE.NombreControl.FormMain_gestionIdiomasToolStripMenuItem, "Gestión de Idiomas");

            _controlesTraducibles.Add(menuTraducible);
        }

        private void CargarIdiomas()
        {
            _isChangingLanguage = true;
            try
            {
                cmbIdioma.Items.Clear();
                IdiomaBLL bll = new IdiomaBLL();
                List<Idioma> activos = bll.ObtenerIdiomasActivos();
                foreach (var id in activos)
                {
                    cmbIdioma.Items.Add(id);
                }

                if (GestorIdioma.Instancia.IdiomaActual != null)
                {
                    for (int i = 0; i < cmbIdioma.Items.Count; i++)
                    {
                        if (((Idioma)cmbIdioma.Items[i]).Id == GestorIdioma.Instancia.IdiomaActual.Id)
                        {
                            cmbIdioma.SelectedIndex = i;
                            break;
                        }
                    }
                }
                else if (cmbIdioma.Items.Count > 0)
                {
                    cmbIdioma.SelectedIndex = 0;
                }
            }
            finally
            {
                _isChangingLanguage = false;
            }
        }

        private void CmbIdioma_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_isChangingLanguage) return;

            if (cmbIdioma.SelectedItem is Idioma seleccionado)
            {
                if (SessionManager.Instance.EstaLogueado())
                {
                    var user = SessionManager.Instance.UsuarioActual;
                    user.IdIdioma = seleccionado.Id;
                    new IdiomaBLL().GuardarIdiomaUsuario(user.Id, seleccionado.Id);
                }
                
                GestorIdioma.Instancia.CambiarIdioma(seleccionado);
            }
        }

        public void Actualizar(Dictionary<string, string> traducciones)
        {
            foreach (var control in _controlesTraducibles)
            {
                control.Traducir(traducciones);
            }

            if (SessionManager.Instance.EstaLogueado())
            {
                string usuarioLabel = "Usuario: ";
                if (traducciones.TryGetValue(BE.NombreControl.FormMain_lblSesionInfo, out string trans))
                {
                    usuarioLabel = trans;
                }
                lblSesionInfo.Text = $"{usuarioLabel}{SessionManager.Instance.UsuarioActual.Nombre}";
            }
            else
            {
                lblSesionInfo.Text = "Usuario no autenticado";
            }
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            GestorIdioma.Instancia.Separar(this);
            base.OnFormClosed(e);
        }

        private void ConfigurarMenu()
        {
            if (SERVICIOS.SessionManager.Instance.EstaLogueado())
            {
                string usuarioLabel = "Usuario: ";
                var traducciones = GestorIdioma.Instancia.ObtenerTraduccionesActuales();
                if (traducciones.TryGetValue(BE.NombreControl.FormMain_lblSesionInfo, out string trans))
                {
                    usuarioLabel = trans;
                }
                lblSesionInfo.Text = $"{usuarioLabel}{SERVICIOS.SessionManager.Instance.UsuarioActual.Nombre}";
                
                adminToolStripMenuItem.Visible = SERVICIOS.SessionManager.Instance.TienePermiso("AccesoAdmin");
                gestionUsuariosToolStripMenuItem.Visible = SERVICIOS.SessionManager.Instance.TienePermiso("GestionUsuarios");
                controlCambiosToolStripMenuItem.Visible = SERVICIOS.SessionManager.Instance.TienePermiso("AccesoAdmin");
                gestionPerfilesToolStripMenuItem.Visible = SERVICIOS.SessionManager.Instance.TienePermiso("AccesoAdmin");
                gestionIdiomasToolStripMenuItem.Visible = SERVICIOS.SessionManager.Instance.TienePermiso("AccesoAdmin");
            }
            else
            {
                lblSesionInfo.Text = "Usuario no autenticado";
                adminToolStripMenuItem.Visible = false;
                gestionUsuariosToolStripMenuItem.Visible = false;
                controlCambiosToolStripMenuItem.Visible = false;
                gestionPerfilesToolStripMenuItem.Visible = false;
                gestionIdiomasToolStripMenuItem.Visible = false;
            }

            // Re-sync selected language in combobox when session config updates
            CargarIdiomas();
        }

        private void logoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            usuarioBLL.Logout();
            this.Hide();
            if (new FormLogin().ShowDialog() == DialogResult.OK)
            {
                ConfigurarMenu();
                this.Show();
            }
            else
            {
                this.Close();
            }
        }

        private void gestionUsuariosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormGestionUsuarios frm = new FormGestionUsuarios();
            frm.MdiParent = this;
            frm.Show();
        }

        private void bitacoraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormBitacora frm = new FormBitacora();
            frm.MdiParent = this;
            frm.Show();
        }

        private void controlCambiosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormHistorialUsuario frm = new FormHistorialUsuario();
            frm.MdiParent = this;
            frm.Show();
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void gestionPerfilesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormGestionPerfiles frm = new FormGestionPerfiles();
            frm.MdiParent = this;
            frm.Show();
        }

        private void GestionIdiomasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormGestionIdiomas frm = new FormGestionIdiomas();
            frm.MdiParent = this;
            frm.Show();
        }
    }
}
