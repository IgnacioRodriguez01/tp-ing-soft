namespace TpIngSoft
{
    partial class FormGestionPerfiles
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

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.tvPerfiles = new System.Windows.Forms.TreeView();
            this.grpAcciones = new System.Windows.Forms.GroupBox();
            this.btnQuitarItem = new System.Windows.Forms.Button();
            this.grpAsignarPermiso = new System.Windows.Forms.GroupBox();
            this.btnAsignarPermiso = new System.Windows.Forms.Button();
            this.cmbPermisos = new System.Windows.Forms.ComboBox();
            this.btnEliminarRol = new System.Windows.Forms.Button();
            this.btnEditarNombre = new System.Windows.Forms.Button();
            this.btnCrearSubRol = new System.Windows.Forms.Button();
            this.btnCrearRolRaiz = new System.Windows.Forms.Button();
            this.txtNombreRol = new System.Windows.Forms.TextBox();
            this.lblNombreRol = new System.Windows.Forms.Label();
            this.lblDetalle = new System.Windows.Forms.Label();
            this.grpAcciones.SuspendLayout();
            this.grpAsignarPermiso.SuspendLayout();
            this.SuspendLayout();
            // 
            // tvPerfiles
            // 
            this.tvPerfiles.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.tvPerfiles.Location = new System.Drawing.Point(12, 12);
            this.tvPerfiles.Name = "tvPerfiles";
            this.tvPerfiles.Size = new System.Drawing.Size(350, 436);
            this.tvPerfiles.TabIndex = 0;
            this.tvPerfiles.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvPerfiles_AfterSelect);
            // 
            // grpAcciones
            // 
            this.grpAcciones.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpAcciones.Controls.Add(this.btnQuitarItem);
            this.grpAcciones.Controls.Add(this.grpAsignarPermiso);
            this.grpAcciones.Controls.Add(this.btnEliminarRol);
            this.grpAcciones.Controls.Add(this.btnEditarNombre);
            this.grpAcciones.Controls.Add(this.btnCrearSubRol);
            this.grpAcciones.Controls.Add(this.btnCrearRolRaiz);
            this.grpAcciones.Controls.Add(this.txtNombreRol);
            this.grpAcciones.Controls.Add(this.lblNombreRol);
            this.grpAcciones.Controls.Add(this.lblDetalle);
            this.grpAcciones.Location = new System.Drawing.Point(378, 12);
            this.grpAcciones.Name = "grpAcciones";
            this.grpAcciones.Size = new System.Drawing.Size(394, 436);
            this.grpAcciones.TabIndex = 1;
            this.grpAcciones.TabStop = false;
            this.grpAcciones.Text = "Acciones";
            // 
            // btnQuitarItem
            // 
            this.btnQuitarItem.Location = new System.Drawing.Point(15, 300);
            this.btnQuitarItem.Name = "btnQuitarItem";
            this.btnQuitarItem.Size = new System.Drawing.Size(364, 30);
            this.btnQuitarItem.TabIndex = 8;
            this.btnQuitarItem.Text = "Quitar Ítem Seleccionado del Padre";
            this.btnQuitarItem.UseVisualStyleBackColor = true;
            this.btnQuitarItem.Click += new System.EventHandler(this.btnQuitarItem_Click);
            // 
            // grpAsignarPermiso
            // 
            this.grpAsignarPermiso.Controls.Add(this.btnAsignarPermiso);
            this.grpAsignarPermiso.Controls.Add(this.cmbPermisos);
            this.grpAsignarPermiso.Location = new System.Drawing.Point(15, 180);
            this.grpAsignarPermiso.Name = "grpAsignarPermiso";
            this.grpAsignarPermiso.Size = new System.Drawing.Size(364, 100);
            this.grpAsignarPermiso.TabIndex = 7;
            this.grpAsignarPermiso.TabStop = false;
            this.grpAsignarPermiso.Text = "Asignar Permiso";
            // 
            // btnAsignarPermiso
            // 
            this.btnAsignarPermiso.Location = new System.Drawing.Point(15, 60);
            this.btnAsignarPermiso.Name = "btnAsignarPermiso";
            this.btnAsignarPermiso.Size = new System.Drawing.Size(150, 23);
            this.btnAsignarPermiso.TabIndex = 1;
            this.btnAsignarPermiso.Text = "Asignar Permiso";
            this.btnAsignarPermiso.UseVisualStyleBackColor = true;
            this.btnAsignarPermiso.Click += new System.EventHandler(this.btnAsignarPermiso_Click);
            // 
            // cmbPermisos
            // 
            this.cmbPermisos.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPermisos.FormattingEnabled = true;
            this.cmbPermisos.Location = new System.Drawing.Point(15, 30);
            this.cmbPermisos.Name = "cmbPermisos";
            this.cmbPermisos.Size = new System.Drawing.Size(334, 21);
            this.cmbPermisos.TabIndex = 0;
            // 
            // btnEliminarRol
            // 
            this.btnEliminarRol.Location = new System.Drawing.Point(204, 140);
            this.btnEliminarRol.Name = "btnEliminarRol";
            this.btnEliminarRol.Size = new System.Drawing.Size(175, 23);
            this.btnEliminarRol.TabIndex = 6;
            this.btnEliminarRol.Text = "Eliminar Rol";
            this.btnEliminarRol.UseVisualStyleBackColor = true;
            this.btnEliminarRol.Click += new System.EventHandler(this.btnEliminarRol_Click);
            // 
            // btnEditarNombre
            // 
            this.btnEditarNombre.Location = new System.Drawing.Point(15, 140);
            this.btnEditarNombre.Name = "btnEditarNombre";
            this.btnEditarNombre.Size = new System.Drawing.Size(175, 23);
            this.btnEditarNombre.TabIndex = 5;
            this.btnEditarNombre.Text = "Editar Nombre";
            this.btnEditarNombre.UseVisualStyleBackColor = true;
            this.btnEditarNombre.Click += new System.EventHandler(this.btnEditarNombre_Click);
            // 
            // btnCrearSubRol
            // 
            this.btnCrearSubRol.Location = new System.Drawing.Point(204, 110);
            this.btnCrearSubRol.Name = "btnCrearSubRol";
            this.btnCrearSubRol.Size = new System.Drawing.Size(175, 23);
            this.btnCrearSubRol.TabIndex = 4;
            this.btnCrearSubRol.Text = "Crear Sub-Rol";
            this.btnCrearSubRol.UseVisualStyleBackColor = true;
            this.btnCrearSubRol.Click += new System.EventHandler(this.btnCrearSubRol_Click);
            // 
            // btnCrearRolRaiz
            // 
            this.btnCrearRolRaiz.Location = new System.Drawing.Point(15, 110);
            this.btnCrearRolRaiz.Name = "btnCrearRolRaiz";
            this.btnCrearRolRaiz.Size = new System.Drawing.Size(175, 23);
            this.btnCrearRolRaiz.TabIndex = 3;
            this.btnCrearRolRaiz.Text = "Crear Rol Raíz";
            this.btnCrearRolRaiz.UseVisualStyleBackColor = true;
            this.btnCrearRolRaiz.Click += new System.EventHandler(this.btnCrearRolRaiz_Click);
            // 
            // txtNombreRol
            // 
            this.txtNombreRol.Location = new System.Drawing.Point(15, 80);
            this.txtNombreRol.Name = "txtNombreRol";
            this.txtNombreRol.Size = new System.Drawing.Size(364, 20);
            this.txtNombreRol.TabIndex = 2;
            // 
            // lblNombreRol
            // 
            this.lblNombreRol.AutoSize = true;
            this.lblNombreRol.Location = new System.Drawing.Point(15, 60);
            this.lblNombreRol.Name = "lblNombreRol";
            this.lblNombreRol.Size = new System.Drawing.Size(102, 13);
            this.lblNombreRol.TabIndex = 1;
            this.lblNombreRol.Text = "Nombre Rol / Perfil:";
            // 
            // lblDetalle
            // 
            this.lblDetalle.AutoSize = true;
            this.lblDetalle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDetalle.Location = new System.Drawing.Point(15, 25);
            this.lblDetalle.Name = "lblDetalle";
            this.lblDetalle.Size = new System.Drawing.Size(199, 16);
            this.lblDetalle.TabIndex = 0;
            this.lblDetalle.Text = "Detalle: Selección vacía";
            // 
            // FormGestionPerfiles
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 461);
            this.Controls.Add(this.grpAcciones);
            this.Controls.Add(this.tvPerfiles);
            this.Name = "FormGestionPerfiles";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Gestión de Perfiles (Composite)";
            this.Load += new System.EventHandler(this.FormGestionPerfiles_Load);
            this.grpAcciones.ResumeLayout(false);
            this.grpAcciones.PerformLayout();
            this.grpAsignarPermiso.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView tvPerfiles;
        private System.Windows.Forms.GroupBox grpAcciones;
        private System.Windows.Forms.Label lblDetalle;
        private System.Windows.Forms.Label lblNombreRol;
        private System.Windows.Forms.TextBox txtNombreRol;
        private System.Windows.Forms.Button btnCrearRolRaiz;
        private System.Windows.Forms.Button btnCrearSubRol;
        private System.Windows.Forms.Button btnEditarNombre;
        private System.Windows.Forms.Button btnEliminarRol;
        private System.Windows.Forms.GroupBox grpAsignarPermiso;
        private System.Windows.Forms.ComboBox cmbPermisos;
        private System.Windows.Forms.Button btnAsignarPermiso;
        private System.Windows.Forms.Button btnQuitarItem;
    }
}
