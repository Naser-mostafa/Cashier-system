using CadeDateACcess;
using CafeDateAccess;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cafe
{
    public partial class FrmSettings : Form
    {
        public FrmSettings()
        {
            InitializeComponent();
            TxEmail.Text = ClsUser.GetUserName();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
        private bool IsINformationCOmpeleteAndConsistant()
        {
            return (TxPassword.Text == TxConfirmPassword.Text && !string.IsNullOrEmpty(TxEmail.Text) && !string.IsNullOrEmpty(TxPassword.Text)&&TxEmail.Text.Contains("@gmail.com"));
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if(IsINformationCOmpeleteAndConsistant())
            {
                string Result = Interaction.InputBox("ادخل كلمه السر القديمه لتحديث المعلومات");

                if (Result == ClsUser.GetPassword())
                {
                    if (ClsUser.UpdateUserEmailAndPassword(TxEmail.Text, TxPassword.Text))
                    {
                        ClsSettings.ShowMessagboxForSuccessUPdating();
                        this.Close();
                    }
                    else
                    {
                        ClsSettings.ShowMessagboxForFalireOperations();
                    }
                }
                else
                {
                    ClsSettings.ShowMessagboxForUnCompeleteDetails();
                }
            }
            else
            {
                ClsSettings.ShowMessagboxForUnCompeleteDetails();

            }

        }

        private void TxEmail_MouseHover(object sender, EventArgs e)
        {
            if (TxEmail.Text == "البريد الالكتروني الجديد") 
            TxEmail.Text = "";
        }

        private void TxEmail_MouseEnter(object sender, EventArgs e)
        {
            if (TxEmail.Text == "البريد الالكتروني الجديد") 
            TxEmail.Text = "";
        }

        private void TxPassword_MouseEnter(object sender, EventArgs e)
        {
            if (TxPassword.Text == "كلمه السر الجديده") 
            TxPassword.Text = "";
        }

        private void TxPassword_MouseHover(object sender, EventArgs e)
        {
            if (TxPassword.Text == "كلمه السر الجديده") 
            TxPassword.Text = "";
        }

        private void TxConfirmPassword_MouseEnter(object sender, EventArgs e)
        {
            if (TxConfirmPassword.Text == "تاكيد كلمه السر")             TxConfirmPassword.Text = "";
        }

        private void TxConfirmPassword_MouseHover(object sender, EventArgs e)
        {
            if (TxConfirmPassword.Text == "تاكيد كلمه السر") 
            TxConfirmPassword.Text = "";
        }

        private void TxEmail_TextChanged(object sender, EventArgs e)
        {

        }

        private void FrmSettings_Load(object sender, EventArgs e)
        {

        }
    }
}
