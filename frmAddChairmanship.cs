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
        SqlConnection cn;
        SqlDataReader dr;
        SqlCommand cm;
        frmMaintenance f;
        frmOfficial f1;
        public string DbString = @"Data Source = MUNDAS26\SQLEXPRESS; Initial Catalog = bmis; Integrated Security = True";

        public frmAddChairmanship(frmMaintenance f)
        {
            InitializeComponent();
            cn = new SqlConnection(DbString);
            this.f = f;
        }
        public frmAddChairmanship(frmOfficial f)
        {
            InitializeComponent();
            cn = new SqlConnection(dbconstring.connection);
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
                cn.Open();
                cm = new SqlCommand("Select *from tblChairmanship where role like '"+cboChairmanship.Text+"'",cn);
                dr = cm.ExecuteReader();
                dr.Read();
                if (dr.HasRows)
                {
                    txtAddChairmanship.Text = dr["role"].ToString();
                    btnAdd.Text = "UPDATE";
                }
                dr.Close();
                cn.Close();
            }
            catch (Exception ex)
            {
                cn.Close();
                MessageBox.Show(ex.Message, vars._title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        public void LoadExistingChairmanship()
        {
            try
            {
                cn.Open();
                cm = new SqlCommand("Select role from tblChairmanship", cn);
                dr = cm.ExecuteReader();
                while (dr.Read())
                {
                    cboChairmanship.Items.Add(dr["role"].ToString());
                }
                dr.Close();
                cn.Close();
            }
            catch (Exception ex)
            {
                cn.Close();
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
                        cn.Open();
                        cm = new SqlCommand("Update tblChairmanship set role=@role, status=@status where role like '"+txtAddChairmanship.Text+"'",cn);
                        cm.Parameters.AddWithValue("@role",txtAddChairmanship.Text);
                        cm.Parameters.AddWithValue("@status", "InActive");
                        cm.ExecuteNonQuery();
                        cn.Close();
                        MessageBox.Show("Record has been successfully updated",vars._title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtAddChairmanship.Clear();
                        LoadExistingChairmanship();
                        f.LoadExistingChairmanshipIntoDataview();
                        this.Dispose();
                    }
                    else
                    {
                        cn.Open();
                        cm = new SqlCommand("Insert into tblChairmanship (role, status) values (@role, @status)", cn);
                        cm.Parameters.AddWithValue("@role", txtAddChairmanship.Text);
                        cm.Parameters.AddWithValue("@status", "InActive");
                        cm.ExecuteNonQuery();
                        cn.Close();
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
                cn.Close();
                MessageBox.Show(ex.Message, vars._title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
