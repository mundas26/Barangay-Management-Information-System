using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace BMIS
{
    public partial class frmIssue : Form
    {
        SqlConnection _sqlConnection;
        SqlCommand _sqlCommand;
        SqlDataReader _sqlDataReader;
        

        [Obsolete]
        public frmIssue()
        {
            InitializeComponent();
            _sqlConnection = new SqlConnection(vars.DbString);
        }

        private void btnAddnewBlotter_Click(object sender, EventArgs e)
        {
            frmBlotter f = new frmBlotter(this);
            f.lblFileno.Text = f.GetFileNO();
            f.btnUpdate.Enabled = false;
            f.ShowDialog();
        }

        private void viewBlotter_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                string colname = viewBlotter.Columns[e.ColumnIndex].Name;
                if (colname == "btnEditBlotter")
                {
                    frmBlotter f = new frmBlotter(this);
                    f._id = viewBlotter.Rows[e.RowIndex].Cells[0].Value.ToString();
                    f.lblFileno.Text = viewBlotter.Rows[e.RowIndex].Cells[1].Value.ToString();
                    f.txtBrgy.Text = viewBlotter.Rows[e.RowIndex].Cells[2].Value.ToString();
                    f.txtPurok.Text = viewBlotter.Rows[e.RowIndex].Cells[3].Value.ToString();
                    f.txtIncident.Text = viewBlotter.Rows[e.RowIndex].Cells[4].Value.ToString();
                    f.txtPlaceofincident.Text = viewBlotter.Rows[e.RowIndex].Cells[5].Value.ToString();
                    f.DtDate.Value = DateTime.Parse(viewBlotter.Rows[e.RowIndex].Cells[6].Value.ToString());
                    f.txtTime.Text = viewBlotter.Rows[e.RowIndex].Cells[7].Value.ToString();
                    f.txtComplainant.Text = viewBlotter.Rows[e.RowIndex].Cells[8].Value.ToString();
                    f.txtWitness1.Text = viewBlotter.Rows[e.RowIndex].Cells[9].Value.ToString();
                    f.txtWitness2.Text = viewBlotter.Rows[e.RowIndex].Cells[10].Value.ToString();
                    f.txtNarrative.Text = viewBlotter.Rows[e.RowIndex].Cells[11].Value.ToString();
                    f.btnSave.Enabled = false;
                    f.ShowDialog();
                }
                else if (colname == "btnDelBlotter")
                {
                    if (MessageBox.Show("Do you want to delete is record?", vars._title, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        _sqlConnection.Open();
                        _sqlCommand = new SqlCommand("delete from tblBlotter where id like '" + viewBlotter.Rows[e.RowIndex].Cells[0].Value.ToString() + "'", _sqlConnection);
                        _sqlCommand.ExecuteNonQuery();
                        _sqlConnection.Close();
                        MessageBox.Show("Record  has been successfully deleted!", vars._title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        loadBlotter();
                    }
                }
                else if (colname == "BtnBlotterprint")
                {
                    frmPrintAllReports f = new frmPrintAllReports();
                    f.GenerateBlotterReport("Select *from tblBlotter where id like '" + viewBlotter.Rows[e.RowIndex].Cells[0].Value.ToString() + "'");
                    f.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                _sqlConnection.Close();
                MessageBox.Show(ex.Message, vars._title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void txtSearchBlotter_TextChanged(object sender, EventArgs e)
        {
            loadBlotter();
        }
        public void loadBlotter()
        {
            try
            {
                viewBlotter.Rows.Clear();
                _sqlConnection.Open();
                _sqlCommand = new SqlCommand("Select *from tblBlotter where complainant like '%" + txtSearchBlotter.Text + "%'", _sqlConnection);
                _sqlDataReader = _sqlCommand.ExecuteReader();
                while (_sqlDataReader.Read())
                {
                    viewBlotter.Rows.Add(_sqlDataReader["id"].ToString(), _sqlDataReader["fileno"].ToString(), _sqlDataReader["barangay"].ToString(), _sqlDataReader["purok"].ToString(), _sqlDataReader["incident"].ToString(), _sqlDataReader["place"].ToString(), DateTime.Parse(_sqlDataReader["idate"].ToString()).ToShortDateString(), _sqlDataReader["itime"].ToString(), _sqlDataReader["complainant"].ToString(), _sqlDataReader["witness1"].ToString(), _sqlDataReader["witness2"].ToString(), _sqlDataReader["narrative"].ToString(), _sqlDataReader["status"].ToString());
                }
                _sqlConnection.Close();
                viewBlotter.ClearSelection();
                lblTotalblotter.Text = CountRecords("Select count(*) from tblBlotter");
            }
            catch (Exception ex)
            {
                _sqlConnection.Close();
                MessageBox.Show(ex.Message, vars._title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void button1_Click_1(object sender, EventArgs e)
        {
            this.Dispose();
        }
        public string CountRecords(string sql)
        {
            _sqlConnection.Open();
            _sqlCommand = new SqlCommand(sql, _sqlConnection);
            string _count = _sqlCommand.ExecuteScalar().ToString();
            _sqlConnection.Close();
            return _count;
        }

        private void picClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
