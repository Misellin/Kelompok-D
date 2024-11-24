namespace Kelompok_D
{
    partial class User_Page
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtNim = new System.Windows.Forms.TextBox();
            this.btnLogin = new System.Windows.Forms.Button();
            this.lblUserLogin = new System.Windows.Forms.Label();
            this.txtPass = new System.Windows.Forms.TextBox();
            this.cboShowPass = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtNim
            // 
            this.txtNim.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNim.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNim.ForeColor = System.Drawing.Color.DimGray;
            this.txtNim.Location = new System.Drawing.Point(30, 29);
            this.txtNim.Multiline = true;
            this.txtNim.Name = "txtNim";
            this.txtNim.ShortcutsEnabled = false;
            this.txtNim.Size = new System.Drawing.Size(370, 38);
            this.txtNim.TabIndex = 1;
            this.txtNim.Text = "Nim";
            this.txtNim.UseWaitCursor = true;
            // 
            // btnLogin
            // 
            this.btnLogin.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLogin.Location = new System.Drawing.Point(122, 156);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(167, 35);
            this.btnLogin.TabIndex = 3;
            this.btnLogin.Text = "Login";
            this.btnLogin.UseVisualStyleBackColor = true;
            // 
            // lblUserLogin
            // 
            this.lblUserLogin.AutoSize = true;
            this.lblUserLogin.Font = new System.Drawing.Font("Arial", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUserLogin.Location = new System.Drawing.Point(185, 9);
            this.lblUserLogin.Name = "lblUserLogin";
            this.lblUserLogin.Size = new System.Drawing.Size(83, 32);
            this.lblUserLogin.TabIndex = 0;
            this.lblUserLogin.Text = "Login";
            // 
            // txtPass
            // 
            this.txtPass.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPass.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPass.ForeColor = System.Drawing.Color.DimGray;
            this.txtPass.Location = new System.Drawing.Point(30, 75);
            this.txtPass.Multiline = true;
            this.txtPass.Name = "txtPass";
            this.txtPass.ShortcutsEnabled = false;
            this.txtPass.Size = new System.Drawing.Size(370, 38);
            this.txtPass.TabIndex = 4;
            this.txtPass.Text = "Password";
            this.txtPass.UseWaitCursor = true;
            // 
            // cboShowPass
            // 
            this.cboShowPass.AutoSize = true;
            this.cboShowPass.Location = new System.Drawing.Point(30, 119);
            this.cboShowPass.Name = "cboShowPass";
            this.cboShowPass.Size = new System.Drawing.Size(125, 20);
            this.cboShowPass.TabIndex = 5;
            this.cboShowPass.Text = "Show Password";
            this.cboShowPass.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.cboShowPass);
            this.panel1.Controls.Add(this.txtPass);
            this.panel1.Controls.Add(this.btnLogin);
            this.panel1.Controls.Add(this.txtNim);
            this.panel1.Location = new System.Drawing.Point(11, 53);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(429, 217);
            this.panel1.TabIndex = 6;
            // 
            // User_Page
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(452, 280);
            this.ControlBox = false;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lblUserLogin);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "User_Page";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "User Page";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox txtNim;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.Label lblUserLogin;
        private System.Windows.Forms.TextBox txtPass;
        private System.Windows.Forms.CheckBox cboShowPass;
        private System.Windows.Forms.Panel panel1;
    }
}