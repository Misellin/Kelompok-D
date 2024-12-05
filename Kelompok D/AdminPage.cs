using Dapper;
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

namespace Kelompok_D
{
    public partial class AdminPage : Form
    {
        public AdminPage()
        {
            InitializeComponent();
            LoadData();
        }
        private void LoadData()
        {
            try
            {
                using (IDbConnection conn = new SQLiteConnection(Connection.ConnectionString))
                {
                    conn.Open();

                    // Sesuaikan query SQL dengan tabel Users
                    string query = "SELECT * FROM Users " +
                                   "WHERE Role = 'peserta'" +
                                   "ORDER BY Username";

                    // Jalankan query dengan Dapper
                    var users = conn.Query<users>(query, new
                    {
                        NIM = txtFilterNIM.Text.Trim() + "%", // Sesuaikan dengan nama textbox filter NIM
                        Nama = txtFilterNama.Text.Trim() + "%" // Sesuaikan dengan nama textbox filter Nama
                        // ... (parameter filter lainnya)
                    }).AsList();

                    // Tampilkan data di DataGridView
                    dgvData.AutoGenerateColumns = false;
                    dgvData.DataSource = users; // users adalah list objek User

                    dgvData.Columns[0].DataPropertyName = "Username";
                    dgvData.Columns[1].DataPropertyName = "Nama";
                    dgvData.Columns[2].DataPropertyName = "Tanggal";
                    dgvData.Columns[3].DataPropertyName = "Password";

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnTambah_Click(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void btnDataPeserta_Click(object sender, EventArgs e)
        {
            pnlDataPeserta.Visible = true;
            pnlDataSoal.Visible = false;
        }

        private void btnDataSoal_Click(object sender, EventArgs e)
        {
            pnlDataPeserta.Visible = false;
            pnlDataSoal.Visible = true;
        }

        private void btnTambah_Click_1(object sender, EventArgs e)
        {
            AddPeserta addPeserta = new AddPeserta();
            addPeserta.ShowDialog();
        }
    }
}
