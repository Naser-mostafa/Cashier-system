namespace Cafe
{
    partial class FrmCafeDetails
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
            this.TxCafePhone = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.TxCafeAddress = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.label4 = new System.Windows.Forms.Label();
            this.NudTaxes = new System.Windows.Forms.NumericUpDown();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.NudTaxes)).BeginInit();
            this.SuspendLayout();
            // 
            // TxCafePhone
            // 
            this.TxCafePhone.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.TxCafePhone.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxCafePhone.Location = new System.Drawing.Point(25, 122);
            this.TxCafePhone.Name = "TxCafePhone";
            this.TxCafePhone.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.TxCafePhone.Size = new System.Drawing.Size(172, 30);
            this.TxCafePhone.TabIndex = 0;
            this.TxCafePhone.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(250, 119);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(137, 25);
            this.label1.TabIndex = 1;
            this.label1.Text = "رقم هاتف المطعم";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(276, 207);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(111, 25);
            this.label2.TabIndex = 2;
            this.label2.Text = "عنوان المطعم";
            // 
            // TxCafeAddress
            // 
            this.TxCafeAddress.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.TxCafeAddress.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxCafeAddress.Location = new System.Drawing.Point(25, 211);
            this.TxCafeAddress.Name = "TxCafeAddress";
            this.TxCafeAddress.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.TxCafeAddress.Size = new System.Drawing.Size(172, 30);
            this.TxCafeAddress.TabIndex = 3;
            this.TxCafeAddress.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(327, 297);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 25);
            this.label3.TabIndex = 4;
            this.label3.Text = "الخدمه";
            this.toolTip1.SetToolTip(this.label3, "هذه الضريبه لا تعتمد في فواتير الديلفري");
            // 
            // toolTip1
            // 
            this.toolTip1.BackColor = System.Drawing.SystemColors.Desktop;
            this.toolTip1.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(111, 33);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(173, 32);
            this.label4.TabIndex = 6;
            this.label4.Text = "تفاصيل الفاتوره";
            this.toolTip1.SetToolTip(this.label4, "هذه التفاصيل التي تظهر في الفواتير للمشتري");
            // 
            // NudTaxes
            // 
            this.NudTaxes.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.NudTaxes.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NudTaxes.Location = new System.Drawing.Point(25, 297);
            this.NudTaxes.Maximum = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            this.NudTaxes.Name = "NudTaxes";
            this.NudTaxes.Size = new System.Drawing.Size(172, 30);
            this.NudTaxes.TabIndex = 5;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(165, 361);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(95, 35);
            this.button1.TabIndex = 7;
            this.button1.Text = "حفظ";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // FrmCafeDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(399, 408);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.NudTaxes);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.TxCafeAddress);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.TxCafePhone);
            this.Name = "FrmCafeDetails";
            this.Text = "FrmCafeDetails";
            this.Load += new System.EventHandler(this.FrmCafeDetails_Load);
            ((System.ComponentModel.ISupportInitialize)(this.NudTaxes)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox TxCafePhone;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox TxCafeAddress;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.NumericUpDown NudTaxes;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button1;
    }
}