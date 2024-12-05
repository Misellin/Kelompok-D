using Dapper;
using Guna.UI2.WinForms.Suite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kelompok_D
{
    public partial class AdminPage : Form
    {
        useControl.uC_DataSoal DataSoal = new useControl.uC_DataSoal();
        public AdminPage()
        {
            InitializeComponent();
            //pnlDataPeserta.Location = new Point(213, 128);
            //pnlDataSoal.Location = new Point(213, 128);
            //pnlDataSoal.Visible = false;
            //pnlLaporan.Visible = false;
            //Data Peserta
            typeof(DataGridView).InvokeMember(
                "DoubleBuffered",
                BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty,
                null,
                dgvUsers,
                new object[] { true });
            dgvUsers.AutoGenerateColumns = false;
            dgvUsers.Columns[0].DataPropertyName = nameof(users.Username);
            dgvUsers.Columns[1].DataPropertyName = nameof(users.Nama);
            dgvUsers.Columns[2].DataPropertyName = nameof(users.Gender);
            dgvUsers.Columns[3].DataPropertyName = nameof(users.Tanggal);
            dgvUsers.Columns[4].DataPropertyName = nameof(users.Password);

            LoadDataPeserta(); // Panggil LoadData saat form dibuka

            //Laporan
            typeof(DataGridView).InvokeMember(
                "DoubleBuffered",
                BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty,
                null,
                dgvLaporan,
                new object[] { true });
            dgvLaporan.AutoGenerateColumns = false;
            dgvLaporan.Columns[0].DataPropertyName = nameof(users.Username);
            dgvLaporan.Columns[1].DataPropertyName = nameof(users.Nama);
            dgvLaporan.Columns[2].DataPropertyName = nameof(users.Tanggal);
            dgvLaporan.Columns[3].DataPropertyName = nameof(laporan.Benar);
            dgvLaporan.Columns[4].DataPropertyName = nameof(laporan.Salah);
            dgvLaporan.Columns[5].DataPropertyName = nameof(laporan.Nilai);
            dgvLaporan.Columns[6].DataPropertyName = nameof(laporan.Status);
            dgvLaporan.Columns[7].DataPropertyName = nameof(laporan.LamaUjian);
            LoadDataLaporan("", "", "");


        }
        //DataPeserta
        private void LoadDataPeserta()
        {
            try
            {
                using (IDbConnection conn = new SQLiteConnection(Connection.ConnectionString))
                {
                    conn.Open();

                    // Query untuk mengambil data User dengan filter
                    string query = "SELECT * FROM Users " +
                                   "WHERE Role = 'peserta' " +
                                   "AND Username LIKE @Username " +
                                   "AND Nama LIKE @Nama " +
                                   //"AND Tanggal = @Tanggal " + // Tambahkan filter tanggal
                                   "ORDER BY Username ASC";

                    // Jalankan query dengan Dapper
                    var users = conn.Query<users>(query, new
                    {
                        Username = txtFilterNIM.Text.Trim() + "%",
                        Nama = txtFilterNama.Text.Trim() + "%",
                        Tanggal = dtpFilterTanggal.Value.ToString("yyyy-MM-dd") // Ambil nilai dari DateTimePicker
                    }).AsList();

                    // Tampilkan data di DataGridView
                    dgvUsers.DataSource = users;
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
            AddPeserta addPeserta = new AddPeserta();
            if (addPeserta.ShowDialog() == DialogResult.OK) // Cek apakah form ditutup dengan OK
            {
                LoadDataPeserta(); // Refresh DataGridView setelah tambah data
            }
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            LoadDataPeserta();
        }

        private void btnHapus_Click(object sender, EventArgs e)
        {
            if (dgvUsers.CurrentRow != null)
            {
                // Ambil data user yang dipilih
                users user = (users)dgvUsers.CurrentRow.DataBoundItem;

                // Konfirmasi penghapusan
                DialogResult result = MessageBox.Show(
                    $"Apakah Anda yakin ingin menghapus user {user.Nama}?",
                    "Konfirmasi Hapus",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    try
                    {
                        using (IDbConnection conn = new SQLiteConnection(Connection.ConnectionString))
                        {
                            conn.Open();

                            // Query untuk menghapus data
                            string query = "DELETE FROM Users WHERE UserID = @UserID";

                            // Eksekusi query dengan Dapper
                            conn.Execute(query, new { UserID = user.UserID });
                        }

                        LoadDataPeserta(); // Refresh DataGridView
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }
            }
            else
            {
                MessageBox.Show("Pilih user yang ingin dihapus.");
            }

        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvUsers.CurrentRow != null)
            {
                // Ambil data user yang dipilih
                users user = (users)dgvUsers.CurrentRow.DataBoundItem;

                // Buka EditPeserta dan kirim data user
                EditPeserta editPeserta = new EditPeserta(user);
                if (editPeserta.ShowDialog() == DialogResult.OK)
                {
                    LoadDataPeserta(); // Refresh DataGridView
                }
            }
            else
            {
                MessageBox.Show("Pilih user yang ingin diedit.");
            }
        }

        private void dtpFilterTanggal_ValueChanged(object sender, EventArgs e)
        {
            dtpFilterTanggal.Format = DateTimePickerFormat.Custom;
            dtpFilterTanggal.CustomFormat = "yyyy-MM-dd";
        }


        //DataSoal
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        //Laporan
        private void LoadDataLaporan(string filterNIM, string filterNama, string filterStatus)
        {
            try
            {
                using (IDbConnection conn = new SQLiteConnection(Connection.ConnectionString))
                {
                    conn.Open();

                    // Query untuk mengambil data laporan dengan filter
                    string query = @"
                SELECT 
                    u.Username, 
                    u.Nama, 
                    u.Tanggal,
                    l.Benar, 
                    l.Salah, 
                    l.Nilai, 
                    l.Status, 
                    l.LamaUjian
                FROM 
                    laporan l
                INNER JOIN 
                    Users u ON l.UserID = u.UserID
                WHERE 1=1 "; // Trik agar mudah menambahkan filter

                    // Tambahkan filter NIM
                    if (!string.IsNullOrEmpty(filterNIM))
                    {
                        query += "AND u.Username     LIKE @NIM ";
                    }

                    // Tambahkan filter Nama
                    if (!string.IsNullOrEmpty(filterNama))
                    {
                        query += "AND u.Nama LIKE @Nama ";
                    }

                    // Tambahkan filter Status
                    if (!string.IsNullOrEmpty(filterStatus))
                    {
                        query += "AND l.Status = @Status ";
                    }

                    // Parameter query
                    var parameters = new
                    {
                        NIM = filterNIM + "%",
                        Nama = filterNama + "%",
                        Status = filterStatus
                    };

                    // Jalankan query dengan Dapper
                    var laporan = conn.Query<laporan>(query, parameters).AsList();

                    // Tampilkan data di DataGridView
                    dgvLaporan.DataSource = laporan;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void btnFilterLaporan_Click(object sender, EventArgs e)
        {
            // Ambil nilai filter dari kontrol
            string nim = txtFilterNIM.Text.Trim();
            string nama = txtFilterNama.Text.Trim();
            string status = radPassed.Checked ? "Pass" : (radFailed.Checked ? "Fail" : ""); // Ternary operator

            // Panggil LoadDataLaporan dengan parameter filter
            LoadDataLaporan(nim, nama, status);
        }

        //BtnNav
        private void btnDataPeserta_Click(object sender, EventArgs e)
        {
            pnlDataPeserta.Visible = true;
            pnlDataSoal.Visible = false;
            pnlLaporan.Visible = false;
        }

        private void btnDataSoal_Click(object sender, EventArgs e)
        {
            pnlDataPeserta.Visible = false;
            pnlDataSoal.Visible = true;
            pnlLaporan.Visible = false;
        }
        private void btnLaporan_Click(object sender, EventArgs e)
        {
            pnlDataPeserta.Visible = false;
            pnlDataSoal.Visible = false;
            pnlLaporan.Visible = true;
        }

        private void txtFilterNIM_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtFilterNIM_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
            if (char.IsNumber(e.KeyChar) || e.KeyChar == (char)Keys.Back) e.Handled = false;
        }
    }
}
