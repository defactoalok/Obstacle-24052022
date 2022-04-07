namespace Obstacle
{
    partial class frmMainForm
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
            this.btn_NewRecord = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btn_NewRecord
            // 
            this.btn_NewRecord.Font = new System.Drawing.Font("Tahoma", 10.2F);
            this.btn_NewRecord.Location = new System.Drawing.Point(90, 34);
            this.btn_NewRecord.Name = "btn_NewRecord";
            this.btn_NewRecord.Size = new System.Drawing.Size(403, 54);
            this.btn_NewRecord.TabIndex = 0;
            this.btn_NewRecord.Text = "Heliport Obstructions Calculation";
            this.btn_NewRecord.UseVisualStyleBackColor = true;
            this.btn_NewRecord.Click += new System.EventHandler(this.btn_NewRecord_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(82, 448);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(133, 54);
            this.button2.TabIndex = 1;
            this.button2.Text = "Existing Helipad Data";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Visible = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Tahoma", 10F);
            this.button1.Location = new System.Drawing.Point(90, 229);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(403, 54);
            this.button1.TabIndex = 3;
            this.button1.Text = "Exit";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button3
            // 
            this.button3.Font = new System.Drawing.Font("Tahoma", 10.2F);
            this.button3.Location = new System.Drawing.Point(90, 169);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(403, 54);
            this.button3.TabIndex = 4;
            this.button3.Text = "Calculate Distance && Bearing";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Font = new System.Drawing.Font("Tahoma", 10.2F);
            this.button4.Location = new System.Drawing.Point(90, 94);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(403, 54);
            this.button4.TabIndex = 5;
            this.button4.Text = "Airport Code 2 Obstacle Calculations";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // frmMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(582, 581);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.btn_NewRecord);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "frmMainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmMainForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_NewRecord;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
    }
}