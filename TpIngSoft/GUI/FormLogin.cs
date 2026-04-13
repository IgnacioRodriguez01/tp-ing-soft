using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TpIngSoft
{
    public partial class FormLogin : Form
    {
        public FormLogin()
        {
            InitializeComponent();

            this.usuarioBLL = new BLL.Usuario();
            this.usuario = null;

            this.Text = "Login";
            label1.Text = "Login Usuario";
            label1.Left = (label1.Parent.Width - label1.Width) / 2;


            label4.Hide();
            this.Hide();
        }

        private BE.Usuario usuario;

        public BE.Usuario Usuario
        {
            get { return usuario; }
            set { usuario = value; }
        }

        private BLL.Usuario usuarioBLL;

        public BLL.Usuario UsuarioBLL
        {
            get { return usuarioBLL; }
            set { usuarioBLL = value; }
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            try
            {
                usuario = usuarioBLL.Login(textBoxNombre.Text, textBoxPass.Text);

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                this.label4.Text = ex.Message;
                label4.Left = (label4.Parent.Width - label4.Width) / 2;

                this.label4.Show();
            }
        }
    }
}
