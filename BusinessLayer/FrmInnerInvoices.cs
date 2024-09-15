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
    public partial class FrmInnerInvoices : Form
    {
        
        public FrmInnerInvoices( int  MultiInvoiceId)
        {
            InitializeComponent();
            label1.Text = " استرجاع من الفاتوره";
            AllSingleInvoices = SingleSalescs.GetInvoicesWhereInvoiceId(MultiInvoiceId);
            DgvForInnerInvoices.ContextMenuStrip = contextMenuStrip1;
            DgvForInnerInvoices.DataSource = AllSingleInvoices;
            MultiInvoiceID = MultiInvoiceId;

        }
        public FrmInnerInvoices(int MultiInvoiceId,short num=0)
        {
            InitializeComponent();
            label1.Text = " الفواتير الداخليه";
            AllSingleInvoices = SingleSalescs.GetInvoicesWhereInvoiceId(MultiInvoiceId);
            DgvForInnerInvoices.ContextMenuStrip = null;
            DgvForInnerInvoices.DataSource = AllSingleInvoices;
            MultiInvoiceID = MultiInvoiceId;

        }
        byte OpenTableId;
        public FrmInnerInvoices(int MultiInvoiceId,byte tableId)
        {
            InitializeComponent();
            DgvForInnerInvoices.ContextMenuStrip = contextMenuStrip1;
            label1.Text = "استرجاع من الفاتوره";
            DataTable AllInvoices = SingleSalescs.GetInvoicesWhereInvoiceId(MultiInvoiceId);
            this.OpenTableId = tableId;
            DgvForInnerInvoices.DataSource = AllInvoices;
            MultiInvoiceID = MultiInvoiceId;

        }

        DataTable AllSingleInvoices = new DataTable();
        int MultiInvoiceID;
 
      
        int selectedRowIndex = -1;
        private void DgvForInnerInvoices_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {

            if (DgvForInnerInvoices.RowCount > 0)
            {
                if (selectedRowIndex != -1 && selectedRowIndex < DgvForInnerInvoices.RowCount)
                {
                    DgvForInnerInvoices.Rows[selectedRowIndex].Selected = false;
                }

                // تحديد الصف الجديد
                if (e.RowIndex >= 0 && e.RowIndex < DgvForInnerInvoices.RowCount)
                {
                    DgvForInnerInvoices.CurrentCell = DgvForInnerInvoices.Rows[e.RowIndex].Cells[0]; // اختر الخلية الأولى في الصف
                    DgvForInnerInvoices.Rows[e.RowIndex].Selected = true;
                    selectedRowIndex = e.RowIndex;
                }
            }
        }
        private bool IsLastRowSelected(DataGridView dataGridView)
        {
            // التحقق من وجود صف محدد في DataGridView
            if (dataGridView.SelectedRows.Count > 0)
            {
                // الحصول على الصف المحدد
                DataGridViewRow selectedRow = dataGridView.SelectedRows[0];

                // الحصول على آخر صف (باستثناء صف الإدخال الجديد إذا كان موجودًا)
                int lastRowIndex = dataGridView.Rows.Count - 1;
                if (dataGridView.AllowUserToAddRows) // إذا كان هناك صف للإدخال
                {
                    lastRowIndex--; // استبعاد صف الإدخال الجديد
                }

                // التحقق مما إذا كان الصف المحدد هو آخر صف
                if (dataGridView.Rows.IndexOf(selectedRow) == lastRowIndex)
                {
                    return true; // الصف المحدد هو آخر صف
                }
            }

            return false; // الصف المحدد ليس هو الأخير
        }

        private void CmsForMulti_Opening(object sender, CancelEventArgs e)
        {

        }

        private void استرجاعكلالفاتورهToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int invoiceId = int.Parse(DgvForInnerInvoices.CurrentRow.Cells["الرقم"].Value.ToString());
            int Quantity= int.Parse(DgvForInnerInvoices.CurrentRow.Cells["الكميه"].Value.ToString());
            string DrinkName= (DgvForInnerInvoices.CurrentRow.Cells["اسم المنتج"].Value.ToString());
            int Multi = int.Parse(DgvForInnerInvoices.CurrentRow.Cells["الفاتوره المدمجه"].Value.ToString());
            int SingleInvoiceId = 0; 

            {
                int DrinkId = ClsDrinks.GetIDByName(DrinkName);
                decimal Taxes = SingleSalescs.GetTaxes(invoiceId);
                if (SingleSalescs.DeleteInvoice(invoiceId))
                {
                    SingleInvoiceId = SingleSalescs.FindInvoiceIDBYMultiId(Multi);
                    Tables.DeleteOpenTableRecordByInvoiceId(Multi);
                    
                    if (IsLastRowSelected(DgvForInnerInvoices)&&Taxes!=0)
                    {
                        SingleSalescs.SetTaxesForTheLastRowByMulti_Id(Multi,Taxes);
                        Tables.SetTaxesAlLastInvoiceWithMulti(Multi);
                    }

                }
           
                //when there is no single invoice or has 1 single invoice we delete it cause its not contain more than one product
                if (!SingleSalescs.IsThereAreMore2SingleINvoicesONThisMulti(MultiInvoiceID))
                {
                    MultiInvoice.DeleteInvoice(MultiInvoiceID);
                    byte TableNumber = Tables.FindTableIDBYMultiId(MultiInvoiceID);
                    byte OpenTableId = Tables.FindOpenTableIdIDBYMultiId(MultiInvoiceID);
          //          int SingleInvoiceId = SingleSalescs.FindInvoiceIDBYMultiId(MultiInvoiceID);
                    Tables.SetTaxesAlLastInvoiceWithMulti(Multi);
                    SingleSalescs.CancelRelationBetweenMultiInvoiceAndSingle(MultiInvoiceID);
                    Tables.UpdateInvoiceIdWithNewMultInvoice(OpenTableId, SingleInvoiceId);
                    ClsSales.TableAndTheMultiBill[TableNumber] = SingleInvoiceId;
                }
            }
            ClsSettings.ShowSuccessMessageBoxForRecover();

            this.Close(); 
        }

        private void استرجاعجزءToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string input = Interaction.InputBox("كم عنصر تريد ان تسترجع", "استرجاع جزء من الفاتورة", "", -1, -1);
            float Price = ClsDrinks.GetPriceByName(DgvForInnerInvoices.CurrentRow.Cells["اسم المنتج"].Value.ToString());
            if (int.TryParse(input, out int UserInput) && Convert.ToInt32(input) < Convert.ToUInt32(DgvForInnerInvoices.CurrentRow.Cells["الكميه"].Value) && SingleSalescs.UpdateInvoice(Convert.ToInt32(DgvForInnerInvoices.CurrentRow.Cells["الرقم"].Value), Convert.ToInt32(input), Price))
            {
                ClsSettings.ShowSuccessMessageBoxForRecover();
                DgvForInnerInvoices.DataSource = SingleSalescs.GetInvoicesWhereInvoiceId(MultiInvoiceID); ;
            }
            else
            {          
                ClsSettings.ShowMessagboxForUnCompeleteDetails();
            }
            //when there is no single invoice or has 1 single invoice we delete it cause its not contain more than one product
            if (!SingleSalescs.IsThereAreMore2SingleINvoicesONThisMulti(MultiInvoiceID))
            {
                MultiInvoice.DeleteInvoice(MultiInvoiceID);
                SingleSalescs.CancelRelationBetweenMultiInvoiceAndSingle(MultiInvoiceID);
            }
        }

        private void FrmInnerInvoices_Load(object sender, EventArgs e)
        {
             DgvForInnerInvoices.AllowUserToAddRows = false;
            DgvForInnerInvoices.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void استرجاعجزءToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            
        }
    }
}
