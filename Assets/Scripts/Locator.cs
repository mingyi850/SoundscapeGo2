using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mapbox.Unity.Utilities;
using Mapbox.Utils;
using Mapbox.Unity.Map;
using Mapbox.Unity.Location;
using Scripts.BingMapClasses;
using Scripts.BingMapClassesLocator;
using UnityEngine.UI;
using TMPro;
using BingSearch;
using BingMapsEntities;
using TTS;

public class Locator : MonoBehaviour
{
    public TextMeshProUGUI locationTextMeshAddress;
    public TextMeshProUGUI infoTextMesh;
    public LocationProviderFactory locationProviderFactory;

    private AbstractMap abstractMap;
    private Vector2d mapCenter;
    private BingMapsClasses getAddressRequest;
    private List<BingMapsClasses.Result> resultsList;
    private TextToSpeechHandler ttsHandler;
    private BingSearchHandler bsHandler;
    private BingMapsClassesLocator.address streetAddress;
    private ILocationProvider locationProvider;

    // Use this for initialization

    public IEnumerator Start()
    {
        locationProvider = locationProviderFactory.TransformLocationProvider;
        //locationProvider = GameObject.Find("LocationProviderFactoryObject").GetComponent<LocationProviderFactory>().TransformLocationProvider;
        Debug.Log(locationProvider);
        abstractMap = GameObject.Find("Map").GetComponent<AbstractMap>();
        //streetAddress = GameObject.Find("BingMapsClassesLocator").GetComponent<BingMapsClassesLocator.address>();
        //Debug.Log(streetAddress);
        yield return new WaitUntil(() => abstractMap.isReady == true);

        mapCenter = abstractMap.getCenterLongLat();
    }

    void Update() // Update is called once per frame//
    {
        //Debug.Log("CurrentPosition is: " + getLongLat().ToString());
    }

    public Vector2d getLongLat()
    {
        Vector2d newLatLong;
        newLatLong = locationProvider.CurrentLocation.LatitudeLongitude;
        double Lat = newLatLong.x;
        double Long = newLatLong.y;
        newLatLong.x = Long;
        newLatLong.y = Lat;
        Debug.Log(newLatLong);
        return newLatLong;
    }


    //public Vector2d getLongLat()
    //{
    //    Vector2d newLatLong = locationProvider.CurrentLocation.LatitudeLongitude;
    //    double Lat = newLatLong.x;
    //    double Long = newLatLong.y;
    //    newLatLong.x = Long;
    //    newLatLong.y = Lat;
    //    Debug.Log(newLatLong);
    //    return newLatLong;
    //}

    public string getAddress()
    {
        Vector2d currentLocationVector = getLongLat();
        Debug.Log(currentLocationVector);

        string currentLocation = currentLocationVector.ToString();
        Debug.Log(currentLocation);

        //string latLong = BingMapsClassesLocator.requestAddressFromBingMaps();
        //string currentAddress = BingMapsClassesLocator.requestAddressFromBingMaps(string addressLine);
        BingMapsClassesLocator bmcl = new BingMapsClassesLocator();
        string currentAddress = bmcl.requestAddressFromBingMaps(currentLocation);
        return currentAddress;

        //string displayAddress = BingMapsClassesLocator.rootObjectAddress.resourceSets[0].resources[0].address.addressLine;
        //BingMapsClassesLocator.rootObjectAddress.resourceSets[0].resources[0].address.addressLine
        //}
    }

    //public string getStreetAddress(string)
    //StartCoroutine(requestAddressFromBingMaps(latLong);


    void updateInfoPanel(Text textBox, string content)
    {
        textBox.text = content;
    }

}

