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
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;

namespace BMIS
{
    public partial class frmResidentInformation : Form
    {
        SqlConnection _sqlConnection;
        SqlCommand _sqlCommand;
        SqlDataReader _sqlDataReader;
        frmResidentList f;
        public string _id;
        

        [Obsolete]
        public frmResidentInformation(frmResidentList f)
        {
            InitializeComponent();
            this.f = f;
            _sqlConnection = new SqlConnection(vars.DbString);
            LoadPurok();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            var textboxes = new[]
                {
                txtID,txtLname,txtFname,txtMname,txtAllias,txtBplace, txtAge, txtReligion,
                txtEmail, txtContact, txtEduc, txtOccupation,
                txtAddress, txtHouse, txtHeadofthefamily
                };

            var comboboxes = new[]
            {
                cboCivilstatus, cboGender, cboVoters, cboPurok,
                cboCategory, cboPersonwithAbility,cboStatus
                };
            foreach (var tb in textboxes)
            {
                foreach (var cb in comboboxes)
                {
                    if (string.IsNullOrEmpty(cb.Text))
                    {
                        errorProvider.SetError(cb, "Some fields are empty!");
                        return;
                    }
                    else
                    {
                        errorProvider.SetError(cb, "");
                    }
                }
                if (string.IsNullOrWhiteSpace(tb.Text))
                {
                    errorProvider.SetError(tb, "Some fields are empty");
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
                    MemoryStream ms = new MemoryStream();
                    picImage.BackgroundImage.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                    picImage.BackgroundImage.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                    byte[] arrImage = ms.GetBuffer();

                    _sqlConnection.Open();
                    _sqlCommand = new SqlCommand("Insert into tblResident (nid, lname, fname, mname, alias, bdate, bplace, age, civilstatus, gender, religion, email, contact, voters, precint, purok, educational, occupation, address, category, house, head, disability, status, pic) values (@nid, @lname, @fname, @mname, @alias, @bdate, @bplace, @age, @civilstatus, @gender, @religion, @email, @contact, @voters, @precint, @purok, @educational, @occupation, @address, @category, @house, @head, @disability, @status, @pic)", _sqlConnection);
                    _sqlCommand.Parameters.AddWithValue("@nid", txtID.Text);
                    _sqlCommand.Parameters.AddWithValue("@lname", txtLname.Text);
                    _sqlCommand.Parameters.AddWithValue("@fname", txtFname.Text);
                    _sqlCommand.Parameters.AddWithValue("@mname", txtMname.Text);
                    _sqlCommand.Parameters.AddWithValue("@alias", txtAllias.Text);
                    _sqlCommand.Parameters.AddWithValue("@bdate", dtBdate.Value);
                    _sqlCommand.Parameters.AddWithValue("@bplace", txtBplace.Text);
                    _sqlCommand.Parameters.AddWithValue("@age", txtAge.Text);
                    _sqlCommand.Parameters.AddWithValue("@civilstatus", cboCivilstatus.Text);
                    _sqlCommand.Parameters.AddWithValue("@gender", cboGender.Text);
                    _sqlCommand.Parameters.AddWithValue("@religion", txtReligion.Text);
                    _sqlCommand.Parameters.AddWithValue("@email", txtEmail.Text);
                    _sqlCommand.Parameters.AddWithValue("@contact", txtContact.Text);
                    _sqlCommand.Parameters.AddWithValue("@voters", cboVoters.Text);
                    _sqlCommand.Parameters.AddWithValue("@precint", txtPrecint.Text);
                    _sqlCommand.Parameters.AddWithValue("@purok", cboPurok.Text);
                    _sqlCommand.Parameters.AddWithValue("@educational", txtEduc.Text);
                    _sqlCommand.Parameters.AddWithValue("@occupation", txtOccupation.Text);
                    _sqlCommand.Parameters.AddWithValue("@address", txtAddress.Text);
                    _sqlCommand.Parameters.AddWithValue("@category", cboCategory.Text);
                    _sqlCommand.Parameters.AddWithValue("@house", txtHouse.Text);
                    _sqlCommand.Parameters.AddWithValue("@head", txtHeadofthefamily.Text);
                    _sqlCommand.Parameters.AddWithValue("@disability", cboPersonwithAbility.Text);
                    _sqlCommand.Parameters.AddWithValue("@status", cboStatus.Text);
                    _sqlCommand.Parameters.AddWithValue("@pic", arrImage);
                    _sqlCommand.ExecuteNonQuery();
                    _sqlConnection.Close();
                    f.loadrecordResident();
                    MessageBox.Show("Record has been successfully saved!", vars._title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, vars._title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                _sqlConnection.Close();
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            var textboxes = new[]
                {
                txtID,txtLname,txtFname,txtMname,txtAllias,txtBplace, txtAge, txtReligion,
                txtEmail, txtContact, txtEduc, txtOccupation,
                txtAddress, txtHouse, txtHeadofthefamily
                };

            var comboboxes = new[]
            {
                cboCivilstatus, cboGender, cboVoters, cboPurok,
                cboCategory, cboPersonwithAbility,cboStatus
                };
            foreach (var tb in textboxes)
            {
                foreach (var cb in comboboxes)
                {
                    if (string.IsNullOrEmpty(cb.Text))
                    {
                        errorProvider.SetError(cb, "Some fields are empty!");
                        return;
                    }
                    else
                    {
                        errorProvider.SetError(cb, "");
                    }
                }
                if (string.IsNullOrWhiteSpace(tb.Text))
                {
                    errorProvider.SetError(tb, "Some fields are empty");
                    return;
                }
                else
                {
                    errorProvider.SetError(tb, "");
                }
            }

            try
            {
                if (MessageBox.Show("Do you want to update this record?", vars._title, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    MemoryStream ms = new MemoryStream();
                    picImage.BackgroundImage.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                    picImage.BackgroundImage.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                    byte[] arrImage = ms.GetBuffer();

                    _sqlConnection.Open();
                    _sqlCommand = new SqlCommand("Update tblResident set nid=@nid, lname= @lname, fname= @fname, mname= @mname, alias= @alias, bdate= @bdate, bplace= @bplace, age= @age, civilstatus= @civilstatus, gender= @gender, religion= @religion, email= @email, contact= @contact, voters= @voters, precint= @precint, purok= @purok, educational= @educational, occupation= @occupation, address= @address, category= @category, house= @house, head= @head, disability= @disability, status= @status, pic= @pic where id= @id", _sqlConnection);
                    _sqlCommand.Parameters.AddWithValue("@nid", txtID.Text);
                    _sqlCommand.Parameters.AddWithValue("@lname", txtLname.Text);
                    _sqlCommand.Parameters.AddWithValue("@fname", txtFname.Text);
                    _sqlCommand.Parameters.AddWithValue("@mname", txtMname.Text);
                    _sqlCommand.Parameters.AddWithValue("@alias", txtAllias.Text);
                    _sqlCommand.Parameters.AddWithValue("@bdate", dtBdate.Value);
                    _sqlCommand.Parameters.AddWithValue("@bplace", txtBplace.Text);
                    _sqlCommand.Parameters.AddWithValue("@age", txtAge.Text);
                    _sqlCommand.Parameters.AddWithValue("@civilstatus", cboCivilstatus.Text);
                    _sqlCommand.Parameters.AddWithValue("@gender", cboGender.Text);
                    _sqlCommand.Parameters.AddWithValue("@religion", txtReligion.Text);
                    _sqlCommand.Parameters.AddWithValue("@email", txtEmail.Text);
                    _sqlCommand.Parameters.AddWithValue("@contact", txtContact.Text);
                    _sqlCommand.Parameters.AddWithValue("@voters", cboVoters.Text);
                    _sqlCommand.Parameters.AddWithValue("@precint", txtPrecint.Text);
                    _sqlCommand.Parameters.AddWithValue("@purok", cboPurok.Text);
                    _sqlCommand.Parameters.AddWithValue("@educational", txtEduc.Text);
                    _sqlCommand.Parameters.AddWithValue("@occupation", txtOccupation.Text);
                    _sqlCommand.Parameters.AddWithValue("@address", txtAddress.Text);
                    _sqlCommand.Parameters.AddWithValue("@category", cboCategory.Text);
                    _sqlCommand.Parameters.AddWithValue("@house", txtHouse.Text);
                    _sqlCommand.Parameters.AddWithValue("@head", txtHeadofthefamily.Text);
                    _sqlCommand.Parameters.AddWithValue("@disability", cboPersonwithAbility.Text);
                    _sqlCommand.Parameters.AddWithValue("@status", cboStatus.Text);
                    _sqlCommand.Parameters.AddWithValue("@pic", arrImage);
                    _sqlCommand.Parameters.AddWithValue("@id", _id);
                    _sqlCommand.ExecuteNonQuery();
                    _sqlConnection.Close();
                    MessageBox.Show("Record has been successfully Updated!", vars._title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Clear();
                    f.loadrecordResident();
                    this.Dispose();
                }
            }
            catch (Exception ex)
            {
                _sqlConnection.Close();
                MessageBox.Show(ex.Message, vars._title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        public void Clear()
        {
            picImage.BackgroundImage = Image.FromFile(Application.StartupPath + @"\man.jpg");
            txtID.Clear();
            txtLname.Clear();
            txtFname.Clear();
            txtMname.Clear();
            txtAllias.Clear();
            dtBdate.Value = DateTime.Today;
            txtBplace.Clear();
            txtAge.Clear();
            cboCivilstatus.SelectedIndex =-1;
            cboGender.SelectedIndex =-1;
            txtReligion.Clear();
            txtEmail.Clear();
            txtContact.Clear();
            cboVoters.SelectedIndex = -1;
            txtPrecint.Clear();
            cboPurok.SelectedIndex = -1;
            txtEduc.Clear();
            txtOccupation.Clear();
            txtAddress.Clear();
            cboCategory.SelectedIndex = -1;
            txtHouse.Clear();
            txtHeadofthefamily.Clear();
            cboPersonwithAbility.SelectedIndex =-1;
            cboStatus.SelectedIndex =-1;
            lblEmail.Text = "";
        }
        public void LoadPurok()
        {
            try
            {
                cboPurok.Items.Clear();
                _sqlConnection.Open();
                _sqlCommand = new SqlCommand("Select *from tblPurok", _sqlConnection);
                _sqlDataReader = _sqlCommand.ExecuteReader();
                while (_sqlDataReader.Read())
                {
                    cboPurok.Items.Add(_sqlDataReader["purok"].ToString());
                }
                _sqlDataReader.Close();
                _sqlConnection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, vars._title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                _sqlConnection.Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            f.loadrecordResident();
            Clear();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            try
            {
                openFileDialog1.Filter = "Image Files(*.png)|*.png|(*.jpg)|*.jpg|(*.gif)|*.gif";
                openFileDialog1.ShowDialog();
                picImage.BackgroundImage = Image.FromFile(openFileDialog1.FileName);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, vars._title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            f.loadrecordResident();
            this.Dispose();
        }
        private void cboCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboCategory.Text == "HEAD OF THE FAMILY")
            {
                txtHouse.Enabled = true;
                txtHeadofthefamily.Enabled = false;
                btnBrowser.Visible = false;
                txtHouse.Clear();
                txtHeadofthefamily.Text = txtFname.Text+" "+txtMname.Text+" "+txtLname.Text;
            }
            else
            {
                txtHeadofthefamily.Text = "";
                txtHouse.Enabled = false;
                txtHeadofthefamily.Enabled = false;
                btnBrowser.Visible = true;
                txtHouse.Clear();
            }
        }
        private void cboVoters_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboVoters.Text == "YES")
            {
                txtPrecint.Enabled = true;
            }
            else
            {
                txtPrecint.Enabled = false;
                txtPrecint.Clear();
            }
        }
        private void dtBdate_ValueChanged(object sender, EventArgs e)
        {
            /*DateTime today = DateTime.Today;
            int age = today.Year - dtBdate.Value.Year;
            if (today.Month < dtBdate.Value.Month || (today.Month == dtBdate.Value.Month && today.Day < dtBdate.Value.Day))
                age--;
            txtAge.Text = age.ToString();*/

            DateTime today = DateTime.Today;
            int age = today.Year-dtBdate.Value.Year;
            age--;
            txtAge.Text = age.ToString();
        }
        private void txtAge_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsDigit(e.KeyChar) || e.KeyChar == (char)Keys.Back);
        }
        private void ValidateEmail()
        {
            string Email = txtEmail.Text;
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(Email);
            if (match.Success)
            {
                lblEmail.Text = "("+Email+") "+"VERY GOOD!";
                lblEmail.ForeColor = System.Drawing.Color.Green;
            }
            else
            {
                lblEmail.Text = "("+Email+") "+"is Invalid Email Address";
                lblEmail.ForeColor = System.Drawing.Color.Red;

            }
        }
        private void txtEmail_Validated(object sender, EventArgs e)
        {
            ValidateEmail();
        }

        [Obsolete]
        private void btnBrowser_Click_1(object sender, EventArgs e)
        {
            frmHousehold f = new frmHousehold(this);
            f.loadRecord();
            f.ShowDialog();
        }
    }
}
