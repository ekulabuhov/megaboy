namespace megaboy
{
    partial class MainDebug
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
            this.components = new System.ComponentModel.Container();
            this.bNext = new System.Windows.Forms.Button();
            this.lAF = new System.Windows.Forms.Label();
            this.lBC = new System.Windows.Forms.Label();
            this.lDE = new System.Windows.Forms.Label();
            this.lHL = new System.Windows.Forms.Label();
            this.lSP = new System.Windows.Forms.Label();
            this.lPC = new System.Windows.Forms.Label();
            this.lTitle = new System.Windows.Forms.Label();
            this.cbZ = new System.Windows.Forms.CheckBox();
            this.cbN = new System.Windows.Forms.CheckBox();
            this.cbH = new System.Windows.Forms.CheckBox();
            this.cbC = new System.Windows.Forms.CheckBox();
            this.hexBox1 = new Be.Windows.Forms.HexBox();
            this.contextMHex = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.goToToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lbDisasm = new System.Windows.Forms.ListBox();
            this.contextMAsm = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.goToMenuItemAsm = new System.Windows.Forms.ToolStripMenuItem();
            this.addBreakpointToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.iOMapToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.paletteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.debugToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stepToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.runToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addBreakToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMHex.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.contextMAsm.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // bNext
            // 
            this.bNext.Location = new System.Drawing.Point(97, 178);
            this.bNext.Name = "bNext";
            this.bNext.Size = new System.Drawing.Size(75, 23);
            this.bNext.TabIndex = 0;
            this.bNext.Text = "Next";
            this.bNext.UseVisualStyleBackColor = true;
            this.bNext.Click += new System.EventHandler(this.bNext_Click);
            // 
            // lAF
            // 
            this.lAF.AutoSize = true;
            this.lAF.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lAF.Location = new System.Drawing.Point(399, 38);
            this.lAF.Name = "lAF";
            this.lAF.Size = new System.Drawing.Size(28, 13);
            this.lAF.TabIndex = 1;
            this.lAF.Text = "af = ";
            // 
            // lBC
            // 
            this.lBC.AutoSize = true;
            this.lBC.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lBC.Location = new System.Drawing.Point(399, 51);
            this.lBC.Name = "lBC";
            this.lBC.Size = new System.Drawing.Size(31, 13);
            this.lBC.TabIndex = 2;
            this.lBC.Text = "bc = ";
            // 
            // lDE
            // 
            this.lDE.AutoSize = true;
            this.lDE.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lDE.Location = new System.Drawing.Point(399, 64);
            this.lDE.Name = "lDE";
            this.lDE.Size = new System.Drawing.Size(31, 13);
            this.lDE.TabIndex = 3;
            this.lDE.Text = "de = ";
            // 
            // lHL
            // 
            this.lHL.AutoSize = true;
            this.lHL.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lHL.Location = new System.Drawing.Point(399, 77);
            this.lHL.Name = "lHL";
            this.lHL.Size = new System.Drawing.Size(27, 13);
            this.lHL.TabIndex = 4;
            this.lHL.Text = "hl = ";
            // 
            // lSP
            // 
            this.lSP.AutoSize = true;
            this.lSP.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lSP.Location = new System.Drawing.Point(399, 90);
            this.lSP.Name = "lSP";
            this.lSP.Size = new System.Drawing.Size(30, 13);
            this.lSP.TabIndex = 4;
            this.lSP.Text = "sp = ";
            // 
            // lPC
            // 
            this.lPC.AutoSize = true;
            this.lPC.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lPC.Location = new System.Drawing.Point(399, 103);
            this.lPC.Name = "lPC";
            this.lPC.Size = new System.Drawing.Size(31, 13);
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
            // cbZ
            // 
            this.cbZ.AutoSize = true;
            this.cbZ.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cbZ.Location = new System.Drawing.Point(402, 120);
            this.cbZ.Name = "cbZ";
            this.cbZ.Size = new System.Drawing.Size(31, 17);
            this.cbZ.TabIndex = 7;
            this.cbZ.Text = "z";
            this.cbZ.UseVisualStyleBackColor = true;
            this.cbZ.Click += new System.EventHandler(this.cbZ_Click);
            // 
            // cbN
            // 
            this.cbN.AutoSize = true;
            this.cbN.Location = new System.Drawing.Point(402, 134);
            this.cbN.Name = "cbN";
            this.cbN.Size = new System.Drawing.Size(32, 17);
            this.cbN.TabIndex = 7;
            this.cbN.Text = "n";
            this.cbN.UseVisualStyleBackColor = true;
            this.cbN.Click += new System.EventHandler(this.cbN_Click);
            // 
            // cbH
            // 
            this.cbH.AutoSize = true;
            this.cbH.Location = new System.Drawing.Point(402, 148);
            this.cbH.Name = "cbH";
            this.cbH.Size = new System.Drawing.Size(32, 17);
            this.cbH.TabIndex = 7;
            this.cbH.Text = "h";
            this.cbH.UseVisualStyleBackColor = true;
            this.cbH.Click += new System.EventHandler(this.cbH_Click);
            // 
            // cbC
            // 
            this.cbC.AutoSize = true;
            this.cbC.Location = new System.Drawing.Point(402, 162);
            this.cbC.Name = "cbC";
            this.cbC.Size = new System.Drawing.Size(32, 17);
            this.cbC.TabIndex = 7;
            this.cbC.Text = "c";
            this.cbC.UseVisualStyleBackColor = true;
            this.cbC.Click += new System.EventHandler(this.cbC_Click);
            // 
            // hexBox1
            // 
            this.hexBox1.BytesPerLine = 8;
            this.hexBox1.ContextMenuStrip = this.contextMHex;
            this.hexBox1.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.hexBox1.LineInfoForeColor = System.Drawing.Color.Empty;
            this.hexBox1.LineInfoVisible = true;
            this.hexBox1.Location = new System.Drawing.Point(12, 220);
            this.hexBox1.Name = "hexBox1";
            this.hexBox1.ShadowSelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(60)))), ((int)(((byte)(188)))), ((int)(((byte)(255)))));
            this.hexBox1.Size = new System.Drawing.Size(482, 102);
            this.hexBox1.StringViewVisible = true;
            this.hexBox1.TabIndex = 8;
            this.hexBox1.UseFixedBytesPerLine = true;
            this.hexBox1.VScrollBarVisible = true;
            this.hexBox1.CurrentPositionInLineChanged += new System.EventHandler(this.PositionChanged);
            this.hexBox1.CurrentLineChanged += new System.EventHandler(this.PositionChanged);
            // 
            // contextMHex
            // 
            this.contextMHex.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.goToToolStripMenuItem});
            this.contextMHex.Name = "contextMHex";
            this.contextMHex.Size = new System.Drawing.Size(166, 26);
            // 
            // goToToolStripMenuItem
            // 
            this.goToToolStripMenuItem.Name = "goToToolStripMenuItem";
            this.goToToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.G)));
            this.goToToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.goToToolStripMenuItem.Text = "Go to ...";
            this.goToToolStripMenuItem.Click += new System.EventHandler(this.goToToolStripMenuItem_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 331);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(506, 22);
            this.statusStrip1.TabIndex = 9;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(109, 17);
            this.toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            // 
            // lbDisasm
            // 
            this.lbDisasm.ContextMenuStrip = this.contextMAsm;
            this.lbDisasm.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.lbDisasm.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lbDisasm.FormattingEnabled = true;
            this.lbDisasm.ItemHeight = 15;
            this.lbDisasm.Location = new System.Drawing.Point(12, 42);
            this.lbDisasm.Name = "lbDisasm";
            this.lbDisasm.ScrollAlwaysVisible = true;
            this.lbDisasm.Size = new System.Drawing.Size(381, 109);
            this.lbDisasm.TabIndex = 10;
            this.lbDisasm.Tag = "0";
            this.lbDisasm.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lbDisasm_MouseDoubleClick);
            this.lbDisasm.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.lbDisasm_DrawItem);
            this.lbDisasm.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lbDisasm_MouseDown);
            // 
            // contextMAsm
            // 
            this.contextMAsm.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.goToMenuItemAsm,
            this.addBreakpointToolStripMenuItem});
            this.contextMAsm.Name = "contextMAsm";
            this.contextMAsm.Size = new System.Drawing.Size(148, 48);
            // 
            // goToMenuItemAsm
            // 
            this.goToMenuItemAsm.Name = "goToMenuItemAsm";
            this.goToMenuItemAsm.Size = new System.Drawing.Size(147, 22);
            this.goToMenuItemAsm.Text = "Go to ...";
            this.goToMenuItemAsm.Click += new System.EventHandler(this.goToMenuItemAsm_Click);
            // 
            // addBreakpointToolStripMenuItem
            // 
            this.addBreakpointToolStripMenuItem.Name = "addBreakpointToolStripMenuItem";
            this.addBreakpointToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
            this.addBreakpointToolStripMenuItem.Text = "Toggle Break";
            this.addBreakpointToolStripMenuItem.Click += new System.EventHandler(this.addBreakpointToolStripMenuItem_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.toolStripMenuItem2,
            this.debugToolStripMenuItem});
            this.menuStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.menuStrip1.Size = new System.Drawing.Size(506, 24);
            this.menuStrip1.TabIndex = 11;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(35, 20);
            this.toolStripMenuItem1.Text = "File";
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.iOMapToolStripMenuItem,
            this.paletteToolStripMenuItem});
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(57, 20);
            this.toolStripMenuItem2.Text = "Window";
            // 
            // iOMapToolStripMenuItem
            // 
            this.iOMapToolStripMenuItem.Name = "iOMapToolStripMenuItem";
            this.iOMapToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F10;
            this.iOMapToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            this.iOMapToolStripMenuItem.Text = "I/O Map";
            this.iOMapToolStripMenuItem.Click += new System.EventHandler(this.iOMapToolStripMenuItem_Click);
            // 
            // paletteToolStripMenuItem
            // 
            this.paletteToolStripMenuItem.Name = "paletteToolStripMenuItem";
            this.paletteToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            this.paletteToolStripMenuItem.Text = "Palette";
            this.paletteToolStripMenuItem.Click += new System.EventHandler(this.paletteToolStripMenuItem_Click);
            // 
            // debugToolStripMenuItem
            // 
            this.debugToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.stepToolStripMenuItem,
            this.runToolStripMenuItem,
            this.addBreakToolStripMenuItem});
            this.debugToolStripMenuItem.Name = "debugToolStripMenuItem";
            this.debugToolStripMenuItem.Size = new System.Drawing.Size(50, 20);
            this.debugToolStripMenuItem.Text = "Debug";
            // 
            // stepToolStripMenuItem
            // 
            this.stepToolStripMenuItem.Name = "stepToolStripMenuItem";
            this.stepToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F7;
            this.stepToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.stepToolStripMenuItem.Text = "Step";
            this.stepToolStripMenuItem.Click += new System.EventHandler(this.stepToolStripMenuItem_Click);
            // 
            // runToolStripMenuItem
            // 
            this.runToolStripMenuItem.Name = "runToolStripMenuItem";
            this.runToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F9;
            this.runToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.runToolStripMenuItem.Text = "Run";
            this.runToolStripMenuItem.Click += new System.EventHandler(this.runToolStripMenuItem_Click);
            // 
            // addBreakToolStripMenuItem
            // 
            this.addBreakToolStripMenuItem.Name = "addBreakToolStripMenuItem";
            this.addBreakToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.addBreakToolStripMenuItem.Text = "Add Break";
            this.addBreakToolStripMenuItem.Click += new System.EventHandler(this.addBreakToolStripMenuItem_Click);
            // 
            // MainDebug
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(506, 353);
            this.Controls.Add(this.lbDisasm);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.hexBox1);
            this.Controls.Add(this.cbC);
            this.Controls.Add(this.cbH);
            this.Controls.Add(this.cbN);
            this.Controls.Add(this.cbZ);
            this.Controls.Add(this.lTitle);
            this.Controls.Add(this.lPC);
            this.Controls.Add(this.lSP);
            this.Controls.Add(this.lHL);
            this.Controls.Add(this.lDE);
            this.Controls.Add(this.lBC);
            this.Controls.Add(this.lAF);
            this.Controls.Add(this.bNext);
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainDebug";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Main";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.contextMHex.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.contextMAsm.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button bNext;
        private System.Windows.Forms.Label lAF;
        private System.Windows.Forms.Label lBC;
        private System.Windows.Forms.Label lDE;
        private System.Windows.Forms.Label lHL;
        private System.Windows.Forms.Label lSP;
        private System.Windows.Forms.Label lPC;
        private System.Windows.Forms.Label lTitle;
        private System.Windows.Forms.CheckBox cbZ;
        private System.Windows.Forms.CheckBox cbN;
        private System.Windows.Forms.CheckBox cbH;
        private System.Windows.Forms.CheckBox cbC;
        private Be.Windows.Forms.HexBox hexBox1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ContextMenuStrip contextMHex;
        private System.Windows.Forms.ToolStripMenuItem goToToolStripMenuItem;
        private System.Windows.Forms.ListBox lbDisasm;
        private System.Windows.Forms.ContextMenuStrip contextMAsm;
        private System.Windows.Forms.ToolStripMenuItem goToMenuItemAsm;
        private System.Windows.Forms.ToolStripMenuItem addBreakpointToolStripMenuItem;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem iOMapToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem paletteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem debugToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stepToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem runToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addBreakToolStripMenuItem;
    }
}

