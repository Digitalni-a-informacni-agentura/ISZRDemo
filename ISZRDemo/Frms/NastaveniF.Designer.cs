namespace Autocont.ISZRDemo
{
    partial class NastaveniF
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbProsyOn = new System.Windows.Forms.RadioButton();
            this.rbProxyOff = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tbDom = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbPwd = new System.Windows.Forms.TextBox();
            this.tbUser = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btOk = new System.Windows.Forms.Button();
            this.label12 = new System.Windows.Forms.Label();
            this.cbDefaultNC = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbProsyOn);
            this.groupBox1.Controls.Add(this.rbProxyOff);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(140, 94);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Proxy";
            // 
            // rbProsyOn
            // 
            this.rbProsyOn.AutoSize = true;
            this.rbProsyOn.Location = new System.Drawing.Point(18, 59);
            this.rbProsyOn.Name = "rbProsyOn";
            this.rbProsyOn.Size = new System.Drawing.Size(75, 21);
            this.rbProsyOn.TabIndex = 1;
            this.rbProsyOn.TabStop = true;
            this.rbProsyOn.Text = "Systém";
            this.rbProsyOn.UseVisualStyleBackColor = true;
            // 
            // rbProxyOff
            // 
            this.rbProxyOff.AutoSize = true;
            this.rbProxyOff.Location = new System.Drawing.Point(18, 32);
            this.rbProxyOff.Name = "rbProxyOff";
            this.rbProxyOff.Size = new System.Drawing.Size(81, 21);
            this.rbProxyOff.TabIndex = 0;
            this.rbProxyOff.TabStop = true;
            this.rbProxyOff.Text = "Vypnuto";
            this.rbProxyOff.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cbDefaultNC);
            this.groupBox2.Controls.Add(this.tbDom);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.tbPwd);
            this.groupBox2.Controls.Add(this.tbUser);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new System.Drawing.Point(158, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(215, 156);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Proxy ověření";
            // 
            // tbDom
            // 
            this.tbDom.Location = new System.Drawing.Point(94, 115);
            this.tbDom.Name = "tbDom";
            this.tbDom.Size = new System.Drawing.Size(100, 22);
            this.tbDom.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 118);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 17);
            this.label3.TabIndex = 4;
            this.label3.Text = "Doména";
            // 
            // tbPwd
            // 
            this.tbPwd.Location = new System.Drawing.Point(94, 87);
            this.tbPwd.Name = "tbPwd";
            this.tbPwd.Size = new System.Drawing.Size(100, 22);
            this.tbPwd.TabIndex = 3;
            // 
            // tbUser
            // 
            this.tbUser.Location = new System.Drawing.Point(94, 59);
            this.tbUser.Name = "tbUser";
            this.tbUser.Size = new System.Drawing.Size(100, 22);
            this.tbUser.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 90);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "Heslo";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 62);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Uživatel";
            // 
            // btOk
            // 
            this.btOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btOk.Location = new System.Drawing.Point(298, 196);
            this.btOk.Name = "btOk";
            this.btOk.Size = new System.Drawing.Size(75, 23);
            this.btOk.TabIndex = 2;
            this.btOk.Text = "Ok";
            this.btOk.UseVisualStyleBackColor = true;
            this.btOk.Click += new System.EventHandler(this.btOk_Click);
            // 
            // label12
            // 
            this.label12.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label12.Location = new System.Drawing.Point(12, 184);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(223, 35);
            this.label12.TabIndex = 11;
            this.label12.Text = "Nastavení lze provést v konfiguračním souboru aplikace";
            // 
            // cbDefaultNC
            // 
            this.cbDefaultNC.AutoSize = true;
            this.cbDefaultNC.Location = new System.Drawing.Point(14, 32);
            this.cbDefaultNC.Name = "cbDefaultNC";
            this.cbDefaultNC.Size = new System.Drawing.Size(197, 21);
            this.cbDefaultNC.TabIndex = 6;
            this.cbDefaultNC.Text = "DefaultNetworkCredentials";
            this.cbDefaultNC.UseVisualStyleBackColor = true;
            // 
            // NastaveniF
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(389, 233);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.btOk);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NastaveniF";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Ostatní nastavení";
            this.Load += new System.EventHandler(this.NastaveniF_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbProsyOn;
        private System.Windows.Forms.RadioButton rbProxyOff;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox tbPwd;
        private System.Windows.Forms.TextBox tbUser;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbDom;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btOk;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.CheckBox cbDefaultNC;
    }
}