namespace Cafe
{
    partial class FrmInnerInvoices
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
            this.components = new System.ComponentModel.Container();
            this.DgvForInnerInvoices = new System.Windows.Forms.DataGridView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.استرجاعكلالفاتورهToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.استرجاعجزءToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.DgvForInnerInvoices)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // DgvForInnerInvoices
            // 
            this.DgvForInnerInvoices.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DgvForInnerInvoices.ContextMenuStrip = this.contextMenuStrip1;
            this.DgvForInnerInvoices.Location = new System.Drawing.Point(-4, 39);
            this.DgvForInnerInvoices.Name = "DgvForInnerInvoices";
            this.DgvForInnerInvoices.RowHeadersWidth = 51;
            this.DgvForInnerInvoices.RowTemplate.Height = 24;
            this.DgvForInnerInvoices.Size = new System.Drawing.Size(795, 421);
            this.DgvForInnerInvoices.TabIndex = 1;
            this.DgvForInnerInvoices.CellMouseLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgvForInnerInvoices_CellMouseLeave);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.استرجاعكلالفاتورهToolStripMenuItem,
            this.استرجاعجزءToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(201, 52);
            // 
            // استرجاعكلالفاتورهToolStripMenuItem
            // 
            this.استرجاعكلالفاتورهToolStripMenuItem.Name = "استرجاعكلالفاتورهToolStripMenuItem";
            this.استرجاعكلالفاتورهToolStripMenuItem.Size = new System.Drawing.Size(200, 24);
            this.استرجاعكلالفاتورهToolStripMenuItem.Text = "استرجاع كل الفاتوره";
            this.استرجاعكلالفاتورهToolStripMenuItem.Click += new System.EventHandler(this.استرجاعكلالفاتورهToolStripMenuItem_Click);
            // 
            // استرجاعجزءToolStripMenuItem
            // 
            this.استرجاعجزءToolStripMenuItem.Name = "استرجاعجزءToolStripMenuItem";
            this.استرجاعجزءToolStripMenuItem.Size = new System.Drawing.Size(200, 24);
            this.استرجاعجزءToolStripMenuItem.Text = "استرجاع جزء";
            this.استرجاعجزءToolStripMenuItem.Click += new System.EventHandler(this.استرجاعجزءToolStripMenuItem_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Open Sans", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(221, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(329, 27);
            this.label1.TabIndex = 2;
            this.label1.Text = "ارقام الفواتير الفرديه داخل الفاتوره المدمجه";
            // 
            // FrmInnerInvoices
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(789, 456);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.DgvForInnerInvoices);
            this.Name = "FrmInnerInvoices";
            this.Text = "FrmInnerInvoices";
            this.Load += new System.EventHandler(this.FrmInnerInvoices_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DgvForInnerInvoices)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView DgvForInnerInvoices;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem استرجاعكلالفاتورهToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem استرجاعجزءToolStripMenuItem;
        private System.Windows.Forms.Label label1;
    }
}