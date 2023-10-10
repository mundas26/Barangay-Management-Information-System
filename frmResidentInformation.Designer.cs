
using System;
using System.ComponentModel;

namespace BMIS
{
    partial class frmResidentInformation
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        [Obsolete]
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmResidentInformation));
            this.picImage = new System.Windows.Forms.PictureBox();
            this.label24 = new System.Windows.Forms.Label();
            this.cboStatus = new System.Windows.Forms.ComboBox();
            this.label23 = new System.Windows.Forms.Label();
            this.cboPersonwithAbility = new System.Windows.Forms.ComboBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.txtHeadofthefamily = new System.Windows.Forms.TextBox();
            this.label22 = new System.Windows.Forms.Label();
            this.txtHouse = new System.Windows.Forms.TextBox();
            this.label21 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.cboCategory = new System.Windows.Forms.ComboBox();
            this.txtAddress = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.txtOccupation = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.txtEduc = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.cboPurok = new System.Windows.Forms.ComboBox();
            this.txtPrecint = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.cboVoters = new System.Windows.Forms.ComboBox();
            this.txtContact = new System.Windows.Forms.TextBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.lbL = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.txtReligion = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.cboGender = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.cboCivilstatus = new System.Windows.Forms.ComboBox();
            this.txtAge = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtBplace = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.dtBdate = new System.Windows.Forms.DateTimePicker();
            this.txtAllias = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtMname = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtFname = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtLname = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtID = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.lblEmail = new System.Windows.Forms.Label();
            this.btnBrowser = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.picImage)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnBrowser)).BeginInit();
            this.SuspendLayout();
            // 
            // picImage
            // 
            this.picImage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.picImage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picImage.ErrorImage = null;
            this.picImage.InitialImage = null;
            this.picImage.Location = new System.Drawing.Point(45, 118);
            this.picImage.Name = "picImage";
            this.picImage.Size = new System.Drawing.Size(236, 212);
            this.picImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.picImage.TabIndex = 115;
            this.picImage.TabStop = false;
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label24.Location = new System.Drawing.Point(681, 669);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(56, 18);
            this.label24.TabIndex = 113;
            this.label24.Text = "STATUS:";
            // 
            // cboStatus
            // 
            this.cboStatus.BackColor = System.Drawing.Color.White;
            this.cboStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboStatus.FormattingEnabled = true;
            this.cboStatus.Items.AddRange(new object[] {
            "ALIVE",
            "DECEASED"});
            this.cboStatus.Location = new System.Drawing.Point(682, 690);
            this.cboStatus.Name = "cboStatus";
            this.cboStatus.Size = new System.Drawing.Size(317, 23);
            this.cboStatus.TabIndex = 23;
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label23.Location = new System.Drawing.Point(681, 622);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(145, 18);
            this.label23.TabIndex = 111;
            this.label23.Text = "PERSON WITH ABILITY:";
            // 
            // cboPersonwithAbility
            // 
            this.cboPersonwithAbility.BackColor = System.Drawing.Color.White;
            this.cboPersonwithAbility.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPersonwithAbility.FormattingEnabled = true;
            this.cboPersonwithAbility.Items.AddRange(new object[] {
            "YES",
            "NO"});
            this.cboPersonwithAbility.Location = new System.Drawing.Point(682, 643);
            this.cboPersonwithAbility.Name = "cboPersonwithAbility";
            this.cboPersonwithAbility.Size = new System.Drawing.Size(317, 23);
            this.cboPersonwithAbility.TabIndex = 22;
            // 
            // btnBrowse
            // 
            this.btnBrowse.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(184)))), ((int)(((byte)(148)))));
            this.btnBrowse.FlatAppearance.BorderSize = 0;
            this.btnBrowse.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrowse.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBrowse.ForeColor = System.Drawing.Color.White;
            this.btnBrowse.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnBrowse.Location = new System.Drawing.Point(45, 336);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(236, 32);
            this.btnBrowse.TabIndex = 98;
            this.btnBrowse.Text = " Browse a picture from drive";
            this.btnBrowse.UseVisualStyleBackColor = false;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(112)))), ((int)(((byte)(85)))));
            this.btnCancel.FlatAppearance.BorderSize = 0;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancel.Location = new System.Drawing.Point(926, 719);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(73, 28);
            this.btnCancel.TabIndex = 26;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(184)))), ((int)(((byte)(148)))));
            this.btnUpdate.FlatAppearance.BorderSize = 0;
            this.btnUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUpdate.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUpdate.ForeColor = System.Drawing.Color.White;
            this.btnUpdate.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnUpdate.Location = new System.Drawing.Point(848, 719);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(73, 28);
            this.btnUpdate.TabIndex = 25;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.UseVisualStyleBackColor = false;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(184)))), ((int)(((byte)(148)))));
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSave.Location = new System.Drawing.Point(769, 719);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(73, 28);
            this.btnSave.TabIndex = 24;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // txtHeadofthefamily
            // 
            this.txtHeadofthefamily.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtHeadofthefamily.Location = new System.Drawing.Point(682, 596);
            this.txtHeadofthefamily.Name = "txtHeadofthefamily";
            this.txtHeadofthefamily.Size = new System.Drawing.Size(317, 23);
            this.txtHeadofthefamily.TabIndex = 21;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label22.Location = new System.Drawing.Point(679, 575);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(137, 18);
            this.label22.TabIndex = 109;
            this.label22.Text = "HEAD OF THE FAMILY:";
            // 
            // txtHouse
            // 
            this.txtHouse.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtHouse.Enabled = false;
            this.txtHouse.Location = new System.Drawing.Point(682, 549);
            this.txtHouse.Name = "txtHouse";
            this.txtHouse.Size = new System.Drawing.Size(317, 23);
            this.txtHouse.TabIndex = 20;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.Location = new System.Drawing.Point(679, 528);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(81, 18);
            this.label21.TabIndex = 108;
            this.label21.Text = "HOUSE NO.:";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.Location = new System.Drawing.Point(681, 481);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(75, 18);
            this.label20.TabIndex = 107;
            this.label20.Text = "CATEGORY:";
            // 
            // cboCategory
            // 
            this.cboCategory.BackColor = System.Drawing.Color.White;
            this.cboCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCategory.FormattingEnabled = true;
            this.cboCategory.Items.AddRange(new object[] {
            "HEAD OF THE FAMILY",
            "MEMBER"});
            this.cboCategory.Location = new System.Drawing.Point(682, 502);
            this.cboCategory.Name = "cboCategory";
            this.cboCategory.Size = new System.Drawing.Size(317, 23);
            this.cboCategory.TabIndex = 19;
            this.cboCategory.SelectedIndexChanged += new System.EventHandler(this.cboCategory_SelectedIndexChanged);
            // 
            // txtAddress
            // 
            this.txtAddress.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtAddress.Location = new System.Drawing.Point(682, 400);
            this.txtAddress.Multiline = true;
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Size = new System.Drawing.Size(317, 78);
            this.txtAddress.TabIndex = 18;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.Location = new System.Drawing.Point(682, 379);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(68, 18);
            this.label19.TabIndex = 106;
            this.label19.Text = "ADDRESS:";
            // 
            // txtOccupation
            // 
            this.txtOccupation.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtOccupation.Location = new System.Drawing.Point(682, 356);
            this.txtOccupation.Name = "txtOccupation";
            this.txtOccupation.Size = new System.Drawing.Size(317, 23);
            this.txtOccupation.TabIndex = 17;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.Location = new System.Drawing.Point(679, 335);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(93, 18);
            this.label18.TabIndex = 105;
            this.label18.Text = "OCCUPATION:";
            // 
            // txtEduc
            // 
            this.txtEduc.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtEduc.Location = new System.Drawing.Point(682, 306);
            this.txtEduc.Name = "txtEduc";
            this.txtEduc.Size = new System.Drawing.Size(317, 23);
            this.txtEduc.TabIndex = 16;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Location = new System.Drawing.Point(679, 285);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(166, 18);
            this.label17.TabIndex = 104;
            this.label17.Text = "EDUCATION ATTAINMENT:";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(681, 238);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(55, 18);
            this.label16.TabIndex = 103;
            this.label16.Text = "PUROK:";
            // 
            // cboPurok
            // 
            this.cboPurok.BackColor = System.Drawing.Color.White;
            this.cboPurok.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPurok.FormattingEnabled = true;
            this.cboPurok.Location = new System.Drawing.Point(682, 259);
            this.cboPurok.Name = "cboPurok";
            this.cboPurok.Size = new System.Drawing.Size(317, 23);
            this.cboPurok.TabIndex = 15;
            // 
            // txtPrecint
            // 
            this.txtPrecint.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtPrecint.Enabled = false;
            this.txtPrecint.Location = new System.Drawing.Point(682, 212);
            this.txtPrecint.Name = "txtPrecint";
            this.txtPrecint.Size = new System.Drawing.Size(317, 23);
            this.txtPrecint.TabIndex = 14;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(679, 191);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(91, 18);
            this.label15.TabIndex = 102;
            this.label15.Text = "PRECINT NO.:";
            // 
            // cboVoters
            // 
            this.cboVoters.BackColor = System.Drawing.Color.White;
            this.cboVoters.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboVoters.FormattingEnabled = true;
            this.cboVoters.Items.AddRange(new object[] {
            "YES",
            "NO"});
            this.cboVoters.Location = new System.Drawing.Point(682, 165);
            this.cboVoters.Name = "cboVoters";
            this.cboVoters.Size = new System.Drawing.Size(317, 23);
            this.cboVoters.TabIndex = 13;
            this.cboVoters.SelectedIndexChanged += new System.EventHandler(this.cboVoters_SelectedIndexChanged);
            // 
            // txtContact
            // 
            this.txtContact.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtContact.Location = new System.Drawing.Point(682, 118);
            this.txtContact.Name = "txtContact";
            this.txtContact.Size = new System.Drawing.Size(317, 23);
            this.txtContact.TabIndex = 12;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(681, 144);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(110, 18);
            this.label14.TabIndex = 101;
            this.label14.Text = "VOTER\'S STATUS:";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(679, 97);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(69, 18);
            this.label13.TabIndex = 100;
            this.label13.Text = "CONTACT:";
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(184)))), ((int)(((byte)(148)))));
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.Location = new System.Drawing.Point(987, 9);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(52, 30);
            this.button1.TabIndex = 3;
            this.button1.Text = "CLOSE";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtEmail
            // 
            this.txtEmail.Location = new System.Drawing.Point(340, 690);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(317, 23);
            this.txtEmail.TabIndex = 11;
            this.txtEmail.Validated += new System.EventHandler(this.txtEmail_Validated);
            // 
            // lbL
            // 
            this.lbL.AutoSize = true;
            this.lbL.BackColor = System.Drawing.Color.Transparent;
            this.lbL.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbL.ForeColor = System.Drawing.Color.White;
            this.lbL.Location = new System.Drawing.Point(12, 11);
            this.lbL.Name = "lbL";
            this.lbL.Size = new System.Drawing.Size(237, 26);
            this.lbL.TabIndex = 2;
            this.lbL.Text = "RESIDENT INFORMATION:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(338, 669);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(50, 18);
            this.label12.TabIndex = 99;
            this.label12.Text = "EMAIL:";
            // 
            // txtReligion
            // 
            this.txtReligion.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtReligion.Location = new System.Drawing.Point(340, 643);
            this.txtReligion.Name = "txtReligion";
            this.txtReligion.Size = new System.Drawing.Size(317, 23);
            this.txtReligion.TabIndex = 10;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(339, 622);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(70, 18);
            this.label11.TabIndex = 96;
            this.label11.Text = "RELIGION:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(337, 575);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(62, 18);
            this.label10.TabIndex = 94;
            this.label10.Text = "GENDER:";
            // 
            // cboGender
            // 
            this.cboGender.BackColor = System.Drawing.Color.White;
            this.cboGender.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboGender.FormattingEnabled = true;
            this.cboGender.Items.AddRange(new object[] {
            "MALE",
            "FEMALE"});
            this.cboGender.Location = new System.Drawing.Point(340, 596);
            this.cboGender.Name = "cboGender";
            this.cboGender.Size = new System.Drawing.Size(317, 23);
            this.cboGender.TabIndex = 9;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(339, 528);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(90, 18);
            this.label9.TabIndex = 92;
            this.label9.Text = "CIVIL STATUS:";
            // 
            // cboCivilstatus
            // 
            this.cboCivilstatus.BackColor = System.Drawing.Color.White;
            this.cboCivilstatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCivilstatus.FormattingEnabled = true;
            this.cboCivilstatus.Items.AddRange(new object[] {
            "SINGLE",
            "MARRIED",
            "SEPARATED"});
            this.cboCivilstatus.Location = new System.Drawing.Point(340, 549);
            this.cboCivilstatus.Name = "cboCivilstatus";
            this.cboCivilstatus.Size = new System.Drawing.Size(317, 23);
            this.cboCivilstatus.TabIndex = 8;
            // 
            // txtAge
            // 
            this.txtAge.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtAge.Enabled = false;
            this.txtAge.Location = new System.Drawing.Point(340, 502);
            this.txtAge.Name = "txtAge";
            this.txtAge.Size = new System.Drawing.Size(317, 23);
            this.txtAge.TabIndex = 7;
            this.txtAge.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtAge_KeyPress);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(340, 481);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(37, 18);
            this.label8.TabIndex = 87;
            this.label8.Text = "AGE:";
            // 
            // txtBplace
            // 
            this.txtBplace.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtBplace.Location = new System.Drawing.Point(340, 400);
            this.txtBplace.Multiline = true;
            this.txtBplace.Name = "txtBplace";
            this.txtBplace.Size = new System.Drawing.Size(317, 78);
            this.txtBplace.TabIndex = 6;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(340, 379);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(89, 18);
            this.label7.TabIndex = 85;
            this.label7.Text = "BIRTH PLACE:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(339, 332);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(79, 18);
            this.label6.TabIndex = 82;
            this.label6.Text = "BIRTHDATE:";
            // 
            // dtBdate
            // 
            this.dtBdate.Location = new System.Drawing.Point(340, 353);
            this.dtBdate.Name = "dtBdate";
            this.dtBdate.Size = new System.Drawing.Size(317, 23);
            this.dtBdate.TabIndex = 5;
            this.dtBdate.ValueChanged += new System.EventHandler(this.dtBdate_ValueChanged);
            // 
            // txtAllias
            // 
            this.txtAllias.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtAllias.Location = new System.Drawing.Point(340, 306);
            this.txtAllias.Name = "txtAllias";
            this.txtAllias.Size = new System.Drawing.Size(317, 23);
            this.txtAllias.TabIndex = 4;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(340, 285);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 18);
            this.label5.TabIndex = 78;
            this.label5.Text = "ALLIAS:";
            // 
            // txtMname
            // 
            this.txtMname.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtMname.Location = new System.Drawing.Point(340, 259);
            this.txtMname.Name = "txtMname";
            this.txtMname.Size = new System.Drawing.Size(317, 23);
            this.txtMname.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(340, 238);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 18);
            this.label3.TabIndex = 76;
            this.label3.Text = "MIDDLE NAME:";
            // 
            // txtFname
            // 
            this.txtFname.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtFname.Location = new System.Drawing.Point(340, 212);
            this.txtFname.Name = "txtFname";
            this.txtFname.Size = new System.Drawing.Size(317, 23);
            this.txtFname.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(340, 191);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(86, 18);
            this.label4.TabIndex = 72;
            this.label4.Text = "FIRST NAME:";
            // 
            // txtLname
            // 
            this.txtLname.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtLname.Location = new System.Drawing.Point(340, 165);
            this.txtLname.Name = "txtLname";
            this.txtLname.Size = new System.Drawing.Size(317, 23);
            this.txtLname.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(339, 144);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 18);
            this.label2.TabIndex = 69;
            this.label2.Text = "LAST NAME:";
            // 
            // txtID
            // 
            this.txtID.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtID.Location = new System.Drawing.Point(340, 118);
            this.txtID.Name = "txtID";
            this.txtID.Size = new System.Drawing.Size(317, 23);
            this.txtID.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(337, 97);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 18);
            this.label1.TabIndex = 67;
            this.label1.Text = "NATIONAL ID:";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(95)))), ((int)(((byte)(39)))), ((int)(((byte)(205)))));
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.lbL);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1046, 51);
            this.panel1.TabIndex = 63;
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // lblEmail
            // 
            this.lblEmail.AutoSize = true;
            this.lblEmail.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEmail.ForeColor = System.Drawing.Color.Red;
            this.lblEmail.Location = new System.Drawing.Point(339, 716);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(173, 18);
            this.lblEmail.TabIndex = 116;
            this.lblEmail.Text = "                                                       ";
            // 
            // btnBrowser
            // 
            this.btnBrowser.Image = ((System.Drawing.Image)(resources.GetObject("btnBrowser.Image")));
            this.btnBrowser.Location = new System.Drawing.Point(1001, 550);
            this.btnBrowser.Name = "btnBrowser";
            this.btnBrowser.Size = new System.Drawing.Size(20, 19);
            this.btnBrowser.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.btnBrowser.TabIndex = 122;
            this.btnBrowser.TabStop = false;
            this.btnBrowser.Click += new System.EventHandler(this.btnBrowser_Click_1);
            // 
            // frmResidentInformation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1046, 779);
            this.ControlBox = false;
            this.Controls.Add(this.btnBrowser);
            this.Controls.Add(this.lblEmail);
            this.Controls.Add(this.picImage);
            this.Controls.Add(this.label24);
            this.Controls.Add(this.cboStatus);
            this.Controls.Add(this.label23);
            this.Controls.Add(this.cboPersonwithAbility);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.txtHeadofthefamily);
            this.Controls.Add(this.label22);
            this.Controls.Add(this.txtHouse);
            this.Controls.Add(this.label21);
            this.Controls.Add(this.label20);
            this.Controls.Add(this.cboCategory);
            this.Controls.Add(this.txtAddress);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.txtOccupation);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.txtEduc);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.cboPurok);
            this.Controls.Add(this.txtPrecint);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.cboVoters);
            this.Controls.Add(this.txtContact);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.txtReligion);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.cboGender);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.cboCivilstatus);
            this.Controls.Add(this.txtAge);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtBplace);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.dtBdate);
            this.Controls.Add(this.txtAllias);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtMname);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtFname);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtLname);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtID);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "frmResidentInformation";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            ((System.ComponentModel.ISupportInitialize)(this.picImage)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnBrowser)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        public System.Windows.Forms.PictureBox picImage;
        private System.Windows.Forms.Label label24;
        public System.Windows.Forms.ComboBox cboStatus;
        private System.Windows.Forms.Label label23;
        public System.Windows.Forms.ComboBox cboPersonwithAbility;
        public System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.Button btnCancel;
        public System.Windows.Forms.Button btnUpdate;
        public System.Windows.Forms.Button btnSave;
        public System.Windows.Forms.TextBox txtHeadofthefamily;
        private System.Windows.Forms.Label label22;
        public System.Windows.Forms.TextBox txtHouse;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label20;
        public System.Windows.Forms.ComboBox cboCategory;
        public System.Windows.Forms.TextBox txtAddress;
        private System.Windows.Forms.Label label19;
        public System.Windows.Forms.TextBox txtOccupation;
        private System.Windows.Forms.Label label18;
        public System.Windows.Forms.TextBox txtEduc;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label16;
        public System.Windows.Forms.ComboBox cboPurok;
        public System.Windows.Forms.TextBox txtPrecint;
        private System.Windows.Forms.Label label15;
        public System.Windows.Forms.ComboBox cboVoters;
        public System.Windows.Forms.TextBox txtContact;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Button button1;
        public System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Label lbL;
        private System.Windows.Forms.Label label12;
        public System.Windows.Forms.TextBox txtReligion;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        public System.Windows.Forms.ComboBox cboGender;
        private System.Windows.Forms.Label label9;
        public System.Windows.Forms.ComboBox cboCivilstatus;
        public System.Windows.Forms.TextBox txtAge;
        private System.Windows.Forms.Label label8;
        public System.Windows.Forms.TextBox txtBplace;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        public System.Windows.Forms.DateTimePicker dtBdate;
        public System.Windows.Forms.TextBox txtAllias;
        private System.Windows.Forms.Label label5;
        public System.Windows.Forms.TextBox txtMname;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.TextBox txtFname;
        private System.Windows.Forms.Label label4;
        public System.Windows.Forms.TextBox txtLname;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.TextBox txtID;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        public System.Windows.Forms.ErrorProvider errorProvider;
        public System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.PictureBox btnBrowser;
    }
}