using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapCameraZoom : MonoBehaviour
{
	private int state = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	public void changeZoomState()
	{
		state++;
		state = state % 4;
		float newY = (state + 1) * 21;
		float currentX = transform.position.x;
		float currentZ = transform.position.z;
		Debug.Log("Theoretical new Y = " + newY);
		transform.position = new Vector3(currentX, newY, currentZ);
		Debug.Log("Actual Y now = " + transform.position.y);
		Debug.Log("Current Zoom State " + state);
	}
}
