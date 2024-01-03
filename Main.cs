using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace BMIS
{
    public partial class Main : Form
    {
        SqlConnection _sqlConnection;
        SqlCommand _sqlCommand;
        SqlDataReader _sqlDataReader;
        public string DbString = @"Data Source = .; Initial Catalog = bmis; Integrated Security = True";
        public readonly frmPrintBuildingPermit f1;
        public string _username = "", _password = "", _name = "", _role = "", _pic;

        [Obsolete]
        public Main(frmPrintBuildingPermit f1)
        {
            InitializeComponent();
            _sqlConnection = new SqlConnection(DbString);
            this.f1 = f1;
        }
        public readonly frmSecurity f;

        [Obsolete]
        public Main(frmSecurity f)
        {
            InitializeComponent();
            _sqlConnection = new SqlConnection(DbString);
            this.f = f;
        }
        public readonly frmLoading f3;

        [Obsolete]
        public Main(frmLoading f3)
        {
            InitializeComponent();
            _sqlConnection = new SqlConnection(DbString);
            this.f3 = f3;
        }

        private void Main_Resize(object sender, EventArgs e)
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
            if (mainPanel.Controls.Count > 0)
            {
                Control previousForm = mainPanel.Controls[0];
                mainPanel.Controls.Remove(previousForm);
                previousForm.Dispose();
            }
            frmResidentList f = new frmResidentList();
            f.TopLevel = false;
            mainPanel.Controls.Add(f);
            f.Dock = DockStyle.Fill;
            f.BringToFront();
            f.Show();
            f.loadrecordResident();
            f.loadHeadofthefamily();
            f.LoadVaccination();
        }
        private void btnPayment_Click_1(object sender, EventArgs e)
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

        [Obsolete]
        private void btnIssue_Click_1(object sender, EventArgs e)
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

        private void btnMaintainance_Click_1(object sender, EventArgs e)
        {
            // this kind of method the previous form will be remove once the other form is shown
            if (mainPanel.Controls.Count > 0)
            {
                Control previousForm = mainPanel.Controls[0];
                mainPanel.Controls.Remove(previousForm);
                previousForm.Dispose();
            }
            frmMaintenance f = new frmMaintenance();
            f.TopLevel = false;
            mainPanel.Controls.Add(f);
            f.BringToFront();
            f.LoadRecordOfficial();
            f.LoadPurok();
            f.LoadRecordAccount();
            f.LoadExistingPositionIntoDataview();
            f.LoadExistingChairmanshipIntoDataview();
            f.Show();
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
            Application.Run(new frmSecurity());
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
        private void Main_Load(object sender, EventArgs e)
        {
            try
            {
                _sqlConnection.Open();
                _sqlCommand = new SqlCommand("SELECT idPic as Picture, *from tblOfficial where position like 'KAPITAN'", _sqlConnection);
                _sqlDataReader = _sqlCommand.ExecuteReader();
                if (_sqlDataReader.Read())
                {
                    lblPositionKapitan.Text = _sqlDataReader["position"].ToString();
                    lblKapitanName.Text = _sqlDataReader["name"].ToString();
                    picKapitan.Image = Image.FromFile(_sqlDataReader["idPic"].ToString());
                }
                _sqlDataReader.Close();
                _sqlConnection.Close();
            }
            catch (Exception ex)
            {
                _sqlConnection.Close();
                MessageBox.Show(vars._title, ex.Message);
            }
        }
        
    }
}
