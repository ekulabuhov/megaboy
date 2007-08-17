using System;
using System.Collections.Generic;

namespace MyProj
{
    public partial class Form1
    {       
        /* Add here the names of the functions to
         * display near it`s hex representation */
        string AsmDisc(byte instr)
        {
            switch (instr)
            {
                case 0:
                    return AsmForm("NOP");
                case 0xC3:
                    return AsmForm("JP ", (ushort)(rom[gb_pc+1] + (rom[gb_pc+2]<<8)));
                case 0xAF:
                    return AsmForm("XOR A");
                case 0x21:
                    return AsmForm("LD HL, ", (ushort)(rom[gb_pc+1] + (rom[gb_pc+2]<<8)));
                case 0xe:
                    return AsmForm("LD C, ", rom[gb_pc+1]);
                case 6:
                    return AsmForm("LD B, ", rom[gb_pc+1]);
                case 0x3e:
                    return AsmForm("LD A, ", rom[gb_pc+1]);
                case 0x32:
                    return AsmForm("LDD (HL), A");
                case 5:
                    return AsmForm("DEC B");
                case 0xd:
                    return AsmForm("DEC C");
                case 0x20:
                    return AsmForm("JR NZ, ", (ushort)(gb_pc + rom[gb_pc+1] - 0xfe));
                case 0xF3:
                    return AsmForm("DI");
            }
            return null;
        }

        string AsmForm(string desc, params ushort[] arr)
        {
            string desctiption;
            desctiption = desc;
            if (arr.Length > 0) desctiption += arr[0].ToString("X4");
            return desctiption;
        }
    }
    
}
