using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mapbox.Unity.Utilities;
using Mapbox.Utils;
using Mapbox.Unity.Map;
using Mapbox.Unity.Location;

public class PlayerLocation : MonoBehaviour {
	
	private AbstractMap abstractMap; 
	private Vector2d mapCenterLongLat; 
	// Use this for initialization
	IEnumerator Start () {
		
		abstractMap = GameObject.Find("Map").GetComponent<AbstractMap>();

		yield return new WaitUntil(() => abstractMap.isReady == true);

		Vector2d mapCenter = abstractMap.getCenterLongLat ();
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log("CurrentPosition is: " + getLongLat().ToString());
	}

	public Vector2d getLongLat() {
		double newX, newY;
		Vector2d mapCenter = abstractMap.getCenterLongLat ();

		Vector2d longLatChange = VectorExtensions.GetGeoPosition(transform, mapCenter,  (abstractMap.WorldRelativeScale * 1.5f));
		newX = mapCenter.x + longLatChange.x;
		newY = mapCenter.y + longLatChange.y;

		return new Vector2d (newX, newY);

	}

	public void getNearbyFeatures() {

		Vector2d currentLocation = getLongLat ();
		double latitude = currentLocation.x;
		double longitude = currentLocation.y;
		requestPoiFromBingMaps (latitude, longitude, 5.0);





		Debug.Log ("Current Location: " + getLongLat ().ToString ());


	}

	private void requestPoiFromBingMaps(double latitude, double longitude, double distance) {
		string bingMapsApiKey = "Aul2Lj8luxSAtsuBPTb0qlqhXc6kwdTZvQGvGkOc_h_Jg3HI_2F-V6BeeHwHZZ4E";
		string staticEndpoint = "http://spatial.virtualearth.net/REST/v1/data";
		string dataAccessId = "c2ae584bbccc4916a0acf75d1e6947b4";
		string dataSourceName = "NavteqEU";
		string entityTypeName = "NavteqPOIs";

		WWWForm form = new WWWForm ();
		Dictionary<string,string> headers = form.headers;
		string queryURL = (string.Format("http://spatial.virtualearth.net/REST/v1/data/{0}/{1}/{2}?spatialFilter=nearby({3},{4},{5})&$select=DisplayName,Name,AddressLine,EntityTypeID,Latitude,Longitude&$top=10&$format=json&key={6}",dataAccessId, dataSourceName, entityTypeName, latitude, longitude, distance, bingMapsApiKey));

		/*
		 * headers ["queryKey"] = bingMapsApiKey;
		headers ["accessID"] = accessId;
		headers ["dataSourceName"] = dataSourceName;
		headers ["entityTypeName"] = entityTypeName;
		headers["spatialFilter"] = ($"nearby({latitude},{longitude},{distance})");
		*/
		WWW request = new WWW (queryURL);
		StartCoroutine (checkResponse(request));


	}







	private IEnumerator checkResponse(WWW request) {
		yield return request;
		Debug.Log (request.text);

	}

}
