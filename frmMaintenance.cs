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
        SqlConnection cn;
        SqlCommand cm;
        SqlDataReader dr;
        public string _id;
        public string DbString = @"Data Source = MUNDAS26\SQLEXPRESS; Initial Catalog = bmis; Integrated Security = True";
        public frmMaintenance()
        {
            InitializeComponent();
            cn = new SqlConnection(DbString);
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
                cn.Open();
                cm = new SqlCommand("Select * from tblOfficial", cn);
                dr = cm.ExecuteReader();
                while (dr.Read())
                {
                    viewBrgy.Rows.Add(dr["id"].ToString(), dr["name"].ToString(), dr["chairmanship"].ToString(),
                    dr["position"].ToString(), DateTime.Parse(dr["termstart"].ToString()).ToShortDateString(), DateTime.Parse(dr["termend"].ToString()).ToShortDateString(), dr["status"].ToString());
                }
                dr.Close();
                cn.Close();
                viewBrgy.ClearSelection();
            }
            catch (Exception ex)
            {
                cn.Close();
                MessageBox.Show(ex.Message, vars._title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public void LoadPurok()//populate all of this values into DATAVIEW(viewPurok) display
        {
            try
            {
                viewPurok.Rows.Clear();
                cn.Open();
                cm = new SqlCommand("Select *from tblPurok", cn);
                dr = cm.ExecuteReader();
                while (dr.Read())
                {
                    viewPurok.Rows.Add(dr["purok"].ToString(), dr["chairman"].ToString());
                }
                dr.Close();
                cn.Close();
                viewPurok.ClearSelection();
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
                        cn.Open();
                        cm = new SqlCommand("delete  from tblPurok where purok like '" + viewPurok.Rows[e.RowIndex].Cells[0].Value.ToString() + "'", cn);
                        cm.ExecuteNonQuery();
                        cn.Close();
                        MessageBox.Show("Record  has been successfully deleted!", vars._title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadPurok();
                    }
                }
            }
            catch (Exception ex)
            {
                cn.Close();
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
                    cn.Open();
                    cm = new SqlCommand("Select photopost as Pictures, *from tblOfficial where id like '" + viewBrgy.Rows[e.RowIndex].Cells[0].Value.ToString() + "'", cn);
                    dr = cm.ExecuteReader();
                    dr.Read();
                    if (dr.HasRows)
                    {
                        f._id = dr["id"].ToString();
                        f.txtName.Text = dr["name"].ToString();
                        f.cboChairmanship.Text = dr["chairmanship"].ToString();
                        f.cboPosition.Text = dr["position"].ToString();
                        f.dtStart.Value = DateTime.Parse(dr["termstart"].ToString());
                        f.dtEnd.Value = DateTime.Parse(dr["termend"].ToString());
                        f.cboStatus.Text = dr["status"].ToString();

                        string imagePath = dr["photopost"].ToString();
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
                    dr.Close();
                    cn.Close();
                    f.ShowDialog();
                }
                else if (colname == "btnDelete1")
                {
                    string imagePath;
                    string imagePath2;
                    cn.Open();
                    cm = new SqlCommand("Select photopost as OfficialPost, idPic as idPictures, *from tblOfficial where id like '" + viewBrgy.Rows[e.RowIndex].Cells[0].Value.ToString() + "'", cn);
                    dr = cm.ExecuteReader();
                    dr.Read();
                    if (dr.HasRows)
                    {
                        imagePath = dr["photopost"].ToString();
                        if (File.Exists(imagePath))
                        {
                            File.Delete(imagePath);
                        }
                        imagePath2 = dr["idPic"].ToString();
                        if (File.Exists(imagePath2))
                        {
                            File.Delete(imagePath2);
                        }
                    }
                    dr.Close();
                    cn.Close();
                    if (MessageBox.Show("Do you want to delete is record?", vars._title, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        frmOfficial f = new frmOfficial(this);
                        cn.Open();
                        cm = new SqlCommand("Update tblChairmanship set status= 'InActive' where role like '"+viewBrgy.Rows[e.RowIndex].Cells[2].Value.ToString()+"'",cn);
                        cm.ExecuteNonQuery();
                        cn.Close();

                        cn.Open();
                        cm = new SqlCommand("Update tblPosition set status= 'InActive' where position like '" + viewBrgy.Rows[e.RowIndex].Cells[3].Value.ToString() + "'", cn);
                        cm.ExecuteNonQuery();
                        cn.Close();

                        cn.Open();
                        cm = new SqlCommand("delete from tblOfficial where id like '" + viewBrgy.Rows[e.RowIndex].Cells[0].Value.ToString() + "'", cn);
                        cm.ExecuteNonQuery();
                        cn.Close();
                        MessageBox.Show("Record  has been successfully deleted!", vars._title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadRecordOfficial();
                        LoadRecordAccount();
                        f.LoadPosition();
                    }
                }
            }
            catch (Exception ex)
            {
                cn.Close();
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
                cn.Open();
                cm = new SqlCommand("Select *from tblOfficial where accountStatus like 'Completed'", cn);
                dr = cm.ExecuteReader();
                while (dr.Read())
                {
                    viewAccount.Rows.Add(dr["id"].ToString(), dr["name"].ToString(), dr["position"].ToString());
                }
                dr.Close();
                cn.Close();
                viewAccount.ClearSelection();
            }
            catch (Exception ex)
            {
                cn.Close();
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
                    cn.Open();
                    cm = new SqlCommand("Select *from tblOfficial where id like '"+viewAccount.Rows[e.RowIndex].Cells[0].Value.ToString()+"'", cn);
                    dr = cm.ExecuteReader();
                    dr.Read();
                    if (dr.HasRows)
                    {
                        f._id = dr["id"].ToString();
                        f.txtName.Text = dr["name"].ToString();
                        f.cboPosition.Text = dr["position"].ToString();
                        f.txtUser.Text = dr["username"].ToString();
                        f.txtPass.Text = dr["password"].ToString();
                        f.txtConfirmPass.Text = dr["password"].ToString();

                        string imagePath = dr["idPic"].ToString();
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
                    cn.Close();
                    dr.Close();
                    f.ShowDialog();
                }
                else if (colname == "btnDelAccount")
                {
                    if (MessageBox.Show("Do you want to delete is record?", vars._title, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        string selectedRow = viewAccount.Rows[e.RowIndex].Cells[0].Value.ToString();
                        cn.Open();
                        cm = new SqlCommand("Update tblOfficial set accountStatus= 'Incomplete' where position like '"+viewAccount.Rows[e.RowIndex].Cells[2].Value.ToString()+"'",cn);
                        cm.ExecuteNonQuery();
                        cn.Close();

                        cn.Open();
                        cm = new SqlCommand("delete  from tblUser where id like '" + selectedRow + "'", cn);
                        cm.ExecuteNonQuery();
                        cn.Close();
                        MessageBox.Show("Record  has been successfully deleted!", vars._title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadRecordAccount();
                    }
                }
            }
            catch (Exception ex)
            {
                cn.Close();
                MessageBox.Show(ex.Message, vars._title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        public void LoadExistingPositionIntoDataview()
        {
            try
            {
                viewExistingPosition.Rows.Clear();
                cn.Open();
                cm = new SqlCommand("Select *from tblPosition",cn);
                dr = cm.ExecuteReader();
                while (dr.Read())
                {
                    viewExistingPosition.Rows.Add(dr["id"].ToString(), dr["position"].ToString(), dr["status"].ToString());
                }
                dr.Close();
                cn.Close();
                viewExistingPosition.ClearSelection();
            }
            catch (Exception ex)
            {
                cn.Close();
                MessageBox.Show(ex.Message, vars._title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        public void LoadExistingChairmanshipIntoDataview()
        {
            try
            {
                viewExistingChairmanship.Rows.Clear();
                cn.Open();
                cm = new SqlCommand("Select *from tblChairmanship", cn);
                dr = cm.ExecuteReader();
                while (dr.Read())
                {
                    viewExistingChairmanship.Rows.Add(dr["id"].ToString(), dr["role"].ToString(), dr["status"].ToString());
                }
                dr.Close();
                cn.Close();
                viewExistingChairmanship.ClearSelection();
            }
            catch (Exception ex)
            {
                cn.Close();
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
                        cn.Open();
                        cm = new SqlCommand("delete from tblPosition where id like '" + viewExistingPosition.Rows[e.RowIndex].Cells[0].Value.ToString() + "'", cn);
                        cm.ExecuteNonQuery();
                        cn.Close();
                        MessageBox.Show("Record  has been successfully deleted!", vars._title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadExistingPositionIntoDataview();

                    }
                }
            }
            catch (Exception ex)
            {
                cn.Close();
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
                        cn.Open();
                        cm = new SqlCommand("delete from tblChairmanship where id like '" + viewExistingChairmanship.Rows[e.RowIndex].Cells[0].Value.ToString() + "'", cn);
                        cm.ExecuteNonQuery();
                        cn.Close();
                        MessageBox.Show("Record  has been successfully deleted!", vars._title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadExistingChairmanshipIntoDataview();

                    }
                }
            }
            catch (Exception ex)
            {
                cn.Close();
                MessageBox.Show(ex.Message, vars._title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void picClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
