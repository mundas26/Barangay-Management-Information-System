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
        SqlConnection _sqlConnection;
        SqlCommand _sqlCommand;
        SqlDataReader _sqlDataReader;
        frmResidentInformation f;
        

        [Obsolete]
        public frmHousehold(frmResidentInformation f)
        {
            InitializeComponent();
            _sqlConnection = new SqlConnection(vars.DbString);
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
                _sqlConnection.Open();
                _sqlCommand = new SqlCommand("Select *from tblResident where (lname+ ',' +fname+ ' ' +mname) like '%" + txtsearchHousehold.Text + "%' and category like 'HEAD OF THE FAMILY'", _sqlConnection);
                _sqlDataReader = _sqlCommand.ExecuteReader();
                while (_sqlDataReader.Read())
                {
                    viewHousehold.Rows.Add(_sqlDataReader["id"].ToString(), _sqlDataReader["house"].ToString(), _sqlDataReader["lname"].ToString() + ", " + _sqlDataReader["fname"].ToString() + " " + _sqlDataReader["mname"].ToString(), _sqlDataReader["address"].ToString());
                }
                _sqlDataReader.Close();
                _sqlConnection.Close();
                viewHousehold.ClearSelection();
                lblRecordcount.Text = "Record count(" + viewHousehold.RowCount + ")";
            }
            catch (Exception ex)
            {
                _sqlConnection.Close();
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
