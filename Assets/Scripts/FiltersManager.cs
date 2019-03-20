using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BingMapsEntities;

public class FiltersManager : MonoBehaviour
{
    private List<int> settingsList = new List<int>();

    // Start is called before the first frame update
    void Start()
    {
        
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
    }
}
