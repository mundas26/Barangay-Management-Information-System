using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BMIS
{
    public partial class frmLoading : Form
    {
        [Obsolete]
        public frmLoading()
        {
            InitializeComponent();
        }

        private void frmLoading_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }

        [Obsolete]
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (progressBar1.Value < 100)
            {
                progressBar1.Value += 1;
                label2.Text = progressBar1.Value.ToString()+ "%";
            }
            else
            {
                timer1.Stop();
                Main f = new Main(this);
                f.lblName.Text = vars._defaultName;
                f.lblPosition.Text = vars._defaultRole;
                f.picMain.Image = vars._defaultImage;
                vars.Users = vars._defaultRole;
                f.Show();
                this.Hide();
            }
        }
    }
}
