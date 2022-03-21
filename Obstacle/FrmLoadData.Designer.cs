namespace Obstacle
{
    partial class FrmLoadData
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
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.exportToExcelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editParametersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FlatFunnel = new System.Windows.Forms.TextBox();
            this.OEdge = new System.Windows.Forms.TextBox();
            this.RotorDia = new System.Windows.Forms.TextBox();
            this.Diversion = new System.Windows.Forms.TextBox();
            this.Safety = new System.Windows.Forms.TextBox();
            this.App1East = new System.Windows.Forms.TextBox();
            this.App2East = new System.Windows.Forms.TextBox();
            this.App1North = new System.Windows.Forms.TextBox();
            this.HRPElevation = new System.Windows.Forms.TextBox();
            this.ReverseBearing = new System.Windows.Forms.TextBox();
            this.H_Northing = new System.Windows.Forms.TextBox();
            this.App2North = new System.Windows.Forms.TextBox();
            this.H_Easting = new System.Windows.Forms.TextBox();
            this.Bearing = new System.Windows.Forms.TextBox();
            this.MaxId = new System.Windows.Forms.TextBox();
            this.Tolf = new System.Windows.Forms.TextBox();
            this.Fato = new System.Windows.Forms.TextBox();
            this.SelectedID = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 28);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(4);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(1324, 628);
            this.dataGridView1.TabIndex = 66;
            this.dataGridView1.Visible = false;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            this.dataGridView1.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dataGridView1_DataBindingComplete);
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exportToExcelToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.editParametersToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1324, 28);
            this.menuStrip1.TabIndex = 68;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // exportToExcelToolStripMenuItem
            // 
            this.exportToExcelToolStripMenuItem.Name = "exportToExcelToolStripMenuItem";
            this.exportToExcelToolStripMenuItem.Size = new System.Drawing.Size(124, 24);
            this.exportToExcelToolStripMenuItem.Text = "Export To Excel";
            this.exportToExcelToolStripMenuItem.Click += new System.EventHandler(this.exportToExcelToolStripMenuItem_Click);
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(74, 24);
            this.saveAsToolStripMenuItem.Text = "Save As";
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
            // 
            // editParametersToolStripMenuItem
            // 
            this.editParametersToolStripMenuItem.Name = "editParametersToolStripMenuItem";
            this.editParametersToolStripMenuItem.Size = new System.Drawing.Size(126, 24);
            this.editParametersToolStripMenuItem.Text = "Edit Parameters";
            this.editParametersToolStripMenuItem.Visible = false;
            this.editParametersToolStripMenuItem.Click += new System.EventHandler(this.editParametersToolStripMenuItem_Click);
            // 
            // FlatFunnel
            // 
            this.FlatFunnel.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FlatFunnel.Location = new System.Drawing.Point(569, 418);
            this.FlatFunnel.Margin = new System.Windows.Forms.Padding(4);
            this.FlatFunnel.Name = "FlatFunnel";
            this.FlatFunnel.Size = new System.Drawing.Size(84, 26);
            this.FlatFunnel.TabIndex = 82;
            this.FlatFunnel.Visible = false;
            // 
            // OEdge
            // 
            this.OEdge.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OEdge.Location = new System.Drawing.Point(569, 389);
            this.OEdge.Margin = new System.Windows.Forms.Padding(4);
            this.OEdge.Name = "OEdge";
            this.OEdge.Size = new System.Drawing.Size(84, 26);
            this.OEdge.TabIndex = 77;
            this.OEdge.Visible = false;
            // 
            // RotorDia
            // 
            this.RotorDia.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RotorDia.Location = new System.Drawing.Point(569, 360);
            this.RotorDia.Margin = new System.Windows.Forms.Padding(4);
            this.RotorDia.Name = "RotorDia";
            this.RotorDia.Size = new System.Drawing.Size(84, 26);
            this.RotorDia.TabIndex = 76;
            this.RotorDia.Visible = false;
            // 
            // Diversion
            // 
            this.Diversion.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Diversion.Location = new System.Drawing.Point(569, 331);
            this.Diversion.Margin = new System.Windows.Forms.Padding(4);
            this.Diversion.Name = "Diversion";
            this.Diversion.Size = new System.Drawing.Size(84, 26);
            this.Diversion.TabIndex = 75;
            this.Diversion.Visible = false;
            // 
            // Safety
            // 
            this.Safety.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Safety.Location = new System.Drawing.Point(569, 302);
            this.Safety.Margin = new System.Windows.Forms.Padding(4);
            this.Safety.Name = "Safety";
            this.Safety.Size = new System.Drawing.Size(84, 26);
            this.Safety.TabIndex = 73;
            this.Safety.Visible = false;
            // 
            // App1East
            // 
            this.App1East.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.App1East.Location = new System.Drawing.Point(660, 250);
            this.App1East.Margin = new System.Windows.Forms.Padding(4);
            this.App1East.Name = "App1East";
            this.App1East.Size = new System.Drawing.Size(97, 26);
            this.App1East.TabIndex = 79;
            this.App1East.Visible = false;
            // 
            // App2East
            // 
            this.App2East.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.App2East.Location = new System.Drawing.Point(660, 274);
            this.App2East.Margin = new System.Windows.Forms.Padding(4);
            this.App2East.Name = "App2East";
            this.App2East.Size = new System.Drawing.Size(97, 26);
            this.App2East.TabIndex = 81;
            this.App2East.Visible = false;
            // 
            // App1North
            // 
            this.App1North.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.App1North.Location = new System.Drawing.Point(568, 250);
            this.App1North.Margin = new System.Windows.Forms.Padding(4);
            this.App1North.Name = "App1North";
            this.App1North.Size = new System.Drawing.Size(84, 26);
            this.App1North.TabIndex = 78;
            this.App1North.Visible = false;
            // 
            // HRPElevation
            // 
            this.HRPElevation.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.HRPElevation.Location = new System.Drawing.Point(569, 448);
            this.HRPElevation.Margin = new System.Windows.Forms.Padding(4);
            this.HRPElevation.Name = "HRPElevation";
            this.HRPElevation.Size = new System.Drawing.Size(84, 26);
            this.HRPElevation.TabIndex = 74;
            this.HRPElevation.Visible = false;
            // 
            // ReverseBearing
            // 
            this.ReverseBearing.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ReverseBearing.Location = new System.Drawing.Point(660, 216);
            this.ReverseBearing.Margin = new System.Windows.Forms.Padding(4);
            this.ReverseBearing.MaxLength = 6;
            this.ReverseBearing.Name = "ReverseBearing";
            this.ReverseBearing.Size = new System.Drawing.Size(84, 26);
            this.ReverseBearing.TabIndex = 72;
            this.ReverseBearing.Visible = false;
            // 
            // H_Northing
            // 
            this.H_Northing.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.H_Northing.Location = new System.Drawing.Point(567, 183);
            this.H_Northing.Margin = new System.Windows.Forms.Padding(4);
            this.H_Northing.Name = "H_Northing";
            this.H_Northing.Size = new System.Drawing.Size(84, 26);
            this.H_Northing.TabIndex = 69;
            this.H_Northing.Visible = false;
            // 
            // App2North
            // 
            this.App2North.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.App2North.Location = new System.Drawing.Point(568, 273);
            this.App2North.Margin = new System.Windows.Forms.Padding(4);
            this.App2North.Name = "App2North";
            this.App2North.Size = new System.Drawing.Size(84, 26);
            this.App2North.TabIndex = 80;
            this.App2North.Visible = false;
            // 
            // H_Easting
            // 
            this.H_Easting.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.H_Easting.Location = new System.Drawing.Point(660, 187);
            this.H_Easting.Margin = new System.Windows.Forms.Padding(4);
            this.H_Easting.Name = "H_Easting";
            this.H_Easting.Size = new System.Drawing.Size(97, 26);
            this.H_Easting.TabIndex = 70;
            this.H_Easting.Visible = false;
            // 
            // Bearing
            // 
            this.Bearing.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Bearing.Location = new System.Drawing.Point(568, 216);
            this.Bearing.Margin = new System.Windows.Forms.Padding(4);
            this.Bearing.MaxLength = 6;
            this.Bearing.Name = "Bearing";
            this.Bearing.Size = new System.Drawing.Size(84, 26);
            this.Bearing.TabIndex = 71;
            this.Bearing.Visible = false;
            // 
            // MaxId
            // 
            this.MaxId.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MaxId.Location = new System.Drawing.Point(661, 318);
            this.MaxId.Margin = new System.Windows.Forms.Padding(4);
            this.MaxId.Name = "MaxId";
            this.MaxId.Size = new System.Drawing.Size(84, 26);
            this.MaxId.TabIndex = 83;
            this.MaxId.Visible = false;
            // 
            // Tolf
            // 
            this.Tolf.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Tolf.Location = new System.Drawing.Point(661, 383);
            this.Tolf.Margin = new System.Windows.Forms.Padding(4);
            this.Tolf.Name = "Tolf";
            this.Tolf.Size = new System.Drawing.Size(84, 26);
            this.Tolf.TabIndex = 84;
            this.Tolf.Visible = false;
            // 
            // Fato
            // 
            this.Fato.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Fato.Location = new System.Drawing.Point(660, 349);
            this.Fato.Margin = new System.Windows.Forms.Padding(4);
            this.Fato.Name = "Fato";
            this.Fato.Size = new System.Drawing.Size(84, 26);
            this.Fato.TabIndex = 85;
            this.Fato.Visible = false;
            // 
            // SelectedID
            // 
            this.SelectedID.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SelectedID.Location = new System.Drawing.Point(660, 417);
            this.SelectedID.Margin = new System.Windows.Forms.Padding(4);
            this.SelectedID.Name = "SelectedID";
            this.SelectedID.Size = new System.Drawing.Size(84, 26);
            this.SelectedID.TabIndex = 86;
            this.SelectedID.Visible = false;
            // 
            // FrmLoadData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1324, 656);
            this.Controls.Add(this.SelectedID);
            this.Controls.Add(this.Fato);
            this.Controls.Add(this.Tolf);
            this.Controls.Add(this.MaxId);
            this.Controls.Add(this.FlatFunnel);
            this.Controls.Add(this.OEdge);
            this.Controls.Add(this.RotorDia);
            this.Controls.Add(this.Diversion);
            this.Controls.Add(this.Safety);
            this.Controls.Add(this.App1East);
            this.Controls.Add(this.App2East);
            this.Controls.Add(this.App1North);
            this.Controls.Add(this.HRPElevation);
            this.Controls.Add(this.ReverseBearing);
            this.Controls.Add(this.H_Northing);
            this.Controls.Add(this.App2North);
            this.Controls.Add(this.H_Easting);
            this.Controls.Add(this.Bearing);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FrmLoadData";
            this.Text = "FrmLoadData";
            this.Load += new System.EventHandler(this.FrmLoadData_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem exportToExcelToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.TextBox FlatFunnel;
        private System.Windows.Forms.TextBox OEdge;
        private System.Windows.Forms.TextBox RotorDia;
        private System.Windows.Forms.TextBox Diversion;
        private System.Windows.Forms.TextBox Safety;
        private System.Windows.Forms.TextBox App1East;
        private System.Windows.Forms.TextBox App2East;
        private System.Windows.Forms.TextBox App1North;
        private System.Windows.Forms.TextBox HRPElevation;
        private System.Windows.Forms.TextBox ReverseBearing;
        private System.Windows.Forms.TextBox H_Northing;
        private System.Windows.Forms.TextBox App2North;
        private System.Windows.Forms.TextBox H_Easting;
        private System.Windows.Forms.TextBox Bearing;
        private System.Windows.Forms.TextBox MaxId;
        private System.Windows.Forms.TextBox Tolf;
        private System.Windows.Forms.TextBox Fato;
        private System.Windows.Forms.ToolStripMenuItem editParametersToolStripMenuItem;
        private System.Windows.Forms.TextBox SelectedID;
    }
}