namespace MyProj
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
            this.button1 = new System.Windows.Forms.Button();
            this.lAF = new System.Windows.Forms.Label();
            this.lBC = new System.Windows.Forms.Label();
            this.lDE = new System.Windows.Forms.Label();
            this.lHL = new System.Windows.Forms.Label();
            this.lSP = new System.Windows.Forms.Label();
            this.lPC = new System.Windows.Forms.Label();
            this.lTitle = new System.Windows.Forms.Label();
            this.lInstr = new System.Windows.Forms.Label();
            this.cbZ = new System.Windows.Forms.CheckBox();
            this.cbN = new System.Windows.Forms.CheckBox();
            this.cbH = new System.Windows.Forms.CheckBox();
            this.cbC = new System.Windows.Forms.CheckBox();
            this.hexBox1 = new Be.Windows.Forms.HexBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(97, 178);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Next";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // lAF
            // 
            this.lAF.AutoSize = true;
            this.lAF.Font = new System.Drawing.Font("Monospac821 BT", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lAF.Location = new System.Drawing.Point(214, 38);
            this.lAF.Name = "lAF";
            this.lAF.Size = new System.Drawing.Size(42, 14);
            this.lAF.TabIndex = 1;
            this.lAF.Text = "af = ";
            // 
            // lBC
            // 
            this.lBC.AutoSize = true;
            this.lBC.Font = new System.Drawing.Font("Monospac821 BT", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lBC.Location = new System.Drawing.Point(214, 51);
            this.lBC.Name = "lBC";
            this.lBC.Size = new System.Drawing.Size(42, 14);
            this.lBC.TabIndex = 2;
            this.lBC.Text = "bc = ";
            // 
            // lDE
            // 
            this.lDE.AutoSize = true;
            this.lDE.Font = new System.Drawing.Font("Monospac821 BT", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lDE.Location = new System.Drawing.Point(214, 64);
            this.lDE.Name = "lDE";
            this.lDE.Size = new System.Drawing.Size(42, 14);
            this.lDE.TabIndex = 3;
            this.lDE.Text = "de = ";
            // 
            // lHL
            // 
            this.lHL.AutoSize = true;
            this.lHL.Font = new System.Drawing.Font("Monospac821 BT", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lHL.Location = new System.Drawing.Point(214, 77);
            this.lHL.Name = "lHL";
            this.lHL.Size = new System.Drawing.Size(42, 14);
            this.lHL.TabIndex = 4;
            this.lHL.Text = "hl = ";
            // 
            // lSP
            // 
            this.lSP.AutoSize = true;
            this.lSP.Font = new System.Drawing.Font("Monospac821 BT", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lSP.Location = new System.Drawing.Point(214, 90);
            this.lSP.Name = "lSP";
            this.lSP.Size = new System.Drawing.Size(42, 14);
            this.lSP.TabIndex = 4;
            this.lSP.Text = "sp = ";
            // 
            // lPC
            // 
            this.lPC.AutoSize = true;
            this.lPC.Font = new System.Drawing.Font("Monospac821 BT", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lPC.Location = new System.Drawing.Point(214, 103);
            this.lPC.Name = "lPC";
            this.lPC.Size = new System.Drawing.Size(42, 14);
            this.lPC.TabIndex = 4;
            this.lPC.Text = "pc = ";
            // 
            // lTitle
            // 
            this.lTitle.AutoSize = true;
            this.lTitle.Location = new System.Drawing.Point(12, 9);
            this.lTitle.Name = "lTitle";
            this.lTitle.Size = new System.Drawing.Size(33, 13);
            this.lTitle.TabIndex = 5;
            this.lTitle.Text = "Title: ";
            // 
            // lInstr
            // 
            this.lInstr.AutoSize = true;
            this.lInstr.Location = new System.Drawing.Point(12, 22);
            this.lInstr.Name = "lInstr";
            this.lInstr.Size = new System.Drawing.Size(31, 13);
            this.lInstr.TabIndex = 6;
            this.lInstr.Text = "op = ";
            // 
            // cbZ
            // 
            this.cbZ.AutoSize = true;
            this.cbZ.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cbZ.Location = new System.Drawing.Point(217, 120);
            this.cbZ.Name = "cbZ";
            this.cbZ.Size = new System.Drawing.Size(31, 17);
            this.cbZ.TabIndex = 7;
            this.cbZ.Text = "z";
            this.cbZ.UseVisualStyleBackColor = true;
            // 
            // cbN
            // 
            this.cbN.AutoSize = true;
            this.cbN.Location = new System.Drawing.Point(217, 134);
            this.cbN.Name = "cbN";
            this.cbN.Size = new System.Drawing.Size(32, 17);
            this.cbN.TabIndex = 7;
            this.cbN.Text = "n";
            this.cbN.UseVisualStyleBackColor = true;
            // 
            // cbH
            // 
            this.cbH.AutoSize = true;
            this.cbH.Location = new System.Drawing.Point(217, 148);
            this.cbH.Name = "cbH";
            this.cbH.Size = new System.Drawing.Size(32, 17);
            this.cbH.TabIndex = 7;
            this.cbH.Text = "h";
            this.cbH.UseVisualStyleBackColor = true;
            // 
            // cbC
            // 
            this.cbC.AutoSize = true;
            this.cbC.Location = new System.Drawing.Point(217, 162);
            this.cbC.Name = "cbC";
            this.cbC.Size = new System.Drawing.Size(32, 17);
            this.cbC.TabIndex = 7;
            this.cbC.Text = "c";
            this.cbC.UseVisualStyleBackColor = true;
            // 
            // hexBox1
            // 
            this.hexBox1.BytesPerLine = 8;
            this.hexBox1.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.hexBox1.LineInfoVisible = true;
            this.hexBox1.Location = new System.Drawing.Point(12, 220);
            this.hexBox1.Name = "hexBox1";
            this.hexBox1.SelectionLength = ((long)(0));
            this.hexBox1.SelectionStart = ((long)(-1));
            this.hexBox1.ShadowSelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(60)))), ((int)(((byte)(188)))), ((int)(((byte)(255)))));
            this.hexBox1.Size = new System.Drawing.Size(383, 102);
            this.hexBox1.StringViewVisible = true;
            this.hexBox1.TabIndex = 8;
            this.hexBox1.UseFixedBytesPerLine = true;
            this.hexBox1.VScrollBarVisible = true;
            this.hexBox1.CurrentPositionInLineChanged += new System.EventHandler(this.PositionChanged);
            this.hexBox1.CurrentLineChanged += new System.EventHandler(this.PositionChanged);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 331);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(407, 22);
            this.statusStrip1.TabIndex = 9;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(109, 17);
            this.toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(407, 353);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.hexBox1);
            this.Controls.Add(this.cbC);
            this.Controls.Add(this.cbH);
            this.Controls.Add(this.cbN);
            this.Controls.Add(this.cbZ);
            this.Controls.Add(this.lInstr);
            this.Controls.Add(this.lTitle);
            this.Controls.Add(this.lPC);
            this.Controls.Add(this.lSP);
            this.Controls.Add(this.lHL);
            this.Controls.Add(this.lDE);
            this.Controls.Add(this.lBC);
            this.Controls.Add(this.lAF);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label lAF;
        private System.Windows.Forms.Label lBC;
        private System.Windows.Forms.Label lDE;
        private System.Windows.Forms.Label lHL;
        private System.Windows.Forms.Label lSP;
        private System.Windows.Forms.Label lPC;
        private System.Windows.Forms.Label lTitle;
        private System.Windows.Forms.Label lInstr;
        private System.Windows.Forms.CheckBox cbZ;
        private System.Windows.Forms.CheckBox cbN;
        private System.Windows.Forms.CheckBox cbH;
        private System.Windows.Forms.CheckBox cbC;
        private Be.Windows.Forms.HexBox hexBox1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
    }
}

