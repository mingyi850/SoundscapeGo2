using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mapbox.Unity.Utilities;
using Mapbox.Utils;
using Mapbox.Unity.Map;
using Mapbox.Unity.Location;
using Scripts.BingMapClasses;
using Scripts.DistanceCalc;
using UnityEngine.UI;
using TMPro;
using BingSearch;
using BingMapsEntities;
using TTS;

public class poiPanel : MonoBehaviour
{
	private Vector3 sendLocation;
	public AudioBeacon beacon;

    // Start is called before the first frame update
    void Start()
    {
		sendLocation = Vector3.zero;
    }

	public void sendBeacon()
	{
		beacon.setActiveBeacon(sendLocation);
	}

	public void setSendLocation(Vector3 location)
	{
		sendLocation = location;
	}

	public int isASavedMarker(string name)
	{
		return 0;
	}

	public void SaveButtonClicked() 
  {
        if (isASavedMarker("name") == 0)
            SaveMarker();
        else if (isASavedMarker("name") == 1)
            DeleteMarker();
  }

	private void SaveMarker()
  {
        Debug.Log("Marker saved");
        //markerState = 1;
        //saveButtonComponent.GetComponent<Image>().color = Color.red;
  }

	private void DeleteMarker()
  {
        //markerState = 0;
        //saveButtonComponent.GetComponent<Image>().color = Color.white;
        Debug.Log("Marker deleted");

	}
	
    

}
