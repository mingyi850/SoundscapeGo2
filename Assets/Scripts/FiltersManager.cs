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
	private bool initialised = false;

	// Start is called before the first frame update
	void Start()
	{
		dropdown.value = ConvertRadiusToIndex(PlayerPrefs.GetInt("SearchRadius", 200));
		loadFilterToggleSettings();
		initialised = true;
		
	}

	// Update is called once per frame
	void Update()
	{

	}

	public void ModifyFilterSettings()
	{
		if (!initialised)
		{
			return;
		}
		settingsList.Clear();
		foreach (Transform child in transform)
		{
			if (child.gameObject.GetComponent<Toggle>().isOn)
			{
				settingsList.AddRange(child.gameObject.GetComponent<EntitiesList>().codesList);
			
				PlayerPrefs.SetInt(child.name, 1);
				Debug.Log("setting " + child.name + " to active");
					
			}
			else
			{
				PlayerPrefs.SetInt(child.name, 0);
				Debug.Log("setting " + child.name + " to inactive");
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
			searchRadius = 200;
		if (index == 1)
			searchRadius = 500;
		if (index == 2)
			searchRadius = 1000;
		if (index == 3)
			searchRadius = 2000;
		if (index == 4)
			searchRadius = 5000;
		Debug.Log(searchRadius);
		PlayerPrefs.SetInt("SearchRadius", searchRadius);
	}

	private int ConvertRadiusToIndex(int radius)
	{
		if (radius == 200)
			return 0;
		if (radius == 500)
			return 1;
		if (radius == 1000)
			return 2;
		if (radius == 2000)
			return 3;
		if (radius == 5000)
			return 4;
		else
		{
			return 0;
		}
	}

	public void loadFilterToggleSettings()
	{
		foreach (Transform child in transform)
		{
			Debug.Log("loading " + child.name + " as " + PlayerPrefs.GetInt(child.name, 1));
			int value = PlayerPrefs.GetInt(child.name, 1);
			if (value == 0)
			{
				child.gameObject.GetComponent<Toggle>().isOn = false;
			}
			else
			{
				child.gameObject.GetComponent<Toggle>().isOn = true;
			}

		}
	}
}
