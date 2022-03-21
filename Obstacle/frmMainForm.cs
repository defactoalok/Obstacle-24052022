using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Obstacle
{
    public partial class frmMainForm : Form
    {
        public frmMainForm()
        {
            InitializeComponent();
        }

        private void btn_NewRecord_Click(object sender, EventArgs e)
        {
            string Msg= "Please select the Csv file in the format as - Obj.No,Object Name,Latitude,Longitude,Northing,Easting, Elevation";

            MessageBox.Show(Msg);

            Form1 frmForm1 = new Form1();
            frmForm1.Show();
            frmForm1.Controls["SelectedID"].Text = "0";
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            frmSelectRecord frmEdit = new frmSelectRecord();
            frmEdit.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
