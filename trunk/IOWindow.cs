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
        public const byte BGON = 1, OBJON = 2, SPRSIZE = 4, BGMAP = 8, TILEOF = 0x10, WINON = 0x20, WINMAP = 0x40, LCDON = 0x80;
        // Mode names
        string[] modes = { "H-Blank", "V-Blank", "OAM", "VRAM", };

        //public struct IO
        //{
        //    public byte LY;
        //    public byte IF;
        //    public byte IE;
        //}
        
        //public static IO IOPorts;
        
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
            lLY.Text = Memory.readIOByte(IO.LY).ToString("X2");
            lIF.Text = Memory.readIOByte(0x0F).ToString("X2");   // Interrupts Flag
            lIE.Text = Memory.readIOByte(0xFF).ToString("X2");   // Interrupts Enable
            lSCY.Text = Memory.readIOByte(0x42).ToString("X2");  // Scroll Y
            lSCX.Text = Memory.readIOByte(0x43).ToString("X2");  // Scroll X
            lSTAT.Text = Memory.readIOByte(0x41).ToString("X2"); // LCD Status
            lLCDC.Text = Memory.readIOByte(0x40).ToString("X2"); // LCD Conrol
            lSB.Text = Memory.readIOByte(0x01).ToString("X2");   // Serial transfer data
            lSC.Text = Memory.readIOByte(0x02).ToString("X2");   // Serial transfer ctrl
            lBGP.Text = Memory.readIOByte(0x47).ToString("X2");  // Background Palette
            lOBP0.Text = Memory.readIOByte(0x48).ToString("X2"); // Object 0 Palette
            lOBP1.Text = Memory.readIOByte(0x49).ToString("X2"); // Object 1 Palette
            lSVol.Text = Memory.readIOByte(0x24).ToString("X2"); // Sound ON-OFF / Volume
            lSLR.Text = Memory.readIOByte(0x25).ToString("X2");  // Selection of Sound output terminal
            lSOn.Text = Memory.readIOByte(0x26).ToString("X2");  // Sound on/off
            
            tmp = Memory.readIOByte(0x41) & 3;
            lModeNum.Text = tmp.ToString("X1");
            lMode.Text = modes[tmp];
            // FF40 - LCDC Flags
            cbBG0.Checked = ((Memory.readIOByte(0x40) & BGON) > 0);
            lBG0.Text = cbBG0.Checked == true ? "ON" : "OFF";
            cbLCD.Checked = ((Memory.readIOByte(0x40) & LCDON) > 0);
            lLCD.Text = cbLCD.Checked == true ? "ON" : "OFF";
            cbOBJ1.Checked = ((Memory.readIOByte(0x40) & OBJON) > 0);
            lOBJ1.Text = cbOBJ1.Checked == true ? "ON" : "OFF";
            cbCHR.Checked = ((Memory.readIOByte(0x40) & TILEOF) > 0);
            lCHR.Text = cbCHR.Checked == true ? "8000-8FFF" : "8800-97FF";
            cbSprSize.Checked = ((Memory.readIOByte(0x40) & SPRSIZE) > 0);
            lSprSize.Text = cbSprSize.Checked == true ? "8x16" : "8x8";
            cbBGMAP.Checked = ((Memory.readIOByte(0x40) & BGMAP) > 0);
            lBGMAP.Text = cbBGMAP.Checked == true ? "9C00-9FFF" : "9800-9BFF";
            cbWIN.Checked = ((Memory.readIOByte(0x40) & WINON) > 0);
            lWIN.Text = cbWIN.Checked == true ? "ON" : "OFF";
            cbWINMAP.Checked = ((Memory.readIOByte(0x40) & WINMAP) > 0);
            lWINMAP.Text = cbWINMAP.Checked == true ? "9C00-9FFF" : "9800-9BFF";
            // FF0F - Interrupts Flags
            cbIFVBlank.Checked = ((Memory.readIOByte(0x0F) & VBlank) > 0);
            cbIFLCD.Checked = ((Memory.readIOByte(0x0F) & LCDStat) > 0);
            cbIFTimer.Checked = ((Memory.readIOByte(0x0F) & Timer) > 0);
            cbIFSerial.Checked = ((Memory.readIOByte(0x0F) & Serial) > 0);
            cbIFJoypad.Checked = ((Memory.readIOByte(0x0F) & Joypad) > 0);
            // FFFF - Interrupts Enable
            cbIEVBlank.Checked = ((Memory.readIOByte(0xFF) & VBlank) > 0);
            cbIELCD.Checked = ((Memory.readIOByte(0xFF) & LCDStat) > 0);
            cbIETimer.Checked = ((Memory.readIOByte(0xFF) & Timer) > 0);
            cbIESerial.Checked = ((Memory.readIOByte(0xFF) & Serial) > 0);
            cbIEJoypad.Checked = ((Memory.readIOByte(0xFF) & Joypad) > 0);
        }
    }
}
