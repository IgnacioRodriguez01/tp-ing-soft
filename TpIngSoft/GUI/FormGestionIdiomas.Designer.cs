namespace TpIngSoft
{
    partial class FormGestionIdiomas
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.dgvIdiomas = new System.Windows.Forms.DataGridView();
            this.txtNuevoIdioma = new System.Windows.Forms.TextBox();
            this.lblNuevoIdioma = new System.Windows.Forms.Label();
            this.btnCrearIdioma = new System.Windows.Forms.Button();
            this.cmbIdiomas = new System.Windows.Forms.ComboBox();
            this.lblSeleccionarIdioma = new System.Windows.Forms.Label();
            this.dgvTraducciones = new System.Windows.Forms.DataGridView();
            this.btnGuardarTraducciones = new System.Windows.Forms.Button();
            this.btnAplicar = new System.Windows.Forms.Button();
            
            ((System.ComponentModel.ISupportInitialize)(this.dgvIdiomas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTraducciones)).BeginInit();
            this.SuspendLayout();
            
            // 
            // dgvIdiomas
            // 
            this.dgvIdiomas.AllowUserToAddRows = false;
            this.dgvIdiomas.AllowUserToDeleteRows = false;
            this.dgvIdiomas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvIdiomas.Location = new System.Drawing.Point(20, 90);
            this.dgvIdiomas.Name = "dgvIdiomas";
            this.dgvIdiomas.ReadOnly = true;
            this.dgvIdiomas.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvIdiomas.Size = new System.Drawing.Size(260, 290);
            this.dgvIdiomas.TabIndex = 3;
            // 
            // txtNuevoIdioma
            // 
            this.txtNuevoIdioma.Location = new System.Drawing.Point(20, 45);
            this.txtNuevoIdioma.Name = "txtNuevoIdioma";
            this.txtNuevoIdioma.Size = new System.Drawing.Size(160, 20);
            this.txtNuevoIdioma.TabIndex = 1;
            // 
            // lblNuevoIdioma
            // 
            this.lblNuevoIdioma.AutoSize = true;
            this.lblNuevoIdioma.Location = new System.Drawing.Point(20, 25);
            this.lblNuevoIdioma.Name = "lblNuevoIdioma";
            this.lblNuevoIdioma.Size = new System.Drawing.Size(77, 13);
            this.lblNuevoIdioma.TabIndex = 0;
            this.lblNuevoIdioma.Text = "Nuevo Idioma:";
            // 
            // btnCrearIdioma
            // 
            this.btnCrearIdioma.Location = new System.Drawing.Point(190, 43);
            this.btnCrearIdioma.Name = "btnCrearIdioma";
            this.btnCrearIdioma.Size = new System.Drawing.Size(90, 23);
            this.btnCrearIdioma.TabIndex = 2;
            this.btnCrearIdioma.Text = "Crear";
            this.btnCrearIdioma.UseVisualStyleBackColor = true;
            this.btnCrearIdioma.Click += new System.EventHandler(this.btnCrearIdioma_Click);
            // 
            // cmbIdiomas
            // 
            this.cmbIdiomas.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbIdiomas.FormattingEnabled = true;
            this.cmbIdiomas.Location = new System.Drawing.Point(310, 45);
            this.cmbIdiomas.Name = "cmbIdiomas";
            this.cmbIdiomas.Size = new System.Drawing.Size(200, 21);
            this.cmbIdiomas.TabIndex = 5;
            this.cmbIdiomas.SelectedIndexChanged += new System.EventHandler(this.cmbIdiomas_SelectedIndexChanged);
            // 
            // lblSeleccionarIdioma
            // 
            this.lblSeleccionarIdioma.AutoSize = true;
            this.lblSeleccionarIdioma.Location = new System.Drawing.Point(310, 25);
            this.lblSeleccionarIdioma.Name = "lblSeleccionarIdioma";
            this.lblSeleccionarIdioma.Size = new System.Drawing.Size(99, 13);
            this.lblSeleccionarIdioma.TabIndex = 4;
            this.lblSeleccionarIdioma.Text = "Seleccionar Idioma:";
            // 
            // dgvTraducciones
            // 
            this.dgvTraducciones.AllowUserToAddRows = false;
            this.dgvTraducciones.AllowUserToDeleteRows = false;
            this.dgvTraducciones.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvTraducciones.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTraducciones.Location = new System.Drawing.Point(310, 90);
            this.dgvTraducciones.Name = "dgvTraducciones";
            this.dgvTraducciones.Size = new System.Drawing.Size(460, 290);
            this.dgvTraducciones.TabIndex = 6;
            // 
            // btnGuardarTraducciones
            // 
            this.btnGuardarTraducciones.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGuardarTraducciones.Location = new System.Drawing.Point(620, 395);
            this.btnGuardarTraducciones.Name = "btnGuardarTraducciones";
            this.btnGuardarTraducciones.Size = new System.Drawing.Size(150, 30);
            this.btnGuardarTraducciones.TabIndex = 8;
            this.btnGuardarTraducciones.Text = "Guardar Traducciones";
            this.btnGuardarTraducciones.UseVisualStyleBackColor = true;
            this.btnGuardarTraducciones.Click += new System.EventHandler(this.btnGuardarTraducciones_Click);
            // 
            // btnAplicar
            // 
            this.btnAplicar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAplicar.Location = new System.Drawing.Point(310, 395);
            this.btnAplicar.Name = "btnAplicar";
            this.btnAplicar.Size = new System.Drawing.Size(120, 30);
            this.btnAplicar.TabIndex = 7;
            this.btnAplicar.Text = "Aplicar Idioma";
            this.btnAplicar.UseVisualStyleBackColor = true;
            this.btnAplicar.Click += new System.EventHandler(this.btnAplicar_Click);
            // 
            // FormGestionIdiomas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(790, 440);
            this.Controls.Add(this.btnAplicar);
            this.Controls.Add(this.btnGuardarTraducciones);
            this.Controls.Add(this.dgvTraducciones);
            this.Controls.Add(this.lblSeleccionarIdioma);
            this.Controls.Add(this.cmbIdiomas);
            this.Controls.Add(this.btnCrearIdioma);
            this.Controls.Add(this.lblNuevoIdioma);
            this.Controls.Add(this.txtNuevoIdioma);
            this.Controls.Add(this.dgvIdiomas);
            this.Name = "FormGestionIdiomas";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Gestión de Idiomas";
            this.Load += new System.EventHandler(this.FormGestionIdiomas_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvIdiomas)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTraducciones)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.DataGridView dgvIdiomas;
        private System.Windows.Forms.TextBox txtNuevoIdioma;
        private System.Windows.Forms.Label lblNuevoIdioma;
        private System.Windows.Forms.Button btnCrearIdioma;
        private System.Windows.Forms.ComboBox cmbIdiomas;
        private System.Windows.Forms.Label lblSeleccionarIdioma;
        private System.Windows.Forms.DataGridView dgvTraducciones;
        private System.Windows.Forms.Button btnGuardarTraducciones;
        private System.Windows.Forms.Button btnAplicar;
    }
}
