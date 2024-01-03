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
    public partial class frmVaccination : Form
    {
        SqlConnection _sqlConnection;
        SqlCommand _sqlCommand;
        frmResidentList f;
        public string _id;
        public string DbString = @"Data Source = MUNDAS26\SQLEXPRESS; Initial Catalog = bmis; Integrated Security = True";
        public frmVaccination(frmResidentList f)
        {
            InitializeComponent();
            _sqlConnection = new SqlConnection(DbString);
            this.f = f;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cboVaccinationlist_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            var textBoxes = new[]
            {txtVaccinetype};
            var comboBoxes = new[]
            {cboVaccinationlist};

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
                if (string.IsNullOrEmpty(tb.Text))
                {
                    errorProvider.SetError(tb, "Please fill out this line!");
                    return;
                }
                else
                {
                    errorProvider.SetError(tb, "");
                }
            }

            try
            {
                if (MessageBox.Show("Do you want to save changes?", vars._title, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (cboVaccinationlist.Text == "NOT YET VACCINATED")
                    {
                        txtVaccinetype.Text = "N/A";
                        if (CheckDuplicate("Select count(*)from tblVaccine where rid like '" + _id + "'") == true)
                        {
                            _sqlConnection.Open();
                            _sqlCommand = new SqlCommand("Update tblvaccine set vaccine=@vaccine, status=@status where rid=@rid", _sqlConnection);
                            _sqlCommand.Parameters.AddWithValue("vaccine", txtVaccinetype.Text);
                            _sqlCommand.Parameters.AddWithValue("status", cboVaccinationlist.Text);
                            _sqlCommand.Parameters.AddWithValue("rid", _id);
                            _sqlCommand.ExecuteNonQuery();
                            _sqlConnection.Close();
                        }
                        else
                        {
                            _sqlConnection.Open();
                            _sqlCommand = new SqlCommand("insert into tblvaccine (rid,vaccine,status) values (@rid,@vaccine,@status)", _sqlConnection);
                            _sqlCommand.Parameters.AddWithValue("rid", _id);
                            _sqlCommand.Parameters.AddWithValue("vaccine", txtVaccinetype.Text);
                            _sqlCommand.Parameters.AddWithValue("status", cboVaccinationlist.Text);
                            _sqlCommand.ExecuteNonQuery();
                            _sqlConnection.Close();
                        }
                        MessageBox.Show("Record has been successfully saved!", vars._title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        f.LoadVaccination();
                        f.loadrecordResident();
                        this.Dispose();
                    }
                }
            }
            catch (Exception ex)
            {
                _sqlConnection.Close();
                MessageBox.Show(ex.Message, vars._title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        public bool CheckDuplicate(string sql)
        {
            bool duplicate = false;
            try
            {
                _sqlConnection.Open();
                _sqlCommand = new SqlCommand(sql, _sqlConnection);
                int count = int.Parse(_sqlCommand.ExecuteScalar().ToString());
                _sqlConnection.Close();

                if (count == 0)
                    duplicate = false;
                else
                    duplicate = true;
            }
            catch (Exception ex)
            {
                _sqlConnection.Close();
                MessageBox.Show(ex.Message, vars._title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            return duplicate;
        }

        private void cboVaccinationlist_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboVaccinationlist.Text == "1ST DOSE ONLY" || cboVaccinationlist.Text == "FULLY VACCINATED")
            {
                txtVaccinetype.Text = "";
            }
            else
            {
                txtVaccinetype.Text = "N/A";
            }
        }
    }
}
