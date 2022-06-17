using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MapWinGIS;
using AxMapWinGIS;
using System.IO;
using System.Data.OleDb;
using GMap;
namespace Obstacle
{
    public partial class DisplayShapes : Form
    {
        Shapefile sfPoint = new Shapefile();
        public DisplayShapes()
        {
            InitializeComponent();
        }

        private void DisplayShapes_Load(object sender, EventArgs e)
        {
            Shapefile sfPoly = new Shapefile();
            Shapefile sfCon = new Shapefile();
            Shapefile sfArp = new Shapefile();
            Shapefile sPolygon = new Shapefile();

            string CurrentDir = Directory.GetCurrentDirectory();
            string Cdir = CurrentDir + "\\ShapeFiles\\";
            string cPolygon = Cdir + "\\TstPolygon.shp";
            string cPointPath = Cdir + "\\SurveyPoints.shp";
            string cPolyPath = Cdir + "\\Polyline.shp";
            string cCon = Cdir + "\\ConPolygons.shp";
            string cArp = Cdir + "\\ArpPolygon.shp";

            //  axMap1.Projection = tkMapProjection.PROJECTION_GOOGLE_MERCATOR ;// tkMapProjection.PROJECTION_CUSTOM;
            axMap1.KnownExtents = tkKnownExtents.keIndia;
            //      axMap1.ZoomBehavior = tkZoomBehavior.zbUseTileLevels;
            axMap1.Tiles.Provider =  tkTileProvider.OpenStreetMap;
            
            sfPoint.Open(cPointPath, null);
            sfPoly.Open(cPolyPath, null);
            sPolygon.Open(cPolygon, null);
            sfPoint.DefaultDrawingOptions.PointSize = 5;
            sfPoint.DefaultDrawingOptions.PointShape = tkPointShapeType.ptShapeCircle;
            //   sfCon.Open(cCon, null);
            //    sfArp.Open(cArp, null);
            //   sfCon.DefaultDrawingOptions.FillBgTransparent=true;
            //   sfArp.DefaultDrawingOptions.FillBgTransparent = true;
            axMap1.GrabProjectionFromData = true;
            //    axMap1.AddLayer(sfCon, true);
            // axMap1.AddLayer(sfArp, true);
            axMap1.AddLayer(sfPoint, true);
            //axMap1.AddLayer(sfPoly, true);
            axMap1.AddLayer(sPolygon, true);

           
            axMap1.TileProvider = tkTileProvider.OpenStreetMap;
            sfPoint.Selectable = true;
            sPolygon.Selectable = false;
            sPolygon.DefaultDrawingOptions.FillTransparency = 0;
            axMap1.SendMouseDown = true;
            axMap1.CursorMode = tkCursorMode.cmIdentify;
            axMap1.ShapeIdentified += axMap1ShapeIdentified;


            string strDSN = "Provider = Microsoft.ACE.OLEDB.12.0; Data Source = ObstaclesData.accdb";
            dataGridView1.AllowUserToAddRows = false;

            string strSQL = "SELECT * FROM RWYSurveyData";
            // create Objects of ADOConnection and ADOCommand  
            OleDbConnection myConn = new OleDbConnection(strDSN);
            OleDbDataAdapter myCmd = new OleDbDataAdapter(strSQL, myConn);
            //myConn.Open();
            DataSet dtSet = new DataSet();
            myCmd.Fill(dtSet, "Table1");
            DataTable dTable = dtSet.Tables[0];
            dataGridView1.DataSource = dtSet.Tables["Table1"].DefaultView;
            myConn.Close();


        }
     
        private void axMap1ShapeIdentified(object sender, _DMapEvents_ShapeIdentifiedEvent e)
        {
            
            Shapefile sf =  axMap1.get_Shapefile(e.layerHandle);
            if (sf != null)
            {
                string s = "";
                for (int i = 0; i < sf.NumFields; i++)
                {
                    string val = sf.get_CellValue(i, e.shapeIndex).ToString();
                    if (val == "") val = "null";


                    //    s += sf.Table.Field[i].Name + ":" + val + "; ";
                    s += val + ";";
                }
                string[] subs = s.Split(';');

                this.ObjNo.Text = subs[0].ToString();
                this.ObjName.Text = subs[1].ToString();
                this.Elevation.Text = subs[2].ToString();

            }

        }
        private void changeColor()
        {
            Shapefile sf = axMap1.get_Shapefile(0);
            sf.DefaultDrawingOptions.PointSize = 2;

            var names = new HashSet<string>();
            int index = sf.Table.FieldIndexByName["ObstRwyTSApp"];
            for (int i = 0; i < sf.Table.NumRows; i++)
            {
                MessageBox.Show(sf.Table.CellValue[index, i].ToString());

            }



        }

        private void button3_Click(object sender, EventArgs e)
        {
            changeColor();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            gMapControl1.MapProvider = GMap.NET.MapProviders.BingMapProvider.Instance;

            GMap.NET.GMaps.Instance.Mode = GMap.NET.AccessMode.ServerOnly;
            gMapControl1.Position = new GMap.NET.PointLatLng(25.0237358, 73.8938136);
            gMapControl1.Show();
        }
    }
    
}
