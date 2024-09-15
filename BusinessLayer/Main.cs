using CadeDateACcess;
using Cafe.Properties;
using CafeDateAccess;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media.Imaging;

namespace Cafe
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }
        public enum Mode
        {
            AddNew, AddOnCurrent
        }

        public Mode CurrentMode = Mode.AddNew;

        public void RefreshDrinksForAddNew(byte TableId = 0, bool delivery = false)
        {

            List<DrinkDetails> drinks = ClsDrinks.GetAllDrinks();
            FlpFoodAndDrinks.Controls.Clear();
            foreach (var drink in drinks)
            {
                CtrlDrink ctrlDrink = new CtrlDrink
                {
                    DrinkID = Convert.ToInt16(drink.ID),
                    DrinkName = drink.DrinkName,
                    Price = float.Parse(drink.Price.ToString()),
                    ImagePath = drink.PicturePath,
                    TableID = TableId

                };
                ctrlDrink.CurrentMode = CtrlDrink.Mode.AddNew;
                ctrlDrink.Delivery = delivery;

                ctrlDrink.SingleSalesOrNot += IsSIngleSales;
                ctrlDrink.SetImageAtPictureBox();
                ctrlDrink.BackColor = Color.Gray;
                FlpFoodAndDrinks.Controls.Add(ctrlDrink);
            }


        }
        public void RefreshDrinksForAddonCurrentInvoice(int Multi_invoice, byte tableId)
        {
            List<DrinkDetails> drinks = ClsDrinks.GetAllDrinks();
            FlpFoodAndDrinks.Controls.Clear();
            foreach (var drink in drinks)
            {
                CtrlDrink ctrlDrink = new CtrlDrink
                (
                    Multi_invoice,
                     Convert.ToInt16(drink.ID),
                   drink.DrinkName,
                     float.Parse(drink.Price.ToString()),
                     drink.PicturePath,
                    tableId
                );
                ctrlDrink.CurrentMode = CtrlDrink.Mode.AddOnCurrent;

                ctrlDrink.Delivery = false;
                IsSIngleSales(false);
                ctrlDrink.SetImageAtPictureBox();
                ctrlDrink.BackColor = Color.Gray;

                FlpFoodAndDrinks.Controls.Add(ctrlDrink);
            }

        }
        public void RefreshFoodsForAddonCurrentInvoice(int Multi_invoice, byte tableId)
        {


            List<FoodDetails> drinks = ClsDrinks.GetAllFoods();
            FlpFoodAndDrinks.Controls.Clear();
            foreach (var drink in drinks)
            {
                CtrlFood ctrlDrink = new CtrlFood
                (
                    Multi_invoice,
                     Convert.ToInt16(drink.ID),
                   drink.DrinkName,
                     float.Parse(drink.Price.ToString()),
                     drink.PicturePath,
                    tableId

                );
                ctrlDrink.CurrentMode = CtrlFood.Mode.AddOnCurrent;

                ctrlDrink.Delivery = false;
                IsSIngleSales(false);
                ctrlDrink.SetImageAtPictureBox();
                ctrlDrink.BackColor = Color.Gray;
                FlpFoodAndDrinks.Controls.Add(ctrlDrink);
            }

        }
        public void RefreshFoods(byte TableId = 0, bool delivery = false)
        {

            List<FoodDetails> drinks = ClsDrinks.GetAllFoods();
            FlpFoodAndDrinks.Controls.Clear();
            foreach (var drink in drinks)
            {
                CtrlFood ctrlDrink = new CtrlFood
                {
                    DrinkID = Convert.ToInt16(drink.ID),
                    DrinkName = drink.DrinkName,
                    Price = float.Parse(drink.Price.ToString()),
                    ImagePath = drink.PicturePath,
                    TableId = TableId


                };
                ctrlDrink.CurrentMode = CtrlFood.Mode.AddNew;
                ctrlDrink.Delivery = delivery;
                ctrlDrink.SingleSalesOrNot += IsSIngleSales;
                ctrlDrink.SetSizeLarge();
                ctrlDrink.SetImageAtPictureBox();
                ctrlDrink.BackColor = Color.Gray;
                FlpFoodAndDrinks.Controls.Add(ctrlDrink);
            }

        }
        private void SetUpToPerformMain()
        {
            foreach (Control control in this.Controls)
            {
                if (control.Name != BtnInvoices.Name && control.Name != FlpTabels.Name && control.Name != LbDelibery.Name && control.Name != EditInvoiceDetails.Name && control.Name != EditProfileDetials.Name)
                {
                    control.Visible = false;
                }
                else
                {
                    control.Visible = true;

                }
            }
        }
        public static byte TableNumber = 0;
        private void TableButton_MouseDown(object sender, MouseEventArgs e)
        {
            Button btn = sender as Button;
            if (e.Button == MouseButtons.Right)
            {
                btn.ContextMenuStrip.Show(btn, e.Location);
                btn.Enabled = true; // بعد عرض القائمة، يمكنك إعادة الزر إلى حالته غير الممكنة
            }
        }
        private void CreateAllTables(DataTable table)
        {
            foreach (DataRow row in table.Rows)
            {
                Button btn = new Button();
                btn.Text = row["ID"].ToString();
                // ضبط حجم الزر
                btn.Size = new Size(200, 200);
                btn.FlatStyle = FlatStyle.Flat;
                if(ClsSales.TableAndTheMultiBill.TryGetValue(byte.Parse(btn.Text),out int value))
                {
                    btn.BackColor = Color.Red;

                }
                else
                {
                    btn.BackColor = Color.Green;
                }
                btn.ForeColor = Color.Black;
                btn.Font = new Font("Arial", 20, FontStyle.Bold);
                FlpTabels.Controls.Add(btn);
                btn.ContextMenuStrip = contextMenuStrip1;
                btn.Click += new EventHandler(TableButton_MouseClick);
                btn.MouseDown += new MouseEventHandler(CreateHoverButton);
                FlpTabels.Controls.Add(btn);
            }


        }
        private void CreateHoverButton(object sender, EventArgs e)
        {


            Button btn = sender as Button;
            hoveredButton = btn;


        }

        public Button hoveredButton;
        private void DeleteAllTables()
        {
            var controlsToRemove = FlpTabels.Controls.OfType<Control>().ToList();

            // الآن يمكنك التخلص من كل عنصر بشكل آمن
            foreach (Control control in controlsToRemove)
            {
                control.Dispose();
            }

            // تنظيف اللوحة من جميع العناصر
            FlpTabels.Controls.Clear();
        }
        private void deleteAllFoodAndDrinks()
        {
            var controlsToRemove = FlpFoodAndDrinks.Controls.OfType<Control>().ToList();

            // الآن يمكنك التخلص من كل عنصر بشكل آمن
            foreach (Control control in controlsToRemove)
            {
                control.Dispose();
            }

            // تنظيف اللوحة من جميع العناصر
            FlpTabels.Controls.Clear();
        }
        private void TableButton_MouseClick(object sender, EventArgs e)
        {
            ClsSales.counter = 0;
            hoveredButton = sender as Button;
            hoveredButton.MouseDown += (TableButton_MouseDown);
            if (hoveredButton != null)
            {
                Button clickedButton = sender as Button;
                if (Tables.GetAllInvoicesOnTable(Convert.ToByte(hoveredButton.Text)).Rows.Count >= 1)
                {
                    MessageBox.Show("هذه الطاوله يوجد عليها طلبات يرجي استخراج فاتوره الطاوله اولا", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error); ;

                    return;
                }

                SetUpToShowPuchasess();
                HideAllComtrolsBelongsToTableForm();
                LbCurrentTable.Visible = true;
                LbCurrentTable.Text = $"طاوله رقم {TableNumber.ToString()}";
                GbCategories.Visible = true;
                DeleteAllTables();
                RefreshDrinksForAddNew(Convert.ToByte(TableNumber), false);
                IsDelivery = false;
            }
        }
        private void SetUpToShowPuchasess()
        {

            GbPurchase.BackColor = Color.Transparent;
            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView2.RowHeadersVisible = false;
            EnableVisibleAllControlsThatBelongsToDrinkes();
            CtrlDrink.SellSomething += AddToPurchases;
            CtrlFood.SellSomething += AddToPurchases;
            CtrlDrink.OnBillDone += BtnShowDrinks_Click;
            CtrlFood.OnBillDone += BtnShowDrinks_Click;
            FlpFoodAndDrinks.AutoScroll = true;
            FlpTabels.AutoScroll = true;
            FlpTabels.Visible = false;
            LbDelibery.Visible = false;
            BtnInvoices.Visible = false;
            BtnShowTables.Visible = true;
            TableNumber = Convert.ToByte(hoveredButton.Text);
            AddFood.Visible = false;
            AddDrink.Visible = false;
            //  AddTable.Visible = false;
            BtnFoods.Visible = true;
            btnDrinks.Visible = true;
            Refresh.Visible = true;
        }

        bool IsSingle = false;
        private void IsSIngleSales(bool IsSingle)
        {
            this.IsSingle = IsSingle;
            try
            {
                hoveredButton.Tag = IsSingle.ToString();

            }
            catch (Exception)
            {

            }
        }
        private void CachImage()
        {
            
        }
        public class ImageCash
        {
            private static Image CahchBackgroundImage = null;
            public static Image GetCachedImage()
            {
                if(CahchBackgroundImage==null)
                {
                    CahchBackgroundImage = Resources.background1;
                   
                }
                return CahchBackgroundImage;
            }
        }
      public  DataTable AllTables;
        private void Form1_Load(object sender, EventArgs e)
        {
             //that mean add new mode bt defailt
            ClsSales.counter = 0;
            this.DoubleBuffered = true;
           this.BackgroundImage = ImageCash.GetCachedImage();
            FlpFoodAndDrinks.BackColor = Color.DodgerBlue;
            FlpTabels.AutoScroll = true;
            ClsSales.TableAndTheMultiBill = Tables.GetAllOpenInvoices();
            CreateAllTables(Tables.GetAllTables());
            DeleteTheLastOpenTablesWhenSystemWasCLosed();
            SetUpToPerformMain();
            AddDrink.Visible = true;
            AddFood.Visible = true;
            AddTable.Visible = true;

        }
        private void DeleteTheLastOpenTablesWhenSystemWasCLosed()
        {
            foreach (KeyValuePair<byte, int> KVP in ClsSales.TableAndTheMultiBill)
            {
                Tables.DeleteTablesAndTheirInvoices(KVP.Key);
            }
        }

        public void AddToPurchases(string purchases, bool reset)
        {
            if (reset)
            {
                BtnShowTables.Visible = true; ;
                ClsSales.purchasesBuilder.Clear();
                LbPurchase.Text = null;
                return;
                // مسح النصوص السابقة إذا كان reset = true
            }
            else
            {
                BtnShowTables.Visible = false;
                ClsSales.purchasesBuilder.Append($"{purchases}\n");
            }

            // تحديث النص في Label
            LbPurchase.Text = ClsSales.purchasesBuilder.ToString();
            return;
        }

        private void LbAddNew_Click(object sender, EventArgs e)
        {
            //-1 for add new cause this form use for update too
            FrmAddEditDrink AddNew = new FrmAddEditDrink(-1, FrmAddEditDrink.FoodOrDrink.Drink);
            AddNew.ShowDialog();
            RefreshDrinksForAddNew(0, IsDelivery);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            //-1 for add new cause this form use for update too
            FrmAddEditDrink AddNew = new FrmAddEditDrink(-1, FrmAddEditDrink.FoodOrDrink.Drink);
            AddNew.ShowDialog();
            RefreshDrinksForAddNew(0, IsDelivery);
        }
        private void CancelVisibaleForDrinkesControl()
        {
            FlpFoodAndDrinks.Visible = false;
            AddDrink.Visible = false;
            GbPurchase.Visible = false;
            BtnInvoices.Visible = false;
        }
        private void CancelVisibleAllControlsThatBelongsToDrinkes()
        {
            CancelVisibaleForDrinkesControl();
            HideControlsThatBelongsToMultiInvoices();
            ShowControlsThatBelongsToSingleiInvoices();
            label.Visible = true;
            LbDelibery.Visible = false;
            TxSearchForSingle.Visible = true;
            TxSearchForMulti.Visible = false;
            BtnShowTables.Visible = true;
            dataGridView2.Visible = true;
        }
        private void EnableVisibleAllControlsThatBelongsToDrinkes()
        {
            LbTotal.Visible = false;
            FlpFoodAndDrinks.Visible = true;
            label.Visible = false;
            TxSearchForMulti.Visible = false;
            TxSearchForSingle.Visible = false;
            //this for add new drink picture
            AddDrink.Visible = true;
            BtnInvoices.Visible = true;
            GxSales.Visible = false;
            GbPurchase.Visible = true;
            btnSingleInvoice.Visible = false;
            BtnNextSingle.Visible = false;
            BtnPrevMulti.Visible = false;
            btnNextMulti.Visible = false;
            BtnPrevSingle.Visible = false;
            BtnMultiInvoice.Visible = false;
            dataGridView2.Visible = false;
            BtnShowTables.Visible = false;
        }
        private void HideAllComtrolsBelongsToTableForm()
        {
            EditInvoiceDetails.Visible = false;
            EditProfileDetials.Visible = false;
            AddDrink.Visible = false;
            AddFood.Visible = false;
            AddTable.Visible = false;
        }
        private void ShowAllComtrolsBelongsToTableForm()
        {
            EditInvoiceDetails.Visible = true;
            EditProfileDetials.Visible = true;
            AddDrink.Visible = true;
            AddFood.Visible = true;
            AddTable.Visible = true;
        }
        private void SetDataGridViewBeauty()
        {
            dataGridView2.ContextMenuStrip = CmsForSingle;
            dataGridView2.RowHeadersVisible = false;
            dataGridView2.RightToLeft = RightToLeft.Yes;
            dataGridView2.ColumnHeadersDefaultCellStyle.BackColor = Color.LightBlue;
            dataGridView2.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            dataGridView2.ColumnHeadersDefaultCellStyle.Font = new Font("Opensans", 12, FontStyle.Bold);
            dataGridView2.RowHeadersDefaultCellStyle.BackColor = Color.DarkBlue;
            dataGridView2.RowsDefaultCellStyle.SelectionBackColor = Color.LightBlue;

        }
        private void BtnInvoices_Click(object sender, EventArgs e)
        {

            HideAllComtrolsBelongsToTableForm();
            GxSales.Visible = true;
            LbTotal.Visible = false;
            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            FlpTabels.Visible = false;
            btnSingleInvoice.Visible = true;
            SetDataGridViewBeauty();
            ; BtnMultiInvoice.Visible = true;
            CancelVisibleAllControlsThatBelongsToDrinkes();
            dataGridView2.DataSource = SingleSalescs.GetAllInvoices(Convert.ToInt32(BtnNextSingle.Tag));
            dataGridView2.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

        }
        public string PurchasesText
        {
            get { return LbPurchase.Text; } // lblMyLabel هو اسم الـ Label في الـ Form
        }

        private void BtnShowDrinks_Click(object sender, EventArgs e)
        {
            ShowAllComtrolsBelongsToTableForm();

            ClsSales.counter = 1;
            SetUpToPerformMain();
            dataGridView2.DataSource = null;
            AddToPurchases(null, true);
            CtrlDrink.SellSomething -= AddToPurchases;
            CtrlFood.SellSomething -= AddToPurchases;
            CtrlDrink.OnBillDone -= BtnShowDrinks_Click;
            CtrlFood.OnBillDone -= BtnShowDrinks_Click;
            AddDrink.Visible = true;
            LbCurrentTable.Visible = false;
            deleteAllFoodAndDrinks();
            
            CreateAllTables(Tables.GetAllTables());

            AddFood.Visible = true;
            AddTable.Visible = true;
        }
        private void HideControlsThatBelongsToMultiInvoices()
        {
            BtnPrevMulti.Visible = false;
            btnNextMulti.Visible = false;
        }
        private void ShowControlsThatBelongsToMultiInvoices()
        {
            BtnPrevMulti.Visible = true;
            btnNextMulti.Visible = true;
        }
        private void btnSingleInvoice_Click(object sender, EventArgs e)
        {
            dataGridView2.DataSource = SingleSalescs.GetAllInvoices(Convert.ToInt32(BtnNextSingle.Tag));
            dataGridView2.ContextMenuStrip = CmsForSingle;
            LbTotal.Visible = false;
            HideControlsThatBelongsToMultiInvoices();
            ShowControlsThatBelongsToSingleiInvoices();
            TxSearchForSingle.Visible = true;
            TxSearchForMulti.Visible = false;
        }
        private void ShowControlsThatBelongsToSingleiInvoices()
        {
            BtnNextSingle.Visible = true;
            BtnPrevSingle.Visible = true;
        }
        private void hideControlsThatBelongsToSingleInvoices()
        {

            BtnNextSingle.Visible = false;
            BtnPrevSingle.Visible = false;
        }


        private void BtnMultiInvoice_Click(object sender, EventArgs e)
        {
            LbTotal.Visible = false;
            dataGridView2.DataSource = MultiInvoice.GetAllInvoices(Convert.ToInt32(btnNextMulti.Tag));
            dataGridView2.ContextMenuStrip = CmsMulti;
            hideControlsThatBelongsToSingleInvoices();
            ShowControlsThatBelongsToMultiInvoices();
            TxSearchForSingle.Visible = false;
            TxSearchForMulti.Visible = true;

        }
        private DataTable RefreshAllSingleSales()
        {
            return SingleSalescs.GetAllInvoices(Convert.ToInt32(BtnNextSingle.Tag)); ;
        }
        private void BtnNextSingle_Click(object sender, EventArgs e)
        {
            BtnNextSingle.Tag = int.Parse(BtnNextSingle.Tag.ToString()) + 1;
            dataGridView2.DataSource = RefreshAllSingleSales();
        }

        private void BtnPrevSingle_Click(object sender, EventArgs e)
        {
            BtnNextSingle.Tag = int.Parse(BtnNextSingle.Tag.ToString()) - 1;
            dataGridView2.DataSource = RefreshAllSingleSales();
        }
        private DataTable RefreshAllMultiSales()
        {
            return MultiInvoice.GetAllInvoices(Convert.ToInt32(btnNextMulti.Tag));
        }
        private void BtnPrevMulti_Click(object sender, EventArgs e)
        {
            btnNextMulti.Tag = int.Parse(btnNextMulti.Tag.ToString()) - 1;
            dataGridView2.DataSource = RefreshAllMultiSales();
        }
        private void btnNextMulti_Click(object sender, EventArgs e)
        {
            btnNextMulti.Tag = int.Parse(btnNextMulti.Tag.ToString()) + 1;
            dataGridView2.DataSource = RefreshAllMultiSales();
        }
        private DataTable RefreshTodaySales()
        {
            return SingleSalescs.GetDailySales();
        }
        private DataTable RefreshMonthlySales()
        {
            return SingleSalescs.GetMonthlySales();
        }
        private DataTable RefreshYearlySales()
        {
            return SingleSalescs.GetYearlySales();
        }
        private double SumTotalPrice(DataTable dt)
        {

            double totalSum = 0;
            foreach (DataRow row in dt.Rows)
            {
                totalSum += row.Field<double>("السعر الاجمالي");
            }
            return totalSum;
        }
        private void btnTodaySales_Click(object sender, EventArgs e)
        {
            LbTotal.Visible = true;
            hideControlsThatBelongsToSingleInvoices();
            HideControlsThatBelongsToMultiInvoices();
            dataGridView2.DataSource = RefreshTodaySales();
            LbTotal.Text = $" الاجمالي =                    {SumTotalPrice(RefreshTodaySales()).ToString()}";


        }

        private void BtnMonthlySales_Click(object sender, EventArgs e)
        {
            LbTotal.Visible = true;
            hideControlsThatBelongsToSingleInvoices();
            HideControlsThatBelongsToMultiInvoices();
            dataGridView2.DataSource = RefreshMonthlySales();
            LbTotal.Text = $" الاجمالي =                   {SumTotalPrice(RefreshMonthlySales()).ToString()}";

        }

        private void BtnYearlySales_Click(object sender, EventArgs e)
        {
            LbTotal.Visible = true;
            hideControlsThatBelongsToSingleInvoices();
            HideControlsThatBelongsToMultiInvoices();
            dataGridView2.DataSource = RefreshYearlySales();
            LbTotal.Text = $" الاجمالي =                  {SumTotalPrice(RefreshYearlySales()).ToString()}";

        }



        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            int MultiInvoiceId = int.Parse(dataGridView2.CurrentRow.Cells["الفاتوره المدمجه"].Value.ToString());
            int Single = int.Parse(dataGridView2.CurrentRow.Cells["الرقم"].Value.ToString());
            int SingleInvoice = SingleSalescs.FindInvoiceIDBYMultiId(MultiInvoiceId);
            decimal Taxes = SingleSalescs.GetTaxes(Single);

            if (SingleSalescs.DeleteInvoice(Single)) 
            {


                if (MultiInvoiceId != 0 && !SingleSalescs.IsThereAreMore2SingleINvoicesONThisMulti(MultiInvoiceId)&&Taxes!=0)
                {
                    SingleSalescs.SetTaxesForTheLastRowByMulti_Id(MultiInvoiceId, Taxes);
                    SingleSalescs.CancelRelationBetweenMultiInvoiceAndSingle(MultiInvoiceId);
                    Tables.DeleteOpenTableRecordByInvoiceId(MultiInvoiceId);
                    int OpenTableId = Tables.GetOPenTableIdByInvoiceId(MultiInvoiceId);
                    Tables.UpdateInvoiceIdWithNewMultInvoice(OpenTableId, SingleInvoice);
                    Tables.SetTaxesAlLastInvoiceWithMulti(SingleInvoice);
                    Tables.UpdateInvoiceIdWithNewMultInvoiceByMultiInvoiceId(MultiInvoiceId);
                    byte TableNumber = Tables.FindTableIDBYMultiId(SingleInvoice);
                    if (ClsSales.TableAndTheMultiBill.TryGetValue(TableNumber, out int Value))
                    {
                        ClsSales.TableAndTheMultiBill[TableNumber] = SingleInvoice;
                    }


                }
                else
                {
                    int OpenTableId = Tables.GetOPenTableIdByInvoiceId(Single);
                    ClsSales.TableAndTheMultiBill.Remove(TableNumber);
                    Tables.DeleteOpenTableRecordByInvoiceId(Single);

                }
                ClsSettings.ShowSuccessMessageBoxForRecover();
                try
                {
                    dataGridView2.DataSource = RefreshAllSingleSales();
                }
                catch (Exception)
                {
                    MessageBox.Show("لا توجد بيانات لعرضها.");
                }


            }

        }

        private void استرجاعجزءToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string input = Interaction.InputBox("كم عنصر تريد ان تسترجع", "استرجاع جزء من الفاتورة", "", -1, -1);
            float Price = ClsDrinks.GetPriceByName(dataGridView2.CurrentRow.Cells["الاسم"].Value.ToString());
            if (int.TryParse(input, out int UserInput) && Convert.ToInt32(input) < Convert.ToUInt32(dataGridView2.CurrentRow.Cells["الكميه"].Value) && SingleSalescs.UpdateInvoice(Convert.ToInt32(dataGridView2.CurrentRow.Cells["الرقم"].Value), Convert.ToInt32(input), Price))
            {
                ClsSettings.ShowSuccessMessageBoxForRecover();
                dataGridView2.DataSource = RefreshAllSingleSales();
            }


            else
            {
                ClsSettings.ShowMessagboxForUnCompeleteDetails();
            }

        }
        int selectedRowIndex = -1;
        private void dataGridView2_CellMouseLeave(object sender, DataGridViewCellEventArgs e)

        {
            if (dataGridView2.RowCount > 0)
            {
                if (selectedRowIndex != -1 && selectedRowIndex < dataGridView2.RowCount)
                {
                    dataGridView2.Rows[selectedRowIndex].Selected = false;
                }

                // تحديد الصف الجديد
                if (e.RowIndex >= 0 && e.RowIndex < dataGridView2.RowCount)
                {
                    dataGridView2.CurrentCell = dataGridView2.Rows[e.RowIndex].Cells[0]; // اختر الخلية الأولى في الصف
                    dataGridView2.Rows[e.RowIndex].Selected = true;
                    selectedRowIndex = e.RowIndex;
                }
            }
        }



        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            int invoiceId = Convert.ToInt32(dataGridView2.CurrentRow.Cells["الرقم"].Value);
            try
            {
                if (SingleSalescs.DeleteInvoicesWhereInvoiceId(invoiceId))
                {
                    if (MultiInvoice.DeleteInvoice(invoiceId))
                    {
                        ClsSettings.ShowSuccessMessageBoxForRecover();
                        dataGridView2.DataSource = RefreshAllMultiSales();
                    }
                }
            }
            catch (Exception)
            {
                if (MultiInvoice.DeleteInvoice(invoiceId))
                {
                    ClsSettings.ShowMessagboxForFalireOperations();
                    dataGridView2.DataSource = RefreshAllMultiSales();

                }
            }

        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            FrmInnerInvoices Invoices = new FrmInnerInvoices((Convert.ToInt32(dataGridView2.CurrentRow.Cells["الرقم"].Value)));
            Invoices.ShowDialog();
            dataGridView2.DataSource = RefreshAllMultiSales();
        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {

        }

        private void TxSearchForSingle_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(TxSearchForSingle.Text))
                dataGridView2.DataSource = SingleSalescs.FindToSearch(int.Parse(TxSearchForSingle.Text));
            else
                dataGridView2.DataSource = RefreshAllSingleSales();
        }

        private void TxSearchForMulti_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(TxSearchForMulti.Text))
                dataGridView2.DataSource = MultiInvoice.FindById(int.Parse(TxSearchForMulti.Text));
            else
                dataGridView2.DataSource = RefreshAllMultiSales();
        }



        public void AddTheOpenTableInvoicesToDataBaseAsync()
        {
            foreach (KeyValuePair<byte, int> KVP in ClsSales.TableAndTheMultiBill)
            {
                Tables.AddTheDictionryForTablesAndTheirInvoiccs(KVP.Key, KVP.Value);
            }
        }
        public void GetTheOpenTableInvoicesToDataBaseAsync()
        {
            foreach (KeyValuePair<byte, int> KVP in ClsSales.TableAndTheMultiBill)
            {
                Tables.AddTheDictionryForTablesAndTheirInvoiccs(KVP.Key, KVP.Value);
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (ClsSales.TableAndTheMultiBill.Count > 0)
            {
                //    ClsSettings.SendEmail("تنبيه", "لقد اغلقت النظام وهناك فواتير علي طاولات يرجي فتح النظام ومتابعه العمل", ClsUser.GetUserName());
                AddTheOpenTableInvoicesToDataBaseAsync();
            }


            //    ClsSettings.SendEmailWithTodaySalesAsync(RefreshTodaySales(), " تقرير مبيعات اليوم");

        }

        private void طباعهToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int MultiInvoiceId = Convert.ToInt32(dataGridView2.CurrentRow.Cells["الرقم"].Value);
            DataTable dt = SingleSalescs.GetInvoicesWhereInvoiceId(MultiInvoiceId);
            dt.Columns.Remove("التاريخ");
            dt.Columns.Remove("الفاتوره المدمجه");
            dt.Columns.Remove("الرقم");
            FrmInvoice frmInvoice = new FrmInvoice(MultiInvoiceId, dt);
            frmInvoice.ShowDialog();
        }

        private void طباعهToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            int MultiInvoiceId = Convert.ToInt32(dataGridView2.CurrentRow.Cells["الرقم"].Value);
            FrmInvoice frmInvoice = new FrmInvoice(MultiInvoiceId, SingleSalescs.FindById(MultiInvoiceId));
            frmInvoice.ShowDialog();
        }



        private void EditInvoiceDetails_Click(object sender, EventArgs e)
        {
            FrmCafeDetails GetCafeDetails = new FrmCafeDetails();
            GetCafeDetails.ShowDialog();
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {
            FrmSettings Setting = new FrmSettings();
            Setting.ShowDialog();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            deleteAllFoodAndDrinks();
            RefreshDrinksForAddNew(0, IsDelivery);
        }

        private void btnDrinks_Click(object sender, EventArgs e)
        {
            HideAllComtrolsBelongsToTableForm();
            switch (CurrentMode)
            {
                case Mode.AddNew:
                    {
                        ClsSales.counter = 0;


                        RefreshDrinksForAddNew(0, IsDelivery);

                    }
                    break;
                case Mode.AddOnCurrent:
                    {
                        ClsSales.counter = 1;

                        RefreshDrinksForAddonCurrentInvoice(MultiInvoiceIdIfModeAddOnCurent, TableNumberIfModeAddOnCurrent);
                    }
                    break;
            }


        }

        private void BtnFoods_Click(object sender, EventArgs e)
        {
            HideAllComtrolsBelongsToTableForm();
            switch (CurrentMode)
            {
                case Mode.AddNew:
                    {
                        ClsSales.counter = 0;
                        RefreshFoods(TableNumber, IsDelivery);

                    }
                    break;
                case Mode.AddOnCurrent:
                    {
                        ClsSales.counter = 1;
                        RefreshFoodsForAddonCurrentInvoice(GetInvoiceNumber(TableNumber), TableNumber);
                    }
                    break;
            }

        }

        private void AddFood_Click(object sender, EventArgs e)
        {
            FrmAddEditDrink AddNew = new FrmAddEditDrink(-1, FrmAddEditDrink.FoodOrDrink.Food);
            AddNew.ShowDialog();
            RefreshFoods(TableNumber, IsDelivery);
        }

        private void اغلاقالفاتورهToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem menuItem = sender as ToolStripMenuItem;
            if (menuItem != null)
            {
                ContextMenuStrip owner = menuItem.Owner as ContextMenuStrip;
                if (owner != null)
                {
                    Button btn = owner.SourceControl as Button;

                    if (ClsSales.TableAndTheMultiBill.TryGetValue(Convert.ToByte(btn.Text), out int value))
                    {
                        int InvoiceId = ClsSales.TableAndTheMultiBill[Convert.ToByte(btn.Text)];

                        DataTable Allinvoices = Tables.GetAllInvoicesOnTable(Convert.ToByte(btn.Text));
                        if (!Tables.IsMultiInvoice(InvoiceId))
                        {
                            Allinvoices = SingleSalescs.FindById(InvoiceId);

                        }
                        Tables.DeleteOpenTableRecord(Convert.ToByte(btn.Text));
                        ClsSales.TableAndTheMultiBill.Remove(Convert.ToByte(btn.Text));

                        FrmInvoice ShowInvoice = new FrmInvoice(InvoiceId, Allinvoices);

                        ShowInvoice.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("لا وجود لفواتير علي هذه الطاوله", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }
                    DeleteAllTables();
                    CreateAllTables(Tables.GetAllTables());

                }
            }
        }

        private void استرجاعمنالفاتورهToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem menuItem = sender as ToolStripMenuItem;
            ContextMenuStrip owner = menuItem.Owner as ContextMenuStrip;
            Button btn = owner.SourceControl as Button;
            byte TableId = byte.Parse(btn.Text);


            int InvoiceId = ClsSales.TableAndTheMultiBill[TableId];

            if (Tables.IsMultiInvoice(InvoiceId))
            {

                FrmInnerInvoices Return = new FrmInnerInvoices(InvoiceId, TableId);
                Return.ShowDialog();
            }
            else
            {
                MessageBox.Show($"قم بالدخول الي الفواتير الفرديه  ستجد الفاتوره وقم باسترجاع ما تشاء \nرقم الفاتوره هو {InvoiceId}");

            }
        }
        public bool IsDelivery = false;

        private void label1_Click(object sender, EventArgs e)
        {
            LbCurrentTable.Visible = true;
            LbCurrentTable.Text = "فاتوره ديلفري";
            BtnFoods.Visible = true;
            GbPurchase.BackColor = Color.Transparent;
            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView2.RowHeadersVisible = false;
            EnableVisibleAllControlsThatBelongsToDrinkes();
            CtrlDrink.SellSomething += AddToPurchases;
            CtrlFood.SellSomething += AddToPurchases;
            CtrlDrink.OnBillDone += BtnShowDrinks_Click;
            CtrlFood.OnBillDone += BtnShowDrinks_Click;
            FlpFoodAndDrinks.AutoScroll = true;
            FlpTabels.AutoScroll = true;
            FlpTabels.Visible = false;
            LbDelibery.Visible = false;
            BtnInvoices.Visible = false;
            BtnShowTables.Visible = true;
            IsDelivery = true;
            RefreshDrinksForAddNew(Convert.ToByte(TableNumber), IsDelivery);
            btnDrinks.Visible = true;
            AddDrink.Visible = false;
            AddFood.Visible = false;
            AddTable.Visible = false;
            GbCategories.Visible = true;
            Refresh.Visible = true; ;
        }

        private void AddTable_Click(object sender, EventArgs e)
        {
            Tables.AddTable();
            FlpTabels.Controls.Clear();
            CreateAllTables(Tables.GetAllTables());
        }
        private byte GetTableNumber(object sender)
        {
            ToolStripMenuItem menuItem = sender as ToolStripMenuItem;
            if (menuItem != null)
            {
                ContextMenuStrip owner = menuItem.Owner as ContextMenuStrip;

                Button btn = owner.SourceControl as Button;

                return Convert.ToByte(btn.Text);
            }
            return 0;
        }
        public byte TableNumberIfModeAddOnCurrent;
        public int MultiInvoiceIdIfModeAddOnCurent = 0;
        private void GetAllPurcahsesOnTable(int tableId)
        {
            List<string> AllPurchases = Tables.GetAllPurchasesOnThisTable(tableId);
            foreach (string Purchase in AllPurchases)
            {
                this.AddToPurchases(Purchase, false);
            }
        }


        private int GetInvoiceNumber(byte TAbleNumber)
        {
            short OpenTableId = Tables.GetLastInvoiceFromOpenTable(TAbleNumber);
            Tables.UpdateTaxesAndTotalPriceInvoice(OpenTableId);
            int InvoiceId = Tables.GetLastInvoiceId(OpenTableId);
            if (Tables.IsThereAreMoreOneInvoiceWithSameNumberAtOpenTables(InvoiceId))
            {
                return InvoiceId;
            }
            else
            {
                return MultiInvoice.LastId() == 0 ? 2000 : MultiInvoice.LastId() + 1;
            }
        }

        private void أضفاليالفاتورهToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem menuItem = sender as ToolStripMenuItem;
            ContextMenuStrip owner = menuItem.Owner as ContextMenuStrip;
            Button btn = owner.SourceControl as Button;
            CurrentMode = Mode.AddOnCurrent;

            if (ClsSales.TableAndTheMultiBill.TryGetValue(Convert.ToByte(btn.Text), out int value))
            {
                GbCategories.Visible = true;
                LbCurrentTable.Visible = true;
                LbCurrentTable.Text = $"طاوله رقم {btn.Text}";
                ClsSales.counter = 1;
                //get table Number from the button
                byte tableNumber = GetTableNumber(sender);
                TableNumber = tableNumber;
                //add to purchases 
                GetAllPurcahsesOnTable(TableNumber);
                SetUpToShowPuchasess();
                RefreshDrinksForAddonCurrentInvoice(GetInvoiceNumber(tableNumber), tableNumber);
            }
            else
            {
                MessageBox.Show("لا وجود لفواتير علي هذه الطاوله", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void استرجاعكلالفاتورهToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem menuItem = sender as ToolStripMenuItem;
            ContextMenuStrip owner = menuItem.Owner as ContextMenuStrip;
            Button btn = owner.SourceControl as Button;
            byte TableId = byte.Parse(btn.Text);
            int InvoiceId = ClsSales.TableAndTheMultiBill[TableId];
            if (ClsSales.TableAndTheMultiBill.TryGetValue(TableId, out int value))
            {
                ClsSales.TableAndTheMultiBill.Remove(TableId);
                if (Tables.IsThereAreMoreOneInvoiceWithSameNumberAtOpenTables(InvoiceId))
                {
                    SingleSalescs.DeleteInvoicesWhereInvoiceId(InvoiceId);
                    MultiInvoice.DeleteInvoice(InvoiceId);
                    Tables.DeleteOpenTableRecord(TableId);
                    MessageBox.Show("تم بنجاح", "عمليه ناجحه", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    SingleSalescs.DeleteInvoice(InvoiceId);
                    Tables.DeleteOpenTableRecord(TableId);
                    MessageBox.Show("تم بنجاح", "عمليه ناجحه", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                CreateAllTables(Tables.GetAllTables());

            }
            else
            {
                MessageBox.Show("لا وجود لفواتير علي هذه الطاوله ", "عمليه فاشله ", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }

        private async void DailyGmail_Click(object sender, EventArgs e)
        {
            DailyGmail.Enabled = false;

            try
            {
                // تشغيل عملية الإرسال بشكل غير متزامن
                await Task.Run(() => ClsSettings.SendEmailWithTodaySalesAsync(SingleSalescs.GetDailySales(), "تقرير مبيعات اليوم"));

                // رسالة تأكيد بعد الإرسال
                MessageBox.Show("تم إرسال التقرير بنجاح!");
            }
            catch (Exception ex)
            {
                // عرض رسالة في حالة حدوث خطأ أثناء الإرسال
                MessageBox.Show("حدث خطأ أثناء إرسال التقرير: " + ex.Message);
            }
            finally
            {
                // إعادة تمكين الزر بعد انتهاء العملية
                DailyGmail.Enabled = true;

            }
        }

        private async void MonthlyGmail_Click(object sender, EventArgs e)
        {

            MonthlyGmail.Enabled = false;

            try
            {
                await Task.Run(() => ClsSettings.SendEmailWithTodaySalesAsync(SingleSalescs.GetMonthlySales(), "تقرير مبيعات الشهر"));

                MessageBox.Show("تم إرسال التقرير بنجاح!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("حدث خطأ أثناء إرسال التقرير: " + ex.Message);
            }
            finally
            {
                MonthlyGmail.Enabled = true;
            }
        }

        private async void YearlyGmail_Click(object sender, EventArgs e)
        {


            YearlyGmail.Enabled = false;

            try
            {
                // تشغيل عملية الإرسال بشكل غير متزامن
                await Task.Run(() => ClsSettings.SendEmailWithTodaySalesAsync(SingleSalescs.GetDailySales(), "تقرير مبيعات اليوم"));

                // رسالة تأكيد بعد الإرسال
                MessageBox.Show("تم إرسال التقرير بنجاح!");
            }
            catch (Exception ex)
            {
                // عرض رسالة في حالة حدوث خطأ أثناء الإرسال
                MessageBox.Show("حدث خطأ أثناء إرسال التقرير: " + ex.Message);
            }
            finally
            {
                // إعادة تمكين الزر بعد انتهاء العملية
                YearlyGmail.Enabled = true;

            }
        }

        private void DailsSailsExel_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                // تعيين فلتر لتحديد نوع الملفات
                saveFileDialog.Filter = "Excel Files (*.xlsx)|*.xlsx|All Files (*.*)|*.*";
                saveFileDialog.Title = "Save Excel File As";
                saveFileDialog.FileName = $" {DateTime.Today} مبيعات اليوم .xlsx"; // اسم الملف الافتراضي

                // إظهار مربع الحوار
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {

                    string filePath = saveFileDialog.FileName;


                    ClsSettings.SendToExcelSheet(SingleSalescs.GetDailySales(), filePath, "تقرير المبيعات اليوميه");
                    MessageBox.Show("تم ارسال تقرير مبيعات اليوم");
                }
            }
        }

        private void MonthlyExcel_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                // تعيين فلتر لتحديد نوع الملفات
                saveFileDialog.Filter = "Excel Files (*.xlsx)|*.xlsx|All Files (*.*)|*.*";
                saveFileDialog.Title = "Save Excel File As";
                saveFileDialog.FileName = $"{DateTime.Today} مبيعات الشهر.xlsx";

                // إظهار مربع الحوار
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // الحصول على مسار الملف المحدد
                    string filePath = saveFileDialog.FileName;

                    // إرسال البيانات إلى Excel
                    ClsSettings.SendToExcelSheet(SingleSalescs.GetDailySales(), filePath, "تقرير المبيعات الشهر");
                    MessageBox.Show("تم ارسال تقرير مبيعات الشهر");
                }
            }
        }

        private void YearlyExcel_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                // تعيين فلتر لتحديد نوع الملفات
                saveFileDialog.Filter = "Excel Files (*.xlsx)|*.xlsx|All Files (*.*)|*.*";
                saveFileDialog.Title = "Save Excel File As";
                saveFileDialog.FileName = $"{DateTime.Today} مبيعات العام.xlsx";

                // إظهار مربع الحوار
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // الحصول على مسار الملف المحدد
                    string filePath = saveFileDialog.FileName;

                    // إرسال البيانات إلى Excel
                    ClsSettings.SendToExcelSheet(SingleSalescs.GetDailySales(), filePath, "تقرير المبيعات العام");
                    MessageBox.Show("تم ارسال تقرير مبيعات العام");
                }
            }
        }
        private void OpenLink(string url)
        {
            try
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = url,
                    UseShellExecute = true // يستخدم المتصفح الافتراضي
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }


        private void نقلالفاتورهToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem menuItem = sender as ToolStripMenuItem;
            ContextMenuStrip owner = menuItem.Owner as ContextMenuStrip;
            Button btn = owner.SourceControl as Button;
            byte OldTableNumber = byte.Parse(btn.Text);
            byte NewTableNumber =byte.Parse(Interaction.InputBox("ادخل رقم الطاوله المراد نقل الفاتوره عليها"));
            if(Tables.IsThisTableIsAlreadyExist(NewTableNumber) )
            {
                if(!Tables.IsThereAreOrdersOnThisTable(NewTableNumber))
                {
                    int Invoicenumber = ClsSales.TableAndTheMultiBill[OldTableNumber];
                    ClsSales.TableAndTheMultiBill.Remove(OldTableNumber);
                    ClsSales.TableAndTheMultiBill.Add(NewTableNumber, Invoicenumber);
                    Tables.UpdateTableNumberWithNew(OldTableNumber, NewTableNumber);
                    MessageBox.Show("تم النقل بنجاح", "تمت العمليه", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DeleteAllTables();
                    CreateAllTables(Tables.GetAllTables());

                }
                else
                {
                    MessageBox.Show(" خطأ يبدو ان هذه الطاوله يوجد عليها طلبات", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            else
            {
                MessageBox.Show("خطأ يبدو انك تحاول نقل الفاتوره الي طاوله  غير موجوده حاول مره اخري  ", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void عرضToolStripMenuItem_Click(object sender, EventArgs e)
        {
            short num = 0;
            FrmInnerInvoices Invoices = new FrmInnerInvoices((Convert.ToInt32(dataGridView2.CurrentRow.Cells["الرقم"].Value)),num);
            Invoices.ShowDialog();
        }
    }
}


