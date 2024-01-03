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
        SqlDataReader dr;
        SqlCommand cm;
        frmMaintenance f;
        frmOfficial f1;
        public string DbString = @"Data Source = MUNDAS26\SQLEXPRESS; Initial Catalog = bmis; Integrated Security = True";

        public frmAddChairmanship(frmMaintenance f)
        {
            InitializeComponent();
            _sqlConnection = new SqlConnection(DbString);
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
                cm = new SqlCommand("Select *from tblChairmanship where role like '"+cboChairmanship.Text+"'",_sqlConnection);
                dr = cm.ExecuteReader();
                dr.Read();
                if (dr.HasRows)
                {
                    txtAddChairmanship.Text = dr["role"].ToString();
                    btnAdd.Text = "UPDATE";
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
        public void LoadExistingChairmanship()
        {
            try
            {
                _sqlConnection.Open();
                cm = new SqlCommand("Select role from tblChairmanship", _sqlConnection);
                dr = cm.ExecuteReader();
                while (dr.Read())
                {
                    cboChairmanship.Items.Add(dr["role"].ToString());
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

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtAddChairmanship.Text)|| !string.IsNullOrWhiteSpace(txtAddChairmanship.Text))
                {
                    if (Check.checkDuplicate("Select count(*)from tblChairmanship where role like '"+txtAddChairmanship.Text+"'")==true)
                    {
                        _sqlConnection.Open();
                        cm = new SqlCommand("Update tblChairmanship set role=@role, status=@status where role like '"+txtAddChairmanship.Text+"'",_sqlConnection);
                        cm.Parameters.AddWithValue("@role",txtAddChairmanship.Text);
                        cm.Parameters.AddWithValue("@status", "InActive");
                        cm.ExecuteNonQuery();
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
                        cm = new SqlCommand("Insert into tblChairmanship (role, status) values (@role, @status)", _sqlConnection);
                        cm.Parameters.AddWithValue("@role", txtAddChairmanship.Text);
                        cm.Parameters.AddWithValue("@status", "InActive");
                        cm.ExecuteNonQuery();
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
