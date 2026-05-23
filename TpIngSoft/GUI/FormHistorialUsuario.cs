using System;
using System.Collections.Generic;
using System.Windows.Forms;
using BE;
using BLL;
using TpIngSoft.Traduccion;

namespace TpIngSoft
{
    public partial class FormHistorialUsuario : Form, IObservador
    {
        private UsuarioBLL usuarioBLL = new UsuarioBLL();
        private List<IControlTraducible> _controlesTraducibles = new List<IControlTraducible>();
        private DataGridTraducible _dgvTraducible;

        public FormHistorialUsuario()
        {
            InitializeComponent();
            RegistrarControlesTraducibles();
            GestorIdioma.Instancia.Adjuntar(this);
        }

        private void RegistrarControlesTraducibles()
        {
            _controlesTraducibles.Clear();
            _controlesTraducibles.Add(new EtiquetaTraducible(this, "FormHistorialUsuario", "Control de Cambios - Usuarios"));
            _controlesTraducibles.Add(new EtiquetaTraducible(label1, BE.NombreControl.FormHistorialUsuario_label1, "Seleccionar Usuario:"));
            _controlesTraducibles.Add(new EtiquetaTraducible(btnCargarHistorial, "FormHistorialUsuario.btnCargarHistorial", "Ver Historial"));
            _controlesTraducibles.Add(new EtiquetaTraducible(btnRestaurar, BE.NombreControl.FormHistorialUsuario_btnRestaurar, "Restaurar Estado"));

            _dgvTraducible = new DataGridTraducible(dgvHistorial, "FormHistorialUsuario.dgvHistorial")
                .ConColumna("IdHistorial", "FormHistorialUsuario.dgvHistorial.idHistorial", "ID Historial")
                .ConColumna("IdUsuario", "FormHistorialUsuario.dgvHistorial.idUsuario", "ID Usuario")
                .ConColumna("Nombre", BE.NombreControl.FormHistorialUsuario_dgvHistorial_usuario, "Nombre")
                .ConColumna("Activo", BE.NombreControl.FormHistorialUsuario_dgvHistorial_estado, "Estado Activo")
                .ConColumna("FechaCambio", BE.NombreControl.FormHistorialUsuario_dgvHistorial_fecha, "Fecha Cambio")
                .ConColumna("EditorNombre", "FormHistorialUsuario.dgvHistorial.editorNombre", "Modificado Por");
        }

        public void Actualizar(Dictionary<string, string> traducciones)
        {
            foreach (var control in _controlesTraducibles)
            {
                control.Traducir(traducciones);
            }
            _dgvTraducible.Traducir(traducciones);
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            GestorIdioma.Instancia.Separar(this);
            base.OnFormClosed(e);
        }

        private void FormHistorialUsuario_Load(object sender, EventArgs e)
        {
            RefrescarUsuarios();
        }

        private void RefrescarUsuarios()
        {
            cmbUsuarios.DataSource = null;
            cmbUsuarios.DataSource = usuarioBLL.LeerTodos();
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
                
                if (dgvHistorial.Columns["Password"] != null) dgvHistorial.Columns["Password"].Visible = false;
                if (dgvHistorial.Columns["DVH"] != null) dgvHistorial.Columns["DVH"].Visible = false;

                // Re-apply column translations
                var traducciones = GestorIdioma.Instancia.ObtenerTraduccionesActuales();
                _dgvTraducible.Traducir(traducciones);
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
