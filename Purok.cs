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
    public partial class frmPurok : Form
    {
        SqlConnection _sqlConnection;
        SqlCommand _sqlCommand;
        frmMaintenance f;
        public string _purok;
        
        public frmPurok(frmMaintenance f)
        {
            InitializeComponent();
            _sqlConnection = new SqlConnection(vars.DbString);
            this.f = f;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            var textBoxes = new[]
           { txtChairman,txtPurok};
            
            foreach (var tb in textBoxes)
            {
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
                if (MessageBox.Show("Do you want to save this record?", vars._title, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    _sqlConnection.Open();
                    _sqlCommand = new SqlCommand("Insert into tblPurok(purok, chairman)values(@purok, @chairman)", _sqlConnection);
                    _sqlCommand.Parameters.AddWithValue("@purok", txtPurok.Text);
                    _sqlCommand.Parameters.AddWithValue("@chairman", txtChairman.Text);
                    _sqlCommand.ExecuteNonQuery();
                    _sqlConnection.Close();
                    MessageBox.Show("Record has been successfully saved!", vars._title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    f.LoadPurok();
                    Clear();
                }
            }
            catch (Exception ex)
            {
                _sqlConnection.Close();
                MessageBox.Show(ex.Message, vars._title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            var textBoxes = new[]
           { txtChairman,txtPurok};

            foreach (var tb in textBoxes)
            {
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
                    _sqlConnection.Open();
                    _sqlCommand = new SqlCommand("update tblPurok set purok= @purok, chairman= @chairman where purok= @purok1", _sqlConnection);
                    _sqlCommand.Parameters.AddWithValue("@purok", txtPurok.Text);
                    _sqlCommand.Parameters.AddWithValue("@chairman", txtChairman.Text);
                    _sqlCommand.Parameters.AddWithValue("@purok1", _purok);
                    _sqlCommand.ExecuteNonQuery();
                    _sqlConnection.Close();
                    MessageBox.Show("Record has been successfully updated!", vars._title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Clear();
                    f.LoadPurok();
                    this.Dispose();
                }
            }
            catch (Exception ex)
            {
                _sqlConnection.Close();
                MessageBox.Show(ex.Message, vars._title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void Clear()
        {
            txtPurok.Clear();
            txtChairman.Clear();
            txtPurok.Focus();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Clear();
        }
    }
}
