using MapWinGIS;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using Point = MapWinGIS.Point;


namespace Obstacle
{
    public partial class CreateShape : Form
    {
        string CurrentDir = Directory.GetCurrentDirectory();
        

        public CreateShape()
        {
            InitializeComponent();

        }

        private void CreateShape_Load(object sender, EventArgs e)
        {
           

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
                MapWinGIS.Shape pPolyline = new Shape();
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

                GetNewCoordinatesWithAngle(App1BStripE, App1BStripN,2500, double.Parse(this.BackBearing.Text), out  App1N, out  App1E);
                string AppLineE= App1E.ToString(); string AppLineN = App1N.ToString();
                Coord.Add("AppLineE" + "," + App1E.ToString() + "," + "AppLineN" + ","+ App1N.ToString());
                //Get Center line Coordinates

                GetNewCoordinatesWithAngle(App2BStripE, App2BStripN, 2500, double.Parse(this.Bearing.Text), out App1N, out  App1E);
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

                GetNewCoordinatesWithAngle(App1BSLE, App1BSLN, 2500, double.Parse(this.BackBearing.Text) + 5.710,
                      out App1N, out App1E) ;
                string AppUppCord1E = App1E.ToString();
                string AppUppCord1N = App1N.ToString();

                Coord.Add("AppUppCord1E" + "," + App1E.ToString() + "," + "AppUppCord1N" + ","+ App1N.ToString());

                GetNewCoordinatesWithAngle(App1BSRE, App1BSRN, 2500, double.Parse(this.BackBearing.Text) - 5.710,
                    out App1N, out App1E) ;
                string AppUppCord1ELeft = App1E.ToString();
                string AppUppCord1NLeft = App1N.ToString();

                Coord.Add("AppUppCord1ELeft" + "," + App1E.ToString() + "," + "AppUppCord1NLeft" + ","+ App1N.ToString());

                //                GetNewCoordinatesWithAngle(App1BSRE, App1BSRN, 2500, double.Parse(this.BackBearing.Text) ,
                GetNewCoordinatesWithAngle(App1BStripE, App1BStripN, 2500, double.Parse(this.BackBearing.Text),
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

                GetNewCoordinatesWithAngle(App2BSRE, App2BSRN, 2500, double.Parse(this.Bearing.Text) + 5.710,
                    out App1N, out App1E) ;
                string AppLwrCord1ELeft = App1E.ToString();
                string AppLwrCord1NLeft = App1N.ToString();

                Coord.Add("AppLwrCord1ELeft" + "," + App1E.ToString() + "," + "AppLwrCord1NLeft" + ","+ App1N.ToString());

                GetNewCoordinatesWithAngle(App2BStripE, App2BStripN, 2500, double.Parse(this.Bearing.Text),
                   out App1N, out App1E) ;
                string AppLwrCLineE = App1E.ToString();
                string AppLwrCLineN = App1N.ToString();

                Coord.Add("AppLwrCLineE" + "," + App1E.ToString() + "," + "AppLwrCLineN" + ","+ App1N.ToString());

                //******************************
                GetNewCoordinatesWithAngle(App1BSLE, App1BSLN, 225, double.Parse(this.Bearing.Text) - 90,
                  out App1N, out App1E);

                string App1TSLeftE = App1E.ToString();
                string App1TSLeftN = App1N.ToString();

                Coord.Add("App1TSLeftE" + "," + App1E.ToString() + "," + "App1TSLeftN" + ","+ App1N.ToString());

                GetNewCoordinatesWithAngle(App1BSRE, App1BSRN,225, double.Parse(this.Bearing.Text) + 90,
                out App1N, out App1E);

                string App1TSRightE = App1E.ToString();
                string App1TSRightN = App1N.ToString();

                Coord.Add("App1TSRightE" + "," + App1E.ToString() + "," + "App1TSRightN" + ","+ App1N.ToString());

                GetNewCoordinatesWithAngle(App2BSLE, App2BSLN, 225, double.Parse(this.Bearing.Text) - 90,
                 out App1N, out App1E);

                string App2TSLeftE = App1E.ToString();
                string App2TSLeftN = App1N.ToString();

                Coord.Add("App2TSLeftE" + "," + App1E.ToString() + "," + "App2TSLeftN" + ","+ App1N.ToString());

                GetNewCoordinatesWithAngle(App2BSRE, App2BSRN, 225, double.Parse(this.Bearing.Text) + 90,
                out App1N, out App1E);

                string App2TSRightE = App1E.ToString();
                string App2TSRightN = App1N.ToString();

                Coord.Add("App2TSRightE" + "," + App1E.ToString() + "," + "App2TSRightN" + ","+ App1N.ToString());

                GetNewCoordinatesWithAngle(App1BStripE, App1BStripN, 1125, double.Parse(this.BackBearing.Text),
            out App1N, out App1E);

                string App1TsPointE = App1E.ToString();
                string App1TsPointN = App1N.ToString();

                Coord.Add("App1TsPointE" + "," + App1E.ToString() + "," + "App1TsPointN" + ","+ App1N.ToString());

                GetNewCoordinatesWithAngle(App2BStripE, App2BStripN, 1125, double.Parse(this.Bearing.Text),
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

                string RunwayStrip = App1BSRE + "," + App1BSRN + ";" + App1BSLE + "," + App1BSLN + ";" + 
                    App2BSLE +","+ App2BSLN + ";" + App2BSRE + "," + App2BSRN;

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
                MakePolygon(RunwayStrip, myPointIndex, myShapeIndex, PolygonShape, PlgRectangleBottom);

                Shape PlgTringleBottomLeft = new Shape();
                PlgTringleBottomLeft.Create(PolygonShape.ShapefileType);
                MakePolygon(TriangleBottomLeft, myPointIndex, myShapeIndex, PolygonShape, PlgTringleBottomLeft);

                string App1ApplineL = App1BSRE + "," + App1BSRN + ";" + AppUppCord1ELeft + "," + AppUppCord1NLeft;
               
                string Bs1 = App2StripRightE + "," + App2StripRightN + ";" + App2StripLeftE + "," +
                    App2StripLeftN + ";" + App2BSLE + "," + App2BSLN + ";" + App2BSRE + "," + App2BSRN;
                string Bs2 = App1StripLeftE + "," + App1StripRightE + ";" + App1BSRE + "," + App1BSRN + ";" +
                App1BSLE + "," + App1BSLN + ";" + App1StripRightE + "," + App1StripRightN;

                Shape Bs1 = new Shape();
                Bs1.Create(PolygonShape.ShapefileType);
                MakePolygon(Bs1, myPointIndex, myShapeIndex, PolygonShape,Bs1);

                Shape Bs2 = new Shape();
                Bs2.Create(PolygonShape.ShapefileType);
                MakePolygon(Bs2, myPointIndex, myShapeIndex, PolygonShape, Bs2);

             
                PolygonShape.StopEditingShapes(true, true, null);
                PolygonShape.Close();

                //

                string JoinRW1 = App1StripLeftE + "," + App1StripLeftN + ";" + App1StripRightE + "," + App1StripRightN;
     

                Shape JoinRW1Shape = new Shape();
                JoinRW1Shape.Create(poly.ShapefileType);
                MakePolyline(JoinRW1, myPointIndex, myShapeIndex,  poly, JoinRW1Shape, out int PointIndex, out int ShapeIndex);

                string JoinRW2 = App2StripLeftE + "," + App2StripLeftN + ";" + App2StripRightE + "," + App2StripRightN;
                Shape JoinRW2Shape = new Shape();
                JoinRW2Shape.Create(poly.ShapefileType);
                MakePolyline(JoinRW2, myPointIndex, myShapeIndex, poly, JoinRW2Shape, out  PointIndex, out  ShapeIndex);
                
                string AppLeftCentre = App1BStripE + "," + App1BStripN + ";" + AppUpperCLineE + "," + AppUpperCLineN;
                Shape pPolyline13 = new Shape();
                pPolyline13.Create(poly.ShapefileType);
                MakePolyline(AppLeftCentre, myPointIndex, myShapeIndex, poly, pPolyline13, out PointIndex, out ShapeIndex);


                /*
                                string App1TSLF = App1TSLeftE + "," + App1TSLeftN + ";" + App1TsFunnelLE + "," + App1TsFunnelLN;
                                string App1TSRFLwr = App1TSRightE + "," + App1TSRightN + ";" + App1TsFunnelLLwrE + "," + App1TsFunnelLLwrN;
                                string App2TSRF = App2TSLeftE + "," + App2TSLeftN + ";" + App2TsFunnelRE + "," + App2TsFunnelRN;
                                string App2TSRFLwr = App2TSRightE + "," + App2TSRightN + ";" + App2TsFunnelRLwrE + "," + App2TsFunnelRLwrN;


                                string xyz = this.textBox3.Text + "," + this.textBox1.Text + ";" + this.textBox8.Text + "," + this.textBox6.Text;
                                string xyz1 = this.textBox8.Text + "," + this.textBox6.Text + ";" + this.App2East.Text + "," + this.App2North.Text;
                                string xyz2 =  this.textBox4.Text + "," + this.textBox2.Text + ";" + this.textBox7.Text + "," + this.textBox5.Text;
                                string App1BS = this.App1East.Text + "," + this.App1North.Text + ";" + App1BStripE+ "," + App1BStripN;
                                string App2BS = this.App2East.Text + "," + this.App2North.Text + ";" + App2BStripE + "," + App2BStripN;
                                string App1BSLine = App1BSLE + "," + App1BSLN + ";" + App1BSRE + "," + App1BSRN;
                                string App2BSLine = App2BSLE + "," + App2BSLN + ";" + App2BSRE + "," + App2BSRN;
                                string App1JoinR = App1BSLE + "," + App1BSLN + ";" + App1StripRightE + "," + App1StripRightN;
                                string App1JoinL = App1BSRE + "," + App1BSRN + ";" + App1StripLeftE + "," + App1StripLeftN;
                                string App2JoinR = App2BSLE + "," + App2BSLN + ";" + App2StripLeftE + "," + App2StripLeftN;
                                string App2JoinL = App2BSRE + "," + App2BSRN + ";" + App2StripRightE + "," + App2StripRightN;
                                //    *************  
                                 string App1TSL = App1BSLE + "," + App1BSLN + ";" + App1TSLeftE+ "," + App1TSLeftN;
                                 string App1TSR = App1BSRE + "," + App1BSRN + ";" + App1TSRightE + "," + App1TSRightN;


                                string AppJoinUpper1 = AppUppCord1E + "," + AppUppCord1N + ";" + AppUpperCLineE + "," + AppUpperCLineN;
                                string AppJoingUpper2 = AppUppCord1ELeft + "," + AppUppCord1NLeft + ";" + AppUpperCLineE + "," + AppUpperCLineN;
                                string AppJoinLwr1 = AppLwrCord1E + "," + AppLwrCord1N + ";" + AppLwrCLineE + "," + AppLwrCLineN;
                                string AppJoinLwr2 = AppLwrCord1ELeft + "," + AppLwrCord1NLeft + ";" + AppLwrCLineE + "," + AppLwrCLineN;

                                string App2TSL = App2BSRE + "," + App2BSRN + ";" + App2TSLeftE + "," + App2TSLeftN;
                                string App2TSR = App2BSLE + "," + App2BSLN + ";" + App2TSRightE + "," + App2TSRightN;

                                string  TSJoinR = App1TSRightE + "," + App1TSRightN+ ";" + App2TSRightE + "," + App2TSRightN;
                                string  TSJoinL = App1TSLeftE + "," + App1TSLeftN + ";" + App2TSLeftE + "," + App2TSLeftN;

                                string App1ApplineR = App1BSLE+ "," + App1BSLN+ ";" + AppUppCord1E + "," + AppUppCord1N;
                                string App1ApplineL = App1BSRE + "," + App1BSRN + ";" + AppUppCord1ELeft + "," + AppUppCord1NLeft;
                                string AppUpperCentre = App1BStripE + "," + App1BStripN + ";" + AppUpperCLineE + "," + AppUpperCLineN;
                                //string AppAdd = App1ApplineR + "," + App1ApplineL;

                                string App2ApplineR = App2BSLE + "," + App2BSLN + ";" + AppLwrCord1E + "," + AppLwrCord1N;
                                string App2ApplineL = App2BSRE + "," + App2BSRN + ";" + AppLwrCord1ELeft + "," + AppLwrCord1NLeft;
                                string AppLwrCentre = App2BStripE + "," + App2BStripN + ";" + AppLwrCLineE + "," + AppLwrCLineN;

                                string App2TSRightUp= App2BStripE+","+ App2BStripN +";"+ App2TsPointE +","+ App2TsPointN;

                                string trial = App2ApplineR +","+ App2ApplineL+","+ AppLwrCentre;

                                string P1 = this.App1East.Text + "," + this.App1North.Text + ";" + this.App2East.Text + ","
                                    + this.App2North.Text;

                                string RWidth1 = this.App1East.Text + "," + this.App1North.Text + ";"+ textBox3.Text + "," + textBox1.Text;

                                string RWidth2 = textBox4.Text + "," + textBox2.Text+";"+this.App1East.Text + "," + this.App1North.Text ;

                                string RWidth3 = this.App2East.Text + "," + this.App2North.Text + ";" + textBox7.Text + "," + textBox5.Text;
                                string RWidth4 = this.App2East.Text + "," + this.App2North.Text + ";" + textBox8.Text + "," + textBox6.Text;

                                string RWidth5 =   textBox3.Text + "," + textBox1.Text+";"+ textBox8.Text + "," + textBox6.Text;
                                string RWidth6 = textBox4.Text + "," + textBox2.Text + ";" + textBox7.Text + "," + textBox5.Text; ;


                                string AppLine_1 =  this.App1East.Text+","+this.App1North.Text+";"+AppLineE + "," + AppLineN ;
                                string AppLine_2 = this.App2East.Text + "," + this.App2North.Text+";"+RAppLineE + "," + RAppLineN;
                                string AppBottom= this.App1East.Text + "," + this.App1North.Text + ";" + AppLineE + "," + AppLineN;

                                pPolyline = new Shape();
                                pPolyline.Create(poly.ShapefileType);

                               MakePolyline(P1, myPointIndex, myShapeIndex, poly, pPolyline, out int PointIndex,   out  int ShapeIndex);


                                Shape pPolyline1 = new Shape();
                                pPolyline1.Create(poly.ShapefileType);
                                MakePolyline(RWidth1, myPointIndex, myShapeIndex, poly, pPolyline1, out    PointIndex, out   ShapeIndex);

                                 Shape pPolyline2 = new Shape();
                                 pPolyline2.Create(poly.ShapefileType);
                                 MakePolyline(xyzz, myPointIndex, myShapeIndex, poly, pPolyline2, out PointIndex, out ShapeIndex);

                               Shape pPolyline3 = new Shape();
                               pPolyline3.Create(poly.ShapefileType);
                               MakePolyline(xyz, myPointIndex, myShapeIndex, poly, pPolyline3, out PointIndex, out ShapeIndex);

                             Shape pPolyline4 = new Shape();
                             pPolyline4.Create(poly.ShapefileType);
                             MakePolyline(xyz1, myPointIndex, myShapeIndex, poly, pPolyline4, out PointIndex, out ShapeIndex);

                            Shape pPolyline5 = new Shape();
                            pPolyline5.Create(poly.ShapefileType);
                             MakePolyline(xyz2, myPointIndex, myShapeIndex, poly, pPolyline5, out PointIndex, out ShapeIndex);

                                Shape pPolyline6 = new Shape();
                                pPolyline6.Create(poly.ShapefileType);
                                MakePolyline(App2EOR, myPointIndex, myShapeIndex, poly, pPolyline6, out PointIndex, out ShapeIndex);

                                Shape pPolyline7 = new Shape();
                                pPolyline7.Create(poly.ShapefileType);
                                MakePolyline(App1BS, myPointIndex, myShapeIndex, poly, pPolyline7, out PointIndex, out ShapeIndex);

                                Shape pPolyline8 = new Shape();
                                pPolyline8.Create(poly.ShapefileType);
                                MakePolyline(App2BS, myPointIndex, myShapeIndex, poly, pPolyline8, out PointIndex, out ShapeIndex);

                                Shape pPolyline9 = new Shape();
                                pPolyline9.Create(poly.ShapefileType);
                                MakePolyline(App1BSLine, myPointIndex, myShapeIndex, poly, pPolyline9, out PointIndex, out ShapeIndex);

                                 Shape pPolyline10 = new Shape();
                                 pPolyline10.Create(poly.ShapefileType);
                                MakePolyline(App2BSLine, myPointIndex, myShapeIndex, poly, pPolyline10, out PointIndex, out ShapeIndex);

                                Shape pPolyline11 = new Shape();
                                pPolyline11.Create(poly.ShapefileType);
                                MakePolyline(App1ApplineR, myPointIndex, myShapeIndex, poly, pPolyline11, out PointIndex, out ShapeIndex);

                                Shape pPolyline12 = new Shape();
                                pPolyline12.Create(poly.ShapefileType);
                                MakePolyline(App1ApplineL, myPointIndex, myShapeIndex, poly, pPolyline12, out PointIndex, out ShapeIndex);

                                Shape pPolyline13 = new Shape();
                                pPolyline13.Create(poly.ShapefileType);
                                MakePolyline(AppUpperCentre, myPointIndex, myShapeIndex, poly, pPolyline13, out PointIndex, out ShapeIndex);

                              //  string trial = App2ApplineR + "," + App2ApplineL + "," + AppLwrCentre;

                                Shape pPolyline14 = new Shape();
                                pPolyline14.Create(poly.ShapefileType);
                                MakePolyline(App2ApplineR, myPointIndex, myShapeIndex, poly, pPolyline14, out PointIndex, out ShapeIndex);

                                Shape pPolyline15 = new Shape();
                                pPolyline15.Create(poly.ShapefileType);
                                MakePolyline(App2ApplineL, myPointIndex, myShapeIndex, poly, pPolyline15, out PointIndex, out ShapeIndex);

                                Shape pPolyline16 = new Shape();
                                pPolyline16.Create(poly.ShapefileType);
                                MakePolyline(AppLwrCentre, myPointIndex, myShapeIndex, poly, pPolyline16, out PointIndex, out ShapeIndex);

                                Shape pPolyline17 = new Shape();
                                pPolyline17.Create(poly.ShapefileType);
                                MakePolyline(App1JoinR, myPointIndex, myShapeIndex, poly, pPolyline17, out PointIndex, out ShapeIndex);

                                Shape pPolyline18 = new Shape();
                                pPolyline18.Create(poly.ShapefileType);
                                MakePolyline(App1JoinL, myPointIndex, myShapeIndex, poly, pPolyline18, out PointIndex, out ShapeIndex);

                                Shape pPolyline19 = new Shape();
                                pPolyline19.Create(poly.ShapefileType);
                                MakePolyline(App2JoinR, myPointIndex, myShapeIndex, poly, pPolyline19, out PointIndex, out ShapeIndex);

                                Shape pPolyline20 = new Shape();
                                pPolyline20.Create(poly.ShapefileType);
                                MakePolyline(App2JoinL, myPointIndex, myShapeIndex, poly, pPolyline20, out PointIndex, out ShapeIndex);


                                Shape pPolyline211 = new Shape();
                                pPolyline211.Create(poly.ShapefileType);
                                MakePolyline(App1TSR, myPointIndex, myShapeIndex, poly, pPolyline211, out PointIndex, out ShapeIndex);

                                Shape pPolyline21 = new Shape();
                                pPolyline21.Create(poly.ShapefileType);
                                MakePolyline(App1TSL, myPointIndex, myShapeIndex, poly, pPolyline21, out PointIndex, out ShapeIndex);


                              //  Shape pPolyline29 = new Shape();
                              //  pPolyline29.Create(poly.ShapefileType);
                             //   MakePolyline(App2TsLineR, myPointIndex, myShapeIndex, poly, pPolyline29, out PointIndex, out ShapeIndex);

                                Shape pPolyline22 = new Shape();
                                       pPolyline22.Create(poly.ShapefileType);
                                      MakePolyline(App1TSL, myPointIndex, myShapeIndex, poly, pPolyline22, out PointIndex, out ShapeIndex);

                                Shape pPolyline22a = new Shape();
                                pPolyline22a.Create(poly.ShapefileType);
                                MakePolyline(App1TSLF, myPointIndex, myShapeIndex, poly, pPolyline22a, out PointIndex, out ShapeIndex);

                                Shape pPolyline22b = new Shape();
                                pPolyline22b.Create(poly.ShapefileType);
                                MakePolyline(App2TSRF, myPointIndex, myShapeIndex, poly, pPolyline22b, out PointIndex, out ShapeIndex);


                                Shape pPolyline22c = new Shape();
                                pPolyline22c.Create(poly.ShapefileType);
                                MakePolyline(App1TSRFLwr, myPointIndex, myShapeIndex, poly, pPolyline22c, out PointIndex, out ShapeIndex);


                                Shape pPolyline22d = new Shape();
                                pPolyline22d.Create(poly.ShapefileType);
                                MakePolyline(App2TSRFLwr, myPointIndex, myShapeIndex, poly, pPolyline22d, out PointIndex, out ShapeIndex);

                                Shape pPolylineAppUpper1 = new Shape();
                                pPolylineAppUpper1.Create(poly.ShapefileType);
                                MakePolyline(AppJoinUpper1, myPointIndex, myShapeIndex, poly, pPolylineAppUpper1, out PointIndex, out ShapeIndex);

                                Shape pPolylineAppUpper2 = new Shape();
                                pPolylineAppUpper2.Create(poly.ShapefileType);
                                MakePolyline(AppJoingUpper2, myPointIndex, myShapeIndex, poly, pPolylineAppUpper2, out PointIndex, out ShapeIndex);

                                Shape pPolylineAppJoinLwr1 = new Shape();
                                pPolylineAppJoinLwr1.Create(poly.ShapefileType);
                                MakePolyline(AppJoinLwr1, myPointIndex, myShapeIndex, poly, pPolylineAppJoinLwr1, out PointIndex, out ShapeIndex);

                                Shape pPolylineAppJoinLwr2 = new Shape();
                                pPolylineAppJoinLwr2.Create(poly.ShapefileType);
                                MakePolyline(AppJoinLwr2, myPointIndex, myShapeIndex, poly, pPolylineAppJoinLwr2, out PointIndex, out ShapeIndex);



                                Shape pPolyline221 = new Shape();
                                pPolyline221.Create(poly.ShapefileType);
                         //       MakePolyline(App2TSRF, myPointIndex, myShapeIndex, poly, pPolyline221, out PointIndex, out ShapeIndex);

                                Shape pPolyline23 = new Shape();
                                        pPolyline23.Create(poly.ShapefileType);
                                MakePolyline(App2TSR, myPointIndex, myShapeIndex, poly, pPolyline23, out PointIndex, out ShapeIndex);

                                Shape pPolyline123 = new Shape();
                                pPolyline123.Create(poly.ShapefileType);
                                MakePolyline(App2TSRightUp, myPointIndex, myShapeIndex, poly, pPolyline123, out PointIndex, out ShapeIndex);


                                        Shape pPolyline24 = new Shape();
                                        pPolyline24.Create(poly.ShapefileType);
                                        MakePolyline(App2TSL, myPointIndex, myShapeIndex, poly, pPolyline24, out PointIndex, out ShapeIndex);


                                        Shape pPolyline25 = new Shape();
                                        pPolyline25.Create(poly.ShapefileType);
                                        MakePolyline(TSJoinL, myPointIndex, myShapeIndex, poly, pPolyline25, out PointIndex, out ShapeIndex);


                                        Shape pPolyline26 = new Shape();
                                        pPolyline26.Create(poly.ShapefileType);
                                        MakePolyline(TSJoinR, myPointIndex, myShapeIndex, poly, pPolyline26, out PointIndex, out ShapeIndex);


                                MapWinGIS.Shape arp = new Shape();
                                arp.Create(ShpfileType.SHP_POINT);
                                MapWinGIS.Point arpPoint = new Point();
                                arpPoint.x = double.Parse(this.ArpEast.Text);
                                arpPoint.y = double.Parse(this.ArpNorth.Text);
                                arp.InsertPoint(arpPoint, ref myPointIndex);
                                poly.EditInsertShape(arp, ref myShapeIndex);

                                // var shpIndex = myShapefile.EditAddShape(myShape);
                                /*  
                                  axMap1.Projection = tkMapProjection.PROJECTION_NONE;
                                  int handle = axMap1.AddLayer(pPolyline,false);
                                  axMap1.ZoomToLayer(handle);
                                  axMap1.DrawCircleEx(handle,double.Parse(this.App1East.Text), double.Parse(this.App1North.Text), 1000, 16, true);
                                  axMap1.Redraw();
                                */

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

        private void button2_Click(object sender, EventArgs e)
        {
            MakeIHSPolygonShape();
        }

     
    }
}
