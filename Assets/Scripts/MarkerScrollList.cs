using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class ListOfMarkers
{
    public string listName;
    public List<Destination> markersList = new List<Destination>();

    public ListOfMarkers(string nameLabel)
    {
        listName = nameLabel;
        markersList.Clear();
    }
}

public class MarkerScrollList : MonoBehaviour
{
    public List<ListOfMarkers> markerLists;
    public Transform contentPanel;
    public SimpleObjectPool buttonObjectPool;

    // Start is called before the first frame update
    void Start()
    {
        
    }
}
