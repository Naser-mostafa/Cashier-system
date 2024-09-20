namespace Cafe
{
    partial class FrmSettings
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
            this.TxEmail = new System.Windows.Forms.TextBox();
            this.TxPassword = new System.Windows.Forms.TextBox();
            this.TxConfirmPassword = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // TxEmail
            // 
            this.TxEmail.BackColor = System.Drawing.SystemColors.Highlight;
            this.TxEmail.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxEmail.ForeColor = System.Drawing.SystemColors.Desktop;
            this.TxEmail.Location = new System.Drawing.Point(12, 140);
            this.TxEmail.Name = "TxEmail";
            this.TxEmail.Size = new System.Drawing.Size(341, 28);
            this.TxEmail.TabIndex = 0;
            this.TxEmail.Text = "البريد الالكتروني الجديد";
            this.TxEmail.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.toolTip1.SetToolTip(this.TxEmail, "يجب ادخال بريد جميل ");
            this.TxEmail.TextChanged += new System.EventHandler(this.TxEmail_TextChanged);
            this.TxEmail.MouseEnter += new System.EventHandler(this.TxEmail_MouseEnter);
            this.TxEmail.MouseHover += new System.EventHandler(this.TxEmail_MouseHover);
            // 
            // TxPassword
            // 
            this.TxPassword.BackColor = System.Drawing.SystemColors.Highlight;
            this.TxPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxPassword.ForeColor = System.Drawing.SystemColors.Desktop;
            this.TxPassword.Location = new System.Drawing.Point(12, 203);
            this.TxPassword.Name = "TxPassword";
            this.TxPassword.Size = new System.Drawing.Size(341, 28);
            this.TxPassword.TabIndex = 1;
            this.TxPassword.Text = "كلمه السر الجديده";
            this.TxPassword.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.TxPassword.MouseEnter += new System.EventHandler(this.TxPassword_MouseEnter);
            this.TxPassword.MouseHover += new System.EventHandler(this.TxPassword_MouseHover);
            // 
            // TxConfirmPassword
            // 
            this.TxConfirmPassword.BackColor = System.Drawing.SystemColors.HotTrack;
            this.TxConfirmPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxConfirmPassword.ForeColor = System.Drawing.SystemColors.Desktop;
            this.TxConfirmPassword.Location = new System.Drawing.Point(12, 267);
            this.TxConfirmPassword.Name = "TxConfirmPassword";
            this.TxConfirmPassword.Size = new System.Drawing.Size(341, 28);
            this.TxConfirmPassword.TabIndex = 2;
            this.TxConfirmPassword.Text = "تاكيد كلمه السر";
            this.TxConfirmPassword.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.TxConfirmPassword.MouseEnter += new System.EventHandler(this.TxConfirmPassword_MouseEnter);
            this.TxConfirmPassword.MouseHover += new System.EventHandler(this.TxConfirmPassword_MouseHover);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.SystemColors.Highlight;
            this.button1.Font = new System.Drawing.Font("Open Sans", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.button1.Location = new System.Drawing.Point(149, 352);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(88, 35);
            this.button1.TabIndex = 6;
            this.button1.Text = "حفظ";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(4, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(363, 69);
            this.label4.TabIndex = 7;
            this.label4.Text = "معلومات الدخول";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // FrmSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.HotTrack;
            this.ClientSize = new System.Drawing.Size(379, 410);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.TxConfirmPassword);
            this.Controls.Add(this.TxPassword);
            this.Controls.Add(this.TxEmail);
            this.Name = "FrmSettings";
            this.Text = "FrmSettings";
            this.Load += new System.EventHandler(this.FrmSettings_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox TxEmail;
        private System.Windows.Forms.TextBox TxPassword;
        private System.Windows.Forms.TextBox TxConfirmPassword;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}