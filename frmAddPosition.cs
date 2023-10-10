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
        SqlConnection cn;
        SqlDataReader dr;
        SqlCommand cm;
        frmMaintenance f1;
        frmOfficial f;
        public string DbString = @"Data Source = MUNDAS26\SQLEXPRESS; Initial Catalog = bmis; Integrated Security = True";

        public frmAddPosition(frmMaintenance f1)
        {
            InitializeComponent();
            cn = new SqlConnection(DbString);
            this.f1 = f1;
        }
        public frmAddPosition(frmOfficial f)
        {
            InitializeComponent();
            cn = new SqlConnection(dbconstring.connection);
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
                cn.Open();
                cm = new SqlCommand("Select position from tblPosition", cn);
                dr = cm.ExecuteReader();
                while (dr.Read())
                {
                    cboPosition.Items.Add(dr["position"].ToString());
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
                if (!string.IsNullOrEmpty(txtAddPosition.Text) || !string.IsNullOrWhiteSpace(txtAddPosition.Text))
                {
                    frmMaintenance f = new frmMaintenance();
                    if (Check.checkDuplicate("Select count(*)from tblPosition where position like '" + txtAddPosition.Text + "'") == true)
                    {
                        cn.Open();
                        cm = new SqlCommand("Update tblPosition set position = @position, status = @status where position like '"+txtAddPosition.Text+"'", cn);
                        cm.Parameters.AddWithValue("@position", txtAddPosition.Text);
                        cm.Parameters.AddWithValue("@status", "InActive");
                        cm.ExecuteNonQuery();
                        cn.Close();
                        MessageBox.Show("Record has been successfully updated!", vars._title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtAddPosition.Text = "";
                        LoadExistingPostion();
                        f.LoadExistingPositionIntoDataview();
                        this.Dispose();
                    }
                    else
                    {
                        cn.Open();
                        cm = new SqlCommand("Insert into tblPosition (position, status) values (@position, @status)", cn);
                        cm.Parameters.AddWithValue("@position", txtAddPosition.Text);
                        cm.Parameters.AddWithValue("@status", "InActive");
                        cm.ExecuteNonQuery();
                        cn.Close();
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
                cn.Close();
                MessageBox.Show(ex.Message, vars._title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void cboPosition_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                cn.Open();
                cm = new SqlCommand("Select *from tblPosition where position like '" + cboPosition.Text + "'", cn);
                dr = cm.ExecuteReader();
                dr.Read();
                if (dr.HasRows)
                {
                    txtAddPosition.Text = dr["position"].ToString();
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

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Do you want to delete is record?", vars._title, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cn.Open();
                    cm = new SqlCommand("delete from tblPosition where position like '" + cboPosition.Text + "'", cn);
                    cm.ExecuteNonQuery();
                    cn.Close();
                    MessageBox.Show("Record  has been successfully deleted!", vars._title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadExistingPostion();
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
