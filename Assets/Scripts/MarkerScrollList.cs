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

    public void RefreshDisplay() 
    {
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
            ListOfMarkers newList = markerLists[i];
            GameObject newButton = buttonObjectPool.GetObject();
            newButton.transform.SetParent(contentPanel);

            SampleListButton sampleButton = newButton.GetComponent<SampleListButton>();
            sampleButton.Setup(newList, this);
        }
    }

}
