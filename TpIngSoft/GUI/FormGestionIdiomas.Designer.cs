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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvIdiomas = new System.Windows.Forms.DataGridView();
            this.txtNuevoIdioma = new System.Windows.Forms.TextBox();
            this.lblNuevoIdioma = new System.Windows.Forms.Label();
            this.btnCrearIdioma = new System.Windows.Forms.Button();
            this.dgvTraducciones = new System.Windows.Forms.DataGridView();
            this.btnGuardarTraducciones = new System.Windows.Forms.Button();
            this.btnAplicar = new System.Windows.Forms.Button();
            this.btnToggleActivo = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvIdiomas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTraducciones)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvIdiomas
            // 
            this.dgvIdiomas.AllowUserToAddRows = false;
            this.dgvIdiomas.AllowUserToDeleteRows = false;
            this.dgvIdiomas.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(31)))), ((int)(((byte)(50)))));
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(89)))), ((int)(((byte)(75)))), ((int)(((byte)(160)))));
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(98)))), ((int)(((byte)(93)))), ((int)(((byte)(115)))));
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvIdiomas.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dgvIdiomas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(57)))), ((int)(((byte)(79)))));
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(134)))), ((int)(((byte)(129)))), ((int)(((byte)(158)))));
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvIdiomas.DefaultCellStyle = dataGridViewCellStyle6;
            this.dgvIdiomas.EnableHeadersVisualStyles = false;
            this.dgvIdiomas.GridColor = System.Drawing.Color.Black;
            this.dgvIdiomas.Location = new System.Drawing.Point(23, 90);
            this.dgvIdiomas.Name = "dgvIdiomas";
            this.dgvIdiomas.ReadOnly = true;
            this.dgvIdiomas.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvIdiomas.Size = new System.Drawing.Size(260, 290);
            this.dgvIdiomas.TabIndex = 3;
            this.dgvIdiomas.SelectionChanged += new System.EventHandler(this.dgvIdiomas_SelectionChanged);
            // 
            // txtNuevoIdioma
            // 
            this.txtNuevoIdioma.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(57)))), ((int)(((byte)(79)))));
            this.txtNuevoIdioma.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNuevoIdioma.ForeColor = System.Drawing.Color.White;
            this.txtNuevoIdioma.Location = new System.Drawing.Point(20, 45);
            this.txtNuevoIdioma.Name = "txtNuevoIdioma";
            this.txtNuevoIdioma.Size = new System.Drawing.Size(160, 20);
            this.txtNuevoIdioma.TabIndex = 1;
            // 
            // lblNuevoIdioma
            // 
            this.lblNuevoIdioma.BackColor = System.Drawing.Color.Transparent;
            this.lblNuevoIdioma.ForeColor = System.Drawing.Color.White;
            this.lblNuevoIdioma.Location = new System.Drawing.Point(20, 20);
            this.lblNuevoIdioma.Name = "lblNuevoIdioma";
            this.lblNuevoIdioma.Size = new System.Drawing.Size(160, 22);
            this.lblNuevoIdioma.TabIndex = 0;
            this.lblNuevoIdioma.Text = "Nuevo Idioma:";
            this.lblNuevoIdioma.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // btnCrearIdioma
            // 
            this.btnCrearIdioma.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(89)))), ((int)(((byte)(75)))), ((int)(((byte)(160)))));
            this.btnCrearIdioma.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCrearIdioma.ForeColor = System.Drawing.Color.White;
            this.btnCrearIdioma.Location = new System.Drawing.Point(190, 43);
            this.btnCrearIdioma.Name = "btnCrearIdioma";
            this.btnCrearIdioma.Size = new System.Drawing.Size(90, 23);
            this.btnCrearIdioma.TabIndex = 2;
            this.btnCrearIdioma.Text = "Crear";
            this.btnCrearIdioma.UseVisualStyleBackColor = false;
            this.btnCrearIdioma.Click += new System.EventHandler(this.btnCrearIdioma_Click);
            // 
            // dgvTraducciones
            // 
            this.dgvTraducciones.AllowUserToAddRows = false;
            this.dgvTraducciones.AllowUserToDeleteRows = false;
            this.dgvTraducciones.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvTraducciones.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(31)))), ((int)(((byte)(50)))));
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(89)))), ((int)(((byte)(75)))), ((int)(((byte)(160)))));
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(98)))), ((int)(((byte)(93)))), ((int)(((byte)(115)))));
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvTraducciones.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.dgvTraducciones.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(57)))), ((int)(((byte)(79)))));
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(134)))), ((int)(((byte)(129)))), ((int)(((byte)(158)))));
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvTraducciones.DefaultCellStyle = dataGridViewCellStyle8;
            this.dgvTraducciones.EnableHeadersVisualStyles = false;
            this.dgvTraducciones.GridColor = System.Drawing.Color.Black;
            this.dgvTraducciones.Location = new System.Drawing.Point(310, 90);
            this.dgvTraducciones.Name = "dgvTraducciones";
            this.dgvTraducciones.Size = new System.Drawing.Size(460, 290);
            this.dgvTraducciones.TabIndex = 6;
            // 
            // btnGuardarTraducciones
            // 
            this.btnGuardarTraducciones.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGuardarTraducciones.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(89)))), ((int)(((byte)(75)))), ((int)(((byte)(160)))));
            this.btnGuardarTraducciones.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnGuardarTraducciones.ForeColor = System.Drawing.Color.White;
            this.btnGuardarTraducciones.Location = new System.Drawing.Point(620, 395);
            this.btnGuardarTraducciones.Name = "btnGuardarTraducciones";
            this.btnGuardarTraducciones.Size = new System.Drawing.Size(150, 30);
            this.btnGuardarTraducciones.TabIndex = 8;
            this.btnGuardarTraducciones.Text = "Guardar Traducciones";
            this.btnGuardarTraducciones.UseVisualStyleBackColor = false;
            this.btnGuardarTraducciones.Click += new System.EventHandler(this.btnGuardarTraducciones_Click);
            // 
            // btnAplicar
            // 
            this.btnAplicar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAplicar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(89)))), ((int)(((byte)(75)))), ((int)(((byte)(160)))));
            this.btnAplicar.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnAplicar.ForeColor = System.Drawing.Color.White;
            this.btnAplicar.Location = new System.Drawing.Point(310, 395);
            this.btnAplicar.Name = "btnAplicar";
            this.btnAplicar.Size = new System.Drawing.Size(120, 30);
            this.btnAplicar.TabIndex = 7;
            this.btnAplicar.Text = "Aplicar Idioma";
            this.btnAplicar.UseVisualStyleBackColor = false;
            this.btnAplicar.Click += new System.EventHandler(this.btnAplicar_Click);
            // 
            // btnToggleActivo
            // 
            this.btnToggleActivo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnToggleActivo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(89)))), ((int)(((byte)(75)))), ((int)(((byte)(160)))));
            this.btnToggleActivo.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnToggleActivo.ForeColor = System.Drawing.Color.White;
            this.btnToggleActivo.Location = new System.Drawing.Point(20, 395);
            this.btnToggleActivo.Name = "btnToggleActivo";
            this.btnToggleActivo.Size = new System.Drawing.Size(140, 30);
            this.btnToggleActivo.TabIndex = 9;
            this.btnToggleActivo.Text = "Activar / Desactivar";
            this.btnToggleActivo.UseVisualStyleBackColor = false;
            this.btnToggleActivo.Click += new System.EventHandler(this.btnToggleActivo_Click);
            // 
            // FormGestionIdiomas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::TpIngSoft.Properties.Resources.mesh_bg;
            this.ClientSize = new System.Drawing.Size(790, 440);
            this.Controls.Add(this.btnToggleActivo);
            this.Controls.Add(this.btnAplicar);
            this.Controls.Add(this.btnGuardarTraducciones);
            this.Controls.Add(this.dgvTraducciones);
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
        private System.Windows.Forms.DataGridView dgvTraducciones;
        private System.Windows.Forms.Button btnGuardarTraducciones;
        private System.Windows.Forms.Button btnAplicar;
        private System.Windows.Forms.Button btnToggleActivo;
    }
}
