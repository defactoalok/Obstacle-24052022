using System;
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
            //string Msg = "Please select the Csv file WITHOUT header in the format as - Obj.No,Object Name,Northing,Easting, Elevation." +
            //  "The software will calculate Latitude and Longitude itself";


            //MessageBox.Show(Msg);
            //frmDistanceBearing
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

        private void button3_Click(object sender, EventArgs e)
        {
            frmDistanceBearing frmForm1 = new frmDistanceBearing();
            frmForm1.Show();
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            frmAirportCode2 frmForm1 = new frmAirportCode2();
            frmForm1.Show();
        }

       
        
    }
}
