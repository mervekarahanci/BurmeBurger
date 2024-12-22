using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Burme.Burger
{
    public class SqliteDataAccess
    {
        private static string connectionString = "Data Source=Burme.Burger.db;Version=3;";

     


        // Kullanıcıyı kontrol et (Login)
        public static bool ValidateUser(string phone, string password)
        {
            using (SQLiteConnection conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT COUNT(1) FROM Users WHERE Phone = @Phone AND Password = @Password";
                using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Phone", phone);
                    cmd.Parameters.AddWithValue("@Password", password);
                    return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
                }
            }
        }

        // Kullanıcıyı kaydet (Register)
        public static bool RegisterUser(string name, string phone, string password)
        {
            using (SQLiteConnection conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                // Aynı telefon numarası kontrolü
                string checkQuery = "SELECT COUNT(1) FROM Users WHERE Phone = @Phone";
                using (SQLiteCommand checkCmd = new SQLiteCommand(checkQuery, conn))
                {
                    checkCmd.Parameters.AddWithValue("@Phone", phone);
                    if (Convert.ToInt32(checkCmd.ExecuteScalar()) > 0)
                        return false; // Numara kayıtlı
                }

                string insertQuery = "INSERT INTO Users (AdSoyad, Phone, Password) VALUES (@Name, @Phone, @Password)";
                using (SQLiteCommand insertCmd = new SQLiteCommand(insertQuery, conn))
                {
                    insertCmd.Parameters.AddWithValue("@Name", name);
                    insertCmd.Parameters.AddWithValue("@Phone", phone);
                    insertCmd.Parameters.AddWithValue("@Password", password);
                    insertCmd.ExecuteNonQuery();
                    return true;
                }
            }
        }

        // Sipariş Ekleme
        public static void AddOrder(int userID, decimal totalAmount, string paymentMethod, List<(string ProductName, int Quantity, decimal Price)> details)
        {
            using (SQLiteConnection conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                string insertOrderQuery = "INSERT INTO Orders (UserID, TotalAmount, PaymentMethod) VALUES (@UserID, @TotalAmount, @PaymentMethod)";
                using (SQLiteCommand cmd = new SQLiteCommand(insertOrderQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@UserID", userID);
                    cmd.Parameters.AddWithValue("@TotalAmount", totalAmount);
                    cmd.Parameters.AddWithValue("@PaymentMethod", paymentMethod);
                    cmd.ExecuteNonQuery();

                    // Sipariş ID'sini al
                    long orderID = conn.LastInsertRowId;

                    foreach (var item in details)
                    {
                        string insertDetailQuery = @"INSERT INTO OrderDetails (OrderID, ProductName, Quantity, Price) 
                                                 VALUES (@OrderID, @ProductName, @Quantity, @Price)";
                        using (SQLiteCommand detailCmd = new SQLiteCommand(insertDetailQuery, conn))
                        {
                            detailCmd.Parameters.AddWithValue("@OrderID", orderID);
                            detailCmd.Parameters.AddWithValue("@ProductName", item.ProductName);
                            detailCmd.Parameters.AddWithValue("@Quantity", item.Quantity);
                            detailCmd.Parameters.AddWithValue("@Price", item.Price);
                            detailCmd.ExecuteNonQuery();
                        }
                    }
                }
            }
        }

        public static void AddAddress(int userID, string address)
        {
            using (SQLiteConnection conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                string query = "INSERT INTO Addresses (UserID, Address) VALUES (@UserID, @Address)";
                using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserID", userID);
                    cmd.Parameters.AddWithValue("@Address", address);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // Kart Ekleme Metodu
        public static void AddCard(int userID, string cardNumber, string cardHolderName)
        {
            using (SQLiteConnection conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                string query = "INSERT INTO Cards (UserID, CardNumber, CardHolderName) VALUES (@UserID, @CardNumber, @CardHolderName)";
                using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserID", userID);
                    cmd.Parameters.AddWithValue("@CardNumber", cardNumber);
                    cmd.Parameters.AddWithValue("@CardHolderName", cardHolderName);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // Adresleri Getirme Metodu
        public static List<string> GetAddresses(int userID)
        {
            List<string> addresses = new List<string>();
            using (SQLiteConnection conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT Address FROM Addresses WHERE UserID = @UserID";
                using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserID", userID);
                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            addresses.Add(reader.GetString(0));
                        }
                    }
                }
            }
            return addresses;
        }

        // Kartları Getirme Metodu
        public static List<string> GetCards(int userID)
        {
            List<string> cards = new List<string>();
            using (SQLiteConnection conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT CardNumber FROM Cards WHERE UserID = @UserID";
                using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserID", userID);
                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            cards.Add(reader.GetString(0));
                        }
                    }
                }
            }
            return cards;
        }


      
            
        

    }
}
   
