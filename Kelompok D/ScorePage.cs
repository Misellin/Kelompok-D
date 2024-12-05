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
    public partial class ScorePage : Form
    {
        public ScorePage(ExamResult result)
        {
            InitializeComponent();
            lblResult.Text = result.Lulus ? "Pass" : "Fail";
            lblScore.Text = result.Nilai.ToString();

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            Home home = new Home();
            home.Show();
        }
    }
}
