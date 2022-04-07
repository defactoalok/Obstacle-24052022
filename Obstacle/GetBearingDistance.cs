using System;
using System.Linq;

namespace Obstacle
{
    class GetBearingDistance
    {
        public string HN;
        public string HE;
        public string AppN;
        public string AppE;

        public double App1N;
        public double App1E;
        public double App2N;
        public double App2E;
        public double PointE;
        public double PointN;
        public double X = 0;
        public double Y = 0;
        public double BearingSide1 = 0;
        public double BearingReverse = 0;
        public double latitude = 0;
        public double longitude = 0;
        public double ToLatLon(double utmX, double utmY, string utmZone, out double latitude, out double longitude)
        {
            bool isNorthHemisphere = utmZone.Last() >= 'N';

            var diflat = -0.00066286966871111111111111111111111111;
            var diflon = -0.0003868060578;

            var zone = int.Parse(utmZone.Remove(utmZone.Length - 1));
            var c_sa = 6378137.000000;
            var c_sb = 6356752.314245;
            var e2 = Math.Pow((Math.Pow(c_sa, 2) - Math.Pow(c_sb, 2)), 0.5) / c_sb;
            var e2cuadrada = Math.Pow(e2, 2);
            var c = Math.Pow(c_sa, 2) / c_sb;
            var x = utmX - 500000;
            var y = isNorthHemisphere ? utmY : utmY - 10000000;

            var s = ((zone * 6.0) - 183.0);
            var lat = y / (c_sa * 0.9996);
            var v = (c / Math.Pow(1 + (e2cuadrada * Math.Pow(Math.Cos(lat), 2)), 0.5)) * 0.9996;
            var a = x / v;
            var a1 = Math.Sin(2 * lat);
            var a2 = a1 * Math.Pow((Math.Cos(lat)), 2);
            var j2 = lat + (a1 / 2.0);
            var j4 = ((3 * j2) + a2) / 4.0;
            var j6 = ((5 * j4) + Math.Pow(a2 * (Math.Cos(lat)), 2)) / 3.0;
            var alfa = (3.0 / 4.0) * e2cuadrada;
            var beta = (5.0 / 3.0) * Math.Pow(alfa, 2);
            var gama = (35.0 / 27.0) * Math.Pow(alfa, 3);
            var bm = 0.9996 * c * (lat - alfa * j2 + beta * j4 - gama * j6);
            var b = (y - bm) / v;
            var epsi = ((e2cuadrada * Math.Pow(a, 2)) / 2.0) * Math.Pow((Math.Cos(lat)), 2);
            var eps = a * (1 - (epsi / 3.0));
            var nab = (b * (1 - epsi)) + lat;
            var senoheps = (Math.Exp(eps) - Math.Exp(-eps)) / 2.0;
            var delt = Math.Atan(senoheps / (Math.Cos(nab)));
            var tao = Math.Atan(Math.Cos(delt) * Math.Tan(nab));

            longitude = ((delt * (180.0 / Math.PI)) + s) + diflon;
            latitude = ((lat + (1 + e2cuadrada * Math.Pow(Math.Cos(lat), 2) - (3.0 / 2.0) * e2cuadrada * Math.Sin(lat) * Math.Cos(lat) * (tao - lat)) * (tao - lat)) * (180.0 / Math.PI)) + diflat;
            return longitude;
        }
        public double GetBearing()
        {
            double Bearing = 0;
            double Azimuth = 0;

            if (!string.IsNullOrEmpty(HN) && !string.IsNullOrEmpty(HE)
               && !string.IsNullOrEmpty(AppN) && !string.IsNullOrEmpty(AppE))
            {
                {
                    double EMinusE = double.Parse(AppE) - double.Parse(HE);
                    double NMinusN = double.Parse(AppN) - double.Parse(HN);
                    if (Math.Abs(EMinusE) > 0 && Math.Abs(NMinusN) > 0)
                    {
                        Bearing = Math.Atan(((EMinusE) / (NMinusN))) * (180 / Math.PI);
                        Azimuth = GetAzimuth(Bearing, EMinusE, NMinusN);
                    }

                }


            }
            return Azimuth;
        }
        public double GetAzimuth(double Bearing, double EMinusE, double NMinusN)
        {
            double Azimuth = 0;
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

        public double GetDistance()
        {

            double Distance = 0;


            if (!string.IsNullOrEmpty(HN) && !string.IsNullOrEmpty(HE)
               && !string.IsNullOrEmpty(AppN) && !string.IsNullOrEmpty(AppE))
            {
                {

                    double EMinusE = double.Parse(AppE) - double.Parse(HE);
                    double NMinusN = double.Parse(AppN) - double.Parse(HN);
                    Distance = Math.Sqrt((Math.Pow((EMinusE), 2) + Math.Pow((NMinusN), 2)));

                }


            }
            return Distance;
        }
        public class ReturnXY
        {
            public double returnX;
            public double returnY;
            public double RDistApp1;
            public double RDistApp2;
            public double RBearingApp1;
            public double RBearingApp2;


        }
        public ReturnXY GetXY()
        {
            var Return = new ReturnXY();
            double DistAPP1, BearingApp1, DistApp2, BearingApp2, Azimuth1, Azimuth2, PointAzimuth1, PointAzimuth2;

            if (!string.IsNullOrEmpty(App1E.ToString()) && !string.IsNullOrEmpty(App1N.ToString())
               && !string.IsNullOrEmpty(PointN.ToString()) && !string.IsNullOrEmpty(PointE.ToString()))
            {
                {
                    double EMinusE, NMinusN = 0;
                    /*   
                      EMinusE = double.Parse(HE) - App1E;
                      NMinusN = double.Parse(HN)- App1N;
                      double Bearing = Math.Atan((Math.Abs(EMinusE) / Math.Abs(NMinusN))) * (180 / Math.PI);
                      Azimuth1 = GetAzimuth(Bearing,EMinusE,NMinusN );

                      EMinusE = double.Parse(HE) - App2E;
                      NMinusN = double.Parse(HN) - App2N;

                      Bearing = Math.Atan((Math.Abs(EMinusE) / Math.Abs(NMinusN))) * (180 / Math.PI);
                      Azimuth2 = GetAzimuth(Bearing, EMinusE, NMinusN);
                     */
                    EMinusE = App1E - PointE;
                    NMinusN = App1N - PointN;
                    DistAPP1 = Math.Sqrt((Math.Pow(Math.Abs(EMinusE), 2) + Math.Pow(Math.Abs(NMinusN), 2)));
                    BearingApp1 = Math.Atan((Math.Abs(EMinusE) / Math.Abs(NMinusN))) * (180 / Math.PI);
                    PointAzimuth1 = GetAzimuth(BearingApp1, EMinusE, NMinusN);

                    EMinusE = App2E - PointE;
                    NMinusN = App2N - PointN;
                    DistApp2 = Math.Sqrt((Math.Pow(Math.Abs(EMinusE), 2) + Math.Pow(Math.Abs(NMinusN), 2)));
                    BearingApp2 = Math.Atan((Math.Abs(EMinusE) / Math.Abs(NMinusN))) * (180 / Math.PI);
                    PointAzimuth2 = GetAzimuth(BearingApp2, EMinusE, NMinusN);

                    if (DistAPP1 > DistApp2)
                    {
                        X = (DistApp2 * Math.Sin(BearingReverse - PointAzimuth2));
                        Y = (DistApp2 * Math.Cos(BearingReverse - PointAzimuth2));

                    }
                    else
                    {
                        X = (DistAPP1 * Math.Sin(BearingSide1 - PointAzimuth1));
                        Y = (DistAPP1 * Math.Cos(BearingSide1 - PointAzimuth1));

                    }

                    Return.returnX = X;
                    Return.returnY = Y;
                    Return.RDistApp1 = DistAPP1;
                    Return.RDistApp2 = DistApp2;
                    Return.RBearingApp1 = BearingApp1;
                    Return.RBearingApp2 = BearingApp2;
                }



            }

            return Return;
        }
    }
}
