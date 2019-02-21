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
    public Vector2d deviceLocation = new Vector2d( 51.523180F, -0.132522F);  


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
        destination.longitude = currentDestination.longitude;
        destination.latitude = currentDestination.latitude;
        var distance = DistanceCalculator.getDistanceBetweenPlaces(destination.longitude, destination.latitude, deviceLocation.y, deviceLocation.x);
        if (distance < 1.0)
        {
            distance = RoundMeters(distance * 100);
            distanceLabel.text = distance.ToString() + "m";
        } else {
            distanceLabel.text = distance.ToString("0.00") + "km";
        }
        scrollList = currentScrollList;
    }

    public void passCoordinatesToNextScene()
    {
        PlayerPrefs.SetFloat("longitude", this.destination.longitude);
        PlayerPrefs.SetFloat("latitude", this.destination.latitude);
    }

    public void LoadNextScene()
    {
        passCoordinatesToNextScene();
        SceneManager.LoadScene("Navigation Scene");
    }
}
