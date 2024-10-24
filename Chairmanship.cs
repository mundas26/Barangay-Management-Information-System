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
    public partial class frmAddChairmanship : Form
    {
        SqlConnection _sqlConnection;
        SqlDataReader _sqlDataReader;
        SqlCommand _sqlCommand;
        frmMaintenance f;
        frmOfficial f1;
        

        public frmAddChairmanship(frmMaintenance f)
        {
            InitializeComponent();
            _sqlConnection = new SqlConnection(vars.DbString);
            this.f = f;
        }
        public frmAddChairmanship(frmOfficial f)
        {
            InitializeComponent();
            _sqlConnection = new SqlConnection(dbconstring.connection);
            this.f1 = f;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void cboChairmanship_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                _sqlConnection.Open();
                _sqlCommand = new SqlCommand("Select *from tblChairmanship where role like '"+cboChairmanship.Text+"'",_sqlConnection);
                _sqlDataReader = _sqlCommand.ExecuteReader();
                _sqlDataReader.Read();
                if (_sqlDataReader.HasRows)
                {
                    txtAddChairmanship.Text = _sqlDataReader["role"].ToString();
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
        public void LoadExistingChairmanship()
        {
            try
            {
                _sqlConnection.Open();
                _sqlCommand = new SqlCommand("Select role from tblChairmanship", _sqlConnection);
                _sqlDataReader = _sqlCommand.ExecuteReader();
                while (_sqlDataReader.Read())
                {
                    cboChairmanship.Items.Add(_sqlDataReader["role"].ToString());
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
                if (!string.IsNullOrEmpty(txtAddChairmanship.Text)|| !string.IsNullOrWhiteSpace(txtAddChairmanship.Text))
                {
                    if (Check.checkDuplicate("Select count(*)from tblChairmanship where role like '"+txtAddChairmanship.Text+"'")==true)
                    {
                        _sqlConnection.Open();
                        _sqlCommand = new SqlCommand("Update tblChairmanship set role=@role, status=@status where role like '"+txtAddChairmanship.Text+"'",_sqlConnection);
                        _sqlCommand.Parameters.AddWithValue("@role",txtAddChairmanship.Text);
                        _sqlCommand.Parameters.AddWithValue("@status", "InActive");
                        _sqlCommand.ExecuteNonQuery();
                        _sqlConnection.Close();
                        MessageBox.Show("Record has been successfully updated",vars._title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtAddChairmanship.Clear();
                        LoadExistingChairmanship();
                        f.LoadExistingChairmanshipIntoDataview();
                        this.Dispose();
                    }
                    else
                    {
                        _sqlConnection.Open();
                        _sqlCommand = new SqlCommand("Insert into tblChairmanship (role, status) values (@role, @status)", _sqlConnection);
                        _sqlCommand.Parameters.AddWithValue("@role", txtAddChairmanship.Text);
                        _sqlCommand.Parameters.AddWithValue("@status", "InActive");
                        _sqlCommand.ExecuteNonQuery();
                        _sqlConnection.Close();
                        MessageBox.Show("Record has been successfully saved", vars._title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtAddChairmanship.Clear();
                        LoadExistingChairmanship();
                        f.LoadExistingChairmanshipIntoDataview();
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
    }
}
