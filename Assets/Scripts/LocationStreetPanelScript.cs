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
    private GameObject player;
    private AbstractMap map;
    private BingMapsClassesLocator BingMapsClassesLocatorScript;
	private bool playerMoved = false;
	private Vector3 lastPosition;

    // Start is called before the first frame update
    void Start()
    {
		
        locationTextMeshAddress = gameObject.GetComponent<TextMeshProUGUI>();
        player = GameObject.Find("Player");
		lastPosition = player.transform.position;
		map = GameObject.Find("Map").GetComponent<AbstractMap>();
        BingMapsClassesLocatorScript = player.GetComponent<BingMapsClassesLocator>();
        StartCoroutine(CoroutineAddress());
    }

    public IEnumerator CoroutineAddress()
    {
		Debug.Log("Coroutine Address Started");
        yield return new WaitUntil(() => map.isReady == true);
        while (true)
        {
			yield return new WaitForSeconds(5f);
			if (player.transform.position != lastPosition)
			{
				Debug.Log("Player moved");
				locationTextMeshAddress.text = BingMapsClassesLocatorScript.getAddress();
				yield return null;
				Debug.Log("CoroutineAddress: " + Time.time);
			}
        }
    }
}