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
            _controlesTraducibles.Add(new EtiquetaTraducible(this, "FormGestionIdiomas", "Gestión de Idiomas"));
            _controlesTraducibles.Add(new EtiquetaTraducible(lblNuevoIdioma, BE.NombreControl.FormGestionIdiomas_lblNuevoIdioma, "Nuevo Idioma:"));
            _controlesTraducibles.Add(new EtiquetaTraducible(btnCrearIdioma, BE.NombreControl.FormGestionIdiomas_btnCrearIdioma, "Crear"));
            _controlesTraducibles.Add(new EtiquetaTraducible(lblSeleccionarIdioma, BE.NombreControl.FormGestionIdiomas_lblSeleccionarIdioma, "Seleccionar Idioma:"));
            _controlesTraducibles.Add(new EtiquetaTraducible(btnGuardarTraducciones, BE.NombreControl.FormGestionIdiomas_btnGuardarTraducciones, "Guardar Traducciones"));
            _controlesTraducibles.Add(new EtiquetaTraducible(btnAplicar, "FormGestionIdiomas.btnAplicar", "Aplicar Idioma"));

            _dgvIdiomasTraducible = new DataGridTraducible(dgvIdiomas, "FormGestionIdiomas.dgvIdiomas")
                .ConColumna("Id", BE.NombreControl.FormGestionIdiomas_dgvIdiomas_id, "ID")
                .ConColumna("Nombre", BE.NombreControl.FormGestionIdiomas_dgvIdiomas_nombre, "Nombre")
                .ConColumna("Activo", BE.NombreControl.FormGestionIdiomas_dgvIdiomas_activo, "Activo");

            _dgvTraduccionesTraducible = new DataGridTraducible(dgvTraducciones, "FormGestionIdiomas.dgvTraducciones")
                .ConColumna("Formulario", BE.NombreControl.FormGestionIdiomas_dgvTraducciones_formulario, "Formulario")
                .ConColumna("Control", BE.NombreControl.FormGestionIdiomas_dgvTraducciones_control, "Control")
                .ConColumna("Texto", BE.NombreControl.FormGestionIdiomas_dgvTraducciones_texto, "Texto");
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
                ("FormGestionPerfiles", "grpAcciones"),
                ("FormGestionPerfiles", "lblDetalle"),
                ("FormGestionPerfiles", "lblModo"),
                ("FormGestionPerfiles", "rbModoCrear"),
                ("FormGestionPerfiles", "rbModoEditar"),
                ("FormGestionPerfiles", "lblNombreRol"),
                ("FormGestionPerfiles", "btnCrearRolRaiz"),
                ("FormGestionPerfiles", "btnCrearSubRol"),
                ("FormGestionPerfiles", "btnEditarNombre"),
                ("FormGestionPerfiles", "btnEliminarRol"),
                ("FormGestionPerfiles", "grpAsignarPermiso"),
                ("FormGestionPerfiles", "btnAsignarPermiso"),
                ("FormGestionPerfiles", "btnQuitarItem"),
                ("FormHistorialUsuario", "label1"),
                ("FormHistorialUsuario", "btnRestaurar"),
                ("FormHistorialUsuario", "dgvHistorial.fecha"),
                ("FormHistorialUsuario", "dgvHistorial.usuario"),
                ("FormHistorialUsuario", "dgvHistorial.estado"),
                ("FormGestionIdiomas", "lblNuevoIdioma"),
                ("FormGestionIdiomas", "btnCrearIdioma"),
                ("FormGestionIdiomas", "btnToggleActivo"),
                ("FormGestionIdiomas", "lblSeleccionarIdioma"),
                ("FormGestionIdiomas", "btnGuardarTraducciones"),
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
                var activeLanguages = _idiomaBLL.ObtenerIdiomasActivos();
                
                dgvIdiomas.DataSource = null;
                dgvIdiomas.DataSource = activeLanguages;
                
                cmbIdiomas.DataSource = null;
                cmbIdiomas.DataSource = activeLanguages;
                cmbIdiomas.DisplayMember = "Nombre";
                cmbIdiomas.ValueMember = "Id";

                if (GestorIdioma.Instancia.IdiomaActual != null && cmbIdiomas.Items.Count > 0)
                {
                    cmbIdiomas.SelectedValue = GestorIdioma.Instancia.IdiomaActual.Id;
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
            if (cmbIdiomas.SelectedValue == null) return;
            int idIdioma = (int)cmbIdiomas.SelectedValue;

            List<BE.Traduccion> todosControles = _idiomaBLL.ObtenerControles();
            var cache = _idiomaBLL.ObtenerTraduccionesCacheadas(idIdioma);

            List<RenglonTraduccion> renglones = new List<RenglonTraduccion>();
            foreach (var control in todosControles)
            {
                string key = $"{control.Formulario}.{control.NombreControl}";
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

        private void cmbIdiomas_SelectedIndexChanged(object sender, EventArgs e)
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
                if (cmbIdiomas.SelectedValue == null) return;
                int idIdioma = (int)cmbIdiomas.SelectedValue;

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
            if (cmbIdiomas.SelectedItem is Idioma seleccionado)
            {
                GestorIdioma.Instancia.CambiarIdioma(seleccionado);
                MessageBox.Show($"Idioma '{seleccionado.Nombre}' aplicado al sistema.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
