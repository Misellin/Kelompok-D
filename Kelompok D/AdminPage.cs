using Dapper;
using Guna.UI2.WinForms.Suite;
using Microsoft.VisualBasic.ApplicationServices;
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

            LoadDataPeserta(); 

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

            //DataSoal
            typeof(DataGridView).InvokeMember(
                "DoubleBuffered",
                BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty,
                null,
                dgvSoal,
                new object[] { true });
            dgvSoal.AutoGenerateColumns = false;
            dgvSoal.Columns[0].DataPropertyName = nameof(banksoal.soal);
            dgvSoal.Columns[1].DataPropertyName = nameof(banksoal.pilihan);
            dgvSoal.Columns[2].DataPropertyName = nameof(banksoal.tipe);
            dgvSoal.Columns[3].DataPropertyName = nameof(banksoal.jawaban);

            LoadDataSoal("");


        }

        //DataPeserta
        private void LoadDataPeserta()
        {
            try
            {
                using (IDbConnection conn = new SQLiteConnection(Connection.ConnectionString))
                {
                    conn.Open();

                    // Query dasar
                    string query = "SELECT * FROM Users WHERE Role = 'peserta' ";

                    // Tambahkan filter NIM dan Nama
                    query += "AND Username LIKE @Username AND Nama LIKE @Nama ";

                    // Tambahkan filter Gender jika diperlukan
                    if (chkPria.Checked && !chkWanita.Checked)
                    {
                        query += "AND Gender = 'Pria' ";
                    }
                    else if (!chkPria.Checked && chkWanita.Checked)
                    {
                        query += "AND Gender = 'Wanita' ";
                    }

                    query += "ORDER BY Username ASC";

                    // Parameter query
                    var parameters = new
                    {
                        Username = txtFilterNIM.Text.Trim() + "%",
                        Nama = txtFilterNama.Text.Trim() + "%"
                    };

                    // Jalankan query dengan Dapper
                    var users = conn.Query<users>(query, parameters).AsList();

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
            DialogResult result = MessageBox.Show("Apakah Anda yakin ingin logout?", "Konfirmasi Logout", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Home homePage = new Home();
                homePage.Show();
                homePage.btnLoginpage_Click(null, EventArgs.Empty);
                this.Close();
            }                
        }


        private void btnTambah_Click(object sender, EventArgs e)
        {
            AddPeserta addPeserta = new AddPeserta();
            if (addPeserta.ShowDialog() == DialogResult.OK)
            {
                LoadDataPeserta();
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
                        WHERE 1=1 ";

                    // Tambahkan filter NIM
                    if (!string.IsNullOrEmpty(filterNIM))
                    {
                        query += "AND u.Username LIKE @NIM ";
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
            string status = "";
            if (chkPassed.Checked && chkFailed.Checked)
            {
                status = "";
            }
            else if (chkPassed.Checked)
            {
                status = "Pass";
            }
            else if (chkFailed.Checked)
            {
                status = "Fail";
            }

            // Panggil LoadDataLaporan dengan parameter filter
            LoadDataLaporan(nim, nama, status);
        }

        //Data Soal
        private void LoadDataSoal(string topik)
        {
            try
            {
                using (IDbConnection conn = new SQLiteConnection(Connection.ConnectionString))
                {
                    conn.Open();

                    // Query untuk mengambil data soal berdasarkan topik
                    string query = "SELECT * FROM BankSoal WHERE Topik = @Topik";

                    // Jalankan query dengan Dapper
                    var soal = conn.Query<sooal>(query, new { Topik = topik }).AsList();

                    // Tampilkan data di DataGridView
                    dgvSoal.DataSource = soal;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
        private void cboTopik_SelectedIndexChanged(object sender, EventArgs e)
        {
            string topik = cboTopik.SelectedItem.ToString();
            LoadDataSoal(topik);
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

        private void button1_Click(object sender, EventArgs e)
        {
            AddSoal addSoal = new AddSoal();
            addSoal.ShowDialog();
            if (addSoal.ShowDialog() == DialogResult.OK)
            {
                LoadDataSoal(cboTopik.SelectedItem.ToString());
            }
        }

        private void AdminPage_Load(object sender, EventArgs e)
        {
            try
            {
                using (IDbConnection conn = new SQLiteConnection(Connection.ConnectionString))
                {
                    conn.Open();

                    // Query untuk mengambil semua topik unik dari BankSoal
                    string query = "SELECT DISTINCT Topik FROM BankSoal";

                    // Jalankan query dengan Dapper
                    var topikList = conn.Query<string>(query).ToList();

                    // Tambahkan data ke ComboBox
                    cboTopik.DataSource = topikList;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }

        }
        private sooal soal;
        private void button2_Click(object sender, EventArgs e)
        {
            if (dgvSoal.CurrentRow != null)
            {
                // Ambil data soal yang dipilih
                sooal soal = (sooal)dgvSoal.CurrentRow.DataBoundItem;

                // Buka EditSoal dan kirim data soal
                EditSoal editSoal = new EditSoal(soal);
                if (editSoal.ShowDialog() == DialogResult.OK)
                {
                    LoadDataSoal(cboTopik.SelectedItem.ToString());
                }

            }
            else
            {
                MessageBox.Show("Pilih soal yang ingin diedit.");
            }
        }

        private void radFailed_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radPassed_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
