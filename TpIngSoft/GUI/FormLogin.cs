using System;
using System.Windows.Forms;
using BLL;

namespace TpIngSoft
{
    public partial class FormLogin : Form
    {
        private UsuarioBLL usuarioBLL = new UsuarioBLL();

        public FormLogin()
        {
            InitializeComponent();
            this.Text = "Acceso al Sistema";
            label1.Text = "Login de Usuario";
            label4.Visible = false;
            textBoxPass.PasswordChar = '*';
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            try
            {
                string nombre = textBoxNombre.Text.Trim();
                string pass = textBoxPass.Text.Trim();

                if (string.IsNullOrEmpty(nombre) || string.IsNullOrEmpty(pass))
                {
                    MostrarError("Por favor, complete todos los campos.");
                    return;
                }

                if (usuarioBLL.Login(nombre, pass))
                {
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MostrarError("Usuario o contraseña incorrectos.");
                }
            }
            catch (Exception ex)
            {
                GestorBitacora.Instance.RegistrarEvento(null, "Error Crítico", "Error en login: " + ex.Message);
                MostrarError("Error: " + ex.Message);
            }

        }

        private void MostrarError(string mensaje)
        {
            label4.Text = mensaje;
            label4.Visible = true;
            label4.Left = (this.ClientSize.Width - label4.Width) / 2;
        }
    }
}
