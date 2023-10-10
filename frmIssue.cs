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
        SqlConnection cn;
        SqlCommand cm;
        SqlDataReader dr;
        public string DbString = @"Data Source = MUNDAS26\SQLEXPRESS; Initial Catalog = bmis; Integrated Security = True";

        [Obsolete]
        public frmIssue()
        {
            InitializeComponent();
            cn = new SqlConnection(DbString);
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
                        cn.Open();
                        cm = new SqlCommand("delete from tblBlotter where id like '" + viewBlotter.Rows[e.RowIndex].Cells[0].Value.ToString() + "'", cn);
                        cm.ExecuteNonQuery();
                        cn.Close();
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
                cn.Close();
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
                cn.Open();
                cm = new SqlCommand("Select *from tblBlotter where complainant like '%" + txtSearchBlotter.Text + "%'", cn);
                dr = cm.ExecuteReader();
                while (dr.Read())
                {
                    viewBlotter.Rows.Add(dr["id"].ToString(), dr["fileno"].ToString(), dr["barangay"].ToString(), dr["purok"].ToString(), dr["incident"].ToString(), dr["place"].ToString(), DateTime.Parse(dr["idate"].ToString()).ToShortDateString(), dr["itime"].ToString(), dr["complainant"].ToString(), dr["witness1"].ToString(), dr["witness2"].ToString(), dr["narrative"].ToString(), dr["status"].ToString());
                }
                cn.Close();
                viewBlotter.ClearSelection();
                lblTotalblotter.Text = CountRecords("Select count(*) from tblBlotter");
            }
            catch (Exception ex)
            {
                cn.Close();
                MessageBox.Show(ex.Message, vars._title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void button1_Click_1(object sender, EventArgs e)
        {
            this.Dispose();
        }
        public string CountRecords(string sql)
        {
            cn.Open();
            cm = new SqlCommand(sql, cn);
            string _count = cm.ExecuteScalar().ToString();
            cn.Close();
            return _count;
        }

        private void picClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
