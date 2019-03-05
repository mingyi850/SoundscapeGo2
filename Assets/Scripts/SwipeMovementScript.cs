using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SwipeMovementScript : MonoBehaviour
{
	private Touch initTouch = new Touch();
	private Transform playerTransform;
	private CharacterController playerControl;
	private Rigidbody playerBody;
	private Vector3 originalRotation;
	public float rotationSpeed;
	public float movementSpeed;
	private float yRotation;
	private Animator animator;

	void Start() {
		playerTransform = gameObject.transform;
		playerControl = gameObject.GetComponent<CharacterController> ();
		playerBody = gameObject.GetComponent<Rigidbody> ();
		originalRotation = playerTransform.eulerAngles;
		yRotation = originalRotation.y;
		animator = GetComponent<Animator>();

	}

	void FixedUpdate() {
		foreach (Touch touch in Input.touches) {
			if (touch.phase == TouchPhase.Began) 
			{
				Debug.Log ("New Touch");
				initTouch = touch;
			} 
			else if (touch.phase == TouchPhase.Moved) {
				//check touch main direction
				float deltaX = touch.position.x - initTouch.position.x;
				float deltaY = touch.position.y - initTouch.position.y;
				float touchDirectionAngle = Mathf.Rad2Deg * Mathf.Atan2 (deltaY, deltaX);
				Debug.Log ("Touch Direction " + touchDirectionAngle);
				//filters touches for probable left/right swipes
				if (Mathf.Abs (touchDirectionAngle) > 150 || Mathf.Abs (touchDirectionAngle) < 30) {
					yRotation += deltaX * Time.deltaTime * rotationSpeed;
					float degreesToTurn = yRotation / 100;
					Debug.Log ("Delta X: " + deltaX);
					Quaternion rotation = Quaternion.Euler(0f, degreesToTurn, 0f);
					playerBody.MoveRotation (rotation);
					//playerTransform.eulerAngles = new Vector3 (0f, yRotation, 0f);

					initTouch = touch;
				} 
				else if (Mathf.Abs (touchDirectionAngle) < 110 && Mathf.Abs (touchDirectionAngle) > 70) {
					Debug.Log ("Delta Y: " + deltaY);
					float moveSpeed = deltaY * movementSpeed * Time.deltaTime;
					Vector3 movement = transform.forward * moveSpeed;
					playerControl.Move (movement);
					
				}

			} 
			else if (touch.phase == TouchPhase.Ended) {
				Debug.Log ("End Touch");
				initTouch = new Touch ();
			}
		}
		Debug.Log("magnitude: " + playerControl.velocity.magnitude);
		animator.SetFloat("MoveSpeed", playerControl.velocity.magnitude);
	}

}

