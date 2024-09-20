namespace Cafe
{
    partial class FrmGetDeliveryInvoiceDetails
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
            this.TxClientAddress = new System.Windows.Forms.TextBox();
            this.TxClientPhone = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.CbDelivryMan = new System.Windows.Forms.ComboBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.BtnPrintBill = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // TxClientAddress
            // 
            this.TxClientAddress.Location = new System.Drawing.Point(12, 125);
            this.TxClientAddress.Multiline = true;
            this.TxClientAddress.Name = "TxClientAddress";
            this.TxClientAddress.Size = new System.Drawing.Size(197, 22);
            this.TxClientAddress.TabIndex = 0;
            // 
            // TxClientPhone
            // 
            this.TxClientPhone.Location = new System.Drawing.Point(12, 241);
            this.TxClientPhone.Multiline = true;
            this.TxClientPhone.Name = "TxClientPhone";
            this.TxClientPhone.Size = new System.Drawing.Size(197, 22);
            this.TxClientPhone.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(276, 121);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(107, 25);
            this.label1.TabIndex = 2;
            this.label1.Text = "عنوان العميل";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(276, 241);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(102, 25);
            this.label2.TabIndex = 3;
            this.label2.Text = "هاتف العميل";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(313, 339);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 25);
            this.label3.TabIndex = 4;
            this.label3.Text = " السائق";
            // 
            // CbDelivryMan
            // 
            this.CbDelivryMan.FormattingEnabled = true;
            this.CbDelivryMan.Location = new System.Drawing.Point(12, 339);
            this.CbDelivryMan.Name = "CbDelivryMan";
            this.CbDelivryMan.Size = new System.Drawing.Size(197, 24);
            this.CbDelivryMan.TabIndex = 5;
            this.toolTip1.SetToolTip(this.CbDelivryMan, "يمكنك كتابه اسم سائق جديد او اختيار من الموجود");
            // 
            // BtnPrintBill
            // 
            this.BtnPrintBill.Location = new System.Drawing.Point(132, 395);
            this.BtnPrintBill.Name = "BtnPrintBill";
            this.BtnPrintBill.Size = new System.Drawing.Size(104, 38);
            this.BtnPrintBill.TabIndex = 6;
            this.BtnPrintBill.Text = "طباعه الفاتوره";
            this.BtnPrintBill.UseVisualStyleBackColor = true;
            this.BtnPrintBill.Click += new System.EventHandler(this.BtnPrintBill_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 22.2F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(93, 25);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(198, 42);
            this.label4.TabIndex = 7;
            this.label4.Text = "فاتوره ديلفري";
            // 
            // FrmGetDeliveryInvoiceDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(385, 451);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.BtnPrintBill);
            this.Controls.Add(this.CbDelivryMan);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.TxClientPhone);
            this.Controls.Add(this.TxClientAddress);
            this.Name = "FrmGetDeliveryInvoiceDetails";
            this.Text = "FrmGetDeliveryInvoiceDetails";
            this.Load += new System.EventHandler(this.FrmGetDeliveryInvoiceDetails_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox TxClientAddress;
        private System.Windows.Forms.TextBox TxClientPhone;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox CbDelivryMan;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button BtnPrintBill;
        private System.Windows.Forms.Label label4;
    }
}