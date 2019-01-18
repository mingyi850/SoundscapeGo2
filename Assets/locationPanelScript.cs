using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class locationPanelScript : MonoBehaviour
{
	private TextMeshProUGUI locationTextMesh;
	private GameObject player1;
	private PlayerLocation playerLocationScript;
    // Start is called before the first frame update
    void Start()
    {
		locationTextMesh = gameObject.GetComponent<TextMeshProUGUI> ();
		player1 = GameObject.Find ("Player");
		playerLocationScript = player1.GetComponent<PlayerLocation> ();
    }

    // Update is called once per frame
    void Update()
    {
		locationTextMesh.text = playerLocationScript.getLongLat ().ToString();
    }
}
