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
    public partial class frmHousehold : Form
    {
        SqlConnection cn;
        SqlCommand cm;
        SqlDataReader dr;
        frmResidentInformation f;
        public string DbString = @"Data Source = MUNDAS26\SQLEXPRESS; Initial Catalog = bmis; Integrated Security = True";

        [Obsolete]
        public frmHousehold(frmResidentInformation f)
        {
            InitializeComponent();
            cn = new SqlConnection(DbString);
            this.f = f;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void txtsearchHousehold_TextChanged(object sender, EventArgs e)
        {
            loadRecord();
        }
        public void loadRecord()
        {
            try
            {
                viewHousehold.Rows.Clear();
                cn.Open();
                cm = new SqlCommand("Select *from tblResident where (lname+ ',' +fname+ ' ' +mname) like '%" + txtsearchHousehold.Text + "%' and category like 'HEAD OF THE FAMILY'", cn);
                dr = cm.ExecuteReader();
                while (dr.Read())
                {
                    viewHousehold.Rows.Add(dr["id"].ToString(), dr["house"].ToString(), dr["lname"].ToString() + ", " + dr["fname"].ToString() + " " + dr["mname"].ToString(), dr["address"].ToString());
                }
                dr.Close();
                cn.Close();
                viewHousehold.ClearSelection();
                lblRecordcount.Text = "Record count(" + viewHousehold.RowCount + ")";
            }
            catch (Exception ex)
            {
                cn.Close();
                MessageBox.Show(ex.Message, vars._title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void viewHousehold_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colname = viewHousehold.Columns[e.ColumnIndex].Name;
            if (colname == "btnSelectHousdehold")
            {
                f.txtHouse.Text = viewHousehold.Rows[e.RowIndex].Cells[1].Value.ToString();
                f.txtHeadofthefamily.Text = viewHousehold.Rows[e.RowIndex].Cells[2].Value.ToString();
                this.Dispose();
            }
        }
    }
}
