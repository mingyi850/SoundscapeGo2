using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mapbox.Unity.Utilities;
using Mapbox.Utils;
using Mapbox.Unity.Map;
using Mapbox.Unity.Location;
using Scripts.BingMapClasses;

public class PlayerLocation : MonoBehaviour {
	
	private AbstractMap abstractMap; 
	private Vector2d mapCenter;

	// Use this for initialization
	IEnumerator Start () {
		
		abstractMap = GameObject.Find("Map").GetComponent<AbstractMap>();

		yield return new WaitUntil(() => abstractMap.isReady == true);

		mapCenter = abstractMap.getCenterLongLat ();
		Debug.Log (mapCenter.ToString ());
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log("CurrentPosition is: " + getLongLat().ToString());
	}

	public Vector2d getLongLat() {
		double newX, newY;

		Vector2d longLatChange = VectorExtensions.GetGeoPosition(transform, mapCenter,  (abstractMap.WorldRelativeScale * 1.5f));
		Debug.Log(mapCenter.ToString() + " from getLongLat");
		newX = mapCenter.x + longLatChange.x;
		newY = mapCenter.y + longLatChange.y;

		return new Vector2d (newX, newY);

	}

	public void getNearbyFeatures() {

		Vector2d currentLocation = getLongLat ();
		double latitude = currentLocation.x;
		double longitude = currentLocation.y;
		StartCoroutine(requestPoiFromBingMaps (latitude, longitude, 5.0));

		Debug.Log ("Current Location: " + getLongLat ().ToString ());


	}

	IEnumerator requestPoiFromBingMaps(double latitude, double longitude, double distance) {
		string bingMapsApiKey = "Aul2Lj8luxSAtsuBPTb0qlqhXc6kwdTZvQGvGkOc_h_Jg3HI_2F-V6BeeHwHZZ4E";
		string dataAccessId = "c2ae584bbccc4916a0acf75d1e6947b4";
		string dataSourceName = "NavteqEU";
		string entityTypeName = "NavteqPOIs";
		string[] returnParams = { "DisplayName", "Name", "AddressLine", "EntityTypeID", "Latitude", "Longitude" };
		int poiCount = 10;
		string dataFormat = "json";

		
		string queryURL = generateQueryUrlNearby(dataAccessId, dataSourceName, entityTypeName, latitude, longitude, distance, returnParams, poiCount, dataFormat, bingMapsApiKey);
		WWW request = new WWW (queryURL);
		StartCoroutine (checkResponse(request));
		yield return new WaitUntil (() => request.isDone);

		string jsonData = System.Text.Encoding.UTF8.GetString (request.bytes, 0, request.bytes.Length);

		BingMapsClasses.RootObject rootObject = BingMapsClasses.getRootObject(jsonData);

		for (int x = 0; x < rootObject.d.results.Count; x++) {
			Debug.Log ("Location " + x + ":: " + rootObject.d.results [x].DisplayName);
		}





	}

	private string generateQueryUrlNearby(string dataAccessId, string dataSourceName, string entityTypeName, double latitude, double longitude, double distance, string[] returnParams, int poiCount, string format, string apiKey) {
		string staticEndpoint = "http://spatial.virtualearth.net/REST/v1/data";
		string returnParamsString = "";
		foreach (string param in returnParams) {
			returnParamsString = returnParamsString + param + ",";
		}
		Debug.Log (returnParamsString);
		returnParamsString = returnParamsString.Remove(returnParamsString.Length - 1);
		Debug.Log (returnParamsString);
		string queryURL = (string.Format("{0}/{1}/{2}/{3}?spatialFilter=nearby({4},{5},{6})&$select={7}&$top={8}&$format={9}&key={10}",staticEndpoint, dataAccessId, dataSourceName, entityTypeName, latitude, longitude, distance, returnParamsString, poiCount, format, apiKey));
		Debug.Log (queryURL);
		return queryURL;
	}







	private IEnumerator checkResponse(WWW request) {
		
		yield return request;

		Debug.Log (request.text);

	}

}
