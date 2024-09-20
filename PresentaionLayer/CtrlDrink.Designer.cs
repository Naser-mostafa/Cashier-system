namespace Cafe
{
    partial class CtrlDrink
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
            this.label1 = new System.Windows.Forms.Label();
            this.BtnAddTOInvoice = new System.Windows.Forms.Button();
            this.NupQuantity = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.LbProductName = new System.Windows.Forms.Label();
            this.LbProductPrice = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.DrinkPicture = new System.Windows.Forms.PictureBox();
            this.EditPicture = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.NupQuantity)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DrinkPicture)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.EditPicture)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(241, 263);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 32);
            this.label1.TabIndex = 1;
            this.label1.Text = "السعر";
            // 
            // BtnAddTOInvoice
            // 
            this.BtnAddTOInvoice.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnAddTOInvoice.Location = new System.Drawing.Point(60, 392);
            this.BtnAddTOInvoice.Name = "BtnAddTOInvoice";
            this.BtnAddTOInvoice.Size = new System.Drawing.Size(192, 36);
            this.BtnAddTOInvoice.TabIndex = 3;
            this.BtnAddTOInvoice.Text = "اضافه الي الفاتوره";
            this.BtnAddTOInvoice.UseVisualStyleBackColor = true;
            this.BtnAddTOInvoice.Click += new System.EventHandler(this.BtnSell_Click);
            this.BtnAddTOInvoice.MouseLeave += new System.EventHandler(this.BtnSell_MouseLeave);
            this.BtnAddTOInvoice.MouseHover += new System.EventHandler(this.BtnSell_MouseHover);
            // 
            // NupQuantity
            // 
            this.NupQuantity.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NupQuantity.Location = new System.Drawing.Point(38, 319);
            this.NupQuantity.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.NupQuantity.Name = "NupQuantity";
            this.NupQuantity.Size = new System.Drawing.Size(106, 30);
            this.NupQuantity.TabIndex = 4;
            this.NupQuantity.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.NupQuantity.ValueChanged += new System.EventHandler(this.NupQuantity_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(248, 222);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 36);
            this.label3.TabIndex = 5;
            this.label3.Text = "الاسم";
            // 
            // LbProductName
            // 
            this.LbProductName.AutoSize = true;
            this.LbProductName.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LbProductName.Location = new System.Drawing.Point(33, 222);
            this.LbProductName.Name = "LbProductName";
            this.LbProductName.Size = new System.Drawing.Size(39, 29);
            this.LbProductName.TabIndex = 6;
            this.LbProductName.Text = "؟؟";
            // 
            // LbProductPrice
            // 
            this.LbProductPrice.AutoSize = true;
            this.LbProductPrice.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LbProductPrice.Location = new System.Drawing.Point(33, 263);
            this.LbProductPrice.Name = "LbProductPrice";
            this.LbProductPrice.Size = new System.Drawing.Size(39, 29);
            this.LbProductPrice.TabIndex = 7;
            this.LbProductPrice.Text = "؟؟";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(241, 309);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 32);
            this.label2.TabIndex = 8;
            this.label2.Text = "الكميه";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.RoyalBlue;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(231, 14);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(44, 20);
            this.label4.TabIndex = 9;
            this.label4.Text = "تعديل";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // DrinkPicture
            // 
            this.DrinkPicture.BackgroundImage = global::Cafe.Properties.Resources.images;
            this.DrinkPicture.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.DrinkPicture.Location = new System.Drawing.Point(3, 40);
            this.DrinkPicture.Name = "DrinkPicture";
            this.DrinkPicture.Size = new System.Drawing.Size(321, 179);
            this.DrinkPicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.DrinkPicture.TabIndex = 11;
            this.DrinkPicture.TabStop = false;
            // 
            // EditPicture
            // 
            this.EditPicture.BackColor = System.Drawing.Color.LightYellow;
            this.EditPicture.Image = global::Cafe.Properties.Resources.Settings;
            this.EditPicture.Location = new System.Drawing.Point(281, 14);
            this.EditPicture.Name = "EditPicture";
            this.EditPicture.Size = new System.Drawing.Size(34, 20);
            this.EditPicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.EditPicture.TabIndex = 10;
            this.EditPicture.TabStop = false;
            this.EditPicture.Click += new System.EventHandler(this.EditPicture_Click);
            // 
            // CtrlDrink
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkCyan;
            this.Controls.Add(this.DrinkPicture);
            this.Controls.Add(this.EditPicture);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.LbProductPrice);
            this.Controls.Add(this.LbProductName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.NupQuantity);
            this.Controls.Add(this.BtnAddTOInvoice);
            this.Controls.Add(this.label1);
            this.Name = "CtrlDrink";
            this.Size = new System.Drawing.Size(327, 431);
            this.Load += new System.EventHandler(this.CtrlDrink_Load);
            ((System.ComponentModel.ISupportInitialize)(this.NupQuantity)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DrinkPicture)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.EditPicture)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button BtnAddTOInvoice;
        private System.Windows.Forms.NumericUpDown NupQuantity;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label LbProductName;
        private System.Windows.Forms.Label LbProductPrice;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.PictureBox EditPicture;
        private System.Windows.Forms.PictureBox DrinkPicture;
    }
}
