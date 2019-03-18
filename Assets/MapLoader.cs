using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mapbox;
using Mapbox.Utils;
using UnityEditor;
using Mapbox.Unity.Map;


public class MapLoader : MonoBehaviour
{
	private AbstractMap map;
	
	// Start is called before the first frame update
	void Start()
    {

		/*map = GameObject.Find("Map").GetComponent<AbstractMap>();
		int zoom = map.AbsoluteZoom;
		double currentX = (double)PlayerPrefs.GetFloat("currentX");
		double currentY = (double)PlayerPrefs.GetFloat("currentY");

		Vector2d coordinateVector = new Vector2d(currentX, currentY);
		map.UpdateMap(coordinateVector, zoom);*/
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
