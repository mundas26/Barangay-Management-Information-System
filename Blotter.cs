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
    public partial class frmBlotter : Form
    {
        SqlConnection _sqlConnection;
        SqlCommand _sqlCommand;
        SqlDataReader _sqlDataReader;
        frmIssue f;
        public string _id;
        
        public frmBlotter(frmIssue f)
        {
            InitializeComponent();
            _sqlConnection = new SqlConnection(vars.DbString);
            this.f = f;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            var textBoxes = new[]
            { txtBrgy, txtComplainant, txtIncident,txtNarrative,
                txtPlaceofincident,txtPurok,txtTime,txtWitness1,txtWitness2};
            
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
                if (MessageBox.Show("Do you want to save this blotter?", vars._title, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    _sqlConnection.Open();
                    _sqlCommand = new SqlCommand("insert into tblBlotter (fileno,barangay, purok, incident, place, idate, itime, complainant, witness1, witness2, narrative) values (@fileno, @barangay, @purok, @incident, @place, @idate, @itime, @complainant, @witness1, @witness2, @narrative)", _sqlConnection);
                    _sqlCommand.Parameters.AddWithValue("@fileno", lblFileno.Text);
                    _sqlCommand.Parameters.AddWithValue("@barangay", txtBrgy.Text);
                    _sqlCommand.Parameters.AddWithValue("@purok", txtPurok.Text);
                    _sqlCommand.Parameters.AddWithValue("@incident", txtIncident.Text);
                    _sqlCommand.Parameters.AddWithValue("@place", txtPlaceofincident.Text);
                    _sqlCommand.Parameters.AddWithValue("@idate", DateTime.Parse(DtDate.Value.ToLongDateString()));
                    _sqlCommand.Parameters.AddWithValue("@itime", txtTime.Text);
                    _sqlCommand.Parameters.AddWithValue("@complainant", txtComplainant.Text);
                    _sqlCommand.Parameters.AddWithValue("@witness1", txtWitness1.Text);
                    _sqlCommand.Parameters.AddWithValue("@witness2", txtWitness2.Text);
                    _sqlCommand.Parameters.AddWithValue("@narrative", txtNarrative.Text);
                    _sqlCommand.ExecuteNonQuery();
                    _sqlConnection.Close();
                    MessageBox.Show("Record has been successfully saved!", vars._title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Clear();
                    f.loadBlotter();
                }
            }
            catch (Exception ex)
            {
                _sqlConnection.Close();
                MessageBox.Show(ex.Message, vars._title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        public string GetFileNO()
        {
            string fileno = "CASE-";
            Random rnd = new Random();
            for (int x = 0; x < 6; x++)
            {
                fileno += rnd.Next(1, 9).ToString();
            }
            try
            {
                _sqlConnection.Open();
                _sqlCommand = new SqlCommand("Select  top 1 fileno from  tblBlotter  where fileno like '" + fileno + "%' order by id desc", _sqlConnection);
                _sqlDataReader = _sqlCommand.ExecuteReader();
                _sqlDataReader.Read();
                if (_sqlDataReader.HasRows)
                {
                    lblFileno.Text = GetFileNO();
                    _sqlDataReader.Close();
                    _sqlConnection.Close();
                }
                else
                {
                    _sqlDataReader.Close();
                    _sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                _sqlConnection.Close();
                MessageBox.Show(ex.Message, vars._title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            return fileno;
        }
        public void Clear()
        {
            txtBrgy.Clear();
            txtPurok.Text = "";
            txtIncident.Clear();
            txtPlaceofincident.Clear();
            DtDate.Value = DateTime.Now;
            txtTime.Clear();
            txtComplainant.Clear();
            txtWitness1.Clear();
            txtWitness2.Clear();
            txtNarrative.Clear();
            txtBrgy.Focus();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            var textBoxes = new[]
           { txtBrgy, txtComplainant, txtIncident,txtNarrative,
                txtPlaceofincident,txtPurok,txtTime,txtWitness1,txtWitness2};

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
                if (MessageBox.Show("Are you sure you want to update this Record?", vars._title, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    _sqlConnection.Open();
                    _sqlCommand = new SqlCommand("Update tblBlotter set barangay=@barangay, purok=@purok, incident=@incident, place=@place, idate=@idate, itime=@itime, complainant=@complainant, witness1=@witness1, witness2=@witness2, narrative=@narrative where id=@id", _sqlConnection);
                    _sqlCommand.Parameters.AddWithValue("barangay", txtBrgy.Text);
                    _sqlCommand.Parameters.AddWithValue("purok", txtBrgy.Text);
                    _sqlCommand.Parameters.AddWithValue("incident", txtIncident.Text);
                    _sqlCommand.Parameters.AddWithValue("place", txtPlaceofincident.Text);
                    _sqlCommand.Parameters.AddWithValue("idate", DtDate.Value);
                    _sqlCommand.Parameters.AddWithValue("itime", txtTime.Text);
                    _sqlCommand.Parameters.AddWithValue("complainant", txtComplainant.Text);
                    _sqlCommand.Parameters.AddWithValue("witness1", txtWitness1.Text);
                    _sqlCommand.Parameters.AddWithValue("witness2", txtWitness2.Text);
                    _sqlCommand.Parameters.AddWithValue("narrative", txtNarrative.Text);
                    _sqlCommand.Parameters.AddWithValue("id", _id);
                    _sqlCommand.ExecuteNonQuery();
                    _sqlConnection.Close();
                    MessageBox.Show("Record has been successfully Updated!", vars._title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    f.loadBlotter();
                    Clear();
                    this.Dispose();
                }
            }
            catch (Exception ex)
            {
                _sqlConnection.Close();
                MessageBox.Show(ex.Message, vars._title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
