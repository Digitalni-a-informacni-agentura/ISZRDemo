namespace Autocont.ISZRDemo.Frms
{
    partial class AutorizaceInfoF
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
            this.checkedListBoxROB = new System.Windows.Forms.CheckedListBox();
            this.checkedListBoxROS = new System.Windows.Forms.CheckedListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btAutorizaceInfoOk = new System.Windows.Forms.Button();
            this.btAutorizaceInfoCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // checkedListBoxROB
            // 
            this.checkedListBoxROB.CheckOnClick = true;
            this.checkedListBoxROB.FormattingEnabled = true;
            this.checkedListBoxROB.Location = new System.Drawing.Point(12, 58);
            this.checkedListBoxROB.Name = "checkedListBoxROB";
            this.checkedListBoxROB.Size = new System.Drawing.Size(288, 579);
            this.checkedListBoxROB.TabIndex = 0;
            // 
            // checkedListBoxROS
            // 
            this.checkedListBoxROS.CheckOnClick = true;
            this.checkedListBoxROS.FormattingEnabled = true;
            this.checkedListBoxROS.Location = new System.Drawing.Point(354, 58);
            this.checkedListBoxROS.Name = "checkedListBoxROS";
            this.checkedListBoxROS.Size = new System.Drawing.Size(288, 579);
            this.checkedListBoxROS.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 32);
            this.label1.TabIndex = 2;
            this.label1.Text = "ROB";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label2.Location = new System.Drawing.Point(358, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 32);
            this.label2.TabIndex = 3;
            this.label2.Text = "ROS";
            // 
            // btAutorizaceInfoOk
            // 
            this.btAutorizaceInfoOk.Location = new System.Drawing.Point(677, 58);
            this.btAutorizaceInfoOk.Name = "btAutorizaceInfoOk";
            this.btAutorizaceInfoOk.Size = new System.Drawing.Size(109, 32);
            this.btAutorizaceInfoOk.TabIndex = 4;
            this.btAutorizaceInfoOk.Text = "OK";
            this.btAutorizaceInfoOk.UseVisualStyleBackColor = true;
            this.btAutorizaceInfoOk.Click += new System.EventHandler(this.btAutorizaceInfoOk_Click);
            // 
            // btAutorizaceInfoCancel
            // 
            this.btAutorizaceInfoCancel.Location = new System.Drawing.Point(677, 112);
            this.btAutorizaceInfoCancel.Name = "btAutorizaceInfoCancel";
            this.btAutorizaceInfoCancel.Size = new System.Drawing.Size(109, 32);
            this.btAutorizaceInfoCancel.TabIndex = 5;
            this.btAutorizaceInfoCancel.Text = "Cancel";
            this.btAutorizaceInfoCancel.UseVisualStyleBackColor = true;
            this.btAutorizaceInfoCancel.Click += new System.EventHandler(this.btAutorizaceInfoCancel_Click);
            // 
            // AutorizaceInfoF
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(817, 661);
            this.Controls.Add(this.btAutorizaceInfoCancel);
            this.Controls.Add(this.btAutorizaceInfoOk);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.checkedListBoxROS);
            this.Controls.Add(this.checkedListBoxROB);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AutorizaceInfoF";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Autorizace Info";
            this.Load += new System.EventHandler(this.AutorizaceInfoF_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.CheckedListBox checkedListBoxROB;
        public System.Windows.Forms.CheckedListBox checkedListBoxROS;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btAutorizaceInfoOk;
        private System.Windows.Forms.Button btAutorizaceInfoCancel;
    }
}