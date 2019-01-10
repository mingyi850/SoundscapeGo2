using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Scripts.DistanceCalc;

public class SampleButton : MonoBehaviour
{
    public Button button;
    public Text nameLabel;
    public Text distanceLabel;
    Destination deviceLocation = new Destination("Device", 51.523180F, -0.132522F);  


    private Destination destination;
    private ScrollList scrollList;

    // Start is called before the first frame update
    void Start()
    {
        
    }

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
        var distance = DistanceCalculator.getDistanceBetweenPlaces(destination.longitude, destination.latitude, deviceLocation.longitude, deviceLocation.latitude);
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
