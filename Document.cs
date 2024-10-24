using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using Microsoft.Reporting.WinForms;

namespace BMIS
{
    public partial class frmDocument : Form
    {
        SqlConnection _sqlConnection;
        SqlCommand _sqlCommand;
        SqlDataReader _sqlDataReader;
        
        public frmDocument()
        {
            InitializeComponent();
            _sqlConnection = new SqlConnection(vars.DbString);
        }
        private void btnBrgyClearance_Click(object sender, EventArgs e)
        {
            frmPrintClearance f = new frmPrintClearance(this);
            f.LoadRefnoForClearance();
            f.ShowDialog();
        }
        public void loadBusinessPermit()
        {
            try
            {
                viewBusinessPermit.Rows.Clear();
                _sqlConnection.Open();
                _sqlCommand = new SqlCommand("Select *from tblDocument where type like 'BUSINESS PERMIT' and idate = cast(getdate() as date)",_sqlConnection);
                _sqlDataReader = _sqlCommand.ExecuteReader();
                while (_sqlDataReader.Read())
                {
                    viewBusinessPermit.Rows.Add(_sqlDataReader["id"].ToString(), _sqlDataReader["refno"].ToString(), _sqlDataReader["details1"].ToString(), _sqlDataReader["details2"].ToString(), _sqlDataReader["details3"].ToString(), DateTime.Parse(_sqlDataReader["idate"].ToString()).ToShortDateString(), _sqlDataReader["user"].ToString());
                }
                _sqlDataReader.Close();
                _sqlConnection.Close();
                viewBusinessPermit.ClearSelection();
            }
            catch (Exception ex)
            {
                _sqlConnection.Close();
                MessageBox.Show(ex.Message, vars._title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        public void loadBrgyClearance()
        {
            try
            {
                viewBrgyClearance.Rows.Clear();
                _sqlConnection.Open();
                _sqlCommand = new SqlCommand("Select *from tblDocument where type like 'BARANGAY CLEARANCE' and idate = cast(getdate() as date)", _sqlConnection);
                _sqlDataReader = _sqlCommand.ExecuteReader();
                while (_sqlDataReader.Read())
                {
                    viewBrgyClearance.Rows.Add(_sqlDataReader["id"].ToString(), _sqlDataReader["refno"].ToString(), _sqlDataReader["details1"].ToString(), _sqlDataReader["details2"].ToString(), _sqlDataReader["details3"].ToString(), _sqlDataReader["details4"].ToString(), DateTime.Parse(_sqlDataReader["idate"].ToString()).ToShortDateString(), _sqlDataReader["user"].ToString());
                }
                _sqlConnection.Close();
                viewBrgyClearance.ClearSelection();
            }
            catch (Exception ex)
            {
                _sqlConnection.Close();
                MessageBox.Show(ex.Message, vars._title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        public void loadBuildingPermit()
        {
            try
            {
                viewBuildingPermit.Rows.Clear();
                _sqlConnection.Open();
                _sqlCommand = new SqlCommand("Select *from tblDocument where type like 'BARANGAY BUILDING PERMIT' and idate = cast(getdate() as date)", _sqlConnection);
                _sqlDataReader = _sqlCommand.ExecuteReader();
                while (_sqlDataReader.Read())
                {
                    viewBuildingPermit.Rows.Add(_sqlDataReader["id"].ToString(), _sqlDataReader["refno"].ToString(), _sqlDataReader["details1"].ToString(), _sqlDataReader["details2"].ToString(), _sqlDataReader["details3"].ToString(), DateTime.Parse(_sqlDataReader["idate"].ToString()).ToShortDateString(), _sqlDataReader["user"].ToString());
                }
                _sqlDataReader.Close();
                _sqlConnection.Close();
                viewBuildingPermit.ClearSelection();
            }
            catch (Exception ex)
            {
                _sqlConnection.Close();
                MessageBox.Show(ex.Message, vars._title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void btnAddBusinessPermit_Click(object sender, EventArgs e)
        {
            frmPrintBusiness f = new frmPrintBusiness(this);
            f.LoadRefNOforBusiness();
            loadBusinessPermit();
            f.ShowDialog();
        }
        private void viewBusinessPermit_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                string colname = viewBusinessPermit.Columns[e.ColumnIndex].Name;
                if (colname == "BtnPrintBusinessPermit")
                {
                    frmPrintAllReports f = new frmPrintAllReports();
                    f.GenerateBusinessReport("Select *from tblDocument where id like '" + viewBusinessPermit.Rows[e.RowIndex].Cells[0].Value.ToString() + "'");
                    f.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                _sqlConnection.Close();
                MessageBox.Show(ex.Message, vars._title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnAddnewBuildingPermit_Click(object sender, EventArgs e)
        {
            frmPrintBuildingPermit f = new frmPrintBuildingPermit(this);
            f.LoadRefnoForBuildingPermit();
            loadBuildingPermit();
            f.ShowDialog();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnAddnewBgryClearance_Click(object sender, EventArgs e)
        {
            frmPrintClearance f = new frmPrintClearance(this);
            f.LoadRefnoForClearance();
            loadBrgyClearance();
            f.ShowDialog();
        }

        private void viewBrgyClearance_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                string colname = viewBrgyClearance.Columns[e.ColumnIndex].Name;
                if (colname == "btnPrintBrgyClearance")
                {
                    frmPrintAllReports f = new frmPrintAllReports();
                    f.GenerateClearanceReport(viewBrgyClearance.Rows[e.RowIndex].Cells[3].Value.ToString(), viewBrgyClearance.Rows[e.RowIndex].Cells[4].Value.ToString(), viewBrgyClearance.Rows[e.RowIndex].Cells[5].Value.ToString(), viewBrgyClearance.Rows[e.RowIndex].Cells[6].Value.ToString(), viewBrgyClearance.Rows[e.RowIndex].Cells[1].Value.ToString());
                    f.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                _sqlConnection.Close();
                MessageBox.Show(ex.Message, vars._title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void viewBuildingPermit_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                string colname = viewBuildingPermit.Columns[e.ColumnIndex].Name;
                if (colname == "btnPrintBuildingPermit")
                {
                    frmPrintAllReports f = new frmPrintAllReports();
                    f.GenerateBuildingReport(viewBuildingPermit.Rows[e.RowIndex].Cells[2].Value.ToString(), viewBuildingPermit.Rows[e.RowIndex].Cells[3].Value.ToString(), viewBuildingPermit.Rows[e.RowIndex].Cells[4].Value.ToString(), viewBuildingPermit.Rows[e.RowIndex].Cells[1].Value.ToString());
                    f.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                _sqlConnection.Close();
                MessageBox.Show(ex.Message, vars._title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void picClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
