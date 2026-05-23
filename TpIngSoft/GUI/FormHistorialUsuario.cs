using System;
using System.Collections.Generic;
using System.Windows.Forms;
using BE;
using BLL;

namespace TpIngSoft
{
    public partial class FormHistorialUsuario : Form
    {
        private UsuarioBLL usuarioBLL = new UsuarioBLL();
        private MapperUsuario mapperUsuario = new DAL.MapperUsuario();

        public FormHistorialUsuario()
        {
            InitializeComponent();
            this.Text = "Control de Cambios - Usuarios";
        }

        private void FormHistorialUsuario_Load(object sender, EventArgs e)
        {
            RefrescarUsuarios();
        }

        private void RefrescarUsuarios()
        {
            cmbUsuarios.DataSource = null;
            cmbUsuarios.DataSource = mapperUsuario.LeerTodos();
            cmbUsuarios.DisplayMember = "Nombre";
            cmbUsuarios.ValueMember = "Id";
        }

        private void btnCargarHistorial_Click(object sender, EventArgs e)
        {
            if (cmbUsuarios.SelectedValue != null)
            {
                int idUsuario = (int)cmbUsuarios.SelectedValue;
                dgvHistorial.DataSource = null;
                dgvHistorial.DataSource = usuarioBLL.LeerHistorial(idUsuario);
                
                // Formatear columnas
                if (dgvHistorial.Columns["Password"] != null) dgvHistorial.Columns["Password"].Visible = false;
                if (dgvHistorial.Columns["DVH"] != null) dgvHistorial.Columns["DVH"].Visible = false;
            }
        }

        private void btnRestaurar_Click(object sender, EventArgs e)
        {
            if (dgvHistorial.CurrentRow != null)
            {
                var historial = (UsuarioHistorial)dgvHistorial.CurrentRow.DataBoundItem;
                
                DialogResult dr = MessageBox.Show($"¿Desea restaurar al usuario '{historial.Nombre}' al estado del {historial.FechaCambio}?", 
                    "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (dr == DialogResult.Yes)
                {
                    try
                    {
                        usuarioBLL.Restaurar(historial.IdHistorial);
                        MessageBox.Show("Usuario restaurado con éxito. Se ha generado un nuevo registro en el historial y se han actualizado los DVs.", 
                            "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        btnCargarHistorial_Click(null, null); // Refrescar grilla
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al restaurar: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Seleccione un registro del historial.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
