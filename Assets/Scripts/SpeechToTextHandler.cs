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

		public IEnumerator GetSpeechToText(byte[] fileName) { //"fileName" here refers to either a string containing the file path to be used with UploadHandlerFile, or a byte arr containing a Wav file.
			//parameters:
			string language = "en-GB";
			string format = "simple";
			string accessToken = GetAccessToken();
			DownloadHandlerBuffer downloader = new DownloadHandlerBuffer ();
			WWWForm form = new WWWForm ();
			string queryString = string.Format ("?language={0}&format={1}", language, format);
			string endpoint = sttHost + queryString;
			form.AddBinaryData ("data", fileName);
			UnityWebRequest request = new UnityWebRequest ();

			// Set the HTTP method
			// Construct the URI
			request.downloadHandler = downloader;
			request.url = endpoint;
			request.method = UnityWebRequest.kHttpVerbPOST;
			request.SetRequestHeader("Content-Type", @"audio/wav; codecs=audio/pcm; samplerate=16000");
			request.SetRequestHeader("Authorization", "Bearer " + accessToken);
			request.SetRequestHeader("Accept", "application/json;text/xml");
			request.uploadHandler = (UploadHandler)(new UploadHandlerRaw (fileName));
			request.uploadHandler.contentType = @"audio/wav; codecs=audio/pcm; samplerate=16000";
			debugRequest (request);

			Debug.Log ("Endpoint: " + request.url);
			Debug.Log ("File to send: " + fileName);
			//request.uploadHandler = new UploadHandlerRaw(fileName); //FIX HERE 
			//uploader.data = new StringContent(body, Encoding.UTF8, "application/ssml+xml");

			// Create a request
			debugRequest (request);
			Debug.Log(request.GetRequestHeader("Authorization"));
			Debug.Log("Calling the STT service. Please wait... \n");
			yield return request.SendWebRequest ();
			if (request.isNetworkError || request.isHttpError) {
				Debug.Log (request.error);
				Debug.Log (request.responseCode);
				Debug.Log (request.downloadHandler.data);
				Debug.Log (request.downloadHandler.text);
			} 
			else {
				//Debug.Log (request.ToString ());
				if (request.isDone) {
					Debug.Log ("File Length: " + request.downloadedBytes);

				}

			}




		}

		private void debugRequest(UnityWebRequest requester) {
			UploadHandler uploader = requester.uploadHandler;
			Debug.Log ("Upload Data length: " + uploader.data.Length);
			Debug.Log ("Upload Data Type: " + uploader.data.ToString ());
			Debug.Log ("Upload Handler Content Type: " + uploader.contentType);

			Debug.Log ("Request Header Content-Type: " + requester.GetRequestHeader ("Content-Type"));
			Debug.Log ("URL: " + requester.url);
		}

			
	}
}

