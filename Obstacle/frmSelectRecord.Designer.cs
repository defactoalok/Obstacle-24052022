namespace Obstacle
{
    partial class frmSelectRecord
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.TOLF = new System.Windows.Forms.TextBox();
            this.Fato = new System.Windows.Forms.TextBox();
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
            this.SelectedID = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(25, 54);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(1402, 487);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(25, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(115, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "Select Record";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.button1.Location = new System.Drawing.Point(1274, 547);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(155, 34);
            this.button1.TabIndex = 2;
            this.button1.Text = "Show Data";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // TOLF
            // 
            this.TOLF.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TOLF.Location = new System.Drawing.Point(626, 438);
            this.TOLF.Margin = new System.Windows.Forms.Padding(4);
            this.TOLF.Name = "TOLF";
            this.TOLF.Size = new System.Drawing.Size(84, 26);
            this.TOLF.TabIndex = 86;
            this.TOLF.Visible = false;
            // 
            // Fato
            // 
            this.Fato.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Fato.Location = new System.Drawing.Point(626, 409);
            this.Fato.Margin = new System.Windows.Forms.Padding(4);
            this.Fato.Name = "Fato";
            this.Fato.Size = new System.Drawing.Size(84, 26);
            this.Fato.TabIndex = 85;
            this.Fato.Visible = false;
            // 
            // FlatFunnel
            // 
            this.FlatFunnel.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FlatFunnel.Location = new System.Drawing.Point(626, 381);
            this.FlatFunnel.Margin = new System.Windows.Forms.Padding(4);
            this.FlatFunnel.Name = "FlatFunnel";
            this.FlatFunnel.Size = new System.Drawing.Size(84, 26);
            this.FlatFunnel.TabIndex = 84;
            this.FlatFunnel.Visible = false;
            // 
            // OEdge
            // 
            this.OEdge.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OEdge.Location = new System.Drawing.Point(626, 352);
            this.OEdge.Margin = new System.Windows.Forms.Padding(4);
            this.OEdge.Name = "OEdge";
            this.OEdge.Size = new System.Drawing.Size(84, 26);
            this.OEdge.TabIndex = 79;
            this.OEdge.Visible = false;
            // 
            // RotorDia
            // 
            this.RotorDia.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RotorDia.Location = new System.Drawing.Point(626, 324);
            this.RotorDia.Margin = new System.Windows.Forms.Padding(4);
            this.RotorDia.Name = "RotorDia";
            this.RotorDia.Size = new System.Drawing.Size(84, 26);
            this.RotorDia.TabIndex = 78;
            this.RotorDia.Visible = false;
            // 
            // Diversion
            // 
            this.Diversion.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Diversion.Location = new System.Drawing.Point(626, 274);
            this.Diversion.Margin = new System.Windows.Forms.Padding(4);
            this.Diversion.Name = "Diversion";
            this.Diversion.Size = new System.Drawing.Size(84, 26);
            this.Diversion.TabIndex = 77;
            this.Diversion.Visible = false;
            // 
            // Safety
            // 
            this.Safety.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Safety.Location = new System.Drawing.Point(626, 247);
            this.Safety.Margin = new System.Windows.Forms.Padding(4);
            this.Safety.Name = "Safety";
            this.Safety.Size = new System.Drawing.Size(84, 26);
            this.Safety.TabIndex = 75;
            this.Safety.Visible = false;
            // 
            // App1East
            // 
            this.App1East.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.App1East.Location = new System.Drawing.Point(717, 195);
            this.App1East.Margin = new System.Windows.Forms.Padding(4);
            this.App1East.Name = "App1East";
            this.App1East.Size = new System.Drawing.Size(97, 26);
            this.App1East.TabIndex = 81;
            this.App1East.Visible = false;
            // 
            // App2East
            // 
            this.App2East.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.App2East.Location = new System.Drawing.Point(717, 219);
            this.App2East.Margin = new System.Windows.Forms.Padding(4);
            this.App2East.Name = "App2East";
            this.App2East.Size = new System.Drawing.Size(97, 26);
            this.App2East.TabIndex = 83;
            this.App2East.Visible = false;
            // 
            // App1North
            // 
            this.App1North.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.App1North.Location = new System.Drawing.Point(626, 195);
            this.App1North.Margin = new System.Windows.Forms.Padding(4);
            this.App1North.Name = "App1North";
            this.App1North.Size = new System.Drawing.Size(84, 26);
            this.App1North.TabIndex = 80;
            this.App1North.Visible = false;
            // 
            // HRPElevation
            // 
            this.HRPElevation.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.HRPElevation.Location = new System.Drawing.Point(626, 301);
            this.HRPElevation.Margin = new System.Windows.Forms.Padding(4);
            this.HRPElevation.Name = "HRPElevation";
            this.HRPElevation.Size = new System.Drawing.Size(84, 26);
            this.HRPElevation.TabIndex = 76;
            this.HRPElevation.Visible = false;
            // 
            // ReverseBearing
            // 
            this.ReverseBearing.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ReverseBearing.Location = new System.Drawing.Point(717, 161);
            this.ReverseBearing.Margin = new System.Windows.Forms.Padding(4);
            this.ReverseBearing.MaxLength = 6;
            this.ReverseBearing.Name = "ReverseBearing";
            this.ReverseBearing.Size = new System.Drawing.Size(84, 26);
            this.ReverseBearing.TabIndex = 74;
            this.ReverseBearing.Visible = false;
            // 
            // H_Northing
            // 
            this.H_Northing.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.H_Northing.Location = new System.Drawing.Point(626, 128);
            this.H_Northing.Margin = new System.Windows.Forms.Padding(4);
            this.H_Northing.Name = "H_Northing";
            this.H_Northing.Size = new System.Drawing.Size(84, 26);
            this.H_Northing.TabIndex = 71;
            this.H_Northing.Visible = false;
            // 
            // App2North
            // 
            this.App2North.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.App2North.Location = new System.Drawing.Point(626, 218);
            this.App2North.Margin = new System.Windows.Forms.Padding(4);
            this.App2North.Name = "App2North";
            this.App2North.Size = new System.Drawing.Size(84, 26);
            this.App2North.TabIndex = 82;
            this.App2North.Visible = false;
            // 
            // H_Easting
            // 
            this.H_Easting.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.H_Easting.Location = new System.Drawing.Point(717, 132);
            this.H_Easting.Margin = new System.Windows.Forms.Padding(4);
            this.H_Easting.Name = "H_Easting";
            this.H_Easting.Size = new System.Drawing.Size(97, 26);
            this.H_Easting.TabIndex = 72;
            this.H_Easting.Visible = false;
            // 
            // Bearing
            // 
            this.Bearing.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Bearing.Location = new System.Drawing.Point(626, 161);
            this.Bearing.Margin = new System.Windows.Forms.Padding(4);
            this.Bearing.MaxLength = 6;
            this.Bearing.Name = "Bearing";
            this.Bearing.Size = new System.Drawing.Size(84, 26);
            this.Bearing.TabIndex = 73;
            this.Bearing.Visible = false;
            // 
            // SelectedID
            // 
            this.SelectedID.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SelectedID.Location = new System.Drawing.Point(718, 253);
            this.SelectedID.Margin = new System.Windows.Forms.Padding(4);
            this.SelectedID.Name = "SelectedID";
            this.SelectedID.Size = new System.Drawing.Size(97, 26);
            this.SelectedID.TabIndex = 87;
            this.SelectedID.Visible = false;
            // 
            // frmSelectRecord
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1441, 593);
            this.Controls.Add(this.SelectedID);
            this.Controls.Add(this.TOLF);
            this.Controls.Add(this.Fato);
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
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dataGridView1);
            this.Name = "frmSelectRecord";
            this.Text = "frmSelectRecord";
            this.Load += new System.EventHandler(this.frmSelectRecord_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox TOLF;
        private System.Windows.Forms.TextBox Fato;
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
        private System.Windows.Forms.TextBox SelectedID;
    }
}