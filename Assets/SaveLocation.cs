using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mapbox;
using Mapbox.Utils;
using UnityEditor;
using Mapbox.Examples;
using UnityEngine.SceneManagement;


public class SaveLocation : MonoBehaviour
{
	private ForwardGeocodeUserInput fowardGeocoder;
	// Start is called before the first frame update
	void Start()
	{
		fowardGeocoder = gameObject.GetComponent<ForwardGeocodeUserInput>();
	}

	// Update is called once per frame
	void Update()
	{

	}

	void passCoordinateToPlayerPrefs(Vector2d coordinates)
	{
		float xCoordinate = (float)coordinates.x;
		float yCoordinate = (float)coordinates.y;
		PlayerPrefs.SetFloat("currentX", xCoordinate);
		PlayerPrefs.SetFloat("currentY", yCoordinate);
	}

	private void passSearchResultToPlayerPrefs()
	{
		Vector2d currentCoordinate = fowardGeocoder.Coordinate;
		passCoordinateToPlayerPrefs(currentCoordinate);
	}


	public void goToSetLocationScene()
	{
		passSearchResultToPlayerPrefs();
		SceneManager.LoadScene("Navigation Scene");
	}
}
