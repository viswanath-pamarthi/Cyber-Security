﻿namespace Viswanath_Pamarthi_Project1
{
    partial class Form1
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
            this.GrpBoxOperation = new System.Windows.Forms.GroupBox();
            this.RadioBtnDecrypt = new System.Windows.Forms.RadioButton();
            this.RadioBtnEncrypt = new System.Windows.Forms.RadioButton();
            this.btnInputFile = new System.Windows.Forms.Button();
            this.lblFilePath = new System.Windows.Forms.Label();
            this.btnRun = new System.Windows.Forms.Button();
            this.GrpBoxOperation.SuspendLayout();
            this.SuspendLayout();
            // 
            // GrpBoxOperation
            // 
            this.GrpBoxOperation.Controls.Add(this.RadioBtnDecrypt);
            this.GrpBoxOperation.Controls.Add(this.RadioBtnEncrypt);
            this.GrpBoxOperation.Location = new System.Drawing.Point(20, 106);
            this.GrpBoxOperation.Name = "GrpBoxOperation";
            this.GrpBoxOperation.Size = new System.Drawing.Size(148, 75);
            this.GrpBoxOperation.TabIndex = 0;
            this.GrpBoxOperation.TabStop = false;
            this.GrpBoxOperation.Text = "Operation";
            // 
            // RadioBtnDecrypt
            // 
            this.RadioBtnDecrypt.AutoSize = true;
            this.RadioBtnDecrypt.Location = new System.Drawing.Point(17, 48);
            this.RadioBtnDecrypt.Name = "RadioBtnDecrypt";
            this.RadioBtnDecrypt.Size = new System.Drawing.Size(78, 21);
            this.RadioBtnDecrypt.TabIndex = 1;
            this.RadioBtnDecrypt.TabStop = true;
            this.RadioBtnDecrypt.Text = "Decrypt";
            this.RadioBtnDecrypt.UseVisualStyleBackColor = true;
            // 
            // RadioBtnEncrypt
            // 
            this.RadioBtnEncrypt.AutoSize = true;
            this.RadioBtnEncrypt.Location = new System.Drawing.Point(17, 22);
            this.RadioBtnEncrypt.Name = "RadioBtnEncrypt";
            this.RadioBtnEncrypt.Size = new System.Drawing.Size(77, 21);
            this.RadioBtnEncrypt.TabIndex = 0;
            this.RadioBtnEncrypt.TabStop = true;
            this.RadioBtnEncrypt.Text = "Encrypt";
            this.RadioBtnEncrypt.UseVisualStyleBackColor = true;
            // 
            // btnInputFile
            // 
            this.btnInputFile.Location = new System.Drawing.Point(27, 21);
            this.btnInputFile.Name = "btnInputFile";
            this.btnInputFile.Size = new System.Drawing.Size(141, 30);
            this.btnInputFile.TabIndex = 1;
            this.btnInputFile.Text = "Browse Input File";
            this.btnInputFile.UseVisualStyleBackColor = true;
            this.btnInputFile.Click += new System.EventHandler(this.btnInputFile_Click);
            // 
            // lblFilePath
            // 
            this.lblFilePath.AutoSize = true;
            this.lblFilePath.Location = new System.Drawing.Point(24, 70);
            this.lblFilePath.Name = "lblFilePath";
            this.lblFilePath.Size = new System.Drawing.Size(63, 17);
            this.lblFilePath.TabIndex = 2;
            this.lblFilePath.Text = "File Path";
            // 
            // btnRun
            // 
            this.btnRun.Location = new System.Drawing.Point(183, 209);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(75, 32);
            this.btnRun.TabIndex = 3;
            this.btnRun.Text = "Run";
            this.btnRun.UseVisualStyleBackColor = true;
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(471, 253);
            this.Controls.Add(this.btnRun);
            this.Controls.Add(this.lblFilePath);
            this.Controls.Add(this.btnInputFile);
            this.Controls.Add(this.GrpBoxOperation);
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Polyalphabetic Ciphers";
            this.GrpBoxOperation.ResumeLayout(false);
            this.GrpBoxOperation.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox GrpBoxOperation;
        private System.Windows.Forms.RadioButton RadioBtnDecrypt;
        private System.Windows.Forms.RadioButton RadioBtnEncrypt;
        private System.Windows.Forms.Button btnInputFile;
        private System.Windows.Forms.Label lblFilePath;
        private System.Windows.Forms.Button btnRun;
    }
}

