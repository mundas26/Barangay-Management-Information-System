using System;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace BMIS
{
    public partial class frmAccount : Form
    {
        SqlConnection cn;
        SqlCommand cm;
        SqlDataReader dr;
        public string _id;
        public frmMaintenance f;
        public frmSecurity f1;
        public string DbString = @"Data Source = MUNDAS26\SQLEXPRESS; Initial Catalog = bmis; Integrated Security = True";

        public frmAccount(frmMaintenance f)
        {
            InitializeComponent();
            cn = new SqlConnection(DbString);
            this.f = f;
        }

        public frmAccount(frmSecurity f1)
        {
            InitializeComponent();
            this.f1 = f1;
            cn = new SqlConnection(dbconstring.connection);
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
                            cn.Open();
                            cm = new SqlCommand("Update tblOfficial set accountStatus = 'Completed' where position like '" + cboPosition.Text + "'", cn);
                            cm.ExecuteNonQuery();
                            cn.Close();

                            var randomFilename = Path.GetRandomFileName() + ".jpg";
                            string folder = @".\Images";
                            string path = System.IO.Path.Combine(folder, randomFilename);

                            cn.Open();
                            cm = new SqlCommand("Insert into tblUser (name, username, password, role, pic) values (@name, @username, @password, @role, @pic)", cn);
                            cm.Parameters.AddWithValue("@name", txtName.Text);
                            cm.Parameters.AddWithValue("@username", txtUser.Text);
                            cm.Parameters.AddWithValue("@password", txtPass.Text);
                            cm.Parameters.AddWithValue("@role", cboPosition.Text);
                            cm.Parameters.AddWithValue("@pic", path);

                            Image image = picIDPicture.Image;
                            image.Save(path);

                            cm.ExecuteNonQuery();
                            dr.Close();
                            cn.Close();
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
                cn.Close();
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
                    cn.Open();
                    cm = new SqlCommand("Select idPic as IdPicture, *from tblOfficial where id like '"+_id+"'",cn);
                    dr = cm.ExecuteReader();
                    dr.Read();
                    if (dr.HasRows)
                    {
                        imagePath = dr["idPic"].ToString();
                        if (File.Exists(imagePath))
                        {
                            File.Delete(imagePath);
                            button1.Visible = false;
                        }
                        cn.Close();
                    }
                    dr.Close();
                    cn.Close();
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
                cn.Open();
                cm = new SqlCommand("Select idPic as IdPicture, *from tblOfficial where id like '"+_id+"'",cn);
                dr = cm.ExecuteReader();
                dr.Read();
                if (dr.HasRows)
                {
                    imagePath = dr["idPic"].ToString();
                    if (File.Exists(imagePath))
                    {
                        File.Delete(imagePath);
                    }
                }
                dr.Close();
                cn.Close();
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

                        cn.Open();
                        cm = new SqlCommand("UPDATE tblOfficial SET name=@name, position=@position, username=@username, password=@password, idPic=@idPic, accountStatus=@accountStatus WHERE position=@position", cn);
                        cm.Parameters.AddWithValue("@name", txtName.Text);
                        cm.Parameters.AddWithValue("@position", cboPosition.Text);
                        cm.Parameters.AddWithValue("@username", txtUser.Text);
                        cm.Parameters.AddWithValue("@password", txtPass.Text);
                        cm.Parameters.AddWithValue("@idPic", imagePath);
                        cm.Parameters.AddWithValue("@accountStatus", "Completed");

                        Image image = picIDPicture.Image;
                        image.Save(imagePath);

                        cm.ExecuteNonQuery();
                        cn.Close();
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
                    cn.Close();
                    MessageBox.Show("Password didn't match!", vars._title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                cn.Close();
                MessageBox.Show(ex.Message, vars._title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        public string GetPreviousImagePathFromDatabase()
        {
            string previousImagePath = "";

            string position = cboPosition.Text; 
            cn.Open();
            cm = new SqlCommand("Select idPic from tblOfficial where position = @position", cn);
            cm.Parameters.AddWithValue("@position", position);
            dr = cm.ExecuteReader();
            if (dr.Read())
            {
                previousImagePath = dr["idPic"].ToString();
            }
            dr.Close();
            cn.Close();
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
                cn.Open();
                cm = new SqlCommand("Select position from tblOfficial where accountStatus like 'Incomplete' order by id asc", cn);
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

        private void cboPosition_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                cn.Open();
                cm = new SqlCommand("Select *from tblOfficial where position like '" + cboPosition.Text + "'", cn);
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
    }
}
