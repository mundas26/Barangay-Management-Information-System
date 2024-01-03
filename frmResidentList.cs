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
using System.IO;

namespace BMIS
{
    public partial class frmResidentList : Form
    {
        SqlConnection _sqlConnection;
        SqlCommand cm;
        SqlDataReader dr;
        public string DbString = @"Data Source = MUNDAS26\SQLEXPRESS; Initial Catalog = bmis; Integrated Security = True";

        [Obsolete]
        public frmResidentList()
        {
            InitializeComponent();
            _sqlConnection = new SqlConnection(DbString);
        }

        private void txtSearchresidentlist_TextChanged(object sender, EventArgs e)
        {
            loadrecordResident();
        }

        [Obsolete]
        private void btnResidentAddnew_Click(object sender, EventArgs e)
        {
            frmResidentInformation f = new frmResidentInformation(this);
            f.btnUpdate.Enabled = false;
            f.Clear();
            f.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        [Obsolete]
        private void viewResident_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                string colname = viewResident.Columns[e.ColumnIndex].Name;
                if (colname == "btnEditResident")
                {
                    frmResidentInformation f = new frmResidentInformation(this);
                    f.LoadPurok();
                    _sqlConnection.Open();
                    cm = new SqlCommand("Select pic as picture, *from tblResident where id like '"+viewResident.Rows[e.RowIndex].Cells[0].Value.ToString()+"'", _sqlConnection);
                    dr = cm.ExecuteReader();
                    dr.Read();
                    if (dr.HasRows)
                    {
                        long len = dr.GetBytes(0, 0, null, 0, 0);
                        byte[] array = new byte[System.Convert.ToInt32(len) + 1];
                        dr.GetBytes(0, 0, array, 0, System.Convert.ToInt32(len));

                        f._id = dr["id"].ToString();    
                        f.txtID.Text = dr["nid"].ToString();
                        f.txtLname.Text = dr["lname"].ToString();
                        f.txtFname.Text = dr["fname"].ToString();
                        f.txtMname.Text = dr["mname"].ToString();
                        f.txtAllias.Text = dr["alias"].ToString();
                        f.dtBdate.Value = DateTime.Parse(dr["bdate"].ToString());
                        f.txtBplace.Text = dr["bplace"].ToString();
                        f.txtAge.Text = dr["age"].ToString();
                        f.cboCivilstatus.Text = dr["civilstatus"].ToString();
                        f.cboGender.Text = dr["gender"].ToString();
                        f.txtReligion.Text = dr["religion"].ToString();
                        f.txtEmail.Text = dr["email"].ToString();
                        f.txtContact.Text = dr["contact"].ToString();
                        f.cboVoters.Text = dr["voters"].ToString();
                        f.txtPrecint.Text = dr["precint"].ToString();
                        f.cboPurok.Text = dr["purok"].ToString();
                        f.txtEduc.Text = dr["educational"].ToString();
                        f.txtOccupation.Text = dr["occupation"].ToString();
                        f.txtAddress.Text = dr["address"].ToString();
                        f.cboCategory.Text = dr["category"].ToString();
                        f.txtHouse.Text = dr["house"].ToString();
                        f.txtHeadofthefamily.Text = dr["head"].ToString();
                        f.cboPersonwithAbility.Text = dr["disability"].ToString();
                        f.cboStatus.Text = dr["status"].ToString();

                        MemoryStream ms = new MemoryStream(array);
                        Bitmap bitmap = new Bitmap(ms);
                        f.picImage.BackgroundImage = bitmap;
                    }
                    f.btnSave.Enabled = false;
                    dr.Close();
                    _sqlConnection.Close();
                    f.ShowDialog();
                }
                else if (colname =="btnDelResident")
                {
                    if (MessageBox.Show("Do you want to delete is record?", vars._title, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        _sqlConnection.Open();
                        cm = new SqlCommand("delete from tblResident where id like '" + viewResident.Rows[e.RowIndex].Cells[0].Value.ToString() + "'", _sqlConnection);
                        cm.ExecuteNonQuery();
                        _sqlConnection.Close();
                        MessageBox.Show("Record  has been successfully deleted!", vars._title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        loadrecordResident();
                    }
                }
            }
            catch (Exception ex)
            {
                _sqlConnection.Close();
                MessageBox.Show(ex.Message, vars._title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        public void loadrecordResident()
        {
            try
            {
                viewResident.Rows.Clear();
                _sqlConnection.Open();
                cm = new SqlCommand("Select *from tblResident where lname like '%" + txtSearchresidentlist.Text + "%' or fname like '%" + txtSearchresidentlist.Text + "%'", _sqlConnection);
                dr = cm.ExecuteReader();
                while (dr.Read())
                {
                    viewResident.Rows.Add(dr["id"].ToString(), dr["nid"].ToString(), dr["lname"].ToString(), dr["fname"].ToString(), dr["mname"].ToString(), dr["alias"].ToString(), dr["address"].ToString(), dr["house"].ToString(), dr["category"].ToString(), DateTime.Parse(dr["bdate"].ToString()).ToShortDateString(), dr["age"].ToString(), dr["gender"].ToString(), dr["civilstatus"].ToString());
                }
                dr.Close();
                _sqlConnection.Close();
                viewResident.ClearSelection();
                lblTotalPopulation.Text = CountRecords("Select count(*) from tblResident");
                lblTotalMember.Text = CountRecords("Select count(*) from tblResident where category like 'MEMBER'");
                lblTotalHousehold.Text = CountRecords("Select count(*) from tblResident where category like 'HEAD OF THE FAMILY'");
                lblTotalRegistered.Text = CountRecords("Select count(*) from tblResident where voters like 'YES'");
                lblTotalFemale.Text = CountRecords("Select count(*) from tblResident where gender like 'FEMALE'");
                lblTotalMale.Text = CountRecords("Select count(*) from tblResident where gender like 'MALE'");
                lblTotalvaccinated.Text = CountRecords("Select count(*) from tblVaccine where status like 'FULLY VACCINATED'");
            }
            catch (Exception ex)
            {
                _sqlConnection.Close();
                MessageBox.Show(ex.Message, vars._title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        public string CountRecords(string sql)
        {
            _sqlConnection.Open();
            cm = new SqlCommand(sql, _sqlConnection);
            string _count = cm.ExecuteScalar().ToString();
            _sqlConnection.Close();
            return _count;
        }
        public void LoadVaccination()
        {
            try
            {
                _sqlConnection.Open();
                viewVaccination.Rows.Clear();
                cm = new SqlCommand("Select *from vwvaccination where (lname like '%" + txtSearch.Text + "%' or fname like '%" + txtSearch.Text + "%') and status like '" + cboStatus.Text + "'", _sqlConnection);
                dr = cm.ExecuteReader();
                while (dr.Read())
                {
                    viewVaccination.Rows.Add(dr["id"].ToString(), dr["lname"].ToString(), dr["fname"].ToString(), dr["mname"].ToString(), dr["vaccine"].ToString(), dr["status"].ToString());
                }
                dr.Close();
                _sqlConnection.Close();
                viewVaccination.ClearSelection();
            }
            catch (Exception ex)
            {
                _sqlConnection.Close();
                MessageBox.Show(ex.Message, vars._title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        public void loadHeadofthefamily()
        {
            try
            {
                viewHousehold.Rows.Clear();
                _sqlConnection.Open();
                cm = new SqlCommand("Select *from tblResident where (lname+','+fname+''+mname) like'%" + txtSearchHousehold.Text + "%' and category like 'HEAD OF THE FAMILY'", _sqlConnection);
                dr = cm.ExecuteReader();
                while (dr.Read())
                {
                    viewHousehold.Rows.Add(dr["id"].ToString(), dr["nid"].ToString(), dr["lname"].ToString(), dr["fname"].ToString(), dr["mname"].ToString(), dr["alias"].ToString(), dr["address"].ToString(), dr["house"].ToString(), dr["category"].ToString(), DateTime.Parse(dr["bdate"].ToString()).ToShortDateString(), dr["age"].ToString(), dr["gender"].ToString(), dr["civilstatus"].ToString());
                }
                dr.Close();
                _sqlConnection.Close();
                viewHousehold.ClearSelection();
            }
            catch (Exception ex)
            {
                _sqlConnection.Close();
                MessageBox.Show(ex.Message, vars._title, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void viewHousehold_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                string colname = viewHousehold.Columns[e.ColumnIndex].Name;
                if (colname == "btnView")
                {
                    string _id = viewHousehold.Rows[e.RowIndex].Cells[0].Value.ToString();
                    frmViewhousehold f = new frmViewhousehold(this);
                    _sqlConnection.Open();
                    cm = new SqlCommand("Select (lname+', '+fname+' '+mname) as fullname, house from tblResident where id =@id", _sqlConnection);
                    cm.Parameters.AddWithValue("@id", _id);
                    dr = cm.ExecuteReader();
                    while (dr.Read())
                    {
                        f.lblname.Text = dr["fullname"].ToString();
                        f.lblHouseholdnumber.Text = dr["house"].ToString();
                    }
                    dr.Close();
                    _sqlConnection.Close();
                    f.loadViewhousehold();
                    f.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                _sqlConnection.Close();
                MessageBox.Show(ex.Message, vars._title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void txtSearchHousehold_TextChanged(object sender, EventArgs e)
        {
            loadHeadofthefamily();
        }

        private void viewVaccination_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colname = viewVaccination.Columns[e.ColumnIndex].Name;
            if (colname == "btnEditvaccine")
            {
                frmVaccination f = new frmVaccination(this);
                f._id = viewVaccination.Rows[e.RowIndex].Cells[0].Value.ToString();
                f.lblname.Text = viewVaccination.Rows[e.RowIndex].Cells[1].Value.ToString() + ", " + viewVaccination.Rows[e.RowIndex].Cells[2].Value.ToString() + " " + viewVaccination.Rows[e.RowIndex].Cells[3].Value.ToString();
                f.txtVaccinetype.Text = viewVaccination.Rows[e.RowIndex].Cells[4].Value.ToString();
                f.cboVaccinationlist.Text = viewVaccination.Rows[e.RowIndex].Cells[5].Value.ToString();
                f.ShowDialog();
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            LoadVaccination();
        }

        private void cboStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadVaccination();
        }

        private void picClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
