using System;
using System.Collections.Generic;
using System.Windows.Forms;
using BLL;
using BE;

namespace TpIngSoft
{
    public partial class FormBitacora : Form
    {
        private GestorBitacora gestor = GestorBitacora.Instance;

        public FormBitacora()
        {
            InitializeComponent();
        }

        private void FormBitacora_Load(object sender, EventArgs e)
        {
            dtpDesde.Value = DateTime.Now.AddDays(-7);
            dtpHasta.Value = DateTime.Now;
            CargarDatos();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            CargarDatos();
        }

        private void CargarDatos()
        {
            try
            {
                string filtroActividad = string.IsNullOrWhiteSpace(txtActividad.Text) ? null : txtActividad.Text;
                string filtroUsuario = string.IsNullOrWhiteSpace(txtUsuario.Text) ? null : txtUsuario.Text;
                
                DateTime desde = dtpDesde.Value.Date;
                DateTime hasta = dtpHasta.Value.Date.AddDays(1).AddSeconds(-1);

                List<Bitacora> lista = gestor.BuscarEventos(null, filtroActividad, desde, hasta);
                
                if (!string.IsNullOrEmpty(filtroUsuario))
                {
                    lista = lista.FindAll(x => x.NombreUsuario.ToLower().Contains(filtroUsuario.ToLower()));
                }

                dgvBitacora.DataSource = null;
                dgvBitacora.DataSource = lista;
                
                if (dgvBitacora.Columns["Usuario"] != null) dgvBitacora.Columns["Usuario"].Visible = false;
                if (dgvBitacora.Columns["Id"] != null) dgvBitacora.Columns["Id"].Width = 50;
                if (dgvBitacora.Columns["FechaHora"] != null) dgvBitacora.Columns["FechaHora"].Width = 120;
                if (dgvBitacora.Columns["NombreUsuario"] != null) dgvBitacora.Columns["NombreUsuario"].Width = 100;
                if (dgvBitacora.Columns["Actividad"] != null) dgvBitacora.Columns["Actividad"].Width = 150;
                if (dgvBitacora.Columns["InfoAsociada"] != null) dgvBitacora.Columns["InfoAsociada"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar bitácora: " + ex.Message);
            }

        }
    }
}
