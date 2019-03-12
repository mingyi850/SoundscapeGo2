using System;
using System.Threading;
using UnityEngine.Networking;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System;
using System.Net;
using System.Text;
using System.IO;
using System.Threading;
using Mapbox.Unity;
using Mapbox.Unity.Map;
using Mapbox.Utils;

namespace TTS
{
	public class TextToSpeechHandler: MonoBehaviour
	{
		public AudioSource directionalVoiceSource;
		public AudioSource nonDirectionalVoiceSource;
		private Queue<Vector3> audioDirQueue;
		private Queue<LocalisedAudioClip> dirAudioQueue;
		string ttsHost = "https://westeurope.tts.speech.microsoft.com/cognitiveservices/v1";
		private bool ready = false; //any action to be preceeded by callout

		void Awake()
		{
			
			audioDirQueue = new Queue<Vector3> ();
			dirAudioQueue = new Queue<LocalisedAudioClip>();
		}

		public string GetAccessToken()
		{
			return AzureSpeechToken.AzureSpeechServicesToken.Instance.GetAccessToken ();
		}

	
		public IEnumerator GetTextToSpeech(string text, int order, Vector3 location, bool directionalFlag) {
			string accessToken = GetAccessToken();
			Debug.Log ("Access Token: " + accessToken);
			WWWForm form = new WWWForm ();
			string body = @"<speak version='1.0' xmlns='http://www.w3.org/2001/10/synthesis' xml:lang='en-GB'>
              <voice name='Microsoft Server Speech Text to Speech Voice (en-GB, HazelRUS)'>" +
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
				request.SetRequestHeader("Authorization", "Bearer " + accessToken);
				// Update your resource name
				request.SetRequestHeader("User-Agent", "Text2Speech2");
				request.SetRequestHeader("X-Microsoft-OutputFormat", "audio-16khz-128kbitrate-mono-mp3");
				request.SetRequestHeader ("Content-Type", "application/ssml+xml");
				// Create a request
				Debug.Log(request.GetRequestHeader("Authorization"));
				Debug.Log(request.GetRequestHeader("User-Agent"));
				Debug.Log(request.GetRequestHeader("X-Microsoft-OutputFormat"));
				Debug.Log("Calling the TTS service. Please wait... \n");
				Debug.Log("Request Body: " + System.Text.Encoding.UTF8.GetString(request.uploadHandler.data));
				yield return request.SendWebRequest ();
				if (request.isNetworkError || request.isHttpError) {
					Debug.Log (request.error);
					Debug.Log (request.ToString ());
					Debug.Log (request.downloadHandler.data);
					//Use NULL Object pattern to prevent breaking.
					dirAudioQueue.Enqueue(LocalisedAudioClip.nullLocalisedAudioClip());
				} 
				else {
					//Debug.Log (request.ToString ());
					if (request.isDone) {
						Debug.Log ("File Length: " + request.downloadedBytes);
						AudioClip audioFile = downloader.audioClip;
						if (directionalFlag == true)
						{
							yield return new WaitWhile(() => dirAudioQueue.Count < order);
							dirAudioQueue.Enqueue(new LocalisedAudioClip(audioFile, location));
							Debug.Log("Enqueueing, current: " + dirAudioQueue.Count);
						}
						else
						{
							nonDirectionalVoiceSource.clip = audioFile;
							nonDirectionalVoiceSource.Play();
						}

					}

				}

			}


		}
		public IEnumerator playDirAudioQueue(int featureCount) {
			yield return new WaitWhile (() => dirAudioQueue.Count < featureCount);
			Debug.Log ("dirAudioQueue: " + dirAudioQueue.Count);
			while (dirAudioQueue.Count != 0){
				yield return new WaitWhile (() => directionalVoiceSource.isPlaying);
				LocalisedAudioClip currentAudio = dirAudioQueue.Dequeue();
				Debug.Log("Dequeueing, current: " + dirAudioQueue.Count);
				playSingleDirAudio (currentAudio);

			}
			dirAudioQueue.Clear ();

			yield return null;
		}

		public IEnumerator playDirAudioFromQueue() {
			Debug.Log ("Playing Dir Audio Queue");
			yield return new WaitWhile (() => (dirAudioQueue.Count < 1));
			yield return new WaitWhile (() => directionalVoiceSource.isPlaying);
			
			LocalisedAudioClip currentAudio = dirAudioQueue.Dequeue();
			playSingleDirAudio(currentAudio);

			yield return null;
		}

		public void playSingleDirAudio(LocalisedAudioClip currentAudio) {
			directionalVoiceSource.clip = currentAudio.AudioFile;
			Debug.Log(directionalVoiceSource.clip.length + " is the length of this clip");
			Vector3 audioLocation = currentAudio.UnityLocation;
			audioLocation.y = 1;
			directionalVoiceSource.transform.position = audioLocation;
			Debug.Log ("Current Location: " + transform.position.ToString() + "    " + audioLocation.ToString());
			directionalVoiceSource.Play ();
	
		}

		public IEnumerator playTTS(string toBeConverted)
		{
			
			StartCoroutine(GetTextToSpeech(toBeConverted, 0, Vector3.zero, false));
			yield return new WaitWhile(() => nonDirectionalVoiceSource.isPlaying);

			

		}
			

	}


}
