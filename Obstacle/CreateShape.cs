using MapWinGIS;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using Point = MapWinGIS.Point;
using System.Linq;


namespace Obstacle
{
    public partial class CreateShape : Form
    {
        string CurrentDir = Directory.GetCurrentDirectory();

        public string TstTopLeft, TstTopRight, TstBottomLeft, TstBottomRight;

        public CreateShape()
        {
            InitializeComponent();

        }

        
        private void CreateShape_Load(object sender, EventArgs e)
        {
            this.Hide();
            button1_Click(null,null);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string CDir = CurrentDir + "\\Shapefiles\\";

            System.IO.DirectoryInfo di = new DirectoryInfo(CDir);

            foreach (FileInfo file in di.GetFiles())
            {
                file.Delete();
            }
        
           
           
       
            Shapefile myShapefile = new Shapefile();
            //Define the path of the new shapefile and geometry type
            myShapefile.CreateNew(@CDir+"\\SurveyPoints.shp", ShpfileType.SHP_POINT);
            //Create new field
            MapWinGIS.Field myField = new Field();
           var objindex= myShapefile.EditAddField("Obj No.", FieldType.STRING_FIELD, 0, 4);
           var objnameindex=   myShapefile.EditAddField("Obj Name", FieldType.STRING_FIELD, 0, 25);
           var ElevationIndex= myShapefile.EditAddField("Elevation", FieldType.DOUBLE_FIELD, 2, 5);
            
            //Add the filed for the shapefile table
            int intFieldIndex = 0;
         
            int myShapeIndex = 0;
             
            openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "CSV files(*.csv)| *.csv";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string filname = openFileDialog1.FileName;

                int myPointIndex = 0;
                //Create Point Shape
                string[] lines = System.IO.File.ReadAllLines(filname);
                dataGridView1.Columns.Add("Obj NO","Object");
                dataGridView1.Columns.Add("Obj Name", "Name");
                dataGridView1.Columns.Add("Elevation", "Elevation");


                int i = 1;
                int j = 0;
                int shpIndex;
                foreach (string line in lines)
                {
                 
                    this.dataGridView1.Rows.Add();
                    
                                       
                    var cols = line.Split(',');
                    MapWinGIS.Shape myShape = new Shape();
                    myShape.Create(ShpfileType.SHP_POINT);
                    MapWinGIS.Point myPoint = new Point();
                    myPoint.x = Math.Round(double.Parse(cols[3]), 2);
                    myPoint.y = Math.Round(double.Parse(cols[2]), 2);
                    myShape.InsertPoint(myPoint, ref myPointIndex);
                    shpIndex = i - 1;
                    myShapefile.EditInsertShape(myShape, ref shpIndex);
                    
                    //myShapefile.EditInsertShape(myShape,j);

                    myShapefile.EditCellValue(objindex, shpIndex, cols[0]);
                    myShapefile.EditCellValue(objnameindex, shpIndex, cols[1]);
                    myShapefile.EditCellValue(ElevationIndex, shpIndex, cols[4]);
                    this.dataGridView1.Rows[i].Cells[0].Value = cols[0];
                    this.dataGridView1.Rows[i].Cells[1].Value = cols[1];
                    this.dataGridView1.Rows[i].Cells[2].Value = cols[4];
                    
                    //  myShapeIndex++;
                    myPointIndex++;
                    i++;
                }
                

                myShapefile.StopEditingShapes(true, true, null);
                myShapefile.Close();
                

                 //Create Polyline Shape
                Shapefile poly = new Shapefile();
                poly.CreateNew(@CDir+"\\Polyline.shp", ShpfileType.SHP_POLYLINE);// ShpfileType.SHP_POLYLINE);
                MapWinGIS.Shape pPolyline =new Shape();
                pPolyline.Create(ShpfileType.SHP_POINT);

               // File.WriteAllText(@CDir + "\\PolyCoordinates.txt", "FieldName,Easting,Northing");

                //Coordinates name and value

                List<string> Coord = new List<string>();

                string head = "Name, Easting,Name,Northing";
                Coord.Add(head);
                
               
                string App1StripRightE, App1StripRightN, App1StripLeftE, App1StripLeftN, 
                    App2StripRightE, App2StripRightN, App2StripLeftE, App2StripLeftN;

               
                //Get Basic Strip Coordinates

                GetNewCoordinatesWithAngle(this.App1East.Text, this.App1North.Text, 60, 
                    double.Parse(this.BackBearing.Text), out double App1N, out double App1E);
                string App1BStripE = App1E.ToString(); string App1BStripN = App1N.ToString();

                Coord.Add("App1BStripE"+","+ App1E.ToString() +"," +"App1BStripN"+","+ App1N.ToString());

                //Get Basic Strip Coordinates

                GetNewCoordinatesWithAngle(this.App2East.Text, this.App2North.Text,60, double.Parse(this.Bearing.Text), 
                    out App1N, out App1E);
                
                string App2BStripE = App1E.ToString(); string App2BStripN = App1N.ToString();
                Coord.Add("App2BStripE " + "," + App1E.ToString() + "," + "App2BStripN" + ","+ App1N.ToString());

                //Get Center line Coordinates

                //  GetNewCoordinatesWithAngle(App1BStripE, App1BStripN,2500, double.Parse(this.BackBearing.Text), out  App1N, out  App1E);
                GetNewCoordinatesWithAngle(App1BStripE, App1BStripN, 15000, double.Parse(this.BackBearing.Text), out App1N, out App1E);
                string AppLineE= App1E.ToString(); string AppLineN = App1N.ToString();
                Coord.Add("AppLineE" + "," + App1E.ToString() + "," + "AppLineN" + ","+ App1N.ToString());
                //Get Center line Coordinates

                //  GetNewCoordinatesWithAngle(App2BStripE, App2BStripN, 2500, double.Parse(this.Bearing.Text), out App1N, out  App1E);
                GetNewCoordinatesWithAngle(App2BStripE, App2BStripN, 15000, double.Parse(this.Bearing.Text), out App1N, out App1E);
                string RAppLineE = App1E.ToString(); string RAppLineN = App1N.ToString();
                Coord.Add("RAppLineE" + "," + App1E.ToString() + "," + "RAppLineN" + ","+ App1N.ToString());
                //Get Runway Strip
                GetNewCoordinatesWithAngle(this.App1East.Text, this.App1North.Text, 
                      double.Parse(this.RunwayStrip.Text)/2, double.Parse(this.Bearing.Text)-90,
                      out  App1N, out   App1E);
                this.textBox3.Text = App1E.ToString();
                this.textBox1.Text = App1N.ToString();

                App1StripRightE= App1E.ToString();
                App1StripRightN= App1N.ToString();

                Coord.Add("App1StripRightE" + "," + App1E.ToString() + "," + "App1StripRightN" + ","+ App1N.ToString());

                GetNewCoordinatesWithAngle(this.App1East.Text, this.App1North.Text,
                    double.Parse(this.RunwayStrip.Text) / 2, double.Parse(this.Bearing.Text) + 90,
                    out App1N, out App1E);

                this.textBox4.Text = App1E.ToString();
                this.textBox2.Text = App1N.ToString();

                App1StripLeftE = App1E.ToString();
                App1StripLeftN = App1N.ToString();
                Coord.Add("App1StripLeftE" + "," + App1E.ToString() + "," + "App1StripLeftN" + ","+ App1N.ToString());

                GetNewCoordinatesWithAngle(App1BStripE, App1BStripN,
                      double.Parse(this.RunwayStrip.Text) / 2, double.Parse(this.Bearing.Text) - 90,
                      out App1N, out App1E);
                string App1BSLE= App1E.ToString();
                string App1BSLN = App1N.ToString();
                
                Coord.Add("App1BSLE" + "," + App1E.ToString() + "," + "App1BSLN" + ","+ App1N.ToString());

                GetNewCoordinatesWithAngle(App1BStripE, App1BStripN,
                     double.Parse(this.RunwayStrip.Text) / 2, double.Parse(this.Bearing.Text) + 90,
                     out App1N, out App1E);
                string App1BSRE = App1E.ToString();
                string App1BSRN = App1N.ToString();

                Coord.Add("App1BSRE" + "," + App1E.ToString() + "," + "App1BSRN" + ","+ App1N.ToString());


                GetNewCoordinatesWithAngle(App2BStripE, App2BStripN,
                  double.Parse(this.RunwayStrip.Text) / 2, double.Parse(this.Bearing.Text) - 90,
                  out App1N, out App1E);
                string App2BSLE = App1E.ToString();
                string App2BSLN = App1N.ToString();

                Coord.Add("App2BSLE" + "," + App1E.ToString() + "," + "App2BSLN" + ","+ App1N.ToString());

                GetNewCoordinatesWithAngle(App2BStripE, App2BStripN,
                     double.Parse(this.RunwayStrip.Text) / 2, double.Parse(this.Bearing.Text) + 90,
                     out App1N, out App1E);
                string App2BSRE = App1E.ToString();
                string App2BSRN = App1N.ToString();

                Coord.Add("App2BSRE" + "," + App1E.ToString() + "," + "App2BSRN" + ","+ App1N.ToString());

               // GetNewCoordinatesWithAngle(App1BSLE, App1BSLN, 2500, double.Parse(this.BackBearing.Text) + 5.710,
                GetNewCoordinatesWithAngle(App1BSLE, App1BSLN, 15000, double.Parse(this.BackBearing.Text) + 5.710,
                      out App1N, out App1E) ;
                string AppUppCord1E = App1E.ToString();
                string AppUppCord1N = App1N.ToString();

                Coord.Add("AppUppCord1E" + "," + App1E.ToString() + "," + "AppUppCord1N" + ","+ App1N.ToString());

                // GetNewCoordinatesWithAngle(App1BSRE, App1BSRN, 2500, double.Parse(this.BackBearing.Text) - 5.710,
                GetNewCoordinatesWithAngle(App1BSRE, App1BSRN, 15000, double.Parse(this.BackBearing.Text) - 5.710,
                    out App1N, out App1E) ;
                string AppUppCord1ELeft = App1E.ToString();
                string AppUppCord1NLeft = App1N.ToString();

                Coord.Add("AppUppCord1ELeft" + "," + App1E.ToString() + "," + "AppUppCord1NLeft" + ","+ App1N.ToString());

                //                GetNewCoordinatesWithAngle(App1BSRE, App1BSRN, 2500, double.Parse(this.BackBearing.Text) ,
                // GetNewCoordinatesWithAngle(App1BStripE, App1BStripN, 2500, double.Parse(this.BackBearing.Text),
                GetNewCoordinatesWithAngle(App1BStripE, App1BStripN, 15000, double.Parse(this.BackBearing.Text),
                   out App1N, out App1E);
                string AppUpperCLineE = App1E.ToString();
                string AppUpperCLineN = App1N.ToString();

                Coord.Add("AppUpperCLineE" + "," + App1E.ToString() + "," + "AppUpperCLineN" + ","+ App1N.ToString());

                //*****************************
                GetNewCoordinatesWithAngle(App2BSLE, App2BSLN, 2500, double.Parse(this.Bearing.Text) - 5.710,
                    out App1N, out App1E) ;
                string AppLwrCord1E = App1E.ToString();
                string AppLwrCord1N = App1N.ToString();

                Coord.Add("AppLwrCord1E" + "," + App1E.ToString() + "," + "AppLwrCord1N" + ","+ App1N.ToString());

                //     GetNewCoordinatesWithAngle(App2BSRE, App2BSRN, 2500, double.Parse(this.Bearing.Text) + 5.710,
                GetNewCoordinatesWithAngle(App2BSRE, App2BSRN, 15000, double.Parse(this.Bearing.Text) + 5.710,
                    out App1N, out App1E) ;
                string AppLwrCord1ELeft = App1E.ToString();
                string AppLwrCord1NLeft = App1N.ToString();

                Coord.Add("AppLwrCord1ELeft" + "," + App1E.ToString() + "," + "AppLwrCord1NLeft" + ","+ App1N.ToString());

                //   GetNewCoordinatesWithAngle(App2BStripE, App2BStripN, 2500, double.Parse(this.Bearing.Text),
                GetNewCoordinatesWithAngle(App2BStripE, App2BStripN, 15000, double.Parse(this.Bearing.Text),
                   out App1N, out App1E) ;
                string AppLwrCLineE = App1E.ToString();
                string AppLwrCLineN = App1N.ToString();

                Coord.Add("AppLwrCLineE" + "," + App1E.ToString() + "," + "AppLwrCLineN" + ","+ App1N.ToString());

                //******************************
                // GetNewCoordinatesWithAngle(App1BSLE, App1BSLN, 225, double.Parse(this.Bearing.Text) - 90,
                GetNewCoordinatesWithAngle(App1BSLE, App1BSLN, 315, double.Parse(this.Bearing.Text) - 90,
                  out App1N, out App1E);

                string App1TSLeftE = App1E.ToString();
                string App1TSLeftN = App1N.ToString();

                Coord.Add("App1TSLeftE" + "," + App1E.ToString() + "," + "App1TSLeftN" + ","+ App1N.ToString());

                //GetNewCoordinatesWithAngle(App1BSRE, App1BSRN,225, double.Parse(this.Bearing.Text) + 90,
                GetNewCoordinatesWithAngle(App1BSRE, App1BSRN, 315, double.Parse(this.Bearing.Text) + 90,
                out App1N, out App1E);

                string App1TSRightE = App1E.ToString();
                string App1TSRightN = App1N.ToString();

                Coord.Add("App1TSRightE" + "," + App1E.ToString() + "," + "App1TSRightN" + ","+ App1N.ToString());

                //GetNewCoordinatesWithAngle(App2BSLE, App2BSLN, 225, double.Parse(this.Bearing.Text) - 90,
                GetNewCoordinatesWithAngle(App2BSLE, App2BSLN, 315, double.Parse(this.Bearing.Text) - 90,
                 out App1N, out App1E);

                string App2TSLeftE = App1E.ToString();
                string App2TSLeftN = App1N.ToString();

                Coord.Add("App2TSLeftE" + "," + App1E.ToString() + "," + "App2TSLeftN" + ","+ App1N.ToString());

                //GetNewCoordinatesWithAngle(App2BSRE, App2BSRN, 225, double.Parse(this.Bearing.Text) + 90,
                GetNewCoordinatesWithAngle(App2BSRE, App2BSRN, 315, double.Parse(this.Bearing.Text) + 90,
                out App1N, out App1E);

                string App2TSRightE = App1E.ToString();
                string App2TSRightN = App1N.ToString();

                Coord.Add("App2TSRightE" + "," + App1E.ToString() + "," + "App2TSRightN" + ","+ App1N.ToString());

                //GetNewCoordinatesWithAngle(App1BStripE, App1BStripN, 1125, double.Parse(this.BackBearing.Text),
                GetNewCoordinatesWithAngle(App1BStripE, App1BStripN, 2250, double.Parse(this.BackBearing.Text),
            out App1N, out App1E);

                string App1TsPointE = App1E.ToString();
                string App1TsPointN = App1N.ToString();

                Coord.Add("App1TsPointE" + "," + App1E.ToString() + "," + "App1TsPointN" + ","+ App1N.ToString());

                //GetNewCoordinatesWithAngle(App2BStripE, App2BStripN, 1125, double.Parse(this.Bearing.Text),
                GetNewCoordinatesWithAngle(App2BStripE, App2BStripN, 2250, double.Parse(this.Bearing.Text),
            out App1N, out App1E);

                string App2TsPointE = App1E.ToString();
                string App2TsPointN = App1N.ToString();

                Coord.Add("App2TsPointE" + "," + App1E.ToString() + "," + "App2TsPointN" + ","+ App1N.ToString());

                GetNewCoordinatesWithAngle(App1TsPointE, App1TsPointN, 152.5, double.Parse(this.BackBearing.Text) + 90, out App1N, out App1E);

                string App1TsFunnelLE = App1E.ToString();
                string App1TsFunnelLN = App1N.ToString();

                Coord.Add("App1TsFunnelLE" + "," + App1E.ToString() + "," + "App1TsFunnelLN" + ","+ App1N.ToString());

                GetNewCoordinatesWithAngle(App2TsPointE, App2TsPointN, 152.5, double.Parse(this.Bearing.Text) - 90, out App1N, out App1E);

                string App2TsFunnelRE = App1E.ToString();
                string App2TsFunnelRN = App1N.ToString();

                Coord.Add("App2TsFunnelRE" + "," + App1E.ToString() + "," + "App2TsFunnelRN" + ","+ App1N.ToString());

                GetNewCoordinatesWithAngle(App1TsPointE, App1TsPointN, 152.5, double.Parse(this.BackBearing.Text) - 90, out App1N, out App1E);

                string App1TsFunnelLLwrE = App1E.ToString();
                string App1TsFunnelLLwrN = App1N.ToString();

                Coord.Add("App1TsFunnelLLwrE" + "," + App1E.ToString() + "," + "App1TsFunnelLLwrN" + ","+ App1N.ToString());

                GetNewCoordinatesWithAngle(App2TsPointE, App2TsPointN, 152.5, double.Parse(this.Bearing.Text) + 90,
          out App1N, out App1E);

                string App2TsFunnelRLwrE = App1E.ToString();
                string App2TsFunnelRLwrN = App1N.ToString();
                Coord.Add("App2TsFunnelRLwrE" + "," + App1E.ToString() + "," + "App2TsFunnelRLwrN" + ","+ App1N.ToString());

                //Get Upper Approach line Coordinates
                GetNewCoordinatesWithAngle(this.textBox3.Text, this.textBox1.Text, 2500, double.Parse(this.BackBearing.Text) - 6,
                      out App1N, out App1E); ;
                string AppBtnCord1E = App1E.ToString();
                string AppBtnCord1N = App1N.ToString();
                Coord.Add("AppBtnCord1E " + "," + App1E.ToString() + "," + "AppBtnCord1N" + ","+ App1N.ToString());


                GetNewCoordinatesWithAngle(this.App2East.Text, this.App2North.Text,
                       double.Parse(this.RunwayStrip.Text) / 2, double.Parse(this.BackBearing.Text) - 90,
                       out App1N, out App1E);

                this.textBox7.Text = App1E.ToString();
                this.textBox5.Text = App1N.ToString();
                App2StripRightE = App1E.ToString();
                App2StripRightN = App1N.ToString();

                Coord.Add("App2StripRightE" + "," + App1E.ToString() + "," + "App2StripRightN" + ","+ App1N.ToString());

                GetNewCoordinatesWithAngle(this.App2East.Text, this.App2North.Text,
                       double.Parse(this.RunwayStrip.Text) / 2, double.Parse(this.BackBearing.Text) + 90,
                       out App1N, out App1E);

                this.textBox8.Text = App1E.ToString();
                this.textBox6.Text = App1N.ToString();
                App2StripLeftE = App1E.ToString();
                App2StripLeftN = App1N.ToString();

                Coord.Add("App2StripLeftE" + "," + App1E.ToString() + "," + "App2StripLeftN" + ","+ App1N.ToString());

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
                    App1TSLeftE + "," + App1TSLeftN +";" + App1BSLE + "," + App1BSLN;

     

                string RectangleTop= App1BSLE + "," + App1BSLN + ";" + App2BSLE + "," + App2BSLN
                    + ";" + App2TSLeftE + "," + App2TSLeftN + ";" + App1TSLeftE + "," + App1TSLeftN;

                string TriangleTopRight = App2TSLeftE + "," + App2TSLeftN + ";" +App2BSLE + "," + App2BSLN
                   + ";" + App2TsFunnelRE + "," + App2TsFunnelRN;
                
            

                string TriangleBottomRight =  App2BSRE+ "," + App2BSRN+";"+ App2TSRightE + "," + App2TSRightN + ";"  
                    + App2TsFunnelRLwrE + "," + App2TsFunnelRLwrN;

                
                string TriangleBottomLeft = App1BSRE + "," + App1BSRN + ";" + App1TSRightE + "," + App1TSRightN + ";"
                   + App1TsFunnelLLwrE + "," + App1TsFunnelLLwrN;

                string RectangleBottom = App1BSRE + "," + App1BSRN+ ";" + App2BSRE+ "," + App2BSRN
                    + ";" + App2TSRightE + "," + App2TSRightN + ";" + App1TSRightE + "," + App1TSRightN;

                //string RunwayStrip = App1BSRE + "," + App1BSRN + ";" + App1BSLE + "," + App1BSLN + ";" + 
                //  App2BSLE +","+ App2BSLN + ";" + App2BSRE + "," + App2BSRN;

                string RunwayStrip = App1StripRightE + "," + App1StripRightN + ";" + App1StripLeftE + "," + App1StripLeftN 
                    + ";" + App2StripRightE + "," + App2StripRightN + ";" + App2StripLeftE + "," + App2StripLeftN;
               
                String leftApporach = App1BSLE + "," + App1BSLN + ";" + AppUppCord1E + "," + AppUppCord1N 
                    + ";" + AppUppCord1ELeft + "," + AppUppCord1NLeft + ";" + App1BSRE + "," + App1BSRN;

                string rightApproach = App2BSLE + "," + App2BSLN + ";" + AppLwrCord1E + "," + AppLwrCord1N + ";" +
                    AppLwrCord1ELeft + "," + AppLwrCord1NLeft + ";" + App2BSRE + "," + App2BSRN;

                Shape PlgLeftApproach= new Shape();
                PlgLeftApproach.Create(PolygonShape.ShapefileType);
                MakePolygon(leftApporach, myPointIndex, myShapeIndex, PolygonShape, PlgLeftApproach);

                Shape PlgRightApproach = new Shape();
                PlgRightApproach.Create(PolygonShape.ShapefileType);
                MakePolygon(rightApproach, myPointIndex, myShapeIndex, PolygonShape, PlgRightApproach);

                Shape PlgTriangleLeft = new Shape();
                PlgTriangleLeft.Create(PolygonShape.ShapefileType);
                MakePolygon(TriangleTopLeft, myPointIndex, myShapeIndex, PolygonShape, PlgTriangleLeft);
                 
                Shape PlgnRectTop = new Shape();
                PlgnRectTop.Create(PolygonShape.ShapefileType);
                MakePolygon(RectangleTop, myPointIndex, myShapeIndex, PolygonShape,PlgnRectTop) ;

                Shape PlgTringleTopRight = new Shape();
                PlgTringleTopRight.Create(PolygonShape.ShapefileType);
                MakePolygon(TriangleTopRight, myPointIndex, myShapeIndex, PolygonShape, PlgTringleTopRight);

                Shape PlgTringleBottomRight = new Shape();
                PlgTringleBottomRight.Create(PolygonShape.ShapefileType);
                MakePolygon(TriangleBottomRight, myPointIndex, myShapeIndex, PolygonShape, PlgTringleBottomRight);

                Shape PlgRectangleBottom = new Shape();
                PlgRectangleBottom.Create(PolygonShape.ShapefileType);
              MakePolygon(RectangleBottom, myPointIndex, myShapeIndex, PolygonShape, PlgRectangleBottom);

                Shape Runway = new Shape();
                Runway.Create(PolygonShape.ShapefileType);
               MakePolygon(RunwayStrip, myPointIndex, myShapeIndex, PolygonShape, Runway);

                Shape PlgTringleBottomLeft = new Shape();
                PlgTringleBottomLeft.Create(PolygonShape.ShapefileType);
                MakePolygon(TriangleBottomLeft, myPointIndex, myShapeIndex, PolygonShape, PlgTringleBottomLeft);

                string App1ApplineL = App1BSRE + "," + App1BSRN + ";" + AppUppCord1ELeft + "," + AppUppCord1NLeft;
               
                string Bs1 = App2StripRightE + "," + App2StripRightN + ";" + App2StripLeftE + "," +
                    App2StripLeftN + ";" + App2BSLE + "," + App2BSLN + ";" + App2BSRE + "," + App2BSRN;

                string Bs2 = App1StripLeftE + "," + App1StripLeftN + ";" + App1BSRE + "," + App1BSRN + ";" +
                App1BSLE + "," + App1BSLN + ";" + App1StripRightE + "," + App1StripRightN;

                Shape ShapeBs1 = new Shape();
                ShapeBs1.Create(PolygonShape.ShapefileType);
                MakePolygon(Bs1, myPointIndex, myShapeIndex, PolygonShape,ShapeBs1);

                Shape ShapeBs2 = new Shape();
                ShapeBs2.Create(PolygonShape.ShapefileType);
                MakePolygon(Bs2, myPointIndex, myShapeIndex, PolygonShape, ShapeBs2);


                string PlyCentreLine = AppUpperCLineE + "," + AppUpperCLineN + ";" + AppLwrCLineE + "," + AppLwrCLineN;
                Shape PlyShapeCentreLine = new Shape();
                PlyShapeCentreLine.Create(poly.ShapefileType);
                MakePolygon(PlyCentreLine, myPointIndex, myShapeIndex, PolygonShape, PlyShapeCentreLine);

                PolygonShape.StopEditingShapes(true, true, null);
                PolygonShape.Close();

                string CentreLine = AppUpperCLineE + "," + AppUpperCLineN+ ";" + AppLwrCLineE+ "," + AppLwrCLineN;
                Shape ShapeCentreLine = new Shape();
                ShapeCentreLine.Create(poly.ShapefileType);
                MakePolyline(CentreLine, myPointIndex, myShapeIndex, poly, ShapeCentreLine, out int PointIndex, out int ShapeIndex);


               
                poly.StopEditingShapes(true, true, null);
                poly.Close();
                MakeIHSPolygonShape();
                MakeCONPolygonShape();

             //   axMap1.DrawCircle(double.Parse(this.App1East.Text), double.Parse(this.App1North.Text), 2500, 1, false);
                MessageBox.Show("Done");
                //
                string utm = "PROJCS[WGS_84_UTM_zone_" + this.Zone.Text + "N";
                string ProjFile = utm + ",GEOGCS['GCS_WGS_1984',DATUM['D_WGS84',SPHEROID['WGS84',6378137,298.257223563]],PRIMEM['Greenwich',0],UNIT['Degree',0.017453292519943295]],PROJECTION['Transverse_Mercator'],PARAMETER['latitude_of_origin',0],PARAMETER['central_meridian',75],PARAMETER['scale_factor',0.9996],PARAMETER['false_easting',500000],PARAMETER['false_northing',0],UNIT['Meter',1]]";
                File.WriteAllText(@CDir + "\\SurveyPoints.prj", ProjFile);
                File.WriteAllText(@CDir + "\\Polyline.prj", ProjFile);
                File.WriteAllText(@CDir + "\\ConPolygon.prj", ProjFile);
                File.WriteAllText(@CDir + "\\ArpPolygon.prj", ProjFile);
                //


            }



        }
        private void MakePolyline(string Coordinates, int myPointIndex, int myShapeIndex, 
            Shapefile poly, Shape pPolyline, out int PointIndex, out int ShapeIndex)
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
            Shapefile poly, Shape pPolyline)
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
            shpPloygon.CreateNew(@CDir+"\\ArpPolygon.shp", ShpfileType.SHP_POLYGON);
            int fldX = shpPloygon.EditAddField("X", FieldType.DOUBLE_FIELD, 9, 12);
            int fldY = shpPloygon.EditAddField("Y", FieldType.DOUBLE_FIELD, 9, 12);
            int fldArea = shpPloygon.EditAddField("area", FieldType.DOUBLE_FIELD, 9, 12);


            double ArpCentreE = double.Parse(this.ArpEast.Text);
            double ArpCentreN = double.Parse(this.ArpNorth.Text);
            double ArpRadius = 2500;
            int myPointIndex = 0;
            int myShapeIndex = 0;
            try
            {
                Shape shp = new Shape();
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
            shpPloygon.CreateNew(CDir+"\\ConPolygon.shp", ShpfileType.SHP_POLYGON);
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
                Shape shp = new Shape();
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
        private void GetNewCoordinatesWithAngle(string He, string Hn,   double Distance,
           double Bearing, out double App1N, out double App1E )
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
        private void GetNewCoordinates(string He, string Hn, string He1, String Hn1, double Distance,
            double Bearing, double BackBearing,
       out double App1N, out double App1E, out double App2E, out double App2N)
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
                CosX = Math.Round(Distance * (Math.Cos(bearRadian)), 3);
                SinX = Math.Round(Distance * (Math.Sin(bearRadian)), 3);

                App1E = Math.Round(double.Parse(He) + SinX, 3);
                App1N = Math.Round(double.Parse(Hn) + CosX, 3);
                ReverseBear = 0;
                ReverseBear = Math.Round(BackBearing, 3);

                bearRadian = Math.Round(Bearing * (Math.PI) / 180, 3);
                CosX = Distance * (Math.Cos(bearRadian));
                SinX = Distance * (Math.Sin(bearRadian));

                App2E = Math.Round(double.Parse(He1) + SinX, 3);
                App2N = Math.Round(double.Parse(Hn1) + CosX, 3);
            }


        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

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
            MapWinGIS.Point lastpoint=new Point();
            lastpoint.x = Xs[2];
            lastpoint.y = Ys[2]; 

            for(i=0;i<=2; i++)
            {
                MapWinGIS.Point b = new MapWinGIS.Point();
                b.x = Xs[i]; b.y = Ys[i];

                if ((b.x == pnt.x) && (b.y == pnt.y))
                    return true;

                if ((b.y == lastpoint.y) && (pnt.y == lastpoint.y))
                {
                    if (lastpoint.x<= pnt.x && pnt.x<= b.x)
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
            return inout ;

        }

             
        

    }
}
