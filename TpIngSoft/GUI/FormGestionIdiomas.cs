using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using BE;
using BLL;
using TpIngSoft.Traduccion;

namespace TpIngSoft
{
    public partial class FormGestionIdiomas : Form, IObservador
    {
        private readonly IdiomaBLL _idiomaBLL = new IdiomaBLL();
        private List<IControlTraducible> _controlesTraducibles = new List<IControlTraducible>();
        private DataGridTraducible _dgvIdiomasTraducible;
        private DataGridTraducible _dgvTraduccionesTraducible;
        private bool _isChangingLanguage = false;

        public FormGestionIdiomas()
        {
            InitializeComponent();
            RegistrarControlesTraducibles();
            GestorIdioma.Instancia.Adjuntar(this);
        }

        private void RegistrarControlesTraducibles()
        {
            _controlesTraducibles.Clear();
            _controlesTraducibles.Add(new EtiquetaTraducible(this, "FormGestionIdiomas"));
            _controlesTraducibles.Add(new EtiquetaTraducible(lblNuevoIdioma, BE.NombreControl.FormGestionIdiomas_lblNuevoIdioma));
            _controlesTraducibles.Add(new EtiquetaTraducible(btnCrearIdioma, BE.NombreControl.FormGestionIdiomas_btnCrearIdioma));
            _controlesTraducibles.Add(new EtiquetaTraducible(btnGuardarTraducciones, BE.NombreControl.FormGestionIdiomas_btnGuardarTraducciones));
            _controlesTraducibles.Add(new EtiquetaTraducible(btnAplicar, BE.NombreControl.FormGestionIdiomas_btnAplicar));
            _controlesTraducibles.Add(new EtiquetaTraducible(btnEliminarIdioma, BE.NombreControl.FormGestionIdiomas_btnEliminarIdioma));

            _dgvIdiomasTraducible = new DataGridTraducible(dgvIdiomas, "FormGestionIdiomas.dgvIdiomas")
                .ConColumna("Id", BE.NombreControl.FormGestionIdiomas_dgvIdiomas_id)
                .ConColumna("Nombre", BE.NombreControl.FormGestionIdiomas_dgvIdiomas_nombre)
                .ConColumna("Activo", BE.NombreControl.FormGestionIdiomas_dgvIdiomas_activo);

            _dgvTraduccionesTraducible = new DataGridTraducible(dgvTraducciones, "FormGestionIdiomas.dgvTraducciones")
                .ConColumna("Formulario", BE.NombreControl.FormGestionIdiomas_dgvTraducciones_formulario)
                .ConColumna("Control", BE.NombreControl.FormGestionIdiomas_dgvTraducciones_control)
                .ConColumna("Texto", BE.NombreControl.FormGestionIdiomas_dgvTraducciones_texto);
        }

        public void Actualizar(Dictionary<string, string> traducciones)
        {
            foreach (var control in _controlesTraducibles)
            {
                control.Traducir(traducciones);
            }
            _dgvIdiomasTraducible.Traducir(traducciones);
            _dgvTraduccionesTraducible.Traducir(traducciones);
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            GestorIdioma.Instancia.Separar(this);
            base.OnFormClosed(e);
        }

        private void FormGestionIdiomas_Load(object sender, EventArgs e)
        {
            InicializarControlesEnBD();
            RefrescarIdiomas();
        }

        private void InicializarControlesEnBD()
        {
            var list = new List<(string Form, string Ctrl)>
            {
                ("FormLogin", "(form)"),
                ("FormMain", "(form)"),
                ("FormGestionUsuarios", "(form)"),
                ("FormBitacora", "(form)"),
                ("FormGestionRoles", "(form)"),
                ("FormHistorialUsuario", "(form)"),
                ("FormGestionIdiomas", "(form)"),

                ("FormLogin", "label1"),
                ("FormLogin", "label2"),
                ("FormLogin", "label3"),
                ("FormLogin", "buttonLogin"),
                ("FormLogin", "labelIdioma"),
                ("FormMain", "archivoToolStripMenuItem"),
                ("FormMain", "logoutToolStripMenuItem"),
                ("FormMain", "salirToolStripMenuItem"),
                ("FormMain", "adminToolStripMenuItem"),
                ("FormMain", "gestionUsuariosToolStripMenuItem"),
                ("FormMain", "bitacoraToolStripMenuItem"),
                ("FormMain", "controlCambiosToolStripMenuItem"),
                ("FormMain", "gestionPerfilesToolStripMenuItem"),
                ("FormMain", "gestionIdiomasToolStripMenuItem"),
                ("FormMain", "lblSesionInfo"),
                ("FormGestionUsuarios", "label1"),
                ("FormGestionUsuarios", "label2"),
                ("FormGestionUsuarios", "lblRol"),
                ("FormGestionUsuarios", "btnRegistrar"),
                ("FormBitacora", "labelDesde"),
                ("FormBitacora", "labelHasta"),
                ("FormBitacora", "labelActividad"),
                ("FormBitacora", "labelUsuario"),
                ("FormBitacora", "btnBuscar"),
                ("FormBitacora", "dgvBitacora.id"),
                ("FormBitacora", "dgvBitacora.fecha"),
                ("FormBitacora", "dgvBitacora.usuario"),
                ("FormBitacora", "dgvBitacora.descripcion"),
                ("FormBitacora", "dgvBitacora.criticidad"),
                ("FormGestionRoles", "grpAcciones"),
                ("FormGestionRoles", "lblDetalle"),
                ("FormGestionRoles", "lblModo"),
                ("FormGestionRoles", "rbModoCrear"),
                ("FormGestionRoles", "rbModoEditar"),
                ("FormGestionRoles", "lblNombreRol"),
                ("FormGestionRoles", "btnCrearRolRaiz"),
                ("FormGestionRoles", "btnCrearSubRol"),
                ("FormGestionRoles", "btnEditarNombre"),
                ("FormGestionRoles", "btnEliminarRol"),
                ("FormGestionRoles", "grpAsignarPermiso"),
                ("FormGestionRoles", "btnAsignarPermiso"),
                ("FormHistorialUsuario", "(form)"),
                ("FormHistorialUsuario", "label1"),
                ("FormHistorialUsuario", "btnCargarHistorial"),
                ("FormHistorialUsuario", "btnRestaurar"),
                ("FormHistorialUsuario", "dgvHistorial.idHistorial"),
                ("FormHistorialUsuario", "dgvHistorial.idUsuario"),
                ("FormHistorialUsuario", "dgvHistorial.fecha"),
                ("FormHistorialUsuario", "dgvHistorial.usuario"),
                ("FormHistorialUsuario", "dgvHistorial.estado"),
                ("FormHistorialUsuario", "dgvHistorial.editorNombre"),
                ("FormGestionIdiomas", "(form)"),
                ("FormGestionIdiomas", "lblNuevoIdioma"),
                ("FormGestionIdiomas", "btnCrearIdioma"),
                ("FormGestionIdiomas", "btnToggleActivo"),
                ("FormGestionIdiomas", "btnGuardarTraducciones"),
                ("FormGestionIdiomas", "btnAplicar"),
                ("FormGestionIdiomas", "btnEliminarIdioma"),
                ("FormGestionIdiomas", "dgvIdiomas.id"),
                ("FormGestionIdiomas", "dgvIdiomas.nombre"),
                ("FormGestionIdiomas", "dgvIdiomas.activo"),
                ("FormGestionIdiomas", "dgvTraducciones.formulario"),
                ("FormGestionIdiomas", "dgvTraducciones.control"),
                ("FormGestionIdiomas", "dgvTraducciones.texto")
            };

            foreach (var item in list)
            {
                _idiomaBLL.RegistrarControl(item.Ctrl, item.Form);
            }
        }

        private void RefrescarIdiomas()
        {
            _isChangingLanguage = true;
            try
            {
                var todosIdiomas = _idiomaBLL.ObtenerTodosIdiomas();
                var activeLanguages = _idiomaBLL.ObtenerIdiomasActivos();
                
                dgvIdiomas.DataSource = null;
                dgvIdiomas.DataSource = todosIdiomas;

                if (GestorIdioma.Instancia.IdiomaActual != null && dgvIdiomas.Rows.Count > 0)
                {
                    foreach (DataGridViewRow row in dgvIdiomas.Rows)
                    {
                        var id = (Idioma)row.DataBoundItem;
                        if (id.Id == GestorIdioma.Instancia.IdiomaActual.Id)
                        {
                            row.Selected = true;
                            dgvIdiomas.CurrentCell = row.Cells[0];
                            break;
                        }
                    }
                }

                var trans = GestorIdioma.Instancia.ObtenerTraduccionesActuales();
                _dgvIdiomasTraducible.Traducir(trans);
            }
            finally
            {
                _isChangingLanguage = false;
            }

            CargarTraducciones();
        }

        private void CargarTraducciones()
        {
            if (dgvIdiomas.CurrentRow == null) return;
            var idiomaSeleccionado = (Idioma)dgvIdiomas.CurrentRow.DataBoundItem;
            int idIdioma = idiomaSeleccionado.Id;

            List<BE.Traduccion> todosControles = _idiomaBLL.ObtenerControles();
            var cache = _idiomaBLL.ObtenerTraduccionesCacheadas(idIdioma);

            List<RenglonTraduccion> renglones = new List<RenglonTraduccion>();
            foreach (var control in todosControles)
            {
                string key = (control.NombreControl == "(form)" || string.IsNullOrEmpty(control.NombreControl))
                    ? control.Formulario
                    : $"{control.Formulario}.{control.NombreControl}";
                string texto = "";
                if (cache.TryGetValue(key, out string t))
                {
                    texto = t;
                }

                renglones.Add(new RenglonTraduccion
                {
                    IdControl = control.IdControl,
                    Formulario = control.Formulario,
                    Control = control.NombreControl,
                    Texto = texto
                });
            }

            dgvTraducciones.DataSource = null;
            dgvTraducciones.DataSource = new BindingList<RenglonTraduccion>(renglones);

            if (dgvTraducciones.Columns["IdControl"] != null) dgvTraducciones.Columns["IdControl"].Visible = false;
            if (dgvTraducciones.Columns["Formulario"] != null) dgvTraducciones.Columns["Formulario"].ReadOnly = true;
            if (dgvTraducciones.Columns["Control"] != null) dgvTraducciones.Columns["Control"].ReadOnly = true;
            if (dgvTraducciones.Columns["Texto"] != null) dgvTraducciones.Columns["Texto"].Width = 200;

            var trans = GestorIdioma.Instancia.ObtenerTraduccionesActuales();
            _dgvTraduccionesTraducible.Traducir(trans);
        }

        private void dgvIdiomas_SelectionChanged(object sender, EventArgs e)
        {
            if (_isChangingLanguage) return;
            CargarTraducciones();
        }

        private void btnCrearIdioma_Click(object sender, EventArgs e)
        {
            try
            {
                string nombre = txtNuevoIdioma.Text.Trim();
                if (string.IsNullOrEmpty(nombre))
                {
                    MessageBox.Show("Por favor ingrese un nombre.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int id = _idiomaBLL.CrearIdioma(nombre);
                if (id != -1)
                {
                    MessageBox.Show("Idioma creado con éxito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtNuevoIdioma.Clear();
                    RefrescarIdiomas();
                }
                else
                {
                    MessageBox.Show("Error al crear el idioma.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnGuardarTraducciones_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvIdiomas.CurrentRow == null) return;
                var idiomaSeleccionado = (Idioma)dgvIdiomas.CurrentRow.DataBoundItem;
                int idIdioma = idiomaSeleccionado.Id;

                var bindingList = dgvTraducciones.DataSource as BindingList<RenglonTraduccion>;
                if (bindingList != null)
                {
                    foreach (var renglon in bindingList)
                    {
                        _idiomaBLL.GuardarTraduccion(renglon.IdControl, idIdioma, renglon.Texto);
                    }
                    MessageBox.Show("Traducciones guardadas con éxito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                    if (GestorIdioma.Instancia.IdiomaActual != null && GestorIdioma.Instancia.IdiomaActual.Id == idIdioma)
                    {
                        GestorIdioma.Instancia.CambiarIdioma(GestorIdioma.Instancia.IdiomaActual);
                    }
                    
                    CargarTraducciones();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAplicar_Click(object sender, EventArgs e)
        {
            if (dgvIdiomas.CurrentRow != null)
            {
                var seleccionado = (Idioma)dgvIdiomas.CurrentRow.DataBoundItem;
                GestorIdioma.Instancia.CambiarIdioma(seleccionado);
                
                if (SERVICIOS.SessionManager.Instance.EstaLogueado())
                {
                    _idiomaBLL.GuardarIdiomaUsuario(SERVICIOS.SessionManager.Instance.UsuarioActual.Id, seleccionado.Id);
                    SERVICIOS.SessionManager.Instance.UsuarioActual.IdIdioma = seleccionado.Id;
                }

                MessageBox.Show($"Idioma '{seleccionado.Nombre}' aplicado al sistema.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnToggleActivo_Click(object sender, EventArgs e)
        {
            if (dgvIdiomas.CurrentRow != null)
            {
                var idiomaSeleccionado = (Idioma)dgvIdiomas.CurrentRow.DataBoundItem;
                try
                {
                    // Check if we are deactivating the currently active language
                    bool isCurrentlyActiveSessionLanguage = GestorIdioma.Instancia.IdiomaActual != null && 
                                                            GestorIdioma.Instancia.IdiomaActual.Id == idiomaSeleccionado.Id;

                    _idiomaBLL.ToggleEstadoIdioma(idiomaSeleccionado.Id);
                    MessageBox.Show($"El estado del idioma '{idiomaSeleccionado.Nombre}' ha sido modificado.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    if (isCurrentlyActiveSessionLanguage)
                    {
                        // The active language was deactivated! Default to "Español" (ID 1) if active, else any other active
                        var todos = _idiomaBLL.ObtenerTodosIdiomas();
                        var espanol = todos.Find(i => i.Nombre.Equals("Español", StringComparison.OrdinalIgnoreCase) && i.Activo);
                        if (espanol == null)
                        {
                            var activos = _idiomaBLL.ObtenerIdiomasActivos();
                            if (activos.Count > 0)
                            {
                                espanol = activos[0];
                            }
                        }

                        if (espanol != null)
                        {
                            GestorIdioma.Instancia.CambiarIdioma(espanol);
                            if (SERVICIOS.SessionManager.Instance.EstaLogueado())
                            {
                                _idiomaBLL.GuardarIdiomaUsuario(SERVICIOS.SessionManager.Instance.UsuarioActual.Id, espanol.Id);
                                SERVICIOS.SessionManager.Instance.UsuarioActual.IdIdioma = espanol.Id;
                            }
                            MessageBox.Show($"El idioma activo fue desactivado. Se ha cambiado automáticamente a '{espanol.Nombre}'.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }

                    RefrescarIdiomas();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al cambiar el estado: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Seleccione un idioma de la grilla para cambiar su estado.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnEliminarIdioma_Click(object sender, EventArgs e)
        {
            if (dgvIdiomas.CurrentRow != null)
            {
                var seleccionado = (Idioma)dgvIdiomas.CurrentRow.DataBoundItem;
                try
                {
                    // Validation 1: Check if there's only 1 language in the system.
                    var todos = _idiomaBLL.ObtenerTodosIdiomas();
                    if (todos.Count <= 1)
                    {
                        MessageBox.Show("No se puede eliminar el único idioma del sistema.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // Validation 2: Check if it's the active session language.
                    if (GestorIdioma.Instancia.IdiomaActual != null && GestorIdioma.Instancia.IdiomaActual.Id == seleccionado.Id)
                    {
                        MessageBox.Show("No se puede eliminar el idioma activo en la sesión.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // Request confirmation
                    var result = MessageBox.Show($"¿Está seguro que desea eliminar el idioma '{seleccionado.Nombre}' y todas sus traducciones asociadas?", "Confirmar Eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        _idiomaBLL.EliminarIdioma(seleccionado.Id);
                        MessageBox.Show($"Idioma '{seleccionado.Nombre}' eliminado con éxito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        RefrescarIdiomas();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al eliminar el idioma: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Seleccione un idioma de la grilla para eliminar.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }

    public class RenglonTraduccion
    {
        public int IdControl { get; set; }
        public string Formulario { get; set; }
        public string Control { get; set; }
        public string Texto { get; set; }
    }
}
