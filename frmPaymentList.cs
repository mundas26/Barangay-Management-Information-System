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
    public partial class frmPaymentList : Form
    {
        SqlConnection _sqlConnection;
        SqlCommand _sqlCommand;
        SqlDataReader _sqlDataReader;
        public string DbString = @"Data Source = MUNDAS26\SQLEXPRESS; Initial Catalog = bmis; Integrated Security = True";
        public frmPaymentList()
        {
            InitializeComponent();
            _sqlConnection = new SqlConnection(DbString);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            this.Dispose();

        }
        private void viewResident_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                string colname = viewPaymentlist.Columns[e.ColumnIndex].Name;
                if (colname == "btnDelPayment")
                {
                    if (MessageBox.Show("Do you want to delete this record?", vars._title, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        _sqlConnection.Open();
                        _sqlCommand = new SqlCommand("Delete from tblPayment where id =@id", _sqlConnection);
                        _sqlCommand.Parameters.AddWithValue("@id", viewPaymentlist.Rows[e.RowIndex].Cells[0].Value.ToString());
                        _sqlCommand.ExecuteNonQuery();
                        _sqlConnection.Close();
                        MessageBox.Show("The record has been successfully deleted!", vars._title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadPaymentTotalRecord();
                    }
                }
            }
            catch (Exception ex)
            {
                _sqlConnection.Close();
                MessageBox.Show(ex.Message, vars._title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        public void LoadPaymentTotalRecord()
        {
            try
            {
                double _amount = 0;
                viewPaymentlist.Rows.Clear();
                _sqlConnection.Open();
                _sqlCommand = new SqlCommand("Select *from tblPayment where sdate between'"+dt1.Value.ToString("yyyy-MM-dd")+"' and '"+dt2.Value.ToString("yyyy-MM-dd")+"'", _sqlConnection);
                _sqlDataReader = _sqlCommand.ExecuteReader();
                while (_sqlDataReader.Read())
                {
                    _amount += double.Parse(_sqlDataReader["amount"].ToString());
                    viewPaymentlist.Rows.Add(_sqlDataReader["id"].ToString(), _sqlDataReader["refno"].ToString(), _sqlDataReader["name"].ToString(), _sqlDataReader["type"].ToString(), _sqlDataReader["amount"].ToString(), DateTime.Parse(_sqlDataReader["sdate"].ToString()).ToString("MM-dd-yyyy"), _sqlDataReader["username"].ToString());
                }
                _sqlDataReader.Close();
                _sqlConnection.Close();
                viewPaymentlist.ClearSelection();
                lblTotalamount.Text = _amount.ToString("#,##0.00");
            }
            catch (Exception ex)
            {
                _sqlConnection.Close();
                MessageBox.Show(ex.Message, vars._title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void dt1_ValueChanged(object sender, EventArgs e)
        {
            LoadPaymentTotalRecord();
        }

        private void btnResidentAddnew_Click_1(object sender, EventArgs e)
        {
            frmPayment f = new frmPayment(this);
            f.GetReferenceNO();
            f.ShowDialog();
        }

        private void viewPaymentlist_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                string colname = viewPaymentlist.Columns[e.ColumnIndex].Name;
                if (colname == "btnDelPayment")
                {
                    if (MessageBox.Show("Do you want to delete this record?", vars._title, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        _sqlConnection.Open();
                        _sqlCommand = new SqlCommand("Delete from tblPayment where id =@id", _sqlConnection);
                        _sqlCommand.Parameters.AddWithValue("@id", viewPaymentlist.Rows[e.RowIndex].Cells[0].Value.ToString());
                        _sqlCommand.ExecuteNonQuery();
                        _sqlConnection.Close();
                        MessageBox.Show("The record has been successfully deleted!", vars._title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadPaymentTotalRecord();
                    }
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
