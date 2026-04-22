using System;
using System.Windows.Forms;
using BE;
using BLL;

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

                int id = usuarioBLL.Registrar(nuevo);
                if (id != -1)
                {
                    MessageBox.Show($"Usuario registrado con éxito. ID: {id}", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Error al registrar el usuario.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
