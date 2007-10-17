namespace megaboy
{
    partial class IOWindow
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
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lLY = new System.Windows.Forms.Label();
            this.lSCX = new System.Windows.Forms.Label();
            this.lSCY = new System.Windows.Forms.Label();
            this.lSTAT = new System.Windows.Forms.Label();
            this.lLCDC = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lRetrC = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.lLY);
            this.groupBox1.Controls.Add(this.lSCX);
            this.groupBox1.Controls.Add(this.lSCY);
            this.groupBox1.Controls.Add(this.lSTAT);
            this.groupBox1.Controls.Add(this.lLCDC);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(116, 193);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "LCD (hex)";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 68);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(47, 13);
            this.label7.TabIndex = 1;
            this.label7.Text = "FF44 LY";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 55);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(55, 13);
            this.label6.TabIndex = 1;
            this.label6.Text = "FF43 SCX";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 42);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(55, 13);
            this.label5.TabIndex = 1;
            this.label5.Text = "FF42 SCY";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 29);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "FF41 STAT";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "FF40 LCDC";
            // 
            // lLY
            // 
            this.lLY.AutoSize = true;
            this.lLY.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lLY.Location = new System.Drawing.Point(74, 67);
            this.lLY.Name = "lLY";
            this.lLY.Size = new System.Drawing.Size(21, 15);
            this.lLY.TabIndex = 0;
            this.lLY.Text = "00";
            // 
            // lSCX
            // 
            this.lSCX.AutoSize = true;
            this.lSCX.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lSCX.Location = new System.Drawing.Point(74, 54);
            this.lSCX.Name = "lSCX";
            this.lSCX.Size = new System.Drawing.Size(21, 15);
            this.lSCX.TabIndex = 0;
            this.lSCX.Text = "00";
            // 
            // lSCY
            // 
            this.lSCY.AutoSize = true;
            this.lSCY.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lSCY.Location = new System.Drawing.Point(74, 41);
            this.lSCY.Name = "lSCY";
            this.lSCY.Size = new System.Drawing.Size(21, 15);
            this.lSCY.TabIndex = 0;
            this.lSCY.Text = "00";
            // 
            // lSTAT
            // 
            this.lSTAT.AutoSize = true;
            this.lSTAT.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lSTAT.Location = new System.Drawing.Point(74, 28);
            this.lSTAT.Name = "lSTAT";
            this.lSTAT.Size = new System.Drawing.Size(21, 15);
            this.lSTAT.TabIndex = 0;
            this.lSTAT.Text = "00";
            // 
            // lLCDC
            // 
            this.lLCDC.AutoSize = true;
            this.lLCDC.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lLCDC.Location = new System.Drawing.Point(74, 15);
            this.lLCDC.Name = "lLCDC";
            this.lLCDC.Size = new System.Drawing.Size(21, 15);
            this.lLCDC.TabIndex = 0;
            this.lLCDC.Text = "00";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 228);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "retrCounter";
            // 
            // lRetrC
            // 
            this.lRetrC.AutoSize = true;
            this.lRetrC.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lRetrC.Location = new System.Drawing.Point(80, 227);
            this.lRetrC.Name = "lRetrC";
            this.lRetrC.Size = new System.Drawing.Size(27, 15);
            this.lRetrC.TabIndex = 0;
            this.lRetrC.Text = "000";
            // 
            // IOWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 266);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lRetrC);
            this.Name = "IOWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "megaboy I/O Map";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lSTAT;
        private System.Windows.Forms.Label lLCDC;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lLY;
        private System.Windows.Forms.Label lSCX;
        private System.Windows.Forms.Label lSCY;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.Label lRetrC;
    }
}