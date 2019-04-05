using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using TTS;
using ButtonSelect;
using Scripts.BingMapClassesLocator;
using BingSearch;

public class LayeredButton : MonoBehaviour
{

	private ButtonSelector buttonSelector;
	private PlayerLocation playerlocation;
	private TextToSpeechHandler ttsHandler;
	private BingMapsClassesLocator locator;
	private UIManager uiManager;
	private string buttonText;
	private BingSearchHandler bingSearchHandler;

	public string buttonID;
	// Start is called before the first frame update
	void Start()
	{
		buttonText = gameObject.GetComponentInChildren<Text>().text;
		
		buttonSelector = GetComponentInParent<ButtonSelector>();
		playerlocation = GameObject.Find("Player").GetComponent<PlayerLocation>();
		ttsHandler = GameObject.Find("TextToSpeechHandler").GetComponent<TextToSpeechHandler>();
		uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
		locator = GameObject.Find("Player").GetComponent<BingMapsClassesLocator>();
		bingSearchHandler = GameObject.Find("BingSearchHandler").GetComponent<BingSearchHandler>();
	}

	// Update is called once per frame
	void Update()
	{

	}

	public void aroundMeButtonPressed()
	{
		if (buttonSelector.getLastClicked() != buttonID)
		{
			firstClick();
		}
		else
		{
			playerlocation.getNearbyFeatures(0);
			buttonSelector.setLastClickedId("");
		}
	}

	public void aheadOfMeButtonPressed()
	{
		if (buttonSelector.getLastClicked() != buttonID)
		{
			firstClick();
		}
		else
		{
			playerlocation.getNearbyFeatures(1);
			buttonSelector.setLastClickedId("");
		}
	}

	public void myLocationButtonPressed()
	{
		if (buttonSelector.getLastClicked() != buttonID)
		{
			firstClick();
		}
		else
		{
			string addressString = locator.getAddress();
			string bingMapsQuery = string.Format("https://bing.com/maps/default.aspx?where1={0}", addressString);

			#if UNITY_ANDROID
			Debug.Log("Android branch mylocastion");
			bingMapsQuery = string.Format("http://maps.google.com/?q={0}", addressString);
			#endif

			Application.OpenURL(bingMapsQuery);
			buttonSelector.setLastClickedId("");
		}
	}

	public void homeButtonPressed()
	{
		if (buttonSelector.getLastClicked() != buttonID)
		{
			firstClick();
		}
		else
		{
			uiManager.ChangeToHomeScene();
			buttonSelector.setLastClickedId("");
		}
	}

	public void menuButtonPressed()
	{
		if (buttonSelector.getLastClicked() != buttonID)
		{
			firstClick();
		}
		else
		{
			uiManager.menuButtonClicked();
			buttonSelector.setLastClickedId("");
		}
	}



	public void firstClick()
	{
		StartCoroutine(ttsHandler.GetTextToSpeech(buttonText, 0, Vector3.zero, false));
		buttonSelector.setLastClickedId(buttonID);
	}
}


