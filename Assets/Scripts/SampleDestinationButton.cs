using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Mapbox.Utils;
using Scripts.DistanceCalc;

public class SampleDestinationButton : MonoBehaviour
{
    public Button button;
    public Text nameLabel;
    public Text distanceLabel;


    private Destination destination;
    private ScrollList scrollList;

    double RoundMeters(double num)
    {
        double intPart = Math.Truncate(num);
        double rem = intPart % 25;
        return rem >= 5 ? (intPart - rem + 10) : (intPart - rem);
    }   

    public void Setup(Destination currentDestination, ScrollList currentScrollList)
    {
        destination = currentDestination;
        nameLabel.text = destination.destinationName;
        destination.coordinates.x = currentDestination.coordinates.x;
        destination.coordinates.y = currentDestination.coordinates.y;
        scrollList = currentScrollList;
    }

    public void passCoordinatesToNextScene()
    {
        PlayerPrefs.SetFloat("longitude", (float)this.destination.coordinates.y);
        PlayerPrefs.SetFloat("latitude", (float)this.destination.coordinates.x);
    }

    public void LoadNextScene()
    {
        passCoordinatesToNextScene();
        SceneManager.LoadScene("Navigation Scene");
    }
}
