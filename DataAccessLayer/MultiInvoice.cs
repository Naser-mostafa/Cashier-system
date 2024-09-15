using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data;
namespace CafeDateAccess
{
    public class MultiInvoice
    {

        public static int LastId()
        {
            int lastId = 0;
            string connectionString = ClsSettings.ConnectionString;

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                string query = "SELECT MAX(MultiInvoiceId) FROM MultiInvoices;";

                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    try
                    {
                        connection.Open();
                        object result = command.ExecuteScalar();

                        if (result != DBNull.Value && int.TryParse(result.ToString(), out int resultValue))
                        {
                            lastId = resultValue;
                        }
                    }
                    catch (Exception ex)
                    {
                        ClsSettings.CreateTheErrorAtEventLog(ex.Message);
                    }
                }
            }

            return lastId;
        }
        public static bool AddInvoice(int ID,DateTime dt)
        {
            bool IsAdded = false;
            using (SQLiteConnection connection = new SQLiteConnection(ClsSettings.ConnectionString))
            {
                string query = "INSERT INTO MultiInvoices(MultiInvoiceId,dateAndTime) VALUES (@ID,@dt);";

                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@dt", dt);
                    command.Parameters.AddWithValue("@ID", ID);


                    try
                    {
                        connection.Open();
                        int RowsAffected = command.ExecuteNonQuery();
                        if(RowsAffected>0)
                        {
                            IsAdded = true;
                        }
                       
                    }
                    catch (Exception ex)
                    {
                        ClsSettings.CreateTheErrorAtEventLog(ex.Message);
                    }
                }

            }

            return IsAdded;
        }

        public static DataTable GetAllInvoices(int pageNumber)
        {
            // 
            DataTable Drinks = new DataTable();
            using (SQLiteConnection connection = new SQLiteConnection(ClsSettings.ConnectionString))
            {
                // حساب الفهرس الابتدائي للصفوف
                int offset = (pageNumber - 1) * 50; // تغيير 50 إلى العدد المطلوب من الصفوف في كل صفحة

                // تعديل الاستعلام ليشمل التصفح
                string Query = $@"
                            select MultiInvoiceId as 'الرقم',dateAndTime as 'التاريخ والوقت' from MultiInvoices Limit 50 OFFSET {offset}";

                using (SQLiteCommand command = new SQLiteCommand(Query, connection))
                {
                    // استخدام المعلمة للتعامل مع الفهرس
                    command.Parameters.AddWithValue("@offset", offset);

                    try
                    {
                        connection.Open();
                        using (SQLiteDataReader Reader = command.ExecuteReader())
                        {
                            // تحميل البيانات من القارئ إلى الـ DataTable
                            Drinks.Load(Reader);
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
        public static bool DeleteInvoice(int MultiINvoiceId)
        {
            bool isDeleted = false;
            string query = "DELETE FROM MultiInvoices WHERE MultiInvoiceId = @MultiINvoiceId";

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
        public static DataTable FindById(int ID)
        {
            DataTable Drinks = new DataTable();
            using (SQLiteConnection connection = new SQLiteConnection(ClsSettings.ConnectionString))
            {
             
                string Query = @"
            SELECT MultiInvoiceId as 'الرقم' ,dateAndTime as 'التاريخ والوقت' from MultiInvoices
            
            where MultiInvoiceId=@ID "; // تغيير 50 إلى العدد المطلوب من الصفوف في كل صفحة

                using (SQLiteCommand command = new SQLiteCommand(Query, connection))
                {
                    // استخدام المعلمة للتعامل مع الفهرس
                    command.Parameters.AddWithValue("@ID", ID);

                    try
                    {
                        connection.Open();
                        using (SQLiteDataReader Reader = command.ExecuteReader())
                        {
                            // تحميل البيانات من القارئ إلى الـ DataTable
                            Drinks.Load(Reader);
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

        public static bool IsInvoiceExists(int invoiceId)
        {
            bool exists = false;
            string query = "SELECT COUNT(1) FROM MultiInvoices WHERE MultiInvoiceId = @InvoiceId";

            using (SQLiteConnection connection = new SQLiteConnection(ClsSettings.ConnectionString))
            {
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    // إضافة معلمة الفاتورة
                    command.Parameters.AddWithValue("@InvoiceId", invoiceId);

                    try
                    {
                        connection.Open();
                        // تنفيذ الاستعلام والحصول على النتيجة
                        int count = Convert.ToInt32(command.ExecuteScalar());
                        exists = count > 0; // إذا كانت النتيجة أكبر من 0، فهذا يعني أن الفاتورة موجودة
                    }
                    catch (Exception ex)
                    {
                        ClsSettings.CreateTheErrorAtEventLog(ex.Message);
                    }
                }
            }

            return exists;
        }

    }
}
