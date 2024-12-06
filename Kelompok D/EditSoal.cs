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
    public partial class EditSoal : Form
    {
        private sooal soal;
        public EditSoal(sooal soal)
        {
            InitializeComponent();
            this.soal = soal;

            // Isi kontrol dengan data soal
            txtTopik.Text = soal.Topik;
            txtSoal.Text = soal.Soal;
            string[] pilihan = soal.Pilihan.Split(';');
            txtPilihanA.Text = pilihan[0];
            txtPilihanB.Text = pilihan[1];
            txtPilihanC.Text = pilihan[2];
            txtPilihanD.Text = pilihan[3];
            string[] jawabanBenar = soal.Jawaban.Split(';');

            // Centang checkbox jawaban yang benar
            foreach (string jawaban in jawabanBenar)
            {
                // Cari checkbox yang teksnya sama dengan jawaban
                if (txtPilihanA.Text == jawaban) chkA.Checked = true;
                if (txtPilihanB.Text == jawaban) chkB.Checked = true;
                if (txtPilihanC.Text == jawaban) chkC.Checked = true;
                if (txtPilihanD.Text == jawaban) chkD.Checked = true;
            }


        }

        private void EditSoal_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                // 1. Ambil data dari form
                string topikLama = soal.Topik; // Simpan topik lama
                string soalLama = soal.Soal; // Simpan soal lama

                soal.Topik = txtTopik.Text;
                soal.Soal = txtSoal.Text;
                soal.Pilihan = $"{txtPilihanA.Text};{txtPilihanB.Text};{txtPilihanC.Text};{txtPilihanD.Text}";

                // Ambil jawaban yang dicentang di CheckBox
                string jawaban = "";
                if (chkA.Checked) jawaban += txtPilihanA.Text + ";";
                if (chkB.Checked) jawaban += txtPilihanB.Text + ";";
                if (chkC.Checked) jawaban += txtPilihanC.Text + ";";
                if (chkD.Checked) jawaban += txtPilihanD.Text + ";";

                // Hapus ; di akhir jawaban (jika ada)
                soal.Jawaban = jawaban.TrimEnd(';');

                // 2. Validasi data (opsional)
                if (string.IsNullOrEmpty(soal.Soal) || string.IsNullOrEmpty(soal.Pilihan) || string.IsNullOrEmpty(soal.Jawaban))
                {
                    MessageBox.Show("Harap isi semua field!");
                    return;
                }

                // 3. Query SQL untuk update data
                string query = @"
                    UPDATE BankSoal 
                    SET Topik = @Topik, Soal = @Soal, Pilihan = @Pilihan, Tipe = @Tipe, Jawaban = @Jawaban 
                    WHERE Topik = @TopikLama AND Soal = @SoalLama"; // Asumsi Topik dan Soal adalah primary key

                // 4. Eksekusi query dengan Dapper
                using (IDbConnection conn = new SQLiteConnection(Connection.ConnectionString))
                {
                    conn.Open();
                    conn.Execute(query, new
                    {
                        soal.Topik,
                        soal.Soal,
                        soal.Pilihan,
                        soal.Tipe,
                        soal.Jawaban,
                        TopikLama = topikLama,
                        SoalLama = soalLama
                    });
                }

                MessageBox.Show("Soal berhasil diupdate!");

                // 5. Tutup form
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
