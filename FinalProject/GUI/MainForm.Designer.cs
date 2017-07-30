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
            this.nextTickToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newSupplierOrderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadScenarioToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cSVScenarioToXMLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.nextTickToolStripMenuItem,
            this.newSupplierOrderToolStripMenuItem,
            this.toolsToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(1405, 24);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "menuStrip";
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
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadScenarioToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // loadScenarioToolStripMenuItem
            // 
            this.loadScenarioToolStripMenuItem.Name = "loadScenarioToolStripMenuItem";
            this.loadScenarioToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.loadScenarioToolStripMenuItem.Text = "Load Scenario";
            this.loadScenarioToolStripMenuItem.Click += new System.EventHandler(this.loadScenarioToolStripMenuItem_Click);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cSVScenarioToXMLToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(47, 20);
            this.toolsToolStripMenuItem.Text = "Tools";
            // 
            // cSVScenarioToXMLToolStripMenuItem
            // 
            this.cSVScenarioToXMLToolStripMenuItem.Name = "cSVScenarioToXMLToolStripMenuItem";
            this.cSVScenarioToXMLToolStripMenuItem.Size = new System.Drawing.Size(185, 22);
            this.cSVScenarioToXMLToolStripMenuItem.Text = "CSV scenario To XML";
            this.cSVScenarioToXMLToolStripMenuItem.Click += new System.EventHandler(this.cSVScenarioToXMLToolStripMenuItem_Click);
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
        private System.Windows.Forms.ToolStripMenuItem nextTickToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newSupplierOrderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadScenarioToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cSVScenarioToXMLToolStripMenuItem;
    }
}