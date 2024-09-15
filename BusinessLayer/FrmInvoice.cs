using CadeDateACcess;
using CafeDateAccess;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cafe
{
    public partial class FrmInvoice : Form
    {
        private Label TotalLabel;

        //this for printing from the Main
        public FrmInvoice(int InvoceNumber, EnMode mode = EnMode.NormalMode)
        {

            InitializeComponent();
            this.mode = mode;
            this.invoiceNumber.Text = InvoceNumber.ToString();
            TotalLabel = new Label();
            TotalLabel.AutoSize = true;
            this.Controls.Add(TotalLabel);
            UpdateLabelLocation();
            UpdateTotalLabelWithListType();
        }
        public enum EnMode
        {
            NormalMode, DeliveryMode, NullMode
        }
        public EnMode mode;

        private void DeleteTaxesFromLastRow(DataTable dataTable)
        {
            DataRow lastRow = dataTable.Rows[dataTable.Rows.Count - 1];
            lastRow["الاجمالي"] = decimal.Parse(lastRow["الاجمالي"].ToString()) - ClsCafeDetails.GetTaxes(); ;
        }
        private void DeleteTaxesFromLastRow(List<ClsSales.SalesDetails> salesDetails)
        {
            // التحقق من أن القائمة تحتوي على عناصر
            if (salesDetails.Count > 0)
            {
                // الوصول إلى آخر عنصر في القائمة
                var lastDetail = salesDetails[salesDetails.Count - 1];

                // تعديل قيمة خاصية "السعر" بعد خصم الضرائب
                float currentPrice = lastDetail.الاجمالي;
                float taxes = (float)ClsCafeDetails.GetTaxes();
                lastDetail.الاجمالي = currentPrice - taxes;
            }
        }

        public FrmInvoice(int InvoiceNumber, DataTable Invoices)
        {
            //print from CmsMulti  At Main
            InitializeComponent();
            this.invoiceNumber.Text = InvoiceNumber.ToString();
            TotalLabel = new Label();
            TotalLabel.AutoSize = true;
            this.Controls.Add(TotalLabel);

            DeleteTaxesFromLastRow(Invoices);
            UpdateLabelLocation();
            UpdateTotalLabelWithDataTableType(Invoices);
            DgvForInvoice.DataSource = Invoices;
        }
        ~FrmInvoice()
        {
            ClsSales.TotalSales.Clear();
        }
        private void UpdateTotalLabelWithDataTableType(DataTable dt)
        {
            // تأكد من وجود عمود 'الاجمالي' في DataTable
            if (dt.Columns.Contains("الاجمالي"))
            {
                try
                {
                    // حساب الإجمالي باستخدام عمود 'الاجمالي' من DataTable
                    float totalSum = 0;
                    foreach (DataRow row in dt.Rows)
                    {
                        object value = row["الاجمالي"];
                        if (value != DBNull.Value)
                        {
                            totalSum += float.Parse(value.ToString());
                        }
                    }
                    decimal taxes = ClsCafeDetails.GetTaxes();
                    TotalLabel.Text = $"الخدمه = {taxes}";
                    TotalLabel.Text += $"\nالإجمالي = {(totalSum + float.Parse(taxes.ToString()))} ";

                }
                catch (Exception ex)
                {
                    // التعامل مع الأخطاء المحتملة في التحويل
                    TotalLabel.Text = $"خطأ في الحساب: {ex.Message}";
                }
            }
            else
            {
                TotalLabel.Text = "العمود 'الاجمالي' غير موجود.";
            }
        }
        private void UpdateTotalLabelWithListType()
        {
            float totalSum = ClsSales.TotalSales.Sum(item => item.الاجمالي);
            TotalLabel.Text = $"الإجمالي = {(totalSum + (mode == EnMode.DeliveryMode ? 0 : float.Parse(ClsCafeDetails.GetTaxes().ToString())))} ";
        }
        private void UpdateLabelLocation()
        {
            TotalLabel.Location = new Point(DgvForInvoice.Location.X, DgvForInvoice.Bottom + 5);
        }
        private void SetUpDataGrid()
        {
            DgvForInvoice.RowHeadersVisible = false;
            DgvForInvoice.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            DgvForInvoice.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            DgvForInvoice.AllowUserToAddRows = false;
            DgvForInvoice.RightToLeft = RightToLeft.Yes;


            if (mode == EnMode.DeliveryMode)

            {

                DgvForInvoice.DataSource = ClsSales.TotalSales;
            }

        }
        private void FrmInvoice_Load(object sender, EventArgs e)
        {
            SetUpDataGrid();
        }
        private PrintDocument printDocument = new PrintDocument();

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            PrintDocument printDocument = new PrintDocument();

            switch (mode)
            {
                case EnMode.NormalMode:
                    {
                        printDocument.PrintPage += new PrintPageEventHandler(PrintDocument_PrintPage);

                    }
                    break;
                case EnMode.DeliveryMode:
                    {
                        printDocument.PrintPage += new PrintPageEventHandler(PrintDocument_PrintPageForDelivery);

                    }
                    break;
            }


            // إنشاء مربع حوار الطباعة
            PrintDialog printDialog = new PrintDialog
            {
                Document = printDocument
            };

            // عرض مربع حوار الطباعة
            if (printDialog.ShowDialog() == DialogResult.OK)
            {
                printDocument.Print();
            }
        }
        private float GetTotalPrice()
        {
            float totalSum = 0;

            // التحقق من أن هناك بيانات في DataGridView
            if (DgvForInvoice.Rows.Count > 0)
            {
                foreach (DataGridViewRow row in DgvForInvoice.Rows)
                {
                    if (row.Cells["الاجمالي"].Value != null)
                    {
                        // جمع القيم في عمود "الاجمالي"
                        totalSum += Convert.ToSingle(row.Cells["الاجمالي"].Value);
                    }
                }
            }
            return totalSum;
        }
    
        private void PrintDocument_PrintPageForDelivery(object sender, PrintPageEventArgs e)
        {
            Graphics g = e.Graphics;
            Rectangle printArea = e.PageBounds;
            DgvForInvoice.Columns.Remove("التاريخ");

            // إعداد النصوص
            string invoiceNumberText = $"رقم الفاتورة: {invoiceNumber.Text} ";
            string CafeName = "MIX";
            string dateText = $"التاريخ: {DateTime.Now.ToString("dd/MM/yyyy")}";
            string timeText = $"الوقت: {DateTime.Now.ToString("HH:mm:ss")}";
            string CafePhoneNumebr = $"رقم الهاتف : {ClsCafeDetails.GetPhone()}";
            string CafeAddress = $"العنوان : {ClsCafeDetails.GetAddress()}";
            string Taxes = $"الخدمه  =  0";

            // الحقول الجديدة
            string Clientphone = $"رقم العميل : {ClsSales.CurrentDeliveryDetails.ClientPhone}";
            string ClientAddress = $"عنوان العميل : {ClsSales.CurrentDeliveryDetails.ClientAddress}";
            string DeliveryGuy = $"السائق : {ClsSales.CurrentDeliveryDetails.DelviryGuyName}";

            // الجمع بين النصوص
            string dateTimeText = $"{dateText}    {timeText}";
            string totalText = $"الإجمالي = {(ClsSales.TotalSales.Sum(item => item.الاجمالي) + 0)}";

            // إعداد خطوط الطباعة
            Font titleFont = new Font("Arial", 9, FontStyle.Bold);
            Font contentFont = new Font("Arial", 9);
            int margin = 20; // هامش للرقم والإجمالي

            // رسم رقم الفاتورة في الأعلى
            g.DrawString(invoiceNumberText, titleFont, Brushes.Black, printArea.Left + 60 + margin, printArea.Top + margin);

            // رسم اسم الكافيه تحت الفاتورة
            SizeF cafeNameSize = g.MeasureString(CafeName, titleFont);
            Rectangle cafeNameRect = new Rectangle(
                (int)(printArea.Left + 30 + margin),
                (int)(printArea.Top + margin + titleFont.Height + 10),
                (int)cafeNameSize.Width + 150,  // العرض بناءً على النص
                (int)cafeNameSize.Height + 5    // الارتفاع بناءً على النص
            );

            // رسم المستطيل
            g.DrawRectangle(Pens.Black, cafeNameRect);

            // رسم اسم الكافيه داخل المستطيل
            g.DrawString(CafeName, titleFont, Brushes.Black, cafeNameRect.Left + 70, cafeNameRect.Top + 2);

            // تابع رسم بقية العناصر مثل التاريخ والوقت
            g.DrawString(dateTimeText, contentFont, Brushes.Black, printArea.Left + margin, cafeNameRect.Bottom + 10);

            // رسم الحقول الجديدة تحت التاريخ والوقت
            float additionalInfoY = cafeNameRect.Bottom + 30;
            g.DrawString(Clientphone, contentFont, Brushes.Black, printArea.Left + margin+60, additionalInfoY);
            g.DrawString(ClientAddress, contentFont, Brushes.Black, printArea.Left + margin+40, additionalInfoY + 20);
            g.DrawString(DeliveryGuy, contentFont, Brushes.Black, printArea.Left + margin+30, additionalInfoY + 40);

            // إعداد ارتفاع DataGridView للطباعة
            int rowHeight = DgvForInvoice.RowTemplate.Height;
            int columnHeaderHeight = DgvForInvoice.ColumnHeadersHeight;
            int totalHeight = ((DgvForInvoice.RowCount * rowHeight) + columnHeaderHeight);

            // تعيين ارتفاع DataGridView ليتناسب مع المحتوى
            DgvForInvoice.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            DgvForInvoice.Height = totalHeight;

            // رسم DataGridView إلى صورة
            Bitmap bitmap = new Bitmap(DgvForInvoice.Width, DgvForInvoice.Height);
            DgvForInvoice.DrawToBitmap(bitmap, new Rectangle(0, 0, DgvForInvoice.Width, DgvForInvoice.Height));

            // رسم الصورة في وسط الصفحة
            g.DrawImage(bitmap, printArea.Left + margin, additionalInfoY + 60);

            // رسم الإجمالي في الأسفل
            float totalY = additionalInfoY + totalHeight + 100;
            g.DrawString(totalText, titleFont, Brushes.Black, printArea.Left + margin, totalY);

            g.DrawString(Taxes, titleFont, Brushes.Black, printArea.Left + margin + 20, totalY + 20);
            g.DrawString(CafePhoneNumebr, titleFont, Brushes.Black, printArea.Left + 95 + margin, printArea.Top + margin + totalY + 20);
            float Total = printArea.Top + margin + ((titleFont.Height) * 2) + contentFont.Height + totalHeight + 50 + margin + printArea.Top;
            g.DrawString(CafeAddress, titleFont, Brushes.Black, printArea.Left + 140 + margin, printArea.Top + margin + Total + 20);

            // تحديد إذا كانت هناك صفحات إضافية
            e.HasMorePages = false;
        }
        private void PrintDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            Graphics g = e.Graphics;
            Rectangle printArea = e.PageBounds;

            // إعداد النصوص
            string invoiceNumberText = $"رقم الفاتورة: {invoiceNumber.Text} ";
            string CafeName = "MIX";
            string dateText = $"التاريخ: {DateTime.Now.ToString("dd/MM/yyyy")}";
            string timeText = $"الوقت: {DateTime.Now.ToString("HH:mm:ss")}";
            string CafePhoneNumebr = $"رقم الهاتف : {ClsCafeDetails.GetPhone()}";
            string CafeAddress = $"العنوان : {ClsCafeDetails.GetAddress()}";
            string Taxes = $"الخدمه  =  {ClsCafeDetails.GetTaxes()}";

            // الجمع بين النصين
            string dateTimeText = $"{dateText}    {timeText}";
            string totalText = $"الإجمالي = {(GetTotalPrice() + float.Parse(ClsCafeDetails.GetTaxes().ToString())).ToString("N2")}";

            // إعداد خطوط الطباعة
            Font titleFont = new Font("Arial", 9, FontStyle.Bold);
            Font contentFont = new Font("Arial", 9);
            int margin = 20; // هامش للرقم والإجمالي

            // رسم رقم الفاتورة في الأعلى
            g.DrawString(invoiceNumberText, titleFont, Brushes.Black, printArea.Left + 60 + margin, printArea.Top + margin);

            //رسم اسم الكاقي تحت الفاتوره
            SizeF cafeNameSize = g.MeasureString(CafeName, titleFont);
            Rectangle cafeNameRect = new Rectangle(
                (int)(printArea.Left + 30 + margin),
                (int)(printArea.Top + margin + titleFont.Height + 10),
                (int)cafeNameSize.Width + 150,  // العرض بناءً على النص
                (int)cafeNameSize.Height + 5    // الارتفاع بناءً على النص
            );

            // رسم المستطيل
            g.DrawRectangle(Pens.Black, cafeNameRect);

            // رسم اسم الكافي داخل المستطيل
            g.DrawString(CafeName, titleFont, Brushes.Black, cafeNameRect.Left + 70, cafeNameRect.Top + 2);

            // تابع رسم بقية العناصر مثل التاريخ والوقت
            g.DrawString(dateTimeText, contentFont, Brushes.Black, printArea.Left + margin, cafeNameRect.Bottom + 10);
            // رسم التاريخ والوقت تحت رقم الفاتورة

            // إعداد ارتفاع DataGridView للطباعة
            int rowHeight = DgvForInvoice.RowTemplate.Height;
            int columnHeaderHeight = DgvForInvoice.ColumnHeadersHeight;
            int totalHeight = ((DgvForInvoice.RowCount * rowHeight) + columnHeaderHeight);

            // تعيين ارتفاع DataGridView ليتناسب مع المحتوى
            DgvForInvoice.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            DgvForInvoice.Height = totalHeight;

            // رسم DataGridView إلى صورة
            Bitmap bitmap = new Bitmap(DgvForInvoice.Width, DgvForInvoice.Height);
            DgvForInvoice.DrawToBitmap(bitmap, new Rectangle(0, 0, DgvForInvoice.Width, DgvForInvoice.Height));

            // رسم الصورة في وسط الصفحة
            g.DrawImage(bitmap, printArea.Left + margin, printArea.Top + margin + (titleFont.Height) * 2 + contentFont.Height + 30);

            // رسم الإجمالي في الأسفل
            float totalY = printArea.Top + margin + (titleFont.Height) * 2 + contentFont.Height + totalHeight + 50;
            g.DrawString(totalText, titleFont, Brushes.Black, printArea.Left + margin, totalY);

            g.DrawString(Taxes, titleFont, Brushes.Black, printArea.Left + margin + 20, totalY + 20);
            g.DrawString(CafePhoneNumebr, titleFont, Brushes.Black, printArea.Left + 95 + margin, printArea.Top + margin + totalY + 20);
            float Total = printArea.Top + margin + ((titleFont.Height) * 2) + contentFont.Height + totalHeight + 50 + margin + printArea.Top;
            g.DrawString(CafeAddress, titleFont, Brushes.Black, printArea.Left + 140 + margin, printArea.Top + margin + Total + 20);

            // تحديد إذا كانت هناك صفحات إضافية
            e.HasMorePages = false;
        }




    }

}
