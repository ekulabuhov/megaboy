using System;
using System.Collections.Generic;
using System.Text;

namespace megaboy
{
    class Z80
    {
        public ushort gb_bc, gb_de, gb_hl, gb_sp, gb_pc;
        public byte instr, gb_a, gb_flgs, gb_ime;

        public byte carryF = 1 << 4, hcarryF = 1 << 5, nF = 1 << 6, zF = 1 << 7;

        /* OP Pointers  */
        public delegate void smthng();
        public smthng[] op = new smthng[0xFF];

        public Z80()
        {
            // Init regs
            gb_a = 0x01;
            gb_flgs = 0xB0;
            gb_bc = 0x0013;
            gb_de = 0x00D8;
            gb_hl = 0x014D;
            gb_sp = 0xFFFE;
            gb_pc = 0x0100;

            op[0] = iNOP;
            op[0xC3] = iC3;
            op[0xAF] = iAF;
            op[0x21] = i21;
            op[0x0E] = i0E;
            op[0x06] = i06;
            op[0x31] = i31;
            op[0x32] = i32;
            op[0x36] = i36;
            op[0x05] = i05;
            op[0x0D] = i0D;
            op[0x20] = i20;
            op[0x3E] = i3E;
            op[0xF3] = iF3;
            op[0xE0] = iE0;
            op[0xEA] = iEA;
            op[0xF0] = iF0;
            op[0xFE] = iFE;
        }

        public void toggleZFlag()
        {
            gb_flgs ^= zF;
        }
        public void toggleNFlag()
        {
            gb_flgs ^= nF;
        }
        public void toggleHCarryFlag()
        {
            gb_flgs ^= hcarryF;
        }
        public void toggleCarryFlag()
        {
            gb_flgs ^= carryF;
        }
        public string readRegAF()
        {
            return gb_a.ToString("X2") + gb_flgs.ToString("X2");
        }

        public void step()
        {
            instr = Memory.readRomByte(gb_pc);
            op[instr]();   
        }

        void iNOP()
        {
            gb_pc++;
        }

        void iC3()  // C3 nn nn    16 ---- jump to nn, PC=nn
        {
            gb_pc = Memory.readRomWord(gb_pc+1);
        }

        void iAF()  // xor  A           Ax         4 z000
        {
            gb_a = 0;
            gb_flgs = zF;
            gb_pc++;
        }

        void i21()  // LD HL,#nnnn    12 ----  
        {
            gb_hl = Memory.readRomWord(gb_pc + 1);
            gb_pc+=3;
        }

        void i0E()  // LD C,#nn     8 ---- r=n
        {
            gb_bc &= 0xff00;
            gb_bc |= Memory.readRomByte(++gb_pc);
            gb_pc++;
        }

        void i06()  // LD B,#nn     8 ---- r=n
        {
            gb_bc &= 0x00ff;
            gb_bc |= (ushort)(Memory.readRomByte(++gb_pc) << 8);
            gb_pc++;
        }

        void i3E()  // LD A,#nn     8 ---- r=n
        {
            gb_a = Memory.readRomByte(++gb_pc);
            gb_pc++;
        }

        void i31()  // LD SP,#nnnn  12 ---- SP=nnnn
        {
            gb_sp = Memory.readRomWord(gb_pc);
            gb_pc+=3;
        }

        void i32()  // LDD (HL),A	8 ---- (HL)=A, HL=HL-1
        {
            Memory.writeMem(gb_hl--, gb_a);
            gb_pc++;
        }

        void i36()  // LD (HL),#nn  12 ---- (HL)=n
        {
            Memory.writeMem(gb_hl, Memory.readRomByte(++gb_pc));
            gb_pc++;
        }

        void i05()  // dec  B       4 z1h- 
        {
            gb_flgs &= carryF;      // Save carry (fMem.ROM goomba)
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
            gb_flgs &= carryF;      // Save carry (fMem.ROM goomba)
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
                addr = (byte)(Memory.readRomByte(gb_pc + 1) + gb_pc);
                gb_pc &= 0xFF00;    // Leave only high part
                gb_pc |= addr;
                MainDebug.cycles += 1;    
            }
            gb_pc += 2;
        }

        void iF3()  // DI           4 ---- disable interrupts, IME=0
        {
            gb_ime = 0;
            gb_pc++;
        }

        void iE0()  // LD (FFnn),A  12 ---- write to io-port n (memory FF00+n)
        {
            Memory.writeMem((ushort)(0xFF00 + Memory.readRomByte(++gb_pc)), gb_a);
            gb_pc++;
        }

        void iEA()  // LD (nn),A    16 ----
        {
            Memory.writeMem((ushort)Memory.readRomWord(gb_pc+1), gb_a);
            gb_pc+=3;
        }

        void iF0()  // LD A,(FFnn)  12 ---- read from io-port n (memory FF00+n)
        {
            gb_a = Memory.readIOByte(0xFF00 + Memory.readRomByte(++gb_pc));
            gb_pc++;
        }

        void iFE()  // CP A, nn     8 z1hc compare A-n
        {
            byte cmpval = Memory.readRomByte(++gb_pc);
            gb_flgs = nF;

            if (((gb_a & 0xF) - (cmpval & 0xF)) < 0)
                gb_flgs |= hcarryF;

            if ((gb_a - cmpval) < 0)
                gb_flgs |= carryF;

            if ((gb_a - cmpval) == 0)
                gb_flgs |= zF;

            gb_pc++;
        }


    }
}
