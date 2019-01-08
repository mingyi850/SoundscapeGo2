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
}

public class ScrollList : MonoBehaviour
{
    public List<Destination> destinationList;
    public Transform contentPanel;
    public SimpleObjectPool buttonObjectPool;

    // Start is called before the first frame update
    void Start()
    {
        RefreshDisplay();
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    public void RefreshDisplay()
    {
    //    updateDestinationsList("peepee");
        AddButtons();
    }

    /* private void updateDestinationsList(string currentString)
    {

    } */

    private void AddButtons()
    {
        for (int i = 0; i < destinationList.Count; i++)
        {
            Destination destination = destinationList[i];
            GameObject newButton = buttonObjectPool.GetObject();
            newButton.transform.SetParent(contentPanel);

            SampleButton sampleButton = newButton.GetComponent<SampleButton>();
            sampleButton.Setup(destination, this);
        }
    }
}
