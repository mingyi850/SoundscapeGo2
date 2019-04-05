using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BingMapsEntities;

public class EntitiesList : MonoBehaviour
{
    public List<string> namesList;
    public List<int> codesList;
	private BingMapsEntityId entitiesDictionary;
    // Start is called before the first frame update
    void Start()
    {
		entitiesDictionary = GameObject.Find("BingMapsEntityMap").GetComponent<BingMapsEntityId>();
        foreach (string name in namesList)
        {
            string s = BingMapsEntityId.getIdFromEntityName(name);
            int xs = Int32.Parse(s);
            codesList.Add(xs);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
