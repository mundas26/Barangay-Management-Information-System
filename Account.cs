using System;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace BMIS
{
    public partial class frmAccount : Form
    {
        private readonly SqlConnection _sqlConnection;
        private SqlCommand _sqlCommand;
        private SqlDataReader _sqlDataReader;
        public string _id;
        public frmMaintenance f;
        public frmSecurity f1;
        public frmAccount(frmMaintenance f)
        {
            InitializeComponent();
            _sqlConnection = new SqlConnection(vars.DbString);
            this.f = f;
        }

        public frmAccount(frmSecurity f1)
        {
            InitializeComponent();
            this.f1 = f1;
            _sqlConnection = new SqlConnection(dbconstring.connection);
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            var textboxes = new[]
               {txtName, txtUser, txtPass, txtConfirmPass};

            var comboboxes = new[]
            {cboPosition};
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
                if (txtPass.Text == txtConfirmPass.Text)
                {
                    if (Check.checkDuplicate("Select count(*)from tblUser where username like '" + txtUser.Text + "'") != true)
                    {
                        if (cboPosition.Text == cboPosition.SelectedItem.ToString())
                        {
                            _sqlConnection.Open();
                            _sqlCommand = new SqlCommand("Update tblOfficial set accountStatus = 'Completed' where position like '" + cboPosition.Text + "'", _sqlConnection);
                            _sqlCommand.ExecuteNonQuery();
                            _sqlConnection.Close();

                            var randomFilename = Path.GetRandomFileName() + ".jpg";
                            string folder = @".\Images";
                            string path = System.IO.Path.Combine(folder, randomFilename);

                            _sqlConnection.Open();
                            _sqlCommand = new SqlCommand("Insert into tblUser (name, username, password, role, pic) values (@name, @username, @password, @role, @pic)", _sqlConnection);
                            _sqlCommand.Parameters.AddWithValue("@name", txtName.Text);
                            _sqlCommand.Parameters.AddWithValue("@username", txtUser.Text);
                            _sqlCommand.Parameters.AddWithValue("@password", txtPass.Text);
                            _sqlCommand.Parameters.AddWithValue("@role", cboPosition.Text);
                            _sqlCommand.Parameters.AddWithValue("@pic", path);

                            Image image = picIDPicture.Image;
                            image.Save(path);

                            _sqlCommand.ExecuteNonQuery();
                            _sqlDataReader.Close();
                            _sqlConnection.Close();
                            f.LoadRecordAccount();
                            LoadPositionForMakingAnNewAccount();
                            Clear();
                            MessageBox.Show("Record has been successfully saved!", vars._title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.Dispose();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Username : (" + txtUser.Text + ") is already existing! Please try another one.", vars._title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtUser.Clear();
                        txtUser.Focus();
                    }
                }
                else
                {
                    MessageBox.Show("Password didn't Match!", vars._title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                _sqlConnection.Close();
                MessageBox.Show(ex.Message, vars._title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnBrowseID_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Do you want to change this Image?", vars._title, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string imagePath;
                    _sqlConnection.Open();
                    _sqlCommand = new SqlCommand("Select idPic as IdPicture, *from tblOfficial where id like '"+_id+"'", _sqlConnection);
                    _sqlDataReader = _sqlCommand.ExecuteReader();
                    _sqlDataReader.Read();
                    if (_sqlDataReader.HasRows)
                    {
                        imagePath = _sqlDataReader["idPic"].ToString();
                        if (File.Exists(imagePath))
                        {
                            File.Delete(imagePath);
                            button1.Visible = false;
                        }
                        _sqlConnection.Close();
                    }
                    _sqlDataReader.Close();
                    _sqlConnection.Close();
                    openFileDialog1.Filter = "Select image(*.jpg;*.png;*.jpeg)| *.jpg;*.png;*.jpeg";
                    if (openFileDialog1.ShowDialog() == DialogResult.OK)
                    {
                        picIDPicture.Image = Image.FromFile(openFileDialog1.FileName);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, vars._title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        public void Clear()
        {
            string defaultImagePath = Application.StartupPath + @"\man.jpg";
            picIDPicture.Image = Image.FromFile(defaultImagePath);

            cboPosition.SelectedIndex = -1;
            txtPass.Clear();
            txtUser.Clear();
            txtConfirmPass.Clear();

            picEyeClose.BringToFront();
            txtPass.UseSystemPasswordChar = false;
            txtConfirmPass.UseSystemPasswordChar = false;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to clear it all?", vars._title, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                string imagePath;
                _sqlConnection.Open();
                _sqlCommand = new SqlCommand("Select idPic as IdPicture, *from tblOfficial where id like '"+_id+"'", _sqlConnection);
                _sqlDataReader = _sqlCommand.ExecuteReader();
                _sqlDataReader.Read();
                if (_sqlDataReader.HasRows)
                {
                    imagePath = _sqlDataReader["idPic"].ToString();
                    if (File.Exists(imagePath))
                    {
                        File.Delete(imagePath);
                    }
                }
                _sqlDataReader.Close();
                _sqlConnection.Close();
                Clear();
                button1.Visible = false;
            }
        }
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            var textboxes = new[]
              {txtName, txtUser, txtPass, txtConfirmPass};

            var comboboxes = new[]
            {cboPosition};
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
                if (txtPass.Text == txtConfirmPass.Text)
                {
                    if (Check.checkDuplicate("Select count(*)from tblOfficial where username like '" + txtUser.Text + "'") != true)
                    {
                        string newFilename = Path.GetRandomFileName();
                        string imagePath = @".\Images\" + newFilename + ".jpg";

                        _sqlConnection.Open();
                        _sqlCommand = new SqlCommand("UPDATE tblOfficial SET name=@name, position=@position, username=@username, password=@password, idPic=@idPic, accountStatus=@accountStatus WHERE position=@position", _sqlConnection);
                        _sqlCommand.Parameters.AddWithValue("@name", txtName.Text);
                        _sqlCommand.Parameters.AddWithValue("@position", cboPosition.Text);
                        _sqlCommand.Parameters.AddWithValue("@username", txtUser.Text);
                        _sqlCommand.Parameters.AddWithValue("@password", txtPass.Text);
                        _sqlCommand.Parameters.AddWithValue("@idPic", imagePath);
                        _sqlCommand.Parameters.AddWithValue("@accountStatus", "Completed");

                        Image image = picIDPicture.Image;
                        image.Save(imagePath);

                        _sqlCommand.ExecuteNonQuery();
                        _sqlConnection.Close();
                        MessageBox.Show("Record has been successfully updated!", vars._title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Clear();
                        f.LoadRecordAccount();
                        this.Dispose();
                    }
                    else
                    {
                        MessageBox.Show("Username : (" + txtUser.Text + ") is already existing! Please try another one.", vars._title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtUser.Clear();
                        txtUser.Focus();
                    }
                }
                else
                {
                    _sqlConnection.Close();
                    MessageBox.Show("Password didn't match!", vars._title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                _sqlConnection.Close();
                MessageBox.Show(ex.Message, vars._title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        public string GetPreviousImagePathFromDatabase()
        {
            string previousImagePath = "";

            string position = cboPosition.Text;
            _sqlConnection.Open();
            _sqlCommand = new SqlCommand("Select idPic from tblOfficial where position = @position", _sqlConnection);
            _sqlCommand.Parameters.AddWithValue("@position", position);
            _sqlDataReader = _sqlCommand.ExecuteReader();
            if (_sqlDataReader.Read())
            {
                previousImagePath = _sqlDataReader["idPic"].ToString();
            }
            _sqlDataReader.Close();
            _sqlConnection.Close();
            return previousImagePath;
        }
        private void picEyeClose_Click(object sender, EventArgs e)
        {
            picEyeOpen.BringToFront();
            txtPass.UseSystemPasswordChar = true;
            txtConfirmPass.UseSystemPasswordChar = true;
        }

        private void picEyeOpen_Click_1(object sender, EventArgs e)
        {
            picEyeClose.BringToFront();
            txtPass.UseSystemPasswordChar = false;
            txtConfirmPass.UseSystemPasswordChar = false;
        }
        public void LoadPositionForMakingAnNewAccount()
        {
            try
            {
                _sqlConnection.Open();
                _sqlCommand = new SqlCommand("Select position from tblOfficial where accountStatus like 'Incomplete' order by id asc", _sqlConnection);
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

        private void cboPosition_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                _sqlConnection.Open();
                _sqlCommand = new SqlCommand("Select *from tblOfficial where position like '" + cboPosition.Text + "'", _sqlConnection);
                _sqlDataReader = _sqlCommand.ExecuteReader();
                _sqlDataReader.Read();
                if (_sqlDataReader.HasRows)
                {
                    txtName.Text = _sqlDataReader["name"].ToString();
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
