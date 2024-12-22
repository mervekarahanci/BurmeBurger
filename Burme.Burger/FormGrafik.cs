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
using System.Windows.Forms.DataVisualization.Charting;

namespace Burme.Burger
{
    public partial class FormGrafik : Form
    {
        public FormGrafik()
        {
            InitializeComponent();
        }

        private void FormGrafik_Load(object sender, EventArgs e)
        {
            string connectionString = "Data Source=Burme.Burger.db;Version=3;";
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT ProductName, SUM(Quantity) AS TotalQuantity FROM OrderDetails GROUP BY ProductName ORDER BY TotalQuantity DESC LIMIT 5";

                SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, connection);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                chart1.Series.Clear();
                Series series = new Series("Ürünler")
                {
                    ChartType = SeriesChartType.Column
                };

                foreach (DataRow row in dataTable.Rows)
                {
                    series.Points.AddXY(row["ProductName"].ToString(), Convert.ToInt32(row["TotalQuantity"]));
                }

                chart1.Series.Add(series);
                chart1.Titles.Add("En Çok Sipariş Edilen Ürünler");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide(); // LoginForm'u gizler.
            FormMain form_main = new FormMain();
            form_main.Show(); // RegisterForm'u açar.
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }
    }
}
