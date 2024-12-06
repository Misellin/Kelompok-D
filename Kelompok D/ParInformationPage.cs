using Dapper;
using Microsoft.VisualBasic.ApplicationServices;
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
using System.Xml.Linq;

namespace Kelompok_D
{
    public partial class ParInformationPage : Form
    {
        public ParInformationPage()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            KonfirmasiPage kon = new KonfirmasiPage();

            kon.Show();
            this.Close();
        }

        private void ParInformationPage_Load(object sender, EventArgs e)
        {
            try
            {
                // 1. Ambil UserID yang sedang login
                int userID = Home.currentUserID; // Ganti dengan cara Anda mendapatkan UserID

                // 2. Query database untuk mengambil data user
                using (IDbConnection conn = new SQLiteConnection(Connection.ConnectionString))
                {
                    conn.Open();
                    string query = "SELECT Username, Nama, Tanggal FROM Users WHERE UserID = @UserID";
                    var user = conn.QueryFirstOrDefault<users>(query, new { UserID = userID });

                    // 3. Tampilkan data di kontrol
                    if (user != null)
                    {
                        txtNIM.Text = user.Username;
                        txtName.Text = user.Nama;
                        txtExamDate.Text = user.Tanggal;
                    }
                    else
                    {
                        MessageBox.Show("Data user tidak ditemukan.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }

        }
    }
}
