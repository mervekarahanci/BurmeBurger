using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Burme.Burger
{
    public static class GlobalData
    {
        private static string connectionString = "Data Source=Burme.Burger.db;Version=3;";

        // Kullanıcı ID'si (Giriş yapan kullanıcıyı takip etmek için)
        public static int LoggedInUserID { get; set; }

        // Sepet ve toplam tutar verisi
        public static List<(string Name, int Quantity, decimal Price)> SepetListesi { get; set; } = new List<(string, int, decimal)>();
        public static decimal ToplamTutar { get; set; } = 0;

        // Adresler ve kartlar listesi
        public static List<string> Adresler { get; set; } = new List<string>();
        public static List<string> Kartlar { get; set; } = new List<string>();

        // Aktif sipariş (sipariş onaylandıktan sonra kullanıcı hesabında görülecek siparişler)
        public static List<(string ProductName, int Quantity, decimal Price)> AktifSiparis { get; set; } = new List<(string, int, decimal)>();

        /// <summary>
        /// Kullanıcı adreslerini ve kartlarını veritabanından yükler.
        /// </summary>
        public static void LoadUserData()
        {
            // Önce listeleri temizleyelim ki tekrar eklenmesin
            Adresler.Clear();
            Kartlar.Clear();

            // Adres ve kart verilerini veritabanından yükle
            Adresler = SqliteDataAccess.GetAddresses(LoggedInUserID);
            Kartlar = SqliteDataAccess.GetCards(LoggedInUserID);
        }

        /// <summary>
        /// GlobalData sıfırlama (örneğin kullanıcı çıkış yaptığında kullanılır)
        /// </summary>
        public static void Reset()
        {
            LoggedInUserID = 0;
            SepetListesi.Clear();
            ToplamTutar = 0;
            Adresler.Clear();
            Kartlar.Clear();
            AktifSiparis.Clear();
        }
    }
}
