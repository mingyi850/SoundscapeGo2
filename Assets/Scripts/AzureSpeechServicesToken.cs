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

namespace AzureSpeechToken
{
	public class AzureSpeechServicesToken : MonoBehaviour
	{
		public static readonly string FetchTokenUri = "https://westeurope.api.cognitive.microsoft.com/sts/v1.0/issuetoken";
		private string subscriptionKey = "2f9e610344d9487fa3c18575739cb9bf";
		private string currentToken;
		private Timer accessTokenRenewer;
		private const int RefreshTokenDuration = 9;
		private static AzureSpeechServicesToken _instance;

		void Awake() {
			//initialising as singleton
			if (_instance != null && _instance != this)
			{
				Destroy(this.gameObject);
			} else {
				_instance = this;
			}

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

		public static AzureSpeechServicesToken Instance {
			get  { return _instance; }
		}
	}
}

