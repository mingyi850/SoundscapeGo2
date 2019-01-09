using System;
using UnityEngine;
using UnityEngine.UI;

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

    private double toRadian(double val)
    {
        return (Math.PI / 180) * val;
    }

    double RoundMeters(double num)
    {
        double intPart = Math.Truncate(num);
        double rem = intPart % 25;
        return rem >= 5 ? (intPart - rem + 10) : (intPart - rem);
    }   

    public double Distance (Destination pos1)
    {
        Destination pos2 = deviceLocation;
        double R = 6371;
        double dLat = this.toRadian(pos2.latitude - pos1.latitude);
        double dLon = this.toRadian(pos2.longitude - pos1.longitude);
        double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) + Math.Cos(this.toRadian(pos1.latitude)) * Math.Cos(this.toRadian(pos2.latitude)) * Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
        double c = 2 * Math.Asin(Math.Min(1, Math.Sqrt(a)));
        double d = R * c;
        return d;
    }

    public void Setup(Destination currentDestination, ScrollList currentScrollList)
    {
        destination = currentDestination;
        nameLabel.text = destination.destinationName;
        destination.longitude = currentDestination.longitude;
        destination.latitude = currentDestination.latitude;
        var distance = Distance(destination);
        if (distance < 1.0)
        {
            distance = RoundMeters(distance * 100);
            distanceLabel.text = distance.ToString() + "m";
        } else {
            distanceLabel.text = distance.ToString("0.00") + "km";
        }
        scrollList = currentScrollList;
    }
}
