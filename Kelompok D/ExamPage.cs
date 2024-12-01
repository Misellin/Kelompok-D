using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Dapper;
using System.Data.SQLite;

namespace Kelompok_D
{
    public partial class ExamPage : Form
    {
        private List<sooal> daftarSoal;
        private int currentSoalIndex;
        private Timer examTimer;
        private int timeLeft;
        private List<string> jawabanUser; // List untuk menyimpan jawaban user
        private List<bool> soalDiragukan; // List untuk menyimpan status "doubt"
        private Button[] buttons;

        public ExamPage()
        {
            InitializeComponent();
            LoadSoal();
            currentSoalIndex = 0;
            timeLeft = 60 * 60; // 60 menit

            jawabanUser = new List<string>();
            soalDiragukan = new List<bool>();
            for (int i = 0; i < daftarSoal.Count; i++)
            {
                jawabanUser.Add(""); // Isi dengan string kosong
                soalDiragukan.Add(false);
            }

            buttons = new Button[20];
            for (int i = 1; i <= 20; i++)
            {
                string buttonName = "btnNo" + i;
                Control[] controls = this.Controls.Find(buttonName, true);
                if (controls.Length > 0 && controls[0] is Button button)
                {
                    buttons[i - 1] = button;

                }
            }

            StartTimer();
            DisplaySoal();
            UpdateQuestionList(); // Panggil UpdateQuestionList di sini
        }

        private void LoadSoal()
        {
            try
            {
                using (IDbConnection conn = new SQLiteConnection(Connection.ConnectionString))
                {
                    conn.Open();
                    daftarSoal = conn.Query<sooal>(
                        "SELECT * FROM banksoal ORDER BY RANDOM() LIMIT 20"
                    ).AsList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void DisplaySoal()
        {
            if (currentSoalIndex >= 0 && currentSoalIndex < daftarSoal.Count)
            {
                sooal soal = daftarSoal[currentSoalIndex];
                lblQuestion.Text = soal.Soal;
                string[] pilihan = soal.Pilihan.Split(';');

                // Clear pilihan jawaban sebelumnya
                cbAns1.Checked = false;
                cbAns2.Checked = false;
                cbAns3.Checked = false;
                cbAns4.Checked = false;

                // Sembunyikan semua checkbox terlebih dahulu
                cbAns1.Visible = false;
                cbAns2.Visible = false;
                cbAns3.Visible = false;
                cbAns4.Visible = false;

                // Tampilkan pilihan jawaban
                for (int i = 0; i < pilihan.Length; i++)
                {
                    pilihan[i] = pilihan[i].Trim();
                    switch (i)
                    {
                        case 0:
                            cbAns1.Text = pilihan[i];
                            cbAns1.Visible = true;
                            break;
                        case 1:
                            cbAns2.Text = pilihan[i];
                            cbAns2.Visible = true;
                            break;
                        case 2:
                            cbAns3.Text = pilihan[i];
                            cbAns3.Visible = true;
                            break;
                        case 3:
                            cbAns4.Text = pilihan[i];
                            cbAns4.Visible = true;
                            break;
                    }
                }

                lblSoalCount.Text = $"Question {currentSoalIndex + 1}";


                if (jawabanUser != null && currentSoalIndex >= 0 && currentSoalIndex < jawabanUser.Count)
                {
                    string jawabanSoal = jawabanUser[currentSoalIndex];
                    if (!string.IsNullOrEmpty(jawabanSoal))
                    {
                        string[] pilihanJawaban = jawabanSoal.Split(';');
                        foreach (string piliha in pilihanJawaban)
                        {
                            if (!string.IsNullOrEmpty(piliha.Trim()))
                            {
                                if (cbAns1.Text == piliha.Trim()) cbAns1.Checked = true;
                                if (cbAns2.Text == piliha.Trim()) cbAns2.Checked = true;
                                if (cbAns3.Text == piliha.Trim()) cbAns3.Checked = true;
                                if (cbAns4.Text == piliha.Trim()) cbAns4.Checked = true;
                            }
                        }
                    }
                }


            }
        }

        private void UpdateQuestionList()
        {
            for (int i = 0; i < daftarSoal.Count; i++)
            {
                if (buttons != null && i >= 0 && i < buttons.Length && buttons[i] != null)
                {
                    Button btnSoal = buttons[i];

                    // Tentukan status jawaban dan atur warna button
                    if (soalDiragukan[i])
                    {
                        btnSoal.BackColor = Color.Orange; // Soal diragukan
                    }
                    else if (!string.IsNullOrEmpty(jawabanUser[i]))
                    {
                        btnSoal.BackColor = Color.Green; // Soal sudah dijawab
                    }
                    else
                    {
                        btnSoal.BackColor = Color.Red; // Soal belum dijawab
                    }
                    
                }
            }
        }


        private void cbAns1_CheckedChanged(object sender, EventArgs e)
        {
            if (currentSoalIndex >= 0 && currentSoalIndex < jawabanUser.Count)
            {
                if (cbAns1.Checked)
                {
                    jawabanUser[currentSoalIndex] += cbAns1.Text + ";";
                }
                else
                {
                    jawabanUser[currentSoalIndex] = jawabanUser[currentSoalIndex].Replace(cbAns1.Text + ";", "");
                }
                UpdateQuestionList();
            }
        }


        private void cbAns2_CheckedChanged(object sender, EventArgs e)
        {
            if (cbAns2.Checked)
            {
                jawabanUser[currentSoalIndex] += cbAns2.Text + ";"; // Tambahkan jawaban ke list
            }
            else
            {
                jawabanUser[currentSoalIndex] = jawabanUser[currentSoalIndex].Replace(cbAns2.Text + ";", ""); // Hapus jawaban dari list
            }

            UpdateQuestionList(); // Update tampilan pnlQuestionsList
        }
        private void cbAns3_CheckedChanged(object sender, EventArgs e)
        {
            if (cbAns3.Checked)
            {
                jawabanUser[currentSoalIndex] += cbAns3.Text + ";"; // Tambahkan jawaban ke list
            }
            else
            {
                jawabanUser[currentSoalIndex] = jawabanUser[currentSoalIndex].Replace(cbAns3.Text + ";", ""); // Hapus jawaban dari list
            }

            UpdateQuestionList(); // Update tampilan pnlQuestionsList
        }

        private void cbAns4_CheckedChanged(object sender, EventArgs e)
        {
            if (cbAns4.Checked)
            {
                jawabanUser[currentSoalIndex] += cbAns4.Text + ";"; // Tambahkan jawaban ke list
            }
            else
            {
                jawabanUser[currentSoalIndex] = jawabanUser[currentSoalIndex].Replace(cbAns4.Text + ";", ""); // Hapus jawaban dari list
            }

            UpdateQuestionList(); // Update tampilan pnlQuestionsList
        }
        private void BtnSoal_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int soalIndex = int.Parse(btn.Text) - 1;
            currentSoalIndex = soalIndex;
            DisplaySoal();
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            if (currentSoalIndex > 0)
            {
                currentSoalIndex--;
                DisplaySoal();
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (currentSoalIndex < daftarSoal.Count - 1)
            {
                currentSoalIndex++;
                DisplaySoal();

                // Ubah teks btnNext menjadi "Submit" jika sudah soal terakhir
                if (currentSoalIndex == daftarSoal.Count - 1)
                {
                    btnNext.Text = "Submit";
                }
            }
            else
            {
                // Soal terakhir, submit jawaban
                examTimer.Stop(); // Hentikan timer

                ExamResult result = ExamResultChecker.CheckResult(daftarSoal, jawabanUser);
                MessageBox.Show($"Nilai Anda: {result.Nilai}\nStatus: {(result.Lulus ? "Lulus" : "Gagal")}");

                // ... (logika untuk mengakhiri ujian atau menampilkan form hasil)
            }
        }


        private void btnDoubt_Click(object sender, EventArgs e)
        {
            if (currentSoalIndex >= 0 && currentSoalIndex < soalDiragukan.Count)
            {
                soalDiragukan[currentSoalIndex] = !soalDiragukan[currentSoalIndex]; // Toggle status "doubt"
                UpdateQuestionList();
            }
        }

        private void StartTimer()
        {
            examTimer = new Timer();
            examTimer.Interval = 1000; // 1 detik
            examTimer.Tick += ExamTimer_Tick;
            examTimer.Start();
        }

        private void ExamTimer_Tick(object sender, EventArgs e)
        {
            if (timeLeft > 0)
            {
                timeLeft--;
                UpdateTimerLabel();
            }
            else
            {
                examTimer.Stop();
                MessageBox.Show("Waktu habis!");
                // Akhiri ujian, submit jawaban, dll.
            }
        }

        private void UpdateTimerLabel()
        {
            int minutes = timeLeft / 60;
            int seconds = timeLeft % 60;
            lblTimer.Text = $"{minutes:D2}:{seconds:D2}";
        }

        public class ExamResultChecker
        {
            public static ExamResult CheckResult(List<sooal> daftarSoal, List<string> jawabanUser)
            {
                int totalSoal = daftarSoal.Count;
                int jumlahBenar = 0;

                for (int i = 0; i < totalSoal; i++)
                {
                    sooal soal = daftarSoal[i];
                    string jawabanBenar = soal.Jawaban;
                    string jawabanUserTrim = jawabanUser[i].TrimEnd(';'); // Hapus titik koma di akhir

                    // Membandingkan jawaban user dengan jawaban benar
                    if (jawabanBenar == jawabanUserTrim)
                    {
                        jumlahBenar++;
                    }
                }

                int nilai = jumlahBenar * 5; // Setiap soal bernilai 5 poin
                bool lulus = nilai >= 70; // Minimal nilai kelulusan 70

                return new ExamResult(nilai, lulus);
            }
        }

        public class ExamResult
        {
            public int Nilai { get; }
            public bool Lulus { get; }

            public ExamResult(int nilai, bool lulus)
            {
                Nilai = nilai;
                Lulus = lulus;
            }
        }

    }

}
