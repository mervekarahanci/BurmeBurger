using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Burme.Burger
{
    public partial class FormRegister : Form
    {
        private static string connectionString = "Data Source=Burme.Burger.db;Version=3;";

        public FormRegister()
        {
            InitializeComponent();
        }


        private void btn_giris2_Click(object sender, EventArgs e)
        {
            this.Hide(); // Bu formu gizler.
            FormLogin form_login = new FormLogin();
            form_login.Show(); // LoginForm'u gösterir.

        }

        private void btn_kayit2_Click(object sender, EventArgs e)
        {
            // Kullanıcıdan değerleri al
            string fullName = txt_kisi.Text.Trim();
            string phone = msktxt_phone2.Text.Trim();
            string password = txt_sifre2.Text;

            // Gerekli alanların dolu olup olmadığını kontrol et
            if (string.IsNullOrEmpty(fullName) || string.IsNullOrEmpty(phone) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Lütfen tüm alanları doldurun!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (SqliteDataAccess.RegisterUser(fullName, phone, password))
            {
                MessageBox.Show("Kayıt başarılı! Lütfen giriş yapınız.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Hide();
                FormLogin loginForm = new FormLogin();
                loginForm.Show();
            }
            else
            {
                MessageBox.Show("Bu telefon numarası zaten kayıtlı!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            // Kullanıcıyı Login Formuna yönlendir
            this.Hide();
            FormLogin formLogin = new FormLogin();
            formLogin.Show();
        }
    }
    }
