using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Mapbox.Utils;
using Scripts.BingMapClassesLocator;

public class locationStreetPanelScript : MonoBehaviour
{
    //private TextMeshProUGUI locationTextMesh;
    //private GameObject address1;
    public Locator LocatorScript;

    // Start is called before the first frame update
    void Start()
    {
        //locationTextMesh = gameObject.GetComponent<TextMeshProUGUI>();
        //address1 = GameObject.Find("Address");
        //LocatorScript = address1.GetComponent<Locator>();
        StartCoroutine(CoroutineAddress());
    }

    public IEnumerator CoroutineAddress()
    {
        while (true)
        {
            //locationTextMesh.text = playerLocationScript.getLongLat().ToString();
            //string address = LocatorScript.getAddress();
            //Debug.Log(address);
            Debug.Log("CoroutineAddress: " + Time.time);
            yield return new WaitForSeconds(5f);
        }
    }



    //public IEnumerator CoroutineAddress()
    //{
    //    while (true)
    //    {
    //        locationTextMesh.text = playerLocationScript.getLongLat().ToString();
    //        Debug.Log("CoroutineAddress: " + Time.time);
    //        Debug.Log("CoroutineAddress: " + locationTextMesh);
    //        Debug.Log("CoroutineAddress: " + playerLocationScript);
    //        Debug.Log("getLongLat: " + Time.time);
    //        Debug.Log("ToString: " + Time.time);


    //        Debug.Log(player1);
    //        Debug.Log(locationTextMesh);
    //        Debug.Log(playerLocationScript);
    //        yield return new WaitForSeconds(5f);
    //    }
    //}

    // Update is called once per frame
    //void Update()
    //{
    //    StartCoroutine(CoroutineAddress());
    //    // getAddress
    //    //locationTextMeshAddress.text = address;
    //}
}

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
