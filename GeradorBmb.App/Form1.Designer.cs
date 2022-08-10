namespace GeradorBmb.App
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.gerador = new System.Windows.Forms.Button();
            this.btnGerar = new System.Windows.Forms.Button();
            this.txtNameClass = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dgw = new System.Windows.Forms.DataGridView();
            this.Property = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.type = new System.Windows.Forms.DataGridViewComboBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgw)).BeginInit();
            this.SuspendLayout();
            // 
            // gerador
            // 
            this.gerador.Location = new System.Drawing.Point(818, 367);
            this.gerador.Name = "gerador";
            this.gerador.Size = new System.Drawing.Size(115, 31);
            this.gerador.TabIndex = 0;
            this.gerador.Text = "Gerar";
            this.gerador.UseVisualStyleBackColor = true;
            // 
            // btnGerar
            // 
            this.btnGerar.Location = new System.Drawing.Point(536, 374);
            this.btnGerar.Name = "btnGerar";
            this.btnGerar.Size = new System.Drawing.Size(140, 37);
            this.btnGerar.TabIndex = 0;
            this.btnGerar.Text = "Gerar";
            this.btnGerar.UseVisualStyleBackColor = true;
            this.btnGerar.Click += new System.EventHandler(this.btnGerar_Click);
            // 
            // txtNameClass
            // 
            this.txtNameClass.Location = new System.Drawing.Point(12, 38);
            this.txtNameClass.Name = "txtNameClass";
            this.txtNameClass.Size = new System.Drawing.Size(664, 23);
            this.txtNameClass.TabIndex = 1;
            this.txtNameClass.TextChanged += new System.EventHandler(this.txtNameClass_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 15);
            this.label1.TabIndex = 2;
            this.label1.Text = "Nome da Classe";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // dgw
            // 
            this.dgw.AllowUserToOrderColumns = true;
            this.dgw.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgw.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Property,
            this.type});
            this.dgw.Location = new System.Drawing.Point(12, 67);
            this.dgw.Name = "dgw";
            this.dgw.RowTemplate.Height = 25;
            this.dgw.Size = new System.Drawing.Size(664, 301);
            this.dgw.TabIndex = 3;
            this.dgw.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgw_CellContentClick);
            // 
            // Property
            // 
            this.Property.HeaderText = "Property";
            this.Property.Name = "Property";
            this.Property.Width = 300;
            // 
            // type
            // 
            this.type.HeaderText = "type";
            this.type.Items.AddRange(new object[] {
            "string ",
            "int",
            "bool",
            "DateTime",
            "DateTime?",
            "decimal",
            "float "});
            this.type.Name = "type";
            this.type.Width = 200;
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(688, 423);
            this.Controls.Add(this.dgw);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtNameClass);
            this.Controls.Add(this.btnGerar);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            ((System.ComponentModel.ISupportInitialize)(this.dgw)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button gerador;
        private Button btnGerar;
        private TextBox txtNameClass;
        private Label label1;
        private DataGridView dgw;
        private DataGridViewTextBoxColumn Property;
        private DataGridViewComboBoxColumn type;
    }
}