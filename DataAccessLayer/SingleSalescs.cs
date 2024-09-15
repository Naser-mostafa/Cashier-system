using CadeDateACcess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CafeDateAccess
{
    public class SingleSalescs
    {
        public static int AddInvoice(int DrinkID,int Quantity, DateTime dt,int MultiINvoiceId,float TotalPrice,decimal Taxes)
        {
            int invoiceId = 0;
            TotalPrice +=float.Parse(Taxes.ToString());
            // استعلام لإدراج الفاتورة
            string Query = @"
        INSERT INTO [SingleInvoice]
        ([DrinkId], [SoldQuantity], [dateAndTime], [Multi_InvoiceiD], [Taxes], [TotalPrice])
        VALUES
        (@DrinkID, @Quantity, @dt, @MultiINvoiceId, @Taxes, @TotalPrice);

        SELECT last_insert_rowid();
    "; using ( SQLiteConnection connection = new SQLiteConnection(ClsSettings.ConnectionString))
            {
                using (SQLiteCommand insertInvoiceCommand = new SQLiteCommand(Query, connection))
                {
                    insertInvoiceCommand.Parameters.AddWithValue("@Taxes", Taxes);
                    insertInvoiceCommand.Parameters.AddWithValue("@dt", dt);
                    insertInvoiceCommand.Parameters.AddWithValue("@DrinkID", DrinkID);
                    insertInvoiceCommand.Parameters.AddWithValue("@MultiINvoiceId", MultiINvoiceId);
                    insertInvoiceCommand.Parameters.AddWithValue("@TotalPrice", TotalPrice);
                    insertInvoiceCommand.Parameters.AddWithValue("@Quantity", Quantity);

                    try
                    {
                        connection.Open();

                        // إدراج الفاتورة واسترجاع الـ ID
                        invoiceId = Convert.ToInt32(insertInvoiceCommand.ExecuteScalar());
                                            }
                    catch (Exception ex)
                    {
                        // التعامل مع الأخطاء
                        Console.WriteLine("An error occurred: " + ex.Message);
                    }
                }
            }

            return invoiceId;
        }
        public static decimal GetTaxes(int SingleInvoiceId)
        {
            decimal Taxes = -1;
            // استعلام لإدراج الفاتورة
            string Query = @"
   select Taxes From SingleInvoice where InvoiceId=@SingleInvoiceId

    "; using (SQLiteConnection connection = new SQLiteConnection(ClsSettings.ConnectionString))
            {
                using (SQLiteCommand insertInvoiceCommand = new SQLiteCommand(Query, connection))
                {
                    insertInvoiceCommand.Parameters.AddWithValue("@SingleInvoiceId", SingleInvoiceId);
              

                    try
                    {
                        connection.Open();

                        // إدراج الفاتورة واسترجاع الـ ID
                        Taxes = Convert.ToDecimal(insertInvoiceCommand.ExecuteScalar());
                    }
                    catch (Exception ex)
                    {
                        // التعامل مع الأخطاء
                        Console.WriteLine("An error occurred: " + ex.Message);
                    }
                }
            }

            return Taxes ;
        }

        public static decimal GetTaxesForBill(int BillId)
        {
            decimal invoiceId = 0;
            // استعلام لإدراج الفاتورة
            string Query = @"
        select Taxes from  SingleInvoice Where InvoiceId=@BillId;
    "; using (SQLiteConnection connection = new SQLiteConnection(ClsSettings.ConnectionString))
            {
                using (SQLiteCommand insertInvoiceCommand = new SQLiteCommand(Query, connection))
                {
                    insertInvoiceCommand.Parameters.AddWithValue("@BillId", BillId);
                   

                    try
                    {
                        connection.Open();

                        // إدراج الفاتورة واسترجاع الـ ID
                        invoiceId = Convert.ToInt32(insertInvoiceCommand.ExecuteScalar());
                    }
                    catch (Exception ex)
                    {
                        // التعامل مع الأخطاء
                        Console.WriteLine("An error occurred: " + ex.Message);
                    }
                }
            }

            return invoiceId;
        }
        public static DataTable GetAllInvoices(int pageNumber)
        {
            DataTable invoices = new DataTable();
            using (SQLiteConnection connection = new SQLiteConnection(ClsSettings.ConnectionString))
            {
                // حساب الفهرس الابتدائي للصفوف
                int offset = (pageNumber - 1) * 50;

                // تعديل الاستعلام ليشمل التصفح
                string query = $@"
            SELECT 
                SingleInvoice.InvoiceId AS 'الرقم',
                FoodsAndDrinks.Name AS 'الاسم',
                SingleInvoice.SoldQuantity AS 'الكميه',
                SingleInvoice.dateAndTime AS 'التاريخ والوقت',
                SingleInvoice.Taxes AS 'الخدمه',
                SingleInvoice.Multi_InvoiceiD AS 'الفاتوره المدمجه',

                SingleInvoice.TotalPrice AS 'الاجمالي'
            FROM 
                SingleInvoice
            INNER JOIN 
                FoodsAndDrinks 
            ON 
                FoodsAndDrinks.ID = SingleInvoice.DrinkId
            LIMIT 50 OFFSET {offset};
        ";

                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    try
                    {
                        connection.Open();
                        using (SQLiteDataReader reader = command.ExecuteReader())
                        {
                            // تحميل البيانات من القارئ إلى الـ DataTable
                            invoices.Load(reader);
                        }
                    }
                    catch (Exception ex)
                    {
                        ClsSettings.CreateTheErrorAtEventLog(ex.Message);
                    }
                }
            }
            return invoices;
        }
        public static DataTable GetDailySales()
        {
            DataTable invoices = new DataTable();
            using (SQLiteConnection connection = new SQLiteConnection(ClsSettings.ConnectionString))
            {

                string query = @"
SELECT 
    FoodsAndDrinks.ID AS 'الرقم',
    FoodsAndDrinks.Name AS 'اسم المنتج',
    FoodsAndDrinks.Price AS 'سعر المنتج',
    SUM(SingleInvoice.Taxes) AS 'الخدمه',
    SUM(SingleInvoice.TotalPrice) AS 'السعر الاجمالي',
    SUM(SingleInvoice.SoldQuantity) AS 'الكميه المباعه'
FROM 
    SingleInvoice
INNER JOIN 
    FoodsAndDrinks 
ON 
    SingleInvoice.DrinkId = FoodsAndDrinks.ID
WHERE 
    DATE(SingleInvoice.dateAndTime) = DATE('now') -- تأكد من تطابق تاريخ اليوم
GROUP BY 
    FoodsAndDrinks.ID, FoodsAndDrinks.Name;
";




                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    // استخدام معلمة للـ offset

                    try
                    {
                        connection.Open();
                        using (SQLiteDataReader reader = command.ExecuteReader())
                        {
                            // تحميل البيانات من القارئ إلى الـ DataTable
                            invoices.Load(reader);
                        }
                    }
                    catch (Exception ex)
                    {
                        ClsSettings.CreateTheErrorAtEventLog(ex.Message);
                    }
                }
            }
            return invoices;
        }
        public static DataTable GetMonthlySales()
        {
            DataTable invoices = new DataTable();
            using (SQLiteConnection connection = new SQLiteConnection(ClsSettings.ConnectionString))
            {

                string query = @"

SELECT
    FoodsAndDrinks.ID AS 'الرقم',
    FoodsAndDrinks.Name AS 'اسم المنتج',
    FoodsAndDrinks.Price AS 'سعر المنتج',
    SUM(SingleInvoice.Taxes) AS 'الخدمه',
SUM(SingleInvoice.TotalPrice) AS 'السعر الاجمالي',
    SUM(SingleInvoice.SoldQuantity) AS 'الكميه المباعه'
FROM
    SingleInvoice
INNER JOIN
    FoodsAndDrinks
ON
    SingleInvoice.DrinkId = FoodsAndDrinks.ID
WHERE
    strftime('%Y-%m', SingleInvoice.dateAndTime) = strftime('%Y-%m', 'now')-- تأكد من تطابق الشهر الحالي
GROUP BY
    FoodsAndDrinks.ID, FoodsAndDrinks.Name;
                ";





                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    // استخدام معلمة للـ offset

                    try
                    {
                        connection.Open();
                        using (SQLiteDataReader reader = command.ExecuteReader())
                        {
                            // تحميل البيانات من القارئ إلى الـ DataTable
                            invoices.Load(reader);
                        }
                    }
                    catch (Exception ex)
                    {
                        ClsSettings.CreateTheErrorAtEventLog(ex.Message);
                    }
                }
            }
            return invoices;
        }
        public static DataTable GetYearlySales()
        {
            DataTable invoices = new DataTable();
            using (SQLiteConnection connection = new SQLiteConnection(ClsSettings.ConnectionString))
            {

                string query = @"
SELECT 
    FoodsAndDrinks.ID AS 'الرقم',
    FoodsAndDrinks.Name AS 'اسم المنتج',
     FoodsAndDrinks.Price AS 'سعر المنتج',
    SUM(SingleInvoice.Taxes) AS 'الخدمه',
    SUM(SingleInvoice.TotalPrice) AS 'السعر الاجمالي',
    SUM(SingleInvoice.SoldQuantity) AS 'الكميه المباعه'
FROM 
    SingleInvoice
INNER JOIN 
    FoodsAndDrinks 
ON 
    SingleInvoice.DrinkId = FoodsAndDrinks.ID
WHERE 
    strftime('%Y', SingleInvoice.dateAndTime) = strftime('%Y', 'now')
GROUP BY 
    FoodsAndDrinks.ID, FoodsAndDrinks.Name
HAVING 
    SUM(SingleInvoice.TotalPrice) > 0
ORDER BY 
    FoodsAndDrinks.ID;
"
;


                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    // استخدام معلمة للـ offset

                    try
                    {
                        connection.Open();
                        using (SQLiteDataReader reader = command.ExecuteReader())
                        {
                            // تحميل البيانات من القارئ إلى الـ DataTable
                            invoices.Load(reader);
                        }
                    }
                    catch (Exception ex)
                    {
                        ClsSettings.CreateTheErrorAtEventLog(ex.Message);
                    }
                   
                }
            }
            return invoices;
        }
        public static bool DeleteInvoice(int invoiceId)
        {
            bool isDeleted = false;
            string query = "DELETE FROM SingleInvoice WHERE InvoiceId = @InvoiceId";

            using (SQLiteConnection connection = new SQLiteConnection(ClsSettings.ConnectionString))
            {
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    // إضافة معلمة InvoiceId
                    command.Parameters.AddWithValue("@InvoiceId", invoiceId);

                    try
                    {
                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();
                        isDeleted = rowsAffected > 0; // إذا تم حذف صف واحد أو أكثر، يعتبر الحذف ناجحًا
                    }
                    catch (Exception ex)
                    {
                        ClsSettings.CreateTheErrorAtEventLog(ex.Message);
                    }
                }
            }

            return isDeleted;
        }
        public static bool UpdateInvoice(int invoiceId, int soldQuantity, float price)
        {
            bool isUpdated = false;
            decimal Taxes = GetTaxesForBill(invoiceId);
            string query = @"
        UPDATE SingleInvoice 
        SET 
            SoldQuantity = SoldQuantity - @SoldQuantity,
            TotalPrice = (@Price * (SoldQuantity - @SoldQuantity)+@Taxes) 
            
        WHERE 
            InvoiceId = @InvoiceId;";

            using (SQLiteConnection connection = new SQLiteConnection(ClsSettings.ConnectionString))
            {
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    // إضافة معلمات الاستعلام
                    command.Parameters.AddWithValue("@Taxes", Taxes);

                    command.Parameters.AddWithValue("@SoldQuantity", soldQuantity);
                    command.Parameters.AddWithValue("@Price", price);
                    command.Parameters.AddWithValue("@InvoiceId", invoiceId);

                    try
                    {
                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();
                        isUpdated = rowsAffected > 0; // إذا تم تحديث صف واحد أو أكثر، يعتبر التحديث ناجحًا
                    }
                    catch (Exception ex)
                    {
                        ClsSettings.CreateTheErrorAtEventLog(ex.Message);
                    }
                }
            }

            return isUpdated;
        }
        public static bool DeleteInvoicesWhereInvoiceId(int MultiINvoiceId)
        {
            bool isDeleted = false;
            string query = "DELETE FROM SingleInvoice WHERE Multi_InvoiceiD = @MultiINvoiceId";

            using (SQLiteConnection connection = new SQLiteConnection(ClsSettings.ConnectionString))
            {
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    // إضافة معلمة InvoiceId
                    command.Parameters.AddWithValue("@MultiINvoiceId", MultiINvoiceId);

                    try
                    {
                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();
                        isDeleted = rowsAffected > 0; // إذا تم حذف صف واحد أو أكثر، يعتبر الحذف ناجحًا
                    }
                    catch (Exception ex)
                    {
                        ClsSettings.CreateTheErrorAtEventLog(ex.Message);
                    }
                }
            }

            return isDeleted;
        }
        public static DataTable GetInvoicesWhereInvoiceId(int MultiINvoiceId)
        {
            DataTable AllInvoices = new DataTable();
            string query = @"
        SELECT 
            SingleInvoice.InvoiceId as 'الرقم',
            FoodsAndDrinks.Name as 'اسم المنتج',
            SingleInvoice.SoldQuantity as 'الكميه',
            SingleInvoice.dateAndTime as 'التاريخ',
            SingleInvoice.Multi_InvoiceiD as 'الفاتوره المدمجه',
            SingleInvoice.TotalPrice as 'الاجمالي'
        FROM 
            SingleInvoice
        INNER JOIN 
            FoodsAndDrinks 
        ON 
            SingleInvoice.Multi_InvoiceiD = @MultiINvoiceId
where FoodsAndDrinks.ID=SingleInvoice.DrinkId
        GROUP BY 
            SingleInvoice.InvoiceId;";

            using (SQLiteConnection connection = new SQLiteConnection(ClsSettings.ConnectionString))
            {
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    // إضافة معلمة InvoiceId
                    command.Parameters.AddWithValue("@MultiINvoiceId", MultiINvoiceId);

                    try
                    {
                        connection.Open();
                        using (SQLiteDataReader reader = command.ExecuteReader())
                        {
                            AllInvoices.Load(reader);
                        }
                    }
                    catch (Exception ex)
                    {
                        ClsSettings.CreateTheErrorAtEventLog(ex.Message);
                    }
                }
            }
            return AllInvoices;
        }
        public static bool IsThereAreMore2SingleINvoicesONThisMulti(int MultiID)
        {
            bool IsFound = false;
            using (SQLiteConnection connection = new SQLiteConnection(ClsSettings.ConnectionString))
            {
                string Query = "SELECT COUNT(*) FROM SingleInvoice WHERE Multi_InvoiceiD = @MultiID;";
                using (SQLiteCommand command = new SQLiteCommand(Query, connection))
                {
                    command.Parameters.AddWithValue("@MultiID", MultiID);

                    try
                    {
                        connection.Open();
                        int rowCount = Convert.ToInt32(command.ExecuteScalar()); // جلب عدد الصفوف

                        if (rowCount > 1)
                        {
                            IsFound = true;
                        }
                    
                    }
                    catch (Exception ex)
                    {
                        ClsSettings.CreateTheErrorAtEventLog(ex.Message);
                    }
                }
            }
            return IsFound;
        }
        public static int CancelRelationBetweenMultiInvoiceAndSingle(int MultiinvoiceId)
        {
            byte NewmultiinvoiceID = 0;
            int invoiceId = -1;  // لتخزين معرف الفاتورة المُحدثة
            string updateQuery = @"
    UPDATE SingleInvoice 
    SET 
        Multi_InvoiceiD = @NewmultiinvoiceID
    WHERE 
        Multi_InvoiceiD = @MultiinvoiceId;";

            string selectQuery = @"
    SELECT InvoiceId 
    FROM SingleInvoice 
    WHERE Multi_InvoiceiD = @NewmultiinvoiceID 
    LIMIT 1;";  // استخدام LIMIT 1 للحصول على أول فاتورة فقط

            using (SQLiteConnection connection = new SQLiteConnection(ClsSettings.ConnectionString))
            {
                using (SQLiteCommand updateCommand = new SQLiteCommand(updateQuery, connection))
                {
                    // إضافة معلمات الاستعلام للتحديث
                    updateCommand.Parameters.AddWithValue("@NewmultiinvoiceID", NewmultiinvoiceID);
                    updateCommand.Parameters.AddWithValue("@MultiinvoiceId", MultiinvoiceId);

                    try
                    {
                        connection.Open();

                        // تنفيذ جملة التحديث
                        int rowsAffected = updateCommand.ExecuteNonQuery();

                        if (rowsAffected > 0) // إذا تم تحديث الصفوف بنجاح
                        {
                            using (SQLiteCommand selectCommand = new SQLiteCommand(selectQuery, connection))
                            {
                                selectCommand.Parameters.AddWithValue("@NewmultiinvoiceID", NewmultiinvoiceID);

                                using (SQLiteDataReader reader = selectCommand.ExecuteReader())
                                {
                                    // قراءة أول نتيجة من جملة الاختيار
                                    if (reader.Read())
                                    {
                                        invoiceId = reader.GetInt32(0);  // تخزين رقم الفاتورة
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        ClsSettings.CreateTheErrorAtEventLog(ex.Message);
                    }
                }
            }

            return invoiceId; // إرجاع رقم الفاتورة المُحدثة أو -1 إذا لم يتم العثور على فواتير
        }
        public static int FindInvoiceIDBYMultiId(int Multi_InvoiceiD)
        {
            string query = "SELECT InvoiceId FROM SingleInvoice WHERE Multi_InvoiceiD = @Multi_InvoiceiD ;";
            int multiInvoiceId = -1; // افتراضياً، قد تحتاج إلى معالجة حالة عدم العثور على أي نتائج

            using (SQLiteConnection connection = new SQLiteConnection(ClsSettings.ConnectionString))
            {
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Multi_InvoiceiD", Multi_InvoiceiD);

                    try
                    {
                        connection.Open();
                        object result = command.ExecuteScalar();

                        if (result != null && result != DBNull.Value)
                        {
                            multiInvoiceId = Convert.ToInt32(result);
                        }
                    }
                    catch (Exception ex)
                    {
                        // Handle exception (log it, rethrow it, etc.)
                        ClsSettings.CreateTheErrorAtEventLog(ex.Message);
                    }
                }
            }

            return multiInvoiceId;
        }
        public static DataTable FindToSearch(int ID)
        {
            DataTable invoices = new DataTable();
            using (SQLiteConnection connection = new SQLiteConnection(ClsSettings.ConnectionString))
            {
                // حساب الفهرس الابتدائي للصفوف

                // تعديل الاستعلام ليشمل التصفح
                string query = $@"
            SELECT 
                SingleInvoice.InvoiceId AS 'الرقم',
                FoodsAndDrinks.Name AS 'الاسم',
                SingleInvoice.SoldQuantity AS 'الكميه',
                SingleInvoice.dateAndTime AS 'التاريخ والوقت',
                SingleInvoice.Taxes AS 'الخدمه',
                SingleInvoice.Multi_InvoiceiD AS 'الفاتوره المدمجه',

                SingleInvoice.TotalPrice AS 'الاجمالي'
            FROM 
                SingleInvoice
            INNER JOIN 
                FoodsAndDrinks 
            ON 
                FoodsAndDrinks.ID = SingleInvoice.DrinkId
           
        ";

                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    // استخدام معلمة للـ offset
                    command.Parameters.AddWithValue("@ID", ID);

                    try
                    {
                        connection.Open();
                        using (SQLiteDataReader reader = command.ExecuteReader())
                        {
                            // تحميل البيانات من القارئ إلى الـ DataTable
                            invoices.Load(reader);
                        }
                    }
                    catch (Exception ex)
                    {
                        ClsSettings.CreateTheErrorAtEventLog(ex.Message);
                    }
                }
            }
            return invoices;
        }
        public static DataTable FindById(int ID)
        {
            DataTable invoices = new DataTable();
            using (SQLiteConnection connection = new SQLiteConnection(ClsSettings.ConnectionString))
            {
                // حساب الفهرس الابتدائي للصفوف

                // تعديل الاستعلام ليشمل التصفح
                string query = @"
            SELECT 
SingleInvoice.InvoiceId as 'الرقم',
                FoodsAndDrinks.Name as 'اسم المنتج',
                SingleInvoice.SoldQuantity as 'الكميه',
            
                SingleInvoice.TotalPrice as 'الاجمالي'
            FROM 
                SingleInvoice
            INNER JOIN 
                FoodsAndDrinks 
            ON 
                SingleInvoice.DrinkId = FoodsAndDrinks.ID
            where SingleInvoice.InvoiceId=@ID";

                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    // استخدام معلمة للـ offset
                    command.Parameters.AddWithValue("@ID", ID);

                    try
                    {
                        connection.Open();
                        using (SQLiteDataReader reader = command.ExecuteReader())
                        {
                            // تحميل البيانات من القارئ إلى الـ DataTable
                            invoices.Load(reader);
                        }
                    }
                    catch (Exception ex)
                    {
                        ClsSettings.CreateTheErrorAtEventLog(ex.Message);
                    }
                }
            }
            return invoices;
        }
        public static void DeleteTaxesWhereMultiIdIsDESC(int MultiInvoiceId)
        {
            decimal Taxes = ClsCafeDetails.GetTaxes();
            string query = @"
        UPDATE SingleInvoice 
        SET Taxes = 0, TotalPrice = TotalPrice - @Taxes 
        WHERE InvoiceId = (
            SELECT InvoiceId 
            FROM SingleInvoice 
            WHERE Multi_InvoiceiD = @MultiInvoiceId 
            ORDER BY InvoiceId DESC 
            LIMIT 1
        );
    ";

            using (SQLiteConnection connection = new SQLiteConnection(ClsSettings.ConnectionString))
            {
                try
                {
                    connection.Open();
                    using (SQLiteCommand command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Taxes", Taxes);
                        command.Parameters.AddWithValue("@MultiInvoiceId", MultiInvoiceId);

                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    // Handle exception, e.g., log it
                    ClsSettings.CreateTheErrorAtEventLog(ex.Message);
                }
            }
        }
        public static void DeleteTaxesFromLastInvoicehByMultiInvoice(int InvoiceId)
        {
            string query = "UPDATE SingleInvoice Set TotalPrice = TotalPrice-Taxes, Multi_InvoiceiD=@InvoiceId ,Taxes= 0  WHERE Multi_InvoiceiD = @InvoiceId order by InvoiceId desc Limit 1";

            using (SQLiteConnection connection = new SQLiteConnection(ClsSettings.ConnectionString))
            {
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@InvoiceId", InvoiceId);

                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        // Handle exception (log it, rethrow it, etc.)
                        ClsSettings.CreateTheErrorAtEventLog(ex.Message);
                    }
                }
            }
        }
        public static void DeleteTaxesFromLastInvoiceAtMultiInvoiceToPerformAdding(int InvoiceId)
        {
            string query = "UPDATE SingleInvoice Set TotalPrice = TotalPrice-Taxes ,Taxes= 0  WHERE InvoiceId = @InvoiceId";

            using (SQLiteConnection connection = new SQLiteConnection(ClsSettings.ConnectionString))
            {
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@InvoiceId", InvoiceId);

                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        // Handle exception (log it, rethrow it, etc.)
                        ClsSettings.CreateTheErrorAtEventLog(ex.Message);
                    }
                }
            }
        }
        public static void UpdateInvoiceWithNewMultiInvoiceNumberBySingleInvoice(int MultiInvoice, int InvoiceId)
        {
            string query = "update SingleInvoice set Multi_InvoiceiD=@MultiInvoice where InvoiceId=@InvoiceId order by InvoiceId desc limit 1";

            using (SQLiteConnection connection = new SQLiteConnection(ClsSettings.ConnectionString))
            {
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@MultiInvoice", MultiInvoice);
                    command.Parameters.AddWithValue("@InvoiceId", InvoiceId);

                    try
                    {
                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        // التعامل مع الأخطاء وتسجيلها
                        ClsSettings.CreateTheErrorAtEventLog(ex.Message);
                    }
                }
            }
        }
        public static bool SetTaxesForTheLastRowByMulti_Id(int MultiInvoceId,decimal Taxes)
        {
            bool Updated = false;
            string Query = @"
        UPDATE SingleInvoice 
        SET TotalPrice = TotalPrice + @Taxes, Taxes = @Taxes 
        WHERE InvoiceId = (
            SELECT MAX(InvoiceId) FROM SingleInvoice WHERE Multi_InvoiceiD = @MultiInvoceId
        );";

            using (SQLiteConnection connection = new SQLiteConnection(ClsSettings.ConnectionString))
            {
                using (SQLiteCommand command = new SQLiteCommand(Query, connection))
                {
                    // تمرير المعلمات (ضرائب ورقم الفاتورة)
                    command.Parameters.AddWithValue("@Taxes", Taxes);
                    command.Parameters.AddWithValue("@MultiInvoceId", MultiInvoceId);

                    try
                    {
                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();
                        if(rowsAffected>0)
                        {
                            Updated = true;
                        }
                     
                    }
                    catch (Exception ex)
                    {
                        ClsSettings.CreateTheErrorAtEventLog(ex.Message);
                    }
                }
            }
            return Updated;
        }



    }
}
