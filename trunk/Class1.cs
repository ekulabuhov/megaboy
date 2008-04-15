using System;
using System.Collections.Generic;

namespace megaboy
{   
    public partial class MainDebug
    {
        public struct PosData
        {
            public int PC, index;
        }

        PosData[] listboxPos = new PosData[30];

        /*------------For calling from main--------*/
        // Define delegate
        public delegate void updateIODelegate();
        public delegate void updatePaletteDelegate(byte PAL);
        // Create instance (null)
        public static updatePaletteDelegate UpdatePalette;
        public static updateIODelegate UpdateIOForm;
        /*-----------------------------------------*/

        /* Add here the names of the functions to
         * display near it`s hex representation */
        string AsmDisc(byte instr)
        {
            
            switch (instr)
            {
                case 0:
                    return "NOP";
                case 0xC3:  
                    return String.Format("JP {0:X4}", Memory.readRomWord(CPU.gb_pc+1));
                case 0xAF:
                    return "XOR A";
                case 0x21:
                    return String.Format("LD HL, {0:X4}", Memory.readRomWord(CPU.gb_pc+1));
                case 0x31:
                    return String.Format("LD SP, {0:X4}", Memory.readRomWord(CPU.gb_pc + 1));
                case 0xe:
                    return String.Format("LD C, {0:X2}", Memory.readRomByte(CPU.gb_pc+1));
                case 6:
                    return String.Format("LD B, {0:X2}", Memory.readRomByte(CPU.gb_pc+1));
                case 0x3e:
                    return String.Format("LD A, {0:X2}", Memory.readRomByte(CPU.gb_pc+1));
                case 0x32:
                    return "LDD (HL), A";
                case 0x36:
                    return String.Format("LD (HL),{0:X2}", Memory.readRomByte(CPU.gb_pc + 1));
                case 5:
                    return "DEC B";
                case 0xd:
                    return "DEC C";
                case 0x20:
                    return AsmForm("JR NZ, ", (ushort)(CPU.gb_pc + Memory.readRomByte(CPU.gb_pc+1) - 0xfe));
                case 0xF3:
                    return "DI";
                case 0xFF:
                    return "RST 0x38";
                case 0xE0:
                    return String.Format("LD (FF00+{0:X2}),a  ;{1}", Memory.readRomByte(CPU.gb_pc + 1), IOPortDesc[Memory.readRomByte(CPU.gb_pc+1)]);
                case 0xEA:
                    return String.Format("LD ({0:X4}),a", Memory.readRomWord(CPU.gb_pc+1));
                case 0xF0:
                    return String.Format("LD a,(FF00+{0:X2})  ;{1}", Memory.readRomByte(CPU.gb_pc + 1), IOPortDesc[Memory.readRomByte(CPU.gb_pc + 1)]);
                case 0xFE:
                    return String.Format("CP {0:X2}",Memory.readRomByte(CPU.gb_pc + 1));
            }
            return "undefined";
        }

        string AsmForm(string desc, params ushort[] arr)
        {
            string desctiption;
            desctiption = desc;
            if (arr.Length > 0) desctiption += arr[0].ToString("X4");
            return desctiption;
        }

        void Disassemble()
        {
            CPU.gb_pc = 0;
            int m = 0;
            byte[] curInstr = new byte[3];

            // Shutdown repainting
            lbDisasm.BeginUpdate();
            for (int i = 0; i <= 1024; i++)
            {
                curInstr = new byte[InstrLength[Memory.readRomByte(CPU.gb_pc)]];
                Array.ConstrainedCopy(Memory.Rom, CPU.gb_pc, curInstr, 0, InstrLength[Memory.readRomByte(CPU.gb_pc)]);
                lbDisasm.Items.Add(string.Format("ROM0:{0:X4} {1,-10} {2,-15} ;{3}", CPU.gb_pc, BitConverter.ToString(curInstr, 0), AsmDisc(Memory.readRomByte(CPU.gb_pc)), cycleTbl[curInstr[0]]));
                CPU.gb_pc += InstrLength[Memory.readRomByte(CPU.gb_pc)];

                // Position table for faster access
                if (CPU.gb_pc >= (m) * 50)
                {
                    listboxPos[m].PC = CPU.gb_pc;
                    listboxPos[m].index = lbDisasm.Items.Count;
                    m++;
                }
            }
            lbDisasm.EndUpdate();
            lbDisasm.SelectedIndex = 0;
            CPU.gb_pc = 0x100;  // restore counter
            GotoPC(0x100);
        }

        void FillIODesc()
        {
            IOPortDesc[0xF] = "int flag";
            IOPortDesc[0xFF] = "int enable";
            IOPortDesc[0x42] = "lcd scroll y";
            IOPortDesc[0x43] = "lcd scroll x";
            IOPortDesc[0x44] = "lcd line y";
            IOPortDesc[0x41] = "lcd stat";
            IOPortDesc[0x40] = "lcd ctrl";
            IOPortDesc[0x1] = "serial data";
            IOPortDesc[0x2] = "serial ctrl";
            IOPortDesc[0x47] = "lcd BG pal";
            IOPortDesc[0x48] = "lcd OBJ0 pal";
            IOPortDesc[0x49] = "lcd OBJ1 pal";
        }

        byte[] InstrLength = new byte[] 
/*0x00*/        { 1, 1, 1, 1, 1, 1, 2, 1, 1, 1, 1, 1, 1, 1, 2, 1,
/*0x10*/          1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
/*0x20*/          2, 3, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
/*0x30*/          1, 3, 1, 1, 1, 1, 2, 1, 1, 1, 1, 1, 1, 1, 2, 1,
/*0x40*/          1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
/*0x50*/          1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
/*0x60*/          1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
/*0x70*/          1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
/*0x80*/          1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
/*0x90*/          1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
/*0xA0*/          1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
/*0xB0*/          1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
/*0xC0*/          1, 1, 1, 3, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
/*0xD0*/          1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
/*0xE0*/          2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 3, 1, 1, 1, 1, 1,
/*0xF0*/          2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 1 };

        byte[] cycleTbl = new byte[] 
/*0x00*/        { 1, 1, 1, 1, 1, 1, 2, 1, 1, 1, 1, 1, 1, 1, 2, 1,
/*0x10*/          1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
/*0x20*/          2, 3, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
/*0x30*/          1, 3, 2, 1, 1, 1, 3, 1, 1, 1, 1, 1, 1, 1, 2, 1,
/*0x40*/          1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
/*0x50*/          1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
/*0x60*/          1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
/*0x70*/          1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
/*0x80*/          1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
/*0x90*/          1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
/*0xA0*/          1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
/*0xB0*/          1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
/*0xC0*/          1, 1, 1, 4, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
/*0xD0*/          1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
/*0xE0*/          3, 1, 1, 1, 1, 1, 1, 1, 1, 1, 4, 1, 1, 1, 1, 1,
/*0xF0*/          3, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 1 };

        string[] IOPortDesc = new string[0x100];
        

    }
         
}
