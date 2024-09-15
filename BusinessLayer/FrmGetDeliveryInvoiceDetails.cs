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
    public partial class FrmGetDeliveryInvoiceDetails : Form
    {
        public int InvoiceId;
        public FrmGetDeliveryInvoiceDetails(int InvoiceId)
        {
            InitializeComponent();
            this.InvoiceId = InvoiceId;
        }
        private DataTable GetAllDriversName()
        {
          return  ClsDrivers.GetAllDriversName();
        }
        private void SetDtiversNameAtCompoBox()
        {
            DataTable dt = GetAllDriversName();
            foreach(DataRow Row in dt.Rows)
            {
                CbDelivryMan.Items.Add(Row["DriverName"]);
            }
        }
        private void FrmGetDeliveryInvoiceDetails_Load(object sender, EventArgs e)
        {
            SetDtiversNameAtCompoBox();
        }
        private bool IsInfoCompeleteAndCosistancy()
        {
            return (!string.IsNullOrEmpty(TxClientAddress.Text) && !string.IsNullOrEmpty(TxClientPhone.Text) );
           
        }
        private void AddNewDriverIfIsNotExist()
        {
            if (!ClsDrivers.IsThisDriverAlreadeyExists(CbDelivryMan.Text))
            {
                ClsDrivers.AddNew(CbDelivryMan.Text);
            }
        }
        private void GetDliveryDetails()
        {
            
            ClsSales.CurrentDeliveryDetails.DelviryGuyName = CbDelivryMan.Text;
            ClsSales.CurrentDeliveryDetails.ClientAddress = TxClientAddress.Text;
            ClsSales.CurrentDeliveryDetails.ClientPhone = TxClientPhone.Text;
        }
        private void BtnPrintBill_Click(object sender, EventArgs e)
        {
            if(IsInfoCompeleteAndCosistancy())
            {
                AddNewDriverIfIsNotExist();
                GetDliveryDetails();
                FrmInvoice Invoice = new FrmInvoice(this.InvoiceId,FrmInvoice.EnMode.DeliveryMode);
                Invoice.ShowDialog();

            }
            else
            {
                ClsSettings.ShowMessagboxForUnCompeleteDetails();
            }
        
        }
    }
}
