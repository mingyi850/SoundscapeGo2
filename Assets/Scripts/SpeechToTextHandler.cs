using System;
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

namespace STT
{
	public class SpeechToTextHandler : MonoBehaviour
	{
		string sttHost = "https://westeurope.tts.speech.microsoft.com/cognitiveservices/v1";

		void Awake() {}

		public string GetAccessToken()
		{
			return AzureSpeechToken.AzureSpeechServicesToken.Instance.GetAccessToken ();
		}

		public IEnumerator GetSpeechToText(string fileName) {
			//parameters:
			string language = "en-GB";
			string format = "simple";
			string accessToken = GetAccessToken();
			DownloadHandlerBuffer downloader = new DownloadHandlerBuffer ();

			WWWForm form = new WWWForm ();
			string queryString = string.Format ("language={0}&format={1}", language, format);
			using (var request = new UnityWebRequest())
			{
				// Set the HTTP method
				// Construct the URI
				request.downloadHandler = downloader;
				request.url = sttHost;
				request.method = UnityWebRequest.kHttpVerbPOST;
				// Set the content type header

				Debug.Log ("File to send: " + fileName);
				request.uploadHandler = new UploadHandlerFile(fileName); //FIX HERE 
				//uploader.data = new StringContent(body, Encoding.UTF8, "application/ssml+xml");

				// Set additional header, such as Authorization and User-Agent
				request.SetRequestHeader("Authorization", "Bearer " + accessToken);
				// Update your resource name
				request.SetRequestHeader("Accept", "application/json");
				request.chunkedTransfer = true;
				//request.SetRequestHeader("Content-Type", @"audio/wav; codec=audio/pcm; samplerate=16000");
				request.SetRequestHeader("Content-Type", @"application/octet-stream");
				Debug.Log(request.GetRequestHeader("Content-Type"));
					
				// Create a request
				Debug.Log(request.GetRequestHeader("Authorization"));
				Debug.Log("Calling the STT service. Please wait... \n");
				yield return request.SendWebRequest ();
				if (request.isNetworkError || request.isHttpError) {
					Debug.Log (request.error);
					Debug.Log (request.responseCode);
					Debug.Log (request.downloadHandler.data.Length);
				} 
				else {
					//Debug.Log (request.ToString ());
					if (request.isDone) {
						Debug.Log ("File Length: " + request.downloadedBytes);

					}

				}

			}


		}

			
	}
}

