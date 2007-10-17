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
        public struct IO
        {
            public byte LY;
        }

        public static IO IOPorts;

        public IOWindow()
        {
            InitializeComponent();
            MainDebug.UpdateIOForm = new MainDebug.updateIODelegate(UpdateIO);
            //UpdateIO(0);
        }

        public void UpdateIO()
        {
            lRetrC.Text = MainDebug.retraceCounter.ToString("D3");
            
            lLY.Text = IOPorts.LY.ToString("X2");
        }
    }
}
