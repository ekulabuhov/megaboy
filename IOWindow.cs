using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace megaboy
{
    public partial class IOWindow : Form
    {
        // Interrupt flags
        const byte VBlank = 1, LCDStat = 2, Timer = 4, Serial = 8, Joypad = 16;
        // LCDC flags
        const byte BGON = 1, OBJON = 2, LCDON = 0x80;
        // Mode names
        string[] modes = { "H-Blank", "V-Blank", "OAM", "VRAM", };

        public struct IO
        {
            public byte LY;
            public byte IF;
            public byte IE;
        }

        public static IO IOPorts;

        public IOWindow()
        {
            InitializeComponent();
            MainDebug.UpdateIOForm = new MainDebug.updateIODelegate(UpdateIO);
        }
        
        // Use it only to update info on the form, not the actual data!
        public void UpdateIO()
        {
            int tmp;

            lRetrC.Text = MainDebug.retraceCounter.ToString("D3");
            lLY.Text = IOPorts.LY.ToString("X2");
            lIF.Text = MainDebug.IO[0x0F].ToString("X2");   // Interrupts Flag
            lIE.Text = MainDebug.IO[0xFF].ToString("X2");   // Interrupts Enable
            lSCY.Text = MainDebug.IO[0x42].ToString("X2");  // Scroll Y
            lSCX.Text = MainDebug.IO[0x43].ToString("X2");  // Scroll X
            lSTAT.Text = MainDebug.IO[0x41].ToString("X2"); // LCD STAT
            lSB.Text = MainDebug.IO[0x01].ToString("X2");   // Serial transfer data
            lSC.Text = MainDebug.IO[0x02].ToString("X2");   // Serial transfer ctrl
            tmp = MainDebug.IO[0x41] & 3;
            lModeNum.Text = tmp.ToString("X1");
            lMode.Text = modes[tmp];
            // FF40 - LCDC Flags
            cbBG0.Checked = ((MainDebug.IO[0x40] & BGON) > 0);
            lBG0.Text = cbBG0.Checked == true ? "ON" : "OFF";
            cbLCD.Checked = ((MainDebug.IO[0x40] & LCDON) > 0);
            lLCD.Text = cbLCD.Checked == true ? "ON" : "OFF";
            cbOBJ1.Checked = ((MainDebug.IO[0x40] & OBJON) > 0);
            lOBJ1.Text = cbOBJ1.Checked == true ? "ON" : "OFF";
            // FF0F - Interrupts Flags
            cbIFVBlank.Checked = ((MainDebug.IO[0x0F] & VBlank) > 0);
            cbIFLCD.Checked = ((MainDebug.IO[0x0F] & LCDStat) > 0);
            cbIFTimer.Checked = ((MainDebug.IO[0x0F] & Timer) > 0);
            cbIFSerial.Checked = ((MainDebug.IO[0x0F] & Serial) > 0);
            cbIFJoypad.Checked = ((MainDebug.IO[0x0F] & Joypad) > 0);
            // FFFF - Interrupts Enable
            cbIEVBlank.Checked = ((MainDebug.IO[0xFF] & VBlank) > 0);
            cbIELCD.Checked = ((MainDebug.IO[0xFF] & LCDStat) > 0);
            cbIETimer.Checked = ((MainDebug.IO[0xFF] & Timer) > 0);
            cbIESerial.Checked = ((MainDebug.IO[0xFF] & Serial) > 0);
            cbIEJoypad.Checked = ((MainDebug.IO[0xFF] & Joypad) > 0);
        }
    }
}
