using Microsoft.Data.Sqlite;

namespace _20._09
{
    class Program
    {
        static string connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=master;Trusted_Connection=true;";

        static void Main(string[] args)
        {
            if (ConnectToDatabase())
            {
                Console.WriteLine("Connected.");

                ShowAllData();
                ShowAllNames();
                ShowAllColors();
                ShowMaxCalories();
                ShowMinCalories();
                ShowAverageCalories();
                ShowCountByType("Vegetable");
                ShowCountByType("Fruit");
                ShowCountByColor("Red");
                ShowItemsBelowCalories(100);
                ShowItemsAboveCalories(200);
                ShowItemsInRangeCalories(100, 200);
                ShowItemsWithRedOrYellowColor();
            }
            else
            {
                Console.WriteLine("ERROR!!!");
            }
        }

        static bool ConnectToDatabase()
        {
            try
            {
                using (SqliteConnection connection = new SqliteConnection(connectionString))
                {
                    connection.Open();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR: " + ex.Message);
                return false;
            }
        }

        static void ShowAllData()
        {
            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM VegetablesAndFruits";
                SqliteCommand command = new SqliteCommand(query, connection);
                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine($"{reader["Name"]} - {reader["Type"]}, {reader["Color"]}, {reader["Calories"]} kcal");
                    }
                }
            }
        }

        static void ShowAllNames()
        {
            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT Name FROM VegetablesAndFruits";
                SqliteCommand command = new SqliteCommand(query, connection);
                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine($"{reader["Name"]}");
                    }
                }
            }
        }

        static void ShowAllColors()
        {
            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT DISTINCT Color FROM VegetablesAndFruits";
                SqliteCommand command = new SqliteCommand(query, connection);
                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine($"{reader["Color"]}");
                    }
                }
            }
        }

        static void ShowMaxCalories()
        {
            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT MAX(Calories) FROM VegetablesAndFruits";
                SqliteCommand command = new SqliteCommand(query, connection);
                var maxCalories = command.ExecuteScalar();
                Console.WriteLine($"Max cal: {maxCalories}");
            }
        }

        static void ShowMinCalories()
        {
            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT MIN(Calories) FROM VegetablesAndFruits";
                SqliteCommand command = new SqliteCommand(query, connection);
                var minCalories = command.ExecuteScalar();
                Console.WriteLine($"Min cal: {minCalories}");
            }
        }

        static void ShowAverageCalories()
        {
            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT AVG(Calories) FROM VegetablesAndFruits";
                SqliteCommand command = new SqliteCommand(query, connection);
                var avgCalories = command.ExecuteScalar();
                Console.WriteLine($"Average cal: {avgCalories}");
            }
        }

        static void ShowCountByType(string type)
        {
            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT COUNT(*) FROM VegetablesAndFruits WHERE Type = @Type";
                SqliteCommand command = new SqliteCommand(query, connection);
                command.Parameters.AddWithValue("@Type", type);
                var count = command.ExecuteScalar();
                Console.WriteLine($"Number of {type}: {count}");
            }
        }

        static void ShowCountByColor(string color)
        {
            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT COUNT(*) FROM VegetablesAndFruits WHERE Color = @Color";
                SqliteCommand command = new SqliteCommand(query, connection);
                command.Parameters.AddWithValue("@Color", color);
                var count = command.ExecuteScalar();
                Console.WriteLine($"Number of product color {color}: {count}");
            }
        }

        static void ShowItemsBelowCalories(int calories)
        {
            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM VegetablesAndFruits WHERE Calories < @Calories";
                SqliteCommand command = new SqliteCommand(query, connection);
                command.Parameters.AddWithValue("@Calories", calories);
                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine($"{reader["Name"]} - {reader["Calories"]} kcal");
                    }
                }
            }
        }

        static void ShowItemsAboveCalories(int calories)
        {
            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM VegetablesAndFruits WHERE Calories > @Calories";
                SqliteCommand command = new SqliteCommand(query, connection);
                command.Parameters.AddWithValue("@Calories", calories);
                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine($"{reader["Name"]} - {reader["Calories"]} kcal");
                    }
                }
            }
        }

        static void ShowItemsInRangeCalories(int minCalories, int maxCalories)
        {
            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM VegetablesAndFruits WHERE Calories BETWEEN @MinCalories AND @MaxCalories";
                SqliteCommand command = new SqliteCommand(query, connection);
                command.Parameters.AddWithValue("@MinCalories", minCalories);
                command.Parameters.AddWithValue("@MaxCalories", maxCalories);
                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine($"{reader["Name"]} - {reader["Calories"]} kcal");
                    }
                }
            }
        }

        static void ShowItemsWithRedOrYellowColor()
        {
            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM VegetablesAndFruits WHERE Color = 'Red' OR Color = 'Yellow'";
                SqliteCommand command = new SqliteCommand(query, connection);
                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine($"{reader["Name"]} - {reader["Color"]}");
                    }
                }
            }
        }
    }
}
