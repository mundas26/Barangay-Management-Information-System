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
using System.IO;

namespace BMIS
{
    public partial class frmSecurity : Form
    {
        readonly SqlConnection cn;
        SqlCommand cm;
        SqlDataReader dr;
        public string _username = "", _password = "", _name = "", _role = "", _pic; 
        int counter = 0;
        public string[] imagePaths;
        public bool slideshowRunning = true;
        public string DbString = @"Data Source = MUNDAS26\SQLEXPRESS; Initial Catalog = bmis; Integrated Security = True";

        [Obsolete]
        public frmSecurity()
        {
            InitializeComponent();
            cn = new SqlConnection(DbString);
            ImagesSlideShow();
        }
        
        public void ImagesSlideShow()
        {
            string folderPaths = @".\OfficialPostImages";
            string[] Files = Directory.GetFiles(folderPaths, "*.jpg").Concat(Directory.GetFiles(folderPaths, "*.png")).ToArray();
            imagePaths = Files;
            Image image = Image.FromFile(imagePaths[counter]);
            picBrgyOfficialPost.Image = image;
            timer1.Interval = 2000;
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            Image image;
            if (slideshowRunning==true)
            {
                counter++;
                if (counter >= imagePaths.Length)
                {
                    counter = 0;
                }
                image = Image.FromFile(imagePaths[counter]);
                picBrgyOfficialPost.Image = image;
            }
            else
            {
                picBrgyOfficialPost.Image = null;
            }
        }

        private void frmSecurity_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to close this Application?", vars._title, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        [Obsolete]
        private void btnLogin_Click_1(object sender, EventArgs e)
        {
            try
            {
                bool Found = false;
                cn.Open();

                cm = new SqlCommand("Select *from tblOfficial where username=@username and password=@password", cn);
                cm.Parameters.AddWithValue("@username", txtUser.Text);
                cm.Parameters.AddWithValue("@password", txtPass.Text);
                dr = cm.ExecuteReader();
                if (dr.Read())
                {
                    Found = true;
                    slideshowRunning = false;
                    _username = dr["username"].ToString();
                    _role = dr["position"].ToString();
                    _name = dr["name"].ToString();
                    _pic = dr["idPic"].ToString();
                }
                else
                {
                    Found = false;
                }

                dr.Close();
                cn.Close();

                if (Found)
                {
                    string[] ColleagesUser = { "KAGAWAD 1", "KAGAWAD 2", "KAGAWAD 3", "KAGAWAD 4", "KAGAWAD 5", "KAGAWAD 6", "KAGAWAD 7", "SK CHAIRMAN", "SECRETARY", "TREASURER" };
                    string[] kapitanUser = { "KAPITAN" };

                    if (kapitanUser.Contains(_role))
                    {
                        // Form for kapitanUser
                        slideshowRunning = false;
                        MessageBox.Show("Welcome " + _role + "!", "ACCESS GRANTED", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        Main f2 = new Main(this);
                        f2.lblName.Text = _name;
                        f2.lblPosition.Text = _role;
                        f2.picKapitan.Image = Image.FromFile(_pic);
                        vars.Users = _role;

                        f2.Show();
                        this.Hide();
                    }
                    else
                    {
                        // Form for ColleagesUser
                        slideshowRunning = false;
                        MessageBox.Show("Welcome " + _role + "!", "ACCESS GRANTED", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        MainBrgy f1 = new MainBrgy(this);
                        f1.lblName.Text = _name;
                        f1.lblPosition.Text = _role;
                        f1.picMainBrgy.Image = Image.FromFile(_pic);
                        vars.Users = _role;

                        f1.Show();
                        this.Hide();
                    }
                }
                else
                {
                    // Form for ADMINISTRATOR
                    if (txtUser.Text == vars._defaultUser && txtPass.Text == vars._defaultPass)
                    {
                        slideshowRunning = false;
                        MessageBox.Show("Welcome " + vars._defaultRole + "!", "ACCESS GRANTED", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //frmLoading f = new frmLoading();
                        Main f = new Main(this);
                        f.lblName.Text = vars._defaultName;
                        f.lblPosition.Text = vars._defaultRole;
                        f.picMain.Image = vars._defaultImage;
                        vars.Users = vars._defaultRole;
                        f.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Invalid username or password!", "ACCESS DENIED", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtUser.Clear();
                        txtPass.Clear();
                        txtUser.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                cn.Close();
                MessageBox.Show(ex.Message, vars._title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void btnSignup_Click(object sender, EventArgs e)
        {
            frmAccount f = new frmAccount(this);
            f.Clear();
            f.ShowDialog();
        }
    }
}
