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
    public partial class frmPrintClearance : Form
    {
        SqlConnection cn;
        SqlCommand cm;
        frmDocument f;
        SqlDataReader dr;
        public string DbString = @"Data Source = MUNDAS26\SQLEXPRESS; Initial Catalog = bmis; Integrated Security = True";
        public frmPrintClearance(frmDocument f)
        {
            InitializeComponent();
            cn = new SqlConnection(DbString);
            this.f = f;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
        public void LoadRefnoForClearance()
        {
            try
            {
                cboRefNOForClearance.Items.Clear();
                cn.Open();
                cm = new SqlCommand("Select refno from tblPayment where type like 'BARANGAY CLEARANCE' and status like 'Pending'order by id desc", cn);
                dr = cm.ExecuteReader();
                while (dr.Read())
                {
                    cboRefNOForClearance.Items.Add(dr[0].ToString());
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

        private void cboRefNO_SelectedIndexChanged(object sender, EventArgs e)
        {
                try
            {
                cn.Open();
                cm = new SqlCommand("Select *from tblPayment where refno like '" + cboRefNOForClearance.Text + "'", cn);
                dr = cm.ExecuteReader();
                dr.Read();
                if (dr.HasRows)
                {
                    txtName.Text = dr["name"].ToString();
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

        private void btnSave_Click(object sender, EventArgs e)
        {
            var textBoxes = new[]
            { txtName, txtAddress, txtPurpose, txtBC};
            var comboBox = new[]
            { cboRefNOForClearance};
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
                        errorProvider.SetError(cb, "");
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
                    if (MessageBox.Show("Do you want to save this record?", vars._title, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        string user = vars.Users;
                        cn.Open();
                        cm = new SqlCommand("Update tblPayment set status = 'Completed' where refno like '" + cboRefNOForClearance.Text + "'", cn);
                        cm.ExecuteNonQuery();
                        cn.Close();

                        cn.Open();
                        cm = new SqlCommand("Insert into tblDocument (refno, type, details1, details2, details3, details4, idate, [user]) values(@refno, @type, @details1, @details2, @details3, @details4, @idate, @user)", cn);
                        cm.Parameters.AddWithValue("@refno", cboRefNOForClearance.Text);
                        cm.Parameters.AddWithValue("@type", "BARANGAY CLEARANCE");
                        cm.Parameters.AddWithValue("@details1", txtName.Text);
                        cm.Parameters.AddWithValue("@details2", txtAddress.Text);
                        cm.Parameters.AddWithValue("@details3", txtPurpose.Text);
                        cm.Parameters.AddWithValue("@details4", txtBC.Text);
                        cm.Parameters.AddWithValue("@idate", DateTime.Now);
                        cm.Parameters.AddWithValue("@user", user);
                        cm.ExecuteNonQuery();
                        cn.Close();
                        MessageBox.Show("Record has been successfully saved!", vars._title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        f.loadBrgyClearance();
                        this.Dispose();
                    }
                }
                catch(Exception ex)
                {
                    cn.Close();
                    MessageBox.Show(ex.Message, vars._title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
        }
    }
}
