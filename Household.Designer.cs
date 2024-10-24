
namespace BMIS
{
    partial class frmHousehold
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
        [System.Obsolete]
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmHousehold));
            this.panel1 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.lbL = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.lblRecordcount = new System.Windows.Forms.Label();
            this.viewHousehold = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnSelectHousdehold = new System.Windows.Forms.DataGridViewImageColumn();
            this.panel2 = new System.Windows.Forms.Panel();
            this.txtsearchHousehold = new MetroFramework.Controls.MetroTextBox();
            this.panel1.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.viewHousehold)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(95)))), ((int)(((byte)(39)))), ((int)(((byte)(205)))));
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.lbL);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(884, 51);
            this.panel1.TabIndex = 12;
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
            this.button1.Location = new System.Drawing.Point(825, 9);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(52, 30);
            this.button1.TabIndex = 3;
            this.button1.Text = "CLOSE";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // lbL
            // 
            this.lbL.AutoSize = true;
            this.lbL.BackColor = System.Drawing.Color.Transparent;
            this.lbL.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbL.ForeColor = System.Drawing.Color.White;
            this.lbL.Location = new System.Drawing.Point(8, 14);
            this.lbL.Name = "lbL";
            this.lbL.Size = new System.Drawing.Size(235, 26);
            this.lbL.TabIndex = 2;
            this.lbL.Text = "LIST OF THE HOUSEHOLD:";
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.lblRecordcount);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel4.Location = new System.Drawing.Point(0, 553);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(884, 41);
            this.panel4.TabIndex = 13;
            // 
            // lblRecordcount
            // 
            this.lblRecordcount.AutoSize = true;
            this.lblRecordcount.BackColor = System.Drawing.Color.Transparent;
            this.lblRecordcount.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRecordcount.ForeColor = System.Drawing.Color.Black;
            this.lblRecordcount.Location = new System.Drawing.Point(12, 13);
            this.lblRecordcount.Name = "lblRecordcount";
            this.lblRecordcount.Size = new System.Drawing.Size(128, 17);
            this.lblRecordcount.TabIndex = 5;
            this.lblRecordcount.Text = "Record count(0)";
            // 
            // viewHousehold
            // 
            this.viewHousehold.AllowUserToAddRows = false;
            this.viewHousehold.AllowUserToDeleteRows = false;
            this.viewHousehold.AllowUserToResizeColumns = false;
            this.viewHousehold.AllowUserToResizeRows = false;
            this.viewHousehold.BackgroundColor = System.Drawing.Color.White;
            this.viewHousehold.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.viewHousehold.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.viewHousehold.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            this.viewHousehold.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.viewHousehold.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.viewHousehold.ColumnHeadersHeight = 25;
            this.viewHousehold.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.viewHousehold.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column8,
            this.Column3,
            this.Column2,
            this.btnSelectHousdehold});
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.LemonChiffon;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.viewHousehold.DefaultCellStyle = dataGridViewCellStyle5;
            this.viewHousehold.EnableHeadersVisualStyles = false;
            this.viewHousehold.GridColor = System.Drawing.SystemColors.Control;
            this.viewHousehold.Location = new System.Drawing.Point(0, 96);
            this.viewHousehold.MultiSelect = false;
            this.viewHousehold.Name = "viewHousehold";
            this.viewHousehold.ReadOnly = true;
            this.viewHousehold.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.viewHousehold.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.viewHousehold.RowHeadersVisible = false;
            this.viewHousehold.RowHeadersWidth = 60;
            this.viewHousehold.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.viewHousehold.RowTemplate.Height = 30;
            this.viewHousehold.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.viewHousehold.Size = new System.Drawing.Size(884, 457);
            this.viewHousehold.TabIndex = 15;
            this.viewHousehold.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.viewHousehold_CellContentClick);
            // 
            // Column1
            // 
            this.Column1.HeaderText = "ID";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Visible = false;
            // 
            // Column8
            // 
            this.Column8.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Column8.HeaderText = "HOUSE NO.";
            this.Column8.MinimumWidth = 10;
            this.Column8.Name = "Column8";
            this.Column8.ReadOnly = true;
            this.Column8.Width = 90;
            // 
            // Column3
            // 
            this.Column3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Column3.HeaderText = "NAME";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.Width = 62;
            // 
            // Column2
            // 
            this.Column2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column2.HeaderText = "ADDRESS";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            // 
            // btnSelectHousdehold
            // 
            this.btnSelectHousdehold.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.btnSelectHousdehold.HeaderText = "";
            this.btnSelectHousdehold.Image = ((System.Drawing.Image)(resources.GetObject("btnSelectHousdehold.Image")));
            this.btnSelectHousdehold.Name = "btnSelectHousdehold";
            this.btnSelectHousdehold.ReadOnly = true;
            this.btnSelectHousdehold.Width = 5;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.txtsearchHousehold);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 51);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(884, 45);
            this.panel2.TabIndex = 16;
            // 
            // txtsearchHousehold
            // 
            this.txtsearchHousehold.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtsearchHousehold.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            // 
            // 
            // 
            this.txtsearchHousehold.CustomButton.Image = null;
            this.txtsearchHousehold.CustomButton.Location = new System.Drawing.Point(236, 2);
            this.txtsearchHousehold.CustomButton.Name = "";
            this.txtsearchHousehold.CustomButton.Size = new System.Drawing.Size(31, 31);
            this.txtsearchHousehold.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtsearchHousehold.CustomButton.TabIndex = 1;
            this.txtsearchHousehold.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtsearchHousehold.CustomButton.UseSelectable = true;
            this.txtsearchHousehold.CustomButton.Visible = false;
            this.txtsearchHousehold.DisplayIcon = true;
            this.txtsearchHousehold.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.txtsearchHousehold.Icon = ((System.Drawing.Image)(resources.GetObject("txtsearchHousehold.Icon")));
            this.txtsearchHousehold.Lines = new string[0];
            this.txtsearchHousehold.Location = new System.Drawing.Point(3, 3);
            this.txtsearchHousehold.MaxLength = 32767;
            this.txtsearchHousehold.Multiline = true;
            this.txtsearchHousehold.Name = "txtsearchHousehold";
            this.txtsearchHousehold.PasswordChar = '\0';
            this.txtsearchHousehold.PromptText = "Search";
            this.txtsearchHousehold.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtsearchHousehold.SelectedText = "";
            this.txtsearchHousehold.SelectionLength = 0;
            this.txtsearchHousehold.SelectionStart = 0;
            this.txtsearchHousehold.ShortcutsEnabled = true;
            this.txtsearchHousehold.Size = new System.Drawing.Size(270, 36);
            this.txtsearchHousehold.TabIndex = 11;
            this.txtsearchHousehold.UseSelectable = true;
            this.txtsearchHousehold.WaterMark = "Search";
            this.txtsearchHousehold.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtsearchHousehold.WaterMarkFont = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtsearchHousehold.TextChanged += new System.EventHandler(this.txtsearchHousehold_TextChanged);
            // 
            // frmHousehold
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(884, 594);
            this.ControlBox = false;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.viewHousehold);
            this.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "frmHousehold";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.viewHousehold)).EndInit();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label lbL;
        private System.Windows.Forms.Panel panel4;
        public System.Windows.Forms.Label lblRecordcount;
        private System.Windows.Forms.DataGridView viewHousehold;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewImageColumn btnSelectHousdehold;
        private System.Windows.Forms.Panel panel2;
        public MetroFramework.Controls.MetroTextBox txtsearchHousehold;
    }
}