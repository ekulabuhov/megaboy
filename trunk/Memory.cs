using System;
using System.Collections.Generic;
using System.Text;

namespace megaboy
{
    public class Memory
    {
        /* Memory Map  */
        public byte[] ROM = new byte[0x4000];
        public byte[] RAM0 = new byte[0x1000];
        public byte[] RAM1 = new byte[0x1000];
        public byte[] IO = new byte[0x100];

        Random rnd = new Random(123);

        public bool Changed = false;
        public int Address, Value;


        public Memory()
        {
            // Fill RAM with random values
            rnd.NextBytes(RAM0);
            rnd.NextBytes(RAM1);
        }

        public void copyRom(Array source)
        {
            Array.ConstrainedCopy(source, 0, ROM, 0, 0x4000);
        }

        public ushort readRomU16(int pos)
        {
            return (ushort)(ROM[pos] + (ROM[pos + 1] << 8));
        }
        public byte readRomU8(int pos)
        {
            return ROM[pos];
        }

        public void writemem(ushort addr, byte value)
        {
            Changed = true;
            Address = addr;
            Value = value;

            int map = addr >> 12;

            switch (map)
            {
                case 0xC:       // RAM0
                    RAM0[addr & 0xFFF] = value;
                    if (MainDebug.dynMemOffset == 0xC000)
                        MainDebug.dynamicbp.WriteByte(addr & 0xFFF, value);
                    break;
                case 0xD:       // RAM1
                    RAM1[addr & 0xFFF] = value;
                    if (MainDebug.dynMemOffset == 0xD000)
                        MainDebug.dynamicbp.WriteByte(addr & 0xFFF, value);
                    break;
                case 0xF:
                    switch (addr & 0xFF00)
                    {
                        case 0xFF00:    // I/O                            
                            if (MainDebug.dynMemOffset == 0xFF00)
                                /* update only if we`re viewing IO region */
                                MainDebug.dynamicbp.WriteByte(addr & 0xFF, value);

                            switch (addr & 0xFF)
                            {
                                case 0x40:  // LCD Control
                                    IO[0x40] = value;
                                    if ((IO[0x40] & IOWindow.LCDON) == 0)
                                    {
                                        IOWindow.IOPorts.LY = 0;
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
        public byte readIOU8(int addr)
        {
            return IO[addr];
        }

    }
}
