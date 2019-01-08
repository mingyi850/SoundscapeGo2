using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SampleButton : MonoBehaviour
{
    public Button button;
    public Text nameLabel;
    public Text distanceLabel;

    private Destination destination;
    private ScrollList scrollList;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Setup(Destination currentDestination, ScrollList currentScrollList)
    {
        destination = currentDestination;
        nameLabel.text = destination.destinationName;
        destination.longitude = currentDestination.longitude;
        destination.latitude = currentDestination.latitude;

        scrollList = currentScrollList;
    }
}
