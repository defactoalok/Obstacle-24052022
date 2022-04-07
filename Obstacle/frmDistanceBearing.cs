using CoordinateSharp;
using Microsoft.VisualBasic;
using System;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using System.Windows.Forms;

namespace Obstacle
{
    public partial class frmDistanceBearing : Form
    {
        public frmDistanceBearing()
        {
            InitializeComponent();

        }

        private void frmDistanceBearing_Load(object sender, EventArgs e)
        {
            LatD.KeyPress += ValidateKeyPress;
            LatM.KeyPress += ValidateKeyPress;
            LatS.KeyPress += ValidateKeyPress;
            LngD.KeyPress += ValidateKeyPress;
            LngM.KeyPress += ValidateKeyPress;
            LngS.KeyPress += ValidateKeyPress;
            LatD2.KeyPress += ValidateKeyPress;
            LatM2.KeyPress += ValidateKeyPress;
            LatS2.KeyPress += ValidateKeyPress;
            LngD2.KeyPress += ValidateKeyPress;
            LngM2.KeyPress += ValidateKeyPress;
            LngS2.KeyPress += ValidateKeyPress;

            RLatD.KeyPress += ValidateKeyPress;
            RLatM.KeyPress += ValidateKeyPress;
            RLatS.KeyPress += ValidateKeyPress;
            RLngD.KeyPress += ValidateKeyPress;
            RLngM.KeyPress += ValidateKeyPress;
            RLngS.KeyPress += ValidateKeyPress;

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }
        private void ValidateKeyPress(object sender, KeyPressEventArgs e)
        {

            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
                (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one double point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }
        private void App1East_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.LatD.Text != "" && this.LatM.Text != "" && this.LatS.Text != "" && this.LngD.Text != "" &&
              this.LngM.Text != "" && this.LngS.Text != "" && this.LatD2.Text != "" && this.LatM2.Text != "" &&
              this.LatS2.Text != "" && this.LngD2.Text != "" && this.LngM2.Text != "" && this.LngS2.Text != "")
            {

                double latitude = double.Parse(this.LatD.Text) + (double.Parse(this.LatM.Text) / 60) + (double.Parse(this.LatS.Text) / 3600);
                double longitude = double.Parse(this.LngD.Text) + (double.Parse(this.LngM.Text) / 60) + (double.Parse(this.LngS.Text) / 3600);
                double latitude2 = double.Parse(this.LatD2.Text) + (double.Parse(this.LatM2.Text) / 60) + (double.Parse(this.LatS2.Text) / 3600);
                double longitude2 = double.Parse(this.LngD2.Text) + (double.Parse(this.LngM2.Text) / 60) + (double.Parse(this.LngS2.Text) / 3600);

                Coordinate c = new Coordinate(latitude, longitude);
                Coordinate c2 = new Coordinate(latitude2, longitude2);
                Distance d = new Distance(c, c2, Shape.Ellipsoid);
                this.Distance.Text = Math.Round (d.Meters,2).ToString();
                this.Bearing.Text = Math.Round(d.Bearing,2).ToString();
                d = new Distance(c2, c, Shape.Ellipsoid);
                this.BackBearing.Text = Math.Round(d.Bearing,2).ToString();
                this.Easting.Text = c.UTM.Easting.ToString();
                this.Northing.Text = c.UTM.Northing.ToString();
                this.Easting1.Text = c2.UTM.Easting.ToString();
                this.Northing1.Text = c2.UTM.Northing.ToString();
                this.ForwardDms.Text = decimaltodms(double.Parse(this.Bearing.Text));
                this.BackwardDms.Text = decimaltodms(double.Parse(this.BackBearing.Text));
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            if (this.RLatD.Text != "" && this.RLatM.Text != "" && this.RLatS.Text != "" && this.RLngD.Text != "" &&
             this.RLngM.Text != "" && this.RLngS.Text != "" && this.Zone.Text != "")
            {
                /*double rlngd = double.Parse(this.RLngD.Text);
                double zone1 = ((rlngd+ 180) / 6)+1;
                this.Zone.Text = Convert.ToInt32(zone1).ToString();*/

                {
                    string Msg = "Please select the Csv file WITHOUT header in the format as - Obj.No,Object Name,Northing,Easting, Elevation." +
                        "The software will calculate Latitude and Longitude itself";


                    MessageBox.Show(Msg);
                    string DeleteData = "Delete from ConvCoordinates";
                    string appendData = "Insert into ConvCoordinates([SL NO], [OBJECT],  " +
                        "NORTHING, EASTING, Elevation,LATITUDE, LONGITUDE,Distance," +
                        "Backward_Bearing,Forward_Bearing,ForwardBDMS,BackwardBDMS ) " +
                        "Values(@SL,@Object,@North,@East,@Elev,@Lat,@long,@Distance," +
                        "@Backward_Bearing,@Forward_Bearing,@ForwardBDMS,@BackwardBDMS)";
                    openFileDialog1 = new OpenFileDialog();
                    // openFileDialog1.ShowDialog();
                    if (openFileDialog1.ShowDialog() == DialogResult.OK)
                        try
                        {
                            {
                                string filname = openFileDialog1.FileName;

                                // var reader = new StreamReader(File.OpenRead(filname));

                                string OledbConnectString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=ObstaclesData.accdb";
                                using (OleDbConnection connection = new OleDbConnection(OledbConnectString))
                                {
                                   // this.Zone.Text = Interaction.InputBox("Please input Zone No.", "Zone", "");

                                    connection.Open();
                                    OleDbCommand cmdd = new OleDbCommand(DeleteData, connection);
                                    cmdd.ExecuteNonQuery();

                                    OleDbCommand cmd = new OleDbCommand(appendData, connection);

                                    string[] lines = System.IO.File.ReadAllLines(filname);

                                    foreach (string line in lines)
                                    {

                                        var cols = line.Split(',');

                                        cmd.Parameters.AddWithValue("@SL", cols[0]);
                                        cmd.Parameters.AddWithValue("@Object", cols[1]);
                                        cmd.Parameters.AddWithValue("@North", double.Parse(cols[2]));
                                        cmd.Parameters.AddWithValue("@East", double.Parse(cols[3]));
                                        cmd.Parameters.AddWithValue("@Elev", double.Parse(cols[4]));
                                        LatLong(cols[3], cols[2], int.Parse(this.Zone.Text), 
                                            out string Lat, out string Lng, out double PDistance, 
                                            out double Backward, out double Forward, 
                                            out string ForwardDms, out string BackwardDms);
                                        cmd.Parameters.AddWithValue("@Lat", Lat);
                                        cmd.Parameters.AddWithValue("@long", Lng);
                                        cmd.Parameters.AddWithValue("@Distance", PDistance);
                                        cmd.Parameters.AddWithValue("@Backward_Bearing", Backward);
                                        cmd.Parameters.AddWithValue("@Forward_Bearing", Forward);
                                        cmd.Parameters.AddWithValue("@ForwardBDMS", ForwardDms);
                                        cmd.Parameters.AddWithValue("@BackwardBDMS", BackwardDms);
                                        
                                        cmd.ExecuteNonQuery();
                                        cmd.Parameters.Clear();


                                    }


                                }

                            }
                        }
                        catch (Exception ex)
                        {
                            var st = new StackTrace(ex, true);
                            // Get the top stack frame
                            var frame = st.GetFrame(0);
                            // Get the line number from the stack frame
                            var line = frame.GetFileLineNumber();
                            MessageBox.Show("Exception Message: " + ex.Message + " " + line);
                        }
                    MessageBox.Show("Finish");
                    showCoordgrid();
                  


                }
            }
        }
        private void showgrid()
        {

            string OledbConnectString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source= ObstaclesData.accdb";
            string getRecords = "SELECT [SL NO], OBJECT, NORTHING, EASTING, LATITUDE, LONGITUDE," +
                 " Elevation, Distance,Forward_Bearing,ForwardBDMS,Backward_Bearing,BackwardBDMS FROM ConvCoordinates";
 
            using (OleDbConnection connection = new OleDbConnection(OledbConnectString))
            {

                try
                {
                    OleDbCommand cmd = new OleDbCommand(getRecords, connection);
                    connection.Open();
                    DataTable dt = new DataTable();
                    using (OleDbDataAdapter adapter = new OleDbDataAdapter(getRecords, connection))
                    {
                        DataSet ds = new DataSet();
                        adapter.Fill(ds);
                        FrmLoadData loadData = new FrmLoadData();
                        loadData.Show();
                        loadData.Controls["showCoordinates"].Text= "1";

                        DataGridView dg = (DataGridView)loadData.Controls["dataGridView1"];

                        //dataGridView1.DataSource = ds.Tables[0];
                        dg.DataSource = ds.Tables[0];
                        
                        //dataGridView1.Refresh();
                        dg.Refresh();

                       
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Exception Message: " + ex.Message);
                }
            }
        }

        private void showCoordgrid()
        {

            string OledbConnectString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source= ObstaclesData.accdb";
            string getRecords = "SELECT   id, [SL NO], OBJECT, NORTHING, EASTING, LATITUDE, LONGITUDE," +
                 " Elevation, Distance,Forward_Bearing,ForwardBDMS,Backward_Bearing,BackwardBDMS FROM ConvCoordinates";

            using (OleDbConnection connection = new OleDbConnection(OledbConnectString))
            {

                try
                {
                    
                    OleDbCommand cmd = new OleDbCommand(getRecords, connection);
                    connection.Open();
                    DataTable dt = new DataTable();
                    using (OleDbDataAdapter adapter = new OleDbDataAdapter(getRecords, connection))
                    {
                        DataSet ds = new DataSet();
                        adapter.Fill(ds);
                        frmShowCoordinates loadData = new frmShowCoordinates();
                        loadData.Show();
                       // loadData.Controls["showCoordinates"].Text = "1";

                        DataGridView dg = (DataGridView)loadData.Controls["dataGridView1"];

                        //dataGridView1.DataSource = ds.Tables[0];
                        dg.DataSource = ds.Tables[0];

                        //dataGridView1.Refresh();
                        dg.Refresh();


                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Exception Message: " + ex.Message);
                }
            }
        }
        private string decimaltodms(double Forward )
        {
            double dd = Math.Truncate(Forward);
            double dM = Math.Truncate((Forward - dd) * 60);
            double ds = Math.Truncate((((Forward - dd) * 60)-dM)*60);
           string todms = dd.ToString() + "º" + dM.ToString() + "'" + ds.ToString() + '"';
            return todms;
        }
        private string LatLong(string Easting, string Northing, int ZoneNo, out string Lat, 
            out string Lng,out double PDistance, out double Backward,out double Forward,
            out string ForwardDms,out string BackwardDms)
        {
            UniversalTransverseMercator utm = new UniversalTransverseMercator("N", ZoneNo, double.Parse(Easting), double.Parse(Northing));
            Coordinate c = UniversalTransverseMercator.ConvertUTMtoLatLong(utm);
            c.FormatOptions.Format = CoordinateFormatType.Degree_Minutes_Seconds;
            c.FormatOptions.Display_Leading_Zeros = true;
           c.FormatOptions.Round = 7;
            Lat = c.Latitude.ToString();  //N 2º 7' 2.332" E 6º 36' 12.653"
            Lng = c.Longitude.ToString();
            c.FormatOptions.Format = CoordinateFormatType.Decimal;
            double latitude = double.Parse(this.RLatD.Text) + (double.Parse(this.RLatM.Text) / 60) + (double.Parse(this.RLatS.Text) / 3600);
            double longitude = double.Parse(this.RLngD.Text) + (double.Parse(this.RLngM.Text) / 60) + (double.Parse(this.RLngS.Text) / 3600);

            double latitude2 = c.Latitude.ToDouble();
            double longitude2 = c.Longitude.ToDouble();

            c = new Coordinate(latitude,longitude);
            Coordinate c2 = new Coordinate(latitude2, longitude2);
         
            Distance d = new Distance(c, c2, Shape.Ellipsoid);

            PDistance= Math.Round(d.Meters, 1) ;
          
            Forward= Math.Round(d.Bearing, 2);
            
            ForwardDms = decimaltodms(Forward);
            
            d = new Distance(c2, c, Shape.Ellipsoid);
            
            Backward = Math.Round(d.Bearing, 2) ;
            
            BackwardDms = decimaltodms(Backward);
            
            return Lat;
        }
    }
}
