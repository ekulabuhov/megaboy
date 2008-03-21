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

        /* Memory */
        public static Memory Mem = new Memory();

        /* Misc  */
        public static int cycles = 0, autorun = 0, lbPC;
        public static int retraceCounter;
        int modeCounter;
        string[] modes = {"H-Blank", "V-Blank", "OAM", "VRAM", };
        int[] modeInterval = { 51, 1140, 20, 43, }; // how much in cycles each period longs
        ushort breakpoint;

        

        /* HexViewer  */
        public static DynamicByteProvider dynamicbp;
        string dynMemMap = "RAM1";
        public static ushort dynMemOffset = 0xD000;

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
            Mem.copyRom(fileBuf);   
            r.Close();
            fs.Close();
            // Close it
            // Copy some bytes from array to form an RomTitle
            Array.ConstrainedCopy(fileBuf, 0x134, some, 0, 16);
            lTitle.Text += Hex2String(some);



            // Display memory
            dynamicbp = new DynamicByteProvider(Mem.RAM1);
            hexBox1.ByteProvider = dynamicbp;

            /* GameBoy starts in the VBlank mode 
             * and stays there for 17 cycles only */
            retraceCounter = 131;   // from no$gmb, originally: LY_INTERVAL
            modeCounter = 17;
            Mem.IO[0x41] = 1;
            // Create I/O window
            IOMap = new IOWindow();
            IOMap.Location = new Point(80, 60);
            IOMap.Show();

            VramMap = new VramViewer();
            
            Mem.IO[0x47] = 0xFC;    // Background Palette
            Mem.IO[0x48] = 0xFF;    // Sprite 0 Palette
            Mem.IO[0x49] = 0xFF;    // Sprite 1 Palette

            UpdatePalette(BGPAL);
            UpdatePalette(OBJ0PAL);
            UpdatePalette(OBJ1PAL);

            /* Set volume to MAX, Vin to OFF
             * Set which channels to output to L/R
             * Sound is ON  */
            Mem.IO[0x24] = 0x77;    // Sound ON-OFF / Volume
            Mem.IO[0x25] = 0xF3;    // Selection of Sound output terminal
            Mem.IO[0x26] = 0xF1;    // Sound on/off

            /* LCD is ON, BG is ON */
            Mem.IO[0x40] = 0x91;    // LCDC

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
        private void button1_Click(object sender, EventArgs e)
        {
            do
            {
                CPU.step();
                if (Mem.Changed)
                {
                    MessageBox.Show("memwrite\n Replace me with event!");
                }

                cycles += cycleTbl[CPU.instr];
                
                // Update cycle counters if display enabled
                if ((Mem.IO[0x40] & IOWindow.LCDON) > 0)
                {
                    retraceCounter -= cycles;
                    modeCounter -= cycles;
                }
                cycles = 0;

            if (retraceCounter <= 0)
            {
                // INCREASE LY register & RESET retrace counter
                IOWindow.IOPorts.LY++;
                if (IOWindow.IOPorts.LY > 153)
                    // if overflown, reset it
                    IOWindow.IOPorts.LY = 0;

                retraceCounter += LY_INTERVAL;
            }

            if (modeCounter <= 0)
            {
                Mem.IO[0x41] += 1;  // LCD Stat
                Mem.IO[0x41] &= 3;
                // if mode is V-blank, but LY not in range (144..153), skip it
                if (((Mem.IO[0x41] & 3) == 1) && (IOWindow.IOPorts.LY < 144))
                    Mem.IO[0x41] += 1;
                modeCounter += modeInterval[(Mem.IO[0x41] & 3)];
            }

            if (CPU.gb_pc == breakpoint)
                autorun = 0;
            } while (autorun == 1);

            GotoPC(CPU.gb_pc);
            UpdateInfo();
            UpdateIOForm();
        }

        // Memory window cursor handler
        private void PositionChanged(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = string.Format("{1}:{0:X4}",
                (hexBox1.CurrentLine-1)*hexBox1.BytesPerLine + hexBox1.CurrentPositionInLine - 1 + dynMemOffset, dynMemMap);
        }

        // Memory window Go To handler
        private void goToToolStripMenuItem_Click(object sender, EventArgs e)
        {           
            Form Goto = new dlgGoto();
            if (Goto.ShowDialog() == DialogResult.Cancel)
                return;
            int map = dlgGoto.addr >> 12;

            switch (map)
            {
                case 0x0:
                case 0x1:
                case 0x2:
                case 0x3:
                    dynamicbp = new DynamicByteProvider(Mem.ROM);
                    dynMemMap = "ROM0";
                    dynMemOffset = 0;
                    break;
                case 0xC:
                    dynamicbp = new DynamicByteProvider(Mem.RAM0);
                    dynMemMap = "RAM0";
                    dynMemOffset = 0xC000;
                    break;
                case 0xD:
                    dynamicbp = new DynamicByteProvider(Mem.RAM1);
                    dynMemMap = "RAM1";
                    dynMemOffset = 0xD000;
                    break;
                case 0xF:
                    dynamicbp = new DynamicByteProvider(Mem.IO);
                    dynMemMap = "IO";
                    dynMemOffset = 0xFF00;
                    dlgGoto.addr &= 0xFF;
                    break;
            }
            hexBox1.ByteProvider = dynamicbp;
            hexBox1.ScrollByteIntoView(dlgGoto.addr & 0xFFF);
            hexBox1.Select(dlgGoto.addr & 0xFFF, 1);
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
                lbPC += InstrLength[Mem.readRomU8(lbPC)];
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

            if (e.KeyValue == 118)  // F7 - Trace
                bNext.PerformClick();
            if (e.KeyValue == 120)  // F9 - Run
            {
                autorun = 1;
                bNext.PerformClick();
            }
            
        }

        private void addBreakpointToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form SetBreak = new dlgGoto();
            SetBreak.Text = "Set Break on";
            if (SetBreak.ShowDialog() == DialogResult.Cancel) return;
            breakpoint = dlgGoto.addr;
        }

        private void iOMapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IOMap.Show();
        }

        private void paletteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            VramMap.Show();
        }
        

    }
}