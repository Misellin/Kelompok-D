﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kelompok_D
{
    public partial class ParInformationPage : Form
    {
        public ParInformationPage()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ExamPage exam = new ExamPage();
            exam.Show();
        }
    }
}
