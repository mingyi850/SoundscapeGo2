using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Mapbox.Unity.Map;
using Mapbox.Unity.Location;
using UnityEngine.UI;
using Mapbox.Utils;
using Scripts.BingMapClassesLocator;


public class LocationStreetPanelScript : MonoBehaviour
{
    private TextMeshProUGUI locationTextMeshAddress;
    private GameObject address1;
    private AbstractMap map;
    public BingMapsClassesLocator BingMapsClassesLocatorScript;


    // Start is called before the first frame update
    void Start()
    {
        locationTextMeshAddress = gameObject.GetComponent<TextMeshProUGUI>();
        address1 = GameObject.Find("Player");
        map = GameObject.Find("Map").GetComponent<AbstractMap>();
        BingMapsClassesLocatorScript = address1.GetComponent<BingMapsClassesLocator>();
        StartCoroutine(CoroutineAddress());
    }

    public IEnumerator CoroutineAddress()
    {
		Debug.Log("Coroutine Address Started");
        yield return new WaitUntil(() => map.isReady == true);
        while (true)
        {
            yield return new WaitForSeconds(5f);
            locationTextMeshAddress.text = BingMapsClassesLocatorScript.getAddress();
            Debug.Log("CoroutineAddress: " + Time.time);
        }
    }
}

//string address = LocatorScript.getAddress();
//Debug.Log(address);
//locationTextMeshAddress.text = LocatorScript.getAddress();


//public IEnumerator CoroutineAddress()
//{
//    while (true)
//    {
//        locationTextMesh.text = playerLocationScript.getLongLat().ToString();
//        Debug.Log("CoroutineAddress: " + Time.time);
//        locationTextMeshAddress.text = LocatorScript.getAddress();
//        Debug.Log(locationTextMeshAddress);
//        string address = LocatorScript.getAddress();
//        Debug.Log(address);
//        yield return new WaitForSeconds(5f);
//    }
//}
