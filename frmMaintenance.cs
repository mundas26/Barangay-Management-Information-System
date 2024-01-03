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

namespace BMIS
{
    public partial class frmMaintenance : Form
    {
        SqlConnection _sqlConnection;
        SqlCommand _sqlCommand;
        SqlDataReader _sqlDataReader;
        public string _id;
        public string DbString = @"Data Source = .; Initial Catalog = bmis; Integrated Security = True";
        public frmMaintenance()
        {
            InitializeComponent();
            _sqlConnection = new SqlConnection(DbString);
        }
        private void btnmaintenanceBrgyAddnew_Click(object sender, EventArgs e)
        {
            frmOfficial f = new frmOfficial(this);
            f.btnUpdate.Enabled = false;
            f.LoadChairmanship();
            f.LoadPosition();
            f.Clear();
            f.ShowDialog();
        }
        public void LoadRecordOfficial()//populate all of this values into DATAVIEW(viewBrgy) display
        {
            try
            {
                viewBrgy.Rows.Clear();
                _sqlConnection.Open();
                _sqlCommand = new SqlCommand("Select * from tblOfficial", _sqlConnection);
                _sqlDataReader = _sqlCommand.ExecuteReader();
                while (_sqlDataReader.Read())
                {
                    viewBrgy.Rows.Add(_sqlDataReader["id"].ToString(), _sqlDataReader["name"].ToString(), _sqlDataReader["chairmanship"].ToString(),
                    _sqlDataReader["position"].ToString(), DateTime.Parse(_sqlDataReader["termstart"].ToString()).ToShortDateString(), DateTime.Parse(_sqlDataReader["termend"].ToString()).ToShortDateString(), _sqlDataReader["status"].ToString());
                }
                _sqlDataReader.Close();
                _sqlConnection.Close();
                viewBrgy.ClearSelection();
            }
            catch (Exception ex)
            {
                _sqlConnection.Close();
                MessageBox.Show(ex.Message, vars._title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public void LoadPurok()//populate all of this values into DATAVIEW(viewPurok) display
        {
            try
            {
                viewPurok.Rows.Clear();
                _sqlConnection.Open();
                _sqlCommand = new SqlCommand("Select *from tblPurok", _sqlConnection);
                _sqlDataReader = _sqlCommand.ExecuteReader();
                while (_sqlDataReader.Read())
                {
                    viewPurok.Rows.Add(_sqlDataReader["purok"].ToString(), _sqlDataReader["chairman"].ToString());
                }
                _sqlDataReader.Close();
                _sqlConnection.Close();
                viewPurok.ClearSelection();
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

        private void btnMaintenancePurokAddnew2_Click(object sender, EventArgs e)
        {
            frmPurok f = new frmPurok(this);
            f.btnUpdate.Enabled = false;
            f.ShowDialog();
        }
        private void viewPurok_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                string colname = viewPurok.Columns[e.ColumnIndex].Name;
                if (colname == "btnEdit2")
                {
                    frmPurok f = new frmPurok(this);
                    f.btnSave.Enabled = false;
                    f._purok = viewPurok.Rows[e.RowIndex].Cells[0].Value.ToString();
                    f.txtPurok.Text = viewPurok.Rows[e.RowIndex].Cells[0].Value.ToString();
                    f.txtChairman.Text = viewPurok.Rows[e.RowIndex].Cells[1].Value.ToString();
                    f.ShowDialog();
                }
                else if (colname == "btnDelete2")
                {
                    if (MessageBox.Show("Do you want to delete is record?", vars._title, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        _sqlConnection.Open();
                        _sqlCommand = new SqlCommand("delete  from tblPurok where purok like '" + viewPurok.Rows[e.RowIndex].Cells[0].Value.ToString() + "'", _sqlConnection);
                        _sqlCommand.ExecuteNonQuery();
                        _sqlConnection.Close();
                        MessageBox.Show("Record  has been successfully deleted!", vars._title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadPurok();
                    }
                }
            }
            catch (Exception ex)
            {
                _sqlConnection.Close();
                MessageBox.Show(ex.Message, vars._title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void viewBrgy_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                string colname = viewBrgy.Columns[e.ColumnIndex].Name;
                if (colname == "btnEdit1")
                {
                    frmOfficial f = new frmOfficial(this);
                    f.btnSave.Enabled = false;
                    f.cboChairmanship.DropDownStyle = ComboBoxStyle.Simple;
                    f.cboPosition.DropDownStyle = ComboBoxStyle.Simple;
                    f.cboChairmanship.Enabled = false;
                    f.cboPosition.Enabled = false;
                    _sqlConnection.Open();
                    _sqlCommand = new SqlCommand("Select photopost as Pictures, *from tblOfficial where id like '" + viewBrgy.Rows[e.RowIndex].Cells[0].Value.ToString() + "'", _sqlConnection);
                    _sqlDataReader = _sqlCommand.ExecuteReader();
                    _sqlDataReader.Read();
                    if (_sqlDataReader.HasRows)
                    {
                        f._id = _sqlDataReader["id"].ToString();
                        f.txtName.Text = _sqlDataReader["name"].ToString();
                        f.cboChairmanship.Text = _sqlDataReader["chairmanship"].ToString();
                        f.cboPosition.Text = _sqlDataReader["position"].ToString();
                        f.dtStart.Value = DateTime.Parse(_sqlDataReader["termstart"].ToString());
                        f.dtEnd.Value = DateTime.Parse(_sqlDataReader["termend"].ToString());
                        f.cboStatus.Text = _sqlDataReader["status"].ToString();

                        string imagePath = _sqlDataReader["photopost"].ToString();
                        if (!string.IsNullOrEmpty(imagePath) && File.Exists(imagePath))
                        {
                            Image image = Image.FromFile(imagePath);
                            try
                            {
                                f.picPhotoPost.Image = new Bitmap(image);
                                // Use the image as needed
                            }
                            finally
                            {
                                image.Dispose();
                                // At this point, the image has been disposed and the file is closed
                            }
                        }
                    }
                    _sqlDataReader.Close();
                    _sqlConnection.Close();
                    f.ShowDialog();
                }
                else if (colname == "btnDelete1")
                {
                    string imagePath;
                    string imagePath2;
                    _sqlConnection.Open();
                    _sqlCommand = new SqlCommand("Select photopost as OfficialPost, idPic as idPictures, *from tblOfficial where id like '" + viewBrgy.Rows[e.RowIndex].Cells[0].Value.ToString() + "'", _sqlConnection);
                    _sqlDataReader = _sqlCommand.ExecuteReader();
                    _sqlDataReader.Read();
                    if (_sqlDataReader.HasRows)
                    {
                        imagePath = _sqlDataReader["photopost"].ToString();
                        if (File.Exists(imagePath))
                        {
                            File.Delete(imagePath);
                        }
                        imagePath2 = _sqlDataReader["idPic"].ToString();
                        if (File.Exists(imagePath2))
                        {
                            File.Delete(imagePath2);
                        }
                    }
                    _sqlDataReader.Close();
                    _sqlConnection.Close();
                    if (MessageBox.Show("Do you want to delete is record?", vars._title, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        frmOfficial f = new frmOfficial(this);
                        _sqlConnection.Open();
                        _sqlCommand = new SqlCommand("Update tblChairmanship set status= 'InActive' where role like '"+viewBrgy.Rows[e.RowIndex].Cells[2].Value.ToString()+"'",_sqlConnection);
                        _sqlCommand.ExecuteNonQuery();
                        _sqlConnection.Close();

                        _sqlConnection.Open();
                        _sqlCommand = new SqlCommand("Update tblPosition set status= 'InActive' where position like '" + viewBrgy.Rows[e.RowIndex].Cells[3].Value.ToString() + "'", _sqlConnection);
                        _sqlCommand.ExecuteNonQuery();
                        _sqlConnection.Close();

                        _sqlConnection.Open();
                        _sqlCommand = new SqlCommand("delete from tblOfficial where id like '" + viewBrgy.Rows[e.RowIndex].Cells[0].Value.ToString() + "'", _sqlConnection);
                        _sqlCommand.ExecuteNonQuery();
                        _sqlConnection.Close();
                        MessageBox.Show("Record  has been successfully deleted!", vars._title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadRecordOfficial();
                        LoadRecordAccount();
                        f.LoadPosition();
                    }
                }
            }
            catch (Exception ex)
            {
                _sqlConnection.Close();
                MessageBox.Show(ex.Message, vars._title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnAddAcount_Click(object sender, EventArgs e)
        {
            frmAccount f = new frmAccount(this);
            f.LoadPositionForMakingAnNewAccount();
            f.Clear();
            f.ShowDialog();
        }
        public void LoadRecordAccount()//populate all of this values into DATAVIEW(viewAccount) display
        {
            try
            {
                viewAccount.Rows.Clear();
                _sqlConnection.Open();
                _sqlCommand = new SqlCommand("Select *from tblOfficial where accountStatus like 'Completed'", _sqlConnection);
                _sqlDataReader = _sqlCommand.ExecuteReader();
                while (_sqlDataReader.Read())
                {
                    viewAccount.Rows.Add(_sqlDataReader["id"].ToString(), _sqlDataReader["name"].ToString(), _sqlDataReader["position"].ToString());
                }
                _sqlDataReader.Close();
                _sqlConnection.Close();
                viewAccount.ClearSelection();
            }
            catch (Exception ex)
            {
                _sqlConnection.Close();
                MessageBox.Show(ex.Message, vars._title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void viewAccount_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                string colname = viewAccount.Columns[e.ColumnIndex].Name;
                if (colname =="btnEditAccount")
                {
                    frmAccount f = new frmAccount(this);
                    f.cboPosition.DropDownStyle = ComboBoxStyle.Simple;
                    f.cboPosition.Enabled = false;
                    f.txtName.Enabled = false;
                    _sqlConnection.Open();
                    _sqlCommand = new SqlCommand("Select *from tblOfficial where id like '"+viewAccount.Rows[e.RowIndex].Cells[0].Value.ToString()+"'", _sqlConnection);
                    _sqlDataReader = _sqlCommand.ExecuteReader();
                    _sqlDataReader.Read();
                    if (_sqlDataReader.HasRows)
                    {
                        f._id = _sqlDataReader["id"].ToString();
                        f.txtName.Text = _sqlDataReader["name"].ToString();
                        f.cboPosition.Text = _sqlDataReader["position"].ToString();
                        f.txtUser.Text = _sqlDataReader["username"].ToString();
                        f.txtPass.Text = _sqlDataReader["password"].ToString();
                        f.txtConfirmPass.Text = _sqlDataReader["password"].ToString();

                        string imagePath = _sqlDataReader["idPic"].ToString();
                        if (!string.IsNullOrEmpty(imagePath) && File.Exists(imagePath))
                        {
                            Image image = Image.FromFile(imagePath);
                            try
                            {
                                f.picIDPicture.Image = new Bitmap(image);
                            }
                            finally
                            {
                                image.Dispose();
                            }
                        }
                    }
                    f.btnSave.Enabled = false;
                    _sqlConnection.Close();
                    _sqlDataReader.Close();
                    f.ShowDialog();
                }
                else if (colname == "btnDelAccount")
                {
                    if (MessageBox.Show("Do you want to delete is record?", vars._title, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        string selectedRow = viewAccount.Rows[e.RowIndex].Cells[0].Value.ToString();
                        _sqlConnection.Open();
                        _sqlCommand = new SqlCommand("Update tblOfficial set accountStatus= 'Incomplete' where position like '"+viewAccount.Rows[e.RowIndex].Cells[2].Value.ToString()+"'",_sqlConnection);
                        _sqlCommand.ExecuteNonQuery();
                        _sqlConnection.Close();

                        _sqlConnection.Open();
                        _sqlCommand = new SqlCommand("delete  from tblUser where id like '" + selectedRow + "'", _sqlConnection);
                        _sqlCommand.ExecuteNonQuery();
                        _sqlConnection.Close();
                        MessageBox.Show("Record  has been successfully deleted!", vars._title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadRecordAccount();
                    }
                }
            }
            catch (Exception ex)
            {
                _sqlConnection.Close();
                MessageBox.Show(ex.Message, vars._title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        public void LoadExistingPositionIntoDataview()
        {
            try
            {
                viewExistingPosition.Rows.Clear();
                _sqlConnection.Open();
                _sqlCommand = new SqlCommand("Select *from tblPosition",_sqlConnection);
                _sqlDataReader = _sqlCommand.ExecuteReader();
                while (_sqlDataReader.Read())
                {
                    viewExistingPosition.Rows.Add(_sqlDataReader["id"].ToString(), _sqlDataReader["position"].ToString(), _sqlDataReader["status"].ToString());
                }
                _sqlDataReader.Close();
                _sqlConnection.Close();
                viewExistingPosition.ClearSelection();
            }
            catch (Exception ex)
            {
                _sqlConnection.Close();
                MessageBox.Show(ex.Message, vars._title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        public void LoadExistingChairmanshipIntoDataview()
        {
            try
            {
                viewExistingChairmanship.Rows.Clear();
                _sqlConnection.Open();
                _sqlCommand = new SqlCommand("Select *from tblChairmanship", _sqlConnection);
                _sqlDataReader = _sqlCommand.ExecuteReader();
                while (_sqlDataReader.Read())
                {
                    viewExistingChairmanship.Rows.Add(_sqlDataReader["id"].ToString(), _sqlDataReader["role"].ToString(), _sqlDataReader["status"].ToString());
                }
                _sqlDataReader.Close();
                _sqlConnection.Close();
                viewExistingChairmanship.ClearSelection();
            }
            catch (Exception ex)
            {
                _sqlConnection.Close();
                MessageBox.Show(ex.Message, vars._title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void viewExistingPosition_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                string colname = viewExistingPosition.Columns[e.ColumnIndex].Name;
                if (colname == "btnEditPosition")
                {
                    frmAddPosition f = new frmAddPosition(this);
                    f.txtAddPosition.Text = viewExistingPosition.Rows[e.RowIndex].Cells[1].Value.ToString();
                    f.btnAdd.Text = "UPDATE";
                    f.ShowDialog();
                }
                else if (colname == "btnDelPosition")
                {
                    if (MessageBox.Show("Do you want to delete is record?", vars._title, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        _sqlConnection.Open();
                        _sqlCommand = new SqlCommand("delete from tblPosition where id like '" + viewExistingPosition.Rows[e.RowIndex].Cells[0].Value.ToString() + "'", _sqlConnection);
                        _sqlCommand.ExecuteNonQuery();
                        _sqlConnection.Close();
                        MessageBox.Show("Record  has been successfully deleted!", vars._title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadExistingPositionIntoDataview();

                    }
                }
            }
            catch (Exception ex)
            {
                _sqlConnection.Close();
                MessageBox.Show(ex.Message, vars._title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void viewExistingChairmanship_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                string colname = viewExistingChairmanship.Columns[e.ColumnIndex].Name;
                if (colname == "btnEditChairmanship")
                {
                    frmAddChairmanship f = new frmAddChairmanship(this);
                    f.txtAddChairmanship.Text = viewExistingChairmanship.Rows[e.RowIndex].Cells[1].Value.ToString();
                    f.btnAdd.Text = "UPDATE";
                    f.ShowDialog();
                }
                else if (colname == "btnDelChairmanship")
                {
                    if (MessageBox.Show("Do you want to delete is record?", vars._title, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        _sqlConnection.Open();
                        _sqlCommand = new SqlCommand("delete from tblChairmanship where id like '" + viewExistingChairmanship.Rows[e.RowIndex].Cells[0].Value.ToString() + "'", _sqlConnection);
                        _sqlCommand.ExecuteNonQuery();
                        _sqlConnection.Close();
                        MessageBox.Show("Record  has been successfully deleted!", vars._title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadExistingChairmanshipIntoDataview();

                    }
                }
            }
            catch (Exception ex)
            {
                _sqlConnection.Close();
                MessageBox.Show(ex.Message, vars._title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void picClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
