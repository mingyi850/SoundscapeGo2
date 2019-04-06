using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BingMapsEntities;

public class FiltersManager : MonoBehaviour
{
    public Dropdown dropdown;

    private List<int> settingsList = new List<int>();
    private int searchRadius = 200;

    // Start is called before the first frame update
    void Start()
    {
        dropdown.value = ConvertRadiusToIndex(PlayerPrefs.GetInt("SearchRadius", 200));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ModifyFilterSettings()
    {
        settingsList.Clear();
        foreach (Transform child in transform)
        {
            if (child.gameObject.GetComponent<Toggle>().isOn)
            {
                settingsList.AddRange(child.gameObject.GetComponent<EntitiesList>().codesList);
            }
        }
        int[] settingsArray = settingsList.ToArray();
        PlayerPrefsX.SetIntArray("EntitiesCodes", settingsArray);
		Debug.Log(settingsArray.Length);
		
    }

    public void ModifySearchRadius()
    {
        int index = dropdown.value;
        if (index == 0)
            searchRadius = 50;
        if (index == 1)
            searchRadius = 200;
        if (index == 2)
            searchRadius = 500;
        if (index == 3)
            searchRadius = 1000;
        if (index == 4)
            searchRadius = 2000;
        Debug.Log(searchRadius);
        PlayerPrefs.SetInt("SearchRadius", searchRadius);
    }

    private int ConvertRadiusToIndex(int radius)
    {
        if (radius == 200)
            return 1;
        if (radius == 500)
            return 2;
        if (radius == 1000)
            return 3;
        if (radius == 2000)
            return 4;
        else return 0;
    }
}
