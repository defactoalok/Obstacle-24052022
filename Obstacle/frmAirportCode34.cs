using CoordinateSharp;
using Microsoft.VisualBasic;
using System;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using System.Windows.Forms;
using System.IO;
using MapWinGIS;
using Point = MapWinGIS.Point;
using System.Collections.Generic;

namespace Obstacle
{
    public partial class frmAirportCode34 : Form

    {
        string CurrentDir = Directory.GetCurrentDirectory();
        string STstTopLeft, STstTopRight, STstBottomLeft, STstBottomRight;
        public frmAirportCode34()
        {
            InitializeComponent();
           

        }

        private void frmAirportCode34_Load(object sender, EventArgs e)
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
              this.LatS2.Text != "" && this.LngD2.Text != "" && this.LngM2.Text != "" && this.LngS2.Text != "" &&
              this.ArpLatD.Text != "" && this.ArpLatM.Text != "" && this.ArpLatS.Text != "" && this.ArpLngD.Text != "" &&
               this.ArpLngM.Text != "" && this.ArpLngS.Text != "")
            {

                double latitude = double.Parse(this.LatD.Text) + (double.Parse(this.LatM.Text) / 60) + (double.Parse(this.LatS.Text) / 3600);
                double longitude = double.Parse(this.LngD.Text) + (double.Parse(this.LngM.Text) / 60) + (double.Parse(this.LngS.Text) / 3600);
                double latitude2 = double.Parse(this.LatD2.Text) + (double.Parse(this.LatM2.Text) / 60) + (double.Parse(this.LatS2.Text) / 3600);
                double longitude2 = double.Parse(this.LngD2.Text) + (double.Parse(this.LngM2.Text) / 60) + (double.Parse(this.LngS2.Text) / 3600);
                double Arplatitude = double.Parse(this.ArpLatD.Text) + (double.Parse(this.ArpLatM.Text) / 60) + (double.Parse(this.ArpLatS.Text) / 3600);
                double Arplongitude = double.Parse(this.ArpLngD.Text) + (double.Parse(this.ArpLngM.Text) / 60) + (double.Parse(this.ArpLngS.Text) / 3600);
                
                Coordinate c = new Coordinate(latitude, longitude);
                Coordinate c2 = new Coordinate(latitude2, longitude2);
                Coordinate c3 = new Coordinate(Arplatitude, Arplongitude);

                Distance d = new Distance(c, c2,  CoordinateSharp.Shape .Ellipsoid);
                this.RwyLength.Text = Math.Round(d.Meters, 2).ToString();
                this.Bearing.Text = Math.Round(d.Bearing, 6).ToString();
                d = new Distance(c2, c, CoordinateSharp.Shape.Ellipsoid);

                this.BackBearing.Text = Math.Round(d.Bearing, 6).ToString();
                this.Easting.Text = c.UTM.Easting.ToString();
                this.Northing.Text = c.UTM.Northing.ToString();
                this.Easting1.Text = c2.UTM.Easting.ToString();
                this.Northing1.Text = c2.UTM.Northing.ToString();
                this.ForwardDms.Text = decimaltodms(double.Parse(this.Bearing.Text));
                this.BackwardDms.Text = decimaltodms(double.Parse(this.BackBearing.Text));

                this.ArpEast.Text = c3.UTM.Easting.ToString();
                this.ArpNorth.Text = c3.UTM.Northing.ToString();

                GetNewCoordinates(this.Easting.Text, this.Northing.Text, this.Easting1.Text, this.Northing1.Text, 
                    double.Parse(this.BasicStrip.Text), double.Parse(this.Bearing.Text), double.Parse(this.BackBearing.Text), 
                    out double App1N, out double App1E, out double App2E, out double App2N);

                this.Rwy1East.Text = App1E.ToString(); this.Rwy1North.Text = App1N.ToString();
                this.Rwy2East.Text = App2E.ToString(); this.Rwy2North.Text = App2N.ToString();

                //  GetNewCoordinates(this.Easting1.Text, this.Northing1.Text, double.Parse(this.BasicStrip.Text), double.Parse(this.Bearing.Text),
                //   double.Parse(this.BackBearing.Text), out App1N, out App1E, out App2E, out App2N);
                this.Rwy2East.Text = App2E.ToString(); this.Rwy2North.Text = App2N.ToString();
               
                this.Zone.Text = Math.Truncate( (((longitude + 180) / 6) + 1)).ToString();
                
                LatLong(this.Rwy1East.Text, this.Rwy1North.Text, Convert.ToInt32(this.Zone.Text), out string Lat, 
                    out string Lng, out string LatDecimal, out string LongDecimal);
                
                App1Lat.Text = LatDecimal.ToString();
                App1Lng.Text = LongDecimal.ToString();
                
                LatLong(this.Rwy2East.Text, this.Rwy2North.Text, Convert.ToInt32(this.Zone.Text), out  Lat,
                 out  Lng, out  LatDecimal, out  LongDecimal);

                App2Lat.Text = LatDecimal.ToString();
                App2Lng.Text = LongDecimal.ToString();
                
                SetApp(Math.Round(double.Parse(this.Bearing.Text),6), out string NewBearing);
                this.Rwy1Label.Text = NewBearing;
                SetApp(Math.Round(double.Parse(this.BackBearing.Text),6), out NewBearing);
                this.Rwy2Label.Text = NewBearing;

            }
        }

        private void  GetNewCoordinates(string He, string Hn, string He1, String Hn1, double Distance, double Bearing, double BackBearing,
            out double App1N, out double App1E, out double App2E, out double App2N) //, out double ReverseBear)
        {
            App1E = 0;
            App2E = 0;
            App1N = 0;
            App2N = 0;
            double ReverseBear = 0;
            double SinX = 0;
            double CosX = 0;


            if (Bearing > 0)
            {
                double bearRadian = Math.Round(BackBearing * (Math.PI) / 180, 3);
                CosX = Math.Round(Distance * (Math.Cos(bearRadian)), 2);
                SinX = Math.Round(Distance * (Math.Sin(bearRadian)), 2);

                App1E = Math.Round(double.Parse(He) + SinX, 2);
                App1N = Math.Round(double.Parse(Hn) + CosX, 2);
                ReverseBear = 0;
                ReverseBear = Math.Round(BackBearing, 2);

                bearRadian = Math.Round( Bearing * (Math.PI) / 180, 3);
                CosX = Distance * (Math.Cos(bearRadian));
                SinX = Distance * (Math.Sin(bearRadian));

                App2E = Math.Round(double.Parse(He1) + SinX, 2);
                App2N = Math.Round(double.Parse(Hn1) + CosX, 2);
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
         
            Distance d = new Distance(c, c2, CoordinateSharp.Shape.Ellipsoid);

            PDistance= Math.Round(d.Meters, 1) ;
          
            Forward= Math.Round(d.Bearing, 2);
            
            ForwardDms = decimaltodms(Forward);
            
            d = new Distance(c2, c, CoordinateSharp.Shape.Ellipsoid);
            
            Backward = Math.Round(d.Bearing, 2) ;
            
            BackwardDms = decimaltodms(Backward);
            
            return Lat;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string Msg = "Please select the Csv file WITHOUT header in the format as - Obj.No,Object Name,Northing,Easting, Elevation." +
              "The software will calculate Latitude and Longitude itself";
            MessageBox.Show(Msg);
            string DeleteData = "Delete from RWYSurveyData";
            string appendData = "Insert into RWYSurveyData([SL NO], [OBJECT],  NORTHING, EASTING, Elevation," +
                "LATITUDE, LONGITUDE,LatDecimal, LongDecimal  ) " +
                @"Values(@SL,@Object,@North,@East,@Elev,@Lat,@long,@LatDecimal, @LongDecimal )";

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
                            this.Zone.Text = Interaction.InputBox("Please input Zone No.", "Zone", "");
                          //  this.SiteLocation.Text = Interaction.InputBox("Please give a name", "Location", "");
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
                                LatLong(cols[3], cols[2], int.Parse(this.Zone.Text), out string Lat, out string Lng,out string LatDecimal,out string LongDecimal);
                                cmd.Parameters.AddWithValue("@Lat", Lat);
                                cmd.Parameters.AddWithValue("@long", Lng);
                                cmd.Parameters.AddWithValue("@LatDecimal", LatDecimal);
                                cmd.Parameters.AddWithValue("@LongDecimal", LongDecimal);
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
         //   showMenu = false;

        }
        private string LatLong(string Easting, string Northing, int ZoneNo, out string Lat, out string Lng, out string LatDecimal, out string LongDecimal)
        {
            UniversalTransverseMercator utm = new UniversalTransverseMercator("N", ZoneNo, double.Parse(Easting), double.Parse(Northing));
            Coordinate c = UniversalTransverseMercator.ConvertUTMtoLatLong(utm);
            c.FormatOptions.Format = CoordinateFormatType.Degree_Minutes_Seconds;
            c.FormatOptions.Display_Leading_Zeros = true;
            c.FormatOptions.Round = 3;


            Lat = c.Latitude.ToString();  //N 2º 7' 2.332" E 6º 36' 12.653"
            Lng = c.Longitude.ToString();
            c.FormatOptions.Format = CoordinateFormatType.Decimal;
            c.FormatOptions.Round = 7;
            LatDecimal = c.Latitude.ToString();
            LongDecimal = c.Longitude.ToString();

            return Lat;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string Msg = "Please select the Csv file WITHOUT header in the format as -Distance, Elevation";
            MessageBox.Show(Msg);
            string DeleteData = "Delete from RunwayProfile";
            string appendData = "Insert into RunwayProfile([Distance], [Elevation]) "+
                @"Values(@Distance,@Elevation)";
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
                            //  this.SiteLocation.Text = Interaction.InputBox("Please give a name", "Location", "");
                            connection.Open();
                            OleDbCommand cmdd = new OleDbCommand(DeleteData, connection);
                            cmdd.ExecuteNonQuery();

                            OleDbCommand cmd = new OleDbCommand(appendData, connection);

                            string[] lines = System.IO.File.ReadAllLines(filname);

                            foreach (string line in lines)
                            {

                                var cols = line.Split(',');

                                cmd.Parameters.AddWithValue("@Distance", cols[0]);
                                cmd.Parameters.AddWithValue("@Elevation", cols[1]);
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
            MessageBox.Show("Runway Profile Imported");
            //   showMenu = false;

        }
        private double PointsLatLong(double Lat1, double Lng1, double Lat2, double Lng2,
        out double RDistance, out double Bearing)
        {
            Coordinate c1 = new Coordinate(Lat1, Lng1);
            Coordinate c2 = new Coordinate(Lat2, Lng2);
            Distance d = new Distance(c1, c2, CoordinateSharp.Shape.Ellipsoid);
            Bearing = Math.Round(d.Bearing, 2);
            RDistance = Math.Round(d.Meters,1);

            return RDistance;
        }
        private void GetObstacles(object sender, EventArgs e)
        {



            double latitude = double.Parse(this.App1Lat.Text.ToString());
            double longitude = double.Parse(this.App1Lng.Text.ToString());
            double latitude2 = double.Parse(this.App2Lat.Text.ToString());
            double longitude2 = double.Parse(this.App2Lng.Text.ToString());


            double ARPLat = double.Parse(this.ArpLatD.Text) + (double.Parse(this.ArpLatM.Text) / 60) + (double.Parse(this.ArpLatS.Text) / 3600);
            double ARPLng = double.Parse(this.ArpLngD.Text) + (double.Parse(this.ArpLngM.Text) / 60) + (double.Parse(this.ArpLngS.Text) / 3600);

            double Bear = double.Parse(this.Bearing.Text);
            double ReverseBear = double.Parse(this.BackBearing.Text);

            double distance;
            double DistanceFromApp1, DistanceFromApp2, BearingFromApp1, BearingFromApp2,
                 getX, getY, getYY, ARPDistance, PointLat, PointLng, Elevation, RWYSTRIP, RunwayLength;

            bool nearer;


            string updateQuery = "UPDATE RWYSurveyData SET Surface=@surface,X=@X,Y=@Y,YY=@YY,Distance=@Distance,DistApp1=@DistApp1," +
                "DistApp2=@DistApp2, Bearing=@Bearing,AplElevTSRWY=@AplElevTSRWY,APPElevApproach=@APPElevApproach, " +
                "APPElevIHSCONOHS=@APPElevIHSCONOHS, PElevTSAPPRWY=@PETSRWYAPP,PElevIHSCONOHS=@PElevIHSCONOHS, " +
                " ObstRwyTSApp = @ObstRwyTSApp, ObstIHSCONOHSAPPIHSAPPCON = @ObstIHSCONOHSAPPIHSAPPCON, Nearest=@Nearest where ID=@ID";

            string OledbConnectString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=ObstaclesData.accdb";

            using (OleDbConnection connection = new OleDbConnection(OledbConnectString))

            {
                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }

                OleDbCommand cmd = new OleDbCommand("Select * from RWYSurveyData  ", connection);

                {

                    using (OleDbDataReader reader = cmd.ExecuteReader())
                    {

                        while (reader.Read())
                        {
                            OleDbCommand cmdUpdate = new OleDbCommand(updateQuery, connection);
                            //  GetBearingDistance getBD = new GetBearingDistance();


                            //  try
                            // {
                            getX = 0; getY = 0; getYY = 0; DistanceFromApp1 = 0; DistanceFromApp2 = 0;
                                RWYSTRIP = 0; ARPDistance = 0; RunwayLength = 0; nearer = false;

                                double PointE = double.Parse(reader["Easting"].ToString());
                                double PointN = double.Parse(reader["Northing"].ToString());

                            PointLat = double.Parse(reader["LatDecimal"].ToString());
                                PointLng = double.Parse(reader["LongDecimal"].ToString());
                                double PointElevation= double.Parse(reader["Elevation"].ToString());

                            PointsLatLong(latitude, longitude, PointLat, PointLng, out double RDistance, out double Bearing);
                                DistanceFromApp1 = RDistance;
                                BearingFromApp1 = Bearing;

                                PointsLatLong(latitude2, longitude2, PointLat, PointLng, out RDistance, out Bearing);
                                DistanceFromApp2 = RDistance;
                                BearingFromApp2 = Bearing;

                                PointsLatLong(ARPLat, ARPLng, PointLat, PointLng, out RDistance, out Bearing);
                                ARPDistance = RDistance;

                            

                                nearer = false;

                                nearer = true ? DistanceFromApp1 <= DistanceFromApp2 : DistanceFromApp1 > DistanceFromApp2;

                                if (nearer)
                                {
                                    distance = DistanceFromApp1; Bearing = BearingFromApp1;
                                    getX = Math.Round(Math.Abs(distance * (Math.Cos((Bear - Bearing) * (Math.PI) / 180))));
                                    getY = Math.Round(Math.Abs(distance * (Math.Sin((Bear - Bearing) * (Math.PI) / 180))));
                                }
                                else
                                {
                                    distance = DistanceFromApp2; Bearing = BearingFromApp2;

                                    getX = Math.Round(Math.Abs(distance * (Math.Cos((ReverseBear - Bearing) * (Math.PI) / 180))));
                                    getY = Math.Round(Math.Abs(distance * (Math.Sin((ReverseBear - Bearing) * (Math.PI) / 180))));
                                }

                                if (!string.IsNullOrEmpty(Diversion.Text) && !string.IsNullOrEmpty(getX.ToString()) && !string.IsNullOrEmpty(RunwayStrip.Text))
                                {
                                    getYY = Math.Abs(Math.Round((getX * (double.Parse(Diversion.Text) / 100) + (int.Parse(RunwayStrip.Text)) / 2)));
                                }

                                RWYSTRIP = double.Parse(this.RunwayStrip.Text) / 2;
                                RunwayLength = double.Parse(this.RwyLength.Text.ToString());

                                Getsurface(getX, getY, getYY, DistanceFromApp1, DistanceFromApp2, RWYSTRIP, ARPDistance, RunwayLength, PointE, PointN, nearer, out string surface);

                                double nearestRwyDistance = getNearer(getX, RunwayLength, DistanceFromApp1, DistanceFromApp2, out double Nearest);

                                int ID = int.Parse(reader["ID"].ToString());

                                cmdUpdate.Parameters.AddWithValue("@Surface", surface);
                                cmdUpdate.Parameters.AddWithValue("@X", getX);
                                cmdUpdate.Parameters.AddWithValue("@Y", getY);
                                cmdUpdate.Parameters.AddWithValue("@YY", getY - getYY);
                                cmdUpdate.Parameters.AddWithValue("@Distance", distance);
                                cmdUpdate.Parameters.AddWithValue("@DistApp1",  DistanceFromApp1);
                                cmdUpdate.Parameters.AddWithValue("@DistApp2",  DistanceFromApp2);
                                cmdUpdate.Parameters.AddWithValue("@Bearing", Bearing);

                       
                            double APPElevApproach = 0;
                            double APPElevIHSCONOHS = 0;
                            double PElevIHSCONOHS=0;

                            double PETSRWYAPP = 0;
                            Elevation = 0;
                            double AplElevTSRWY = 0;
                            string ApplicableElev;
                            double ObstRwyTSApp = 0;
                            double ObstIHSCONOHSAPPIHSAPPCON=0;
// Get nearest distance for TS
                            getNearer(Math.Round(getX,0), Math.Round(RunwayLength,0), Math.Round(DistanceFromApp1,0), Math.Round(DistanceFromApp2,0), out double NearestDistance);
                            double NearDistance= Math.Round(NearestDistance);
// Get nearest distance for TS
                            ApplicableElev = "Select top 1 Elevation from RunwayProfile where Distance =@RwyDistance";

                            if (surface.Substring(0, 2) == "IH" || surface.Substring(0, 2) == "CO" || 
                                surface.Substring(0, 2) == "OC" || surface.Substring(0, 2) == "OH" || surface.Contains("-"))
                            {
                                APPElevIHSCONOHS =Convert.ToDouble( this.MaximumElevation.Text.ToString());
                                if (surface.Contains("IHS")) 
                                {
                                    PElevIHSCONOHS = 45 + APPElevIHSCONOHS;
                                  

                                }
                                if (surface.Contains("CON"))
                                {
                                    PElevIHSCONOHS = 45 + ((ARPDistance- 15000) / 20) + APPElevIHSCONOHS;
                                    
                                }
                                if (surface.Contains("OHS"))
                                {
                                    PElevIHSCONOHS = 150  + APPElevIHSCONOHS;

                                }
                                ObstIHSCONOHSAPPIHSAPPCON = Math.Round( PointElevation - PElevIHSCONOHS,1);
                            }

                            if (surface.Substring(0, 2) == "AP")
                            {
                                if (DistanceFromApp1 < DistanceFromApp2) 
                                {
                                    APPElevApproach = Convert.ToDouble(this.Rwy1Elevation.Text.ToString());
                                   
                                }
                                else
                                {
                                    APPElevApproach = Convert.ToDouble(this.Rwy2Elevation.Text.ToString());
                               
                                }
                                if (getX <= 3000)
                                {
                                    PETSRWYAPP = (getX /50) + APPElevApproach;

                                }
                                if (getX > 3000 && getX <=6600)
                                {
                                    PETSRWYAPP = 60+((getX -3000)/40) + APPElevApproach;

                                }
                                if (getX > 15000)
                                {
                                    PETSRWYAPP =150 + APPElevApproach;
                                    
                                }
                                ObstRwyTSApp = Math.Round(PointElevation - PETSRWYAPP,1);
                            }


                            OleDbCommand cmdElev = new OleDbCommand(ApplicableElev, connection);
                            
                            if (surface.Substring(0, 2) == "TS" || surface.Substring(0, 2) == "RW")
                            {
                                cmdElev.Parameters.AddWithValue("@RwyDistance", nearestRwyDistance);
                                AplElevTSRWY = Convert.ToDouble(cmdElev.ExecuteScalar());
                                if (!string.IsNullOrEmpty(AplElevTSRWY.ToString()))
                                {
                                    PETSRWYAPP = AplElevTSRWY;
                                    Elevation = AplElevTSRWY;
                                    if (surface.Substring(0, 2) == "TS")
                                    {
                                        PETSRWYAPP = ((getY - (Convert.ToDouble(this.RunwayStrip.Text) / 2)) / 7) + Elevation;
                                    }
                                    if (surface.Substring(0, 3) == "TST")
                                    {
                                        PETSRWYAPP = ((getX) * 0.04) + (getYY / 7) + Elevation;
                                    }
                                 
                                }
                                ObstRwyTSApp = Math.Round(PointElevation - PETSRWYAPP,1);
                            }
                            cmdUpdate.Parameters.AddWithValue("@AplElevTSRWY", Elevation);
                            cmdUpdate.Parameters.AddWithValue("@APPElevApproach", APPElevApproach);
                            cmdUpdate.Parameters.AddWithValue("@APPElevIHSCONOHS", APPElevIHSCONOHS);
                            cmdUpdate.Parameters.AddWithValue("@PETSRWYAPP", PETSRWYAPP);
                            cmdUpdate.Parameters.AddWithValue("@PElevIHSCONOHS", PElevIHSCONOHS);
                            cmdUpdate.Parameters.AddWithValue("@ObstRwyTSApp", ObstRwyTSApp);
                            cmdUpdate.Parameters.AddWithValue("@ObstIHSCONOHSAPPIHSAPPCON", ObstIHSCONOHSAPPIHSAPPCON);
                            cmdUpdate.Parameters.AddWithValue("@Nearest", NearDistance);
                            cmdUpdate.Parameters.AddWithValue("@ID", ID);
                            cmdUpdate.ExecuteNonQuery();
                            cmdUpdate.Parameters.Clear();
                            cmdUpdate.Dispose();
                            cmdElev.Dispose();
                            



                            //  }

                            /*     catch (Exception ex)
                                 {
                                     var st = new StackTrace(ex, true);
                                     // Get the top stack frame
                                     var frame = st.GetFrame(0);
                                     // Get the line number from the stack frame
                                     var line = frame.GetFileLineNumber();
                                     MessageBox.Show("Exception Message: " + ex.Message + " " + line);

                                 }*/

                        }

                    }


                }

                // this.dataGridView1.DefaultCellStyle.Font = new System.Drawing.Font("Tahoma", 9);
                //           showgrid();
            }
           
            MessageBox.Show("Records Updated ");
            CreateShapeFiles();
        }
        

        private void Getsurface(double getX, double getY, double getYY, double distanceFromApp1, double distanceFromApp2, double rWYSTRIP, double aRPDistance, double runwayLength, double PointE, double PointY,
            bool nearer, out string surface)
        {
            bool Funnel; string found; double DISTANCE; double strip; Funnel = false; string app;
            surface = "Nil";
            bool isTst = false;
            string PointCoordinates = PointE.ToString() + "," + PointY.ToString();

            // try
            //{
            if (distanceFromApp1 < distanceFromApp2)
                {
                    DISTANCE = distanceFromApp1;
                    app = Rwy1Label.Text.Substring(3, 2);
                }
                else
                {
                    DISTANCE = distanceFromApp2;
                    app = Rwy2Label.Text.Substring(3, 2);
            }

                strip = Math.Truncate(rWYSTRIP);

                found = "Nil";

                //Funnel

                Funnel = true ? getYY >= getY : getYY < getY;
                if (Funnel && getX <= 15000)
                {
                    found = "APP"+app;
                }


                if (getY <= strip && distanceFromApp1 <= runwayLength && distanceFromApp2 <= runwayLength)
                {
                    found = "RWY"+app;
                }

                if (getY > strip && getY <= ((strip) + 315) && getX <= 2250 && distanceFromApp1 <= runwayLength && distanceFromApp2 <= runwayLength)
                {
                    found = "TS"+app;
                }

            //******************TST

            isTst = IsInPolygon(PointCoordinates, STstTopLeft);

                if (isTst==false)
                {
                    isTst = IsInPolygon(PointCoordinates, STstTopRight);
                }
                if (isTst == false)
                {
                    isTst = IsInPolygon(PointCoordinates, STstBottomLeft);
                }
                if (isTst == false)
                {
                    isTst = IsInPolygon(PointCoordinates, STstBottomRight);
                }

            //if (getY > strip && getY <= ((strip) + 315) && getX <= 2250 && getY >=getYY)
           
            if (isTst == true)
                {
                    found = "TST" + app;
                }
            //******************TST

            /*
                        double newyY = (45 - (getX * 0.04) / 0.20 + getYY);

                            if (getX <= 2250 && found == "" && getYY<getY )
                            {
                                found = "TST"+app;
                            }
            */

            if (found.Substring(0, 2) != "TS" && found.Substring(0, 3) != "RWY" && aRPDistance <= 15000)
                {
                    if (found.Substring(0, 3) == "APP")
                    {
                        found = "APP-IHS";
                    }
                    else
                    {
                        found = "IHS";
                    }

                }
             

            if (aRPDistance > 15000 && aRPDistance <= 3600)
            {
               
                    if (found.Substring(0, 3) == "APP")
                    {
                        found = "APP-CON";
                    }
                    else
                    {
                        found = "CON";
                    }
                
                }

                if (aRPDistance > 3600 && aRPDistance <= 13740)
                {
                
                    if (found.Substring(0, 3) == "APP")
                    {
                        found = "APP-OHS";
                    }
                    else
                    {
                        found = "OHS";
                    }
                 
            }

            surface = found;
            //}
 /*
            catch (Exception ex)
            {
                var st = new StackTrace(ex, true);
                // Get the top stack frame
                var frame = st.GetFrame(0);
                // Get the line number from the stack frame
                var line = frame.GetFileLineNumber();
                MessageBox.Show("Exception Message: " + ex.Message + " " + line);

            }
 */
        }

        private string WorkLatLong(string Easting, string Northing, int ZoneNo, out string Lat, out string Lng)
        {
            UniversalTransverseMercator utm = new UniversalTransverseMercator("N", ZoneNo, double.Parse(Easting), double.Parse(Northing));
            Coordinate c = UniversalTransverseMercator.ConvertUTMtoLatLong(utm);
            c.FormatOptions.Format = CoordinateFormatType.Decimal;
            c.FormatOptions.Round = 7;
            Lat = c.Latitude.ToString();  //N 2º 7' 2.332" E 6º 36' 12.653"
            Lng = c.Longitude.ToString();
            return Lat;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //25°01'07.5926"N	73°54'14.2403"E
            //25°01'24.5406"N	73°53'39.6233"E
            

            this.LatD2.Text = "19"; this.LatM2.Text = "10"; this.LatS2.Text = "57.83";
            this.LngD2.Text = "77";this.LngM2.Text = "18"; this.LngS2.Text = "52.98";
            this.LatD.Text = "19"; this.LatM.Text = "10"; this.LatS.Text = "48.46";
            this.LngD.Text = "77"; this.LngM.Text = "20"; this.LngS.Text = "11.08";

            //19° 10′ 55″ N, 77° 19′ 7″ E
            this.ArpLatD.Text = "19"; this.ArpLatM.Text = "10"; this.ArpLatS.Text = "55";
            this.ArpLngD.Text = "77"; this.ArpLngM.Text = "19"; this.ArpLngS.Text = "07";
            this.BasicStrip.Text = "60";this.RunwayStrip.Text = "140";this.Diversion.Text = "10";

            this.MaximumElevation.Text = "535.055";
            this.Rwy1Elevation.Text = "534.432";
            this.Rwy2Elevation.Text = "534.94";
            this.MaxApproachLength.Text = "15000";
            this.AppSection1.Text = "3000";
            this.AppSection2.Text = "6600";
            this.TSWidth.Text = "315";
            this.TSLength.Text = "2250";
            this.IHSWidth.Text = "6000";
            this.ConWidth.Text = "9100";
            this.OHSWidth.Text = "15000";


        }

        private void button6_Click(object sender, EventArgs e)
        {

            //Get Basic Strip Coordinates

            GetNewCoordinatesWithAngle(this.Easting.Text, this.Northing.Text, 60, double.Parse(this.BackBearing.Text), out double App1N, out double App1E);
            string App1BStripE = App1E.ToString(); string App1BStripN = App1N.ToString();

            //Get Basic Strip Coordinates

            GetNewCoordinatesWithAngle(this.Easting1.Text, this.Northing1.Text, 60, double.Parse(this.Bearing.Text), out App1N, out App1E);
            string App2BStripE = App1E.ToString(); string App2BStripN = App1N.ToString();

            //Get Center line Coordinates

            GetNewCoordinatesWithAngle(App1BStripE, App1BStripN, 15000, double.Parse(this.BackBearing.Text), out App1N, out App1E);
            string AppLineE = App1E.ToString(); string AppLineN = App1N.ToString();

            //Get Center line Coordinates

            GetNewCoordinatesWithAngle(App2BStripE, App2BStripN, 15000, double.Parse(this.Bearing.Text), out App1N, out App1E);
            string RAppLineE = App1E.ToString(); string RAppLineN = App1N.ToString();

            //Get Runway Strip
            GetNewCoordinatesWithAngle(this.Easting.Text, this.Northing.Text, double.Parse(this.RunwayStrip.Text) / 2, double.Parse(this.Bearing.Text) - 90,
                  out App1N, out App1E);

            string App1StripRightE = App1E.ToString();
            string App1StripRightN = App1N.ToString();


            GetNewCoordinatesWithAngle(this.Easting.Text, this.Northing.Text, double.Parse(this.RunwayStrip.Text) / 2, double.Parse(this.Bearing.Text) + 90,
                out App1N, out App1E);

            string App1StripLeftE = App1E.ToString();
            string App1StripLeftN = App1N.ToString();


            GetNewCoordinatesWithAngle(App1BStripE, App1BStripN, double.Parse(this.RunwayStrip.Text) / 2, double.Parse(this.Bearing.Text) - 90,
                  out App1N, out App1E);
            string App1BSLE = App1E.ToString();
            string App1BSLN = App1N.ToString();

            GetNewCoordinatesWithAngle(App1BStripE, App1BStripN, double.Parse(this.RunwayStrip.Text) / 2, double.Parse(this.Bearing.Text) + 90,
                 out App1N, out App1E);
            string App1BSRE = App1E.ToString();
            string App1BSRN = App1N.ToString();

            GetNewCoordinatesWithAngle(App2BStripE, App2BStripN, double.Parse(this.RunwayStrip.Text) / 2, double.Parse(this.Bearing.Text) - 90,
              out App1N, out App1E);
            string App2BSLE = App1E.ToString();
            string App2BSLN = App1N.ToString();

            GetNewCoordinatesWithAngle(App2BStripE, App2BStripN, double.Parse(this.RunwayStrip.Text) / 2, double.Parse(this.Bearing.Text) + 90,
                 out App1N, out App1E);
            string App2BSRE = App1E.ToString();
            string App2BSRN = App1N.ToString();

            GetNewCoordinatesWithAngle(App1BSLE, App1BSLN, 15000, double.Parse(this.BackBearing.Text) + 5.710, out App1N, out App1E);
            string AppUppCord1E = App1E.ToString();
            string AppUppCord1N = App1N.ToString();

            GetNewCoordinatesWithAngle(App1BSRE, App1BSRN, 15000, double.Parse(this.BackBearing.Text) - 5.710, out App1N, out App1E);
            string AppUppCord1ELeft = App1E.ToString();
            string AppUppCord1NLeft = App1N.ToString();

            GetNewCoordinatesWithAngle(App1BStripE, App1BStripN, 15000, double.Parse(this.BackBearing.Text), out App1N, out App1E);
            string AppUpperCLineE = App1E.ToString();
            string AppUpperCLineN = App1N.ToString();



            //*****************************
            GetNewCoordinatesWithAngle(App2BSLE, App2BSLN, 15000, double.Parse(this.Bearing.Text) - 5.710, out App1N, out App1E);
            string AppLwrCord1E = App1E.ToString();
            string AppLwrCord1N = App1N.ToString();

            GetNewCoordinatesWithAngle(App2BSRE, App2BSRN, 15000, double.Parse(this.Bearing.Text) + 5.710, out App1N, out App1E);
            string AppLwrCord1ELeft = App1E.ToString();
            string AppLwrCord1NLeft = App1N.ToString();

            GetNewCoordinatesWithAngle(App2BStripE, App2BStripN, 15000, double.Parse(this.Bearing.Text),
               out App1N, out App1E);
            string AppLwrCLineE = App1E.ToString();
            string AppLwrCLineN = App1N.ToString();

            //******************************
            GetNewCoordinatesWithAngle(App1BSLE, App1BSLN, 315, double.Parse(this.Bearing.Text) - 90,
              out App1N, out App1E);

            string App1TSLeftE = App1E.ToString();
            string App1TSLeftN = App1N.ToString();

            GetNewCoordinatesWithAngle(App1BSRE, App1BSRN, 315, double.Parse(this.Bearing.Text) + 90,
            out App1N, out App1E);

            string App1TSRightE = App1E.ToString();
            string App1TSRightN = App1N.ToString();

            GetNewCoordinatesWithAngle(App2BSLE, App2BSLN, 315, double.Parse(this.Bearing.Text) - 90,
            out App1N, out App1E);

            string App2TSLeftE = App1E.ToString();
            string App2TSLeftN = App1N.ToString();

            GetNewCoordinatesWithAngle(App2BSRE, App2BSRN, 315, double.Parse(this.Bearing.Text) + 90,
            out App1N, out App1E);

            string App2TSRightE = App1E.ToString();
            string App2TSRightN = App1N.ToString();

            GetNewCoordinatesWithAngle(App1BStripE, App1BStripN, 2250, double.Parse(this.BackBearing.Text),
        out App1N, out App1E);

            string App1TsPointE = App1E.ToString();
            string App1TsPointN = App1N.ToString();



            GetNewCoordinatesWithAngle(App2BStripE, App2BStripN, 2250, double.Parse(this.Bearing.Text),
        out App1N, out App1E);

            string App2TsPointE = App1E.ToString();
            string App2TsPointN = App1N.ToString();



            GetNewCoordinatesWithAngle(App1TsPointE, App1TsPointN, 152.5, double.Parse(this.BackBearing.Text) + 90, out App1N, out App1E);

            string App1TsFunnelLE = App1E.ToString();
            string App1TsFunnelLN = App1N.ToString();



            GetNewCoordinatesWithAngle(App2TsPointE, App2TsPointN, 152.5, double.Parse(this.Bearing.Text) - 90, out App1N, out App1E);

            string App2TsFunnelRE = App1E.ToString();
            string App2TsFunnelRN = App1N.ToString();



            GetNewCoordinatesWithAngle(App1TsPointE, App1TsPointN, 152.5, double.Parse(this.BackBearing.Text) - 90, out App1N, out App1E);

            string App1TsFunnelLLwrE = App1E.ToString();
            string App1TsFunnelLLwrN = App1N.ToString();



            GetNewCoordinatesWithAngle(App2TsPointE, App2TsPointN, 152.5, double.Parse(this.Bearing.Text) + 90,
      out App1N, out App1E);

            string App2TsFunnelRLwrE = App1E.ToString();
            string App2TsFunnelRLwrN = App1N.ToString();


            //Get Upper Approach line Coordinates
      //      GetNewCoordinatesWithAngle(App1StripRightE, App1StripRightN, 15000, double.Parse(this.BackBearing.Text) - 6,
       //           out App1N, out App1E); ;
        //    string AppBtnCord1E = App1E.ToString();
          //  string AppBtnCord1N = App1N.ToString();



            GetNewCoordinatesWithAngle(this.Easting1.Text, this.Northing1.Text,
                   double.Parse(this.RunwayStrip.Text) / 2, double.Parse(this.BackBearing.Text) - 90,
                   out App1N, out App1E);

          string App2StripRightE = App1E.ToString();
          string App2StripRightN = App1N.ToString();



            GetNewCoordinatesWithAngle(this.Easting1.Text, this.Northing1.Text,
                   double.Parse(this.RunwayStrip.Text) / 2, double.Parse(this.BackBearing.Text) + 90,
                   out App1N, out App1E);

           // this.textBox8.Text = App1E.ToString();
           // App2StripLeftN = App1N.ToString();
           // App2StripLeftE = App1E.ToString();
            //App2StripLeftN = App1N.ToString();



            STstTopLeft = App1TsFunnelLE + "," + App1TsFunnelLN + ";" + App1TSLeftE + "," + App1TSLeftN + ";" + App1BSLE + "," + App1BSLN;
              STstTopRight = App2TSLeftE + "," + App2TSLeftN + ";" + App2BSLE + "," + App2BSLN + ";" + App2TsFunnelRE + "," + App2TsFunnelRN;
              STstBottomRight = App2BSRE + "," + App2BSRN + ";" + App2TSRightE + "," + App2TSRightN + ";" + App2TsFunnelRLwrE + "," + App2TsFunnelRLwrN;
              STstBottomLeft = App1BSRE + "," + App1BSRN + ";" + App1TSRightE + "," + App1TSRightN + ";" + App1TsFunnelLLwrE + "," + App1TsFunnelLLwrN;




            GetObstacles(null, null);
            
        }

        private void GetNewCoordinatesWithAngle(string He, string Hn, double Distance,
         double Bearing, out double App1N, out double App1E)
        {
            App1E = 0;
            //App2E = 0;
            App1N = 0;
            //App2N = 0;
            //double ReverseBear = 0;
            double SinX = 0;
            double CosX = 0;


            if (Bearing > 0)
            {
                double bearRadian = Math.Round(Bearing * (Math.PI) / 180, 3);
                CosX = Math.Round(Distance * (Math.Cos(bearRadian)), 3);
                SinX = Math.Round(Distance * (Math.Sin(bearRadian)), 3);

                App1E = Math.Round(double.Parse(He) + SinX, 3);
                App1N = Math.Round(double.Parse(Hn) + CosX, 3);
                //    ReverseBear = 0;
                //   ReverseBear = Math.Round(BackBearing, 2);

                // bearRadian = Math.Round(Bearing * (Math.PI) / 180, 2);
                //CosX = Distance * (Math.Cos(bearRadian));
                //SinX = Distance * (Math.Sin(bearRadian));

                //     App2E = Math.Round(double.Parse(He1) + SinX, 2);
                //   App2N = Math.Round(double.Parse(Hn1) + CosX, 2);
            }


        }
        private string SetApp(double Bear, out string NewBearing)
        {
             NewBearing = "";
           
            double B = Math.Truncate(Math.Round(Bear / 10));
            
            var Runway = B > 9 ? B : Math.Truncate(Bear);


            if (Runway <= 9)
            {
                NewBearing = "APP" + "0" + Runway.ToString();
            }

            if (Runway > 9 && Runway <= 99)
            {
                NewBearing = "APP" + Runway.ToString();
            }

          //  if (Bear > 99)
        //    {
          //      NewBearing = "APP" + Bear.ToString().Substring(0, 2);
         //   }


            return NewBearing;
        }

        private double getNearer(double getX, double RwyLgth, double d1, double d2, out double NearestDistance)
        {
            NearestDistance = 0;
            double xMinus60, rwy;
            xMinus60 = Math.Round(getX,0) - 60;
            rwy = Math.Round(RwyLgth,0) / 2;

            if (d1 > d2)
            { 
                xMinus60 = Math.Round(RwyLgth,0) - Math.Round( xMinus60,0); 
            }

            double intNear = Math.Truncate( ((xMinus60) / 30)) * 30;
            double difference = Math.Abs(xMinus60 - intNear);
            if(difference <= 15) 
            {
                NearestDistance = intNear;
            }else
            {
                NearestDistance = intNear+30;
            } 
            
            return NearestDistance;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            CreateShapeFiles();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            CreateShape frmd = new CreateShape();
            frmd.Controls["App1North"].Text = this.Northing.Text;
            frmd.Controls["App1East"].Text = this.Easting.Text;
            frmd.Controls["App2North"].Text = this.Northing1.Text;
            frmd.Controls["App2East"].Text = this.Easting1.Text;
            frmd.Controls["Bearing"].Text = this.Bearing.Text;
            frmd.Controls["BackBearing"].Text = this.BackBearing.Text;
            frmd.Controls["BasicStrip"].Text = this.BasicStrip.Text;
            frmd.Controls["RunwayStrip"].Text = this.RunwayStrip.Text;
            frmd.Controls["ArpEast"].Text = this.ArpEast.Text;
            frmd.Controls["ArpNorth"].Text = this.ArpNorth.Text;
            frmd.Controls["Zone"].Text = this.Zone.Text;
            frmd.Show();


        }

        public void CreateShapeFiles()
        {
            double MaxApproach = double.Parse(this.MaxApproachLength.Text);
            double Tswidth = double.Parse(this.TSWidth.Text);
            double IHSCircle = double.Parse(this.IHSWidth.Text);
            double ConCircle = double.Parse(this.ConWidth.Text);
            double OHSCirvle = double.Parse(this.ConWidth.Text);
            double Tslength = double.Parse(this.TSLength.Text);
            double TsFunnel = Tslength * (double.Parse(this.Diversion.Text) / 100) + (double.Parse(this.RunwayStrip.Text) / 2);
            double DiversionTan=6;
            
            
            if ( this.Diversion.Text  == "10") 
            {
                DiversionTan = 6;
            }else 
            {
                DiversionTan = 9;
             }
            

            ;            string CDir = Directory.GetCurrentDirectory() + "\\Shapefiles\\";
            int myPointIndex = 0;
            System.IO.DirectoryInfo di = new DirectoryInfo(CDir);

            foreach (FileInfo file in di.GetFiles())
            {
                file.Delete();
            }

            Shapefile myShapefile = new Shapefile();
            //Define the path of the new shapefile and geometry type
            myShapefile.CreateNew(@CDir + "\\SurveyPoints.shp", ShpfileType.SHP_POINT);
            //Create new field
            MapWinGIS.Field myField = new Field();
            
            var objindex = myShapefile.EditAddField("Obj No.", FieldType.STRING_FIELD, 0, 4);
            var objnameindex = myShapefile.EditAddField("Obj Name", FieldType.STRING_FIELD, 0, 25);
            var ElevationIndex = myShapefile.EditAddField("Elevation", FieldType.DOUBLE_FIELD, 2, 5);
            var PE_Elev_TSAPPRWY= myShapefile.EditAddField("PE_Elev_TS-App-Rwy", FieldType.DOUBLE_FIELD, 2, 5);
            var PE_Elev_IHSConOhs = myShapefile.EditAddField("PE_Elev_IHS-Con-OHS", FieldType.DOUBLE_FIELD, 2, 5);
            var PE_Elev_Approach = myShapefile.EditAddField("PE_Elev_Approach", FieldType.DOUBLE_FIELD, 2, 5);
            var PE_Elev_AppIhs_AppCon = myShapefile.EditAddField("PE_Elev_APP-Ihs_App-Con", FieldType.DOUBLE_FIELD, 2, 5);
            var Obst_Rwy_Ts_App = myShapefile.EditAddField("Obst_Rwy_Ts_App ", FieldType.DOUBLE_FIELD, 2, 5);
            var Obst_IHS_Con_OHS_App = myShapefile.EditAddField("Obst_IHS_Con_OHS_App", FieldType.DOUBLE_FIELD, 2, 5);

            //Add the filed for the shapefile table
            int intFieldIndex = 0;
            int myShapeIndex = 0;

            string OledbConnectString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=ObstaclesData.accdb";

            using (OleDbConnection connection = new OleDbConnection(OledbConnectString))

            {
                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }

                OleDbCommand cmd = new OleDbCommand("Select * from RWYSurveyData  ", connection);

                {

                    using (OleDbDataReader reader = cmd.ExecuteReader())
                    {

                        while (reader.Read())
                        {
                          
                            //Create Point Shape
                        
                            int i = 1;
                            int j = 0;
                            int shpIndex;

                            
                            MapWinGIS.Shape myShape = new MapWinGIS.Shape();
                            myShape.Create(ShpfileType.SHP_POINT);
                            MapWinGIS.Point myPoint = new Point();
                            myPoint.x = Math.Round(double.Parse(reader["Easting"].ToString()), 2);
                            myPoint.y = Math.Round(double.Parse(reader["Northing"].ToString()), 2);
                            myShape.InsertPoint(myPoint, ref myPointIndex);
                            shpIndex = i - 1;
                            myShapefile.EditInsertShape(myShape, ref shpIndex);
                            myShapefile.EditCellValue(objindex, shpIndex, reader["SL No"].ToString());
                            myShapefile.EditCellValue(objnameindex, shpIndex, reader["Object"].ToString());
                            myShapefile.EditCellValue(ElevationIndex, shpIndex, reader["Elevation"].ToString());
                            myShapefile.EditCellValue(PE_Elev_TSAPPRWY, shpIndex, reader["PElevTSAPPRWY"].ToString());
                            myShapefile.EditCellValue(PE_Elev_IHSConOhs, shpIndex, reader["PElevIHSCONOHS"].ToString());
                            myShapefile.EditCellValue(PE_Elev_Approach, shpIndex, reader["PEElevApproach"].ToString());
                            myShapefile.EditCellValue(PE_Elev_AppIhs_AppCon, shpIndex, reader["PElevAPP-IHSAPP-CON"].ToString());
                            myShapefile.EditCellValue(Obst_Rwy_Ts_App, shpIndex, reader["ObstRwyTSApp"].ToString());
                            myShapefile.EditCellValue(Obst_IHS_Con_OHS_App, shpIndex, reader["ObstIHSCONOHSAPPIHSAPPCON"].ToString());

                            myPointIndex++;
                            i++;
                        }


                        myShapefile.StopEditingShapes(true, true, null);
                        myShapefile.Close();
                        List<string> Coord = new List<string>();

                        string head = "Name, Easting,Name,Northing";
                        Coord.Add(head);


                        string App1StripRightE, App1StripRightN, App1StripLeftE, App1StripLeftN,
                            App2StripRightE, App2StripRightN, App2StripLeftE, App2StripLeftN;


                        //Get Basic Strip Coordinates

                        GetNewCoordinatesWithAngle(this.Easting.Text, this.Northing.Text, 60,
                            double.Parse(this.BackBearing.Text), out double App1N, out double App1E);
                        string App1BStripE = App1E.ToString(); string App1BStripN = App1N.ToString();

                        Coord.Add("App1BStripE" + "," + App1E.ToString() + "," + "App1BStripN" + "," + App1N.ToString());

                        //Get Basic Strip Coordinates

                        GetNewCoordinatesWithAngle(this.Easting1.Text, this.Northing1.Text, 60, double.Parse(this.Bearing.Text),
                            out App1N, out App1E);

                        string App2BStripE = App1E.ToString(); string App2BStripN = App1N.ToString();
                        Coord.Add("App2BStripE " + "," + App1E.ToString() + "," + "App2BStripN" + "," + App1N.ToString());

                        //Get Center line Coordinates

                        GetNewCoordinatesWithAngle(App1BStripE, App1BStripN, MaxApproach, double.Parse(this.BackBearing.Text), out App1N, out App1E);
                        string AppLineE = App1E.ToString(); string AppLineN = App1N.ToString();
                        Coord.Add("AppLineE" + "," + App1E.ToString() + "," + "AppLineN" + "," + App1N.ToString());
                        //Get Center line Coordinates

                        GetNewCoordinatesWithAngle(App2BStripE, App2BStripN, MaxApproach, double.Parse(this.Bearing.Text), out App1N, out App1E);
                        string RAppLineE = App1E.ToString(); string RAppLineN = App1N.ToString();
                        Coord.Add("RAppLineE" + "," + App1E.ToString() + "," + "RAppLineN" + "," + App1N.ToString());
                        //Get Runway Strip
                        GetNewCoordinatesWithAngle(this.Easting.Text, this.Northing.Text,
                              double.Parse(this.RunwayStrip.Text) / 2, double.Parse(this.Bearing.Text) - 90,
                              out App1N, out App1E);
                       
                        App1StripRightE = App1E.ToString();
                        App1StripRightN = App1N.ToString();

                        Coord.Add("App1StripRightE" + "," + App1E.ToString() + "," + "App1StripRightN" + "," + App1N.ToString());

                        GetNewCoordinatesWithAngle(this.Easting.Text, this.Northing.Text,
                            double.Parse(this.RunwayStrip.Text) / 2, double.Parse(this.Bearing.Text) + 90,
                            out App1N, out App1E);

                       // App1StripLeftE = App1E.ToString();
                        //App1StripLeftN = App1N.ToString();

                        App1StripLeftE = App1E.ToString();
                        App1StripLeftN = App1N.ToString();
                        Coord.Add("App1StripLeftE" + "," + App1E.ToString() + "," + "App1StripLeftN" + "," + App1N.ToString());

                        GetNewCoordinatesWithAngle(App1BStripE, App1BStripN,
                              double.Parse(this.RunwayStrip.Text) / 2, double.Parse(this.Bearing.Text) - 90,
                              out App1N, out App1E);
                        string App1BSLE = App1E.ToString();
                        string App1BSLN = App1N.ToString();

                        Coord.Add("App1BSLE" + "," + App1E.ToString() + "," + "App1BSLN" + "," + App1N.ToString());

                        GetNewCoordinatesWithAngle(App1BStripE, App1BStripN,
                             double.Parse(this.RunwayStrip.Text) / 2, double.Parse(this.Bearing.Text) + 90,
                             out App1N, out App1E);
                        string App1BSRE = App1E.ToString();
                        string App1BSRN = App1N.ToString();

                        Coord.Add("App1BSRE" + "," + App1E.ToString() + "," + "App1BSRN" + "," + App1N.ToString());


                        GetNewCoordinatesWithAngle(App2BStripE, App2BStripN,
                          double.Parse(this.RunwayStrip.Text) / 2, double.Parse(this.Bearing.Text) - 90,
                          out App1N, out App1E);
                        string App2BSLE = App1E.ToString();
                        string App2BSLN = App1N.ToString();

                        Coord.Add("App2BSLE" + "," + App1E.ToString() + "," + "App2BSLN" + "," + App1N.ToString());

                        GetNewCoordinatesWithAngle(App2BStripE, App2BStripN,
                             double.Parse(this.RunwayStrip.Text) / 2, double.Parse(this.Bearing.Text) + 90,
                             out App1N, out App1E);
                        string App2BSRE = App1E.ToString();
                        string App2BSRN = App1N.ToString();

                        Coord.Add("App2BSRE" + "," + App1E.ToString() + "," + "App2BSRN" + "," + App1N.ToString());

                        GetNewCoordinatesWithAngle(App1BSLE, App1BSLN, MaxApproach, double.Parse(this.BackBearing.Text) + 5.710,
                              out App1N, out App1E);
                        string AppUppCord1E = App1E.ToString();
                        string AppUppCord1N = App1N.ToString();

                        Coord.Add("AppUppCord1E" + "," + App1E.ToString() + "," + "AppUppCord1N" + "," + App1N.ToString());

                        GetNewCoordinatesWithAngle(App1BSRE, App1BSRN, MaxApproach, double.Parse(this.BackBearing.Text) - 5.710,
                            out App1N, out App1E);
                        string AppUppCord1ELeft = App1E.ToString();
                        string AppUppCord1NLeft = App1N.ToString();

                        Coord.Add("AppUppCord1ELeft" + "," + App1E.ToString() + "," + "AppUppCord1NLeft" + "," + App1N.ToString());

                        //                GetNewCoordinatesWithAngle(App1BSRE, App1BSRN, 15000, double.Parse(this.BackBearing.Text) ,
                        GetNewCoordinatesWithAngle(App1BStripE, App1BStripN, MaxApproach, double.Parse(this.BackBearing.Text),
                           out App1N, out App1E);
                        string AppUpperCLineE = App1E.ToString();
                        string AppUpperCLineN = App1N.ToString();

                        Coord.Add("AppUpperCLineE" + "," + App1E.ToString() + "," + "AppUpperCLineN" + "," + App1N.ToString());

                        //*****************************
                        GetNewCoordinatesWithAngle(App2BSLE, App2BSLN, MaxApproach, double.Parse(this.Bearing.Text) - 5.710,
                            out App1N, out App1E);
                        string AppLwrCord1E = App1E.ToString();
                        string AppLwrCord1N = App1N.ToString();

                        Coord.Add("AppLwrCord1E" + "," + App1E.ToString() + "," + "AppLwrCord1N" + "," + App1N.ToString());

                        GetNewCoordinatesWithAngle(App2BSRE, App2BSRN, MaxApproach, double.Parse(this.Bearing.Text) + 5.710,
                            out App1N, out App1E);
                        string AppLwrCord1ELeft = App1E.ToString();
                        string AppLwrCord1NLeft = App1N.ToString();

                        Coord.Add("AppLwrCord1ELeft" + "," + App1E.ToString() + "," + "AppLwrCord1NLeft" + "," + App1N.ToString());

                        GetNewCoordinatesWithAngle(App2BStripE, App2BStripN, MaxApproach, double.Parse(this.Bearing.Text),
                           out App1N, out App1E);
                        string AppLwrCLineE = App1E.ToString();
                        string AppLwrCLineN = App1N.ToString();

                        Coord.Add("AppLwrCLineE" + "," + App1E.ToString() + "," + "AppLwrCLineN" + "," + App1N.ToString());

                        //******************************
                        GetNewCoordinatesWithAngle(App1BSLE, App1BSLN, Tswidth, double.Parse(this.Bearing.Text) - 90,
                          out App1N, out App1E);

                        string App1TSLeftE = App1E.ToString();
                        string App1TSLeftN = App1N.ToString();

                        Coord.Add("App1TSLeftE" + "," + App1E.ToString() + "," + "App1TSLeftN" + "," + App1N.ToString());

                        GetNewCoordinatesWithAngle(App1BSRE, App1BSRN, Tswidth, double.Parse(this.Bearing.Text) + 90,
                        out App1N, out App1E);

                        string App1TSRightE = App1E.ToString();
                        string App1TSRightN = App1N.ToString();

                        Coord.Add("App1TSRightE" + "," + App1E.ToString() + "," + "App1TSRightN" + "," + App1N.ToString());

                        GetNewCoordinatesWithAngle(App2BSLE, App2BSLN, Tswidth, double.Parse(this.Bearing.Text) - 90,
                         out App1N, out App1E);

                        string App2TSLeftE = App1E.ToString();
                        string App2TSLeftN = App1N.ToString();

                        Coord.Add("App2TSLeftE" + "," + App1E.ToString() + "," + "App2TSLeftN" + "," + App1N.ToString());

                        GetNewCoordinatesWithAngle(App2BSRE, App2BSRN, Tswidth, double.Parse(this.Bearing.Text) + 90,
                        out App1N, out App1E);

                        string App2TSRightE = App1E.ToString();
                        string App2TSRightN = App1N.ToString();

                        Coord.Add("App2TSRightE" + "," + App1E.ToString() + "," + "App2TSRightN" + "," + App1N.ToString());

                        GetNewCoordinatesWithAngle(App1BStripE, App1BStripN, Tslength, double.Parse(this.BackBearing.Text),
                    out App1N, out App1E);

                        string App1TsPointE = App1E.ToString();
                        string App1TsPointN = App1N.ToString();

                        Coord.Add("App1TsPointE" + "," + App1E.ToString() + "," + "App1TsPointN" + "," + App1N.ToString());

                        GetNewCoordinatesWithAngle(App2BStripE, App2BStripN, Tslength, double.Parse(this.Bearing.Text),
                    out App1N, out App1E);

                        string App2TsPointE = App1E.ToString();
                        string App2TsPointN = App1N.ToString();

                        Coord.Add("App2TsPointE" + "," + App1E.ToString() + "," + "App2TsPointN" + "," + App1N.ToString());

                        GetNewCoordinatesWithAngle(App1TsPointE, App1TsPointN, TsFunnel, double.Parse(this.BackBearing.Text) + 90, out App1N, out App1E);

                        string App1TsFunnelLE = App1E.ToString();
                        string App1TsFunnelLN = App1N.ToString();

                        Coord.Add("App1TsFunnelLE" + "," + App1E.ToString() + "," + "App1TsFunnelLN" + "," + App1N.ToString());

                        GetNewCoordinatesWithAngle(App2TsPointE, App2TsPointN, TsFunnel, double.Parse(this.Bearing.Text) - 90, out App1N, out App1E);

                        string App2TsFunnelRE = App1E.ToString();
                        string App2TsFunnelRN = App1N.ToString();

                        Coord.Add("App2TsFunnelRE" + "," + App1E.ToString() + "," + "App2TsFunnelRN" + "," + App1N.ToString());

                        GetNewCoordinatesWithAngle(App1TsPointE, App1TsPointN, TsFunnel, double.Parse(this.BackBearing.Text) - 90, out App1N, out App1E);

                        string App1TsFunnelLLwrE = App1E.ToString();
                        string App1TsFunnelLLwrN = App1N.ToString();

                        Coord.Add("App1TsFunnelLLwrE" + "," + App1E.ToString() + "," + "App1TsFunnelLLwrN" + "," + App1N.ToString());

                        GetNewCoordinatesWithAngle(App2TsPointE, App2TsPointN, TsFunnel, double.Parse(this.Bearing.Text) + 90,
                  out App1N, out App1E);

                        string App2TsFunnelRLwrE = App1E.ToString();
                        string App2TsFunnelRLwrN = App1N.ToString();
                        Coord.Add("App2TsFunnelRLwrE" + "," + App1E.ToString() + "," + "App2TsFunnelRLwrN" + "," + App1N.ToString());

                        //Get Upper Approach line Coordinates
                        GetNewCoordinatesWithAngle(App1StripRightE, App1StripRightN, MaxApproach, double.Parse(this.BackBearing.Text) - 6,
                              out App1N, out App1E); ;
                        string AppBtnCord1E = App1E.ToString();
                        string AppBtnCord1N = App1N.ToString();
                        Coord.Add("AppBtnCord1E " + "," + App1E.ToString() + "," + "AppBtnCord1N" + "," + App1N.ToString());


                        GetNewCoordinatesWithAngle(this.Easting1.Text, this.Northing1.Text,
                               double.Parse(this.RunwayStrip.Text) / 2, double.Parse(this.BackBearing.Text) - 90,
                               out App1N, out App1E);

                        //App2StripRightE = App1E.ToString();
                        //App2StripRightN = App1N.ToString();
                        App2StripRightE = App1E.ToString();
                        App2StripRightN = App1N.ToString();

                        Coord.Add("App2StripRightE" + "," + App1E.ToString() + "," + "App2StripRightN" + "," + App1N.ToString());

                        GetNewCoordinatesWithAngle(this.Easting1.Text, this.Northing1.Text,
                               double.Parse(this.RunwayStrip.Text) / 2, double.Parse(this.BackBearing.Text) + 90,
                               out App1N, out App1E);

                      //App2StripLeftE = App1E.ToString();
                        //App2StripLeftN = App1N.ToString();
                        App2StripLeftE = App1E.ToString();
                        App2StripLeftN = App1N.ToString();

                        Coord.Add("App2StripLeftE" + "," + App1E.ToString() + "," + "App2StripLeftN" + "," + App1N.ToString());

                        string[] Tlines = { "" };
                        File.WriteAllLines("Coordinates.txt", Tlines);
                        using (TextWriter tw = new StreamWriter("Coordinates.txt"))
                        {
                            foreach (String s in Coord)
                                tw.WriteLine(s);
                        }
                        //  System.IO.File.WriteAllLines(@CDir + "\\PolyCoordinates.txt" + "Coordinates.txt", List. Lists.Coords);

                        Shapefile PolygonShape = new Shapefile();
                        PolygonShape.CreateNew(@CDir + "\\TstPolygon.shp", ShpfileType.SHP_POLYGON);

                        //Make TST polygon

                        string TriangleTopLeft = App1TsFunnelLE + "," + App1TsFunnelLN + ";" +
                            App1TSLeftE + "," + App1TSLeftN + ";" + App1BSLE + "," + App1BSLN;

                        string RectangleTop = App1BSLE + "," + App1BSLN + ";" + App2BSLE + "," + App2BSLN
                            + ";" + App2TSLeftE + "," + App2TSLeftN + ";" + App1TSLeftE + "," + App1TSLeftN;

                        string TriangleTopRight = App2TSLeftE + "," + App2TSLeftN + ";" + App2BSLE + "," + App2BSLN
                           + ";" + App2TsFunnelRE + "," + App2TsFunnelRN;

                        string TriangleBottomRight = App2BSRE + "," + App2BSRN + ";" + App2TSRightE + "," + App2TSRightN + ";"
                            + App2TsFunnelRLwrE + "," + App2TsFunnelRLwrN;


                        string TriangleBottomLeft = App1BSRE + "," + App1BSRN + ";" + App1TSRightE + "," + App1TSRightN + ";"
                           + App1TsFunnelLLwrE + "," + App1TsFunnelLLwrN;

                        string RectangleBottom = App1BSRE + "," + App1BSRN + ";" + App2BSRE + "," + App2BSRN
                            + ";" + App2TSRightE + "," + App2TSRightN + ";" + App1TSRightE + "," + App1TSRightN;

                         
                        string RunwayStrip = App1StripRightE + "," + App1StripRightN + ";" + App1StripLeftE + "," + App1StripLeftN
                            + ";" + App2StripRightE + "," + App2StripRightN + ";" + App2StripLeftE + "," + App2StripLeftN;

                        String leftApporach = App1BSLE + "," + App1BSLN + ";" + AppUppCord1E + "," + AppUppCord1N
                            + ";" + AppUppCord1ELeft + "," + AppUppCord1NLeft + ";" + App1BSRE + "," + App1BSRN;

                        string rightApproach = App2BSLE + "," + App2BSLN + ";" + AppLwrCord1E + "," + AppLwrCord1N + ";" +
                            AppLwrCord1ELeft + "," + AppLwrCord1NLeft + ";" + App2BSRE + "," + App2BSRN;

                       MapWinGIS.Shape PlgLeftApproach = new MapWinGIS.Shape();
                        PlgLeftApproach.Create(PolygonShape.ShapefileType);
                        MakePolygon(leftApporach, myPointIndex, myShapeIndex, PolygonShape, PlgLeftApproach);

                        MapWinGIS.Shape PlgRightApproach = new MapWinGIS.Shape();
                        PlgRightApproach.Create(PolygonShape.ShapefileType);
                        MakePolygon(rightApproach, myPointIndex, myShapeIndex, PolygonShape, PlgRightApproach);

                        MapWinGIS.Shape PlgTriangleLeft = new MapWinGIS.Shape();
                        PlgTriangleLeft.Create(PolygonShape.ShapefileType);
                        MakePolygon(TriangleTopLeft, myPointIndex, myShapeIndex, PolygonShape, PlgTriangleLeft);

                        MapWinGIS.Shape PlgnRectTop = new MapWinGIS.Shape();
                        PlgnRectTop.Create(PolygonShape.ShapefileType);
                        MakePolygon(RectangleTop, myPointIndex, myShapeIndex, PolygonShape, PlgnRectTop);

                        MapWinGIS.Shape PlgTringleTopRight = new MapWinGIS.Shape();
                        PlgTringleTopRight.Create(PolygonShape.ShapefileType);
                        MakePolygon(TriangleTopRight, myPointIndex, myShapeIndex, PolygonShape, PlgTringleTopRight);

                        MapWinGIS.Shape PlgTringleBottomRight = new MapWinGIS.Shape();
                        PlgTringleBottomRight.Create(PolygonShape.ShapefileType);
                        MakePolygon(TriangleBottomRight, myPointIndex, myShapeIndex, PolygonShape, PlgTringleBottomRight);

                        MapWinGIS.Shape PlgRectangleBottom = new MapWinGIS.Shape();
                        PlgRectangleBottom.Create(PolygonShape.ShapefileType);
                        MakePolygon(RectangleBottom, myPointIndex, myShapeIndex, PolygonShape, PlgRectangleBottom);

                        MapWinGIS.Shape Runway = new MapWinGIS.Shape();
                        Runway.Create(PolygonShape.ShapefileType);
                        MakePolygon(RunwayStrip, myPointIndex, myShapeIndex, PolygonShape, Runway);

                        MapWinGIS.Shape PlgTringleBottomLeft = new MapWinGIS.Shape();
                        PlgTringleBottomLeft.Create(PolygonShape.ShapefileType);
                        MakePolygon(TriangleBottomLeft, myPointIndex, myShapeIndex, PolygonShape, PlgTringleBottomLeft);

                        string App1ApplineL = App1BSRE + "," + App1BSRN + ";" + AppUppCord1ELeft + "," + AppUppCord1NLeft;

                        string Bs1 = App2StripRightE + "," + App2StripRightN + ";" + App2StripLeftE + "," +
                            App2StripLeftN + ";" + App2BSLE + "," + App2BSLN + ";" + App2BSRE + "," + App2BSRN;

                        string Bs2 = App1StripLeftE + "," + App1StripLeftN + ";" + App1BSRE + "," + App1BSRN + ";" +
                        App1BSLE + "," + App1BSLN + ";" + App1StripRightE + "," + App1StripRightN;

                        MapWinGIS.Shape ShapeBs1 = new MapWinGIS.Shape();
                        ShapeBs1.Create(PolygonShape.ShapefileType);
                        MakePolygon(Bs1, myPointIndex, myShapeIndex, PolygonShape, ShapeBs1);

                        MapWinGIS.Shape ShapeBs2 = new MapWinGIS.Shape();
                        ShapeBs2.Create(PolygonShape.ShapefileType);
                        MakePolygon(Bs2, myPointIndex, myShapeIndex, PolygonShape, ShapeBs2);

                        PolygonShape.StopEditingShapes(true, true, null);
                        PolygonShape.Close();

                        string PlyCentreLine = AppUpperCLineE + "," + AppUpperCLineN + ";" + AppLwrCLineE + "," + AppLwrCLineN;
                        Shapefile poly = new Shapefile();
                        poly.CreateNew(@CDir + "\\Polyline.shp", ShpfileType.SHP_POLYLINE);// ShpfileType.SHP_POLYLINE);
                        MapWinGIS.Shape pPolyline = new MapWinGIS.Shape();
                        pPolyline.Create(poly.ShapefileType);

                        MakePolyline(PlyCentreLine, myPointIndex, myShapeIndex, poly, pPolyline, out int PointIndex, out int ShapeIndex);
                        
                        poly.StopEditingShapes(true, true, null);
                        poly.Close();

                        MakeIHSPolygonShape();
                        MakeCONPolygonShape();

                        //   axMap1.DrawCircle(double.Parse(this.Easting.Text), double.Parse(this.Northing.Text), 15000, 1, false);
                        MessageBox.Show("Done");
                        //
                        string utm = "PROJCS[WGS_84_UTM_zone_" + this.Zone.Text + "N";
                        string ProjFile = utm + ",GEOGCS['GCS_WGS_1984',DATUM['D_WGS84',SPHEROID['WGS84',6378137,298.257223563]],PRIMEM['Greenwich',0],UNIT['Degree',0.017453292519943295]],PROJECTION['Transverse_Mercator'],PARAMETER['latitude_of_origin',0],PARAMETER['central_meridian',75],PARAMETER['scale_factor',0.9996],PARAMETER['false_easting',500000],PARAMETER['false_northing',0],UNIT['Meter',1]]";
                        File.WriteAllText(@CDir + "\\SurveyPoints.prj", ProjFile);
                        File.WriteAllText(@CDir + "\\Polyline.prj", ProjFile);
                        File.WriteAllText(@CDir + "\\ConPolygon.prj", ProjFile);
                        File.WriteAllText(@CDir + "\\ArpPolygon.prj", ProjFile);
                        File.WriteAllText(@CDir + "\\TstPolygon.prj", ProjFile);
                        //
                        DisplayShapes ds = new DisplayShapes();
                        ds.Show();



                    }

                }
            }
        }
        private void MakePolyline(string Coordinates, int myPointIndex, int myShapeIndex,
         Shapefile poly, MapWinGIS.Shape pPolyline, out int PointIndex, out int ShapeIndex)
        {
            try
            {
                string[] vpoints = Coordinates.Split(';');

                foreach (string vpoint in vpoints)
                {
                    MapWinGIS.Point PolyPoint = new Point();
                    // ShpfileType.SHP_POINT);
                    var vp = vpoint.Split(',');
                    PolyPoint.x = Convert.ToDouble(vp[0]);
                    PolyPoint.y = Convert.ToDouble(vp[1]);
                    pPolyline.InsertPoint(PolyPoint, ref myPointIndex);
                    poly.EditInsertShape(pPolyline, ref myShapeIndex);
                    myPointIndex++;
                    myShapeIndex++;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            var shapeindex = poly.EditAddShape(pPolyline);

            PointIndex = myPointIndex;
            ShapeIndex = myShapeIndex;

        }

        private void MakePolygon(string Coordinates, int myPointIndex, int myShapeIndex,
            Shapefile poly, MapWinGIS.Shape pPolyline)
        {
            try
            {
                string[] vpoints = Coordinates.Split(';');

                foreach (string vpoint in vpoints)
                {
                    MapWinGIS.Point PolyPoint = new Point();
                    // ShpfileType.SHP_POINT);
                    var vp = vpoint.Split(',');
                    PolyPoint.x = Convert.ToDouble(vp[0]);
                    PolyPoint.y = Convert.ToDouble(vp[1]);
                    pPolyline.InsertPoint(PolyPoint, ref myPointIndex);
                    poly.EditInsertShape(pPolyline, ref myShapeIndex);
                    myPointIndex++;
                    myShapeIndex++;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            var shapeindex = poly.EditAddShape(pPolyline);



        }

        private void MakeIHSPolygonShape()
        {
            string CDir = CurrentDir + "\\Shapefiles\\";
            Shapefile shpPloygon = new Shapefile();
            shpPloygon.CreateNew(@CDir + "\\ArpPolygon.shp", ShpfileType.SHP_POLYGON);
            int fldX = shpPloygon.EditAddField("X", FieldType.DOUBLE_FIELD, 9, 12);
            int fldY = shpPloygon.EditAddField("Y", FieldType.DOUBLE_FIELD, 9, 12);
            int fldArea = shpPloygon.EditAddField("area", FieldType.DOUBLE_FIELD, 9, 12);


            double ArpCentreE = double.Parse(this.ArpEast.Text);
            double ArpCentreN = double.Parse(this.ArpNorth.Text);
            double ArpRadius = 4000;
            int myPointIndex = 0;
            int myShapeIndex = 0;
            try
            {
               MapWinGIS.Shape shp = new MapWinGIS.Shape();
                shp.Create(ShpfileType.SHP_POLYGON);

                for (int i = 0; i <= 37; i++)
                {

                    MapWinGIS.Point pnt = new Point();
                    pnt.x = ArpCentreE + ArpRadius * Math.Cos(i * Math.PI / 18);
                    pnt.y = ArpCentreN - ArpRadius * Math.Sin(i * Math.PI / 18);
                    shp.InsertPoint(pnt, ref myPointIndex);
                    shpPloygon.EditInsertShape(shp, ref myShapeIndex);
                    //   myPointIndex++;
                    //  myShapeIndex++;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            shpPloygon.StopEditingShapes(true, true, null);
            shpPloygon.Close();

        }
        private void MakeCONPolygonShape()
        {
            string CDir = CurrentDir + "\\Shapefiles\\";
            Shapefile shpPloygon = new Shapefile();
            shpPloygon.CreateNew(CDir + "\\ConPolygon.shp", ShpfileType.SHP_POLYGON);
            int fldX = shpPloygon.EditAddField("X", FieldType.DOUBLE_FIELD, 9, 12);
            int fldY = shpPloygon.EditAddField("Y", FieldType.DOUBLE_FIELD, 9, 12);
            int fldArea = shpPloygon.EditAddField("area", FieldType.DOUBLE_FIELD, 9, 12);


            double ArpCentreE = double.Parse(this.ArpEast.Text);
            double ArpCentreN = double.Parse(this.ArpNorth.Text);
            double ArpRadius = 2700;
            int myPointIndex = 0;
            int myShapeIndex = 0;
            try
            {
                MapWinGIS.Shape shp = new MapWinGIS.Shape();
                shp.Create(ShpfileType.SHP_POLYGON);

                for (int i = 0; i <= 37; i++)
                {

                    MapWinGIS.Point pnt = new Point();
                    pnt.x = ArpCentreE + ArpRadius * Math.Cos(i * Math.PI / 18);
                    pnt.y = ArpCentreN - ArpRadius * Math.Sin(i * Math.PI / 18);
                    shp.InsertPoint(pnt, ref myPointIndex);
                    shpPloygon.EditInsertShape(shp, ref myShapeIndex);
                    //   myPointIndex++;
                    //  myShapeIndex++;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            shpPloygon.StopEditingShapes(true, true, null);
            shpPloygon.Close();

        }

        public static bool IsInPolygon(string point, string polygon)
        {
            bool inout = false;
            var SplitPoint = point.Split(',');
            MapWinGIS.Point pnt = new Point();

            pnt.x = double.Parse(SplitPoint[0]);
            pnt.y = double.Parse(SplitPoint[1]);


            double[] Xs = new double[4];
            double[] Ys = new double[4];

            string[] vpoints = polygon.Split(';');

            int i = 0;
            foreach (string vpoint in vpoints)
            {
                MapWinGIS.Point PolyPoint = new Point();
                // ShpfileType.SHP_POINT);
                var vp = vpoint.Split(',');
                Xs[i] = Convert.ToDouble(vp[0]);
                Ys[i] = Convert.ToDouble(vp[1]);
                i++;

            }
            MapWinGIS.Point lastpoint = new Point();
            lastpoint.x = Xs[2];
            lastpoint.y = Ys[2];

            for (i = 0; i <= 2; i++)
            {
                MapWinGIS.Point b = new MapWinGIS.Point();
                b.x = Xs[i]; b.y = Ys[i];

                if ((b.x == pnt.x) && (b.y == pnt.y))
                    return true;

                if ((b.y == lastpoint.y) && (pnt.y == lastpoint.y))
                {
                    if (lastpoint.x <= pnt.x && pnt.x <= b.x)
                        return true;

                    if ((b.x <= pnt.x) && (pnt.x <= lastpoint.x))
                        return true;
                }

                if ((b.y < pnt.y) && (lastpoint.y >= pnt.y) || (lastpoint.y < pnt.y) && (b.y >= pnt.y))
                {
                    if (b.x + (pnt.y - b.y) / (lastpoint.y - b.y) * (lastpoint.x - b.x) <= pnt.x)
                        inout = !inout;
                }
                lastpoint = b;
            }
            return inout;

        }

    }
}
