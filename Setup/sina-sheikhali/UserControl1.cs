using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace sina_sheikhali
{
    public partial class UserControl1 : UserControl
    {
        
        public UserControl1()
        {
            InitializeComponent();
            
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
        
            Form1 Form1 = (Form1)this.Parent;
            if (radioButton1.Checked)
            {
             
                Form1.button_2.Enabled = true;
            }else
            {
                Form1.button_2.Enabled = false;
            }

        
        }
    }
}
