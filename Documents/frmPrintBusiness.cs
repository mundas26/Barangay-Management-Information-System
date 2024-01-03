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

namespace BMIS
{
    public partial class frmPrintBusiness : Form
    {
        SqlConnection _sqlConnection;
        SqlCommand _sqlCommand;
        SqlDataReader _sqlDataReader;
        frmDocument f;
        public string DbString = @"Data Source = .; Initial Catalog = bmis; Integrated Security = True";
        public frmPrintBusiness(frmDocument f)
        {
            InitializeComponent();
            _sqlConnection = new SqlConnection(DbString);
            this.f = f;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
        public void LoadRefNOforBusiness()
        {
            try
            {
                cboRefNOForBusiness.Items.Clear();
                _sqlConnection.Open();
                _sqlCommand = new SqlCommand("Select refno from tblPayment where type like 'BUSINESS PERMIT' and status like 'Pending'order by id desc", _sqlConnection);
                _sqlDataReader = _sqlCommand.ExecuteReader();
                while (_sqlDataReader.Read())
                {
                    cboRefNOForBusiness.Items.Add(_sqlDataReader[0].ToString());
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

        private void btnSave_Click(object sender, EventArgs e)
        {
            var textBoxes = new[]
            { txtNameofonwer, txtNameofbusiness, txtOperationSince };
            var comboBox = new[]
            { cboRefNOForBusiness};
            foreach (var tb in textBoxes)
            {
                foreach (var cb in comboBox)
                {
                    if (string.IsNullOrEmpty(cb.Text))
                    {
                        errorProvider.SetError(cb, "Please fill out this line!");
                        return;
                    }
                    else
                    {
                        errorProvider.SetError(cb,"");
                    }
                }
                if (string.IsNullOrWhiteSpace(tb.Text))
                {
                    errorProvider.SetError(tb, "Some fields are Empty!");
                    return;
                }
                else
                {
                    errorProvider.SetError(tb, "");
                }
            }

            try
            {
                if (MessageBox.Show("Do you want to save this record?",vars._title,MessageBoxButtons.YesNo,MessageBoxIcon.Question)== DialogResult.Yes)
                {
                    string user = vars.Users;
                    _sqlConnection.Open();
                    _sqlCommand = new SqlCommand("Update tblPayment set status = 'Completed' where refno like '" + cboRefNOForBusiness.Text + "'", _sqlConnection);
                    _sqlCommand.ExecuteNonQuery();
                    _sqlConnection.Close();

                    _sqlConnection.Open();
                    _sqlCommand = new SqlCommand("Insert into tblDocument (refno, type, details1, details2, details3, idate, [user]) values(@refno, @type, @details1, @details2, @details3, @idate, @user)",_sqlConnection);
                    _sqlCommand.Parameters.AddWithValue("@refno",cboRefNOForBusiness.Text);
                    _sqlCommand.Parameters.AddWithValue("@type", "BUSINESS PERMIT");
                    _sqlCommand.Parameters.AddWithValue("@details1",txtNameofbusiness.Text);
                    _sqlCommand.Parameters.AddWithValue("@details2",txtNameofonwer.Text);
                    _sqlCommand.Parameters.AddWithValue("@details3",txtOperationSince.Text);
                    _sqlCommand.Parameters.AddWithValue("@idate", DateTime.Now);
                    _sqlCommand.Parameters.AddWithValue("@user", user);
                    _sqlCommand.ExecuteNonQuery();
                    _sqlConnection.Close();
                    MessageBox.Show("Record has been successfully saved!",vars._title,MessageBoxButtons.OK,MessageBoxIcon.Information);
                    f.loadBusinessPermit();
                    this.Dispose();
                }
            }
            catch (Exception ex)
            {
                _sqlConnection.Close();
                MessageBox.Show(ex.Message, vars._title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void cboRefNOForBusiness_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                _sqlConnection.Open();
                _sqlCommand = new SqlCommand("Select *from tblPayment where refno like'"+cboRefNOForBusiness.Text+"'",_sqlConnection);
                _sqlDataReader = _sqlCommand.ExecuteReader();
                _sqlDataReader.Read();
                if (_sqlDataReader.HasRows)
                {
                    txtNameofonwer.Text = _sqlDataReader["name"].ToString();
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
