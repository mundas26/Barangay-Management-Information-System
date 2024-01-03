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
        SqlCommand _sqlCommand;
        SqlDataReader _sqlDataReader;
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
                    _sqlCommand = new SqlCommand("Select pic as picture, *from tblResident where id like '"+viewResident.Rows[e.RowIndex].Cells[0].Value.ToString()+"'", _sqlConnection);
                    _sqlDataReader = _sqlCommand.ExecuteReader();
                    _sqlDataReader.Read();
                    if (_sqlDataReader.HasRows)
                    {
                        long len = _sqlDataReader.GetBytes(0, 0, null, 0, 0);
                        byte[] array = new byte[System.Convert.ToInt32(len) + 1];
                        _sqlDataReader.GetBytes(0, 0, array, 0, System.Convert.ToInt32(len));

                        f._id = _sqlDataReader["id"].ToString();    
                        f.txtID.Text = _sqlDataReader["nid"].ToString();
                        f.txtLname.Text = _sqlDataReader["lname"].ToString();
                        f.txtFname.Text = _sqlDataReader["fname"].ToString();
                        f.txtMname.Text = _sqlDataReader["mname"].ToString();
                        f.txtAllias.Text = _sqlDataReader["alias"].ToString();
                        f.dtBdate.Value = DateTime.Parse(_sqlDataReader["bdate"].ToString());
                        f.txtBplace.Text = _sqlDataReader["bplace"].ToString();
                        f.txtAge.Text = _sqlDataReader["age"].ToString();
                        f.cboCivilstatus.Text = _sqlDataReader["civilstatus"].ToString();
                        f.cboGender.Text = _sqlDataReader["gender"].ToString();
                        f.txtReligion.Text = _sqlDataReader["religion"].ToString();
                        f.txtEmail.Text = _sqlDataReader["email"].ToString();
                        f.txtContact.Text = _sqlDataReader["contact"].ToString();
                        f.cboVoters.Text = _sqlDataReader["voters"].ToString();
                        f.txtPrecint.Text = _sqlDataReader["precint"].ToString();
                        f.cboPurok.Text = _sqlDataReader["purok"].ToString();
                        f.txtEduc.Text = _sqlDataReader["educational"].ToString();
                        f.txtOccupation.Text = _sqlDataReader["occupation"].ToString();
                        f.txtAddress.Text = _sqlDataReader["address"].ToString();
                        f.cboCategory.Text = _sqlDataReader["category"].ToString();
                        f.txtHouse.Text = _sqlDataReader["house"].ToString();
                        f.txtHeadofthefamily.Text = _sqlDataReader["head"].ToString();
                        f.cboPersonwithAbility.Text = _sqlDataReader["disability"].ToString();
                        f.cboStatus.Text = _sqlDataReader["status"].ToString();

                        MemoryStream ms = new MemoryStream(array);
                        Bitmap bitmap = new Bitmap(ms);
                        f.picImage.BackgroundImage = bitmap;
                    }
                    f.btnSave.Enabled = false;
                    _sqlDataReader.Close();
                    _sqlConnection.Close();
                    f.ShowDialog();
                }
                else if (colname =="btnDelResident")
                {
                    if (MessageBox.Show("Do you want to delete is record?", vars._title, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        _sqlConnection.Open();
                        _sqlCommand = new SqlCommand("delete from tblResident where id like '" + viewResident.Rows[e.RowIndex].Cells[0].Value.ToString() + "'", _sqlConnection);
                        _sqlCommand.ExecuteNonQuery();
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
                _sqlCommand = new SqlCommand("Select *from tblResident where lname like '%" + txtSearchresidentlist.Text + "%' or fname like '%" + txtSearchresidentlist.Text + "%'", _sqlConnection);
                _sqlDataReader = _sqlCommand.ExecuteReader();
                while (_sqlDataReader.Read())
                {
                    viewResident.Rows.Add(_sqlDataReader["id"].ToString(), _sqlDataReader["nid"].ToString(), _sqlDataReader["lname"].ToString(), _sqlDataReader["fname"].ToString(), _sqlDataReader["mname"].ToString(), _sqlDataReader["alias"].ToString(), _sqlDataReader["address"].ToString(), _sqlDataReader["house"].ToString(), _sqlDataReader["category"].ToString(), DateTime.Parse(_sqlDataReader["bdate"].ToString()).ToShortDateString(), _sqlDataReader["age"].ToString(), _sqlDataReader["gender"].ToString(), _sqlDataReader["civilstatus"].ToString());
                }
                _sqlDataReader.Close();
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
            _sqlCommand = new SqlCommand(sql, _sqlConnection);
            string _count = _sqlCommand.ExecuteScalar().ToString();
            _sqlConnection.Close();
            return _count;
        }
        public void LoadVaccination()
        {
            try
            {
                _sqlConnection.Open();
                viewVaccination.Rows.Clear();
                _sqlCommand = new SqlCommand("Select *from vwvaccination where (lname like '%" + txtSearch.Text + "%' or fname like '%" + txtSearch.Text + "%') and status like '" + cboStatus.Text + "'", _sqlConnection);
                _sqlDataReader = _sqlCommand.ExecuteReader();
                while (_sqlDataReader.Read())
                {
                    viewVaccination.Rows.Add(_sqlDataReader["id"].ToString(), _sqlDataReader["lname"].ToString(), _sqlDataReader["fname"].ToString(), _sqlDataReader["mname"].ToString(), _sqlDataReader["vaccine"].ToString(), _sqlDataReader["status"].ToString());
                }
                _sqlDataReader.Close();
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
                _sqlCommand = new SqlCommand("Select *from tblResident where (lname+','+fname+''+mname) like'%" + txtSearchHousehold.Text + "%' and category like 'HEAD OF THE FAMILY'", _sqlConnection);
                _sqlDataReader = _sqlCommand.ExecuteReader();
                while (_sqlDataReader.Read())
                {
                    viewHousehold.Rows.Add(_sqlDataReader["id"].ToString(), _sqlDataReader["nid"].ToString(), _sqlDataReader["lname"].ToString(), _sqlDataReader["fname"].ToString(), _sqlDataReader["mname"].ToString(), _sqlDataReader["alias"].ToString(), _sqlDataReader["address"].ToString(), _sqlDataReader["house"].ToString(), _sqlDataReader["category"].ToString(), DateTime.Parse(_sqlDataReader["bdate"].ToString()).ToShortDateString(), _sqlDataReader["age"].ToString(), _sqlDataReader["gender"].ToString(), _sqlDataReader["civilstatus"].ToString());
                }
                _sqlDataReader.Close();
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
                    _sqlCommand = new SqlCommand("Select (lname+', '+fname+' '+mname) as fullname, house from tblResident where id =@id", _sqlConnection);
                    _sqlCommand.Parameters.AddWithValue("@id", _id);
                    _sqlDataReader = _sqlCommand.ExecuteReader();
                    while (_sqlDataReader.Read())
                    {
                        f.lblname.Text = _sqlDataReader["fullname"].ToString();
                        f.lblHouseholdnumber.Text = _sqlDataReader["house"].ToString();
                    }
                    _sqlDataReader.Close();
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
