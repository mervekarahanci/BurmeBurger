using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Burme.Burger
{
    public partial class FormMain : Form
    {
        private static string connectionString = "Data Source=Burme.Burger.db;Version=3;";

        private List<(string Name, int Quantity, decimal Price)> sepetListesi = new List<(string, int, decimal)>();
        private decimal toplamTutar = 0; // Toplam tutar

        public FormMain()
        {
            InitializeComponent();
        }

        private string activeCategory = ""; // Aktif kategori ("menu", "citir", "tatli", "sos")

        private string[] messages = {
    "OSMAN'I DA BİZ DOYURDUK!!!",
    "Bugün Alışveriş Yap, Yarın Kazan!",
    "2 Alana 1 Bedava Fırsatını Kaçırma!",
    "1022'den Beri Değişmeyen Lezzet"
};
        private int currentIndex = 0;

        private void timerMarquee_Tick(object sender, EventArgs e)
        {
         
            lblMarquee.Left -= 5;
            if (lblMarquee.Right < 0)
            {
                lblMarquee.Left = this.panelMarquee.Width;
                currentIndex = (currentIndex + 1) % messages.Length;
                lblMarquee.Text = messages[currentIndex];
            }

        }

        private void timer_lbl_Tick(object sender, EventArgs e)
        {
            label11.Left -= 5;
            if (label11.Right < 0)
            {
                label11.Left = this.transparentRoundedPanel11.Width;
            }

            label12.Left -= 5;
            if (label12.Right < 0)
            {
                label12.Left = this.transparentRoundedPanel12.Width;
            }

            label13.Left -= 5;
            if (label13.Right < 0)
            {
                label13.Left = this.transparentRoundedPanel13.Width;
            }

            label14.Left -= 5;
            if (label14.Right < 0)
            {
                label14.Left = this.transparentRoundedPanel14.Width;
            }
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            transparentRoundedPanel11.Visible = false;
            transparentRoundedPanel12.Visible = false;
            transparentRoundedPanel13.Visible = false;
            transparentRoundedPanel14.Visible = false;
            dolufoto.Visible = false;
            dolufoto2.Visible = false;
            dolufoto3.Visible = false;
            bosfoto.Visible = true;
            bosfoto2.Visible = true;


            pictureBox11.Size = new Size(200, 200); // Boyut
                pictureBox11.SizeMode = PictureBoxSizeMode.StretchImage; // Görüntüleme modu
                pictureBox12.Size = new Size(200, 200); // Boyut
                pictureBox12.SizeMode = PictureBoxSizeMode.StretchImage; // Görüntüleme modu
                pictureBox13.Size = new Size(200, 200); // Boyut
                pictureBox13.SizeMode = PictureBoxSizeMode.StretchImage; // Görüntüleme modu
                pictureBox14.Size = new Size(200, 200); // Boyut
                pictureBox14.SizeMode = PictureBoxSizeMode.StretchImage; // Görüntüleme modu
            
            timer_lbl.Start();
            timerMarquee.Start();
            // Label'ı başlangıçta sağdan başlat
            lblMarquee.Left = this.panelMarquee.Width;
            lblMarquee.Text = messages[currentIndex]; // İlk mesajı ayarla
        }

        // Resimleri projenin Resources kısmına eklediğinizden emin olun.

        private void btn_menu_Click(object sender, EventArgs e)
        {
            bosfoto.Visible = false;
            bosfoto2.Visible = false;
            dolufoto.Visible=true;
            dolufoto2.Visible = true;
            dolufoto3.Visible = true;
            transparentRoundedPanel11.Visible = true;
            transparentRoundedPanel12.Visible = true;
            transparentRoundedPanel13.Visible = true;
            transparentRoundedPanel14.Visible = true;

            activeCategory = "menu"; // Aktif kategori Menüler
            pictureBox11.Image?.Dispose();
            pictureBox11.Image = Properties.Resources.malazgirtsofrasi;
            pictureBox11.Refresh();

            pictureBox12.Image?.Dispose();
            pictureBox12.Image = Properties.Resources.lezzetihurriyet;
            pictureBox12.Refresh();

            pictureBox13.Image?.Dispose();
            pictureBox13.Image = Properties.Resources.ciftekilic;
            pictureBox13.Refresh();

            pictureBox14.Image?.Dispose();
            pictureBox14.Image = Properties.Resources.metropolitan;
            pictureBox14.Refresh();

            label11.Text = "Malazgirt Sofrası";
            label12.Text = "Lezzet-i Hürriyet";
            label13.Text = "Çifte Kılıç";
            label14.Text = "Metropolitan";

        }


        private void btn_citir_Click_1(object sender, EventArgs e)
        {
            bosfoto.Visible = false;
            bosfoto2.Visible = false;
            dolufoto.Visible = true;
            dolufoto2.Visible = true;
            dolufoto3.Visible = true;
            transparentRoundedPanel11.Visible = true;
            transparentRoundedPanel12.Visible = true;
            transparentRoundedPanel13.Visible = true;
            transparentRoundedPanel14.Visible = true;

            activeCategory = "citir"; // Aktif kategori Çıtır Lezzetler
            pictureBox11.Image?.Dispose();
            pictureBox11.Image = Properties.Resources.gurmenugget;
            pictureBox11.Refresh();

            pictureBox12.Image?.Dispose();
            pictureBox12.Image = Properties.Resources.sogankizartma;
            pictureBox12.Refresh();

            pictureBox13.Image?.Dispose();
            pictureBox13.Image = Properties.Resources.hataysoslucitirtavuk;
            pictureBox13.Refresh();

            pictureBox14.Image?.Dispose();
            pictureBox14.Image = Properties.Resources.citirhellim;
            pictureBox14.Refresh();

            label11.Text = "Gurme Nugget";
            label12.Text = "Soğan Kızartma";
            label13.Text = "Hatay Soslu Çıtır Tavuk";
            label14.Text = "Çıtır Hellim";


        }

        private void btn_sos_Click_1(object sender, EventArgs e)
        {
            bosfoto.Visible = false;
            bosfoto2.Visible = false;
            dolufoto.Visible = true;
            dolufoto2.Visible = true;
            dolufoto3.Visible = true;
            transparentRoundedPanel11.Visible = true;
            transparentRoundedPanel12.Visible = true;
            transparentRoundedPanel13.Visible = true;
            transparentRoundedPanel14.Visible = true;

            activeCategory = "sos"; // Aktif kategori Soslar
            pictureBox11.Image?.Dispose();
            pictureBox11.Image = Properties.Resources.ketçap__2___1_;
            pictureBox11.Refresh();

            pictureBox12.Image?.Dispose();
            pictureBox12.Image = Properties.Resources.sarımsaklıyeni;
            pictureBox12.Refresh();

            pictureBox13.Image?.Dispose();
            pictureBox13.Image = Properties.Resources.mandayeni;
            pictureBox13.Refresh();

            pictureBox14.Image?.Dispose();
            pictureBox14.Image = Properties.Resources.mangalyeni;
            pictureBox14.Refresh();

            label11.Text = "Aşk Acısı";
            label12.Text = "Sarımsaklı Mayo";
            label13.Text = "Manda";
            label14.Text = "Mangal";  

        }

        private void btn_tatli_Click(object sender, EventArgs e)
        {
            bosfoto.Visible = false;
            bosfoto2.Visible = false;
            dolufoto.Visible = true;
            dolufoto2.Visible = true;
            dolufoto3.Visible = true;
            transparentRoundedPanel11.Visible = true;
            transparentRoundedPanel12.Visible = true;
            transparentRoundedPanel13.Visible = true;
            transparentRoundedPanel14.Visible = true;

            activeCategory = "tatli"; // Aktif kategori Soslar
            pictureBox11.Image?.Dispose();
            pictureBox11.Image = Properties.Resources.saray_sarmasi_yatay;
            pictureBox11.Refresh();

            pictureBox12.Image?.Dispose();
            pictureBox12.Image = Properties.Resources.kemalpasa;
            pictureBox12.Refresh();

            pictureBox13.Image?.Dispose();
            pictureBox13.Image = Properties.Resources.antep;
            pictureBox13.Refresh();

            pictureBox14.Image?.Dispose();
            pictureBox14.Image = Properties.Resources.keskul;
            pictureBox14.Refresh();

            label11.Text = "Saray Sarması";
            label12.Text = "Kemalpaşa";
            label13.Text = "Fıstıklı Sarma";
            label14.Text = "Keşkül-ü Fukara";
        }

        private void pictureBox_11_Click(object sender, EventArgs e)
        {
            AddToCart(label11.Text); // Ürün ismini sepete ekle
        }

        private void pictureBox_12_Click(object sender, EventArgs e)
        {
            AddToCart(label12.Text);
        }

        private void pictureBox_13_Click(object sender, EventArgs e)
        {
            AddToCart(label13.Text);
        }

        private void pictureBox_14_Click(object sender, EventArgs e)
        {
            AddToCart(label14.Text);
        }

    

        private void AddToCart(string productName)
        {
            if (string.IsNullOrEmpty(activeCategory))
            {
                MessageBox.Show("Lütfen bir kategori seçin!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            decimal productPrice = GetProductPrice(productName);

            // Sepette ürün var mı kontrol et
            var existingItem = sepetListesi.FirstOrDefault(item => item.Name == productName);

            if (existingItem != default)
            {
                // Ürün varsa miktarı artır
                sepetListesi.Remove(existingItem);
                sepetListesi.Add((productName, existingItem.Quantity + 1, productPrice));
            }
            else
            {
                // Ürün yoksa ekle
                sepetListesi.Add((productName, 1, productPrice));
            }

            toplamTutar += productPrice;

            // Global sepete güncelle
            GlobalData.SepetListesi = sepetListesi;
            GlobalData.ToplamTutar = toplamTutar;

            MessageBox.Show($"{productName} ürünü sepete eklendi! Fiyat: {productPrice:C2}", "Sepet Bilgisi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void UpdateCart(List<(string Name, int Quantity, decimal Price)> updatedList, decimal updatedTotal)
        {
            sepetListesi = updatedList;
            toplamTutar = updatedTotal;
        }



        // Ürün fiyatlarını belirleyen metod
        private decimal GetProductPrice(string productName)
        {
            // Örnek ürün fiyatları
            switch (productName)
            {
                // Menüler
                case "Malazgirt Sofrası": return 230.00m;
                case "Lezzet-i Hürriyet": return 270.00m;
                case "Çifte Kılıç": return 220.00m;
                case "Metropolitan": return 250.00m;

                // Çıtır Lezzetler
                case "Gurme Nugget": return 120.00m;
                case "Soğan Kızartma": return 100.00m;
                case "Hatay Soslu Çıtır Tavuk": return 130.00m;
                case "Çıtır Hellim": return 110.00m;

                // Soslar
                case "Aşk Acısı": return 15.00m;
                case "Sarımsaklı Mayo": return 15.00m;
                case "Manda": return 15.00m;
                case "Mangal": return 15.00m;

                case "Saray Sarması": return 150.00m;
                case "Kemalpaşa": return 120.00m;
                case "Fıstıklı Sarma": return 250.00m;
                case "Keşkül-ü Fukara": return 210.00m;

                default: return 0.00m; // Tanımsız ürün
            }
        }

        private void btn_sepet_Click(object sender, EventArgs e)
        {
            this.Hide();
            FormSepet form_sepet = new FormSepet(sepetListesi, toplamTutar);
            form_sepet.ShowDialog();
        }

        private void btn_hesap_Click(object sender, EventArgs e)
        {
            this.Hide(); // LoginForm'u gizler.
            FormHesap form_hesap = new FormHesap();
            form_hesap.Show(); // RegisterForm'u açar.
        }

       

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            GlobalData.Reset();
            this.Hide(); // Mevcut formu gizle
            FormLogin form_login = new FormLogin();
            form_login.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide(); // LoginForm'u gizler.
            FormGrafik form_grafik = new FormGrafik();
            form_grafik.Show(); // RegisterForm'u açar.
        }
    }
}