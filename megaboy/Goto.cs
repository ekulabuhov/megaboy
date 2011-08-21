using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace megaboy
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
            this.DialogResult = DialogResult.OK;
            try
            {
                addr = ushort.Parse(tbGoto.Text, System.Globalization.NumberStyles.HexNumber);
            }
            catch (System.Exception)
            {
                MessageBox.Show(dlgGoto.ActiveForm, "Uh oh", "Error");
                DialogResult = DialogResult.Abort;
            }
            this.Close();
        }
    }
}
