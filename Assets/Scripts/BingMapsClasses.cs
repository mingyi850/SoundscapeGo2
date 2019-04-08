using System.Collections;
using System.Collections.Generic;
using UnityEngine;





namespace Scripts.BingMapClasses {

	[System.Serializable]
	public class BingMapsClasses : MonoBehaviour
	{

		public static T[] getJsonArray<T>(string JSONString)
		{
			string newJsonString = "{\"array\": " + JSONString + "}";
			Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(newJsonString);
			return wrapper.array;
		}

		public static RootObject getRootObject(string JSONString)
		{
			return JsonUtility.FromJson<RootObject>(JSONString);
		}

		private class Wrapper<T>
		{
			public T[] array;
		}

		[System.Serializable]
		public class Metadata
		{
			public string uri;
		}

		[System.Serializable]
		public class Result
		{
			public Metadata __metadata;
			public string DisplayName;
			public string Name;
			public string AddressLine;
			public string EntityTypeID;
			public double Latitude;
			public double Longitude;
			public string websiteUrl;
		}

		[System.Serializable]
		public class D
		{
			public string __copyright;
			public List<Result> results;
		}

		[System.Serializable]
		public class RootObject
		{
			public D d;
		}

		public static List<Result> requestPoiFromBingMaps(double latitude, double longitude, double distance, int poiCount)
		{
			string bingMapsApiKey = "Aul2Lj8luxSAtsuBPTb0qlqhXc6kwdTZvQGvGkOc_h_Jg3HI_2F-V6BeeHwHZZ4E";
			string dataAccessId = "c2ae584bbccc4916a0acf75d1e6947b4";
			string dataSourceName = "NavteqEU";
			string entityTypeName = "NavteqPOIs";
			string[] returnParams = { "DisplayName", "Name", "AddressLine", "EntityTypeID", "Latitude", "Longitude" };
			string dataFormat = "json";


			string queryURL = generateQueryUrlNearby(dataAccessId, dataSourceName, entityTypeName, latitude, longitude, distance, returnParams, poiCount, dataFormat, bingMapsApiKey);
			WWW request = new WWW(queryURL);
			float startTime = Time.time;
			while (request.isDone == false)
			{
				if (Time.time - startTime > 10)
				{
					Debug.Log("API TIMEOUT");
					break;
				}

			}
			if (request.error != null) {
				Debug.Log(request.error);
				return new List<Result>();
			}
			else if (request.isDone)
			{
				string jsonData = System.Text.Encoding.UTF8.GetString(request.bytes, 0, request.bytes.Length);

				BingMapsClasses.RootObject rootObject = BingMapsClasses.getRootObject(jsonData);

				return rootObject.d.results;

			}
			else
			{
				return new List<Result>();
			}

		}

		private static string generateQueryUrlNearby(string dataAccessId, string dataSourceName, string entityTypeName, double latitude, double longitude, double distance, string[] returnParams, int poiCount, string format, string apiKey)
		{
			string staticEndpoint = "http://spatial.virtualearth.net/REST/v1/data";
			string returnParamsString = "";
			foreach (string param in returnParams)
			{
				returnParamsString = returnParamsString + param + ",";
			}
			Debug.Log(returnParamsString);
			returnParamsString = returnParamsString.Remove(returnParamsString.Length - 1);
			Debug.Log(returnParamsString);
			string entityFiltersString = getEntityFiltersString();
			string queryURL = (string.Format("{0}/{1}/{2}/{3}?spatialFilter=nearby({4},{5},{6})&$filter=EntityTypeID in {10}&$select={7}&$top={8}&$format={9}&key={11}", staticEndpoint, dataAccessId, dataSourceName, entityTypeName, latitude, longitude, distance, returnParamsString, poiCount, format, entityFiltersString, apiKey));
			Debug.Log(queryURL);
			return queryURL;
		}

		private static string getEntityFiltersString()
		{
			int[] entityFilters = PlayerPrefsX.GetIntArray("EntitiesCodes");
			string filteredFiltersString = "(";
			int totalFilters = entityFilters.Length;
			if (totalFilters > 50)
			{
				totalFilters = 50; // maxiumum number for Query API
			}
			for (int x = 0; x < totalFilters; x++)
			{
				filteredFiltersString += "'" + entityFilters[x].ToString() + "'";
				if (x < (totalFilters - 1))
				{
					filteredFiltersString += ", ";
				}
				else
				{
					filteredFiltersString += ")";
				}
			}

			Debug.Log(filteredFiltersString);
			return filteredFiltersString;

		}
	}
}
