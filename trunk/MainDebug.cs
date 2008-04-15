/* 1.Create and initialize a delegate
 *      public delegate void smthng();
 * 2.Create an array of those delegates
 *      smthng[] op = new smthng[50];
 * 3.Assign a function to each array delegate
 *      op[0] = UpdateInfo;
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Collections;

using Be.Windows.Forms;


namespace megaboy
{

    public partial class MainDebug : Form
    {
        public static Form IOMap;
        public static Form VramMap;

        byte[] some = new byte[20];

        /* CPU  */
        Z80 CPU = new Z80();

        /* Misc  */
        public static int cycles = 0, autorun = 0, lbPC;
        public static int retraceCounter;
        int modeCounter;
        string[] modes = {"H-Blank", "V-Blank", "OAM", "VRAM", };
        int[] modeInterval = { 51, 1140, 20, 43, }; // how much in cycles each period longs       
        Dictionary<int, int> Breakpoints = new Dictionary<int, int>();

        /* HexViewer  */
        public static CustomByteProvider dynamicbp;
        string dynMemMap;

        /* Constants  */
        const int LY_INTERVAL = 114;
        
        const byte BGPAL = 1, OBJ0PAL = 2, OBJ1PAL = 3;

        /* Palettes  */
        public static Color[] defaultPal = { Color.White, Color.LightGray, Color.Gray, Color.Black }; 
        
        
        void Emu_Init()
        {
            // Open File
            FileStream fs = new FileStream("D:/Tetris.gb", FileMode.Open);
            // Create reader
            BinaryReader r = new BinaryReader(fs);
            // Read rom to memory
            byte[] fileBuf = new byte[fs.Length];
            fileBuf = r.ReadBytes((int)fs.Length);
            // Read first ROM page
            Memory.copyRom(fileBuf);   
            r.Close();
            fs.Close();
            // Close it
            // Copy some bytes from array to form an RomTitle
            Array.ConstrainedCopy(fileBuf, 0x134, some, 0, 16);
            lTitle.Text += Hex2String(some);

            // Display memory
            dynamicbp = new CustomByteProvider();
            hexBox1.ByteProvider = dynamicbp;

            /* GameBoy starts in the VBlank mode 
             * and stays there for 17 cycles only */
            retraceCounter = 131;   // from no$gmb, originally: LY_INTERVAL
            modeCounter = 17;
            Memory.setIO(IO.STAT, 1); // Write to IO
            // Create I/O window
            IOMap = new IOWindow();
            IOMap.Location = new Point(80, 60);
            IOMap.Show();

            VramMap = new VramViewer();

            Memory.setIO(IO.BGP, 0xFC);   // Background Palette
            Memory.setIO(IO.OBP0, 0xFF);  // Sprite 0 Palette
            Memory.setIO(IO.OBP1, 0xFF);  // Sprite 1 Palette  

            UpdatePalette(BGPAL);
            UpdatePalette(OBJ0PAL);
            UpdatePalette(OBJ1PAL);

            /* Set volume to MAX, Vin to OFF
             * Set which channels to output to L/R
             * Sound is ON  */
            Memory.setIO(IO.SVOL, 0x77);   // Sound ON-OFF / Volume
            Memory.setIO(IO.SLR, 0xF3);    // Selection of Sound output terminal
            Memory.setIO(IO.SON, 0xF1);    // Sound on/off

            /* LCD is ON, BG is ON */
            Memory.setIO(IO.LCDC, 0x91);    // LCDC

            UpdateIOForm();
            
            FillIODesc();
            
            UpdateInfo();
              
        }


        void UpdateInfo()
        {
            lAF.Text = "AF = " + CPU.readRegAF();
            lBC.Text = "BC = " + CPU.gb_bc.ToString("X4");
            lDE.Text = "DE = " + CPU.gb_de.ToString("X4");
            lHL.Text = "HL = " + CPU.gb_hl.ToString("X4");
            lSP.Text = "SP = " + CPU.gb_sp.ToString("X4");
            lPC.Text = "PC = " + CPU.gb_pc.ToString("X4");
            cbZ.Checked = ((CPU.gb_flgs & CPU.zF) > 0);
            cbN.Checked = ((CPU.gb_flgs & CPU.nF) > 0);
            cbH.Checked = ((CPU.gb_flgs & CPU.hcarryF) > 0);
            cbC.Checked = ((CPU.gb_flgs & CPU.carryF) > 0);
        }

        public MainDebug()
        {
            InitializeComponent();
            Emu_Init();
            Disassemble();
        }

        string Hex2String(byte[] arr)
        {
            
            return System.Text.Encoding.ASCII.GetString(arr);
        }

        // Execute one preloaded instruction
        private void bNext_Click(object sender, EventArgs e)
        {
            do
            {
                CPU.step();
                cycles += cycleTbl[CPU.instr];
                
                // Update cycle counters if display enabled
                if ((Memory.readIOByte(0x40) & IOWindow.LCDON) > 0)
                {
                    retraceCounter -= cycles;
                    modeCounter -= cycles;
                }
                cycles = 0;

            if (retraceCounter <= 0)
            {
                // INCREASE LY register & RESET retrace counter
                byte LY = Memory.readIOByte(IO.LY);
                LY++;
                if (LY > 153)
                    // if overflown, reset it
                    LY = 0;

                retraceCounter += LY_INTERVAL;
                Memory.setIO(IO.LY, LY);
            }

            if (modeCounter <= 0)
            {
                byte Stat = Memory.readIOByte(IO.STAT);
                Stat += 1;  // LCD Stat
                Stat &= 3;
                // if mode is V-blank, but LY not in range (144..153), skip it
                if (((Stat & 3) == 1) && (Memory.readIOByte(IO.LY) < 144))
                    Stat += 1;
                modeCounter += modeInterval[(Stat & 3)];
                Memory.setIO(IO.STAT, Stat);
            }

            if (Breakpoints.ContainsKey(CPU.gb_pc) == true)
                autorun = 0;
            } while (autorun == 1);

            GotoPC(CPU.gb_pc);
            UpdateInfo();
            UpdateIOForm();
        }

        // Memory window cursor handler
        private void PositionChanged(object sender, EventArgs e)
        {
            ushort CurAddr = (ushort)((hexBox1.CurrentLine-1)*hexBox1.BytesPerLine + hexBox1.CurrentPositionInLine - 1);
            switch (CurAddr >> 12)
            {
                case 0x0:
                case 0x1:
                case 0x2:
                case 0x3:
                    dynMemMap = "ROM0";
                    break;
                case 0xC:
                    dynMemMap = "RAM0";
                    break;
                case 0xD:
                    dynMemMap = "RAM1";
                    break;
                case 0xF:
                    dynMemMap = "IO";
                    break;
            }
            toolStripStatusLabel1.Text = string.Format("{1}:{0:X4}", CurAddr, dynMemMap);
        }

        // Memory window Go To handler
        private void goToToolStripMenuItem_Click(object sender, EventArgs e)
        {           
            Form Goto = new dlgGoto();
            if (Goto.ShowDialog() == DialogResult.Cancel)
                return;
            hexBox1.ScrollByteIntoView(dlgGoto.addr);
            hexBox1.Select(dlgGoto.addr, 1);
        }

        // Disassm window Go To handler
        private void goToMenuItemAsm_Click(object sender, EventArgs e)
        {
            Form Goto = new dlgGoto();
            if (Goto.ShowDialog() == DialogResult.Cancel) return;
            GotoPC(dlgGoto.addr);
        }

        int index;      // Used by GotoPC, in here just to make it global
        void GotoPC(int PC)
        {
            int m;

            // get the closest reference point & count from it
            if ((PC < lbPC) || (PC - lbPC >= 50))
            {
                m = PC / 50;
                if (m > 0)
                {
                    lbPC = listboxPos[m].PC;
                    index = listboxPos[m].index;
                }
                else
                    lbPC = index = 0;
            }
            
            // We can only trace forward
            while (lbPC != PC)
            {
                lbPC += InstrLength[Memory.readRomByte(lbPC)];
                index++;
            }
            lbDisasm.TopIndex = index - 3;
            lbDisasm.SelectedIndex = index;

        }

        private void cbZ_Click(object sender, EventArgs e)
        {
            CPU.toggleZFlag();  
            lAF.Text = "AF = " + CPU.readRegAF();
        }
        private void cbN_Click(object sender, EventArgs e)
        {
            CPU.toggleNFlag();
            lAF.Text = "AF = " + CPU.readRegAF();
        }
        private void cbH_Click(object sender, EventArgs e)
        {
            CPU.toggleHCarryFlag();
            lAF.Text = "AF = " + CPU.readRegAF();
        }
        private void cbC_Click(object sender, EventArgs e)
        {
            CPU.toggleCarryFlag();
            lAF.Text = "AF = " + CPU.readRegAF();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)   // HotKeys!
        {
            //bNext.Text = e.KeyValue.ToString();
            /*  Overriden by strip menu items!
            if (e.KeyValue == 118)  // F7 - Trace
                bNext.PerformClick();
            if (e.KeyValue == 120)  // F9 - Run
            {
                autorun = 1;
                bNext.PerformClick();
            }
            */
            
        }


        private void addBreakpointToolStripMenuItem_Click(object sender, EventArgs e)
        {           
            String addr = lbDisasm.SelectedItem.ToString().Substring(5,4);
            int PC = Convert.ToUInt16(addr, 16);
            if (Breakpoints.ContainsKey(PC) == true)
                Breakpoints.Remove(PC);
            else
                Breakpoints.Add(PC, lbDisasm.SelectedIndex);

            lbDisasm.Invalidate();  // Force redraw
        }

        private void iOMapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IOMap.Show();
        }

        private void paletteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            VramMap.Show();
        }


        private void lbDisasm_DrawItem(object sender, DrawItemEventArgs e)
        {
            // Draw the background of the ListBox control for each item.
            e.DrawBackground();
            // Define the default color of the brush as black.
            Brush myBrush = Brushes.Black;

            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
            {
                myBrush = Brushes.White;
            }
            
            // Determine the color of the brush to draw each item based 
            // on the index of the item to draw.
            if (Breakpoints.ContainsValue(e.Index) == true)
                myBrush = Brushes.Red;

            // Draw the current item text based on the current Font 
            // and the custom brush settings.
            e.Graphics.DrawString(lbDisasm.Items[e.Index].ToString(),
                e.Font, myBrush, e.Bounds, StringFormat.GenericDefault);
            // If the ListBox has focus, draw a focus rectangle around the selected item.
            e.DrawFocusRectangle();
        }

        private void lbDisasm_MouseDown(object sender, MouseEventArgs e)
        {
            /* SelectedIndex changes before MouseDown event ehh?
            int index = lbDisasm.IndexFromPoint(e.Location);
            if (index == lbDisasm.SelectedIndex)
                addBreakpointToolStripMenuItem.PerformClick(); */

            lbDisasm.SelectedIndex = lbDisasm.IndexFromPoint(e.Location);
        }

        private void lbDisasm_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            addBreakpointToolStripMenuItem.PerformClick();
        }

        private void stepToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bNext.PerformClick();
        }

        private void runToolStripMenuItem_Click(object sender, EventArgs e)
        {
            autorun = 1;
            bNext.PerformClick();
        }

        private void addBreakToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form SetBreak = new dlgGoto();
            SetBreak.Text = "Set Break on";
            if (SetBreak.ShowDialog() == DialogResult.Cancel) return;
            if (Breakpoints.ContainsKey(dlgGoto.addr) == false)
            {
                int index = lbDisasm.TopIndex;
                int selIndex = lbDisasm.SelectedIndex;
                GotoPC(dlgGoto.addr);
                Breakpoints.Add(dlgGoto.addr, lbDisasm.SelectedIndex);
                lbDisasm.SelectedIndex = selIndex;
                lbDisasm.TopIndex = index;
            }
                 
        }
    }
}