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
    public partial class UserControl3 : UserControl
    {
        public ProgressBar InstallProgressBar;
        public UserControl3()
        {
            InitializeComponent();
            InstallProgressBar = progressBar1;
        }

        private void UserControl3_Load(object sender, EventArgs e)
        {

        }
    }
}
