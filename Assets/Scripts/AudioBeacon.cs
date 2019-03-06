using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using Mapbox.Unity.Map;
using Mapbox.Utils;



public class AudioBeacon : MonoBehaviour
{
	

	public void setActiveBeacon(Vector3 unityLocation)
	{
		gameObject.SetActive(true);
		transform.position = unityLocation;
		gameObject.GetComponent<AudioSource>().Play();
	}
	

	public void disableBeacon()
	{
		gameObject.GetComponent<AudioSource>().Stop();
		//transform.position = Vector3.zero;
		//gameObject.SetActive(false);
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.name == "Player")
		{
			disableBeacon();
		}
	}


}

