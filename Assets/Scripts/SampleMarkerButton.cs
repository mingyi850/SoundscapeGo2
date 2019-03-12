using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;


public class SampleMarkerButton : MonoBehaviour {
    
    public Button gotoButtonComponent;
    public Button saveButtonComponent;
    public TMP_Text nameLabel;
    
    private Destination destination;
    private MarkerScrollList scrollList;
    private GameObject UIManagerObject;
    private int markerState = 0;

    
    public void Setup(Destination currentDestination, MarkerScrollList currentScrollList, GameObject UIManager) 
    {
        destination = currentDestination;
        nameLabel.text = destination.destinationName;
        scrollList = currentScrollList;
        UIManagerObject = UIManager;
    }

    public void SaveButtonClicked() 
    {
        if (markerState == 0)
            SaveMarker();
        else if (markerState == 1)
            DeleteMarker();
    }

    private void SaveMarker()
    {
        Debug.Log("Marker saved");
        markerState = 1;
        //saveButtonComponent.GetComponent<Image>().color = Color.red;
    }

    private void DeleteMarker()
    {
        markerState = 0;
        //saveButtonComponent.GetComponent<Image>().color = Color.white;
        Debug.Log("Marker deleted");

    }
    

}