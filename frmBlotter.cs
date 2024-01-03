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
        SqlCommand cm;
        SqlDataReader dr;
        frmIssue f;
        public string _id;
        public string DbString = @"Data Source = MUNDAS26\SQLEXPRESS; Initial Catalog = bmis; Integrated Security = True";
        public frmBlotter(frmIssue f)
        {
            InitializeComponent();
            _sqlConnection = new SqlConnection(DbString);
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
                    cm = new SqlCommand("insert into tblBlotter (fileno,barangay, purok, incident, place, idate, itime, complainant, witness1, witness2, narrative) values (@fileno, @barangay, @purok, @incident, @place, @idate, @itime, @complainant, @witness1, @witness2, @narrative)", _sqlConnection);
                    cm.Parameters.AddWithValue("@fileno", lblFileno.Text);
                    cm.Parameters.AddWithValue("@barangay", txtBrgy.Text);
                    cm.Parameters.AddWithValue("@purok", txtPurok.Text);
                    cm.Parameters.AddWithValue("@incident", txtIncident.Text);
                    cm.Parameters.AddWithValue("@place", txtPlaceofincident.Text);
                    cm.Parameters.AddWithValue("@idate", DateTime.Parse(DtDate.Value.ToLongDateString()));
                    cm.Parameters.AddWithValue("@itime", txtTime.Text);
                    cm.Parameters.AddWithValue("@complainant", txtComplainant.Text);
                    cm.Parameters.AddWithValue("@witness1", txtWitness1.Text);
                    cm.Parameters.AddWithValue("@witness2", txtWitness2.Text);
                    cm.Parameters.AddWithValue("@narrative", txtNarrative.Text);
                    cm.ExecuteNonQuery();
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
                cm = new SqlCommand("Select  top 1 fileno from  tblBlotter  where fileno like '" + fileno + "%' order by id desc", _sqlConnection);
                dr = cm.ExecuteReader();
                dr.Read();
                if (dr.HasRows)
                {
                    lblFileno.Text = GetFileNO();
                    dr.Close();
                    _sqlConnection.Close();
                }
                else
                {
                    dr.Close();
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
                    cm = new SqlCommand("Update tblBlotter set barangay=@barangay, purok=@purok, incident=@incident, place=@place, idate=@idate, itime=@itime, complainant=@complainant, witness1=@witness1, witness2=@witness2, narrative=@narrative where id=@id", _sqlConnection);
                    cm.Parameters.AddWithValue("barangay", txtBrgy.Text);
                    cm.Parameters.AddWithValue("purok", txtBrgy.Text);
                    cm.Parameters.AddWithValue("incident", txtIncident.Text);
                    cm.Parameters.AddWithValue("place", txtPlaceofincident.Text);
                    cm.Parameters.AddWithValue("idate", DtDate.Value);
                    cm.Parameters.AddWithValue("itime", txtTime.Text);
                    cm.Parameters.AddWithValue("complainant", txtComplainant.Text);
                    cm.Parameters.AddWithValue("witness1", txtWitness1.Text);
                    cm.Parameters.AddWithValue("witness2", txtWitness2.Text);
                    cm.Parameters.AddWithValue("narrative", txtNarrative.Text);
                    cm.Parameters.AddWithValue("id", _id);
                    cm.ExecuteNonQuery();
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
