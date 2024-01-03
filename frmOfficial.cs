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
using System.IO;
using System.Threading;

namespace BMIS
{
    public partial class frmOfficial : Form
    {
        SqlConnection _sqlConnection;
        SqlCommand _sqlCommand;
        SqlDataReader _sqlDataReader;
        frmMaintenance f;
        public string _id;
        public string DbString = @"Data Source = MUNDAS26\SQLEXPRESS; Initial Catalog = bmis; Integrated Security = True";
        public frmOfficial(frmMaintenance f)
        {
            InitializeComponent();
            _sqlConnection = new SqlConnection(DbString);
            this.f = f;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            var textBoxes = new[]
            { txtName};
            var comboBoxes = new[]
            { cboChairmanship,cboPosition,cboStatus};

            foreach (var tb in textBoxes)
            {
                foreach (var cb in comboBoxes)
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
            }try
            {
                if (MessageBox.Show("Do you want to save this record?", vars._title, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (Check.checkDuplicate("Select count(*)from tblOfficial where name like '" + txtName.Text + "'") != true)
                    { 
                        _sqlConnection.Open();
                        _sqlCommand = new SqlCommand("Update tblChairmanship set status= 'Active' where role like '" + cboChairmanship.Text + "'", _sqlConnection);
                        _sqlCommand.ExecuteNonQuery();
                        _sqlConnection.Close();

                        _sqlConnection.Open();
                        _sqlCommand = new SqlCommand("Update tblPosition set status= 'Active' where position like '" + cboPosition.Text + "'", _sqlConnection);
                        _sqlCommand.ExecuteNonQuery();
                        _sqlConnection.Close();

                        string newFilename = vars.GetRandomNumbers(10);
                        string imagePath = @".\OfficialPostImages\" + newFilename + ".jpg";
                        Image newImage = picPhotoPost.Image;
                        newImage.Save(imagePath);

                        _sqlConnection.Open();
                        _sqlCommand = new SqlCommand("insert into tblOfficial (name, chairmanship, position, termstart, termend, photopost, status) values(@name, @chairmanship, @position, @termstart, @termend, @photopost, @status)", _sqlConnection);
                        _sqlCommand.Parameters.AddWithValue("@name", txtName.Text);
                        _sqlCommand.Parameters.AddWithValue("@chairmanship", cboChairmanship.Text);
                        _sqlCommand.Parameters.AddWithValue("@position", cboPosition.Text);
                        _sqlCommand.Parameters.AddWithValue("@termstart", dtStart.Value);
                        _sqlCommand.Parameters.AddWithValue("@termend", dtEnd.Value);
                        _sqlCommand.Parameters.AddWithValue("@photopost", imagePath);

                        _sqlCommand.Parameters.AddWithValue("@status", cboStatus.Text);
                        _sqlCommand.ExecuteNonQuery();
                        _sqlConnection.Close();
                        MessageBox.Show("Record has been successfully saved!", vars._title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Clear();
                        f.LoadRecordOfficial();
                        f.LoadRecordAccount();
                        this.Dispose();
                    }
                    else
                    {
                        MessageBox.Show("Name : (" +txtName.Text+ ") is already existing! Please try another one.", vars._title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtName.Clear();
                        txtName.Focus();
                    }
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
            string folder = @".\Images";
            string path = System.IO.Path.Combine(folder);
            string DefaultImage = Application.StartupPath + @"\formal.png";
            picPhotoPost.Image = Image.FromFile(File.Exists(path)? path: DefaultImage);

            txtName.Clear();
            cboChairmanship.SelectedIndex = -1;
            cboPosition.SelectedIndex = -1; 
            dtStart.Value = DateTime.Today;
            dtEnd.Value = DateTime.Today;
            cboStatus.SelectedIndex = -1;
            txtName.Focus();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            var textBoxes = new[]
           { txtName};
            var comboBoxes = new[]
            { cboChairmanship,cboPosition,cboStatus};

            foreach (var tb in textBoxes)
            {
                foreach (var cb in comboBoxes)
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
                if (MessageBox.Show("Do you want to update this record?", vars._title, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string newFilename = vars.GetRandomNumbers(10);
                    string newImagePath = @".\OfficialPostImages\"+newFilename + ".jpg";
                    Image newImage = picPhotoPost.Image;
                    
                    _sqlConnection.Open();
                    _sqlCommand = new SqlCommand("update tblOfficial set name= @name, chairmanship= @chairmanship, position= @position, termstart= @termstart, termend= @termend, photopost= @photopost, status= @status where id= @id", _sqlConnection);
                    _sqlCommand.Parameters.AddWithValue("@name", txtName.Text);
                    _sqlCommand.Parameters.AddWithValue("@chairmanship", cboChairmanship.Text);
                    _sqlCommand.Parameters.AddWithValue("@position", cboPosition.Text);
                    _sqlCommand.Parameters.AddWithValue("@termstart", dtStart.Value);
                    _sqlCommand.Parameters.AddWithValue("@termend", dtEnd.Value);
                    _sqlCommand.Parameters.AddWithValue("@status", cboStatus.Text);
                    _sqlCommand.Parameters.AddWithValue("@id", _id);
                    _sqlCommand.Parameters.AddWithValue("@photopost", newImagePath);
                    
                    newImage.Save(newImagePath);
                    _sqlCommand.ExecuteNonQuery();
                    _sqlConnection.Close();
                    MessageBox.Show("Record has been successfully updated!", vars._title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Clear();
                    f.LoadRecordOfficial();
                    f.LoadRecordAccount();
                    this.Dispose();
                }
            }
            catch (Exception ex)
            {
                _sqlConnection.Close();
                MessageBox.Show(ex.Message, vars._title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to change this image?", vars._title, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                string imagePath;
                _sqlConnection.Open();
                _sqlCommand = new SqlCommand("Select photopost as Pictures, *from tblOfficial where id like '" + _id + "'", _sqlConnection);
                _sqlDataReader = _sqlCommand.ExecuteReader();
                _sqlDataReader.Read();
                if (_sqlDataReader.HasRows)
                {
                    imagePath = _sqlDataReader["photopost"].ToString();
                    if (File.Exists(imagePath))
                    {
                        File.Delete(imagePath);
                    }
                    _sqlConnection.Close();
                }
                _sqlDataReader.Close();
                _sqlConnection.Close();
                button1.Visible = false;
                Clear();
            }
        }

        private void btnBrowsePhotoPost_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Do you want to change this image?", vars._title, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string imagePath;
                    _sqlConnection.Open();
                    _sqlCommand = new SqlCommand("Select photopost as Pictures, *from tblOfficial where id like '"+_id+"'",_sqlConnection);
                    _sqlDataReader = _sqlCommand.ExecuteReader();
                    _sqlDataReader.Read();
                    if (_sqlDataReader.HasRows)
                    {
                        imagePath = _sqlDataReader["photopost"].ToString();
                        if (File.Exists(imagePath))
                        {
                            File.Delete(imagePath);
                        }
                        _sqlConnection.Close();
                    }
                    _sqlDataReader.Close();
                    _sqlConnection.Close();
                    button1.Visible = false;

                    openFileDialog1.Filter = "Select image(*.jpg;*.png;*.jpeg)| *.jpg;*.png;*.jpeg";
                    if (openFileDialog1.ShowDialog() == DialogResult.OK)
                    {
                        picPhotoPost.Image = Image.FromFile(openFileDialog1.FileName);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, vars._title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        public void LoadChairmanship()
        {
            try
            {
                _sqlConnection.Open();
                _sqlCommand = new SqlCommand("Select role from tblChairmanship where status like 'InActive' order by id asc", _sqlConnection);
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
        public void LoadPosition()
        {
            try
            {
                _sqlConnection.Open();
                _sqlCommand = new SqlCommand("Select position from tblPosition where status like 'InActive'order by id asc", _sqlConnection);
                _sqlDataReader = _sqlCommand.ExecuteReader();
                while (_sqlDataReader.Read())
                {
                    cboPosition.Items.Add(_sqlDataReader[0].ToString());
                    if (cboPosition.Items.Count > 0)
                    {
                        cboPosition.SelectedIndex = 0;
                    }
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

        private void picAddPosition_Click(object sender, EventArgs e)
        {
            frmAddPosition f = new frmAddPosition(this);
            f.LoadExistingPostion();
            f.ShowDialog();
            this.Dispose();
        }

        private void picAddChairmanship_Click(object sender, EventArgs e)
        {
            frmAddChairmanship f = new frmAddChairmanship(this);
            f.LoadExistingChairmanship();
            f.ShowDialog();
            this.Dispose();
        }
    }
}
