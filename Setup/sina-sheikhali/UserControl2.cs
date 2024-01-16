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
    public partial class UserControl2 : UserControl
    {
        
        public string Text_b { set => Address.Text = value; }
        public TextBox AppAddress;
        public CheckBox Shortcut;
        public CheckBox Default;

        public UserControl2()
        {
            InitializeComponent();
            AppAddress = Address;
            Shortcut = checkBox1;
            Default = checkBox2;
            
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            

            using (var dialog = new FolderBrowserDialog())
            {

                DialogResult result = dialog.ShowDialog();
                if (result == System.Windows.Forms.DialogResult.OK)
                {
                    Address.Text = dialog.SelectedPath;
                    
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            Form1 Form1 = (Form1)this.Parent;
            if (Address.Text == "")
            {
                Form1.button_2.Enabled = false;

            }
            else
            {
                Form1.button_2.Enabled = true;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
