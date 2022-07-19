using CoordinateSharp;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using Microsoft.Office;
using MapWinGIS;
using Point = MapWinGIS.Point;

namespace Obstacle
{
    public partial class Form1 : Form
    {
        string CurrentDir = Directory.GetCurrentDirectory();
        string STstTopLeft, STstTopRight, STstBottomLeft, STstBottomRight;


       

        public object SafetyArea { get; private set; }
        public bool showMenu;
        public Form1()
        {
            InitializeComponent();
            // this.FileName.Visible = false;
            //this.SaveFile.Visible = false;
          

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            H_Northing.KeyPress += ValidateKeyPress;
            H_Easting.KeyPress += ValidateKeyPress;
            App1East.KeyPress += ValidateKeyPress;
            App1North.KeyPress += ValidateKeyPress;
            App2North.KeyPress += ValidateKeyPress;
            App2East.KeyPress += ValidateKeyPress;
            HRPElevation.KeyPress += ValidateKeyPress;
            Safety.KeyPress += ValidateKeyPress;
            Diversion.KeyPress += ValidateKeyPress;
            Bearing.KeyPress += ValidateKeyPress;
            ReverseBearing.KeyPress += ValidateKeyPress;
            RotorDia.KeyPress += ValidateKeyPress;
            LatD.KeyPress += ValidateKeyPress;
            LatM.KeyPress += ValidateKeyPress;
            LatS.KeyPress += ValidateKeyPress;
            LngD.KeyPress += ValidateKeyPress;
            LngM.KeyPress += ValidateKeyPress;
            LngS.KeyPress += ValidateKeyPress;

            //splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // showgrid();
            // if (this.SelectedID.Text == "")
            //{
            //  button2_Click(null, null);

            //}
        }



        private void showgrid()
        {

            string OledbConnectString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source= ObstaclesData.accdb";
            string getRecords = "SELECT [SL NO], OBJECT, LATITUDE, LONGITUDE, NORTHING , EASTING,  " +
                 " HRPDistance,Elevation, HRPBearing, X  , Y  , YFunnel, PEA  , OBA  , PEB, OBB, PEC,OBC, Surface  " +
                 "FROM SurveyData";
            // string getRecords = "SELECT  *  FROM SurveyData";
            
       
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
                        DataGridView dg = (DataGridView)loadData.Controls["dataGridView1"];

                        //dataGridView1.DataSource = ds.Tables[0];
                        dg.DataSource = ds.Tables[0];
                        dg.Columns[0].HeaderText = "Obj No.";
                        dg.Columns[1].HeaderText = "Obj Name";
                        //dataGridView1.Refresh();

                       
                        loadData.Controls["H_Northing"].Text = this.H_Northing.Text;
                        loadData.Controls["H_Easting"].Text = this.H_Easting.Text;
                        loadData.Controls["Bearing"].Text = this.Bearing.Text;
                        loadData.Controls["ReverseBearing"].Text = this.ReverseBearing.Text;
                        loadData.Controls["App1North"].Text = this.App1North.Text;
                        loadData.Controls["App1East"].Text = this.App1East.Text;
                        loadData.Controls["App2East"].Text = this.App2East.Text;
                        loadData.Controls["App2North"].Text = this.App2North.Text;
                        loadData.Controls["Safety"].Text = this.Safety.Text;
                        loadData.Controls["Diversion"].Text = this.Diversion.Text;
                        loadData.Controls["RotorDia"].Text = this.RotorDia.Text;
                        loadData.Controls["FlatFunnel"].Text = this.FlatFunnel.Text;
                        loadData.Controls["OEdge"].Text = this.OEdge.Text;
                        loadData.Controls["HRPElevation"].Text = this.HRPElevation.Text;
                        loadData.Controls["Fato"].Text = this.Fato.Text;
                        loadData.Controls["Tolf"].Text = this.TOLF.Text;
                        loadData.Controls["SiteLocation"].Text = this.SiteLocation.Text;
                        loadData.Controls["Zone"].Text = this.Zone.Text;

                        //  this.dataGridView1.Columns[0].Visible= false;
                        //this.dataGridView1.Columns[1].Frozen = true;
                        //this.dataGridView1.Columns[2].Frozen = true;
                        //                        this.dataGridView1.Columns[3].Frozen = true;
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
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {

            string DeleteData = "Delete from SurveyData";
            string appendData = "Insert into SurveyData([SL NO], [OBJECT],  NORTHING, EASTING, Elevation,LATITUDE, LONGITUDE ) " +
                @"Values(@SL,@Object,@North,@East,@Elev,@Lat,@long)";
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
                            this.SiteLocation.Text = Interaction.InputBox("Please give a name", "Location", "");
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
                                LatLong(cols[3], cols[2], int.Parse(this.Zone.Text), out string Lat, out string Lng);
                                cmd.Parameters.AddWithValue("@Lat", Lat);
                                cmd.Parameters.AddWithValue("@long", Lng);
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
            showMenu = false;

            //WindowState = FormWindowState.Maximized;
        }

        private void H_Easting_TextChanged(object sender, EventArgs e)
        {

        }

        private void AppNorth_TextChanged(object sender, EventArgs e)
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

        private void App2North_TextChanged(object sender, EventArgs e)
        {

        }

        private void H_Northing_TextChanged(object sender, EventArgs e)
        {

        }

        private void GetAppoachCoordinates()
        {
            /*
            if ((!string.IsNullOrWhiteSpace (this.H_Easting.ToString()) && !string.IsNullOrWhiteSpace(this.H_Northing.ToString())
               && !string.IsNullOrWhiteSpace(this.Bearing.ToString()) && !string.IsNullOrWhiteSpace(this.Safety.ToString()) &&
               !string.IsNullOrWhiteSpace(this.H_Easting.ToString()) && !string.IsNullOrWhiteSpace(this.H_Northing.ToString())
               && !string.IsNullOrWhiteSpace(this.Bearing.ToString()) && !string.IsNullOrWhiteSpace(this.Safety.ToString())))
            */

            if (this.H_Easting.Text.ToString() != "" && this.H_Northing.Text.ToString() != ""
            && this.Bearing.Text.ToString() != "" && this.Safety.Text.ToString() != "")
            {

                GetNewCoordinates(this.H_Easting.Text.ToString(), this.H_Northing.Text.ToString(),
                    double.Parse(this.Safety.Text) / 2, double.Parse(this.Bearing.Text),
                    out double App1N, out double App1E,
                    out double App2E, out double App2N);//, out double ReverseBear);

                this.App1East.Text = App1E.ToString();
                this.App1North.Text = App1N.ToString();
                // this.ReverseBearing.Text = ReverseBear.ToString();
                this.App2East.Text = App2E.ToString();
                this.App2North.Text = App2N.ToString();


            }

        }
        private double GetNewCoordinates(string He, string Hn, double Distance, double Bearing,
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
                double bearRadian = Math.Round(Bearing * (Math.PI) / 180, 2);
                CosX = Math.Round(Distance * (Math.Cos(bearRadian)), 2);
                SinX = Math.Round(Distance * (Math.Sin(bearRadian)), 2);

                App1E = Math.Round(double.Parse(He) + SinX, 2);
                App1N = Math.Round(double.Parse(Hn) + CosX, 2);
                ReverseBear = 0;
                ReverseBear = Math.Round(double.Parse(this.ReverseBearing.Text), 2);

                bearRadian = Math.Round(ReverseBear * (Math.PI) / 180, 2);
                CosX = Distance * (Math.Cos(bearRadian));
                SinX = Distance * (Math.Sin(bearRadian));

                App2E = Math.Round(double.Parse(He) + SinX, 2);
                App2N = Math.Round(double.Parse(Hn) + CosX, 2);
            }
            return App1E;

        }

        private void button1_Click(object sender, EventArgs e)
        {

            H_Northing.Text = "2051482.335";
            H_Easting.Text = "373247.972";
            Bearing.Text = "122";
            ReverseBearing.Text = "302";
            RotorDia.Text = "11";
            //App1East.Text = "373247.972";
            //App1North.Text = "1927961.44";
            //App2North.Text = "1927961.44";
            //App2East.Text = "218049.87";
            HRPElevation.Text = "569.6";
            Safety.Text = "28";
            Diversion.Text = "10";


            double Bear = double.Parse(Bearing.Text);// CalBearing(H_Northing.Text, H_Easting.Text, App1North.Text, App1East.Text);
            double ReverseBear = double.Parse(ReverseBearing.Text); //CalBearing(H_Northing.Text, H_Easting.Text, App2North.Text, App2East.Text);
            Bearing.Text = Math.Round(Bear, 2, MidpointRounding.AwayFromZero).ToString();
            ReverseBearing.Text = Math.Round(ReverseBear, 2, MidpointRounding.AwayFromZero).ToString();
            GetAppoachCoordinates();
        }

        private double CalBearing(string Hn, string He, string A1N, string A1E)
        {

            double Bearing, Azimuth = 0;
            Bearing = 0;
            if (!string.IsNullOrEmpty(Hn) && !string.IsNullOrEmpty(He)
                && !string.IsNullOrEmpty(A1N) && !string.IsNullOrEmpty(A1E))
            {
                /*
                MessageBox.Show(Hn);
                MessageBox.Show(He);
                MessageBox.Show(A1N);
                MessageBox.Show(A1E);
           */

                double EMinusE = double.Parse(A1E) - double.Parse(He);
                double NMinusN = double.Parse(A1N) - double.Parse(Hn);
                Bearing = Math.Abs(Math.Atan(((EMinusE) / (NMinusN))) * (180 / Math.PI));

                if (EMinusE > 0 && NMinusN > 0)
                {
                    Azimuth = Bearing;
                }
                if (EMinusE >= 0 && NMinusN <= 0)
                {
                    Azimuth = 360 - Bearing;
                }
                if (EMinusE <= 0 && NMinusN <= 0)
                {
                    Azimuth = 180 + Bearing;
                }
                if (EMinusE <= 0 && NMinusN >= 0)
                {
                    Azimuth = 180 - Bearing;
                }

                return (Azimuth);

                //IF(AND(AI7 > 0, AJ7 > 0), ABS(J3), IF(AND(AI7 >= 0, AJ7 <= 0), 180 - ABS(J3), IF(AND(AI7 <= 0, AJ7 <= 0), 180 + ABS(J3), IF(AND(AI7 <= 0, AJ7 >= 0), 360 - ABS(J3), ABS(J3)))))
            }
            return Azimuth;
        }

        private double SendAzimuth(double EMinusE, double NMinusN, double Bearing, out double Azimuth)
        {
            Azimuth = 0;

            if (EMinusE > 0 && NMinusN > 0)
            {
                Azimuth = Bearing;
            }
            if (EMinusE >= 0 && NMinusN <= 0)
            {
                Azimuth = 360 - Bearing;
            }
            if (EMinusE <= 0 && NMinusN <= 0)
            {
                Azimuth = 180 + Bearing;
            }
            if (EMinusE <= 0 && NMinusN >= 0)
            {
                Azimuth = 180 - Bearing;
            }
            return Azimuth;
        }
        private void splitContainer1_SplitterMoved(object sender, SplitterEventArgs e)
        {

        }

        private string SetApp(double Bear, out string NewBearing)
        {
            if (double.Parse(this.Bearing.Text) > 180)
            {
                this.ReverseBearing.Text = (double.Parse(this.Bearing.Text) - 180).ToString();
            }
            else
            {
                this.ReverseBearing.Text = (double.Parse(this.Bearing.Text) + 180).ToString();
            }
            NewBearing = "";

            var Runway = Bear > 9 ? (Math.Truncate(Bear / 10)) : Math.Truncate(Bear);


            if (Runway <= 9)
            {
                NewBearing = "APP" + "0" + Runway.ToString();
            }

            if (Runway > 9 && Runway <= 99)
            {
                NewBearing = "APP" + Runway.ToString();
            }

            if (Bear > 99)
            {
                NewBearing = "APP" + Bear.ToString().Substring(0, 2);
            }


            return NewBearing;
        }




        private void button3_Click(object sender, EventArgs e)
        {

            GetAppoachCoordinates();
            string NewBearing = "";

            label4.Text = SetApp(double.Parse(this.Bearing.Text), out NewBearing);
            label9.Text = SetApp(double.Parse(this.ReverseBearing.Text), out NewBearing);


            double Bear = double.Parse(this.Bearing.Text); // Math.Round(CalBearing(H_Northing.Text, H_Easting.Text, App1North.Text, App1East.Text));
            double ReverseBear = double.Parse(this.ReverseBearing.Text); // Math.Round(CalBearing(H_Northing.Text, H_Easting.Text, App2North.Text, App2East.Text));


            string updateQuery = "UPDATE SurveyData SET HRPDistance = @HRPDistance, HRPBearing = @HRPBearing, X=@X,Y=@Y  ,YFunnel=@YY, DistApp1=@DistApp1,DistApp2= @DistApp2, BearingApp1=@RBearingApp1, " +
               "BearingApp2=@RBearingApp2, Surface=@Surface  , PEA=@PEA, PEB=@PEB, PEC=@PEC, OBA=@OBA, OBB=@OBB, OBC=@OBC  WHERE id =@ID";

            //  string updateQuery = "UPDATE SurveyData SET HRPDistance = @HRPDistance =HRPBearing = @HRPBearing, PEA=@PEA, PEB=@PEB, PEC=@PEC, OBA=@OBA, OBB=@OBB, OBC=@OBC WHERE id =@ID";


            string OledbConnectString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=ObstaclesData.accdb";

            using (OleDbConnection connection = new OleDbConnection(OledbConnectString))

            {
                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }

                OleDbCommand cmd = new OleDbCommand("Select * from SurveyData", connection);
                try
                {
                    using (OleDbDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            GetBearingDistance getBD = new GetBearingDistance();
                            double distance, Bearing;

                            try
                            {
                                OleDbCommand cmdUpdate = new OleDbCommand(updateQuery, connection);

                                //  getBD.HN = H_Northing.Text;
                                // getBD.HE = H_Easting.Text;
                                double Easting = double.Parse(reader["Easting"].ToString());
                                double Northing = double.Parse(reader["Northing"].ToString());

                                GetXY(Easting, Northing, out double returnX, out double returnY, out double RDistApp1, out double RDistApp2, out double RBearingApp1, out double RBearingApp2, out double YY);

                                double Elevation = double.Parse(reader["Elevation"].ToString());
                                double getX = Math.Abs(returnX);
                                double getY = Math.Abs(returnY);
                                double getYY = Math.Abs(YY);
                                double DistApp1 = Math.Abs(RDistApp1);
                                double DistApp2 = Math.Abs(RDistApp2);
                                double BearingApp1 = Math.Abs(RBearingApp1);
                                double BearingApp2 = Math.Abs(RBearingApp2);

                                bool nearer = false;

                                nearer = true ? DistApp1 <= DistApp2 : DistApp1 > DistApp2;

                                if (nearer)
                                { distance = DistApp1; Bearing = BearingApp1; }
                                else
                                { distance = DistApp2; Bearing = BearingApp2; }


                                string Surface = surface(YY, getY, getX, distance, int.Parse(Safety.Text), nearer);

                                PE(Surface, distance, getX, getY, Elevation, double.Parse(HRPElevation.Text), int.Parse(Safety.Text),
                                    out double PECatA, out double PECatB, out double PECatC, out double OBA, out double OBB, out double OBC);


                                int ID = int.Parse(reader["id"].ToString());

                                cmdUpdate.Parameters.AddWithValue("@HRPDistance", Math.Round(distance, 1));
                                cmdUpdate.Parameters.AddWithValue("@HRPBearing", Math.Round(Bearing, 1));
                                cmdUpdate.Parameters.AddWithValue("@X", Math.Round((getX), 0));
                                cmdUpdate.Parameters.AddWithValue("@Y", Math.Round((getY), 0));
                                cmdUpdate.Parameters.AddWithValue("@YY", Math.Round((getYY), 0));
                                cmdUpdate.Parameters.AddWithValue("@DistApp1", Math.Round((DistApp1), 1));
                                cmdUpdate.Parameters.AddWithValue("@DistApp2", Math.Round((DistApp2), 1));
                                cmdUpdate.Parameters.AddWithValue("@RBearingApp1", (Math.Round(BearingApp1, 1)));
                                cmdUpdate.Parameters.AddWithValue("@RBearingApp2", (Math.Round(BearingApp2, 2)));
                                cmdUpdate.Parameters.AddWithValue("@Surface", Surface);
                                cmdUpdate.Parameters.AddWithValue("@PEA", Math.Round(PECatA, 1));
                                cmdUpdate.Parameters.AddWithValue("@PEB", Math.Round(PECatB, 1));
                                cmdUpdate.Parameters.AddWithValue("@PEC", Math.Round(PECatC, 1));
                                cmdUpdate.Parameters.AddWithValue("@OBA", Math.Round(OBA, 1));
                                cmdUpdate.Parameters.AddWithValue("@OBB", Math.Round(OBB, 1));
                                cmdUpdate.Parameters.AddWithValue("@OBC", Math.Round(OBC, 1));
                                cmdUpdate.Parameters.AddWithValue("@ID", ID);
                                cmdUpdate.ExecuteNonQuery();
                                cmdUpdate.Parameters.Clear();

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
            }

            this.dataGridView1.DefaultCellStyle.Font = new System.Drawing.Font("Tahoma", 9);
            showgrid();
        }
      
        private string surface(double YY, double Y, double X, double Distance, int SafetyArea, bool nearer)
        {
            string Surface = "NIL";

            if (YY < Y && Distance - SafetyArea <= 90)

            { Surface = "TS"; }
            else
            { Surface = "Nil"; }

            if (YY >= Y && YY <= double.Parse(this.OEdge.Text))
            {
                if (nearer)
                { Surface = label4.Text; }
                else
                { Surface = label9.Text; }
            }

            if (Distance > 3386)
            {
                Surface = "NIL";
            }
            return Surface;

        }

        private double PE(string surface, double Distance, double getX, Double getY, double Elevation, double HrpElevation, int SafetyArea, out double PECatA, out double PECatB, out double PECatC, out double OBA, out double OBB, out double OBC)
        {
            string str;
            if (surface.Length > 3)
            {
                str = surface.Substring(0, 3);
            }
            else { str = surface; }

            PECatA = 0;
            PECatB = 0;
            PECatC = 0;
            OBA = 0;
            OBB = 0;
            OBC = 0;

            if (str == "APP")

            {
                if (getX <= 3386)
                {
                    PECatA = (getX * 0.045) + (HrpElevation);
                    OBA =  Elevation- PECatA ;
                    if (getX <= 245)
                    {
                        PECatB = (getX * .08) + HrpElevation;
                        OBB =  Elevation- PECatB ;
                    }
                    if (getX > 245 && getY <= 1075)
                    {
                        PECatB = 19.6 + (getX * (16 / 100)) + HrpElevation;
                        OBB =  Elevation- PECatB ;
                    }
                    if (getX <= 1220)
                    {
                        PECatC = (getX * (12.5 / 100)) + HrpElevation;
                        OBC = Elevation- PECatC;

                    }

                }
                return PECatB;
            }
            if (str != "APP")
            {
                PECatA = (Distance - (SafetyArea / 2)) / 2 + HrpElevation;
                PECatB = (Distance - (SafetyArea / 2)) / 2 + HrpElevation;
                PECatC = (Distance - (SafetyArea / 2)) / 2 + HrpElevation;
                OBA =  Elevation- PECatA ;
                OBB =  Elevation- PECatB;
                OBC = Elevation- PECatC;
                return PECatB;
            }
            return PECatB;
        }
        private double GetXY(double Easting, double Northing, out double returnX, out double returnY, out double RDistApp1, out double RDistApp2, out double RBearingApp1, out double RBearingApp2, out double YY)
        {

            double HN = double.Parse(H_Northing.Text);
            double HE = double.Parse(H_Easting.Text);
            double Bear = double.Parse(this.Bearing.Text);// CalBearing(H_Northing.Text, H_Easting.Text, App1North.Text, App1East.Text);
            double ReverseBear = double.Parse(this.ReverseBearing.Text); // CalBearing(H_Northing.Text, H_Easting.Text, App2North.Text, App2East.Text);

            double PointE = Easting;
            double PointN = Northing;

            double App1N = double.Parse(App1North.Text);
            double App1E = double.Parse(App1East.Text);
            double App2N = double.Parse(App2North.Text);
            double App2E = double.Parse(App2East.Text);

            //  var Return = new ReturnXY();
            double DistAPP1, BearingApp1, DistApp2, BearingApp2;


            {
                {
                    double EMinusE, NMinusN = 0;

                    EMinusE = PointE - App1E;
                    NMinusN = PointN - App1N;
                    double EnMinus = Math.Abs(EMinusE / NMinusN);

                    BearingApp1 = Math.Atan(EnMinus) * 180 / Math.PI;

                    double PointAzimuth1 = SendAzimuth(EMinusE, NMinusN, BearingApp1, out double Azim1);
                    //   MessageBox.Show(EMinusE.ToString() + " " + NMinusN.ToString().ToString());

                    DistAPP1 = Math.Round(Math.Sqrt(Math.Pow(NMinusN, 2) + Math.Pow(EMinusE, 2)), 1);

                    EMinusE = PointE - App2E;
                    NMinusN = PointN - App2N;

                    EnMinus = Math.Abs((EMinusE / NMinusN));

                    BearingApp2 = Math.Atan(EnMinus) * 180 / Math.PI;

                    double PointAzimuth2 = SendAzimuth(EMinusE, NMinusN, BearingApp2, out double Azim2);
                    // MessageBox.Show(EMinusE.ToString() + " " + NMinusN.ToString().ToString());

                    //   MessageBox.Show(DistAPP1.ToString()+" "+DistApp2.ToString()+" "+ PointAzimuth1.ToString() + " " + PointAzimuth2.ToString());
                    DistApp2 = Math.Round(Math.Sqrt(Math.Pow(NMinusN, 2) + Math.Pow(EMinusE, 2)), 1);

                    YY = 0;
                    RDistApp1 = DistAPP1;
                    RDistApp2 = DistApp2;
                    RBearingApp1 = PointAzimuth1;
                    RBearingApp2 = PointAzimuth2;


                    if (DistAPP1 > DistApp2)
                    {
                         returnX = Math.Round(Math.Abs(DistApp2 * (Math.Cos((ReverseBear - Azim2) * (Math.PI) / 180))));
                         returnY = Math.Round(Math.Abs(DistApp2 * (Math.Sin((ReverseBear - Azim2) * (Math.PI) / 180))));
                        
                    }
                    else
                    {
                        returnX = Math.Round(Math.Abs(DistAPP1 * (Math.Cos((Bear - Azim1) * (Math.PI) / 180))));
                       returnY = Math.Round(Math.Abs(DistAPP1 * (Math.Sin((Bear - Azim1) * (Math.PI) / 180))));
                        
                    }

                    if (!string.IsNullOrEmpty(Diversion.Text) && !string.IsNullOrEmpty(returnX.ToString()) && !string.IsNullOrEmpty(Safety.Text))
                    {
                        YY = Math.Round((returnX * (double.Parse(Diversion.Text) / 100) + (int.Parse(Safety.Text)) / 2));
                    }

                }



            }

            return DistAPP1;
        }

      

        private double GetAzimuth(double eMinusE, double nMinusN, double bearingApp1)
        {
            return 1;
            //  throw new NotImplementedException();
        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void splitContainer1_Panel1_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void importDataToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void bbbbToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (this.H_Easting.Text.ToString() != "" && this.H_Northing.Text.ToString() != "" &&
                 this.Bearing.Text.ToString() != "" && this.ReverseBearing.Text.ToString() != "" &&
                 this.Safety.Text.ToString() != "" && this.HRPElevation.Text.ToString() != "" && this.Diversion.Text != ""
                 && this.App1East.Text != "" && this.App1North.Text != "" && this.App2East.Text != ""
                 && this.App2North.Text != "" && this.RotorDia.Text != "" && this.OEdge.Text != "" && this.Zone.Text != "")
                // && this.Fato.Text != "" && this.TOLF.Text != "")
            {

                button3_Click(null, null);
                foreach (DataGridViewRow Myrow in dataGridView1.Rows)
                {
                    if (Myrow.IsNewRow) continue;
                    if (dataGridView1.Rows.Count > 0 &&
                        !string.IsNullOrEmpty(Myrow.Cells[10].Value.ToString()))
                    {
                        string surf = Myrow.Cells[10].Value.ToString();
 
                        try
                        {

                            if (Convert.ToDouble(Myrow.Cells[13].Value) < 0
                                || Convert.ToDouble(Myrow.Cells[15].Value) < 0
                                || Convert.ToDouble(Myrow.Cells[17].Value) < 0)

                            {
                                if (!string.IsNullOrEmpty(surf) && surf.Substring(0, 2) == "AP")
                                { Myrow.DefaultCellStyle.BackColor = System.Drawing.Color.Pink; }
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
 
                    }

                }
            } else
            {
                MessageBox.Show("Please inout all parameters");
            }
        }
        private void aasasToolStripMenuItem_Click(object sender, EventArgs e)
        {

            button2_Click(null, null);
            showgrid();
        }

        private void saveAsButton_Click(object sender, EventArgs e)
        {

        }

        private void ExportToExcel(string filename)

        {
            // Create a spreadsheet document by supplying the filepath.
            // By default, AutoSave = true, Editable = true, and Type = xlsx.
            filename = filename.Trim() + ".XLSX";

            string filepath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), filename);

            SpreadsheetDocument spreadsheetDocument = SpreadsheetDocument.Create(filepath, SpreadsheetDocumentType.Workbook);

            // Add a WorkbookPart to the document.
            WorkbookPart workbookpart = spreadsheetDocument.AddWorkbookPart();
            workbookpart.Workbook = new Workbook();

            // Add a WorksheetPart to the WorkbookPart.
            WorksheetPart worksheetPart = workbookpart.AddNewPart<WorksheetPart>();
            worksheetPart.Worksheet = new Worksheet(new SheetData());

            // Add Sheets to the Workbook.
            Sheets sheets = spreadsheetDocument.WorkbookPart.Workbook.
                AppendChild<Sheets>(new Sheets());

            // Append a new worksheet and associate it with the workbook.
            Sheet sheet = new Sheet()
            {
                Id = spreadsheetDocument.WorkbookPart.
                GetIdOfPart(worksheetPart),
                SheetId = 1,
                Name = "Obstacle"
            };
            sheets.Append(sheet);
            Worksheet worksheet = worksheetPart.Worksheet;
            SheetData sheetData = worksheet.GetFirstChild<SheetData>();

            string OledbConnectString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=ObstaclesData.accdb";
            using (OleDbConnection connection = new OleDbConnection(OledbConnectString))
            {

                try
                {
                    OleDbCommand cmd = new OleDbCommand("Select * from SurveyData", connection);
                    connection.Open();
                    DataTable dt = new DataTable();
                    DataSet ds = new DataSet();
                    Row r = new Row();
                    using (OleDbDataAdapter adapter = new OleDbDataAdapter("Select * from SurveyData", connection))
                    {
                        adapter.Fill(ds);
                        dt = ds.Tables[0];

                        DocumentFormat.OpenXml.Spreadsheet.Row headerRow = new DocumentFormat.OpenXml.Spreadsheet.Row();

                        List<String> columns = new List<string>();
                        foreach (DataColumn column in dt.Columns)
                        {
                            columns.Add(column.ColumnName);

                            Cell c = new Cell()
                            {
                                DataType = DocumentFormat.OpenXml.Spreadsheet.CellValues.String,
                                CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(column.ColumnName)
                            };
                            headerRow.AppendChild(c);
                            // r.Append(cell);
                        }
                        sheetData.AppendChild(headerRow);

                        foreach (DataRow item in dt.Rows)
                        {
                            DocumentFormat.OpenXml.Spreadsheet.Row newRow = new DocumentFormat.OpenXml.Spreadsheet.Row();
                            for (int i = 0; i < item.ItemArray.Length; i++)
                            {
                                DocumentFormat.OpenXml.Spreadsheet.Cell cell = new DocumentFormat.OpenXml.Spreadsheet.Cell();
                                Cell c = new Cell()
                                {
                                    CellValue = new CellValue(item[i].ToString())
                                    //  DataType = CellValues.String

                                };
                                newRow.AppendChild(c);

                            }
                            sheetData.AppendChild(newRow);
                        }
                        worksheetPart.Worksheet.Save();
                        spreadsheetDocument.Close();


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

            }
            // Close the document.
            //   spreadsheetDocument.Close();

        }

        private void SaveFile_Click(object sender, EventArgs e)
        {
            ExportToExcel(this.FileName.Text.ToString());
        }

        private void Bearing_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.Bearing.Text))

            ///       if ( double.Parse(this.Bearing.Text) < 0 && double.Parse(this.Bearing.Text) > 360 )
            {
                GetAppoachCoordinates();
                string NewBearing;
                label4.Text = SetApp(double.Parse(this.Bearing.Text), out NewBearing);
                if (!string.IsNullOrEmpty(this.ReverseBearing.Text))
                {
                    label9.Text = SetApp(double.Parse(this.ReverseBearing.Text), out NewBearing);
                }
            }

        }


        //    double Bear = double.Parse(this.Bearing.Text);
        //   double ReverseBear = double.Parse(this.ReverseBearing.Text);






        private void button3_Click_1(object sender, EventArgs e)
        {
            GetAppoachCoordinates();

        }
        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            foreach (DataGridViewRow Myrow in dataGridView1.Rows)
            {            //Here 2 cell is target value and 1 cell is Volume

                if (!string.IsNullOrEmpty(Myrow.Cells["OBA"].ToString()) && !string.IsNullOrEmpty(Myrow.Cells["OBB"].ToString())
                    && !string.IsNullOrEmpty(Myrow.Cells["OBC"].ToString()))
                {
                    if (Convert.ToInt32(Myrow.Cells["OBA"].Value) < 0 || Convert.ToInt32(Myrow.Cells["OBB"].Value) < 0 || Convert.ToInt32(Myrow.Cells["OBC"].Value) < 0)
                    {

                        Myrow.DefaultCellStyle.BackColor = System.Drawing.Color.Pink;
                    }
                }
            }
        }

        private void splitContainer1_SplitterMoved_1(object sender, SplitterEventArgs e)
        {

        }

        private void Bearing_Leave(object sender, EventArgs e)
        {


            if ((!string.IsNullOrEmpty(this.Bearing.Text) && double.Parse(this.Bearing.Text) < 0) ||
                (double.Parse(this.Bearing.Text) > 360 && !string.IsNullOrEmpty(this.Bearing.Text)))
            {
                MessageBox.Show("Please provide correct Bearing in between 1 to 360");
                this.Bearing.Focus();
            }

        }

        private void ReverseBearing_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.ReverseBearing.Text) && double.Parse(this.ReverseBearing.Text) < 0 &&
                double.Parse(this.ReverseBearing.Text) > 360 && !string.IsNullOrEmpty(this.ReverseBearing.Text))
            {
                MessageBox.Show("Please provide correct Bearing in between 1 to 360");
                this.ReverseBearing.Focus();
            }
        }

        private void ReverseBearing_TextChanged(object sender, EventArgs e)
        {
            //GetAppoachCoordinates();
            if (!string.IsNullOrEmpty(this.ReverseBearing.Text))

            ///       if ( double.Parse(this.Bearing.Text) < 0 && double.Parse(this.Bearing.Text) > 360 )
            {
                string NewBearing;
                //      if (!string.IsNullOrEmpty(this.Bearing.Text))
                {
                    label9.Text = SetApp(double.Parse(this.ReverseBearing.Text), out NewBearing);

                }
                label4.Text = SetApp(double.Parse(this.Bearing.Text), out NewBearing);
            }


        }

        private void button3_Click_2(object sender, EventArgs e)
        {
            GetAppoachCoordinates();
        }


        private void Save_Click(object sender, EventArgs e)
        {

            string NewFile = Interaction.InputBox("Give File Name", "File Name", "");


            if (!string.IsNullOrEmpty(NewFile) && NewFile.Length > 0)
            {
                try
                {
                    {
                        string appendMaster = "Insert into Master(H_Northing,H_Easting,App1East,App1North,App2East,App2North,HRPElevation,Safety," +
                                    "Diversion,Bearing,ReverseBearing) Values(@H_Northing,@H_Easting,@App1East,@App2North,@App2East,@App2North," +
                                    "@HRPElevation,@Safety, @Diversion,@Bearing,@ReverseBearing)";


                        string appendData = " Select * into " + NewFile + " from SurveyData";

                        string OledbConnectString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=ObstaclesData.accdb";
                        using (OleDbConnection connection = new OleDbConnection(OledbConnectString))
                        {
                            connection.Open();
                            OleDbCommand cmd = new OleDbCommand(appendMaster, connection);
                            //cmd.Parameters.AddWithValue("@Location", NewFile);
                            cmd.Parameters.AddWithValue("@H_Northing", double.Parse(this.H_Northing.Text));
                            cmd.Parameters.AddWithValue("@H_Easting", double.Parse(this.H_Easting.Text));
                            cmd.Parameters.AddWithValue("@App1East", double.Parse(this.App1East.Text));
                            cmd.Parameters.AddWithValue("@App1North", double.Parse(this.App1North.Text));
                            cmd.Parameters.AddWithValue("@App2East", double.Parse(this.App2East.Text));
                            cmd.Parameters.AddWithValue("@App2North", double.Parse(this.App2North.Text));
                            cmd.Parameters.AddWithValue("@HRPElevation", double.Parse(this.HRPElevation.Text));
                            cmd.Parameters.AddWithValue("@Safety", double.Parse(this.Safety.Text));
                            cmd.Parameters.AddWithValue("@Diversion", double.Parse(this.Diversion.Text));
                            cmd.Parameters.AddWithValue("@Bearing", double.Parse(this.Bearing.Text));
                            cmd.Parameters.AddWithValue("@ReverseBearing", double.Parse(this.ReverseBearing.Text));
                            cmd.ExecuteNonQuery();
                            MessageBox.Show(cmd.CommandText);
                            cmd = new OleDbCommand(appendData, connection);
                            cmd.ExecuteNonQuery();



                            //   OleDbCommand cmd = new OleDbCommand(appendData, connection);
                            //   cmd.ExecuteNonQuery();


                        }
                        WindowState = FormWindowState.Maximized;
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
            }
        }

        private void splitContainer1_SplitterMoved_2(object sender, SplitterEventArgs e)
        {

        }

        private void RotorDia_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(this.RotorDia.Text) && !string.IsNullOrWhiteSpace(this.Safety.Text))
            {
                if (!string.IsNullOrWhiteSpace(this.Diversion.Text))
                {
                    int div = int.Parse(this.Diversion.Text);
                    if (div == 10)
                    {
                        this.OEdge.Text = (double.Parse(this.RotorDia.Text) * 7).ToString();
                        (double.Parse(RotorDia.Text) * 10).ToString();
                    }
                    else
                    {
                        this.OEdge.Text = (double.Parse(this.RotorDia.Text) * 10).ToString();
                    }
                    double FlatFun1 = (double.Parse(this.OEdge.Text) / 2) - (double.Parse(this.Safety.Text) / 2);
                    double Flat = FlatFun1 / 0.1;
                    this.FlatFunnel.Text = Flat.ToString();
                }
            }
        }

        private void Safety_TextChanged(object sender, EventArgs e)
        {
            RotorDia_TextChanged(null, null);
        }

        private void Diversion_TextChanged(object sender, EventArgs e)
        {
            RotorDia_TextChanged(null, null);
        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void OEdge_TextChanged(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void HRPElevation_TextChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void FlatFunnel_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            frmSelectRecord frmselect = new frmSelectRecord();
            frmselect.Show();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            bbbbToolStripMenuItem_Click(null, null);
        }

        private void Bearing_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void button2_Click_2(object sender, EventArgs e)
        {
            frmDistanceBearing frmd = new frmDistanceBearing();
            frmd.Show();

        }

        private string LatLong(string Easting, string Northing, int ZoneNo, out string Lat, out string Lng)
        {
            UniversalTransverseMercator utm = new UniversalTransverseMercator("N", ZoneNo, double.Parse(Easting), double.Parse(Northing));
            Coordinate c = UniversalTransverseMercator.ConvertUTMtoLatLong(utm);
            c.FormatOptions.Format = CoordinateFormatType.Degree_Minutes_Seconds;
            c.FormatOptions.Display_Leading_Zeros = true;
            c.FormatOptions.Round = 3;


            Lat = c.Latitude.ToString();  //N 2º 7' 2.332" E 6º 36' 12.653"
            Lng = c.Longitude.ToString();
            return Lat;
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
        private void GetObstacles(object sender, EventArgs e)
        {

            GetAppoachCoordinates();
            string NewBearing = "";

            label4.Text = SetApp(double.Parse(this.Bearing.Text), out NewBearing);
            label9.Text = SetApp(double.Parse(this.ReverseBearing.Text), out NewBearing);


            double Bear = double.Parse(this.Bearing.Text); // Math.Round(CalBearing(H_Northing.Text, H_Easting.Text, App1North.Text, App1East.Text));
            double ReverseBear = double.Parse(this.ReverseBearing.Text); // Math.Round(CalBearing(H_Northing.Text, H_Easting.Text, App2North.Text, App2East.Text));


            string updateQuery = "UPDATE SurveyData SET HRPDistance = @HRPDistance, HRPBearing = @HRPBearing, X=@X,Y=@Y  ,YFunnel=@YY, DistApp1=@DistApp1,DistApp2= @DistApp2, BearingApp1=@RBearingApp1, " +
               "BearingApp2=@RBearingApp2, Surface=@Surface  , PEA=@PEA, PEB=@PEB, PEC=@PEC, OBA=@OBA, OBB=@OBB, OBC=@OBC  WHERE id =@ID";

            //string updateFirstRow= "UPDATE SurveyData SET HRPDistance = @HRPDistance, HRPBearing = @HRPBearing, X=@X,Y=@Y  ,YFunnel=@YY, DistApp1=@DistApp1,DistApp2= @DistApp2, BearingApp1=@RBearingApp1, " +
             //"BearingApp2=@RBearingApp2, Surface=@Surface  , PEA=@PEA, PEB=@PEB, PEC=@PEC, OBA=@OBA, OBB=@OBB, OBC=@OBC  WHERE id =@ID";

            //  string updateQuery = "UPDATE SurveyData SET HRPDistance = @HRPDistance =HRPBearing = @HRPBearing, PEA=@PEA, PEB=@PEB, PEC=@PEC, OBA=@OBA, OBB=@OBB, OBC=@OBC WHERE id =@ID";


            string OledbConnectString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=ObstaclesData.accdb";

            using (OleDbConnection connection = new OleDbConnection(OledbConnectString))

            {
                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }

                OleDbCommand cmd = new OleDbCommand("Select * from SurveyData", connection);
                try
                {
                    double HrpLat, HrpLng, App1Lat, App1Lng, App2Lat, App2Lng;
                    //HRP
                    WorkLatLong(this.H_Easting.Text, this.H_Northing.Text, Convert.ToInt32(this.Zone.Text),
                        out string Lat1, out string Lng1);
                      HrpLat = double.Parse(Lat1.ToString());
                     HrpLng = double.Parse(Lng1.ToString());
                    //APP1
                    WorkLatLong(this.App1East.Text, this.App1North.Text, Convert.ToInt32(this.Zone.Text),
                        out string Lat2, out string Lng2);
                     App1Lat = double.Parse(Lat2);
                      App1Lng = double.Parse(Lng2);
                    //App2
                    WorkLatLong(this.App2East.Text, this.App2North.Text, Convert.ToInt32(this.Zone.Text),
                        out string Lat3, out string Lng3);
                     App2Lat = double.Parse(Lat3);
                     App2Lng = double.Parse(Lng3);

                    PointsLatLong(HrpLat, HrpLng, App1Lat, App1Lng, out double RDistance, out double App1Bearing);
                    double DisApp1 = RDistance;
                    double BearApp1 = App1Bearing;

                    PointsLatLong(HrpLat, HrpLng, App2Lat, App2Lng, out double RDistance2, out double App2Bearing);
                    double DisApp2 = RDistance2;
                    double BearApp2 = App2Bearing;
                    double distance, Bearing;
                    double DistanceFromApp1, DistanceFromApp2, BearingFromApp1, BearingFromApp2, getX, getY, getYY;
                    double PointLat,PointLng, DistanceFromFATO,  BearingFromFATO, Elevation;

                    using (OleDbDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                          //  GetBearingDistance getBD = new GetBearingDistance();
                             

                            try
                            {
                                OleDbCommand cmdUpdate = new OleDbCommand(updateQuery, connection);

                                //  getBD.HN = H_Northing.Text;
                                // getBD.HE = H_Easting.Text;


                                WorkLatLong(reader["Easting"].ToString(), reader["Northing"].ToString(),
                                Convert.ToInt32(this.Zone.Text), out string PLat, out string PLng);

                                 PointLat = double.Parse(PLat);
                                 PointLng = double.Parse(PLng);

                                //  double Easting = double.Parse(reader["Easting"].ToString());
                                // double Northing = double.Parse(reader["Northing"].ToString());

                                

                                PointsLatLong(HrpLat, HrpLng, PointLat, PointLng, out RDistance, out Bearing);
                                 DistanceFromFATO = RDistance;
                                 BearingFromFATO = Bearing;

                                PointsLatLong(App1Lat, App1Lng, PointLat, PointLng, out RDistance, out Bearing);
                                DistanceFromApp1 = RDistance;
                                BearingFromApp1 = Bearing;
                               
                                PointsLatLong(App2Lat, App2Lng, PointLat, PointLng, out RDistance, out Bearing);
                                DistanceFromApp2 = RDistance;
                                BearingFromApp2 = Bearing;
                                getYY = 0;


                                 Elevation = double.Parse(reader["Elevation"].ToString());
                              
                                bool nearer = false;

                                nearer = true ? DistanceFromApp1 <= DistanceFromApp2 : DistanceFromApp1 > DistanceFromApp2;

                                if (nearer)
                                { distance = DistanceFromApp1; Bearing = BearingFromApp1;
                                     getX = Math.Round(Math.Abs(distance * (Math.Cos((Bear - Bearing) * (Math.PI) / 180))));
                                     getY = Math.Round(Math.Abs(distance * (Math.Sin((Bear - Bearing) * (Math.PI) / 180))));
                                }
                                else
                                { distance = DistanceFromApp2; Bearing = BearingFromApp2; 

                                  getX = Math.Round(Math.Abs(distance * (Math.Cos((ReverseBear - Bearing) * (Math.PI) / 180))));
                                  getY = Math.Round(Math.Abs(distance * (Math.Sin((ReverseBear - Bearing) * (Math.PI) / 180))));
                                }

                                if (!string.IsNullOrEmpty(Diversion.Text) && !string.IsNullOrEmpty(getX.ToString()) && !string.IsNullOrEmpty(Safety.Text))
                                {
                                    getYY = Math.Round((getX * (double.Parse(Diversion.Text) / 100) + (int.Parse(Safety.Text)) / 2));
                                }
                            
                                string Surface = surface(getYY, getY, getX, distance, int.Parse(Safety.Text), nearer);

                                PE(Surface, distance, getX, getY, Elevation, double.Parse(HRPElevation.Text), int.Parse(Safety.Text),
                                    out double PECatA, out double PECatB, out double PECatC, out double OBA, out double OBB, out double OBC);


                                int ID = int.Parse(reader["id"].ToString());

                                cmdUpdate.Parameters.AddWithValue("@HRPDistance", Math.Round(DistanceFromFATO, 1));
                                cmdUpdate.Parameters.AddWithValue("@HRPBearing", Math.Round(BearingFromFATO, 1));
                                cmdUpdate.Parameters.AddWithValue("@X", Math.Round((getX), 0));
                                cmdUpdate.Parameters.AddWithValue("@Y", Math.Round((getY), 0));
                                cmdUpdate.Parameters.AddWithValue("@YY", Math.Round((getYY), 0));
                                cmdUpdate.Parameters.AddWithValue("@DistApp1", Math.Round((DistanceFromApp1), 1));
                                cmdUpdate.Parameters.AddWithValue("@DistApp2", Math.Round((DistanceFromApp2), 1));
                                cmdUpdate.Parameters.AddWithValue("@RBearingApp1", (Math.Round(BearingFromApp1, 1)));
                                cmdUpdate.Parameters.AddWithValue("@RBearingApp2", (Math.Round(BearingFromApp2, 2)));
                                cmdUpdate.Parameters.AddWithValue("@Surface", Surface);
                                cmdUpdate.Parameters.AddWithValue("@PEA", Math.Round(PECatA, 1));
                                cmdUpdate.Parameters.AddWithValue("@PEB", Math.Round(PECatB, 1));
                                cmdUpdate.Parameters.AddWithValue("@PEC", Math.Round(PECatC, 1));
                                cmdUpdate.Parameters.AddWithValue("@OBA", Math.Round(OBA, 1));
                                cmdUpdate.Parameters.AddWithValue("@OBB", Math.Round(OBB, 1));
                                cmdUpdate.Parameters.AddWithValue("@OBC", Math.Round(OBC, 1));
                                cmdUpdate.Parameters.AddWithValue("@ID", ID);
                                cmdUpdate.ExecuteNonQuery();
                                cmdUpdate.Parameters.Clear();

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
            }

            this.dataGridView1.DefaultCellStyle.Font = new System.Drawing.Font("Tahoma", 9);
            showgrid();
        }

        private double PointsLatLong(double Lat1, double Lng1, double Lat2, double Lng2, 
            out double RDistance, out double Bearing)
        {
            Coordinate c1 = new Coordinate( Lat1,  Lng1);
            Coordinate c2 = new Coordinate(Lat2, Lng2);
            Distance d=  new Distance(c1, c2, CoordinateSharp.Shape.Ellipsoid);
            Bearing= d.Bearing;
            RDistance =  d.Meters;

            return RDistance;
        }
        private void button5_Click(object sender, EventArgs e)
        {


        }

        private void button6_Click(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            if (this.LatD.Text != "" && this.LatM.Text != "" && this.LatS.Text != "" && this.LngD.Text != "" &&
                this.LngM.Text != "" && this.LngS.Text != "")
            {

                double latitude = double.Parse(this.LatD.Text) + (double.Parse(this.LatM.Text) / 60) + (double.Parse(this.LatS.Text) / 3600);
                double longitude = double.Parse(this.LngD.Text) + (double.Parse(this.LngM.Text) / 60) + (double.Parse(this.LngS.Text) / 3600);
                Coordinate c = new Coordinate(latitude, longitude);
                c.FormatOptions.Display_Leading_Zeros = true;
                c.FormatOptions.Round = 3;
                this.H_Easting.Text = Math.Round(c.UTM.Easting, 3).ToString();
                this.H_Northing.Text = Math.Round(c.UTM.Northing, 3).ToString();

            }
        }

        private void ReverseBearing_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void Safety_TextChanged_1(object sender, EventArgs e)
        {
            GetAppoachCoordinates();
        }

        private void ImportData_Click(object sender, EventArgs e)
        {
            string Msg = "Please select the Csv file WITHOUT header in the format as - Obj.No,Object Name,Northing,Easting, Elevation." +
              "The software will calculate Latitude and Longitude itself";
             MessageBox.Show(Msg);
            button2_Click(null, null);
        }

        private void button8_Click(object sender, EventArgs e)
        {
         
            frmSelectRecord frmEdit = new frmSelectRecord();
            frmEdit.Show();
            this.Hide();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            CreateShape frmd = new CreateShape();
            frmd.Show();

        }

        private void CreateShape_Click(object sender, EventArgs e)
        {

            if (
                string.IsNullOrEmpty(this.H_Easting.Text) || string.IsNullOrEmpty(this.H_Northing.Text) ||
                string.IsNullOrEmpty(this.Bearing.Text) || string.IsNullOrEmpty(this.ReverseBearing.Text)
                || string.IsNullOrEmpty(this.Zone.Text)
                || string.IsNullOrEmpty(this.Safety.Text) || string.IsNullOrEmpty(this.Diversion.Text))
            {
                MessageBox.Show("Some values are left blank");
                return;
            }

            string CDir = Directory.GetCurrentDirectory() + "\\Shapefiles\\";


            int myPointIndex = 0;
            int myShapeIndex = 0;

            Shapefile HrpPoint = new Shapefile();
            HrpPoint.CreateNew(CDir + "\\HrpPoint.shp", ShpfileType.SHP_POINT);
            MapWinGIS.Shape HrpShape = new MapWinGIS.Shape();
            
            MapWinGIS.Point myPoint = new Point();
            myPoint.x = Math.Round(double.Parse(H_Easting.Text ), 3);
            myPoint.y = Math.Round(double.Parse(H_Northing.Text), 3);
            
            HrpShape.InsertPoint(myPoint, ref myPointIndex);
            
            HrpPoint.StopEditingShapes(true, true, null);
            HrpPoint.Close();


            Shapefile PolygonShape = new Shapefile();
            PolygonShape.CreateNew(@CDir + "\\FATO.shp", ShpfileType.SHP_POLYGON);
            


            GetNewCoordinatesWithAngle(this.H_Easting.Text, this.H_Northing.Text, double.Parse(this.Safety.Text), double.Parse(this.Bearing.Text)+45, out double App1N, out double App1E);
            string HrpBoxLeftUE = App1E.ToString(); string HrpBoxLeftUN = App1N.ToString();
            GetNewCoordinatesWithAngle(this.H_Easting.Text, this.H_Northing.Text, double.Parse(this.Safety.Text), double.Parse(this.Bearing.Text)-45, out App1N, out App1E);
            string HrpBoxLeftDE = App1E.ToString(); string HrpBoxLeftDN = App1N.ToString();

            GetNewCoordinatesWithAngle(this.H_Easting.Text, this.H_Northing.Text, double.Parse(this.Safety.Text), double.Parse(this.ReverseBearing.Text) + 45, out  App1N, out  App1E);
            string HrpBoxRightUE = App1E.ToString(); string HrpBoxRightUN = App1N.ToString();
            GetNewCoordinatesWithAngle(this.H_Easting.Text, this.H_Northing.Text, double.Parse(this.Safety.Text), double.Parse(this.ReverseBearing.Text) - 45, out App1N, out App1E);
            string HrpBoxRightDE = App1E.ToString(); string HrpBoxRightDN = App1N.ToString();
            
            string FatoString = HrpBoxLeftUE + "," + HrpBoxLeftUN + ";" + HrpBoxLeftDE + "," + HrpBoxLeftDN + ";"
                + HrpBoxRightUE + "," + HrpBoxRightUN + ";" + HrpBoxRightDE + "," + HrpBoxRightDN;


            MapWinGIS.Shape FatoShape = new MapWinGIS.Shape();
            FatoShape.Create(PolygonShape.ShapefileType);
            MakePolygon(FatoString, myPointIndex, myShapeIndex, PolygonShape, FatoShape);
            //string App1BStripE = App1E.ToString(); string App1BStripN = App1N.ToString();


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

            }
        }
        private void CreatePolygon()
        {
            
            
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

    }
}



