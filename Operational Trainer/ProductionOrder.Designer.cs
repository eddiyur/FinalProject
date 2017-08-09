namespace Operational_Trainer
{
    partial class ProductionOrder
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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBoxProductsList = new System.Windows.Forms.ComboBox();
            this.productLabel = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(79, 87);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(139, 20);
            this.textBox1.TabIndex = 7;
            this.textBox1.Text = "1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(30, 87);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Amount";
            // 
            // comboBoxProductsList
            // 
            this.comboBoxProductsList.FormattingEnabled = true;
            this.comboBoxProductsList.Location = new System.Drawing.Point(79, 51);
            this.comboBoxProductsList.Name = "comboBoxProductsList";
            this.comboBoxProductsList.Size = new System.Drawing.Size(139, 21);
            this.comboBoxProductsList.TabIndex = 5;
            this.comboBoxProductsList.SelectedIndexChanged += new System.EventHandler(this.comboBoxProductsList_SelectedIndexChanged);
            // 
            // productLabel
            // 
            this.productLabel.AutoSize = true;
            this.productLabel.Location = new System.Drawing.Point(23, 51);
            this.productLabel.Name = "productLabel";
            this.productLabel.Size = new System.Drawing.Size(50, 13);
            this.productLabel.TabIndex = 4;
            this.productLabel.Text = "Product: ";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(79, 129);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(139, 20);
            this.textBox2.TabIndex = 9;
            this.textBox2.Text = "1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(30, 129);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(30, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Date";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(131, 194);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 10;
            this.button1.Text = "Approve";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(26, 194);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 11;
            this.button2.Text = "Cancle";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // ProductionOrder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(250, 262);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBoxProductsList);
            this.Controls.Add(this.productLabel);
            this.Name = "ProductionOrder";
            this.Text = "ProductionOrder";
            this.Load += new System.EventHandler(this.ProductionOrder_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBoxProductsList;
        private System.Windows.Forms.Label productLabel;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}