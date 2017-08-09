namespace Operational_Trainer
{
    partial class Form1
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadScenarioToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ordersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.purchaseOrderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.productionOrderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.continueToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createScenarioToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridViewCU = new System.Windows.Forms.DataGridView();
            this.dataGridViewPU = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.dataGridViewBank = new System.Windows.Forms.DataGridView();
            this.label3 = new System.Windows.Forms.Label();
            this.dataGridViewPr = new System.Windows.Forms.DataGridView();
            this.label4 = new System.Windows.Forms.Label();
            this.dataGridViewTools = new System.Windows.Forms.DataGridView();
            this.label5 = new System.Windows.Forms.Label();
            this.dataGridViewWa = new System.Windows.Forms.DataGridView();
            this.label6 = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewCU)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPU)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewBank)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPr)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTools)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewWa)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.ordersToolStripMenuItem,
            this.continueToolStripMenuItem,
            this.toolsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1584, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
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
            // ordersToolStripMenuItem
            // 
            this.ordersToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.purchaseOrderToolStripMenuItem,
            this.productionOrderToolStripMenuItem});
            this.ordersToolStripMenuItem.Name = "ordersToolStripMenuItem";
            this.ordersToolStripMenuItem.Size = new System.Drawing.Size(54, 20);
            this.ordersToolStripMenuItem.Text = "Orders";
            // 
            // purchaseOrderToolStripMenuItem
            // 
            this.purchaseOrderToolStripMenuItem.Name = "purchaseOrderToolStripMenuItem";
            this.purchaseOrderToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.purchaseOrderToolStripMenuItem.Text = "Purchase order";
            this.purchaseOrderToolStripMenuItem.Click += new System.EventHandler(this.purchaseOrderToolStripMenuItem_Click);
            // 
            // productionOrderToolStripMenuItem
            // 
            this.productionOrderToolStripMenuItem.Name = "productionOrderToolStripMenuItem";
            this.productionOrderToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.productionOrderToolStripMenuItem.Text = "Production order";
            this.productionOrderToolStripMenuItem.Click += new System.EventHandler(this.productionOrderToolStripMenuItem_Click);
            // 
            // continueToolStripMenuItem
            // 
            this.continueToolStripMenuItem.Name = "continueToolStripMenuItem";
            this.continueToolStripMenuItem.Size = new System.Drawing.Size(68, 20);
            this.continueToolStripMenuItem.Text = "Continue";
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.createScenarioToolStripMenuItem,
            this.settingsToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(47, 20);
            this.toolsToolStripMenuItem.Text = "Tools";
            // 
            // createScenarioToolStripMenuItem
            // 
            this.createScenarioToolStripMenuItem.Name = "createScenarioToolStripMenuItem";
            this.createScenarioToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.createScenarioToolStripMenuItem.Text = "Create Scenario";
            this.createScenarioToolStripMenuItem.Click += new System.EventHandler(this.createScenarioToolStripMenuItem_Click);
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.settingsToolStripMenuItem.Text = "Settings";
            this.settingsToolStripMenuItem.Click += new System.EventHandler(this.settingsToolStripMenuItem_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label1.Location = new System.Drawing.Point(12, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(124, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "Customer Orders";
            // 
            // dataGridViewCU
            // 
            this.dataGridViewCU.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewCU.Location = new System.Drawing.Point(12, 53);
            this.dataGridViewCU.Name = "dataGridViewCU";
            this.dataGridViewCU.Size = new System.Drawing.Size(622, 152);
            this.dataGridViewCU.TabIndex = 2;
            // 
            // dataGridViewPU
            // 
            this.dataGridViewPU.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewPU.Location = new System.Drawing.Point(15, 227);
            this.dataGridViewPU.Name = "dataGridViewPU";
            this.dataGridViewPU.Size = new System.Drawing.Size(622, 213);
            this.dataGridViewPU.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label2.Location = new System.Drawing.Point(12, 208);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(124, 16);
            this.label2.TabIndex = 3;
            this.label2.Text = "Purchase Orders";
            // 
            // dataGridViewBank
            // 
            this.dataGridViewBank.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewBank.Location = new System.Drawing.Point(15, 679);
            this.dataGridViewBank.Name = "dataGridViewBank";
            this.dataGridViewBank.Size = new System.Drawing.Size(622, 170);
            this.dataGridViewBank.TabIndex = 9;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label3.Location = new System.Drawing.Point(12, 660);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 16);
            this.label3.TabIndex = 8;
            this.label3.Text = "Bank";
            // 
            // dataGridViewPr
            // 
            this.dataGridViewPr.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewPr.Location = new System.Drawing.Point(15, 475);
            this.dataGridViewPr.Name = "dataGridViewPr";
            this.dataGridViewPr.Size = new System.Drawing.Size(622, 171);
            this.dataGridViewPr.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label4.Location = new System.Drawing.Point(12, 456);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(133, 16);
            this.label4.TabIndex = 6;
            this.label4.Text = "Production Orders";
            // 
            // dataGridViewTools
            // 
            this.dataGridViewTools.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewTools.Location = new System.Drawing.Point(663, 475);
            this.dataGridViewTools.Name = "dataGridViewTools";
            this.dataGridViewTools.Size = new System.Drawing.Size(909, 374);
            this.dataGridViewTools.TabIndex = 13;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label5.Location = new System.Drawing.Point(663, 456);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(48, 16);
            this.label5.TabIndex = 12;
            this.label5.Text = "Tools";
            // 
            // dataGridViewWa
            // 
            this.dataGridViewWa.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewWa.Location = new System.Drawing.Point(663, 53);
            this.dataGridViewWa.Name = "dataGridViewWa";
            this.dataGridViewWa.Size = new System.Drawing.Size(909, 387);
            this.dataGridViewWa.TabIndex = 11;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label6.Location = new System.Drawing.Point(663, 34);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(87, 16);
            this.label6.TabIndex = 10;
            this.label6.Text = "Warehouse";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1584, 861);
            this.Controls.Add(this.dataGridViewTools);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.dataGridViewWa);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.dataGridViewBank);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dataGridViewPr);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.dataGridViewPU);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dataGridViewCU);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewCU)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPU)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewBank)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPr)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTools)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewWa)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadScenarioToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ordersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem purchaseOrderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem productionOrderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem continueToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem createScenarioToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dataGridViewCU;
        private System.Windows.Forms.DataGridView dataGridViewPU;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dataGridViewBank;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView dataGridViewPr;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridView dataGridViewTools;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridView dataGridViewWa;
        private System.Windows.Forms.Label label6;
    }
}

