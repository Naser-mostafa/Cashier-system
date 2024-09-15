using CadeDateACcess;
using CafeDateAccess;
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
    public partial class FrmLogin : Form
    {
       public byte counter =0;
        public FrmLogin()
        {
            InitializeComponent();
            counter = 3;
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            if(counter!=0)
            {
                if (ClsUser.IsTHisUserExists(TxEmail.Text, TxPassword.Text))
                {
                    this.Hide();
                    Main frm = new Main();
                    frm.Show();
                    frm.FormClosed += (s, args) => this.Close();
                }
                else
                {
                    counter--;
                    MessageBox.Show(" خطأ في كلمه السر او البريد الالكتروني", "كلمه مرور خاطئه", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                counter = 3;
                ClsSettings.SendEmail("تحذير", "انا قلق بشأنك فهناك من يحاول الدخول الي البرنامج الخاص بك وقد ادخل كلمه السر ثلاث مرات خاطئه علي لتوالي يرجي الاهتمام بذالك الموضوع", ClsUser.GetUserName());
            }
          
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            
            ClsSettings.SendEmail("استرداد كلمه المرور", $"كلمه المرور هي{ClsUser.GetPassword() }لا تشاركها مع اي احد ", ClsUser.GetUserName());
            MessageBox.Show("تاكد ان الجهاز متصل بالانترنت ليقوم بارسال كلمه السر لك عبر البريد الالكتروني");
        }

        private void TxEmail_MouseHover(object sender, EventArgs e)
        {
            if(TxEmail.Text=="البريد الالكتروني")
            TxEmail.Text = "";
        }

        private void TxEmail_MouseLeave(object sender, EventArgs e)
        {
            if (TxEmail.Text == "البريد الالكتروني")
                TxEmail.Text = "";
        }

        private void TxPassword_MouseHover(object sender, EventArgs e)
        {
            if(TxPassword.Text=="كلمه السر")
            TxPassword.Text = "";
        }

        private void TxPassword_MouseLeave(object sender, EventArgs e)
        {
            if (TxPassword.Text == "كلمه السر")
                TxPassword.Text = "";
        }

        private void FrmLogin_Load(object sender, EventArgs e)
        {

        }
    }
}
