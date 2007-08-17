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


namespace MyProj
{
    public partial class Form1 : Form
    {
        byte[] rom = new byte[2000];
        byte[] some = new byte[20];
        byte[] RAM1 = new byte[0x1000];
        byte[] IO = new byte[0x100];
        ushort gb_bc, gb_de, gb_hl, gb_sp, gb_pc;
        byte instr, gb_a, gb_flgs, gb_ime;
        int cycles = 0, autorun = 1;
        delegate void smthng();
        smthng[] op = new smthng[0xFF];
        Random rnd = new Random(123);
        DynamicByteProvider dynamicbp;

        const byte carryF = 1<<4, hcarryF = 1<<5, nF = 1<<6, zF = 1<<7;
        

        void Emu_Init()
        {
            // Open File
            FileStream fs = new FileStream("C:/Rom.gb", FileMode.Open);
            // Create reader
            BinaryReader r = new BinaryReader(fs);
            // Read rom to memory
            rom = r.ReadBytes(2000);
            r.Close();
            fs.Close();
            // Close it
            // Copy some bytes from array to form an RomTitle
            Array.ConstrainedCopy(rom, 0x134, some, 0, 16);
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
           // hexBox1.ScrollByteIntoView(0xA0);
            
                        
            

            instr = rom[gb_pc];
            
   
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
            
            UpdateInfo();
              
        }

        void iNOP()
        {
            instr = rom[++gb_pc];
            cycles += 1;
        }

        void iC3()  // C3 nn nn    16 ---- jump to nn, PC=nn
        {
            ushort addr;
            addr = rom[gb_pc+2];
            addr <<= 8;
            addr |= rom[gb_pc+1];
            gb_pc = addr;
            cycles += 4;
        }

        void iAF()  // xor  A           Ax         4 z000
        {
            gb_a = 0;
            gb_flgs = zF;
            gb_pc++;
        }

        void i21()  // ld HL,#nnnn    12 ----  
        {
            gb_hl = Convert.ToUInt16(rom[++gb_pc] + (rom[++gb_pc]<<8));
            gb_pc++;
        }

        void i0E()  // LD C,#nn     8 ---- r=n
        {
            gb_bc &= 0xff00;
            gb_bc |= rom[++gb_pc];
            gb_pc++;
        }

        void i06()  // LD B,#nn     8 ---- r=n
        {
            gb_bc &= 0x00ff;
            gb_bc |= (ushort)(rom[++gb_pc] << 8);
            gb_pc++;
        }

        void i3E()  // LD A,#nn     8 ---- r=n
        {
            gb_a = rom[++gb_pc];
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
            


        void i20()  // JR NZ,*      16;12 ---- jump if not zero
        {
            byte addr;
            if ((gb_flgs & zF) == 0)
            {
                addr = (byte)(rom[gb_pc+1] + gb_pc);
                gb_pc &= 0xFF00;    // Leave only high part
                gb_pc |= addr;
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
            writemem((ushort)(0xFF00 + rom[++gb_pc]), gb_a);
            gb_pc++;
        }

        // No else if`s please... Use switch instead?
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
            lInstr.Text = "op = " + instr.ToString("X2") +
                rom[gb_pc + 1].ToString("X2") + rom[gb_pc + 2].ToString("X2") +
                "; " + AsmDisc(instr);
            cbZ.Checked = ((gb_flgs & zF) > 0);
            cbN.Checked = ((gb_flgs & nF) > 0);
            cbH.Checked = ((gb_flgs & hcarryF) > 0);
            cbC.Checked = ((gb_flgs & carryF) > 0); 
        }

        public Form1()
        {
            InitializeComponent();
            Emu_Init();
        }

        string Hex2String(byte[] arr)
        {
            return System.Text.Encoding.ASCII.GetString(arr);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            do
            {
                op[instr]();
                instr = rom[gb_pc];
            } while (autorun == 1);
            UpdateInfo();
        }


        private void PositionChanged(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = string.Format("Ln {0}  Col {1} Pos {2:X2}",
                hexBox1.CurrentLine, hexBox1.CurrentPositionInLine,
                (hexBox1.CurrentLine-1)*hexBox1.BytesPerLine + hexBox1.CurrentPositionInLine - 1);
        }

        private void goToToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form Goto = new dlgGoto();
            Goto.ShowDialog();
            this.Text = dlgGoto.addr.ToString("X4");
            
        }


    }
}