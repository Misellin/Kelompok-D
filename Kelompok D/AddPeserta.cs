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
            // Validasi input
            if (string.IsNullOrEmpty(txtNIM.Text) || string.IsNullOrEmpty(txtNama.Text) || string.IsNullOrEmpty(lblPassword.Text))
            {
                MessageBox.Show("Semua kolom harus diisi!");
                return;
            }

            // Query SQL untuk insert data
            users user = new users()
            {
                Username = txtNIM.Text.Trim(),
                Nama = txtNama.Text.Trim(),
                Tanggal = dtpTanggal.Value.ToString("yyyy-MM-dd"),
                Password = lblPassword.Text, // Ambil password dari label
            };

            int recCount = 0;

            using (SQLiteConnection conn = new SQLiteConnection(Connection.ConnectionString))
            {
                conn.Open();
                using (SQLiteTransaction trans = conn.BeginTransaction())
                {
                    // Eksekusi query INSERT dengan prepared statement
                    recCount = conn.Execute("INSERT INTO Users (Username, Nama, Tanggal, Password) VALUES (@Username, @Nama, @Tanggal, @Password)", user, trans);
                    trans.Commit();
                }
            }
            if (recCount > 0)
            {
                MessageBox.Show("Data berhasil disimpan!");
                // Clear form (opsional)
                txtNIM.Clear();
                txtNama.Clear();
                lblPassword.Text = ""; // Clear label password
                DialogResult = DialogResult.OK;

            }
            else
            {
                MessageBox.Show("Gagal menyimpan data!");
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
        public static string GenerateRandomPassword(int length = 8)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        private void txtNIM_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
            if (char.IsNumber(e.KeyChar) || e.KeyChar == (char)Keys.Back) e.Handled = false;
        }
    }
}
