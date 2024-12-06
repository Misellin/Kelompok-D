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

            if (user.Gender == "Pria")
            {
                rbPria.Checked = true;
            }
            else
            {
                rbWanita.Checked = true;
            }
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
                string jenisKelamin = rbPria.Checked ? "Pria" : "Wanita";

                // 2. Validasi data
                if (string.IsNullOrEmpty(nim) || string.IsNullOrEmpty(nama) || string.IsNullOrEmpty(password))
                {
                    MessageBox.Show("Harap isi semua field!");
                    return;
                }

                // 3. Pengecekan NIM (pastikan NIM unik)
                using (IDbConnection conn = new SQLiteConnection(Connection.ConnectionString))
                {
                    conn.Open();
                    string checkQuery = "SELECT COUNT(*) FROM Users WHERE Username = @NIM AND UserID != @UserID"; // Perhatikan NIM = @NIM
                    int count = conn.QuerySingle<int>(checkQuery, new { NIM = nim, UserID = user.UserID });
                    if (count > 0)
                    {
                        MessageBox.Show("NIM sudah terdaftar!");
                        return;
                    }
                }


                // 4. Buat objek users untuk update data
                users updatedUser = new users()
                {
                    UserID = user.UserID, // ID user yang akan diupdate
                    Username = nim,
                    Nama = nama,
                    Tanggal = tanggal,
                    Password = password,
                    Gender = jenisKelamin
                };

                // 5. Eksekusi query UPDATE dengan prepared statement
                using (SQLiteConnection conn = new SQLiteConnection(Connection.ConnectionString))
                {
                    conn.Open();
                    using (SQLiteTransaction trans = conn.BeginTransaction())
                    {
                        // Query SQL untuk update data
                        string query = "UPDATE Users SET Username = @Username, Nama = @Nama, Tanggal = @Tanggal, Password = @Password, Gender = @Gender WHERE UserID = @UserID";

                        conn.Execute(query, updatedUser, trans);
                        trans.Commit();
                    }
                }

                MessageBox.Show("Data berhasil diupdate!");

                // 6. Set DialogResult dan tutup form
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
