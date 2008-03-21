using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace megaboy
{
    public partial class VramViewer : Form
    {
        Memory Mem = MainDebug.Mem;
        public VramViewer()
        {
            InitializeComponent();
            MainDebug.UpdatePalette = new MainDebug.updatePaletteDelegate(UpdatePalette);
        }

        public void UpdatePalette(byte PAL)
        {
            byte BGP;
            // BGP - palette byte, PAL - which palette to update
            // Extract needed bits
            switch (PAL)
            {
                case 1:
                    BGP = Mem.IO[0x47];
                    cBGP0.BackColor = MainDebug.defaultPal[BGP & 3];
                    cBGP1.BackColor = MainDebug.defaultPal[(BGP & 12) >> 2];
                    cBGP2.BackColor = MainDebug.defaultPal[(BGP & 48) >> 4];
                    cBGP3.BackColor = MainDebug.defaultPal[(BGP & 192) >> 6];
                    break;
                case 2:
                    BGP = Mem.IO[0x48];
                    cOBP00.BackColor = MainDebug.defaultPal[BGP & 3];
                    cOBP01.BackColor = MainDebug.defaultPal[(BGP & 12) >> 2];
                    cOBP02.BackColor = MainDebug.defaultPal[(BGP & 48) >> 4];
                    cOBP03.BackColor = MainDebug.defaultPal[(BGP & 192) >> 6];
                    break;
                case 3:
                    BGP = Mem.IO[0x49];
                    cOBP10.BackColor = MainDebug.defaultPal[BGP & 3];
                    cOBP11.BackColor = MainDebug.defaultPal[(BGP & 12) >> 2];
                    cOBP12.BackColor = MainDebug.defaultPal[(BGP & 48) >> 4];
                    cOBP13.BackColor = MainDebug.defaultPal[(BGP & 192) >> 6];
                    break;
            }

        }

        

    }
}
