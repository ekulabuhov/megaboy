using System;
using System.Collections.Generic;
using System.Text;

namespace megaboy
{
    public static class Memory
    {
        /* Memory Map  */
        static byte[] ROM = new byte[0x4000];
        static byte[] RAM0 = new byte[0x1000];
        static byte[] RAM1 = new byte[0x1000];
        static byte[] IO = new byte[0x100];

        static Random rnd = new Random(123);

        static Memory()
        {
            // Fill RAM with random values
            rnd.NextBytes(RAM0);
            rnd.NextBytes(RAM1);
        }

        public static void copyRom(Array source)
        {
            Array.ConstrainedCopy(source, 0, ROM, 0, 0x4000);
        }

        public static byte[] Rom
        {
            get
            {
                return ROM;
            }
        }
        public static byte[] Ram0
        {
            get
            {
                return RAM0;
            }
        }
        public static byte[] Ram1
        {
            get
            {
                return RAM1;
            }
        }
        public static byte[] Io
        {
            get { return IO; }
            
        }

        public static ushort readRomWord(int pos)
        {
            return (ushort)(ROM[pos] + (ROM[pos + 1] << 8));
        }
        public static byte readRomByte(int pos)
        {
            return ROM[pos];
        }
        public static byte readIOByte(int addr)
        {
            return IO[addr & 0xFF];
        }
        public static void setIO(ushort addr, byte value)
        {
            // Only for unmanaged writes
            IO[addr & 0xFF] = value;
        }


        public static void writeMem(ushort addr, byte value)
        {
            // Create event memorywrite
            // Move dynamicbp and hexbox.refresh there

            int map = addr >> 12;

            MainDebug.dynamicbp.WriteByte(addr, value);

            switch (map)
            {
                case 0xC:       // RAM0
                    RAM0[addr & 0xFFF] = value;
                    break;
                case 0xD:       // RAM1
                    RAM1[addr & 0xFFF] = value;
                    break;
                case 0xF:
                    switch (addr & 0xFF00)
                    {
                        case 0xFF00:    // I/O                            
                            switch (addr & 0xFF)
                            {
                                case 0x40:  // LCD Control
                                    IO[0x40] = value;
                                    if ((IO[0x40] & IOWindow.LCDON) == 0)
                                    {
                                        IO[0x44] = 0;   // LY
                                        IO[0x41] = 0;
                                    }
                                    break;
                                case 0x41:  // LCD Stat, 3 last bits are read only
                                    value &= 0xf8;
                                    IO[0x41] = (byte)(value | (IO[0x41] & 3));
                                    break;
                                case 0x47:
                                    IO[0x47] = value;
                                    //UpdatePalette(BGPAL);
#warning UpdatePalette died here
                                    break;
                                case 0x48:
                                    IO[0x48] = value;
                                    //UpdatePalette(OBJ0PAL);
                                    break;
                                case 0x49:
                                    IO[0x49] = value;
                                    //UpdatePalette(OBJ1PAL);
                                    break;
                                default:    // Other I/O
                                    IO[addr & 0xFF] = value;
                                    break;
                            }
                            break;
                    }
                    break;
            }
            //if (autorun == 0) hexBox1.Refresh();
        }


    }
}
