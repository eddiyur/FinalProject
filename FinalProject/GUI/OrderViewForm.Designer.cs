﻿namespace OperationalTrainer.GUI
{
    partial class OrderViewForm
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
            this.DTPanel = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // DTPanel
            // 
            this.DTPanel.Location = new System.Drawing.Point(43, 185);
            this.DTPanel.Name = "DTPanel";
            this.DTPanel.Size = new System.Drawing.Size(200, 100);
            this.DTPanel.TabIndex = 0;
            this.DTPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.DTPanel_Paint);
            // 
            // OrderForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(375, 494);
            this.ControlBox = false;
            this.Controls.Add(this.DTPanel);
            this.Name = "OrderForm";
            this.Text = "CustomerOrderForm";
            this.Load += new System.EventHandler(this.CustomerOrderForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel DTPanel;
    }
}