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
    public partial class EditPeserta : Form
    {
        private users user;

        public EditPeserta(users user)
        {
            InitializeComponent();
            this.user = user;

            // Isi kontrol-kontrol dengan data user
            txtNIM.Text = user.Username;
            txtNama.Text = user.Nama;
            dtpTanggal.Value = DateTime.Parse(user.Tanggal);
            lblPassword.Text = user.Password;
        }

        private void btnSimpan_Click(object sender, EventArgs e)
        {
            try
            {
                // 1. Ambil data dari form
                string nim = txtNIM.Text.Trim();
                string nama = txtNama.Text.Trim();
                string tanggal = dtpTanggal.Value.ToString("yyyy-MM-dd");
                string password = lblPassword.Text; // Ambil password dari label

                // 2. Validasi data
                if (string.IsNullOrEmpty(nim) || string.IsNullOrEmpty(nama) || string.IsNullOrEmpty(password))
                {
                    MessageBox.Show("Harap isi semua field!");
                    return;
                }

                // 3. Query SQL untuk update data
                string query = "UPDATE Users SET Username = @NIM, Nama = @Nama, Tanggal = @Tanggal, Password = @Password WHERE UserID = @UserID";

                // 4. Eksekusi query dengan Dapper
                using (IDbConnection conn = new SQLiteConnection(Connection.ConnectionString))
                {
                    conn.Open();
                    conn.Execute(query, new
                    {
                        NIM = nim,
                        Nama = nama,
                        Tanggal = tanggal,
                        Password = password,
                        UserID = user.UserID // Gunakan UserID dari objek user yang diterima di konstruktor
                    });
                }

                MessageBox.Show("Data berhasil diupdate!");

                // 5. Set DialogResult dan tutup form
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void btnGeneratePass_Click(object sender, EventArgs e)
        {
            // Generate password acak
            string password = AddPeserta.GenerateRandomPassword();

            // Tampilkan password di label
            lblPassword.Text = password;
        }

        private void txtNIM_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
            if (char.IsNumber(e.KeyChar) || e.KeyChar == (char)Keys.Back) e.Handled = false;
        }
    }
}
