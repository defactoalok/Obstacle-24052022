using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using Microsoft.Office;
using System.Windows.Forms;
using System.IO;
using Excel = Microsoft.Office.Interop.Excel;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.VisualBasic;

namespace Obstacle
{
    public partial class Form1 : Form
    {
        public object SafetyArea { get; private set; }

        public Form1()
        {
            InitializeComponent();
            this.FileName.Visible = false;
            this.SaveFile.Visible = false;
           

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
            splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            showgrid();
        }



        private void showgrid()
        {

            string OledbConnectString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source= ObstaclesData.accdb";
            string getRecords = "SELECT   id, [SL NO], OBJECT, NORTHING, EASTING, LATITUDE, LONGITUDE," +
                 " Elevation, HRPDistance,HRPBearing, Surface, X, Y, YFunnel as YY, PEA, OBA, PEB, OBB, PEC, " +
                 "OBC FROM SurveyData";
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
                        dataGridView1.DataSource = ds.Tables[0];
                        dataGridView1.Refresh();
                        //  this.dataGridView1.Columns[0].Visible= false;
                        this.dataGridView1.Columns[1].Frozen = true;
                        this.dataGridView1.Columns[2].Frozen = true;
                        //                        this.dataGridView1.Columns[3].Frozen = true;
                    }
                   
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Exception Message: " + ex.Message);
                }
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            string DeleteData = "Delete from SurveyData";
            string appendData = "Insert into SurveyData([SL NO], [OBJECT], LATITUDE, LONGITUDE, NORTHING, EASTING, Elevation ) " +
                @"Values(@SL,@Object,@Lat,@long,@North,@East,@Elev)";
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
                                cmd.Parameters.AddWithValue("@Lat", cols[2]);
                                cmd.Parameters.AddWithValue("@long", cols[3]);
                                cmd.Parameters.AddWithValue("@North", double.Parse(cols[4]));
                                cmd.Parameters.AddWithValue("@East", double.Parse(cols[5]));
                                cmd.Parameters.AddWithValue("@Elev", double.Parse(cols[6]));
                                cmd.ExecuteNonQuery();
                                cmd.Parameters.Clear();


                            }


                        }

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Exception Message: " + ex.Message);
                }
            MessageBox.Show("Finish");
            showgrid();
            WindowState = FormWindowState.Maximized;
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
                
                if ( this.H_Easting.Text.ToString()!= ""  &&  this.H_Northing.Text.ToString() !="" 
                &&  this.Bearing.Text.ToString()!="" &&  this.Safety.Text.ToString()!="")
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

           
            if(Bearing > 0)
            {
                double bearRadian = Math.Round(Bearing * (Math.PI) / 180,2);
                CosX = Math.Round(Distance *   (Math.Cos(bearRadian)),2);
                 SinX = Math.Round(Distance *  (Math.Sin(bearRadian)),2) ;
                
                App1E = Math.Round( double.Parse(He) + SinX,2) ;
                App1N = Math.Round( double.Parse(Hn) + CosX,2);
                ReverseBear = 0;
                ReverseBear =Math.Round(double.Parse(this.ReverseBearing.Text),2);

                bearRadian = Math.Round( ReverseBear * (Math.PI) / 180,2);
                CosX = Distance *  (Math.Cos(bearRadian)) ;
                 SinX = Distance *  (Math.Sin(bearRadian));
              
                App2E = Math.Round( double.Parse(He) +  SinX,2) ;
                App2N =  Math.Round(double.Parse(Hn) +  CosX,2) ;
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
                Bearing =Math.Abs( Math.Atan(( (EMinusE) /  (NMinusN))) * (180 / Math.PI));

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
            if ( double.Parse(this.Bearing.Text) > 180)
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

           label4.Text= SetApp(double.Parse(this.Bearing.Text),out NewBearing);
           label9.Text= SetApp(double.Parse(this.ReverseBearing.Text), out  NewBearing);


            double Bear =  double.Parse(this.Bearing.Text); // Math.Round(CalBearing(H_Northing.Text, H_Easting.Text, App1North.Text, App1East.Text));
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
                                MessageBox.Show(ex.Message);

                            }

                        }

                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);

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

            if (YY >= Y) // && YY<=double.Parse(this.OEdge.Text))
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
                    OBA = PECatA - Elevation;
                    if (getX <= 245)
                    {
                        PECatB = (getX * .08) + HrpElevation;
                        OBB = PECatB - Elevation;
                    }
                    if (getX > 245 && getY <= 1075)
                    {
                        PECatB = 19.6 + (getX * (16 / 100)) + HrpElevation;
                        OBB = PECatB - Elevation;
                    }
                    if (getX <= 1220)
                    {
                        PECatC = (getX * (12.5 / 100)) + HrpElevation;
                        OBC = PECatC - Elevation;

                    }

                }
                return PECatB;
            }
            if ( str != "APP")
            {
                PECatA = (Distance - (SafetyArea / 2)) / 2 + HrpElevation;
                PECatB = (Distance - (SafetyArea / 2)) / 2 + HrpElevation;
                PECatC = (Distance - (SafetyArea / 2)) / 2 + HrpElevation;
                OBA = PECatA - Elevation;
                OBB = PECatB - Elevation;
                OBC = PECatC - Elevation;
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

                    EMinusE =  PointE - App1E;
                    NMinusN = PointN - App1N;
                    double EnMinus = Math.Abs(EMinusE / NMinusN) ;

                    BearingApp1 = Math.Atan(EnMinus) * 180 / Math.PI;

                    double PointAzimuth1 = SendAzimuth(EMinusE, NMinusN, BearingApp1, out double Azim1);
                    //   MessageBox.Show(EMinusE.ToString() + " " + NMinusN.ToString().ToString());

                    DistAPP1 = Math.Round(Math.Sqrt(Math.Pow(NMinusN, 2) + Math.Pow(EMinusE, 2)),1);

                    EMinusE = PointE - App2E;
                    NMinusN = PointN - App2N;

                    EnMinus = Math.Abs((EMinusE / NMinusN));

                    BearingApp2 = Math.Atan(EnMinus) * 180 / Math.PI;

                    double PointAzimuth2 = SendAzimuth(EMinusE, NMinusN, BearingApp2, out double Azim2);
                    // MessageBox.Show(EMinusE.ToString() + " " + NMinusN.ToString().ToString());

                    //   MessageBox.Show(DistAPP1.ToString()+" "+DistApp2.ToString()+" "+ PointAzimuth1.ToString() + " " + PointAzimuth2.ToString());
                    DistApp2 = Math.Round(Math.Sqrt(Math.Pow(NMinusN, 2) + Math.Pow(EMinusE, 2)),1);

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
                 && this.App2North.Text != "" && this.RotorDia.Text != "" && this.OEdge.Text != "")
            {

                button3_Click(null, null);
                foreach (DataGridViewRow Myrow in dataGridView1.Rows)
                {
                    if (Myrow.IsNewRow) continue;
                    if (dataGridView1.Rows.Count > 0 && 
                        !string.IsNullOrEmpty(Myrow.Cells[10].Value.ToString())) { 
                    string surf = Myrow.Cells[10].Value.ToString();
                   
                    try
                   {
                           
                        if (Convert.ToDouble(Myrow.Cells[15].Value) < 0
                            || Convert.ToDouble(Myrow.Cells[17].Value) < 0
                            || Convert.ToDouble(Myrow.Cells[19].Value) < 0)
                           
                        { if (!string.IsNullOrEmpty(surf) && surf.Substring(0, 2) == "AP")
                            { Myrow.DefaultCellStyle.BackColor = System.Drawing.Color.Pink; }
                        }

                   } 
                    catch (Exception ex)
                    {
                      MessageBox.Show(ex.Message);
                    }
                    
                }

                }
            }
        }
        private void aasasToolStripMenuItem_Click(object sender, EventArgs e)
        {

            button2_Click(null, null);
            showgrid();
        }

        private void saveAsButton_Click(object sender, EventArgs e)
        {
            this.FileName.Visible = true;
            this.SaveFile.Visible = true;

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
                        this.FileName.Visible = false;
                        this.SaveFile.Visible = false;

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
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
            if (!string.IsNullOrEmpty(this.Bearing.Text) )

         ///       if ( double.Parse(this.Bearing.Text) < 0 && double.Parse(this.Bearing.Text) > 360 )
            {
               // GetAppoachCoordinates();
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
           
           
            if ((!string.IsNullOrEmpty(this.Bearing.Text) && double.Parse(this.Bearing.Text) < 0 )||
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
       
          
          if(!string.IsNullOrEmpty(NewFile) && NewFile.Length > 0)
            { 
          try { 
            {
                        string appendMaster = "Insert into Master(H_Northing,H_Easting,App1East,App1North,App2East,App2North,HRPElevation,Safety," +
                                    "Diversion,Bearing,ReverseBearing) Values(@H_Northing,@H_Easting,@App1East,@App2North,@App2East,@App2North," +
                                    "@HRPElevation,@Safety, @Diversion,@Bearing,@ReverseBearing)";


                string appendData = " Select * into "+ NewFile+" from SurveyData";
            
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
                }catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
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
            RotorDia_TextChanged(null,null);
        }

        private void Diversion_TextChanged(object sender, EventArgs e)
        {
            RotorDia_TextChanged(null, null);
        }
    }
}



