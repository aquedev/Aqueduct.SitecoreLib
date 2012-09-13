using System;

namespace Aqueduct.Utils
{
    public class GeoSearchService
    {
        public static bool IsInRange(double latFrom, double lonFrom, double latTo, double lonTo, double range)
        {
            if (DistanceInMilesBetween(latFrom, lonFrom, latTo, lonTo) <= range) return true;

            return false;
        }

        public static double DistanceInMilesBetween(double lat1, double lon1, double lat2, double lon2)
        {
            double theta = lon1 - lon2;
            double dist = Math.Sin(deg2rad(lat1)) * Math.Sin(deg2rad(lat2)) + Math.Cos(deg2rad(lat1)) * Math.Cos(deg2rad(lat2)) * Math.Cos(deg2rad(theta));
            dist = Math.Acos(dist);
            dist = rad2deg(dist);
            dist = dist * 60 * 1.1515;
            return (dist);
        }

        private static double deg2rad(double deg)
        {
            return (deg * Math.PI / 180.0);
        }

        private static double rad2deg(double rad)
        {
            return (rad / Math.PI * 180.0);
        }

    }
}
