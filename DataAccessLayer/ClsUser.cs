using CafeDateAccess;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadeDateACcess
{
    public class ClsUser
    {
        public static string GetUserName()
        {
            string UserName = null;
            byte ID=1;
        using(SQLiteConnection connection=new SQLiteConnection(ClsSettings.ConnectionString))
            {
                string Query = "select UserName from Users where UserId=@ID ;";
                    using(SQLiteCommand command=new SQLiteCommand(Query,connection))
                {
                    command.Parameters.AddWithValue("ID", ID);
                    try
                    {
                        connection.Open();
                        object result = command.ExecuteScalar();
                        if (result != null && !string.IsNullOrEmpty(result.ToString()))
                        {
                            UserName = result.ToString();
                        }
                    }catch(Exception ex)
                    {
                        ClsSettings.CreateTheErrorAtEventLog(ex.Message);
                    }
                  
                }
            }
            return UserName;
        }
        public static string GetPassword()
        {
            string password = null;
            byte ID = 1;
            using (SQLiteConnection connection = new SQLiteConnection(ClsSettings.ConnectionString))
            {
                string Query = "select Password from Users where UserId=@ID;";
                using (SQLiteCommand command = new SQLiteCommand(Query, connection))
                {
                    command.Parameters.AddWithValue("@ID", ID);
                    try
                    {
                        connection.Open();
                        object result = command.ExecuteScalar();
                        if (result != null && !string.IsNullOrEmpty(result.ToString()))
                        {
                            password = result.ToString();
                        }
                    }
                    catch (Exception ex)
                    {
                        ClsSettings.CreateTheErrorAtEventLog(ex.Message);
                    }
                }
            }
            return password;
        }

        public static bool IsTHisUserExists(string UserName,string Password)
        {
            bool IsFound = false;
            using(SQLiteConnection connection=new SQLiteConnection(ClsSettings.ConnectionString))
            {
                string query = "select * from Users where UserName=@UserName and Password=@Password";
                using(SQLiteCommand command=new SQLiteCommand(query,connection))
                {
                    command.Parameters.AddWithValue("@UserName", UserName);
                    command.Parameters.AddWithValue("@Password", Password);

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
                    }catch(Exception ex)
                    {
                        ClsSettings.CreateTheErrorAtEventLog(ex.Message);
                    }
                }
                return IsFound;
            }
        }
        public static bool UpdateUserEmailAndPassword(string UserName, string newPassword, byte userID=1)
        {
            bool isUpdated = false;

            using (SQLiteConnection connection = new SQLiteConnection(ClsSettings.ConnectionString))
            {
                string query = "UPDATE Users SET UserName = @UserName, Password = @Password WHERE UserId = @UserId;";

                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserName", UserName);
                    command.Parameters.AddWithValue("@Password", newPassword);
                    command.Parameters.AddWithValue("@UserId", userID);

                    try
                    {
                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            isUpdated = true;
                        }
                    }
                    catch (Exception ex)
                    {
                        ClsSettings.CreateTheErrorAtEventLog(ex.Message);
                    }
                }
            }

            return isUpdated;
        }

    }
}
