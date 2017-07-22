namespace FinalProject.GUI
{
    partial class OrdersMainForm
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
            this.OrdersPanel = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // OrdersPanel
            // 
            this.OrdersPanel.Location = new System.Drawing.Point(26, 13);
            this.OrdersPanel.Name = "OrdersPanel";
            this.OrdersPanel.Size = new System.Drawing.Size(670, 309);
            this.OrdersPanel.TabIndex = 0;
            this.OrdersPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.OrdersPanel_Paint);
            // 
            // OrdersMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(738, 334);
            this.Controls.Add(this.OrdersPanel);
            this.Name = "OrdersMainForm";
            this.Text = "CustomerOrdersForm";
            this.Load += new System.EventHandler(this.OrdersForm_Load_1);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel OrdersPanel;
    }
}