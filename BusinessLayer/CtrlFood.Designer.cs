namespace Cafe
{
    partial class CtrlFood
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.EditPicture = new System.Windows.Forms.PictureBox();
            this.label4 = new System.Windows.Forms.Label();
            this.FoodPicture = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.LbFoodName = new System.Windows.Forms.Label();
            this.LbFoodPrice = new System.Windows.Forms.Label();
            this.CbSize = new System.Windows.Forms.ComboBox();
            this.NupQuantity = new System.Windows.Forms.NumericUpDown();
            this.BtnAddToInvoice = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.EditPicture)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FoodPicture)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NupQuantity)).BeginInit();
            this.SuspendLayout();
            // 
            // EditPicture
            // 
            this.EditPicture.BackColor = System.Drawing.Color.LightYellow;
            this.EditPicture.Image = global::Cafe.Properties.Resources.Settings;
            this.EditPicture.Location = new System.Drawing.Point(270, 14);
            this.EditPicture.Name = "EditPicture";
            this.EditPicture.Size = new System.Drawing.Size(34, 20);
            this.EditPicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.EditPicture.TabIndex = 12;
            this.EditPicture.TabStop = false;
            this.EditPicture.Click += new System.EventHandler(this.EditPicture_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.RoyalBlue;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(220, 14);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(44, 20);
            this.label4.TabIndex = 11;
            this.label4.Text = "تعديل";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // FoodPicture
            // 
            this.FoodPicture.BackgroundImage = global::Cafe.Properties.Resources.images;
            this.FoodPicture.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.FoodPicture.Location = new System.Drawing.Point(3, 49);
            this.FoodPicture.Name = "FoodPicture";
            this.FoodPicture.Size = new System.Drawing.Size(312, 168);
            this.FoodPicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.FoodPicture.TabIndex = 13;
            this.FoodPicture.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(230, 376);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 32);
            this.label2.TabIndex = 16;
            this.label2.Text = "الكميه";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(237, 227);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 36);
            this.label3.TabIndex = 15;
            this.label3.Text = "الاسم";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(228, 329);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 32);
            this.label1.TabIndex = 14;
            this.label1.Text = "السعر";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(230, 275);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(72, 32);
            this.label5.TabIndex = 17;
            this.label5.Text = "الحجم";
            // 
            // LbFoodName
            // 
            this.LbFoodName.AutoSize = true;
            this.LbFoodName.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LbFoodName.Location = new System.Drawing.Point(35, 234);
            this.LbFoodName.Name = "LbFoodName";
            this.LbFoodName.Size = new System.Drawing.Size(39, 29);
            this.LbFoodName.TabIndex = 18;
            this.LbFoodName.Text = "؟؟";
            // 
            // LbFoodPrice
            // 
            this.LbFoodPrice.AutoSize = true;
            this.LbFoodPrice.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LbFoodPrice.Location = new System.Drawing.Point(35, 332);
            this.LbFoodPrice.Name = "LbFoodPrice";
            this.LbFoodPrice.Size = new System.Drawing.Size(39, 29);
            this.LbFoodPrice.TabIndex = 19;
            this.LbFoodPrice.Text = "؟؟";
            // 
            // CbSize
            // 
            this.CbSize.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CbSize.FormattingEnabled = true;
            this.CbSize.Items.AddRange(new object[] {
            "كبير",
            "وسط",
            "صغير"});
            this.CbSize.Location = new System.Drawing.Point(40, 283);
            this.CbSize.Name = "CbSize";
            this.CbSize.Size = new System.Drawing.Size(121, 33);
            this.CbSize.TabIndex = 20;
            this.CbSize.TextChanged += new System.EventHandler(this.CbSize_TextChanged);
            // 
            // NupQuantity
            // 
            this.NupQuantity.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NupQuantity.Location = new System.Drawing.Point(40, 380);
            this.NupQuantity.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.NupQuantity.Name = "NupQuantity";
            this.NupQuantity.Size = new System.Drawing.Size(121, 30);
            this.NupQuantity.TabIndex = 21;
            this.NupQuantity.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.NupQuantity.ValueChanged += new System.EventHandler(this.NupQuantity_ValueChanged);
            // 
            // BtnAddToInvoice
            // 
            this.BtnAddToInvoice.Location = new System.Drawing.Point(65, 438);
            this.BtnAddToInvoice.Name = "BtnAddToInvoice";
            this.BtnAddToInvoice.Size = new System.Drawing.Size(153, 34);
            this.BtnAddToInvoice.TabIndex = 22;
            this.BtnAddToInvoice.Text = "اضافه للفاتوره";
            this.BtnAddToInvoice.UseVisualStyleBackColor = true;
            this.BtnAddToInvoice.Click += new System.EventHandler(this.button1_Click);
            // 
            // CtrlFood
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.BtnAddToInvoice);
            this.Controls.Add(this.NupQuantity);
            this.Controls.Add(this.CbSize);
            this.Controls.Add(this.LbFoodPrice);
            this.Controls.Add(this.LbFoodName);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.FoodPicture);
            this.Controls.Add(this.EditPicture);
            this.Controls.Add(this.label4);
            this.Name = "CtrlFood";
            this.Size = new System.Drawing.Size(318, 475);
            this.Load += new System.EventHandler(this.CtrlFood_Load);
            ((System.ComponentModel.ISupportInitialize)(this.EditPicture)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FoodPicture)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NupQuantity)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox EditPicture;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.PictureBox FoodPicture;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label LbFoodName;
        private System.Windows.Forms.Label LbFoodPrice;
        private System.Windows.Forms.ComboBox CbSize;
        private System.Windows.Forms.NumericUpDown NupQuantity;
        private System.Windows.Forms.Button BtnAddToInvoice;
    }
}
