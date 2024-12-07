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
    public partial class AddSoal : Form
    {

        public AddSoal()
        {
            InitializeComponent();
        }

        private void AddSoal_Load(object sender, EventArgs e)
        {

        }

        private void btnSimpan_Click(object sender, EventArgs e)
        {
            try
            {
                // 1. Ambil data dari form
                string topik = txtTopik.Text; 
                string soal = txtSoal.Text;

                string pilihan = string.Join(";", new string[] {
                    txtPilihanA.Text,
                    txtPilihanB.Text,
                    txtPilihanC.Text,
                    txtPilihanD.Text
                });

                // Hitung jumlah checkbox yang aktif
                int jumlahJawaban = 0;
                if (chkA.Checked) jumlahJawaban++;
                if (chkB.Checked) jumlahJawaban++;
                if (chkC.Checked) jumlahJawaban++;
                if (chkD.Checked) jumlahJawaban++;

                // Tentukan tipe soal berdasarkan jumlah jawaban
                int tipe = jumlahJawaban > 1 ? 2 : 1;

                // Ambil jawaban yang dicentang di CheckBox dan gabungkan dengan ;
                string jawaban = "";
                if (chkA.Checked) jawaban += txtPilihanA.Text + ";";
                if (chkB.Checked) jawaban += txtPilihanB.Text + ";";
                if (chkC.Checked) jawaban += txtPilihanC.Text + ";";
                if (chkD.Checked) jawaban += txtPilihanD.Text + ";";

                // Hapus ; di akhir jawaban (jika ada)
                jawaban = jawaban.TrimEnd(';');


                // 2. Validasi data
                if (string.IsNullOrEmpty(soal) || string.IsNullOrEmpty(pilihan) || string.IsNullOrEmpty(jawaban))
                {
                    MessageBox.Show("Harap isi semua field!");
                    return;
                }

                // 3. Query SQL untuk insert data
                string query = "INSERT INTO BankSoal (Topik, Soal, Pilihan, Tipe, Jawaban) VALUES (@Topik, @Soal, @Pilihan, @Tipe, @Jawaban)";

                // 4. Eksekusi query dengan Dapper
                using (IDbConnection conn = new SQLiteConnection(Connection.ConnectionString))
                {
                    conn.Open();
                    conn.Execute(query, new { Topik = topik, Soal = soal, Pilihan = pilihan, Tipe = tipe, Jawaban = jawaban });
                }

                MessageBox.Show("Soal berhasil ditambahkan!");

                // 5. Clear form 
                this.DialogResult = DialogResult.OK;
                this.Close();


            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

    }
}
