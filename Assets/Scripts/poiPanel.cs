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
	
    

}
