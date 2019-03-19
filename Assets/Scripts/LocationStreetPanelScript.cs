using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Mapbox.Unity.Map;
using Mapbox.Unity.Location;
using UnityEngine.UI;
using Mapbox.Utils;
using Scripts.Locator;


public class LocationStreetPanelScript : MonoBehaviour
{
    private TextMeshProUGUI locationTextMeshAddress;
    private GameObject address1;
    private AbstractMap map;
    public Locator BingMapsClassesLocatorScript;

    // Start is called before the first frame update
    void Start()
    {
        locationTextMeshAddress = gameObject.GetComponent<TextMeshProUGUI>();
        address1 = GameObject.Find("Player");
        map = GameObject.Find("Map").GetComponent<AbstractMap>();
        BingMapsClassesLocatorScript = address1.GetComponent<Locator>();
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