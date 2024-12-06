using Dapper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;
using System.Security.Cryptography;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Windows.Forms.VisualStyles;

namespace Kelompok_D
{
    public partial class Home : Form
    {
        public static int currentUserID;
        public Home()
        {
            InitializeComponent();
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            pnlHomepage.Visible = true;
            pnlLogin.Visible = false;
        }

        public void btnLoginpage_Click(object sender, EventArgs e)
        {
            pnlHomepage.Visible = false;
            pnlLogin.Visible = true;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string nim = txtNIM.Text;
            string password = txtPassword.Text;

            if (ValidateInput(nim, password))
            {

                using (IDbConnection conn = new SQLiteConnection(Connection.ConnectionString))
                {
                    conn.Open();

                    var user = conn.QueryFirstOrDefault<users>(
                        "SELECT * FROM users WHERE Username = @Username AND password = @Password",
                        new { Username = nim, Password = password }
                    );

                    if (user != null)
                    {
                        MessageBox.Show("Login berhasil!");
                        currentUserID = user.UserID;

                        if (user.Role == "admin")
                        {
                            AdminPage adminPage = new AdminPage();
                            adminPage.Show();
                            //this.Hide();
                        }
                        else if (user.Role == "peserta")
                        {

                            ParInformationPage parInformationPage = new ParInformationPage();
                            parInformationPage.Show();
                            //this.Hide();
                        }
                    }
                    else
                    {
                        MessageBox.Show("NIM atau password salah!");
                    }
                }
                txtNIM.Clear();
                txtPassword.Clear();
                this.Hide();
            }
        }
        private bool ValidateInput(string nim, string password)
        {
            // Implementasi validasi input di sini
            // ... (Contoh validasi seperti pada jawaban sebelumnya)

            return true;
        }

        private void btnOut_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void txtNIM_TextChanged(object sender, EventArgs e)
        {

        }

        private void cbShowPass_CheckedChanged_1(object sender, EventArgs e)
        {
            if (cbShowPass.Checked)
            {
                txtPassword.PasswordChar = '\0';
            }
            else
            {
                txtPassword.PasswordChar = '•';
            }
        }

        private void txtNIM_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
            if (char.IsNumber(e.KeyChar) || e.KeyChar == (char)Keys.Back) e.Handled = false;
        }
    }
}
