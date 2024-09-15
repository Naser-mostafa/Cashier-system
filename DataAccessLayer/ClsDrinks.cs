using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeDateAccess
{
    public class DrinkDetails
    {
        public int ID { get; set; }
        public string DrinkName { get; set; }
        public float Price { get; set; }
        public string PicturePath { get; set; }
    }
    public class FoodDetails
    {
        public int ID { get; set; }
        public string DrinkName { get; set; }
        public float Price { get; set; }
        public float MidPrice { get; set; }
        public float SmallPrice { get; set; }


        public string PicturePath { get; set; }
    }
    public class ClsDrinks
    {
        public static List<DrinkDetails> GetAllDrinks()
        {
            List<DrinkDetails> Drinks = new List<DrinkDetails>();
            using (SQLiteConnection connection = new SQLiteConnection(ClsSettings.ConnectionString))
            {
                string Query = "SELECT * FROM FoodsAndDrinks where IsDrink=1;";
                using (SQLiteCommand command = new SQLiteCommand(Query, connection))
                {
                    try
                    {
                        connection.Open();
                        using (SQLiteDataReader Reader = command.ExecuteReader())
                        {
                            // تأكد من وجود الأعمدة
                            int idIndex = Reader.GetOrdinal("ID");
                            int nameIndex = Reader.GetOrdinal("Name");
                            int filePathIndex = Reader.GetOrdinal("FilePath");
                            int priceIndex = Reader.GetOrdinal("Price");

                            while (Reader.Read())
                            {
                                DrinkDetails CurrentDrink = new DrinkDetails
                                {
                                    ID = Reader.IsDBNull(idIndex) ? 0 : Convert.ToInt32(Reader.GetValue(idIndex)),
                                    DrinkName = Reader.IsDBNull(nameIndex) ? string.Empty : Reader.GetString(nameIndex),
                                    PicturePath = Reader.IsDBNull(filePathIndex) ? string.Empty : Reader.GetString(filePathIndex),
                                    Price = Reader.IsDBNull(priceIndex) ? 0.0f : float.Parse(Reader.GetValue(priceIndex).ToString())
                                };
                                Drinks.Add(CurrentDrink);
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
        public static List<FoodDetails> GetAllFoods()
        {
            List<FoodDetails> Drinks = new List<FoodDetails>();
            using (SQLiteConnection connection = new SQLiteConnection(ClsSettings.ConnectionString))
            {
                string Query = "SELECT \r\n   ID, \r\n   Name,\r\n   MAX(Price) AS Price,\r\n   FilePath,\r\n   IsDrink\r\nFROM \r\n   FoodsAndDrinks\r\nWHERE \r\n   IsDrink =0\r\nGROUP BY \r\n   Name, FilePath, IsDrink;\r\n";
                using (SQLiteCommand command = new SQLiteCommand(Query, connection))
                {
                    try
                    {
                        connection.Open();
                        using (SQLiteDataReader Reader = command.ExecuteReader())
                        {
                            // تأكد من وجود الأعمدة
                            int idIndex = Reader.GetOrdinal("ID");
                            int nameIndex = Reader.GetOrdinal("Name");
                            int filePathIndex = Reader.GetOrdinal("FilePath");
                            int priceIndex = Reader.GetOrdinal("Price");

                            while (Reader.Read())
                            {
                                FoodDetails CurrentDrink = new FoodDetails
                                {
                                    ID = Reader.IsDBNull(idIndex) ? 0 : Convert.ToInt32(Reader.GetValue(idIndex)),
                                    DrinkName = Reader.IsDBNull(nameIndex) ? string.Empty : Reader.GetString(nameIndex),
                                    PicturePath = Reader.IsDBNull(filePathIndex) ? string.Empty : Reader.GetString(filePathIndex),
                                    Price = Reader.IsDBNull(priceIndex) ? 0.0f : float.Parse(Reader.GetValue(priceIndex).ToString())
                                };
                                Drinks.Add(CurrentDrink);
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


        //this Use For add Drink Only
        public static bool AddNew(string Name, float Price, string FilePath)
        {
            bool IsAddedSuccessfully = false;
            using (SQLiteConnection connection = new SQLiteConnection(ClsSettings.ConnectionString))
            {
                string Query = "INSERT INTO [FoodsAndDrinks]\r\n           (         [Name]\r\n           ,[Price]\r\n           ,[FilePath],[IsDrink])  VALUES (@Name, @Price, @FilePath,1);";
                using (SQLiteCommand command = new SQLiteCommand(Query, connection))
                {
                    command.Parameters.AddWithValue("@Name", Name);
                    command.Parameters.AddWithValue("@FilePath", FilePath);
                    command.Parameters.AddWithValue("@Price", Price);
                    try
                    {
                        connection.Open();
                        int RowsAffected = command.ExecuteNonQuery();
                        if (RowsAffected > 0)
                        {
                            IsAddedSuccessfully = true;
                        }
                    } catch (Exception ex)
                    {
                        ClsSettings.CreateTheErrorAtEventLog(ex.Message);
                    }


                }
            }
            return IsAddedSuccessfully;
        }

        public static bool AddNewFood(string Name, float LargePrice, float MidePrice, float SmallPrice, string FilePath)
        {
            bool IsAddedSuccessfully = false;
            using (SQLiteConnection connection = new SQLiteConnection(ClsSettings.ConnectionString))
            {
                try
                {
                    connection.Open();
                    using (SQLiteTransaction transaction = connection.BeginTransaction())
                    {
                        // إضافة السعر الكبير
                        string QueryLarge = "INSERT INTO [FoodsAndDrinks]([Name], [Price], [FilePath], [IsDrink]) VALUES (@Name, @LargePrice, @FilePath, 0);";
                        using (SQLiteCommand command = new SQLiteCommand(QueryLarge, connection))
                        {
                            command.Parameters.AddWithValue("@Name", Name);
                            command.Parameters.AddWithValue("@LargePrice", LargePrice);
                            command.Parameters.AddWithValue("@FilePath", FilePath);
                            command.ExecuteNonQuery();
                        }

                        // إضافة السعر المتوسط
                        string QueryMide = "INSERT INTO [FoodsAndDrinks]([Name], [Price], [FilePath], [IsDrink]) VALUES (@Name, @MidePrice, @FilePath, 0);";
                        using (SQLiteCommand command = new SQLiteCommand(QueryMide, connection))
                        {
                            command.Parameters.AddWithValue("@Name", Name);
                            command.Parameters.AddWithValue("@MidePrice", MidePrice);
                            command.Parameters.AddWithValue("@FilePath", FilePath);
                            command.ExecuteNonQuery();
                        }

                        // إضافة السعر الصغير
                        string QuerySmall = "INSERT INTO [FoodsAndDrinks]([Name], [Price], [FilePath], [IsDrink]) VALUES (@Name, @SmallPrice, @FilePath, 0);";
                        using (SQLiteCommand command = new SQLiteCommand(QuerySmall, connection))
                        {
                            command.Parameters.AddWithValue("@Name", Name);
                            command.Parameters.AddWithValue("@SmallPrice", SmallPrice);
                            command.Parameters.AddWithValue("@FilePath", FilePath);
                            command.ExecuteNonQuery();
                        }

                        transaction.Commit();
                        IsAddedSuccessfully = true;
                    }
                }
                catch (Exception ex)
                {
                    ClsSettings.CreateTheErrorAtEventLog(ex.Message);
                }
            }
            return IsAddedSuccessfully;
        }


        public static bool UpdateDrink(int id, string newName, float newPrice, string newFilePath)
{
    bool isUpdatedSuccessfully = false;
    using (SQLiteConnection connection = new SQLiteConnection(ClsSettings.ConnectionString))
    {
        string query = @"UPDATE FoodsAndDrinks
                         SET Name = @Name,
                             Price = @Price,
                             FilePath = @FilePath
                         WHERE ID = @ID;";
                         
        using (SQLiteCommand command = new SQLiteCommand(query, connection))
        {
            command.Parameters.AddWithValue("@ID", id);
            command.Parameters.AddWithValue("@Name", newName);
            command.Parameters.AddWithValue("@Price", newPrice);
            command.Parameters.AddWithValue("@FilePath", newFilePath);

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
        public static bool UpdateFood(int id, string newName, float newLargePrice, float newSmallPrice, float newMidPrice, string newFilePath)
        {
            bool isUpdatedSuccessfully = false;
            using (SQLiteConnection connection = new SQLiteConnection(ClsSettings.ConnectionString))
            {
                // استعلامات التحديث الفردية
                string query1 = @"UPDATE FoodsAndDrinks
                          SET Name = @Name,
                              Price = @newLargePrice,
                              FilePath = @FilePath
                          WHERE ID = @ID";

                string query2 = @"UPDATE FoodsAndDrinks
                          SET Name = @Name,
                              Price = @newMidPrice,
                              FilePath = @FilePath
                          WHERE ID = @ID + 2";

                string query3 = @"UPDATE FoodsAndDrinks
                          SET Name = @Name,
                              Price = @newSmallPrice,
                              FilePath = @FilePath
                          WHERE ID = @ID + 1";

                try
                {
                    connection.Open();

                    // تنفيذ التحديثات بشكل منفصل
                    using (SQLiteCommand command = new SQLiteCommand(query1, connection))
                    {
                        command.Parameters.AddWithValue("@ID", id);
                        command.Parameters.AddWithValue("@Name", newName);
                        command.Parameters.AddWithValue("@newLargePrice", newLargePrice);
                        command.Parameters.AddWithValue("@FilePath", newFilePath);

                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected == 0) // التحقق من التأثير
                        {
                            throw new Exception("Failed to update large price row.");
                        }
                    }

                    using (SQLiteCommand command = new SQLiteCommand(query2, connection))
                    {
                        command.Parameters.AddWithValue("@ID", id);
                        command.Parameters.AddWithValue("@Name", newName);
                        command.Parameters.AddWithValue("@newMidPrice", newMidPrice);
                        command.Parameters.AddWithValue("@FilePath", newFilePath);

                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected == 0) // التحقق من التأثير
                        {
                            throw new Exception("Failed to update mid price row.");
                        }
                    }

                    using (SQLiteCommand command = new SQLiteCommand(query3, connection))
                    {
                        command.Parameters.AddWithValue("@ID", id);
                        command.Parameters.AddWithValue("@Name", newName);
                        command.Parameters.AddWithValue("@newSmallPrice", newSmallPrice);
                        command.Parameters.AddWithValue("@FilePath", newFilePath);

                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected == 0) // التحقق من التأثير
                        {
                            throw new Exception("Failed to update small price row.");
                        }
                    }

                    isUpdatedSuccessfully = true; // إذا وصلت هنا، فإن التحديثات كانت ناجحة
                }
                catch (Exception ex)
                {
                    ClsSettings.CreateTheErrorAtEventLog(ex.Message);
                }
            }
            return isUpdatedSuccessfully;
        }

        public static DrinkDetails GetDrinkById(int id)
        {
            DrinkDetails drink = null;
            using (SQLiteConnection connection = new SQLiteConnection(ClsSettings.ConnectionString))
            {
                string query = "SELECT * FROM FoodsAndDrinks WHERE ID = @ID;";
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ID", id);

                    try
                    {
                        connection.Open();
                        using (SQLiteDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                drink = new DrinkDetails
                                {
                                    ID = reader.IsDBNull(reader.GetOrdinal("ID")) ? 0 : Convert.ToInt32(reader["ID"]),
                                    DrinkName = reader.IsDBNull(reader.GetOrdinal("Name")) ? string.Empty : reader["Name"].ToString(),
                                    PicturePath = reader.IsDBNull(reader.GetOrdinal("FilePath")) ? string.Empty : reader["FilePath"].ToString(),
                                    Price = reader.IsDBNull(reader.GetOrdinal("Price")) ? 0.0f : float.Parse(reader["Price"].ToString())
                                };
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        ClsSettings.CreateTheErrorAtEventLog(ex.Message);
                    }
                }
            }
            return drink;
        }
        public static int GetIDByName(string Name)
        {
            int drinkId = 0;
            using (SQLiteConnection connection = new SQLiteConnection(ClsSettings.ConnectionString))
            {
                string query = "SELECT ID FROM FoodsAndDrinks WHERE Name = @Name;";
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Name", Name);

                    try
                    {
                        connection.Open();
                        object Result = command.ExecuteScalar();
                        drinkId = Result != null ? int.Parse(Result.ToString()) : 0;
                    }
                    catch (Exception ex)
                    {
                        ClsSettings.CreateTheErrorAtEventLog(ex.Message);
                    }
                }
            }
            return drinkId;
        }

        public static FoodDetails GetFoodById(int id)
        {
            FoodDetails drink = null;
            using (SQLiteConnection connection = new SQLiteConnection(ClsSettings.ConnectionString))
            {
                string query = "SELECT * FROM FoodsAndDrinks WHERE ID = @ID;";
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ID", id);

                    try
                    {
                        connection.Open();
                        using (SQLiteDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                drink = new FoodDetails
                                {
                                    ID = reader.IsDBNull(reader.GetOrdinal("ID")) ? 0 : Convert.ToInt32(reader["ID"]),
                                    DrinkName = reader.IsDBNull(reader.GetOrdinal("Name")) ? string.Empty : reader["Name"].ToString(),
                                    PicturePath = reader.IsDBNull(reader.GetOrdinal("FilePath")) ? string.Empty : reader["FilePath"].ToString(),
                                    Price = reader.IsDBNull(reader.GetOrdinal("Price")) ? 0.0f : float.Parse(reader["Price"].ToString())
                                };
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        ClsSettings.CreateTheErrorAtEventLog(ex.Message);
                    }
                }
            }
            drink.MidPrice = GetFoodPriceByID(id +2);
            drink.SmallPrice= GetFoodPriceByID(id + 1);
            return drink;
        }

        public static float GetFoodPriceByID(int ID)
        {
            float price = 0.0f;
            using (SQLiteConnection connection = new SQLiteConnection(ClsSettings.ConnectionString))
            {
                string query = "SELECT Price FROM FoodsAndDrinks WHERE ID = @ID;";
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ID", ID);

                    try
                    {
                        connection.Open();
                        object result = command.ExecuteScalar();
                        if (result != null && !Convert.IsDBNull(result))
                        {
                            price = float.Parse(result.ToString());
                        }
                    }
                    catch (Exception ex)
                    {
                        ClsSettings.CreateTheErrorAtEventLog(ex.Message);
                    }
                }
            }
            return price;
        }

        public static float GetPriceByName(string drinkName)
        {
            float price = 0.0f;
            using (SQLiteConnection connection = new SQLiteConnection(ClsSettings.ConnectionString))
            {
                string query = "SELECT Price FROM FoodsAndDrinks WHERE Name = @Name;";
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Name", drinkName);

                    try
                    {
                        connection.Open();
                        object result = command.ExecuteScalar();
                        if (result != null && !Convert.IsDBNull(result))
                        {
                            price = float.Parse(result.ToString());
                        }
                    }
                    catch (Exception ex)
                    {
                        ClsSettings.CreateTheErrorAtEventLog(ex.Message);
                    }
                }
            }
            return price;
        }
        public static string GetNameByID(int ID)
        {
            string Name = "";
            using (SQLiteConnection connection = new SQLiteConnection(ClsSettings.ConnectionString))
            {
                string query = "SELECT Name FROM FoodsAndDrinks WHERE ID = @ID;";
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ID", ID);

                    try
                    {
                        connection.Open();
                        object result = command.ExecuteScalar();
                        if (result != null && !Convert.IsDBNull(result))
                        {
                            Name = (result.ToString());
                        }
                    }
                    catch (Exception ex)
                    {
                        ClsSettings.CreateTheErrorAtEventLog(ex.Message);
                    }
                }
            }
            return Name;
        }

    }

}

