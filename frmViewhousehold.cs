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
    public partial class frmViewhousehold : Form
    {
        SqlConnection _sqlConnection;
        SqlCommand _sqlCommand;
        SqlDataReader _sqlDataReader;
        frmResidentList f;
        
        public frmViewhousehold(frmResidentList f)
        {
            InitializeComponent();
            _sqlConnection = new SqlConnection(vars.DbString);
            this.f = f;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
        public void loadViewhousehold()
        {
            try
            {
                _sqlConnection.Open();
                viewHousehold.Rows.Clear();
                _sqlCommand = new SqlCommand("Select id, address,(lname+', '+fname+' '+mname) as fullname, bdate, age from tblResident where house like '2987' and category like 'MEMBER'", _sqlConnection);
                _sqlDataReader = _sqlCommand.ExecuteReader();
                while (_sqlDataReader.Read())
                {
                    viewHousehold.Rows.Add(_sqlDataReader["id"].ToString(), _sqlDataReader["fullname"].ToString(), DateTime.Parse(_sqlDataReader["bdate"].ToString()).ToShortDateString(), _sqlDataReader["age"].ToString(), _sqlDataReader["address"].ToString());
                }
                _sqlDataReader.Close();
                _sqlConnection.Close();
            }
            catch (Exception ex)
            {
                _sqlConnection.Close();
                MessageBox.Show(ex.Message, vars._title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
