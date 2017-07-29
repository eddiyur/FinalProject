namespace FinalProject.GUI
{
    partial class NewSupplierOrder
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.productLabel = new System.Windows.Forms.Label();
            this.comboBoxProductsList = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.OKbutton = new System.Windows.Forms.Button();
            this.CencelButton = new System.Windows.Forms.Button();
            this.ShowOrder = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // productLabel
            // 
            this.productLabel.AutoSize = true;
            this.productLabel.Location = new System.Drawing.Point(33, 28);
            this.productLabel.Name = "productLabel";
            this.productLabel.Size = new System.Drawing.Size(50, 13);
            this.productLabel.TabIndex = 0;
            this.productLabel.Text = "Product: ";
            // 
            // comboBoxProductsList
            // 
            this.comboBoxProductsList.FormattingEnabled = true;
            this.comboBoxProductsList.Location = new System.Drawing.Point(89, 28);
            this.comboBoxProductsList.Name = "comboBoxProductsList";
            this.comboBoxProductsList.Size = new System.Drawing.Size(109, 21);
            this.comboBoxProductsList.TabIndex = 1;
            this.comboBoxProductsList.SelectedIndexChanged += new System.EventHandler(this.comboBoxProductsList_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(431, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Amount";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(480, 31);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(139, 20);
            this.textBox1.TabIndex = 3;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(13, 72);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(626, 229);
            this.dataGridView1.TabIndex = 4;
            this.dataGridView1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellDoubleClick);
            // 
            // OKbutton
            // 
            this.OKbutton.Location = new System.Drawing.Point(254, 325);
            this.OKbutton.Name = "OKbutton";
            this.OKbutton.Size = new System.Drawing.Size(129, 44);
            this.OKbutton.TabIndex = 5;
            this.OKbutton.Text = "OK";
            this.OKbutton.UseVisualStyleBackColor = true;
            // 
            // CencelButton
            // 
            this.CencelButton.Location = new System.Drawing.Point(459, 325);
            this.CencelButton.Name = "CencelButton";
            this.CencelButton.Size = new System.Drawing.Size(129, 44);
            this.CencelButton.TabIndex = 6;
            this.CencelButton.Text = "Cancel";
            this.CencelButton.UseVisualStyleBackColor = true;
            // 
            // ShowOrder
            // 
            this.ShowOrder.Location = new System.Drawing.Point(55, 325);
            this.ShowOrder.Name = "ShowOrder";
            this.ShowOrder.Size = new System.Drawing.Size(129, 44);
            this.ShowOrder.TabIndex = 7;
            this.ShowOrder.Text = "Show Order";
            this.ShowOrder.UseVisualStyleBackColor = true;
            // 
            // NewSupplierOrder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(651, 381);
            this.Controls.Add(this.ShowOrder);
            this.Controls.Add(this.CencelButton);
            this.Controls.Add(this.OKbutton);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBoxProductsList);
            this.Controls.Add(this.productLabel);
            this.Name = "NewSupplierOrder";
            this.Text = "NewSupplierOrder";
            this.Load += new System.EventHandler(this.NewSupplierOrder_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label productLabel;
        private System.Windows.Forms.ComboBox comboBoxProductsList;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button OKbutton;
        private System.Windows.Forms.Button CencelButton;
        private System.Windows.Forms.Button ShowOrder;
    }
}