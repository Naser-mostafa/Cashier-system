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
    public partial class FrmCafeDetails : Form
    {
        public FrmCafeDetails()
        {
            InitializeComponent();
        }
        private class CafeDetails
        {
            public string PhoneNumber="";
            public string Address="";
            public decimal Taxes=0;
        }
            private  CafeDetails CurrentDetails = new CafeDetails();

            private void GetInfoFromui()
            {
                CurrentDetails.Address = TxCafeAddress.Text;
                CurrentDetails.Taxes = Convert.ToDecimal(NudTaxes.Value);
                CurrentDetails.PhoneNumber = TxCafePhone.Text;
            }
            private void SetInfoFromToui()
            {
                TxCafeAddress.Text = CurrentDetails.Address;
                NudTaxes.Value = Convert.ToDecimal(CurrentDetails.Taxes); ;
                TxCafePhone.Text = CurrentDetails.PhoneNumber;
            }
            private void GetInfoFromDataBase()
            {
                ClsCafeDetails.GetDetails(ref CurrentDetails.PhoneNumber,ref  CurrentDetails.Address,ref  CurrentDetails.Taxes);
            }
        private bool SetNewinfoIntoDatabase()
        {
         return   ClsCafeDetails.UpdateDetails( CurrentDetails.PhoneNumber,  CurrentDetails.Address,  CurrentDetails.Taxes);
        }
        private void FrmCafeDetails_Load(object sender, EventArgs e)
            {
            GetInfoFromDataBase();
            SetInfoFromToui();
            }

        private void button1_Click(object sender, EventArgs e)
        {
            GetInfoFromui();
            if(SetNewinfoIntoDatabase())
            {
                ClsSettings.ShowMessagboxForSuccessUPdating();
                this.Close();
            }
            else
            {
                ClsSettings.ShowMessagboxForFalireOperations();

            }
        }
    }
    }


