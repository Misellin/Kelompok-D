using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
            // Hentikan timer dari ExamPage
            examPage.examTimer.Stop();

            // Hitung nilai
            ExamResult result = ExamResultChecker.CheckResult(examPage.daftarSoal, examPage.jawabanUser);

            // Buka ScorePage
            ScorePage scorePage = new ScorePage(result);
            scorePage.Show();


        }
    }
}
