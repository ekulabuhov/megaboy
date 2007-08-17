using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MyProj
{
    public partial class dlgGoto : Form
    {
        public static ushort addr;
        public dlgGoto()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                addr = ushort.Parse(tbGoto.Text, System.Globalization.NumberStyles.HexNumber);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Uh oh");
            }
            this.Close();
        }
    }
}
