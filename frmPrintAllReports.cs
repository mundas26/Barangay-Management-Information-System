﻿using System;
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
using System.IO;
using System.Reflection;

namespace BMIS
{
    public partial class frmPrintAllReports : Form
    {
        SqlConnection _sqlConnection;
        SqlCommand _sqlCommand;
        SqlDataReader _sqlDataReader;
        public string _refno;
        public string _captain;
        public string _Address;
        public string _BC;
        public string _Name;
        public string _Purpose;
        
        public frmPrintAllReports()
        {
            InitializeComponent();
            _sqlConnection = new SqlConnection(vars.DbString);
        }
        public void GetKapitan()
        {
            _sqlConnection.Open();
            _sqlCommand = new SqlCommand("SELECT *FROM tblOfficial WHERE position LIKE 'KAPITAN'", _sqlConnection);
            _sqlDataReader = _sqlCommand.ExecuteReader();
            _sqlDataReader.Read();
            if (_sqlDataReader.HasRows)
            {
                _captain = _sqlDataReader["name"].ToString();
            }
            _sqlDataReader.Close();
            _sqlConnection.Close();
        }
        public void GenerateBlotterReport(string sql)
        {
            try
            {
                GetKapitan();
                ReportDataSource reportDataSource;
                //reportViewer1.LocalReport.ReportPath = ".\\rptBlotter.rdlc";
                reportViewer1.LocalReport.ReportPath = ".\\Report1.rdlc";
                reportViewer1.LocalReport.DataSources.Clear();

                DataSet dataSet = new DataSet();
                SqlDataAdapter dataAdapter = new SqlDataAdapter();

                _sqlConnection.Open();
                dataAdapter.SelectCommand = new SqlCommand(sql, _sqlConnection);
                dataAdapter.Fill(dataSet.Tables["tblBlotter"]);

                //ReportParameter pKapitan = new ReportParameter("pKapitan", _captain);
                //reportViewer1.LocalReport.SetParameters(pKapitan);

                reportDataSource = new ReportDataSource("DataSetForBlotter", dataSet.Tables["tblBlotter"]);
                reportViewer1.LocalReport.DataSources.Add(reportDataSource);
                reportViewer1.SetDisplayMode(DisplayMode.PrintLayout);
                reportViewer1.ZoomMode = ZoomMode.Percent;
                reportViewer1.ZoomPercent = 100;
                _sqlConnection.Close();
            }
            catch (Exception ex)
            {
                _sqlConnection.Close();
                MessageBox.Show(ex.Message, vars._title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        public void GenerateBusinessReport(string sql)
        {
            try
            {
                GetKapitan();
                ReportDataSource reportDataSource;
                reportViewer1.LocalReport.ReportPath = ".\\rptBusiness.rdlc";
                reportViewer1.LocalReport.DataSources.Clear();

                DataSet dataSet = new DataSet();
                SqlDataAdapter dataAdapter = new SqlDataAdapter();

                _sqlConnection.Open();
                dataAdapter.SelectCommand = new SqlCommand(sql, _sqlConnection);
                dataAdapter.Fill(dataSet.Tables["tblBusiness"]);

                ReportParameter pKapitan = new ReportParameter("pKapitan", _captain);
                reportViewer1.LocalReport.SetParameters(pKapitan);

                reportDataSource = new ReportDataSource("DataSetForBusiness", dataSet.Tables["tblBusiness"]);
                reportViewer1.LocalReport.DataSources.Add(reportDataSource);
                reportViewer1.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
                reportViewer1.ZoomMode = ZoomMode.Percent;
                reportViewer1.ZoomPercent = 100;
                _sqlConnection.Close();
            }
            catch (Exception ex)
            {
                _sqlConnection.Close();
                MessageBox.Show(ex.Message, vars._title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        public void GenerateClearanceReport(string _Address, string _Name, string _Purpose, string _BC, string _refno)
        {
            try
            {
                GetKapitan();
                ReportDataSource reportDataSource;
                reportViewer1.LocalReport.ReportPath = ".\\rptClearance.rdlc";
                reportViewer1.LocalReport.DataSources.Clear();

                DataSet dataSet = new DataSet();
                SqlDataAdapter dataAdapter = new SqlDataAdapter();

                _sqlConnection.Open();
                dataAdapter.SelectCommand = new SqlCommand("Select *from tblDocument where refno = '" + _refno + "'", _sqlConnection);
                dataAdapter.Fill(dataSet.Tables["tblClearance"]);
                
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

                reportDataSource = new ReportDataSource("DataSetForClearance", dataSet.Tables["tblClearance"]);
                reportViewer1.LocalReport.DataSources.Add(reportDataSource);
                reportViewer1.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
                reportViewer1.ZoomMode = ZoomMode.Percent;
                reportViewer1.ZoomPercent = 100;
                _sqlConnection.Close();
            }
            catch (Exception ex)
            {
                _sqlConnection.Close();
                MessageBox.Show(ex.Message, vars._title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        public void GenerateBuildingReport(string _Name, string _Purpose, string _BC, string _refno)
        {
            try
            {
                GetKapitan();
                ReportDataSource reportDataSource;
                reportViewer1.LocalReport.ReportPath = ".\\rptBuildingPermit.rdlc";
                reportViewer1.LocalReport.DataSources.Clear();

                DataSet dataSet = new DataSet();
                SqlDataAdapter dataAdapter = new SqlDataAdapter();

                _sqlConnection.Open();
                dataAdapter.SelectCommand = new SqlCommand("Select *from tblDocument where refno = '" + _refno + "'", _sqlConnection);
                dataAdapter.Fill(dataSet.Tables["tblBuildingPermit"]);

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

                reportDataSource = new ReportDataSource("DataSetForBuildingPermit", dataSet.Tables["tblBuildingPermit"]);
                reportViewer1.LocalReport.DataSources.Add(reportDataSource);
                reportViewer1.SetDisplayMode(DisplayMode.PrintLayout);
                reportViewer1.ZoomMode = ZoomMode.Percent;
                reportViewer1.ZoomPercent = 100;
                _sqlConnection.Close();
            }
            catch (Exception ex)
            {
                _sqlConnection.Close();
                MessageBox.Show(ex.Message, vars._title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void frmPrintAllReports_Load(object sender, EventArgs e)
        {

            this.reportViewer1.RefreshReport();
        }
    }
}
