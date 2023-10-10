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
using Microsoft.Reporting.WinForms;

namespace BMIS
{
    public partial class frmPrintAllReports : Form
    {
        SqlConnection cn;
        SqlCommand cm;
        SqlDataReader dr;
        public string _refno;
        public string _captain;
        public string _Address;
        public string _BC;
        public string _Name;
        public string _Purpose;
        public string DbString = @"Data Source = MUNDAS26\SQLEXPRESS; Initial Catalog = bmis; Integrated Security = True";
        public frmPrintAllReports()
        {
            InitializeComponent();
            cn = new SqlConnection(DbString);
        }
        public void GetKapitan()
        {
            cn.Open();
            cm = new SqlCommand("Select *from tblOfficial where position like 'KAPITAN'", cn);
            dr = cm.ExecuteReader();
            dr.Read();
            if (dr.HasRows)
            {
                _captain = dr["name"].ToString();
            }
            dr.Close();
            cn.Close();
        }
        public void GenerateBlotterReport(string sql)
        {
            try
            {
                GetKapitan();
                ReportDataSource rds;
                reportViewer1.LocalReport.ReportPath = @"C:\Projects\BARANGAY MANAGEMENT AND INFORMATION SYSTEM\rptBlotter.rdlc";
                reportViewer1.LocalReport.DataSources.Clear();

                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter();

                cn.Open();
                da.SelectCommand = new SqlCommand(sql, cn);
                da.Fill(ds.Tables["tblBlotter"]);

                ReportParameter pKapitan = new ReportParameter("pKapitan", _captain);
                reportViewer1.LocalReport.SetParameters(pKapitan);

                rds = new ReportDataSource("DataSetForBlotter", ds.Tables["tblBlotter"]);
                reportViewer1.LocalReport.DataSources.Add(rds);
                reportViewer1.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
                reportViewer1.ZoomMode = ZoomMode.Percent;
                reportViewer1.ZoomPercent = 100;
                cn.Close();
            }
            catch (Exception ex)
            {
                cn.Close();
                MessageBox.Show(ex.Message, vars._title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        public void GenerateBusinessReport(string sql)
        {
            try
            {
                GetKapitan();
                ReportDataSource rds;
                reportViewer1.LocalReport.ReportPath = @"C:\Projects\BARANGAY MANAGEMENT AND INFORMATION SYSTEM\rptBusiness.rdlc";
                reportViewer1.LocalReport.DataSources.Clear();

                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter();

                cn.Open();
                da.SelectCommand = new SqlCommand(sql, cn);
                da.Fill(ds.Tables["tblBusiness"]);

                ReportParameter pKapitan = new ReportParameter("pKapitan", _captain);
                reportViewer1.LocalReport.SetParameters(pKapitan);

                rds = new ReportDataSource("DataSetForBusiness", ds.Tables["tblBusiness"]);
                reportViewer1.LocalReport.DataSources.Add(rds);
                reportViewer1.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
                reportViewer1.ZoomMode = ZoomMode.Percent;
                reportViewer1.ZoomPercent = 100;
                cn.Close();
            }
            catch (Exception ex)
            {
                cn.Close();
                MessageBox.Show(ex.Message, vars._title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        public void GenerateClearanceReport(string _Address, string _Name, string _Purpose, string _BC, string _refno)
        {
            try
            {
                GetKapitan();
                ReportDataSource rds;
                reportViewer1.LocalReport.ReportPath = @"C:\Projects\BARANGAY MANAGEMENT AND INFORMATION SYSTEM\rptClearance.rdlc";
                reportViewer1.LocalReport.DataSources.Clear();

                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter();

                cn.Open();
                da.SelectCommand = new SqlCommand("Select *from tblDocument where refno = '" + _refno + "'", cn);
                da.Fill(ds.Tables["tblClearance"]);
                
                ReportParameter pKapitan = new ReportParameter("pKapitan", _captain);
                ReportParameter pDay = new ReportParameter("pDay", vars.formattedDay);
                ReportParameter pMonth = new ReportParameter("pMonth", DateTime.Now.ToString("MMMM"));
                ReportParameter pYear = new ReportParameter("pYear", DateTime.Now.Year.ToString());
                ReportParameter pAddress = new ReportParameter("pAddress", _Address);
                ReportParameter pName = new ReportParameter("pName", _Name);
                ReportParameter pPurpose = new ReportParameter("pPurpose", _Purpose);
                ReportParameter pBC = new ReportParameter("pBC", _BC);

                reportViewer1.LocalReport.SetParameters(pKapitan);
                reportViewer1.LocalReport.SetParameters(pDay);
                reportViewer1.LocalReport.SetParameters(pMonth);
                reportViewer1.LocalReport.SetParameters(pYear);
                reportViewer1.LocalReport.SetParameters(pAddress);
                reportViewer1.LocalReport.SetParameters(pName);
                reportViewer1.LocalReport.SetParameters(pPurpose);
                reportViewer1.LocalReport.SetParameters(pBC);

                rds = new ReportDataSource("DataSetForClearance", ds.Tables["tblClearance"]);
                reportViewer1.LocalReport.DataSources.Add(rds);
                reportViewer1.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
                reportViewer1.ZoomMode = ZoomMode.Percent;
                reportViewer1.ZoomPercent = 100;
                cn.Close();
            }
            catch (Exception ex)
            {
                cn.Close();
                MessageBox.Show(ex.Message, vars._title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        public void GenerateBuildingReport(string _Name, string _Purpose, string _BC, string _refno)
        {
            try
            {
                GetKapitan();
                ReportDataSource rds;
                reportViewer1.LocalReport.ReportPath = @"C:\Projects\BARANGAY MANAGEMENT AND INFORMATION SYSTEM\rptBuildingPermit.rdlc";
                reportViewer1.LocalReport.DataSources.Clear();

                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter();

                cn.Open();
                da.SelectCommand = new SqlCommand("Select *from tblDocument where refno = '" + _refno + "'", cn);
                da.Fill(ds.Tables["tblBuildingPermit"]);

                ReportParameter pKapitan = new ReportParameter("pKapitan", _captain);
                ReportParameter pDay = new ReportParameter("pDay", vars.formattedDay);
                ReportParameter pMonth = new ReportParameter("pMonth", DateTime.Now.ToString("MMMM"));
                ReportParameter pYear = new ReportParameter("pYear", DateTime.Now.Year.ToString());
                ReportParameter pName = new ReportParameter("pName", _Name);
                ReportParameter pPurpose = new ReportParameter("pPurpose", _Purpose);
                ReportParameter pBC = new ReportParameter("pBC", _BC);

                reportViewer1.LocalReport.SetParameters(pKapitan);
                reportViewer1.LocalReport.SetParameters(pDay);
                reportViewer1.LocalReport.SetParameters(pMonth);
                reportViewer1.LocalReport.SetParameters(pYear);
                reportViewer1.LocalReport.SetParameters(pName);
                reportViewer1.LocalReport.SetParameters(pPurpose);
                reportViewer1.LocalReport.SetParameters(pBC);

                rds = new ReportDataSource("DataSetForBuildingPermit", ds.Tables["tblBuildingPermit"]);
                reportViewer1.LocalReport.DataSources.Add(rds);
                reportViewer1.SetDisplayMode(DisplayMode.PrintLayout);
                reportViewer1.ZoomMode = ZoomMode.Percent;
                reportViewer1.ZoomPercent = 100;
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
