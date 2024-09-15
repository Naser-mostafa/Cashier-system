namespace Cafe
{
    partial class FrmAddEditDrink
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
            this.LbFormAddress = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.Label2 = new System.Windows.Forms.Label();
            this.LbLarge = new System.Windows.Forms.Label();
            this.TxName = new System.Windows.Forms.TextBox();
            this.TxLargePrice = new System.Windows.Forms.TextBox();
            this.BtnFinish = new System.Windows.Forms.Button();
            this.LkAddPicture = new System.Windows.Forms.LinkLabel();
            this.LbSmall = new System.Windows.Forms.Label();
            this.LbMid = new System.Windows.Forms.Label();
            this.TxSmallPrice = new System.Windows.Forms.TextBox();
            this.TxMidPrice = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // LbFormAddress
            // 
            this.LbFormAddress.AutoSize = true;
            this.LbFormAddress.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LbFormAddress.Location = new System.Drawing.Point(88, 24);
            this.LbFormAddress.Name = "LbFormAddress";
            this.LbFormAddress.Size = new System.Drawing.Size(162, 32);
            this.LbFormAddress.TabIndex = 0;
            this.LbFormAddress.Text = "FormName";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(36, 88);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(255, 124);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label2.Location = new System.Drawing.Point(314, 280);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(45, 22);
            this.Label2.TabIndex = 2;
            this.Label2.Text = "الاسم";
            // 
            // LbLarge
            // 
            this.LbLarge.AutoSize = true;
            this.LbLarge.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LbLarge.Location = new System.Drawing.Point(261, 484);
            this.LbLarge.Name = "LbLarge";
            this.LbLarge.Size = new System.Drawing.Size(98, 22);
            this.LbLarge.TabIndex = 3;
            this.LbLarge.Text = "السعر(كبير.)";
            // 
            // TxName
            // 
            this.TxName.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.TxName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxName.Location = new System.Drawing.Point(36, 282);
            this.TxName.Name = "TxName";
            this.TxName.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.TxName.Size = new System.Drawing.Size(185, 30);
            this.TxName.TabIndex = 4;
            this.TxName.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // TxLargePrice
            // 
            this.TxLargePrice.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.TxLargePrice.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxLargePrice.Location = new System.Drawing.Point(36, 486);
            this.TxLargePrice.Name = "TxLargePrice";
            this.TxLargePrice.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.TxLargePrice.Size = new System.Drawing.Size(185, 30);
            this.TxLargePrice.TabIndex = 5;
            this.TxLargePrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // BtnFinish
            // 
            this.BtnFinish.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.BtnFinish.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnFinish.Location = new System.Drawing.Point(124, 557);
            this.BtnFinish.Name = "BtnFinish";
            this.BtnFinish.Size = new System.Drawing.Size(96, 35);
            this.BtnFinish.TabIndex = 6;
            this.BtnFinish.Text = "اضافه";
            this.BtnFinish.UseVisualStyleBackColor = false;
            this.BtnFinish.Click += new System.EventHandler(this.BtnFinish_Click);
            // 
            // LkAddPicture
            // 
            this.LkAddPicture.AutoSize = true;
            this.LkAddPicture.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LkAddPicture.Location = new System.Drawing.Point(120, 232);
            this.LkAddPicture.Name = "LkAddPicture";
            this.LkAddPicture.Size = new System.Drawing.Size(101, 20);
            this.LkAddPicture.TabIndex = 7;
            this.LkAddPicture.TabStop = true;
            this.LkAddPicture.Text = "اضافه الصوره";
            this.LkAddPicture.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LkAddPicture_LinkClicked);
            // 
            // LbSmall
            // 
            this.LbSmall.AutoSize = true;
            this.LbSmall.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LbSmall.Location = new System.Drawing.Point(244, 349);
            this.LbSmall.Name = "LbSmall";
            this.LbSmall.Size = new System.Drawing.Size(115, 22);
            this.LbSmall.TabIndex = 8;
            this.LbSmall.Text = "السعر (صغير.)";
            // 
            // LbMid
            // 
            this.LbMid.AutoSize = true;
            this.LbMid.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LbMid.Location = new System.Drawing.Point(251, 409);
            this.LbMid.Name = "LbMid";
            this.LbMid.Size = new System.Drawing.Size(108, 22);
            this.LbMid.TabIndex = 9;
            this.LbMid.Text = "السعر (وسط.)";
            // 
            // TxSmallPrice
            // 
            this.TxSmallPrice.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.TxSmallPrice.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxSmallPrice.Location = new System.Drawing.Point(36, 349);
            this.TxSmallPrice.Name = "TxSmallPrice";
            this.TxSmallPrice.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.TxSmallPrice.Size = new System.Drawing.Size(185, 30);
            this.TxSmallPrice.TabIndex = 10;
            this.TxSmallPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // TxMidPrice
            // 
            this.TxMidPrice.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.TxMidPrice.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxMidPrice.Location = new System.Drawing.Point(35, 411);
            this.TxMidPrice.Name = "TxMidPrice";
            this.TxMidPrice.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.TxMidPrice.Size = new System.Drawing.Size(185, 30);
            this.TxMidPrice.TabIndex = 11;
            this.TxMidPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // FrmAddEditDrink
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(365, 604);
            this.Controls.Add(this.TxMidPrice);
            this.Controls.Add(this.TxSmallPrice);
            this.Controls.Add(this.LbMid);
            this.Controls.Add(this.LbSmall);
            this.Controls.Add(this.LkAddPicture);
            this.Controls.Add(this.BtnFinish);
            this.Controls.Add(this.TxLargePrice);
            this.Controls.Add(this.TxName);
            this.Controls.Add(this.LbLarge);
            this.Controls.Add(this.Label2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.LbFormAddress);
            this.Name = "FrmAddEditDrink";
            this.Text = "FrmAddEditDrink";
            this.Load += new System.EventHandler(this.FrmAddEditDrink_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label LbFormAddress;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label Label2;
        private System.Windows.Forms.Label LbLarge;
        private System.Windows.Forms.TextBox TxName;
        private System.Windows.Forms.TextBox TxLargePrice;
        private System.Windows.Forms.Button BtnFinish;
        private System.Windows.Forms.LinkLabel LkAddPicture;
        private System.Windows.Forms.Label LbSmall;
        private System.Windows.Forms.Label LbMid;
        private System.Windows.Forms.TextBox TxSmallPrice;
        private System.Windows.Forms.TextBox TxMidPrice;
    }
}