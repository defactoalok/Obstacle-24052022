using CoordinateSharp;
using Microsoft.VisualBasic;
using System;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using System.Windows.Forms;

namespace Obstacle
{
    public partial class frmAirportCode2 : Form
    {
        public frmAirportCode2()
        {
            InitializeComponent();

        }

        private void frmAirportCode2_Load(object sender, EventArgs e)
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
                this.RwyLength.Text = Math.Round(d.Meters, 2).ToString();
                this.Bearing.Text = Math.Round(d.Bearing, 2).ToString();
                d = new Distance(c2, c, Shape.Ellipsoid);

                this.BackBearing.Text = Math.Round(d.Bearing, 2).ToString();
                this.Easting.Text = c.UTM.Easting.ToString();
                this.Northing.Text = c.UTM.Northing.ToString();
                this.Easting1.Text = c2.UTM.Easting.ToString();
                this.Northing1.Text = c2.UTM.Northing.ToString();
                this.ForwardDms.Text = decimaltodms(double.Parse(this.Bearing.Text));
                this.BackwardDms.Text = decimaltodms(double.Parse(this.BackBearing.Text));

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
                double bearRadian = Math.Round(BackBearing * (Math.PI) / 180, 2);
                CosX = Math.Round(Distance * (Math.Cos(bearRadian)), 2);
                SinX = Math.Round(Distance * (Math.Sin(bearRadian)), 2);

                App1E = Math.Round(double.Parse(He) + SinX, 2);
                App1N = Math.Round(double.Parse(Hn) + CosX, 2);
                ReverseBear = 0;
                ReverseBear = Math.Round(BackBearing, 2);

                bearRadian = Math.Round( Bearing * (Math.PI) / 180, 2);
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
         
            Distance d = new Distance(c, c2, Shape.Ellipsoid);

            PDistance= Math.Round(d.Meters, 1) ;
          
            Forward= Math.Round(d.Bearing, 2);
            
            ForwardDms = decimaltodms(Forward);
            
            d = new Distance(c2, c, Shape.Ellipsoid);
            
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
            Distance d = new Distance(c1, c2, Shape.Ellipsoid);
            Bearing = Math.Round(d.Bearing, 2);
            RDistance = Math.Round(d.Meters);

            return RDistance;
        }
        private void GetObstacles(object sender, EventArgs e)
        {

            /*     double latitude = double.Parse(this.LatD.Text) + (double.Parse(this.LatM.Text) / 60) + (double.Parse(this.LatS.Text) / 3600);
                 double longitude = double.Parse(this.LngD.Text) + (double.Parse(this.LngM.Text) / 60) + (double.Parse(this.LngS.Text) / 3600);
                 double latitude2 = double.Parse(this.LatD2.Text) + (double.Parse(this.LatM2.Text) / 60) + (double.Parse(this.LatS2.Text) / 3600);
                 double longitude2 = double.Parse(this.LngD2.Text) + (double.Parse(this.LngM2.Text) / 60) + (double.Parse(this.LngS2.Text) / 3600);
             */



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
                "DistApp2=@DistApp2, Bearing=@Bearing where ID=@ID";

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
                        OleDbCommand cmdUpdate = new OleDbCommand(updateQuery, connection);
                        while (reader.Read())
                        {

                            //  GetBearingDistance getBD = new GetBearingDistance();


                          //  try
                           // {
                                getX = 0; getY = 0; getYY = 0; DistanceFromApp1 = 0; DistanceFromApp2 = 0;
                                RWYSTRIP = 0; ARPDistance = 0; RunwayLength = 0; nearer = false;



                                PointLat = double.Parse(reader["LatDecimal"].ToString());
                                PointLng = double.Parse(reader["LongDecimal"].ToString());


                                PointsLatLong(latitude, longitude, PointLat, PointLng, out double RDistance, out double Bearing);
                                DistanceFromApp1 = RDistance;
                                BearingFromApp1 = Bearing;

                                PointsLatLong(latitude2, longitude2, PointLat, PointLng, out RDistance, out Bearing);
                                DistanceFromApp2 = RDistance;
                                BearingFromApp2 = Bearing;

                                PointsLatLong(ARPLat, ARPLng, PointLat, PointLng, out RDistance, out Bearing);
                                ARPDistance = RDistance;

                                Elevation = double.Parse(reader["Elevation"].ToString());

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

                                Getsurface(getX, getY, getYY, DistanceFromApp1, DistanceFromApp2, RWYSTRIP, ARPDistance, RunwayLength, nearer, out string surface);


                                int ID = int.Parse(reader["ID"].ToString());

                                cmdUpdate.Parameters.AddWithValue("@Surface", surface);
                                cmdUpdate.Parameters.AddWithValue("@X", getX);
                                cmdUpdate.Parameters.AddWithValue("@Y", getY);
                                cmdUpdate.Parameters.AddWithValue("@YY", getY - getYY);
                                cmdUpdate.Parameters.AddWithValue("@Distance", distance);
                                cmdUpdate.Parameters.AddWithValue("@DistApp1", DistanceFromApp1);
                                cmdUpdate.Parameters.AddWithValue("@DistApp2", DistanceFromApp2);
                                cmdUpdate.Parameters.AddWithValue("@Bearing", Bearing);
                                cmdUpdate.Parameters.AddWithValue("@ID", ID);
                            cmdUpdate.ExecuteNonQuery();
                                cmdUpdate.Parameters.Clear();

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
        }
        

        private void Getsurface(double getX, double getY, double getYY, double distanceFromApp1, double distanceFromApp2, double rWYSTRIP, double aRPDistance, double runwayLength, bool nearer, out string surface)
        {
            bool Funnel; string found; double DISTANCE; double strip; Funnel=false;
            surface = "Nil";
           // try
            //{
                if (distanceFromApp1 < distanceFromApp2)
                {
                    DISTANCE = distanceFromApp1;
                }
                else
                {
                    DISTANCE = distanceFromApp2;
                }

                strip = Math.Truncate(rWYSTRIP);

                found = "Nil";

                //Funnel

                Funnel = true ? getYY >= getY : getYY < getY;
                if (Funnel && getX <= 2500)
                {
                    found = "APP";
                }


                if (getY <= strip && distanceFromApp1 <= runwayLength && distanceFromApp2 <= runwayLength)
                {
                    found = "RWY";
                }

                if (getY > strip && getY <= ((strip) + 225) && getX <= 1125 && distanceFromApp1 <= runwayLength && distanceFromApp2 <= runwayLength)
                {
                    found = "TS";
                }

                double newyY = (45 - (getX * 0.0333)) / 0.1433 + getYY;

                if (getX <= 1125 && found == "" && getY <= newyY)
                {
                    found = "TST";
                }

           
                if (found.Substring(0, 2) != "TS" && found.Substring(0, 3) != "RWY" && aRPDistance <= 2500)
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
             

            if (aRPDistance > 2500 && aRPDistance <= 3600)
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

            this.LatD2.Text = "25"; this.LatM2.Text = "01"; this.LatS2.Text = "07.5926";
            this.LngD2.Text = "73";this.LngM2.Text = "54"; this.LngS2.Text = "14.2403";
            this.LatD.Text = "25"; this.LatM.Text = "01"; this.LatS.Text = "24.5406";
            this.LngD.Text = "73"; this.LngM.Text = "53"; this.LngS.Text = "39.6233";

            //25°01'13.5107"N	73°53'59.4940"E
            this.ArpLatD.Text = "25"; this.ArpLatM.Text = "01"; this.ArpLatS.Text = "13.5107";
            this.ArpLngD.Text = "73"; this.ArpLngM.Text = "53"; this.ArpLngS.Text = "59.4940";
            this.BasicStrip.Text = "60";this.RunwayStrip.Text = "80";this.Diversion.Text = "10";
            

        }

        private void button6_Click(object sender, EventArgs e)
        {
            GetObstacles(null, null);
        }
    }
}
