﻿namespace Kelompok_D
{
    partial class AddPeserta
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
            this.dtpTanggal = new System.Windows.Forms.DateTimePicker();
            this.label6 = new System.Windows.Forms.Label();
            this.btnGeneratePass = new System.Windows.Forms.Button();
            this.txtNama = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtNIM = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnSimpan = new System.Windows.Forms.Button();
            this.lblPassword = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dtpTanggal
            // 
            this.dtpTanggal.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.dtpTanggal.Location = new System.Drawing.Point(117, 93);
            this.dtpTanggal.Name = "dtpTanggal";
            this.dtpTanggal.Size = new System.Drawing.Size(345, 22);
            this.dtpTanggal.TabIndex = 42;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(36, 95);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(68, 20);
            this.label6.TabIndex = 41;
            this.label6.Text = "Tanggal";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnGeneratePass
            // 
            this.btnGeneratePass.Location = new System.Drawing.Point(357, 124);
            this.btnGeneratePass.Name = "btnGeneratePass";
            this.btnGeneratePass.Size = new System.Drawing.Size(105, 31);
            this.btnGeneratePass.TabIndex = 40;
            this.btnGeneratePass.Text = "Generate";
            this.btnGeneratePass.UseVisualStyleBackColor = true;
            this.btnGeneratePass.Click += new System.EventHandler(this.btnGeneratePass_Click);
            // 
            // txtNama
            // 
            this.txtNama.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNama.Location = new System.Drawing.Point(117, 57);
            this.txtNama.Multiline = true;
            this.txtNama.Name = "txtNama";
            this.txtNama.Size = new System.Drawing.Size(345, 25);
            this.txtNama.TabIndex = 38;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(21, 131);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(83, 20);
            this.label7.TabIndex = 37;
            this.label7.Text = "Password";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtNIM
            // 
            this.txtNIM.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNIM.Location = new System.Drawing.Point(117, 21);
            this.txtNIM.Multiline = true;
            this.txtNIM.Name = "txtNIM";
            this.txtNIM.Size = new System.Drawing.Size(345, 25);
            this.txtNIM.TabIndex = 36;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(51, 59);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 20);
            this.label5.TabIndex = 35;
            this.label5.Text = "Nama";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(65, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(39, 20);
            this.label2.TabIndex = 34;
            this.label2.Text = "NIM";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnSimpan
            // 
            this.btnSimpan.Location = new System.Drawing.Point(191, 172);
            this.btnSimpan.Name = "btnSimpan";
            this.btnSimpan.Size = new System.Drawing.Size(93, 30);
            this.btnSimpan.TabIndex = 43;
            this.btnSimpan.Text = "Simpan";
            this.btnSimpan.UseVisualStyleBackColor = true;
            this.btnSimpan.Click += new System.EventHandler(this.btnSimpan_Click);
            // 
            // lblPassword
            // 
            this.lblPassword.Location = new System.Drawing.Point(7, 3);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(142, 20);
            this.lblPassword.TabIndex = 44;
            this.lblPassword.Text = "label1";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Window;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.lblPassword);
            this.panel1.Location = new System.Drawing.Point(117, 127);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(226, 24);
            this.panel1.TabIndex = 45;
            // 
            // AddPeserta
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(487, 219);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnSimpan);
            this.Controls.Add(this.dtpTanggal);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.btnGeneratePass);
            this.Controls.Add(this.txtNama);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtNIM);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddPeserta";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AddPeserta";
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dtpTanggal;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnGeneratePass;
        private System.Windows.Forms.TextBox txtNama;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtNIM;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnSimpan;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.Panel panel1;
    }
}