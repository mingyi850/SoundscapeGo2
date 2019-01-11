using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Destination
{
    public string destinationName;
    public float longitude;
    public float latitude;

    public Destination(string nameLabel, float lat, float longit)
    {
        destinationName = nameLabel;
        latitude = lat;
        longitude = longit;
    }
}

public class ScrollList : MonoBehaviour
{
    public List<Destination> destinationList;
    private List<Destination> filteredDestinationList = new List<Destination>();
    public Transform contentPanel;
    public SimpleObjectPool buttonObjectPool;
    public InputField searchBar;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < destinationList.Count; i++)
        {
            filteredDestinationList.Add(destinationList[i]);
        }
        UpdateDestinationsList(searchBar);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateDestinationsList(InputField searchField)
    {
        filteredDestinationList.Clear();
        for (int i = 0; i < destinationList.Count; i++)
        {
            if (destinationList[i].destinationName.IndexOf(searchField.text, StringComparison.OrdinalIgnoreCase) >= 0)
            {
                filteredDestinationList.Add(destinationList[i]);
            }
        }
        RefreshDisplay();
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
        for (int i = 0; i < filteredDestinationList.Count; i++)
        {
            Destination destination = filteredDestinationList[i];
            GameObject newButton = buttonObjectPool.GetObject();
            newButton.transform.SetParent(contentPanel);
            SampleButton sampleButton = newButton.GetComponent<SampleButton>();
            sampleButton.Setup(destination, this);
        }
    }
}
