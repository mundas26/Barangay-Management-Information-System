using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BMIS
{
    public partial class MainBrgy : Form
    {
        private readonly frmSecurity f;

        [Obsolete]
        public MainBrgy(frmSecurity f)

        {
            InitializeComponent();
            this.f = f;
            /*List<Control> ctrl = new List<Control>((IEnumerable<Control>)mainPanel.Controls);
            mainPanel.Controls.Clear();
            foreach (Control c in ctrl)
            {
                mainPanel.Controls.Remove(c);
                c.Dispose();
            }*/
        }

        private void MainBrgy_Resize(object sender, EventArgs e)
        {
            int y = Screen.PrimaryScreen.Bounds.Height;
            int x = Screen.PrimaryScreen.Bounds.Width;
            this.Height = y - 40;
            this.Width = x;
            this.Left = 0;
            this.Top = 0;
        }

        [Obsolete]
        private void btnResident_Click(object sender, EventArgs e)
        {
            // this kind of method the previous form will be remove once the other form is shown
            if (mainPanel.Controls.Count > 0)
            {
                Control previousForm = mainPanel.Controls[0];
                mainPanel.Controls.Remove(previousForm);
                previousForm.Dispose();
            }
            frmResidentList f = new frmResidentList();
            f.TopLevel = false;
            mainPanel.Controls.Add(f);
            f.BringToFront();
            f.Show();
            f.loadrecordResident();
            f.loadHeadofthefamily();
            f.LoadVaccination();
        }

        private void btnDocument_Click(object sender, EventArgs e)
        {
            // this kind of method the previous form will be remove once the other form is shown
            if (mainPanel.Controls.Count > 0)
            {
                Control previousForm = mainPanel.Controls[0];
                mainPanel.Controls.Remove(previousForm);
                previousForm.Dispose();
            }
            frmDocument f = new frmDocument();
            f.TopLevel = false;
            mainPanel.Controls.Add(f);
            f.loadBusinessPermit();
            f.loadBrgyClearance();
            f.loadBuildingPermit();
            f.BringToFront();
            f.Show();
        }

        private void btnPayment_Click(object sender, EventArgs e)
        {
            // this kind of method the previous form will be remove once the other form is shown
            if (mainPanel.Controls.Count > 0)
            {
                Control previousForm = mainPanel.Controls[0];
                mainPanel.Controls.Remove(previousForm);
                previousForm.Dispose();
            }
            frmPaymentList f = new frmPaymentList();
            f.TopLevel = false;
            mainPanel.Controls.Add(f);
            f.LoadPaymentTotalRecord();
            f.Show();
        }

        [Obsolete]
        private void btnIssue_Click(object sender, EventArgs e)
        {
            // this kind of method the previous form will be remove once the other form is shown
            if (mainPanel.Controls.Count > 0)
            {
                Control previousForm = mainPanel.Controls[0];
                mainPanel.Controls.Remove(previousForm);
                previousForm.Dispose();
            }
            frmIssue f = new frmIssue();
            f.TopLevel = false;
            mainPanel.Controls.Add(f);
            f.loadBlotter();
            f.Show();
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            // this kind of method the previous form will be remove once the other form is shown
            if (mainPanel.Controls.Count > 0)
            {
                Control previousForm = mainPanel.Controls[0];
                mainPanel.Controls.Remove(previousForm);
                previousForm.Dispose();
            }
        }

        [Obsolete]
        private void button8_Click(object sender, EventArgs e)
        {
            Thread t = new Thread(OpenLoginForm);
            t.SetApartmentState(ApartmentState.STA); // Set the thread to STA mode
            t.Start();
            this.Dispose();
        }

        [Obsolete]
        public static void OpenLoginForm()
        {
            Application.Run(new frmSecurity()); //run your new form
        }

        private void MainBrgy_Load(object sender, EventArgs e)
        {
            
        }

        private void btnProject_Click(object sender, EventArgs e)
        {
            // this kind of method the previous form will be remove once the other form is shown
            if (mainPanel.Controls.Count > 0)
            {
                Control previousForm = mainPanel.Controls[0];
                mainPanel.Controls.Remove(previousForm);
                previousForm.Dispose();
            }
        }
    }
}
