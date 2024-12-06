using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlTypes;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kelompok_D
{
    public partial class KonfirmasiPage : Form
    {
        public KonfirmasiPage()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ExamPage exam = new ExamPage();
            exam.Show();
            this.Close();
        }
    }
}
