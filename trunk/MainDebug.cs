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

        /* Memory Map  */
        byte[] ROM = new byte[0x4000];
        byte[] some = new byte[20];
        byte[] RAM1 = new byte[0x1000];
        byte[] IO = new byte[0x100];

        /* CPU  */
        ushort gb_bc, gb_de, gb_hl, gb_sp, gb_pc;
        byte instr, gb_a, gb_flgs, gb_ime;

        /* Misc  */
        int cycles = 0, autorun = 0, lbPC;
        public static int retraceCounter;
        ushort breakpoint;

        /* OP Pointers  */
        delegate void smthng();
        smthng[] op = new smthng[0xFF];

        /* HexViewer  */
        Random rnd = new Random(123);
        DynamicByteProvider dynamicbp;
        string dynMemMap = "RAM1";
        ushort dynMemOffset = 0xD000;

        /* Constants  */
        const int LY_INTERVAL = 114;
        const byte carryF = 1<<4, hcarryF = 1<<5, nF = 1<<6, zF = 1<<7;
        
        
        void Emu_Init()
        {
            // Open File
            FileStream fs = new FileStream("C:/Rom.gb", FileMode.Open);
            // Create reader
            BinaryReader r = new BinaryReader(fs);
            // Read rom to memory
            ROM = r.ReadBytes(0x4000);
            r.Close();
            fs.Close();
            // Close it
            // Copy some bytes from array to form an RomTitle
            Array.ConstrainedCopy(ROM, 0x134, some, 0, 16);
            lTitle.Text += Hex2String(some);
            // Init regs
            gb_a = 0x01;
            gb_flgs = 0xB0;
            gb_bc = 0x0013;
            gb_de = 0x00D8;
            gb_hl = 0x014D;
            gb_sp = 0xFFFE;
            gb_pc = 0x0100;

            // Fill RAM with random values
            rnd.NextBytes(RAM1);

            // Display memory
            dynamicbp = new DynamicByteProvider(RAM1);
            hexBox1.ByteProvider = dynamicbp;

            retraceCounter = 131;   // from no$gmb, originally: LY_INTERVAL
            IOMap = new IOWindow();
            IOMap.Location = new Point(80, 100);
            IOMap.Show();
            UpdateIOForm();
                               
            
            

            instr = ROM[gb_pc];
            
   
            op[0] = iNOP;
            op[0xC3] = iC3;
            op[0xAF] = iAF;
            op[0x21] = i21;
            op[0x0E] = i0E;
            op[0x06] = i06;
            op[0x32] = i32;
            op[0x05] = i05;
            op[0x0D] = i0D;
            op[0x20] = i20;
            op[0x3E] = i3E;
            op[0xF3] = iF3;
            op[0xE0] = iE0;

            FillIODesc();
            
            UpdateInfo();
              
        }

        void iNOP()
        {
            gb_pc++;
        }

        void iC3()  // C3 nn nn    16 ---- jump to nn, PC=nn
        {
            ushort addr;
            addr = ROM[gb_pc+2];
            addr <<= 8;
            addr |= ROM[gb_pc+1];
            gb_pc = addr;            
        }

        void iAF()  // xor  A           Ax         4 z000
        {
            gb_a = 0;
            gb_flgs = zF;
            gb_pc++;
        }

        void i21()  // ld HL,#nnnn    12 ----  
        {
            gb_hl = Convert.ToUInt16(ROM[++gb_pc] + (ROM[++gb_pc]<<8));
            gb_pc++;
        }

        void i0E()  // LD C,#nn     8 ---- r=n
        {
            gb_bc &= 0xff00;
            gb_bc |= ROM[++gb_pc];
            gb_pc++;
        }

        void i06()  // LD B,#nn     8 ---- r=n
        {
            gb_bc &= 0x00ff;
            gb_bc |= (ushort)(ROM[++gb_pc] << 8);
            gb_pc++;
        }

        void i3E()  // LD A,#nn     8 ---- r=n
        {
            gb_a = ROM[++gb_pc];
            gb_pc++;
        }

        void i32()  // LDD (HL),A	8 ---- (HL)=A, HL=HL-1
        {
            writemem(gb_hl--, gb_a);
            gb_pc++;
        }

        void i05()  // dec  B       4 z1h- 
        {
            gb_flgs &= carryF;      // Save carry (from goomba)
            gb_flgs |= nF;
            if ((gb_bc & 0x0F00) == 0)
                gb_flgs |= hcarryF;
            gb_bc -= 0x0100;
            if ((gb_bc & 0xFF00) == 0)
                gb_flgs |= zF;
            gb_pc++;
        }

        void i0D()  // dec  C      4 z1h-
        {
            ushort c;
            c = (ushort)(gb_bc << 8);
            gb_flgs &= carryF;      // Save carry (from goomba)
            gb_flgs |= nF;
            if ((c & 0x0F00) == 0)
                gb_flgs |= hcarryF;
            c -= 0x0100;
            if ((c & 0xFF00) == 0)
                gb_flgs |= zF;
            gb_bc &= 0xFF00;
            gb_bc |= (ushort)(c >> 8);
            gb_pc++;
        }
            
        void i20()  // JR NZ,*      12;8 ---- relative jump if not zero
        {
            byte addr;
            if ((gb_flgs & zF) == 0)
            {
                addr = (byte)(ROM[gb_pc+1] + gb_pc);
                gb_pc &= 0xFF00;    // Leave only high part
                gb_pc |= addr;
                cycles += 1;
            }
            gb_pc+=2;
        }

        void iF3()  // DI           4 ---- disable interrupts, IME=0
        {
            gb_ime = 0;
            gb_pc++;
        }

        void iE0()  // LD (FFnn),A  12 ---- write to io-port n (memory FF00+n)
        {
            writemem((ushort)(0xFF00 + ROM[++gb_pc]), gb_a);
            gb_pc++;
        }


        void writemem(ushort addr, byte value)
        {
            int map = addr >> 12;
            
            switch(map)
            {
                case 0xD:       // RAM1
                    RAM1[addr & 0xFFF] = value;
                    dynamicbp.WriteByte(addr & 0xFFF, value);
                    if (autorun==0) hexBox1.Refresh();
                    break;
                case 0xF:       // I/O
                    IO[addr & 0xFF] = value;
                    break;
            }
        }

            

        void UpdateInfo()
        {
            lAF.Text = "AF = " + gb_a.ToString("X2") + gb_flgs.ToString("X2");
            lBC.Text = "BC = " + gb_bc.ToString("X4");
            lDE.Text = "DE = " + gb_de.ToString("X4");
            lHL.Text = "HL = " + gb_hl.ToString("X4");
            lSP.Text = "SP = " + gb_sp.ToString("X4");
            lPC.Text = "PC = " + gb_pc.ToString("X4");
            cbZ.Checked = ((gb_flgs & zF) > 0);
            cbN.Checked = ((gb_flgs & nF) > 0);
            cbH.Checked = ((gb_flgs & hcarryF) > 0);
            cbC.Checked = ((gb_flgs & carryF) > 0);
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
                op[instr]();
                cycles += cycleTbl[instr];
                instr = ROM[gb_pc];
            

            retraceCounter -= cycles;
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
            if (gb_pc == breakpoint)
                autorun = 0;
            } while (autorun == 1);

            GotoPC(gb_pc);
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
                    dynamicbp = new DynamicByteProvider(ROM);
                    dynMemMap = "ROM0";
                    dynMemOffset = 0;
                    break;
                case 0xD:
                    dynamicbp = new DynamicByteProvider(RAM1);
                    dynMemMap = "RAM1";
                    dynMemOffset = 0xD000;
                    break;
                case 0xF:
                    dynamicbp = new DynamicByteProvider(IO);
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
                lbPC += InstrLength[ROM[lbPC]];
                index++;
            }
            lbDisasm.TopIndex = index - 3;
            lbDisasm.SelectedIndex = index;

        }

        private void cbZ_Click(object sender, EventArgs e)
        {
            gb_flgs ^= zF;
            lAF.Text = "AF = " + gb_a.ToString("X2") + gb_flgs.ToString("X2");
        }
        private void cbN_Click(object sender, EventArgs e)
        {
            gb_flgs ^= nF;
            lAF.Text = "AF = " + gb_a.ToString("X2") + gb_flgs.ToString("X2");
        }
        private void cbH_Click(object sender, EventArgs e)
        {
            gb_flgs ^= hcarryF;
            lAF.Text = "AF = " + gb_a.ToString("X2") + gb_flgs.ToString("X2");
        }
        private void cbC_Click(object sender, EventArgs e)
        {
            gb_flgs ^= carryF;
            lAF.Text = "AF = " + gb_a.ToString("X2") + gb_flgs.ToString("X2");
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
        

    }
}