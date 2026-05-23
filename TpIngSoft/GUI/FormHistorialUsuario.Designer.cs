namespace TpIngSoft
{
    partial class FormHistorialUsuario
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
            this.cmbUsuarios = new System.Windows.Forms.ComboBox();
            this.btnCargarHistorial = new System.Windows.Forms.Button();
            this.dgvHistorial = new System.Windows.Forms.DataGridView();
            this.btnRestaurar = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHistorial)).BeginInit();
            this.SuspendLayout();
            // 
            // cmbUsuarios
            // 
            this.cmbUsuarios.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbUsuarios.FormattingEnabled = true;
            this.cmbUsuarios.Location = new System.Drawing.Point(12, 32);
            this.cmbUsuarios.Name = "cmbUsuarios";
            this.cmbUsuarios.Size = new System.Drawing.Size(200, 21);
            this.cmbUsuarios.TabIndex = 0;
            // 
            // btnCargarHistorial
            // 
            this.btnCargarHistorial.Location = new System.Drawing.Point(218, 30);
            this.btnCargarHistorial.Name = "btnCargarHistorial";
            this.btnCargarHistorial.Size = new System.Drawing.Size(100, 23);
            this.btnCargarHistorial.TabIndex = 1;
            this.btnCargarHistorial.Text = "Ver Historial";
            this.btnCargarHistorial.UseVisualStyleBackColor = true;
            this.btnCargarHistorial.Click += new System.EventHandler(this.btnCargarHistorial_Click);
            // 
            // dgvHistorial
            // 
            this.dgvHistorial.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvHistorial.Location = new System.Drawing.Point(12, 69);
            this.dgvHistorial.Name = "dgvHistorial";
            this.dgvHistorial.ReadOnly = true;
            this.dgvHistorial.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvHistorial.Size = new System.Drawing.Size(760, 300);
            this.dgvHistorial.TabIndex = 2;
            // 
            // btnRestaurar
            // 
            this.btnRestaurar.Location = new System.Drawing.Point(12, 375);
            this.btnRestaurar.Name = "btnRestaurar";
            this.btnRestaurar.Size = new System.Drawing.Size(150, 30);
            this.btnRestaurar.TabIndex = 3;
            this.btnRestaurar.Text = "Restaurar Estado";
            this.btnRestaurar.UseVisualStyleBackColor = true;
            this.btnRestaurar.Click += new System.EventHandler(this.btnRestaurar_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(102, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Seleccionar Usuario:";
            // 
            // FormHistorialUsuario
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 417);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnRestaurar);
            this.Controls.Add(this.dgvHistorial);
            this.Controls.Add(this.btnCargarHistorial);
            this.Controls.Add(this.cmbUsuarios);
            this.Name = "FormHistorialUsuario";
            this.Load += new System.EventHandler(this.FormHistorialUsuario_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvHistorial)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.ComboBox cmbUsuarios;
        private System.Windows.Forms.Button btnCargarHistorial;
        private System.Windows.Forms.DataGridView dgvHistorial;
        private System.Windows.Forms.Button btnRestaurar;
        private System.Windows.Forms.Label label1;
    }
}
