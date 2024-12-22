using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Burme.Burger
{
    public partial class FormSepet : Form
    {
        private static string connectionString = "Data Source=Burme.Burger.db;Version=3;";


        private List<(string Name, int Quantity, decimal Price)> sepetListesi; // Ürün adı, miktar ve fiyat
        private decimal toplamTutar;

        public FormSepet(List<(string Name, int Quantity, decimal Price)> sepetListesi = null, decimal toplamTutar = 0)
        {
            InitializeComponent();
            this.sepetListesi = sepetListesi ?? new List<(string, int, decimal)>();
            this.toplamTutar = toplamTutar;
        }

        private void FormSepet_Load(object sender, EventArgs e)
        {
            simdilik.Visible = true ;
            transparentRoundedPanel2.Visible = false; // Sağ panel varsayılan olarak gizli

            // Kullanıcı verilerini yükle
            GlobalData.LoadUserData();

            // Adresleri combobox'a yükle
            cmb_adres.Items.Clear();
            foreach (var adres in GlobalData.Adresler)
            {
                cmb_adres.Items.Add(adres);
            }
            if (cmb_adres.Items.Count > 0)
            {
                cmb_adres.SelectedIndex = 0; // İlk adresi seç
            }

            // Kartları combobox'a yükle
            cmb_kart.Items.Clear();
            foreach (var kart in GlobalData.Kartlar)
            {
                cmb_kart.Items.Add(kart);
            }
            cmb_kart.Enabled = cmb_kart.Items.Count > 0;

            // Sepeti güncelle
            SepetGuncelle();

            // Ödemeye geç butonunu kontrol et
            btn_gecis.Enabled = sepetListesi.Any(); // Sepet boşsa devre dışı bırak
        }

        private void SepetGuncelle()
        {
            listView.Items.Clear();
            foreach (var urun in sepetListesi)
            {
                ListViewItem item = new ListViewItem(urun.Name);
                item.SubItems.Add(urun.Quantity.ToString());
                item.SubItems.Add($"{(urun.Price * urun.Quantity):C2}");
                listView.Items.Add(item);
            }

            lbl_TL.Text = $"{toplamTutar:C2}";
            lbl_tutar2.Text = $"{toplamTutar:C2}";

            // Sepet boşsa Ödemeye Geç butonunu devre dışı bırak
            btn_gecis.Enabled = sepetListesi.Any();
        }

        private void btn_bosalt_Click(object sender, EventArgs e)
        {
            sepetListesi.Clear();
            toplamTutar = 0;
            SepetGuncelle();
            MessageBox.Show("Sepetiniz boşaltıldı.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btn_ekle_Click(object sender, EventArgs e)
        {
            var formMain = Application.OpenForms.OfType<FormMain>().FirstOrDefault();

            if (formMain != null)
            {
                formMain.UpdateCart(sepetListesi, toplamTutar);
                formMain.Show(); // FormMain'i göster
            }
            this.Hide(); // FormSepet'i gizle
        }

        private void radio_kredi_CheckedChanged(object sender, EventArgs e)
        {
            cmb_kart.Enabled = radio_kredi.Checked;
        }

        private void radio_kapi_CheckedChanged(object sender, EventArgs e)
        {
            if (radio_kapi.Checked)
            {
                cmb_kart.Enabled = false;
                cmb_kart.SelectedIndex = -1;
            }
        }

        private void btn_gecis_Click(object sender, EventArgs e)
        {
            // Sağ paneldeki resim kalkar, ödeme seçenekleri görünür
            simdilik.Visible = false;
            transparentRoundedPanel2.Visible = true;
        }

        
            private void btn_onayla_Click(object sender, EventArgs e)
        {
            if (!radio_kredi.Checked && !radio_kapi.Checked)
            {
                MessageBox.Show("Lütfen bir ödeme yöntemi seçin!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (radio_kredi.Checked && cmb_kart.SelectedIndex < 0)
            {
                MessageBox.Show("Lütfen bir kart seçin!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cmb_adres.SelectedIndex < 0)
            {
                MessageBox.Show("Lütfen teslimat adresi seçin!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string odemeYontemi = radio_kredi.Checked ? "Kredi Kartı" : "Kapıda Ödeme";
            int userID = GlobalData.LoggedInUserID; // GlobalData'dan giriş yapan UserID'yi al
            string adres = cmb_adres.SelectedItem.ToString();

            try
            {
                // Siparişi veritabanına kaydet
                SqliteDataAccess.AddOrder(userID, toplamTutar, odemeYontemi, sepetListesi);

                MessageBox.Show($"Siparişiniz başarıyla onaylandı!\nÖdeme Yöntemi: {odemeYontemi}\nToplam Tutar: {toplamTutar:C2}",
                    "Onay", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // GlobalData'ya aktif siparişi ekle
                GlobalData.AktifSiparis = new List<(string ProductName, int Quantity, decimal Price)>(sepetListesi);

                // Sepeti sıfırla
                sepetListesi.Clear();
                toplamTutar = 0;

                // Ana forma geri dön
                var formMain = Application.OpenForms.OfType<FormMain>().FirstOrDefault();
                if (formMain != null)
                {
                    formMain.UpdateCart(sepetListesi, toplamTutar);
                    formMain.Show();
                }

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_ana_Click(object sender, EventArgs e)
        {
            var formMain = Application.OpenForms.OfType<FormMain>().FirstOrDefault();

            if (formMain != null)
            {
                formMain.UpdateCart(sepetListesi, toplamTutar);
                formMain.Show(); // FormMain'i göster
            }
            this.Hide(); // FormSepet'i gizle
        }


        private void button1_Click(object sender, EventArgs e)
        {
            richTextBox.Clear();
        }

      
    }
    }
