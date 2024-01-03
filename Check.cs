using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace BMIS
{
    public class Check
    {
        public static SqlConnection _sqlConnection;
        public static SqlCommand _sqlCommand;
        public static string DbString = @"Data Source = .; Initial Catalog = bmis; Integrated Security = True";
        public static bool checkDuplicate(string sql)
        {
            bool IsUserExisted = false;
            try
            {
                _sqlConnection = new SqlConnection(DbString);
                _sqlConnection.Open();
                _sqlCommand = new SqlCommand(sql, _sqlConnection);
                int count = (int)_sqlCommand.ExecuteScalar();
                _sqlConnection.Close();

                if (count == 0)
                    IsUserExisted = false;
                else
                    IsUserExisted = true;
            }
            catch (Exception ex)
            {
                _sqlConnection.Close();
                MessageBox.Show(ex.Message, vars._title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            return IsUserExisted;
        }
    }
}
