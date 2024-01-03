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
    public partial class frmAddPosition : Form
    {
        SqlConnection _sqlConnection;
        SqlDataReader _sqlDataReader;
        SqlCommand _sqlCommand;
        frmMaintenance f1;
        frmOfficial f;
        public string DbString = @"Data Source = .; Initial Catalog = bmis; Integrated Security = True";

        public frmAddPosition(frmMaintenance f1)
        {
            InitializeComponent();
            _sqlConnection = new SqlConnection(DbString);
            this.f1 = f1;
        }
        public frmAddPosition(frmOfficial f)
        {
            InitializeComponent();
            _sqlConnection = new SqlConnection(dbconstring.connection);
            this.f = f;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
        public void LoadExistingPostion()
        {
            try
            {
                _sqlConnection.Open();
                _sqlCommand = new SqlCommand("Select position from tblPosition", _sqlConnection);
                _sqlDataReader = _sqlCommand.ExecuteReader();
                while (_sqlDataReader.Read())
                {
                    cboPosition.Items.Add(_sqlDataReader["position"].ToString());
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

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtAddPosition.Text) || !string.IsNullOrWhiteSpace(txtAddPosition.Text))
                {
                    frmMaintenance f = new frmMaintenance();
                    if (Check.checkDuplicate("Select count(*)from tblPosition where position like '" + txtAddPosition.Text + "'") == true)
                    {
                        _sqlConnection.Open();
                        _sqlCommand = new SqlCommand("Update tblPosition set position = @position, status = @status where position like '"+txtAddPosition.Text+"'", _sqlConnection);
                        _sqlCommand.Parameters.AddWithValue("@position", txtAddPosition.Text);
                        _sqlCommand.Parameters.AddWithValue("@status", "InActive");
                        _sqlCommand.ExecuteNonQuery();
                        _sqlConnection.Close();
                        MessageBox.Show("Record has been successfully updated!", vars._title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtAddPosition.Text = "";
                        LoadExistingPostion();
                        f.LoadExistingPositionIntoDataview();
                        this.Dispose();
                    }
                    else
                    {
                        _sqlConnection.Open();
                        _sqlCommand = new SqlCommand("Insert into tblPosition (position, status) values (@position, @status)", _sqlConnection);
                        _sqlCommand.Parameters.AddWithValue("@position", txtAddPosition.Text);
                        _sqlCommand.Parameters.AddWithValue("@status", "InActive");
                        _sqlCommand.ExecuteNonQuery();
                        _sqlConnection.Close();
                        MessageBox.Show("Record has been successfully saved!", vars._title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtAddPosition.Clear();
                        LoadExistingPostion();
                        f.LoadExistingPositionIntoDataview();
                        this.Dispose();
                    }
                }
                else
                {
                    MessageBox.Show("You must fill out the blank!", vars._title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                _sqlConnection.Close();
                MessageBox.Show(ex.Message, vars._title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void cboPosition_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                _sqlConnection.Open();
                _sqlCommand = new SqlCommand("Select *from tblPosition where position like '" + cboPosition.Text + "'", _sqlConnection);
                _sqlDataReader = _sqlCommand.ExecuteReader();
                _sqlDataReader.Read();
                if (_sqlDataReader.HasRows)
                {
                    txtAddPosition.Text = _sqlDataReader["position"].ToString();
                    btnAdd.Text = "UPDATE";
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

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Do you want to delete is record?", vars._title, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    _sqlConnection.Open();
                    _sqlCommand = new SqlCommand("delete from tblPosition where position like '" + cboPosition.Text + "'", _sqlConnection);
                    _sqlCommand.ExecuteNonQuery();
                    _sqlConnection.Close();
                    MessageBox.Show("Record  has been successfully deleted!", vars._title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadExistingPostion();
                }
            }
            catch (Exception ex)
            {
                _sqlConnection.Close();
                MessageBox.Show(ex.Message, vars._title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
