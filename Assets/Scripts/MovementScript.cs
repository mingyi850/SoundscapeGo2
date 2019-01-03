using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mapbox.Unity.Utilities;
using Mapbox.Utils;
using Mapbox.Unity.Map;
using Mapbox.Unity.Location;


public class MovementScript : MonoBehaviour
{
	// Identifies which player this belongs to

	// How quickly player moves forward and back
	public float speed;

	// How quickly player rotates (degrees per second)
	public float rotationSpeed = 180f;
	private Vector2d mapCenterLongLat; 
	private Rigidbody body;
	private CharacterController controller;
	private PlayerLocation playerLocation;



	// Use this for initialization
	void Start ()
	{
		// Retrieve reference to this GameObject's Rigidbody component
		controller = GetComponent<CharacterController>();
		body = GetComponent<Rigidbody>();
		transform.position = new Vector3 (0.0f, 0.0f, 0.0f);



	}

	// Update is called once per frame
	void FixedUpdate ()
	{
		
		// Get movement input value
		float movementInput = GetMovementInput();
		float turbo = getTurboInput ();
		// Determine amount to move based on current forward direction and speed
		Vector3 movement = transform.forward * movementInput * speed * Time.deltaTime * turbo;

		// Move our Rigidbody to this position
		controller.Move(movement);

		// Get rotation input value
		float rotationInput = GetRotationInput();

		// Determine number of degrees to turn based on rotation speed
		float degreesToTurn = rotationInput * rotationSpeed * Time.deltaTime;

		// Get Quaternion equivalent of this amount of rotation around the y-axis
		Quaternion rotation = Quaternion.Euler(0f, degreesToTurn, 0f);

		// Rotate our Rigidbody by this amount
		body.MoveRotation(body.rotation * rotation);
		//Debug.Log ("Direction Facing: " + transform.forward.ToString ());
	}

	// Returns input values of 0, 1 or -1 based on whether Player tries to move forward or back
	float GetMovementInput()
	{
		// Player 1 moves forward and back with W and S; Player 2 with Up and Down arrows
		KeyCode positiveKey = KeyCode.W;
		KeyCode negativeKey = KeyCode.S;

		if (Input.GetKey(positiveKey))
		{
			return 1f;
		}
		else if (Input.GetKey(negativeKey))
		{
			return -1f;
		}
		else
		{
			return 0f;
		}
	}

	// Returns input values of 0, 1 or -1 based on whether Player tries to rotate right or left
	float GetRotationInput()
	{
		// Player 1 rotates with A and D; Player 2 with Left and Right arrows 
		KeyCode positiveKey = KeyCode.D;
		KeyCode negativeKey = KeyCode.A;

		if (Input.GetKey(positiveKey))
		{
			return 1f;
		}
		else if (Input.GetKey(negativeKey))
		{
			return -1f;
		}
		else
		{
			return 0f;
		}
	}

	float getTurboInput() {
		KeyCode turbo = KeyCode.LeftShift;
		if (Input.GetKey(turbo)) {
			return 4f;
		}
		else {
			return 1f;
		}
	}
			

	void OnCollisionEnter(Collision collision) {
		Vector3 movement = transform.forward * speed * 0.05f;

		Debug.Log ("Collision");
		//body.velocity = Vector3.zero;
		//body.MovePosition (body.position - movement);
	}



}