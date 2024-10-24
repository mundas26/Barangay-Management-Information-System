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
    public partial class frmPayment : Form
    {
        SqlConnection _sqlConnection;
        SqlCommand _sqlCommand;
        Random rnd;
        frmPaymentList f;
        
        public frmPayment(frmPaymentList f)
        {
            InitializeComponent();
            _sqlConnection = new SqlConnection(vars.DbString);
            this.f = f;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            var textBoxes = new[]
           { txtAmount,txtname};
            var comboBoxes = new[]
            { cboTypeofpayment};

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
                if (MessageBox.Show("Do you want to save this payment?", vars._title, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    _sqlConnection.Open();
                    _sqlCommand = new SqlCommand("Insert into tblPayment (refno, name, type, amount, sdate, username) values(@refno, @name, @type, @amount, @sdate, @username)", _sqlConnection);
                    _sqlCommand.Parameters.AddWithValue("@refno", lblRefno.Text);
                    _sqlCommand.Parameters.AddWithValue("@name", txtname.Text);
                    _sqlCommand.Parameters.AddWithValue("@type", cboTypeofpayment.Text);
                    _sqlCommand.Parameters.AddWithValue("@amount", double.Parse(txtAmount.Text));
                    _sqlCommand.Parameters.AddWithValue("@sdate", DateTime.Now);
                    _sqlCommand.Parameters.AddWithValue("@username", vars.Users);
                    _sqlCommand.ExecuteNonQuery();
                    _sqlConnection.Close();
                    MessageBox.Show("Record has been successfully saved!", vars._title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Clear();
                    f.LoadPaymentTotalRecord();
                    this.Dispose();
                }
            }
            catch (Exception ex)
            {
                _sqlConnection.Close();
                MessageBox.Show(ex.Message, vars._title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                GetReferenceNO();
            }
        }
        public void Clear()
        {
            GetReferenceNO();
            cboTypeofpayment.Text = "";
            txtname.Clear();
            txtAmount.Clear();
        }
        public void GetReferenceNO()
        {
            rnd = new Random();
            lblRefno.Text = DateTime.Now.ToString("yyyymmdd-") + rnd.Next(11111, 99999);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void txtAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsDigit(e.KeyChar) || e.KeyChar == (char)Keys.Back);
        }

        private void cboTypeofpayment_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
