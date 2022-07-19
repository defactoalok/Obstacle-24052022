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
       Shapefile PolygonShape = new Shapefile();
       PolygonShape.CreateNew(@CDir + "\\TstPolygon.shp", ShpfileType.SHP_POLYGON);
       
                       MapWinGIS.Shape PlgLeftApproach = new MapWinGIS.Shape();
                        PlgLeftApproach.Create(PolygonShape.ShapefileType);
                        MakePolygon(leftApporach, myPointIndex, myShapeIndex, PolygonShape, PlgLeftApproach);
                          string App1BStripE = App1E.ToString(); string App1BStripN = App1N.ToString();
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
