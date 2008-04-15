using System;
using System.Collections.Generic;
using System.Text;

namespace megaboy
{
    public class CustomByteProvider : Be.Windows.Forms.IByteProvider
    {
#pragma warning disable 67
        public event EventHandler LengthChanged;
        public event EventHandler Changed;
#pragma warning restore 67

        public byte ReadByte(long index)
        {
            switch (index >> 12)
            {
                case 0:
                case 1:
                case 2:
                case 3:
                    return Memory.Rom[index];
                case 0xC:
                    return Memory.Ram0[index - 0xC000];
                case 0xD:
                    return Memory.Ram1[index - 0xD000];
                case 0xF:
                    if ((index & 0xFF00) == 0xFF00)
                        return Memory.Io[index - 0xFF00];
                    goto default;
                // else fallthrough
                default: return (byte)'?';
            }
        }
        public void WriteByte(long index, byte value) 
        {
            switch (index >> 12)
            {
                case 0:
                case 1:
                case 2:
                case 3:
                    Memory.Rom[index] = value;
                    break;
                case 0xC:
                    Memory.Ram0[index - 0xC000] = value;
                    break;
                case 0xD:
                    Memory.Ram1[index - 0xD000] = value;
                    break;
                case 0xF:
                    if ((index & 0xFF00) == 0xFF00)
                        Memory.Io[index - 0xFF00] = value;
                    break;
            }
        }

        public CustomByteProvider() {  }
        public void InsertBytes(long index, byte[] bs) { }
        public void ApplyChanges() { }
        public bool HasChanges() { return false; }
        public void DeleteBytes(long index, long length) { }
        public bool SupportsWriteByte()
        {
            return true;
        }

        public bool SupportsInsertBytes()
        {
            return true;
        }

        public bool SupportsDeleteBytes()
        {
            return true;
        }

        public long Length
        {
            get
            {
                return 0x10000;
            }
        }
    }

    public static class IO
    {
        public const ushort 
            INTF = 0xFF0F,    // Interrupts Flag
            INTE = 0xFFFF,    // Interrupts Enable
            SCY = 0xFF42,     // Scroll Y
            SCX = 0xFF43,     // Scroll X
            STAT = 0xFF41,    // LCD Status
            LCDC = 0xFF40,    // LCD Conrol
            SB = 0xFF01,      // Serial transfer data
            SC = 0xFF02,      // Serial transfer ctrl
            BGP = 0xFF47,     // Background Palette
            OBP0 = 0xFF48,    // Object 0 Palette
            OBP1 = 0xFF49,    // Object 1 Palette
            SVOL = 0xFF24,    // Sound ON-OFF / Volume
            SLR = 0xFF25,     // Selection of Sound output terminal
            SON = 0xFF26,     // Sound on/off
            LY = 0xFF44;      // Y-Coordinate
    }
}
 
            