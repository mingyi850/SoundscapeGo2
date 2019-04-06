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
	private bool newSearchResultPassed = false;
	private ForwardGeocodeUserInput forwardGeocoder;
	// Start is called before the first frame update
	void Start()
	{
		forwardGeocoder = gameObject.GetComponent<ForwardGeocodeUserInput>();
		newSearchResultPassed = false;
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

	private IEnumerator passSearchResultToPlayerPrefs()
	{
		yield return new WaitUntil(() => forwardGeocoder.HasResponse);
		Vector2d currentCoordinate = forwardGeocoder.Coordinate;
		Debug.Log("Geocoder current coordinate" + currentCoordinate.ToString());
		passCoordinateToPlayerPrefs(currentCoordinate);
		newSearchResultPassed = true;
	}


	private IEnumerator goToSetLocationScene()
	{
		StartCoroutine(passSearchResultToPlayerPrefs());
		yield return new WaitUntil(() => newSearchResultPassed);
		if ((PlayerPrefs.GetFloat("currentX") != 0.000f) && (PlayerPrefs.GetFloat("currentY") != 0.000f))
		{
			SceneManager.LoadScene("Navigation Scene");
		}
		else
		{
			Debug.Log("Invalid Coordinatnes");
		}
	}

	public void LoadNextPageFunction()
	{
		StartCoroutine(goToSetLocationScene());
	}
}
