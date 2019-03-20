using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mapbox.Utils;


[System.Serializable]

public class MarkerScrollList : MonoBehaviour
{
    public List<Destination> markerLists;
    public Transform contentPanel;
    public SimpleObjectPool buttonObjectPool;
    public GameObject UIManager;
    public MarkersManager markersManager;

    // Start is called before the first frame update
    void Start()
    {
        UpdateMarkersList();
        RefreshDisplay();
    }

    private void UpdateMarkersList()
    {
        markerLists.Clear();
        foreach(KeyValuePair<string, Vector2d> entry in markersManager.savedMarkers)
        {
            Destination destination = new Destination(entry.Key, entry.Value.x, entry.Value.y); //HERE too
            markerLists.Add(destination);
        }
    }

    public void RefreshDisplay() 
    {
        UpdateMarkersList();
        RemoveButtons();
        AddButtons();
    }

    private void RemoveButtons()
    {
        while (contentPanel.childCount > 0) 
        {
            GameObject toRemove = transform.GetChild(0).gameObject;
            buttonObjectPool.ReturnObject(toRemove);
        }
    }

    private void AddButtons()
    {
        for (int i = 0; i < markerLists.Count; i++) 
        {
            Destination newDest = markerLists[i];
            GameObject newButton = buttonObjectPool.GetObject();
            newButton.transform.SetParent(contentPanel);

            SampleMarkerButton sampleButton = newButton.GetComponent<SampleMarkerButton>();
            sampleButton.Setup(newDest, this, UIManager, markersManager);
        }
    }

}
