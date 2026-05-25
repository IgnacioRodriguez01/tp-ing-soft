using System;
using System.Collections.Generic;
using System.Windows.Forms;
using BE;
using BLL;
using SERVICIOS;
using TpIngSoft.Traduccion;

namespace TpIngSoft
{
    public partial class FormGestionUsuarios : Form, IObservador
    {
        private UsuarioBLL usuarioBLL = new UsuarioBLL();
        private List<IControlTraducible> _controlesTraducibles = new List<IControlTraducible>();

        public FormGestionUsuarios()
        {
            InitializeComponent();
            RegistrarControlesTraducibles();
            GestorIdioma.Instancia.Adjuntar(this);
        }

        private void RegistrarControlesTraducibles()
        {
            _controlesTraducibles.Clear();
            _controlesTraducibles.Add(new EtiquetaTraducible(this, "FormGestionUsuarios"));
            _controlesTraducibles.Add(new EtiquetaTraducible(label1, BE.NombreControl.FormGestionUsuarios_label1));
            _controlesTraducibles.Add(new EtiquetaTraducible(label2, BE.NombreControl.FormGestionUsuarios_label2));
            _controlesTraducibles.Add(new EtiquetaTraducible(lblRol, BE.NombreControl.FormGestionUsuarios_lblRol));
            _controlesTraducibles.Add(new EtiquetaTraducible(btnRegistrar, BE.NombreControl.FormGestionUsuarios_btnRegistrar));
        }

        public void Actualizar(Dictionary<string, string> traducciones)
        {
            foreach (var control in _controlesTraducibles)
            {
                control.Traducir(traducciones);
            }
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            GestorIdioma.Instancia.Separar(this);
            base.OnFormClosed(e);
        }

        private void FormGestionUsuarios_Load(object sender, EventArgs e)
        {
            if (SERVICIOS.SessionManager.Instance.TienePermiso("AccesoAdmin"))
            {
                lblRol.Visible = true;
                cmbRoles.Visible = true;
                cmbRoles.DataSource = usuarioBLL.ObtenerRoles();
                cmbRoles.DisplayMember = "Nombre";
                cmbRoles.ValueMember = "Id";
            }
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            try
            {
                Usuario nuevo = new Usuario
                {
                    Nombre = txtNombre.Text.Trim(),
                    Password = txtPassword.Text.Trim(),
                    Activo = true
                };

                int idRol = 2; // Default User
                if (cmbRoles.Visible && cmbRoles.SelectedValue != null)
                {
                    idRol = (int)cmbRoles.SelectedValue;
                }

                int result = usuarioBLL.Registrar(nuevo, idRol);
                if (result == -2)
                {
                    MessageBox.Show("El usuario ya existe.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (result != -1)
                {
                    MessageBox.Show("Registrado con éxito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Error al registrar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
