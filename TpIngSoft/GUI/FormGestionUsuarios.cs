using System;
using System.Windows.Forms;
using BE;
using BLL;
using SERVICIOS;

namespace TpIngSoft
{
    public partial class FormGestionUsuarios : Form
    {
        private UsuarioBLL usuarioBLL = new UsuarioBLL();

        public FormGestionUsuarios()
        {
            InitializeComponent();
            this.Text = "Gestión de Usuarios";
        }

        private void FormGestionUsuarios_Load(object sender, EventArgs e)
        {
            if (SERVICIOS.SessionManager.Instance.HasPermission("AccesoAdmin"))
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
