using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Burme.Burger
{
    public partial class FormLogin : Form
    {
        private static string connectionString = "Data Source=Burme.Burger.db;Version=3;";



        public FormLogin()
        {
            InitializeComponent();
        }

        private void btn_kayit_Click(object sender, EventArgs e)
        {
            this.Hide(); // LoginForm'u gizler.
            FormRegister form_register = new FormRegister();
            form_register.Show(); // RegisterForm'u açar.

        }

    

        private void btn_giris_Click(object sender, EventArgs e)
        {
            string phoneNumber = msktxt_phone.Text.Trim();
            string password = txt_sifre.Text.Trim();

            if (string.IsNullOrEmpty(phoneNumber) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Lütfen telefon numarası ve şifrenizi girin.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SQLiteConnection conn = new SQLiteConnection("Data Source=Burme.Burger.db;Version=3;"))
            {
                conn.Open();
                string query = "SELECT UserID FROM Users WHERE Phone = @Phone AND Password = @Password";

                using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Phone", phoneNumber);
                    cmd.Parameters.AddWithValue("@Password", password);

                    object result = cmd.ExecuteScalar();

                    if (result != null)
                    {
                        GlobalData.LoggedInUserID = Convert.ToInt32(result); // UserID'yi kaydet
                        MessageBox.Show("Giriş başarılı!", "Başarı", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // FormMain'e geçiş
                        this.Hide();
                        FormMain formMain = new FormMain();
                        formMain.Show();
                    }
                    else
                    {
                        MessageBox.Show("Telefon numarası veya şifre hatalı.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void FormLogin_Load(object sender, EventArgs e)
        {

        }
    }
}
