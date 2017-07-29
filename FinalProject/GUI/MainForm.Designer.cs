namespace FinalProject.GUI
{
    partial class MainForm
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
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.testToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nextTickToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newSupplierOrderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.testToolStripMenuItem,
            this.nextTickToolStripMenuItem,
            this.newSupplierOrderToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(1405, 24);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "menuStrip";
            // 
            // testToolStripMenuItem
            // 
            this.testToolStripMenuItem.Name = "testToolStripMenuItem";
            this.testToolStripMenuItem.Size = new System.Drawing.Size(46, 20);
            this.testToolStripMenuItem.Text = "test()";
            this.testToolStripMenuItem.Click += new System.EventHandler(this.testToolStripMenuItem_Click);
            // 
            // nextTickToolStripMenuItem
            // 
            this.nextTickToolStripMenuItem.Name = "nextTickToolStripMenuItem";
            this.nextTickToolStripMenuItem.Size = new System.Drawing.Size(68, 20);
            this.nextTickToolStripMenuItem.Text = "Next Tick";
            this.nextTickToolStripMenuItem.Click += new System.EventHandler(this.nextTickToolStripMenuItem_Click);
            // 
            // newSupplierOrderToolStripMenuItem
            // 
            this.newSupplierOrderToolStripMenuItem.Name = "newSupplierOrderToolStripMenuItem";
            this.newSupplierOrderToolStripMenuItem.Size = new System.Drawing.Size(117, 20);
            this.newSupplierOrderToolStripMenuItem.Text = "new supplier order";
            this.newSupplierOrderToolStripMenuItem.Click += new System.EventHandler(this.newSupplierOrderToolStripMenuItem_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1405, 649);
            this.Controls.Add(this.menuStrip);
            this.MainMenuStrip = this.menuStrip;
            this.Name = "MainForm";
            this.Text = "Operational Trainer";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem testToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem nextTickToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newSupplierOrderToolStripMenuItem;
    }
}