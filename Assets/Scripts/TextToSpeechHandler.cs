using System;
using System.Threading;
using UnityEngine.Networking;
using System.Collections.Generic;
using System.Collections;
using Utils;
using UnityEngine;
using System;
using System.Net;
using System.Text;
using System.IO;
using System.Threading;

namespace TTS
{
	public class TextToSpeechHandler: MonoBehaviour
	{
		public static readonly string FetchTokenUri = "https://westeurope.api.cognitive.microsoft.com/sts/v1.0/issuetoken";
		private string subscriptionKey = "2f9e610344d9487fa3c18575739cb9bf";
		private string currentToken;
		private Timer accessTokenRenewer;
		private AudioSource voiceSource;
		string ttsHost = "https://westeurope.tts.speech.microsoft.com/cognitiveservices/v1";


		//Access token expires every 10 minutes. Renew it every 9 minutes.
		private const int RefreshTokenDuration = 9;

		void Awake()
		{
			voiceSource = gameObject.GetComponent<AudioSource> ();
			Debug.Log ("Fetching Token from TTS server");
			StartCoroutine(FetchToken(FetchTokenUri, subscriptionKey));

			// renew the token on set duration.
			accessTokenRenewer = new Timer(new TimerCallback(OnTokenExpiredCallback),
				this,
				TimeSpan.FromMinutes(RefreshTokenDuration),
				TimeSpan.FromMilliseconds(-1));
		}

		public string GetAccessToken()
		{
			return currentToken;
		}

		private void RenewAccessToken()
		{
			StartCoroutine(FetchToken(FetchTokenUri, this.subscriptionKey));
			Debug.Log("Renewed token.");
		}

		private void OnTokenExpiredCallback(object stateInfo)
		{
			try
			{
				RenewAccessToken();
			}
			catch (Exception ex)
			{
				Console.WriteLine(String.Format("Failed renewing access token. Details: {0}", ex.Message));
			}
			finally
			{
				try
				{
					accessTokenRenewer.Change(TimeSpan.FromMinutes(RefreshTokenDuration), TimeSpan.FromMilliseconds(-1));
				}
				catch (Exception ex)
				{
					Console.WriteLine(String.Format("Failed to reschedule the timer to renew access token. Details: {0}", ex.Message));
				}
			}
		}

		IEnumerator FetchToken(String fetchUri, String subscriptionKey)
		{
			WWWForm form = new WWWForm ();
			UnityWebRequest request = UnityWebRequest.Post (fetchUri, form);
			request.SetRequestHeader ("Ocp-Apim-Subscription-Key", subscriptionKey);
			//request.SetRequestHeader("Ocp-Apim-Subscription-Key", subscriptionKey);

			yield return request.SendWebRequest ();
			if (request.isNetworkError || request.isHttpError) {
				Debug.Log (request.error);
			} 
			else {
				Debug.Log (request.ToString ());
				currentToken = request.downloadHandler.text;
				Debug.Log ("Token: " + currentToken);
			}


		}
		IEnumerator getTextToSpeechTest() {
			string accessToken = currentToken;
			WWWForm form = new WWWForm ();
			string text = "Hi, my name is joe";
			string body = @"<speak version='1.0' xmlns='http://www.w3.org/2001/10/synthesis' xml:lang='en-US'>
              <voice name='Microsoft Server Speech Text to Speech Voice (en-US, ZiraRUS)'>" +
				text + "</voice></speak>";
			using (var request = new UnityWebRequest())
			{
				// Set the HTTP method
				// Construct the URI

				DownloadHandlerAudioClip downloader = new DownloadHandlerAudioClip(ttsHost, AudioType.MPEG);
				request.downloadHandler = downloader;
				request.url = ttsHost;
				request.method = UnityWebRequest.kHttpVerbPOST;
				// Set the content type header
				byte[] textToSend = new System.Text.UTF8Encoding().GetBytes(body);
				Debug.Log ("Text to send: " + textToSend);
				request.uploadHandler = (UploadHandler)new UploadHandlerRaw(textToSend);
				//uploader.data = new StringContent(body, Encoding.UTF8, "application/ssml+xml");

				// Set additional header, such as Authorization and User-Agent
				request.SetRequestHeader("Authorization", "Bearer " + currentToken);
				// Update your resource name
				request.SetRequestHeader("User-Agent", "Text2Speech2");
				request.SetRequestHeader("X-Microsoft-OutputFormat", "audio-16khz-128kbitrate-mono-mp3");
				request.SetRequestHeader ("Content-Type", "application/ssml+xml");
				// Create a request
				Debug.Log(request.GetRequestHeader("Authorization"));
				Debug.Log(request.GetRequestHeader("User-Agent"));
				Debug.Log(request.GetRequestHeader("X-Microsoft-OutputFormat"));
				Debug.Log("Calling the TTS service. Please wait... \n");
				yield return request.SendWebRequest ();
				if (request.isNetworkError || request.isHttpError) {
					Debug.Log (request.error);
					Debug.Log (request.ToString ());
					Debug.Log (request.downloadHandler.data);
				} 
				else {
					//Debug.Log (request.ToString ());
					if (request.isDone) {
						Debug.Log (request.downloadedBytes);
						AudioClip audioFile = downloader.audioClip;
						Debug.Log (audioFile.length);
						voiceSource.clip = audioFile;
						voiceSource.Play ();
					}

				}

			}
				
		}
		public IEnumerator GetTextToSpeech(string text) {
			string accessToken = currentToken;
			WWWForm form = new WWWForm ();
			string body = @"<speak version='1.0' xmlns='http://www.w3.org/2001/10/synthesis' xml:lang='en-US'>
              <voice name='Microsoft Server Speech Text to Speech Voice (en-US, ZiraRUS)'>" +
				text + "</voice></speak>";
			using (var request = new UnityWebRequest())
			{
				// Set the HTTP method
				// Construct the URI

				DownloadHandlerAudioClip downloader = new DownloadHandlerAudioClip(ttsHost, AudioType.MPEG);
				request.downloadHandler = downloader;
				request.url = ttsHost;
				request.method = UnityWebRequest.kHttpVerbPOST;
				// Set the content type header
				byte[] textToSend = new System.Text.UTF8Encoding().GetBytes(body);
				Debug.Log ("Text to send: " + textToSend);
				request.uploadHandler = (UploadHandler)new UploadHandlerRaw(textToSend);
				//uploader.data = new StringContent(body, Encoding.UTF8, "application/ssml+xml");

				// Set additional header, such as Authorization and User-Agent
				request.SetRequestHeader("Authorization", "Bearer " + currentToken);
				// Update your resource name
				request.SetRequestHeader("User-Agent", "Text2Speech2");
				request.SetRequestHeader("X-Microsoft-OutputFormat", "audio-16khz-128kbitrate-mono-mp3");
				request.SetRequestHeader ("Content-Type", "application/ssml+xml");
				// Create a request
				Debug.Log(request.GetRequestHeader("Authorization"));
				Debug.Log(request.GetRequestHeader("User-Agent"));
				Debug.Log(request.GetRequestHeader("X-Microsoft-OutputFormat"));
				Debug.Log("Calling the TTS service. Please wait... \n");
				yield return request.SendWebRequest ();
				if (request.isNetworkError || request.isHttpError) {
					Debug.Log (request.error);
					Debug.Log (request.ToString ());
					Debug.Log (request.downloadHandler.data);
				} 
				else {
					//Debug.Log (request.ToString ());
					if (request.isDone) {
						Debug.Log (request.downloadedBytes);
						AudioClip audioFile = downloader.audioClip;
						Debug.Log (audioFile.length);
						voiceSource.clip = audioFile;
						voiceSource.Play ();
					}

				}

			}

		}


	}
}

