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
  public MarkersManager markersManager;
  public TextMeshProUGUI textField;
  public double coordinateX;
  public double coordinateY;

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

	public void SaveButtonClicked() 
  {
        if (markersManager.isASavedMarker(textField.text) == 0)
            SaveMarker();
        else if (markersManager.isASavedMarker(textField.text) == 1)
            DeleteMarker();
  }

	private void SaveMarker()
  {
      Debug.Log("Marker saved");
      markersManager.SaveMarker(textField.text, new Vector2d(coordinateX, coordinateY));
  }

	private void DeleteMarker()
  {
      markersManager.DeleteMarker(textField.text);    
      Debug.Log("Marker deleted");
        
	}
	
    

}
