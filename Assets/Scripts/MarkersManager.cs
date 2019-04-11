using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mapbox.Utils;

public class MarkersManager : MonoBehaviour
{
    private static int playerPrefsCurrentArraySize = 0;
    public static int playerPrefsMaxArraySize = 100;
    public Dictionary<string, Vector2d> savedMarkers = new Dictionary<string, Vector2d>();
    string [] nameList = new string[playerPrefsMaxArraySize];
    //Vector2d [] coordinateList = new Vector2d[playerPrefsMaxArraySize];
    string [] XList = new string[playerPrefsMaxArraySize];
    string [] YList = new string[playerPrefsMaxArraySize];

    // Start is called before the first frame update
    void Start()
    {
        ReadPlayerPrefsData();
        DisplayMarkersDictionary();
        PlayerPrefs.GetInt("listLength", playerPrefsCurrentArraySize);
    }

    void OnApplicationQuit()
    {
        SavePlayerPrefsData();
    }

    private void ReadPlayerPrefsData() 
    {

        nameList = PlayerPrefsX.GetStringArray("nameList", "null", playerPrefsMaxArraySize);
        //coordinateList = PlayerPrefsX.GetVector2Array("coordinateList", Vector2d.zero, playerPrefsMaxArraySize);
        XList = PlayerPrefsX.GetStringArray("XList", "null", playerPrefsMaxArraySize);
        YList = PlayerPrefsX.GetStringArray("YList", "null", playerPrefsMaxArraySize);

        int j = 0;

        while (nameList[j] != "null")
        {
            j++;
        }

        playerPrefsCurrentArraySize = j;

        for (int i = 0; i < playerPrefsCurrentArraySize; i++)
        {
            savedMarkers.Add(nameList[i], ParseStringIntoVector(XList[i], YList[i]));
        }      
    }

    private Vector2d ParseStringIntoVector(string xs, string ys)
    {
        double x = double.Parse(xs, System.Globalization.CultureInfo.InvariantCulture);
        double y = double.Parse(ys, System.Globalization.CultureInfo.InvariantCulture);
        return new Vector2d(x, y);
    }

    private void SavePlayerPrefsData()
    {
        for (int i = 0; i < playerPrefsMaxArraySize; i++)
        {
            nameList[i] = "null";
            //coordinateList[i] = Vector2d.zero;
            XList[i] = "null";
            YList[i] = "null";
        }
        int j = 0;
        foreach(KeyValuePair<string, Vector2d> entry in savedMarkers)
        {
            // do something with entry.Value or entry.Key
            nameList[j] = entry.Key;
            //coordinateList[j] = entry.Value;
            XList[j] = entry.Value.x.ToString();
            YList[j] = entry.Value.y.ToString();
            j++;
        }
        PlayerPrefsX.SetStringArray("nameList", nameList);
        //PlayerPrefsX.SetVector2Array("coordinateList", coordinateList);
        PlayerPrefsX.SetStringArray("XList", XList);
        PlayerPrefsX.SetStringArray("YList", YList);
        PlayerPrefs.SetInt("listLength", playerPrefsCurrentArraySize);
    } 

    public void DisplayMarkersDictionary()
    {
        foreach(KeyValuePair<string, Vector2d> entry in savedMarkers)
        {
            // do something with entry.Value or entry.Key
            Debug.Log(entry.Key + ": " + entry.Value);
        }
    }

    public void SaveMarker(string name, Vector2d coordinates)
    {
        savedMarkers.Add(name, coordinates);
        playerPrefsCurrentArraySize++;
        DisplayMarkersDictionary();     // Debug 
    }

    public void DeleteMarker(string name)
    {
        savedMarkers.Remove(name);
        playerPrefsCurrentArraySize--;
        DisplayMarkersDictionary();     // Debug
    }

    public int isASavedMarker(string name)
    {
        if (savedMarkers.ContainsKey(name))
            return 1;
        else 
            return 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
