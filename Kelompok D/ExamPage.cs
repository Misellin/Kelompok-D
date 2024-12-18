﻿using System;
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
        public List<sooal> daftarSoal;
        private int currentSoalIndex;
        public Timer examTimer;
        public TimeSpan timeElapsed { get; private set; }
        private int timeLeft;
        public List<string> jawabanUser;
        private List<bool> soalDiragukan;
        private Button[] buttons;

        public ExamPage()
        {
            InitializeComponent();
            LoadSoal();
            label14.Text = Home.currentUserID.ToString();
            currentSoalIndex = 0;
            timeLeft = 60 * 60;

            jawabanUser = new List<string>();
            soalDiragukan = new List<bool>();
            for (int i = 0; i < daftarSoal.Count; i++)
            {
                jawabanUser.Add("");
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
            btnPrevious.Visible = false;
            StartTimer();
            DisplaySoal();
            UpdateQuestionList(); 
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
            sooal soal; 
            soal = daftarSoal[currentSoalIndex];
            string tipeSoal = soal.Tipe == 1 ? "Single Choice" : "Multiple Choice";
            label11.Text = $"Question Type: {tipeSoal}, Answers: {soal.Jawaban}";

            if (currentSoalIndex >= 0 && currentSoalIndex < daftarSoal.Count)
            {
                lblQuestion.Text = soal.Soal;
                string[] pilihan = soal.Pilihan.Split(';');

                cbAns1.Checked = false;
                cbAns2.Checked = false;
                cbAns3.Checked = false;
                cbAns4.Checked = false;

                cbAns1.Visible = false;
                cbAns2.Visible = false;
                cbAns3.Visible = false;
                cbAns4.Visible = false;
                Random rnd = new Random();
                pilihan = pilihan.OrderBy(x => rnd.Next()).ToArray();

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

                lblSoalCount.Text = $"{currentSoalIndex + 1}";


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

            ExamResult result = ExamResultChecker.CheckResult(daftarSoal, jawabanUser);
            label10.Text = "Nilai: " + result.Nilai.ToString();
            for (int i = 0; i < daftarSoal.Count; i++)
            {
                if (buttons != null && i >= 0 && i < buttons.Length && buttons[i] != null)
                {
                    Button btnSoal = buttons[i];

                    // Tentukan status jawaban dan atur warna button
                    if (soalDiragukan[i])
                    {
                        btnSoal.BackColor = Color.Orange;
                    }
                    else if (!string.IsNullOrEmpty(jawabanUser[i]))
                    {
                        btnSoal.BackColor = Color.Green;
                    }
                    else
                    {
                        btnSoal.BackColor = SystemColors.Control;
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
                    if (!jawabanUser[currentSoalIndex].Contains(cbAns1.Text))
                    {
                        jawabanUser[currentSoalIndex] += cbAns1.Text + ";";
                    }
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
            if (currentSoalIndex >= 0 && currentSoalIndex < jawabanUser.Count)
            {
                if (cbAns2.Checked)
                {
                    if (!jawabanUser[currentSoalIndex].Contains(cbAns2.Text))
                    {
                        jawabanUser[currentSoalIndex] += cbAns2.Text + ";";
                    }
                }
                else
                {
                    jawabanUser[currentSoalIndex] = jawabanUser[currentSoalIndex].Replace(cbAns2.Text + ";", "");
                }
                UpdateQuestionList();
            }
        }

        private void cbAns3_CheckedChanged(object sender, EventArgs e)
        {
            if (currentSoalIndex >= 0 && currentSoalIndex < jawabanUser.Count)
            {
                if (cbAns3.Checked)
                {
                    if (!jawabanUser[currentSoalIndex].Contains(cbAns3.Text))
                    {
                        jawabanUser[currentSoalIndex] += cbAns3.Text + ";";
                    }
                }
                else
                {
                    jawabanUser[currentSoalIndex] = jawabanUser[currentSoalIndex].Replace(cbAns3.Text + ";", "");
                }
                UpdateQuestionList();
            }
        }

        private void cbAns4_CheckedChanged(object sender, EventArgs e)
        {
            if (currentSoalIndex >= 0 && currentSoalIndex < jawabanUser.Count)
            {
                if (cbAns4.Checked)
                {
                    if (!jawabanUser[currentSoalIndex].Contains(cbAns4.Text))
                    {
                        jawabanUser[currentSoalIndex] += cbAns4.Text + ";";
                    }
                }
                else
                {
                    jawabanUser[currentSoalIndex] = jawabanUser[currentSoalIndex].Replace(cbAns4.Text + ";", "");
                }
                UpdateQuestionList();
            }
        }
        private void BtnSoal_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int soalIndex = int.Parse(btn.Text) - 1;
            currentSoalIndex = soalIndex;
            DisplaySoal();
            if (currentSoalIndex == daftarSoal.Count - 1)
            {
                btnNext.Visible = false;
                btnPrevious.Visible = true;
            }
            else if (currentSoalIndex == 0)
            {
                btnNext.Visible = true;
                btnPrevious.Visible = false;
            }
            else {
                btnNext.Visible = true;
                btnPrevious.Visible = true;
            }
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            if (currentSoalIndex > 0)
            {
                currentSoalIndex--;
                DisplaySoal();
            }
            if (currentSoalIndex == daftarSoal.Count - 1)
            {
                btnNext.Visible = false;
                btnPrevious.Visible = true;
            }
            else if (currentSoalIndex == 0)
            {
                btnNext.Visible = true;
                btnPrevious.Visible = false;
            }
            else
            {
                btnNext.Visible = true;
                btnPrevious.Visible = true;
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (currentSoalIndex < daftarSoal.Count - 1)
            {
                currentSoalIndex++;
                DisplaySoal();
            }
            if (currentSoalIndex == daftarSoal.Count - 1)
            {
                btnNext.Visible = false;
                btnPrevious.Visible = true;
            }
            else if (currentSoalIndex == 0)
            {
                btnNext.Visible = true;
                btnPrevious.Visible = false;
            }
            else
            {
                btnNext.Visible = true;
                btnPrevious.Visible = true;
            }
        }


        private void btnDoubt_Click(object sender, EventArgs e)
        {
            if (currentSoalIndex >= 0 && currentSoalIndex < soalDiragukan.Count)
            {
                soalDiragukan[currentSoalIndex] = !soalDiragukan[currentSoalIndex];
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
                timeElapsed += TimeSpan.FromSeconds(1); 
                UpdateTimerLabel();

            }
            else
            {
                examTimer.Stop();
                MessageBox.Show("Waktu habis!");
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
                List<string> jawabanBenarList = soal.Jawaban.Split(';').ToList();

                List<string> jawabanUserList = jawabanUser[i].TrimEnd(';').Split(';').ToList();

                // Normalisasi dan urutkan jawaban
                jawabanBenarList = NormalizeJawaban(jawabanBenarList);
                jawabanUserList = NormalizeJawaban(jawabanUserList);

                // Bandingkan jawaban yang sudah dinormalisasi dan diurutkan
                if (jawabanBenarList.SequenceEqual(jawabanUserList))
                {
                    jumlahBenar++;
                }
            }

            int nilai = jumlahBenar * 5;
            bool lulus = nilai >= 70;

            return new ExamResult(nilai, lulus);
        }

        public static List<string> NormalizeJawaban(List<string> jawaban)
        {
            if (jawaban == null || jawaban.Count == 0)
            {
                return new List<string>();
            }

            jawaban = jawaban.Select(j => j.Trim().ToLower()).ToList();

            jawaban.Sort();

            return jawaban;
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

        private void button1_Click(object sender, EventArgs e)
        {
            examTimer.Stop();
        }

        private void pnlQuestionsList_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lblSoalCount_Click(object sender, EventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (soalDiragukan.Any(d => d)) 
            {
                MessageBox.Show("Masih ada soal yang di-doubt. Silakan periksa kembali.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; 
            }
            else
            {
                if (timeLeft > 0)
                {
                    FinishedForm konfirmasiForm = new FinishedForm(this); 
                    konfirmasiForm.ShowDialog();

                }
                else
                {
                    examTimer.Stop();
                    ExamResult result = ExamResultChecker.CheckResult(daftarSoal, jawabanUser);

                    ScorePage scorePage = new ScorePage(result); 
                    scorePage.Show();
                }
            }
        }
    }

}
