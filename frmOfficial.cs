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
        SqlCommand cm;
        SqlDataReader dr;
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
                        cm = new SqlCommand("Update tblChairmanship set status= 'Active' where role like '" + cboChairmanship.Text + "'", _sqlConnection);
                        cm.ExecuteNonQuery();
                        _sqlConnection.Close();

                        _sqlConnection.Open();
                        cm = new SqlCommand("Update tblPosition set status= 'Active' where position like '" + cboPosition.Text + "'", _sqlConnection);
                        cm.ExecuteNonQuery();
                        _sqlConnection.Close();

                        string newFilename = vars.GetRandomNumbers(10);
                        string imagePath = @".\OfficialPostImages\" + newFilename + ".jpg";
                        Image newImage = picPhotoPost.Image;
                        newImage.Save(imagePath);

                        _sqlConnection.Open();
                        cm = new SqlCommand("insert into tblOfficial (name, chairmanship, position, termstart, termend, photopost, status) values(@name, @chairmanship, @position, @termstart, @termend, @photopost, @status)", _sqlConnection);
                        cm.Parameters.AddWithValue("@name", txtName.Text);
                        cm.Parameters.AddWithValue("@chairmanship", cboChairmanship.Text);
                        cm.Parameters.AddWithValue("@position", cboPosition.Text);
                        cm.Parameters.AddWithValue("@termstart", dtStart.Value);
                        cm.Parameters.AddWithValue("@termend", dtEnd.Value);
                        cm.Parameters.AddWithValue("@photopost", imagePath);

                        cm.Parameters.AddWithValue("@status", cboStatus.Text);
                        cm.ExecuteNonQuery();
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
                    cm = new SqlCommand("update tblOfficial set name= @name, chairmanship= @chairmanship, position= @position, termstart= @termstart, termend= @termend, photopost= @photopost, status= @status where id= @id", _sqlConnection);
                    cm.Parameters.AddWithValue("@name", txtName.Text);
                    cm.Parameters.AddWithValue("@chairmanship", cboChairmanship.Text);
                    cm.Parameters.AddWithValue("@position", cboPosition.Text);
                    cm.Parameters.AddWithValue("@termstart", dtStart.Value);
                    cm.Parameters.AddWithValue("@termend", dtEnd.Value);
                    cm.Parameters.AddWithValue("@status", cboStatus.Text);
                    cm.Parameters.AddWithValue("@id", _id);
                    cm.Parameters.AddWithValue("@photopost", newImagePath);
                    
                    newImage.Save(newImagePath);
                    cm.ExecuteNonQuery();
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
                cm = new SqlCommand("Select photopost as Pictures, *from tblOfficial where id like '" + _id + "'", _sqlConnection);
                dr = cm.ExecuteReader();
                dr.Read();
                if (dr.HasRows)
                {
                    imagePath = dr["photopost"].ToString();
                    if (File.Exists(imagePath))
                    {
                        File.Delete(imagePath);
                    }
                    _sqlConnection.Close();
                }
                dr.Close();
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
                    cm = new SqlCommand("Select photopost as Pictures, *from tblOfficial where id like '"+_id+"'",_sqlConnection);
                    dr = cm.ExecuteReader();
                    dr.Read();
                    if (dr.HasRows)
                    {
                        imagePath = dr["photopost"].ToString();
                        if (File.Exists(imagePath))
                        {
                            File.Delete(imagePath);
                        }
                        _sqlConnection.Close();
                    }
                    dr.Close();
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
                cm = new SqlCommand("Select role from tblChairmanship where status like 'InActive' order by id asc", _sqlConnection);
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
        public void LoadPosition()
        {
            try
            {
                _sqlConnection.Open();
                cm = new SqlCommand("Select position from tblPosition where status like 'InActive'order by id asc", _sqlConnection);
                dr = cm.ExecuteReader();
                while (dr.Read())
                {
                    cboPosition.Items.Add(dr[0].ToString());
                    if (cboPosition.Items.Count > 0)
                    {
                        cboPosition.SelectedIndex = 0;
                    }
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
