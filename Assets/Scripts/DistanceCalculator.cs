using System;

namespace Scripts.DistanceCalc
{
	static class DistanceCalculator
	{
		const double PI = 3.141592653589793;
		const double RADIUS = 6378.16;

		public static double toRadian(double x) {
			return x * PI / 180;
		}

		public static double getDistanceBetweenPlaces(double lon1, double lat1, double lon2, double lat2) {
			double dlon = toRadian(lon2 - lon1);
			double dlat = toRadian(lat2 - lat1);

			double a = (Math.Sin(dlat / 2) * Math.Sin(dlat / 2)) + Math.Cos(toRadian(lat1)) * Math.Cos(toRadian(lat2)) * (Math.Sin(dlon / 2) * Math.Sin(dlon / 2));
			double angle = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
			return angle * RADIUS;
		}

		public static string getRelativeDirectionString(float relativeAngle) {
			string returnString = "";
			if (relativeAngle > -60.0f && relativeAngle < 60.0f) {
				returnString += "Ahead of you ";
			}
			else if (relativeAngle < -120.0f || relativeAngle > 120.0f) {
				returnString += "Behind you ";
			}
			if (relativeAngle > 20.0f && relativeAngle < 160.0f) {
				returnString += "To your right";
			}
			else if (relativeAngle < -20.0f && relativeAngle > -160.0f) {
				returnString += "To your left";
			}
			return returnString;
		}
	}


}

