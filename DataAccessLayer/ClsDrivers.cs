using CafeDateAccess;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadeDateACcess
{
    public  class ClsDrivers
    {
        public static DataTable GetAllDriversName()
        {
            DataTable Drinks = new DataTable();
            using (SQLiteConnection connection = new SQLiteConnection(ClsSettings.ConnectionString))
            {
                // حساب الفهرس الابتدائي للصفوف

                // تعديل الاستعلام ليشمل التصفح
                string Query = @"
          select DriverName from Drivers"; // تغيير 50 إلى العدد المطلوب من الصفوف في كل صفحة

                using (SQLiteCommand command = new SQLiteCommand(Query, connection))
                {
                    // استخدام المعلمة للتعامل مع الفهرس

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
        public static bool IsThisDriverAlreadeyExists(string DriverName)
        {
            bool IsFound = false;
            using (SQLiteConnection connection = new SQLiteConnection(ClsSettings.ConnectionString))
            {
                string Query = "SELECT * FROM Drivers WHERE DriverName = @DriverName;";
                using (SQLiteCommand command = new SQLiteCommand(Query, connection))
                {
                    command.Parameters.AddWithValue("@DriverName", DriverName);

                    try
                    {
                        connection.Open();
                        using(SQLiteDataReader reader=command.ExecuteReader())
                        {
                            if(reader.HasRows)
                            {
                                IsFound = true;
                            }
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
        public static bool AddNew(string Name)
        {
            bool IsAddedSuccessfully = false;
            using (SQLiteConnection connection = new SQLiteConnection(ClsSettings.ConnectionString))
            {
                string Query = "INSERT INTO [Drivers]\r\n           (         [DriverName]\r\n           )  VALUES ( @Name);";
                using (SQLiteCommand command = new SQLiteCommand(Query, connection))
                {
                    command.Parameters.AddWithValue("@Name", Name);
             
                    try
                    {
                        connection.Open();
                        int RowsAffected = command.ExecuteNonQuery();
                        if (RowsAffected > 0)
                        {
                            IsAddedSuccessfully = true;
                        }
                    }
                    catch (Exception ex)
                    {
                        ClsSettings.CreateTheErrorAtEventLog(ex.Message);
                    }


                }
            }
            return IsAddedSuccessfully;
        }


    }
}
