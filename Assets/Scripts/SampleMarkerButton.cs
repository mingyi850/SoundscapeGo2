using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;
using Mapbox.Utils;
using Mapbox.Unity.Map;


public class SampleMarkerButton : MonoBehaviour {
    
    //public Button gotoButtonComponent;
    private Image saveButtonComponent;
    public TMP_Text nameLabel;
    public MarkersManager markersManager;
    

    private Vector2d coordinates;
    private MarkerScrollList scrollList;
    private GameObject UIManagerObject;
    private int markerState = 1;
	private AbstractMap map;

	private void Start()
	{
		map = GameObject.Find("Map").GetComponent<AbstractMap>();
	}
	public void Setup(Destination currentDestination, MarkerScrollList currentScrollList, GameObject UIManager, MarkersManager currentMarkersManager) 
    {
        coordinates = currentDestination.coordinates; //HERE
        nameLabel.text = currentDestination.destinationName;
        scrollList = currentScrollList;
        UIManagerObject = UIManager;
        markersManager = currentMarkersManager;
		saveButtonComponent = transform.Find("Save Button").GetComponent<Image>();
		saveButtonComponent.color = Color.red;
	}

    public void SaveButtonClicked() 
    {
		if (markersManager.isASavedMarker(nameLabel.text) == 0)
		{
			SaveMarker();
			saveButtonComponent.color = Color.red;
		}
		else if (markersManager.isASavedMarker(nameLabel.text) == 1)
		{
			DeleteMarker();
			saveButtonComponent.color = Color.white;
		}
	}

    private void SaveMarker()
    {
        markersManager.SaveMarker(nameLabel.text, coordinates);
        Debug.Log("Marker saved");
        markerState = 1;
        //saveButtonComponent.GetComponent<Image>().color = Color.red;
    }

    private void DeleteMarker()
    {
        markersManager.DeleteMarker(nameLabel.text);
        markerState = 0;
        //saveButtonComponent.GetComponent<Image>().color = Color.white;
        Debug.Log("Marker deleted");

    }

	public void gotoButtonClicked()
	{
		if (coordinates != null)
		{
			map.UpdateMap(coordinates);
			Debug.Log("Map Updated");
		}
	}

	
    
	

}