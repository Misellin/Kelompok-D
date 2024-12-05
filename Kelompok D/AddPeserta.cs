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
    public partial class AddPeserta : Form
    {
        public AddPeserta()
        {
            InitializeComponent();
        }

        private void btnSimpan_Click(object sender, EventArgs e)
        {
            try
            {
                // Ambil data dari form
                string nim = txtNIM.Text;
                string nama = txtNama.Text;
                string tanggal = dtpTanggal.Value.ToString("yyyy-MM-dd"); // Format tanggal menjadi yyyy-MM-dd
                string password = lblPassword.Text;

                // Validasi data (opsional)
                if (string.IsNullOrEmpty(nim) || string.IsNullOrEmpty(nama) || string.IsNullOrEmpty(password))
                {
                    MessageBox.Show("Harap isi semua field!");
                    return;
                }

                // Query SQL untuk insert data
                string query = "INSERT INTO Users (Username, Nama, Tanggal, Password) VALUES (@NIM, @Nama, @Tanggal, @Password)";

                // Eksekusi query dengan Dapper
                using (IDbConnection conn = new SQLiteConnection(Connection.ConnectionString))
                {
                    conn.Open();
                    conn.Execute(query, new { Username = nim, Nama = nama, Tanggal = tanggal, Password = password });
                }

                MessageBox.Show("Data berhasil disimpan!");

                // Clear form (opsional)
                txtNIM.Clear();
                txtNama.Clear();
                lblPassword.Text = string.Empty;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }

        }

        private void btnGeneratePass_Click(object sender, EventArgs e)
        {
            // Generate password acak
            string password = GenerateRandomPassword();

            // Tampilkan password di label
            lblPassword.Text = password;
        }

        // Fungsi untuk menggenerate password acak
        private string GenerateRandomPassword(int length = 8)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

    }
}
