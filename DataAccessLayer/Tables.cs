using CafeDateAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadeDateACcess
{
    public class Tables
    {
        public static DataTable GetAllTables()
        {
            DataTable Drinks = new DataTable();
                using (SQLiteConnection connection = new SQLiteConnection(ClsSettings.ConnectionString))
                {
                    string Query = "SELECT * FROM Tables;";
                    using (SQLiteCommand command = new SQLiteCommand(Query, connection))
                    {
                        try
                        {
                            connection.Open();
                            using (SQLiteDataReader Reader = command.ExecuteReader())
                            {
                                // تأكد من وجود الأعمدة
                             

                              
                                {
                                Drinks.Load(Reader);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            ClsSettings.CreateTheErrorAtEventLog(ex.Message);
                        }
                    }
                }
                return Drinks;
            

        }
        public static void AddNewOpenTable(byte TableId,int DrinkId, int InvoiceId, int Quantity, float TotalPrice, float Taxes)
        {
            TotalPrice += Taxes;
            using (SQLiteConnection connection = new SQLiteConnection(ClsSettings.ConnectionString))
            {
                // تحديد استعلام SQL لإدراج السجل الجديد
                string query = @"
            INSERT INTO OpenTables (TableId,DrinkId, InvoiceId, Quantity, TotalPrice, Taxes)
            VALUES (@TableId,@DrinkId, @InvoiceId, @Quantity, @TotalPrice, @Taxes)";

                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    // إضافة القيم إلى المعلمات
                    command.Parameters.AddWithValue("@TableId", TableId);

                    command.Parameters.AddWithValue("@DrinkId", DrinkId);
                    command.Parameters.AddWithValue("@InvoiceId", InvoiceId);
                    command.Parameters.AddWithValue("@Quantity", Quantity);
                    command.Parameters.AddWithValue("@TotalPrice", TotalPrice);
                    command.Parameters.AddWithValue("@Taxes", Taxes);

                    try
                    {
                        // فتح الاتصال بقاعدة البيانات وتنفيذ الاستعلام
                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();

                    
                    }
                    catch (Exception ex)
                    {
                        // التعامل مع الاستثناءات المحتملة
                        Console.WriteLine("Error: " + ex.Message);
                    }
                }
            }
        }
        public static DataTable GetAllInvoicesOnTable(byte TableNumber)
        {
            DataTable result = new DataTable();

            string query = "SELECT FoodsAndDrinks.Name as 'اسم المنتج',OpenTables.Quantity as 'الكميه',OpenTables.TotalPrice as 'الاجمالي' from OpenTables inner join FoodsAndDrinks on  FoodsAndDrinks.ID=OpenTables.DrinkId   WHERE TableId = @TableNumber";

            using (SQLiteConnection connection = new SQLiteConnection(ClsSettings.ConnectionString))
            {
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@TableNumber", TableNumber);

                    try
                    {
                        connection.Open();

                        using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(command))
                        {
                            // Fill the DataTable with the results of the query
                            adapter.Fill(result);
                        }
                    }
                    catch (Exception ex)
                    {
                        // Handle any potential exceptions
                        Console.WriteLine("Error: " + ex.Message);
                    }
                }
            }

            return result;
        }
        public static bool DeleteOpenTableRecord(byte TableId)
        {
            bool isDeletedSuccessfully = false;

            using (SQLiteConnection connection = new SQLiteConnection(ClsSettings.ConnectionString))
            {
                // SQL query to delete the record
                string query = "DELETE FROM OpenTables WHERE TableId = @TableId";

                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    // Adding the value to the parameter
                    command.Parameters.AddWithValue("@TableId", TableId);

                    try
                    {
                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();

                        // Check if a record was deleted
                        if (rowsAffected > 0)
                        {
                            isDeletedSuccessfully = true;
                            Console.WriteLine("Record deleted successfully!");
                        }
                        else
                        {
                            Console.WriteLine("No record was deleted.");
                        }
                    }
                    catch (Exception ex)
                    {
                        // Handle any potential exceptions
                        Console.WriteLine("Error: " + ex.Message);
                    }
                }
            }

            return isDeletedSuccessfully;
        }
        public static bool DeleteOpenTableRecordByInvoiceId(int InvoiceId )
        {
            bool isDeletedSuccessfully = false;

            using (SQLiteConnection connection = new SQLiteConnection(ClsSettings.ConnectionString))
            {
                // SQL query to delete the record
                string query = @"
DELETE FROM OpenTables 
WHERE OpenTableId = (SELECT MAX(OpenTableId) FROM OpenTables WHERE InvoiceId = @InvoiceId);
";

                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    // Adding the value to the parameter
                    command.Parameters.AddWithValue("@InvoiceId", InvoiceId);

                    try
                    {
                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();

                        // Check if a record was deleted
                        if (rowsAffected > 0)
                        {
                            isDeletedSuccessfully = true;
                        }

                    }
                    catch (Exception ex)
                    {
                        ClsSettings.CreateTheErrorAtEventLog(ex.Message);
                    }
                }
            }

            return isDeletedSuccessfully;
        }
        public static bool IsMultiInvoice(int invoiceId)
        {
            int count = 0;
            string query = "SELECT COUNT(*) FROM OpenTables WHERE InvoiceId = @invoiceId";

            using (SQLiteConnection connection = new SQLiteConnection(ClsSettings.ConnectionString))
            {
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@invoiceId", invoiceId);
                    try
                    {
                        connection.Open();
                        var Result = command.ExecuteScalar();
                        if(Result!=null&&int.TryParse(Result.ToString(),out int Resultt))
                        {
                            count = int.Parse(Result.ToString());
                        }

                    }
                    catch (Exception ex)
                    {
                        // Handle exception (log it, rethrow it, etc.)
                        ClsSettings.CreateTheErrorAtEventLog(ex.Message);
                    }
                }
            }

            return count > 1;
        }
        public static void AddTable()
        {
            // استرجاع عدد الطاولات الحالية
            int tableNumber = GetAllTables().Rows.Count + 1;

            // استعلام SQL لإدخال رقم الطاولة الجديد
            string query = "INSERT INTO Tables (ID) VALUES (@tableNumber)";

            using (SQLiteConnection connection = new SQLiteConnection(ClsSettings.ConnectionString))
            {
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    // إضافة المعلمة للقيمة الجديدة (رقم الطاولة)
                    command.Parameters.AddWithValue("@tableNumber", tableNumber);

                    try
                    {
                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            Console.WriteLine("Table added successfully!");
                        }
                        else
                        {
                            Console.WriteLine("No table was added.");
                        }
                    }
                    catch (Exception ex)
                    {
                        // التعامل مع الاستثناءات المحتملة
                        ClsSettings.CreateTheErrorAtEventLog(ex.Message);
                    }
                }
            }
        }
        public static bool IsThisTableIsAlreadyExist(byte TableNumber)
        {
            bool isDeletedSuccessfully = false;

            using (SQLiteConnection connection = new SQLiteConnection(ClsSettings.ConnectionString))
            {
                // SQL query to delete the record
                string query = @"
    select * from Tables where ID=@TableNumber ;
";

                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    // Adding the value to the parameter
                    command.Parameters.AddWithValue("@TableNumber", TableNumber);

                    try
                    {
                        connection.Open();
                     using(SQLiteDataReader reader =command.ExecuteReader())
                        {
                            if(reader.HasRows)
                            {
                                isDeletedSuccessfully = true;
                            }
                        }

                    }
                    catch (Exception ex)
                    {
                        ClsSettings.CreateTheErrorAtEventLog(ex.Message);
                    }
                }
            }

            return isDeletedSuccessfully;
        }
        public static bool IsThereAreOrdersOnThisTable(byte TableNumber)
        {
            bool isDeletedSuccessfully = false;

            using (SQLiteConnection connection = new SQLiteConnection(ClsSettings.ConnectionString))
            {
                // SQL query to delete the record
                string query = @"
          select * from OpenTables where TableId=@TableNumber ;
";

                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    // Adding the value to the parameter
                    command.Parameters.AddWithValue("@TableNumber", TableNumber);

                    try
                    {
                        connection.Open();
                        using (SQLiteDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                isDeletedSuccessfully = true;
                            }
                        }

                    }
                    catch (Exception ex)
                    {
                        ClsSettings.CreateTheErrorAtEventLog(ex.Message);
                    }
                }
            }

            return isDeletedSuccessfully;
        }

        public static void AddTheDictionryForTablesAndTheirInvoiccs(byte Table,int InvoicesId)
        {
            // استعلام SQL لإدخال رقم الطاولة الجديد
            string query = "INSERT INTO  DictionryInfo(key,value) VALUES (@Table,@InvoicesId)";

            using (SQLiteConnection connection = new SQLiteConnection(ClsSettings.ConnectionString))
            {
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    // إضافة المعلمة للقيمة الجديدة (رقم الطاولة)
                    command.Parameters.AddWithValue("@Table", Table);
                    command.Parameters.AddWithValue("@InvoicesId", InvoicesId);


                    try
                    {
                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        // التعامل مع الاستثناءات المحتملة
                        ClsSettings.CreateTheErrorAtEventLog(ex.Message);
                    }
                }
            }
        }
        public static Dictionary<byte,int> GetAllOpenInvoices()
        {
            Dictionary<byte, int> All = new Dictionary<byte, int>();
            string query = "select * from DictionryInfo;";
            using (SQLiteConnection connection = new SQLiteConnection(ClsSettings.ConnectionString))
            {
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {

                    try
                    {
                        connection.Open();

                        using (SQLiteDataReader reader =  command.ExecuteReader())
                         while(reader.Read())
                        {
                         All.Add(Convert.ToByte(reader["Key"]), Convert.ToInt32(reader["Value"]));
                        }
                    }
                    catch (Exception ex)
                    {
                        // Handle any potential exceptions
                        ClsSettings.CreateTheErrorAtEventLog(ex.Message);
                    }
                }
            }

            return All;
        }
        public static byte FindTableIDBYMultiId(int Multi_InvoiceiD)
        {
            string query = "SELECT Tableid FROM OpenTables WHERE InvoiceId = @Multi_InvoiceiD ;";
            byte multiInvoiceId = 0; // افتراضياً، قد تحتاج إلى معالجة حالة عدم العثور على أي نتائج

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
                            multiInvoiceId = Convert.ToByte(result);
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
        public static byte FindOpenTableIdIDBYMultiId(int Multi_InvoiceiD)
        {
            string query = "SELECT OpenTableId FROM OpenTables WHERE InvoiceId = @Multi_InvoiceiD ;";
            byte multiInvoiceId = 0; // افتراضياً، قد تحتاج إلى معالجة حالة عدم العثور على أي نتائج

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
                            multiInvoiceId = Convert.ToByte(result);
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

        public static bool DeleteTablesAndTheirInvoices(byte Key)
        {
            bool isDeletedSuccessfully = false;

            using (SQLiteConnection connection = new SQLiteConnection(ClsSettings.ConnectionString))
            {
                // SQL query to delete the record
                string query = "DELETE FROM DictionryInfo WHERE Key = @Key";

                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    // Adding the value to the parameter
                    command.Parameters.AddWithValue("@Key", Key);

                    try
                    {
                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();

                        // Check if a record was deleted
                        if (rowsAffected > 0)
                        {
                            isDeletedSuccessfully = true;
                        }
                     
                    }
                    catch (Exception ex)
                    {
                        ClsSettings.CreateTheErrorAtEventLog(ex.Message);
                    }
                }
            }

            return isDeletedSuccessfully;
        }
        public static void AddOpenTablesWithInvoices(byte TableId,int InvoiceId)
        {
           
            using (SQLiteConnection connection = new SQLiteConnection(ClsSettings.ConnectionString))
            {
                // تحديد استعلام SQL لإدراج السجل الجديد
                string query = @"
            INSERT INTO OpenTables (Key,Value)
            VALUES (@TableId,@InvoiceId)";

                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    // إضافة القيم إلى المعلمات
                    command.Parameters.AddWithValue("@TableId", TableId);

                    command.Parameters.AddWithValue("@TableId", TableId);
                    command.Parameters.AddWithValue("@InvoiceId", InvoiceId);
                  

                    try
                    {
                        // فتح الاتصال بقاعدة البيانات وتنفيذ الاستعلام
                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            Console.WriteLine("Record added successfully!");
                        }
                        else
                        {
                            Console.WriteLine("No record was added.");
                        }
                    }
                    catch (Exception ex)
                    {
                        // التعامل مع الاستثناءات المحتملة
                        ClsSettings.CreateTheErrorAtEventLog(ex.Message);
                    }
                }
            }
        }
        public static short GetLastInvoiceFromOpenTable(short TableNumber)
        {
            int lastInvoiceId = 0;
            string query = "SELECT MAX(OpenTableId) FROM OpenTables WHERE TableId = @TableNumber";

            using (SQLiteConnection connection = new SQLiteConnection(ClsSettings.ConnectionString))
            {
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@TableNumber", TableNumber);

                    try
                    {
                        connection.Open();
                        object result = command.ExecuteScalar();
                        lastInvoiceId = result != DBNull.Value ? Convert.ToInt32(result) : 0;
                    }
                    catch (Exception ex)
                    {
                        // Handle exception (log it, rethrow it, etc.)
                        ClsSettings.CreateTheErrorAtEventLog(ex.Message);
                    }
                }
            }

            return Convert.ToInt16(lastInvoiceId);
        }
        public static void UpdateTaxesAndTotalPriceInvoice(short OpenTableId)
        {
            string query = "UPDATE OpenTables SET TotalPrice = TotalPrice - Taxes,Taxes = 0  WHERE OpenTableId = @OpenTableId";

            using (SQLiteConnection connection = new SQLiteConnection(ClsSettings.ConnectionString))
            {
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@OpenTableId", OpenTableId);

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
        public static int GetLastInvoiceId(short OpenTableId)
        {
            int lastInvoiceId = 0;
            string query = "SELECT InvoiceId FROM OpenTables WHERE OpenTableId = @OpenTableId ";

            using (SQLiteConnection connection = new SQLiteConnection(ClsSettings.ConnectionString))
            {
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@OpenTableId", OpenTableId);

                    try
                    {
                        connection.Open();
                        object result = command.ExecuteScalar();
                        lastInvoiceId = result != DBNull.Value ? Convert.ToInt32(result) : 0;
                    }
                    catch (Exception ex)
                    {
                        // Handle exception (log it, rethrow it, etc.)
                        ClsSettings.CreateTheErrorAtEventLog(ex.Message);
                    }
                }
            }

            return lastInvoiceId;
        }
        public static List<string> GetAllPurchasesOnThisTable(int TableId)
        {
            List<string> purchases = new List<string>();
            string Query = "SELECT * FROM OpenTables WHERE TableId = @TableId";

            using (SQLiteConnection connection = new SQLiteConnection(ClsSettings.ConnectionString))
            {
                using (SQLiteCommand command = new SQLiteCommand(Query, connection))
                {
                    // إضافة معلمة TableId للاستعلام
                    command.Parameters.AddWithValue("@TableId", TableId);
                    
                    try
                    {
                        connection.Open();
                        using (SQLiteDataReader reader = command.ExecuteReader())
                        {
                            // قراءة البيانات وإضافتها إلى القائمة
                            while (reader.Read())
                            { 
                                string Name = ClsDrinks.GetNameByID(Convert.ToInt32(reader["DrinkId"]));
                                Name +=reader[$"Quantity"].ToString()+"-";
                                purchases.Add(Name);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        ClsSettings.CreateTheErrorAtEventLog(ex.Message);
                        
                    }
                }
            }

            return purchases; // إرجاع القائمة التي تحتوي على كل المشروبات المرتبطة بهذا الجدول
        }
        public static bool IsThereAreMoreOneInvoiceWithSameNumberAtOpenTables(int InvoiceId)
        {
            bool hasMoreThanOne = false;
            string query = "SELECT COUNT(*) FROM OpenTables WHERE InvoiceId = @InvoiceId";

            using (SQLiteConnection connection = new SQLiteConnection(ClsSettings.ConnectionString))
            {
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    // إضافة معلمة الاستعلام
                    command.Parameters.AddWithValue("@InvoiceId", InvoiceId);

                    try
                    {
                        connection.Open();
                        int count = Convert.ToInt32(command.ExecuteScalar());

                        // إذا كان عدد الفواتير أكبر من 1، يعني أن هناك أكثر من فاتورة بنفس الرقم
                        hasMoreThanOne = count > 1;
                    }
                    catch (Exception ex)
                    {
                        ClsSettings.CreateTheErrorAtEventLog(ex.Message);
                    }
                }
            }

            return hasMoreThanOne;
        }
        public static void UpdateInvoiceIdWithNewMultInvoice(int OpenTableId,int MultiInvoice)
        {
            string query = "UPDATE OpenTables SET InvoiceId = @MultiInvoice WHERE OpenTableId = @OpenTableId";

            using (SQLiteConnection connection = new SQLiteConnection(ClsSettings.ConnectionString))
            {
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@OpenTableId", OpenTableId);
                    command.Parameters.AddWithValue("@MultiInvoice", MultiInvoice);


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

        public static void UpdateInvoiceIdWithNewMultInvoiceByMultiInvoiceId( int MultiInvoice)
        {
            string query = "UPDATE OpenTables SET InvoiceId = @MultiInvoice WHERE InvoiceId = @MultiInvoice";

            using (SQLiteConnection connection = new SQLiteConnection(ClsSettings.ConnectionString))
            {
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@MultiInvoice", MultiInvoice);


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

        public static void SetTaxesAlLastInvoiceWithMulti(int Multi)
        {
            decimal Taxes = ClsCafeDetails.GetTaxes(); // حساب الضرائب

            // استعلام SQL لتحديث الصف الأخير
            string Query = @"
        UPDATE OpenTables 
        SET TotalPrice = TotalPrice + @Taxes, Taxes = @Taxes 
        WHERE OpenTableId = (SELECT MAX(OpenTableId) FROM OpenTables WHERE InvoiceId = @InvoiceId);
    ";

            using (SQLiteConnection connection = new SQLiteConnection(ClsSettings.ConnectionString))
            {
                using (SQLiteCommand command = new SQLiteCommand(Query, connection))
                {
                    // إضافة المعلمات إلى الاستعلام
                    command.Parameters.AddWithValue("@Taxes", Taxes);
                    command.Parameters.AddWithValue("@InvoiceId", Multi);

                    try
                    {
                        // فتح الاتصال وتنفيذ التحديث
                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();

                        // التحقق إذا تم تحديث صف واحد على الأقل
                     
                    }
                    catch (Exception ex)
                    {
                        // التعامل مع الأخطاء وتسجيلها
                        ClsSettings.CreateTheErrorAtEventLog(ex.Message);
                    }
                }
            }
        }
        public static int GetOPenTableIdByInvoiceId(int InvoiceId)
        {
            int lastInvoiceId = 0;
            string query = "SELECT OpenTableId FROM OpenTables WHERE InvoiceId = @InvoiceId ";

            using (SQLiteConnection connection = new SQLiteConnection(ClsSettings.ConnectionString))
            {
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@InvoiceId", InvoiceId);

                    try
                    {
                        connection.Open();
                        object result = command.ExecuteScalar();
                        lastInvoiceId = result != DBNull.Value ? Convert.ToInt32(result) : 0;
                    }
                    catch (Exception ex)
                    {
                        // Handle exception (log it, rethrow it, etc.)
                        ClsSettings.CreateTheErrorAtEventLog(ex.Message);
                    }
                }
            }

            return lastInvoiceId;
        }
   
        public static void UpdateTableNumberWithNew(byte OldTableNumber, byte NewTableNumber)
        {
            string query = "update OpenTables set  TableId =@NewTableNumber where TableId=@OldTableNumber";

            using (SQLiteConnection connection = new SQLiteConnection(ClsSettings.ConnectionString))
            {
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@NewTableNumber", NewTableNumber);
                    command.Parameters.AddWithValue("@OldTableNumber", OldTableNumber);



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


    }
}
