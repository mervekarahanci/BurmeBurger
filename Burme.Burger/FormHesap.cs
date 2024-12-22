using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Burme.Burger
{
    public partial class FormHesap : Form
    {
        private static string connectionString = "Data Source=Burme.Burger.db;Version=3;";

        private List<(string ProductName, int Quantity, decimal Price)> aktifSiparisListesi;
        private int colorIndex = 0; // Şu anki renk sırasını tutar
        private Color[] colors = { Color.DarkGreen, Color.LightCoral, Color.Gold, Color.Tan, Color.DarkOrange }; // Renkler
        private int backgroundColorIndex = 0; // Arka plan renk sırasını tutar
        private Color[] colors2 = {
         ColorTranslator.FromHtml("#FA987C"),
    ColorTranslator.FromHtml("#FBA38B"),
    ColorTranslator.FromHtml("#FBAE99"),
    ColorTranslator.FromHtml("#FCBAA7"),
    ColorTranslator.FromHtml("#FCC5B6"),
    ColorTranslator.FromHtml("#FDD1C4"),
    ColorTranslator.FromHtml("#FDDCD3"),
    ColorTranslator.FromHtml("#FEE8E2")
};
        private Timer colorTimer; // Timer değişkeninin adı değiştirildi



        public FormHesap(List<(string ProductName, int Quantity, decimal Price)> siparisListesi = null)
        {
            InitializeComponent();
            aktifSiparisListesi = siparisListesi ?? new List<(string, int, decimal)>();
            colorTimer = new Timer // Yeni timer
            {
                Interval = 300 // 500 ms (yarım saniye)
            };
            colorTimer.Tick += colorChangeTimer_Tick;
        }

        private void FormHesap_Load(object sender, EventArgs e)
        {
            lvSiparis.Visible = false;
            colorTimer.Start();
            richTextBox_adresekle.Visible = false;
            btn_akayit.Visible = false;
            maskedTextBox_kart.Visible = false;
            btn_kkayit.Visible = false;
            // Kullanıcı verilerini yükle
            GlobalData.LoadUserData();

            // Adresleri listbox'a yükle
            listBox_adreslerim.Items.Clear();
            foreach (var adres in GlobalData.Adresler)
            {
                listBox_adreslerim.Items.Add(adres);
            }

            // Kartları listbox'a yükle
            listBox_kartlarim.Items.Clear();
            foreach (var kart in GlobalData.Kartlar)
            {
                listBox_kartlarim.Items.Add(kart);
            }

            // Aktif sipariş var mı kontrol et
            if (GlobalData.AktifSiparis.Any())
            {
                AktifSiparisGoster();
                pictureBox1.Visible = false;
                lbl_siparisver.Visible = false;
            }
            else
            {
                // Sipariş yoksa
                pictureBox1.Visible = true;
                lbl_siparisver.Visible = true;
            }
        }

        private void AktifSiparisGoster()
        {
            // Aktif siparişleri göstermek için ListView oluştur
            ListView lvAktifSiparis = new ListView
            {
                View = View.Details,
                Dock = DockStyle.Fill,
                FullRowSelect = true
            };

            lvAktifSiparis.Columns.Add("Ürün Adı", 200);
            lvAktifSiparis.Columns.Add("Adet", 50);
            lvAktifSiparis.Columns.Add("Fiyat", 100);

            foreach (var urun in GlobalData.AktifSiparis)
            {
                var item = new ListViewItem(urun.ProductName);
                item.SubItems.Add(urun.Quantity.ToString());
                item.SubItems.Add($"{urun.Price:C2}");
                lvAktifSiparis.Items.Add(item);
            }

            // Transparent panelin içeriğini temizle ve yeni veriyi ekle
            transparentRoundedPanel4.Controls.Clear();
            transparentRoundedPanel4.Controls.Add(lvAktifSiparis);
        }


        private void SiparisleriGoster()
        {

            ListView lvSiparis = new ListView
            {
                View = View.Details,
                Dock = DockStyle.Fill,
                FullRowSelect = true
            };

            lvSiparis.Columns.Add("Ürün Adı", 150);
            lvSiparis.Columns.Add("Adet", 50);
            lvSiparis.Columns.Add("Fiyat", 100);

            // Siparişleri ListView'e ekle
            foreach (var urun in aktifSiparisListesi)
            {
                var item = new ListViewItem(urun.ProductName);
                item.SubItems.Add(urun.Quantity.ToString());
                item.SubItems.Add($"{urun.Price:C2}");
                lvSiparis.Items.Add(item);
            }

            // Siparişleri ListView'e ekle
            foreach (var urun in aktifSiparisListesi)
            {
                var item = new ListViewItem(urun.ProductName);
                item.SubItems.Add(urun.Quantity.ToString());
                item.SubItems.Add($"{urun.Price:C2}");
                lvSiparis.Items.Add(item);
            }

            // Transparent panelin içeriğini temizle ve yeni veriyi ekle
            pictureBox1.Visible=false;
            label1.Visible = false;
            lbl_siparisver.Visible=false;
            transparentRoundedPanel4.Controls.Add(lvSiparis);
        }

        private void AdresleriYukle()
        {
            
            foreach (var adres in GlobalData.Adresler)
            {
                listBox_adreslerim.Items.Add(adres);
            }
        }

        private void KartlariYukle()
        {
          
            foreach (var kart in GlobalData.Kartlar)
            {
                listBox_kartlarim.Items.Add(kart);
            }
        }

        private void btn_adresekle_Click(object sender, EventArgs e)
        {
            richTextBox_adresekle.Visible = true;
            btn_akayit.Visible = true;
        }

        private void btn_akayit_Click(object sender, EventArgs e)
        {
            string yeniAdres = richTextBox_adresekle.Text.Trim();

            if (!string.IsNullOrEmpty(yeniAdres))
            {
                // Adresi veritabanına ekle
                SqliteDataAccess.AddAddress(GlobalData.LoggedInUserID, yeniAdres);
                MessageBox.Show("Adres başarıyla eklendi!", "Başarı", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Adres listesini güncelle
                LoadAddresses();
                richTextBox_adresekle.Clear();
                richTextBox_adresekle.Visible = false;
                btn_akayit.Visible = false;
            }
            else
            {
                MessageBox.Show("Adres boş olamaz!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_kartekle_Click(object sender, EventArgs e)
        {
            maskedTextBox_kart.Visible = true;
            btn_kkayit.Visible = true;
        }

        private void btn_kkayit_Click(object sender, EventArgs e)
        {
            string yeniKart = maskedTextBox_kart.Text.Trim();

            if (!string.IsNullOrEmpty(yeniKart))
            {
                // Kartı veritabanına ekle
                SqliteDataAccess.AddCard(GlobalData.LoggedInUserID, yeniKart, "Kart Sahibim"); // Kart sahibi adını düzenleyebilirsiniz
                MessageBox.Show("Kart başarıyla eklendi!", "Başarı", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Kart listesini güncelle
                LoadCards();
                maskedTextBox_kart.Clear();
                maskedTextBox_kart.Visible = false;
                btn_kkayit.Visible = false;
            }
            else
            {
                MessageBox.Show("Kart bilgisi boş olamaz!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lbl_siparisver_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            FormMain form_main = new FormMain();
            form_main.Show();
        }

        public void SiparisEkle(List<(string ProductName, int Quantity, decimal Price)> siparisler)
        {
            aktifSiparisListesi.AddRange(siparisler);
            FormHesap_Load(null, null); // Formu yenile
        }

        private void btn_ana_Click(object sender, EventArgs e)
        {
            this.Hide();
            FormMain form_main = new FormMain();
            form_main.Show();
        }

        private void colorChangeTimer_Tick(object sender, EventArgs e)
        {
            lbl_slogan.ForeColor = colors[colorIndex];
            colorIndex = (colorIndex + 1) % colors.Length;

            this.BackColor = colors2[backgroundColorIndex];
            backgroundColorIndex = (backgroundColorIndex + 1) % colors2.Length;
        }

        private void LoadAddresses()
        {
            listBox_adreslerim.Items.Clear();
            var addresses = SqliteDataAccess.GetAddresses(GlobalData.LoggedInUserID);
            foreach (var address in addresses)
            {
                listBox_adreslerim.Items.Add(address);
            }
        }

        private void LoadCards()
        {
            listBox_kartlarim.Items.Clear();
            var cards = SqliteDataAccess.GetCards(GlobalData.LoggedInUserID);
            foreach (var card in cards)
            {
                listBox_kartlarim.Items.Add(card);
            }
        }

   
    }
}
