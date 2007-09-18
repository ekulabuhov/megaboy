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
        // Create instance (null)
        public static updateIODelegate UpdateIOForm;
        /*-----------------------------------------*/

        /* Add here the names of the functions to
         * display near it`s hex representation 
         * Why the fuck am I using AsmForm?  */
        string AsmDisc(byte instr)
        {
            switch (instr)
            {
                case 0:
                    return "NOP";
                case 0xC3:
                    return AsmForm("JP ", (ushort)(ROM[gb_pc+1] + (ROM[gb_pc+2]<<8)));
                case 0xAF:
                    return AsmForm("XOR A");
                case 0x21:
                    return AsmForm("LD HL, ", (ushort)(ROM[gb_pc+1] + (ROM[gb_pc+2]<<8)));
                case 0xe:
                    return AsmForm("LD C, ", ROM[gb_pc+1]);
                case 6:
                    return AsmForm("LD B, ", ROM[gb_pc+1]);
                case 0x3e:
                    return AsmForm("LD A, ", ROM[gb_pc+1]);
                case 0x32:
                    return AsmForm("LDD (HL), A");
                case 5:
                    return AsmForm("DEC B");
                case 0xd:
                    return AsmForm("DEC C");
                case 0x20:
                    return AsmForm("JR NZ, ", (ushort)(gb_pc + ROM[gb_pc+1] - 0xfe));
                case 0xF3:
                    return AsmForm("DI");
                case 0xFF:
                    return AsmForm("RST 0x38");
                case 0xE0:
                    return String.Format("LD (FF00+{0:X2}),a ;{1}", ROM[gb_pc + 1], IOPortDesc[ROM[gb_pc+1]]);
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
            gb_pc = 0;
            int m = 0;
            byte[] curInstr = new byte[3];
            
            for (int i = 0; i <= 1024; i++)
            {
                curInstr = new byte[InstrLength[ROM[gb_pc]]];
                Array.ConstrainedCopy(ROM, gb_pc, curInstr, 0, InstrLength[ROM[gb_pc]]);
                lbDisasm.Items.Add(string.Format("ROM0:{0:X4} {1,-10} {2,-15} ;{3}", gb_pc, BitConverter.ToString(curInstr, 0), AsmDisc(ROM[gb_pc]), cycleTbl[curInstr[0]]));
                gb_pc += InstrLength[ROM[gb_pc]];

                // Position table for faster access
                if (gb_pc >= (m) * 50)
                {
                    listboxPos[m].PC = gb_pc;
                    listboxPos[m].index = lbDisasm.Items.Count;
                    m++;
                }
            }
            lbDisasm.SelectedIndex = 0;
            gb_pc = 0x100;  // restore counter
            GotoPC(0x100);
        }

        void FillIODesc()
        {
            IOPortDesc[0xF] = "int flag";
            IOPortDesc[0xFF] = "int enable";
            IOPortDesc[0x42] = "lcd scroll y";
            IOPortDesc[0x43] = "lcd scroll x";
            IOPortDesc[0x41] = "lcd stat";
            IOPortDesc[0x40] = "lcd ctrl";
            IOPortDesc[0x1] = "serial data";
            IOPortDesc[0x2] = "serial ctrl";
        }

        byte[] InstrLength = new byte[] 
/*0x00*/        { 1, 1, 1, 1, 1, 1, 2, 1, 1, 1, 1, 1, 1, 1, 2, 1,
/*0x10*/          1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
/*0x20*/          2, 3, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
/*0x30*/          1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 1,
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
/*0xE0*/          2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
/*0xF0*/          1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };

        byte[] cycleTbl = new byte[] 
/*0x00*/        { 1, 1, 1, 1, 1, 1, 2, 1, 1, 1, 1, 1, 1, 1, 2, 1,
/*0x10*/          1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
/*0x20*/          2, 3, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
/*0x30*/          1, 1, 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 1,
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
/*0xE0*/          3, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
/*0xF0*/          1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };

        string[] IOPortDesc = new string[0x100];
        

    }
         
}
