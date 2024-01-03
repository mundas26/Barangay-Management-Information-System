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
        SqlCommand cm;
        SqlDataReader dr;
        frmResidentList f;
        public string DbString = @"Data Source = MUNDAS26\SQLEXPRESS; Initial Catalog = bmis; Integrated Security = True";
        public frmViewhousehold(frmResidentList f)
        {
            InitializeComponent();
            _sqlConnection = new SqlConnection(DbString);
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
                cm = new SqlCommand("Select id, address,(lname+', '+fname+' '+mname) as fullname, bdate, age from tblResident where house like '2987' and category like 'MEMBER'", _sqlConnection);
                dr = cm.ExecuteReader();
                while (dr.Read())
                {
                    viewHousehold.Rows.Add(dr["id"].ToString(), dr["fullname"].ToString(), DateTime.Parse(dr["bdate"].ToString()).ToShortDateString(), dr["age"].ToString(), dr["address"].ToString());
                }
                dr.Close();
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
