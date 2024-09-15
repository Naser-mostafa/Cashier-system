using CadeDateACcess;
using Cafe.Properties;
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
using static Cafe.CtrlDrink;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Cafe
{
    public partial class CtrlFood : UserControl
    {
        public CtrlFood()
        {
            InitializeComponent();
        }
        public enum Mode
        {
            AddNew, AddOnCurrent
        }

        public Mode CurrentMode;


        int MultiInvoiceId;

        public delegate void BillDone(object sender, EventArgs e);
        public static event BillDone OnBillDone;
        public CtrlFood(int Multi_Invoice, short DrinkId, string Drinkname, float Price, string ImagePath, byte TableId)
        {
            InitializeComponent();
            CurrentMode = Mode.AddOnCurrent;
            this.Id = DrinkId;
            this.DrinkName = Drinkname;
            this.Price = Price;
            this.ImagePath = ImagePath;
            this.TableID = TableId;
            this.MultiInvoiceId = Multi_Invoice;

        }
        private void AddCurrentSingleInvoiceToOpenTables(int invoiceId)
        {
                Tables.AddNewOpenTable(this.TableId, this.Id, invoiceId, this.Quantity, this.Price * this.Quantity, float.Parse(ClsCafeDetails.GetTaxes().ToString()));

        }
        private void CtrlFood_Load(object sender, EventArgs e)
        {
            CbSize_TextChanged(sender, e);
            CbSize.DropDownStyle = ComboBoxStyle.DropDownList;

        }

        public delegate void OnSellSomething(string DrinkName, bool recet);

        public static event OnSellSomething SellSomething;
        public short Id = 0;
        public byte TableId = 0;

        public static bool IsDeliveryInvoice;
        public bool Delivery
        {
            get { return IsDeliveryInvoice; }
            set { IsDeliveryInvoice = value; }
        }

        public void SetImageAtPictureBox()
        {
            FoodPicture.BackgroundImage = null;
            if (string.IsNullOrEmpty(ImagePath))
            {
                FoodPicture.Image = Resources.images;

            }
            else
            {
                FoodPicture.ImageLocation = ImagePath;
                ImagePath = null;
            }


        }

        public delegate void IsSingleSales(bool IsIsSingle);

        public IsSingleSales SingleSalesOrNot;

        public void SetSizeLarge()
        {
            CbSize.SelectedIndex = 0;
        }
        public void ChangeButtonBAckColor(Color colorName)
        {
            this.BtnAddToInvoice.BackColor = colorName;
        }
        public string ImagePath { get; set; }
        public short DrinkID
        {
            get { return Id; }
            set { Id = Convert.ToInt16(value.ToString()); }
        }
        public byte TableID
        {
            get { return TableId; }
            set { TableId = Convert.ToByte(value.ToString()); }
        }
        public string DrinkName
        {
            get { return LbFoodName.Text; }
            set { LbFoodName.Text = value.ToString(); }
        }
        public short Quantity
        {
            set { NupQuantity.Value = decimal.Parse(value.ToString()); }
            get { return Convert.ToInt16(NupQuantity.Value); }
        }
        public float Price
        {
            set { LbFoodPrice.Text = value.ToString(); }
            get { return float.Parse(LbFoodPrice.Text); }
        }
        public bool IsSingle = true;
        private int AddToSingleSales(int Multi_InvoiceId = 0)
        {
            return SingleSalescs.AddInvoice(this.Id, this.Quantity, DateTime.Now, Multi_InvoiceId, this.Quantity * this.Price, IsDeliveryInvoice ? 0 : ClsCafeDetails.GetTaxes());
        }
        private void AddToDeliveryList()
        {
            ClsSales.SalesDetails SaleDrink = new ClsSales.SalesDetails(ClsDrinks.GetNameByID(this.Id), this.Price,this.Quantity, Convert.ToInt16(this.Quantity * this.Price), DateTime.Now);
            ClsSales.TotalSales.Add(SaleDrink);
        }
        private void AddCurrentMultiInvoice(int Multi)
        {
            if (!MultiInvoice.IsInvoiceExists(Multi))
                MultiInvoice.AddInvoice(Multi, DateTime.Now);

        }
        public static int NewMultiInvoiceIfTheBillWasSingle = 0;
        public static int CurrentMultiInvoiceId = 0;
        private void UpdateFeesOnOpenTablesAndSingleSales(byte tableId)
        {
            short OpenTableId = Tables.GetLastInvoiceFromOpenTable(tableId);
            Tables.UpdateTaxesAndTotalPriceInvoice(OpenTableId);
            // may be single and may be multi
            int InvoiceId = Tables.GetLastInvoiceId(OpenTableId);
            //must check Has MultiId or not to know its single or double
            if (GetLabelText() == 2)
            {
                 //means its Single
                SingleSalescs.UpdateInvoiceWithNewMultiInvoiceNumberBySingleInvoice(MultiInvoiceId, InvoiceId);
                SingleSalescs.DeleteTaxesFromLastInvoiceAtMultiInvoiceToPerformAdding(InvoiceId);
                //cause its single will create anew invoice id
                this.MultiInvoiceId = MultiInvoice.LastId()==0?2000: MultiInvoice.LastId() + 1;
                Tables.UpdateInvoiceIdWithNewMultInvoice(OpenTableId, this.MultiInvoiceId);
                ClsSales.TableAndTheMultiBill[tableId] = MultiInvoiceId;
                SingleSalescs.DeleteTaxesWhereMultiIdIsDESC(InvoiceId);

             }
            else
            {
                SingleSalescs.DeleteTaxesFromLastInvoicehByMultiInvoice(InvoiceId);
                Tables.UpdateInvoiceIdWithNewMultInvoice(OpenTableId, this.MultiInvoiceId); 
                                                                                            
            }

        }
        public int GetLabelText()
        {
            string purchases = ClsSales.purchasesBuilder.ToString();

            if (!string.IsNullOrEmpty(purchases))
            {
                // تقسيم النص إلى أسطر بناءً على فواصل الأسطر
                string[] lines = purchases.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);

                // إرجاع عدد الأسطر
                return lines.Length;
            }

            // إذا كان النص فارغاً، نعيد 0
            return 0;
        }
        private void ProcessForCreateTheINvoiceForAddOnCurrent()
        {

            //     
            {
                if (GetLabelText() == 1)
                {
                    int ID = MultiInvoice.LastId();
                    ClsSales.TableAndTheMultiBill[TableId] = ID;
                    AddCurrentMultiInvoice(ID);
                    IsSingle = true;
                    //i put two table for  invoices, one that have only one item and the other for many Items
                    ;

                    //add the last invoice into single sales
                    SingleSalescs.AddInvoice(this.Id, this.Quantity, DateTime.Now, ID, this.Quantity * this.Price, IsDeliveryInvoice ? 0 : decimal.Parse(ClsCafeDetails.GetTaxes().ToString()));
                    AddCurrentMultiInvoice(ID);
                    //add to to the open tables
                    AddCurrentSingleInvoiceToOpenTables(ID /*LastIdForTheInvoice == 0 ? 1 : LastIdForTheInvoice + 1*/);
                    //recet quantity at ui
                    NupQuantity.Value = 0;

                }
                else
                {
                    IsSingle = false;
                    //i put two table for  invoices, one that have only one item and the other for many Items
                    ;
                    ClsSales.TableAndTheMultiBill[TableId] = MultiInvoiceId;
                    AddCurrentMultiInvoice(MultiInvoiceId);
                    //add the last invoice into single sales
                    SingleSalescs.AddInvoice(this.Id, this.Quantity, DateTime.Now, MultiInvoiceId, this.Quantity * this.Price, IsDeliveryInvoice ? 0 : decimal.Parse(ClsCafeDetails.GetTaxes().ToString()));
                    //add to to the open tables
                    AddCurrentSingleInvoiceToOpenTables(MultiInvoiceId /*LastIdForTheInvoice == 0 ? 1 : LastIdForTheInvoice + 1*/);
                    //recet quantity at ui
                    NupQuantity.Value = 0;



                    ClsSales.TotalSales.Clear();

                }

            }

        }
        private void processForAddPurchaseOnCurrent()
        {

            IsSingle = false;
            //add the bill to the single sales
            SingleSalescs.AddInvoice(this.Id, this.Quantity, DateTime.Now, this.MultiInvoiceId, this.Quantity * this.Price, 0);
            //add to the open tables if nor delivery bill
            Tables.AddNewOpenTable(this.TableId, this.Id, this.MultiInvoiceId, this.Quantity, this.Price * this.Quantity, 0);
            NupQuantity.Value = 0;

        }
        private void processForAddPurchaseForAddNew()
        {

            IsSingle = false;
            SellSomething.Invoke($"{this.Quantity}-{this.DrinkName}-{this.CbSize.Text}", false);
            //when enter this func that mean its single
            int LastIdForTheInvoice = MultiInvoice.LastId();
            if (IsDeliveryInvoice)
            {
                //add to the list
                ClsSales.SalesDetails SaleDrink = new ClsSales.SalesDetails(ClsDrinks.GetNameByID(this.Id)+"-"+CbSize.Text,this.Price ,this.Quantity, Convert.ToInt16(this.Quantity * this.Price), DateTime.Now);
                ClsSales.TotalSales.Add(SaleDrink);
            }
            //add the bill to the single sales
            SingleSalescs.AddInvoice(this.Id, this.Quantity, DateTime.Now, LastIdForTheInvoice == 0 ? 2000 : LastIdForTheInvoice + 1, this.Quantity * this.Price, 0);
            //add to the open tables if nor delivery bill
            if (!IsDeliveryInvoice)
                Tables.AddNewOpenTable(this.TableId, this.Id, LastIdForTheInvoice == 0 ? 2000 : LastIdForTheInvoice + 1, this.Quantity, this.Price * this.Quantity, 0);


            NupQuantity.Value = 0;
            //code to Show Invoice


        }

        private void ProcessForCreateTheINvoiceForAddNew()
        {
            //check its single or multi bill
            int PurchasesLine = GetLabelText();
            if (PurchasesLine == 0)
            {
                SellSomething.Invoke($"{this.Quantity}-{this.DrinkName}-{this.CbSize.Text}", false);
                SellSomething.Invoke(null, true);
                IsSingle = true;
                SingleSalesOrNot(true);
                //add the invoice into list Sales 
                if (IsDeliveryInvoice)
                {
                    AddToDeliveryList();
                }
                //add the invoice into invoices table
                int InvoiceId = AddToSingleSales();
                if (!IsDeliveryInvoice)
                {
                    //add the invoice into OpenTables
                    AddCurrentSingleInvoiceToOpenTables(InvoiceId);
                    //add the invoiceId And The Table To The dictionry
                    ClsSales.TableAndTheMultiBill.Add(TableId, InvoiceId);
                }
                NupQuantity.Value = 0;
                if (IsDeliveryInvoice)
                {
                    //get MoreDetails And PrintTheInvoice
                    FrmGetDeliveryInvoiceDetails GetDeveryDetails = new FrmGetDeliveryInvoiceDetails(InvoiceId);
                    GetDeveryDetails.ShowDialog();
                }
            }
            else
            {
                IsSingle = false;
                //    SingleSalesOrNot(false);
                //i put two table for  invoices, one that have only one item and the other for many Items
                int LastIdForTheInvoice = MultiInvoice.LastId();
                if (IsDeliveryInvoice)
                {
                    //Get More Details About Invoice Number
                    ClsSales.SalesDetails SaleDrink = new ClsSales.SalesDetails(ClsDrinks.GetNameByID(this.Id) + "-" + CbSize.Text, this.Price,this.Quantity, Convert.ToInt16(this.Quantity * this.Price), DateTime.Now);
                    ClsSales.TotalSales.Add(SaleDrink);

                }
                //add the last invoice into single sales
                SingleSalescs.AddInvoice(this.Id, this.Quantity, DateTime.Now, LastIdForTheInvoice == 0 ? 2000 : LastIdForTheInvoice + 1, this.Quantity * this.Price, IsDeliveryInvoice ? 0 : decimal.Parse(ClsCafeDetails.GetTaxes().ToString()));
                //cannot be identity specification cause the single invoices depend on it and can be most one dependent on it
                //add to the open tables
                if (!IsDeliveryInvoice)
                {
                    Tables.AddNewOpenTable(this.TableId, this.Id, LastIdForTheInvoice == 0 ? 2000 : LastIdForTheInvoice + 1, this.Quantity, this.Price * this.Quantity, IsDeliveryInvoice ? 0 : float.Parse(ClsCafeDetails.GetTaxes().ToString()));
                    //add the invoiceId And The TableNumber To The dictionry to print it when need
                    ClsSales.TableAndTheMultiBill.Add(this.TableId, LastIdForTheInvoice == 0 ? 2000 : LastIdForTheInvoice + 1);

                }
                AddCurrentMultiInvoice(LastIdForTheInvoice == 0 ? 2000 : LastIdForTheInvoice + 1);
                //recet quantity at ui
                NupQuantity.Value = 0;

                //add to the multiinvoice
                if (IsDeliveryInvoice)
                {
                    FrmGetDeliveryInvoiceDetails GetDeveryDetails = new FrmGetDeliveryInvoiceDetails(LastIdForTheInvoice == 0 ? 2000 : LastIdForTheInvoice + 1);
                    GetDeveryDetails.ShowDialog();
                }


            }
            ClsSales.TotalSales.Clear();
            SellSomething.Invoke(null, true);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            switch (CurrentMode)
            {
                case Mode.AddOnCurrent:
                    {
                        SellSomething.Invoke($"{this.Quantity}-{this.DrinkName}-{this.CbSize.Text}", false);
                        if (ClsSales.counter == 1)
                        {
                            ClsSales.counter = 0;
                            UpdateFeesOnOpenTablesAndSingleSales(this.TableId);

                            //Mean itx double bill

                        }
                        DialogResult result = MessageBox.Show(
     $"نم اضافه {this.DrinkName} بنجاح هل تود اضافه شي اخر الي هذه الفاتوره",
     "سؤال",
     MessageBoxButtons.YesNo,
     MessageBoxIcon.Question
 );

                        if (result == DialogResult.Yes)
                        {
                            processForAddPurchaseOnCurrent();
                        }
                        else if (result == DialogResult.No)
                        {
                            ProcessForCreateTheINvoiceForAddOnCurrent();
                            OnBillDone.Invoke(this, e);
                        }
                    }
                    break;
                case Mode.AddNew:

                    {
                        DialogResult result = MessageBox.Show(
     $"نم اضافه {this.DrinkName} بنجاح هل تود اضافه شي اخر الي هذه الفاتوره",
     "سؤال",
     MessageBoxButtons.YesNo,
     MessageBoxIcon.Question
 );

                        if (result == DialogResult.Yes)
                        {
                            processForAddPurchaseForAddNew();

                        }
                        else if (result == DialogResult.No)
                        {
                            ProcessForCreateTheINvoiceForAddNew();
                            OnBillDone.Invoke(this, e);

                        }
                    }
                    break;
            }

        }


        private void CbSize_TextChanged(object sender, EventArgs e)
        {
            string selectedSize = CbSize.Text;
            float newPrice = 0;

            if (selectedSize == "كبير")
            {
                newPrice = ClsDrinks.GetFoodPriceByID(this.Id);  // افترض أن السعر الأساسي هو السعر الصغير
            }
            else if (selectedSize == "وسط")
            {
                newPrice = ClsDrinks.GetFoodPriceByID(this.Id + 2); // افترض أن هذا هو السعر للمتوسط
            }
            else if (selectedSize == "صغير")
            {
                newPrice = ClsDrinks.GetFoodPriceByID(this.Id + 1); // افترض أن هذا هو السعر للكبير
            }
            else
            {
                CbSize.SelectedIndex = 0;
                newPrice = ClsDrinks.GetFoodPriceByID(this.Id);  // افترض أن السعر الأساسي هو السعر الصغير
            }

            LbFoodPrice.Text = newPrice.ToString();
            this.Price = newPrice;
        }

        private void label4_Click(object sender, EventArgs e)
        {
            string Password = Interaction.InputBox("ادخل كلمه السر المربوطه بتغير السعر");
            if (Password == "تغير السعر")
            {
                FrmAddEditDrink Edit = new FrmAddEditDrink(this.Id, FrmAddEditDrink.FoodOrDrink.Food);
                Edit.ShowDialog();
            }
            else
            {

                MessageBox.Show("لقد ادهلت كلمه سر خاطئه وقمنا بمراسله المالك بمحاوله تغير اسعار المنتجات");
                ClsSettings.SendEmail("انت في خطر", "هناك من يحاول تغير الاسعار في البرنامج الخاص بك وقد ادخل كلمه السر  خطأ المربوطه بتغير الاسعار ,يبدو ان هناك من يحاول الاحتيال علي العملاء وان يبيع بسعر اكبر من المتوقع", ClsUser.GetUserName());
            }

        
        }

        private void EditPicture_Click(object sender, EventArgs e)
        {
            string Password = Interaction.InputBox("ادخل كلمه السر المربطه بتغير السعر");
            if (Password == "تغير السعر")
            {
                FrmAddEditDrink Edit = new FrmAddEditDrink(this.Id, FrmAddEditDrink.FoodOrDrink.Food);
                Edit.ShowDialog();
            }
            else
            {
                MessageBox.Show("لقد ادهلت كلمه سر خاطئه وقمنا بمراسله المالك بمحاوله تغير اسعار المنتجات");
                ClsSettings.SendEmail("انت في خطر", "هناك من يحاول تغير الاسعار في البرنامج الخاص بك وقد ادخل كلمه السر  خطأ المربوطه بتغير الاسعار ,يبدو ان هناك من يحاول الاحتيال علي العملاء وان يبيع بسعر اكبر من المتوقع", ClsUser.GetUserName());

            }
        }
            private void NupQuantity_ValueChanged(object sender, EventArgs e)
        {
            if (NupQuantity.Value > 0)
            {
                BtnAddToInvoice.Enabled = true;
                return;
            }
            BtnAddToInvoice.Enabled = false;
        }
    }
    
    }

