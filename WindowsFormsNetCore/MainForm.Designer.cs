namespace SEI.Desktop
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBoxMarcador = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxQuantidade = new System.Windows.Forms.TextBox();
            this.botaoDistribuir = new System.Windows.Forms.Button();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.labelProgress = new System.Windows.Forms.Label();
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
            this.labelProcessando = new System.Windows.Forms.Label();
            this.comboBoxMatricula = new System.Windows.Forms.ComboBox();
            this.checkBoxVerNavegador = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(33, 76);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Perito:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(334, 76);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "Marcador:";
            // 
            // comboBoxMarcador
            // 
            this.comboBoxMarcador.FormattingEnabled = true;
            this.comboBoxMarcador.Location = new System.Drawing.Point(401, 72);
            this.comboBoxMarcador.Name = "comboBoxMarcador";
            this.comboBoxMarcador.Size = new System.Drawing.Size(121, 23);
            this.comboBoxMarcador.TabIndex = 3;
            this.comboBoxMarcador.TextUpdate += new System.EventHandler(this.comboBoxMarcador_TextUpdate);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(528, 76);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 15);
            this.label3.TabIndex = 4;
            this.label3.Text = "Quantidade:";
            // 
            // textBoxQuantidade
            // 
            this.textBoxQuantidade.Location = new System.Drawing.Point(606, 73);
            this.textBoxQuantidade.Name = "textBoxQuantidade";
            this.textBoxQuantidade.Size = new System.Drawing.Size(52, 23);
            this.textBoxQuantidade.TabIndex = 5;
            this.textBoxQuantidade.Text = "1";
            // 
            // botaoDistribuir
            // 
            this.botaoDistribuir.Location = new System.Drawing.Point(685, 73);
            this.botaoDistribuir.Name = "botaoDistribuir";
            this.botaoDistribuir.Size = new System.Drawing.Size(75, 23);
            this.botaoDistribuir.TabIndex = 6;
            this.botaoDistribuir.Text = "Distribuir";
            this.botaoDistribuir.UseVisualStyleBackColor = true;
            this.botaoDistribuir.Click += new System.EventHandler(this.button1_Click);
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(12, 12);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(776, 23);
            this.progressBar.TabIndex = 7;
            // 
            // labelProgress
            // 
            this.labelProgress.AutoSize = true;
            this.labelProgress.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.labelProgress.Location = new System.Drawing.Point(388, 16);
            this.labelProgress.Name = "labelProgress";
            this.labelProgress.Size = new System.Drawing.Size(24, 15);
            this.labelProgress.TabIndex = 8;
            this.labelProgress.Text = "0/0";
            this.labelProgress.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // listView1
            // 
            this.listView1.AllowColumnReorder = true;
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader5,
            this.columnHeader3,
            this.columnHeader4});
            this.listView1.FullRowSelect = true;
            this.listView1.GridLines = true;
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(33, 142);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(727, 218);
            this.listView1.TabIndex = 9;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Registro";
            this.columnHeader1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader1.Width = 150;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Matricula";
            this.columnHeader2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader2.Width = 150;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Perito";
            this.columnHeader5.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader5.Width = 200;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Marcador";
            this.columnHeader3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader3.Width = 100;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Quantidade";
            this.columnHeader4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader4.Width = 100;
            // 
            // labelProcessando
            // 
            this.labelProcessando.AutoSize = true;
            this.labelProcessando.Location = new System.Drawing.Point(12, 38);
            this.labelProcessando.Name = "labelProcessando";
            this.labelProcessando.Size = new System.Drawing.Size(89, 15);
            this.labelProcessando.TabIndex = 10;
            this.labelProcessando.Text = "Processando.....";
            this.labelProcessando.Visible = false;
            // 
            // comboBoxMatricula
            // 
            this.comboBoxMatricula.FormattingEnabled = true;
            this.comboBoxMatricula.Location = new System.Drawing.Point(80, 73);
            this.comboBoxMatricula.Name = "comboBoxMatricula";
            this.comboBoxMatricula.Size = new System.Drawing.Size(248, 23);
            this.comboBoxMatricula.TabIndex = 11;
            this.comboBoxMatricula.TextUpdate += new System.EventHandler(this.comboBoxMatricula_TextUpdate);
            // 
            // checkBox1
            // 
            this.checkBoxVerNavegador.AutoSize = true;
            this.checkBoxVerNavegador.Location = new System.Drawing.Point(685, 41);
            this.checkBoxVerNavegador.Name = "checkBox1";
            this.checkBoxVerNavegador.Size = new System.Drawing.Size(108, 19);
            this.checkBoxVerNavegador.TabIndex = 12;
            this.checkBoxVerNavegador.Text = "Ver Navegador?";
            this.checkBoxVerNavegador.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.checkBoxVerNavegador);
            this.Controls.Add(this.comboBoxMatricula);
            this.Controls.Add(this.labelProcessando);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.labelProgress);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.botaoDistribuir);
            this.Controls.Add(this.textBoxQuantidade);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.comboBoxMarcador);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Distribuir Processos - SEI";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBoxMarcador;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxQuantidade;
        private System.Windows.Forms.Button botaoDistribuir;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Label labelProgress;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.Label labelProcessando;
        private System.Windows.Forms.ComboBox comboBoxMatricula;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.CheckBox checkBoxVerNavegador;
    }
}

