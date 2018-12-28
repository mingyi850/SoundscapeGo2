using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Scripts.BingMapClasses {
	
	[System.Serializable]
	public class BingMapsClasses : MonoBehaviour {

		public static T[] getJsonArray<T>(string JSONString) {
			string newJsonString = "{\"array\": " + JSONString + "}";
			Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>> (newJsonString);
			return wrapper.array;
		}

		public static RootObject getRootObject(string JSONString) {
			return JsonUtility.FromJson<RootObject> (JSONString);
		}

		private class Wrapper<T> {
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
	}
}