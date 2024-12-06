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
using static Kelompok_D.ExamPage;


namespace Kelompok_D
{
    public partial class FinishedForm : Form
    {
        private ExamPage examPage; // Variabel untuk menyimpan instance ExamPage

        public FinishedForm(ExamPage examPage)
        {
            InitializeComponent();
            this.examPage = examPage;

        }

        private void btnNotFinished_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnFinished_Click(object sender, EventArgs e)
        {
            examPage.Close();
            try
            {
                // Hentikan timer dari ExamPage
                examPage.examTimer.Stop();

                // Hitung nilai
                ExamResult result = ExamResultChecker.CheckResult(examPage.daftarSoal, examPage.jawabanUser);

                // Hitung jumlah jawaban benar dan salah
                int jumlahBenar = 0;
                int jumlahSalah = 0;
                for (int i = 0; i < examPage.daftarSoal.Count; i++)
                {
                    string jawabanBenar = examPage.daftarSoal[i].Jawaban.Trim();
                    string jawabanUserTrim = examPage.jawabanUser[i].TrimEnd(';');

                    // Normalisasi jawaban
                    string jawabanBenarNormalized = NormalizeJawaban(jawabanBenar);
                    string jawabanUserNormalized = NormalizeJawaban(jawabanUserTrim);

                    if (jawabanBenarNormalized == jawabanUserNormalized)
                    {
                        jumlahBenar++;
                    }
                    else
                    {
                        jumlahSalah++;
                    }
                }

                // Ambil lama ujian dari ExamPage (asumsikan ada properti timeElapsed di ExamPage)
                TimeSpan timeElapsed = examPage.timeElapsed;
                string lamaUjian = $"{timeElapsed.Hours:D2}:{timeElapsed.Minutes:D2}:{timeElapsed.Seconds:D2}";
                int userID = Home.currentUserID;
                // Simpan data ke database laporan
                using (IDbConnection conn = new SQLiteConnection(Connection.ConnectionString))
                {
                    conn.Open();

                    // Query untuk insert data ke laporan
                    string query = @"
                        INSERT INTO laporan (UserID, Benar, Salah, Nilai, Status, LamaUjian) 
                        VALUES (@UserID, @Benar, @Salah, @Nilai, @Status, @LamaUjian)";

                    // Eksekusi query dengan Dapper
                    conn.Execute(query, new
                    {
                        UserID = userID, // Ganti dengan cara Anda mendapatkan UserID
                        Benar = jumlahBenar,
                        Salah = jumlahSalah,
                        Nilai = result.Nilai,
                        Status = result.Lulus ? "Pass" : "Fail",
                        LamaUjian = lamaUjian
                    });
                }

                // Buka ScorePage
                ScorePage scorePage = new ScorePage(result);
                scorePage.Show();

                // Tutup form
                //this.Close();
                //examPage.Close(); // Tutup ExamPage
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
        private static string NormalizeJawaban(string jawaban)
        {
            if (string.IsNullOrEmpty(jawaban))
            {
                return "";
            }

            jawaban = jawaban.Trim().ToLower();
            string[] pilihan = jawaban.Split(';');
            Array.Sort(pilihan);
            return string.Join(";", pilihan);
        }


    }
}
