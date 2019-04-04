using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Mapbox.Utils;
using Scripts.DistanceCalc;

public class SampleDestinationButton : MonoBehaviour
{
    public Button button;
    public Text nameLabel;
    public Text distanceLabel;


    private Destination destination;
    private ScrollList scrollList;


    public void Setup(Destination currentDestination, ScrollList currentScrollList)
    {
        destination = currentDestination;
        nameLabel.text = destination.destinationName;
        destination.coordinates.x = currentDestination.coordinates.x;
        destination.coordinates.y = currentDestination.coordinates.y;
        scrollList = currentScrollList;
    }

	private void passCoordinateToPlayerPrefs(Vector2d coordinates)
	{
		float xCoordinate = (float)coordinates.x;
		float yCoordinate = (float)coordinates.y;
		PlayerPrefs.SetFloat("currentX", xCoordinate);
		PlayerPrefs.SetFloat("currentY", yCoordinate);
	}

    public void LoadNextScene()
    {
        passCoordinateToPlayerPrefs(destination.coordinates);
        SceneManager.LoadScene("Navigation Scene");
    }
}
