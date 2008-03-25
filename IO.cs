using System;
using System.Collections.Generic;
using System.Text;

namespace megaboy
{
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
 
            