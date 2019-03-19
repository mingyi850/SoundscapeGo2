#if !UNITY_EDITOR_OSX

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using Microsoft.CognitiveServices.Speech;

using System.Collections;
using System.Collections.Generic;
using System;
using TMPro;
using VoiceSearchTermsNS;
using UnityEngine.SceneManagement;
#if PLATFORM_ANDROID
using UnityEngine.Android;
#endif

public class SpeechSDKStart : MonoBehaviour
{
	// Hook up the two properties below with a Text and Button object in your UI.
	public TextMeshProUGUI outputText;
	public Button startRecoButton;

	private object threadLocker = new object();
	private bool waitingForReco;
	private string message;
	private string subscriptionKey = "2f9e610344d9487fa3c18575739cb9bf";
	private string serviceRegion = "westeurope";
	private bool micPermissionGranted = false;

	private bool recordStarted = false;
	private Touch initTouch = new Touch();
	private UnityEvent recordEvent;
	private float startTime = 0.0f;
	private Animator animator;
	public GameObject speechPanel;
	private VoiceSearchTerms searchTermManager;

	public Button aroundMeButton;
	public Button aheadOfMeButton;
	public Button myLocationbutton;
	public Button optionsButton;
	public Button homeButton;

	private string executableMessage = "";
	public string sceneName;



	#if PLATFORM_ANDROID
	// Required to manifest microphone permission, cf.
	// https://docs.unity3d.com/Manual/android-manifest.html
	private Microphone mic;
	#endif

	public async void ButtonClick()
	{
		// Creates an instance of a speech config with specified subscription key and service region.
		// Replace with your own subscription key and service region (e.g., "westus").
		
		var config = SpeechConfig.FromSubscription(subscriptionKey, serviceRegion);
		Debug.Log("Firing up speech recogniser");
		// Make sure to dispose the recognizer after use!
		using (var recognizer = new SpeechRecognizer(config))
		{
			lock (threadLocker)
			{
				waitingForReco = true;
			}

			// Starts speech recognition, and returns after a single utterance is recognized. The end of a
			// single utterance is determined by listening for silence at the end or until a maximum of 15
			// seconds of audio is processed.  The task returns the recognition text as result.
			// Note: Since RecognizeOnceAsync() returns only a single utterance, it is suitable only for single
			// shot recognition like command or query.
			// For long-running multi-utterance recognition, use StartContinuousRecognitionAsync() instead.
			var result = await recognizer.RecognizeOnceAsync().ConfigureAwait(false);

			// Checks result.
			string newMessage = string.Empty;
			if (result.Reason == ResultReason.RecognizedSpeech)
			{
				newMessage = result.Text;
			}
			else if (result.Reason == ResultReason.NoMatch)
			{
				newMessage = "NOMATCH: Speech could not be recognized.";
			}
			else if (result.Reason == ResultReason.Canceled)
			{
				var cancellation = CancellationDetails.FromResult(result);
				newMessage = $"CANCELED: Reason={cancellation.Reason} ErrorDetails={cancellation.ErrorDetails}";
			}

			lock (threadLocker)
			{
				message = newMessage;
				executableMessage = newMessage;
				waitingForReco = false;
				
			}
			
		}
		
	}

	void Start()
	{
		sceneName = SceneManager.GetActiveScene().name;
		searchTermManager = gameObject.GetComponent<VoiceSearchTerms>();
		animator = speechPanel.GetComponent<Animator>();
		recordEvent = new UnityEvent();
		if (outputText == null)
		{
			UnityEngine.Debug.LogError("outputText property is null! Assign a UI Text element to it.");
		}
		else if (startRecoButton == null)
		{
			message = "startRecoButton property is null! Assign a UI Button to it.";
			UnityEngine.Debug.LogError(message);
		}
		else
		{
			// Continue with normal initialization, Text and Button objects are present.

			#if PLATFORM_ANDROID
			// Request to use the microphone, cf.
			// https://docs.unity3d.com/Manual/android-RequestingPermissions.html
			message = "Waiting for mic permission";
			if (!Permission.HasUserAuthorizedPermission(Permission.Microphone))
			{
			Permission.RequestUserPermission(Permission.Microphone);
			}
			#else
			micPermissionGranted = true;
			message = "Click button to recognize speech";
			#endif
			startRecoButton.onClick.AddListener(ButtonClick);
			recordEvent.AddListener(ButtonClick);
		}
	}

	void Update()
	{
#if PLATFORM_ANDROID
		if (!micPermissionGranted && Permission.HasUserAuthorizedPermission(Permission.Microphone))
		{
		micPermissionGranted = true;
		
		}
#endif

		lock (threadLocker)
		{
			if (startRecoButton != null)
			{
				startRecoButton.interactable = !waitingForReco && micPermissionGranted;
			}
			if (outputText != null)
			{
				outputText.text = message;
			}

				
		}
		foreach (Touch touch in Input.touches)
		{
			if (touch.phase == TouchPhase.Began)
			{
				Debug.Log("New Touch: Speech Engine");
				initTouch = touch;
				startTime = Time.time;
				recordStarted = false;

			}
			else if (touch.phase == TouchPhase.Stationary)
			{
				Debug.Log("Staying: " + (Time.time - startTime));
				if (Math.Abs(startTime - Time.time) > 1.2 && recordStarted == false)
				{
					Debug.Log("Starting Recording");
					message = "Recognizing Speech......";
					recordStarted = true;
					recordEvent.Invoke();
					toggleSpeechPanel();
					Debug.Log("End of Recording");
				}
			}
			else if (touch.phase == TouchPhase.Ended)
			{
				if (animator.GetBool("open") == true)
				{
					toggleSpeechPanel();
				}
			}
		}
		if (executableMessage != "")
		{
			readMessageAction(executableMessage, sceneName);
			executableMessage = "";
		}

	}

	public void toggleSpeechPanel()
	{
		if (speechPanel != null)
		{
			if (animator != null)
			{
				bool isOpen = animator.GetBool("open");
				animator.SetBool("open", !isOpen);
				
			}
		}
	}

	public void readMessageAction(string newMessage, string sceneName) {
		Debug.Log("Reading Message: " + newMessage);
		if (sceneName == "Navigation Scene")
		{
			string[] words = newMessage.Split(' ');
			int actionCode = -1;
			Dictionary<string, int> lookupTable = searchTermManager.VoiceCommandLookupTable;
			Debug.Log("Words: " + words);
			foreach (string word in words)
			{
				string lowerWord = word.ToLower();
				if (lookupTable.ContainsKey(lowerWord))
				{
					actionCode = lookupTable[lowerWord];
					break;
				}
			}
			Debug.Log("Action Code: " + actionCode);
			switch (actionCode)
			{
				case ((int)VoiceAction.AROUND):
					aroundMeButton.onClick.Invoke();
					aroundMeButton.onClick.Invoke();
					Debug.Log("Called Around me");
					break;
				case ((int)VoiceAction.AHEAD):
					aheadOfMeButton.onClick.Invoke();
					aheadOfMeButton.onClick.Invoke();
					break;
				case ((int)VoiceAction.MYLOCATION):
					myLocationbutton.onClick.Invoke();
					myLocationbutton.onClick.Invoke();
					break;
				case ((int)VoiceAction.OPTIONS):
					optionsButton.onClick.Invoke();
					optionsButton.onClick.Invoke();
					break;
				case ((int)VoiceAction.HOME):
					homeButton.onClick.Invoke();
					homeButton.onClick.Invoke();
					break;
				default:
					message = "No Valid Command Found, Try Again";
					Debug.Log(message);
					break;

			}
		}
		else if (sceneName == "Destination Select")
		{
			InputField searchBarField = GameObject.Find("Search Bar").GetComponent<InputField>();
			Debug.Log("Destination Select Voice Activated");
			searchBarField.text = newMessage;

		}
	}










}
#endif