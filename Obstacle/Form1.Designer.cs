namespace Obstacle
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.button1 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button3 = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportToExcelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveACopyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ImportButton = new System.Windows.Forms.ToolStripMenuItem();
            this.CalCulateButton = new System.Windows.Forms.ToolStripMenuItem();
            this.FileName = new System.Windows.Forms.ToolStripTextBox();
            this.location = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.FlatFunnel = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.OEdge = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.RotorDia = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.Diversion = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.Safety = new System.Windows.Forms.TextBox();
            this.App1East = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.App2East = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.App1North = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.HRPElevation = new System.Windows.Forms.TextBox();
            this.ReverseBearing = new System.Windows.Forms.TextBox();
            this.H_Northing = new System.Windows.Forms.TextBox();
            this.App2North = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.H_Easting = new System.Windows.Forms.TextBox();
            this.Bearing = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Fato = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.TOLF = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.SelectedID = new System.Windows.Forms.TextBox();
            this.button4 = new System.Windows.Forms.Button();
            this.Head = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(440, 548);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(241, 26);
            this.button1.TabIndex = 28;
            this.button1.Text = "Fill Data";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(465, 563);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(10, 28);
            this.textBox1.TabIndex = 31;
            this.textBox1.Visible = false;
            // 
            // button3
            // 
            this.button3.Font = new System.Drawing.Font("Segoe UI Symbol", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button3.Location = new System.Drawing.Point(440, 580);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(241, 32);
            this.button3.TabIndex = 30;
            this.button3.Text = "Get Funnel Coordinates";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click_2);
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.ImportButton,
            this.CalCulateButton,
            this.FileName});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(709, 31);
            this.menuStrip1.TabIndex = 26;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exportToExcelToolStripMenuItem,
            this.saveACopyToolStripMenuItem,
            this.loadDataToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(46, 27);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // exportToExcelToolStripMenuItem
            // 
            this.exportToExcelToolStripMenuItem.Name = "exportToExcelToolStripMenuItem";
            this.exportToExcelToolStripMenuItem.Size = new System.Drawing.Size(191, 26);
            this.exportToExcelToolStripMenuItem.Text = "Export to Excel";
            // 
            // saveACopyToolStripMenuItem
            // 
            this.saveACopyToolStripMenuItem.Name = "saveACopyToolStripMenuItem";
            this.saveACopyToolStripMenuItem.Size = new System.Drawing.Size(191, 26);
            this.saveACopyToolStripMenuItem.Text = "Save a Copy";
            // 
            // loadDataToolStripMenuItem
            // 
            this.loadDataToolStripMenuItem.Name = "loadDataToolStripMenuItem";
            this.loadDataToolStripMenuItem.Size = new System.Drawing.Size(191, 26);
            this.loadDataToolStripMenuItem.Text = "Load Data";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(191, 26);
            this.exitToolStripMenuItem.Text = "Exit";
            // 
            // ImportButton
            // 
            this.ImportButton.Image = ((System.Drawing.Image)(resources.GetObject("ImportButton.Image")));
            this.ImportButton.Name = "ImportButton";
            this.ImportButton.Size = new System.Drawing.Size(88, 27);
            this.ImportButton.Text = "Import";
            this.ImportButton.Click += new System.EventHandler(this.aasasToolStripMenuItem_Click);
            // 
            // CalCulateButton
            // 
            this.CalCulateButton.Image = ((System.Drawing.Image)(resources.GetObject("CalCulateButton.Image")));
            this.CalCulateButton.Name = "CalCulateButton";
            this.CalCulateButton.Size = new System.Drawing.Size(104, 27);
            this.CalCulateButton.Text = "Calculate";
            this.CalCulateButton.Click += new System.EventHandler(this.bbbbToolStripMenuItem_Click);
            // 
            // FileName
            // 
            this.FileName.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.FileName.Name = "FileName";
            this.FileName.Size = new System.Drawing.Size(100, 27);
            // 
            // location
            // 
            this.location.Location = new System.Drawing.Point(465, 563);
            this.location.Name = "location";
            this.location.Size = new System.Drawing.Size(10, 28);
            this.location.TabIndex = 29;
            this.location.Visible = false;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Segoe UI Symbol", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(28, 310);
            this.label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(80, 20);
            this.label13.TabIndex = 64;
            this.label13.Text = "Flat Funnel";
            // 
            // FlatFunnel
            // 
            this.FlatFunnel.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FlatFunnel.Location = new System.Drawing.Point(143, 307);
            this.FlatFunnel.Margin = new System.Windows.Forms.Padding(4);
            this.FlatFunnel.Name = "FlatFunnel";
            this.FlatFunnel.Size = new System.Drawing.Size(84, 26);
            this.FlatFunnel.TabIndex = 63;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Segoe UI Symbol", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(28, 281);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(84, 20);
            this.label12.TabIndex = 62;
            this.label12.Text = "Outer Edge";
            // 
            // OEdge
            // 
            this.OEdge.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OEdge.Location = new System.Drawing.Point(143, 278);
            this.OEdge.Margin = new System.Windows.Forms.Padding(4);
            this.OEdge.Name = "OEdge";
            this.OEdge.Size = new System.Drawing.Size(84, 26);
            this.OEdge.TabIndex = 56;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI Symbol", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(28, 253);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(73, 20);
            this.label8.TabIndex = 61;
            this.label8.Text = "Rotor Dia";
            // 
            // RotorDia
            // 
            this.RotorDia.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RotorDia.Location = new System.Drawing.Point(143, 250);
            this.RotorDia.Margin = new System.Windows.Forms.Padding(4);
            this.RotorDia.Name = "RotorDia";
            this.RotorDia.Size = new System.Drawing.Size(84, 26);
            this.RotorDia.TabIndex = 55;
            this.RotorDia.TextChanged += new System.EventHandler(this.RotorDia_TextChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI Symbol", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(28, 203);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(71, 20);
            this.label7.TabIndex = 54;
            this.label7.Text = "Diversion";
            // 
            // Diversion
            // 
            this.Diversion.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Diversion.Location = new System.Drawing.Point(143, 200);
            this.Diversion.Margin = new System.Windows.Forms.Padding(4);
            this.Diversion.Name = "Diversion";
            this.Diversion.Size = new System.Drawing.Size(84, 26);
            this.Diversion.TabIndex = 53;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI Symbol", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(28, 176);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(85, 20);
            this.label6.TabIndex = 51;
            this.label6.Text = "Safety Area";
            // 
            // Safety
            // 
            this.Safety.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Safety.Location = new System.Drawing.Point(143, 173);
            this.Safety.Margin = new System.Windows.Forms.Padding(4);
            this.Safety.Name = "Safety";
            this.Safety.Size = new System.Drawing.Size(84, 26);
            this.Safety.TabIndex = 50;
            // 
            // App1East
            // 
            this.App1East.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.App1East.Location = new System.Drawing.Point(234, 398);
            this.App1East.Margin = new System.Windows.Forms.Padding(4);
            this.App1East.Name = "App1East";
            this.App1East.Size = new System.Drawing.Size(97, 26);
            this.App1East.TabIndex = 58;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Symbol", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(145, 87);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 20);
            this.label1.TabIndex = 39;
            this.label1.Text = "Northing";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI Symbol", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(28, 401);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(88, 20);
            this.label4.TabIndex = 43;
            this.label4.Text = "Approach-1";
            // 
            // App2East
            // 
            this.App2East.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.App2East.Location = new System.Drawing.Point(234, 422);
            this.App2East.Margin = new System.Windows.Forms.Padding(4);
            this.App2East.Name = "App2East";
            this.App2East.Size = new System.Drawing.Size(97, 26);
            this.App2East.TabIndex = 60;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Segoe UI Symbol", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(326, 144);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(60, 20);
            this.label11.TabIndex = 48;
            this.label11.Text = "Reverse";
            // 
            // App1North
            // 
            this.App1North.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.App1North.Location = new System.Drawing.Point(143, 398);
            this.App1North.Margin = new System.Windows.Forms.Padding(4);
            this.App1North.Name = "App1North";
            this.App1North.Size = new System.Drawing.Size(84, 26);
            this.App1North.TabIndex = 57;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Segoe UI Symbol", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(28, 424);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(88, 20);
            this.label9.TabIndex = 44;
            this.label9.Text = "Approach-2";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI Symbol", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(245, 86);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 20);
            this.label2.TabIndex = 40;
            this.label2.Text = "Easting";
            // 
            // HRPElevation
            // 
            this.HRPElevation.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.HRPElevation.Location = new System.Drawing.Point(143, 227);
            this.HRPElevation.Margin = new System.Windows.Forms.Padding(4);
            this.HRPElevation.Name = "HRPElevation";
            this.HRPElevation.Size = new System.Drawing.Size(84, 26);
            this.HRPElevation.TabIndex = 52;
            // 
            // ReverseBearing
            // 
            this.ReverseBearing.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ReverseBearing.Location = new System.Drawing.Point(234, 141);
            this.ReverseBearing.Margin = new System.Windows.Forms.Padding(4);
            this.ReverseBearing.MaxLength = 6;
            this.ReverseBearing.Name = "ReverseBearing";
            this.ReverseBearing.Size = new System.Drawing.Size(84, 26);
            this.ReverseBearing.TabIndex = 46;
            // 
            // H_Northing
            // 
            this.H_Northing.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.H_Northing.Location = new System.Drawing.Point(143, 112);
            this.H_Northing.Margin = new System.Windows.Forms.Padding(4);
            this.H_Northing.Name = "H_Northing";
            this.H_Northing.Size = new System.Drawing.Size(84, 26);
            this.H_Northing.TabIndex = 38;
            // 
            // App2North
            // 
            this.App2North.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.App2North.Location = new System.Drawing.Point(143, 421);
            this.App2North.Margin = new System.Windows.Forms.Padding(4);
            this.App2North.Name = "App2North";
            this.App2North.Size = new System.Drawing.Size(84, 26);
            this.App2North.TabIndex = 59;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9.2F);
            this.label3.Location = new System.Drawing.Point(28, 114);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(39, 19);
            this.label3.TabIndex = 42;
            this.label3.Text = "HRP";
            // 
            // H_Easting
            // 
            this.H_Easting.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.H_Easting.Location = new System.Drawing.Point(234, 112);
            this.H_Easting.Margin = new System.Windows.Forms.Padding(4);
            this.H_Easting.Name = "H_Easting";
            this.H_Easting.Size = new System.Drawing.Size(97, 26);
            this.H_Easting.TabIndex = 41;
            // 
            // Bearing
            // 
            this.Bearing.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Bearing.Location = new System.Drawing.Point(143, 141);
            this.Bearing.Margin = new System.Windows.Forms.Padding(4);
            this.Bearing.MaxLength = 6;
            this.Bearing.Name = "Bearing";
            this.Bearing.Size = new System.Drawing.Size(84, 26);
            this.Bearing.TabIndex = 45;
            this.Bearing.TextChanged += new System.EventHandler(this.Bearing_TextChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Segoe UI Symbol", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(28, 144);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(60, 20);
            this.label10.TabIndex = 47;
            this.label10.Text = "Bearing";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI Symbol", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(28, 229);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(102, 20);
            this.label5.TabIndex = 49;
            this.label5.Text = "HRP Elevation";
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.Location = new System.Drawing.Point(488, 87);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(4);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(163, 404);
            this.dataGridView1.TabIndex = 65;
            this.dataGridView1.Visible = false;
            // 
            // Fato
            // 
            this.Fato.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Fato.Location = new System.Drawing.Point(143, 335);
            this.Fato.Margin = new System.Windows.Forms.Padding(4);
            this.Fato.Name = "Fato";
            this.Fato.Size = new System.Drawing.Size(84, 26);
            this.Fato.TabIndex = 68;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Segoe UI Symbol", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(28, 337);
            this.label14.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(45, 20);
            this.label14.TabIndex = 67;
            this.label14.Text = "FATO";
            // 
            // TOLF
            // 
            this.TOLF.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TOLF.Location = new System.Drawing.Point(143, 364);
            this.TOLF.Margin = new System.Windows.Forms.Padding(4);
            this.TOLF.Name = "TOLF";
            this.TOLF.Size = new System.Drawing.Size(84, 26);
            this.TOLF.TabIndex = 70;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Segoe UI Symbol", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(28, 366);
            this.label15.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(42, 20);
            this.label15.TabIndex = 69;
            this.label15.Text = "TOLF";
            // 
            // SelectedID
            // 
            this.SelectedID.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SelectedID.Location = new System.Drawing.Point(278, 366);
            this.SelectedID.Margin = new System.Windows.Forms.Padding(4);
            this.SelectedID.Name = "SelectedID";
            this.SelectedID.Size = new System.Drawing.Size(97, 26);
            this.SelectedID.TabIndex = 71;
            this.SelectedID.Visible = false;
            // 
            // button4
            // 
            this.button4.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button4.Location = new System.Drawing.Point(440, 508);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(241, 34);
            this.button4.TabIndex = 72;
            this.button4.Text = "Calculate && Show Data";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // Head
            // 
            this.Head.AutoSize = true;
            this.Head.Font = new System.Drawing.Font("Tahoma", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Head.Location = new System.Drawing.Point(116, 44);
            this.Head.Name = "Head";
            this.Head.Size = new System.Drawing.Size(0, 23);
            this.Head.TabIndex = 73;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(25, 535);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 74;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click_2);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(268, 508);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(75, 23);
            this.button5.TabIndex = 75;
            this.button5.Text = "button5";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(709, 622);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.Head);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.SelectedID);
            this.Controls.Add(this.TOLF);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.Fato);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.FlatFunnel);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.OEdge);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.RotorDia);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.Diversion);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.Safety);
            this.Controls.Add(this.App1East);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.App2East);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.App1North);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.HRPElevation);
            this.Controls.Add(this.ReverseBearing);
            this.Controls.Add(this.H_Northing);
            this.Controls.Add(this.App2North);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.H_Easting);
            this.Controls.Add(this.Bearing);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.location);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.Text = "Helipad Obstacle Calculations";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ImportButton;
        private System.Windows.Forms.ToolStripMenuItem CalCulateButton;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripTextBox FileName;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.ToolStripMenuItem exportToExcelToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveACopyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadDataToolStripMenuItem;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox location;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox FlatFunnel;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox OEdge;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox RotorDia;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox Diversion;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox Safety;
        private System.Windows.Forms.TextBox App1East;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox App2East;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox App1North;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox HRPElevation;
        private System.Windows.Forms.TextBox ReverseBearing;
        private System.Windows.Forms.TextBox H_Northing;
        private System.Windows.Forms.TextBox App2North;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox H_Easting;
        private System.Windows.Forms.TextBox Bearing;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TextBox Fato;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox TOLF;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox SelectedID;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Label Head;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button5;
    }
}

