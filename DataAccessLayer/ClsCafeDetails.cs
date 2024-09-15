using CafeDateAccess;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadeDateACcess
{
    public class ClsCafeDetails
    {
        public static bool AddNew(string CafeNumber,string CafeAddress,decimal Taxes)
        {
            bool IsAddedSuccessfully = false;
            using (SQLiteConnection connection = new SQLiteConnection(ClsSettings.ConnectionString))
            {
                string Query = "INSERT INTO [CafeDetails]\r\n           (         [CafeNumber],[CafeAddress],[Taxes]          )  VALUES ( @CafeNumber,@CafeAddress,@axes);";
                using (SQLiteCommand command = new SQLiteCommand(Query, connection))
                {
                    command.Parameters.AddWithValue("@CafeNumber", CafeNumber);
                    command.Parameters.AddWithValue("@CafeAddress", CafeAddress);
                    command.Parameters.AddWithValue("@Taxes", Taxes);
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
        public static bool UpdateDetails( string CafeNumber, string CafeAddress, decimal Taxes)
        {
            byte id = 1;
            bool isUpdatedSuccessfully = false;
            using (SQLiteConnection connection = new SQLiteConnection(ClsSettings.ConnectionString))
            {
                string query = @"UPDATE CafeDetails
                         SET CafeNumber = @CafeNumber,
                             CafeAddress = @CafeAddress,
                             Taxes = @Taxes
                         WHERE ID = @ID;";

                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ID", id);
                    command.Parameters.AddWithValue("@CafeNumber", CafeNumber);
                    command.Parameters.AddWithValue("@CafeAddress", CafeAddress);
                    command.Parameters.AddWithValue("@Taxes", Taxes);

                    try
                    {
                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            isUpdatedSuccessfully = true;
                        }
                    }
                    catch (Exception ex)
                    {
                        ClsSettings.CreateTheErrorAtEventLog(ex.Message);
                    }
                }
            }
            return isUpdatedSuccessfully;
        }
        public static void GetDetails(ref string CafeNumber,ref string CafeAddress, ref decimal Taxes)
        {
            using(SQLiteConnection connection =new SQLiteConnection(ClsSettings.ConnectionString))
            {
                string Query = "select * from CafeDetails;";
                    using (SQLiteCommand command=new SQLiteCommand(Query,connection))
                {
                    try
                    {
                        connection.Open();
                        using (SQLiteDataReader Reader = command.ExecuteReader())
                    {
                       
                            while (Reader.Read())
                            {
                                CafeNumber = Reader["CafeNumber"].ToString();
                                CafeAddress = Reader["cafeAddress"].ToString();
                                Taxes = Convert.ToDecimal(Reader["Taxes"]);
                            }
                        }
                      
                    }
                    catch (Exception ex)
                    {
                        ClsSettings.CreateTheErrorAtEventLog(ex.Message);
                    }
                }
            }
        }
        public static string GetPhone()
        {
            string CafeNumber = "";
            using (SQLiteConnection connection = new SQLiteConnection(ClsSettings.ConnectionString))
            {
                string Query = "select CafeNumber from CafeDetails Where Id=1;";
                using (SQLiteCommand command = new SQLiteCommand(Query, connection))
                {
                    try
                    {
                        connection.Open();
                        using (SQLiteDataReader Reader = command.ExecuteReader())
                        {

                            while (Reader.Read())
                            {
                                CafeNumber = Reader["CafeNumber"].ToString();
                            }
                        }

                    }
                    catch (Exception ex)
                    {
                        ClsSettings.CreateTheErrorAtEventLog(ex.Message);
                    }
                }
            }
            return CafeNumber;
        }
        public static string GetAddress()
        {
            string CafeAddress = "";
            using (SQLiteConnection connection = new SQLiteConnection(ClsSettings.ConnectionString))
            {
                string Query = "select CafeAddress from CafeDetails Where Id=1;";
                using (SQLiteCommand command = new SQLiteCommand(Query, connection))
                {
                    try
                    {
                        connection.Open();
                        using (SQLiteDataReader Reader = command.ExecuteReader())
                        {

                            while (Reader.Read())
                            {
                                CafeAddress = Reader["CafeAddress"].ToString();
                            }
                        }

                    }
                    catch (Exception ex)
                    {
                        ClsSettings.CreateTheErrorAtEventLog(ex.Message);
                    }
                }
            }
            return CafeAddress;
        }
        public static decimal GetTaxes()
        {
            decimal Taxes=0;
            using (SQLiteConnection connection = new SQLiteConnection(ClsSettings.ConnectionString))
            {
                string Query = "select Taxes from CafeDetails Where Id=1;";
                using (SQLiteCommand command = new SQLiteCommand(Query, connection))
                {
                    try
                    {
                        connection.Open();
                        using (SQLiteDataReader Reader = command.ExecuteReader())
                        {
                            while (Reader.Read())
                            {
                                Taxes = Convert.ToDecimal(Reader["Taxes"]);
                            }
                        }

                    }
                    catch (Exception ex)
                    {
                        ClsSettings.CreateTheErrorAtEventLog(ex.Message);
                    }
                }
            }
            return Taxes;
        }



    }
}
