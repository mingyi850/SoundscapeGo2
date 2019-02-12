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

namespace BingSearch
{	
	[System.Serializable]
	public class BingSearchHandler: MonoBehaviour
	{
		private static string subscriptionKey = "7297f2ba41ab482e9f588cc620d26635";
		private static string bsHosturi = "https://api.cognitive.microsoft.com/bing/v7.0/search";
		private string url;

		public static RootObject getRootObject(string JSONString) {
			return JsonUtility.FromJson<RootObject> (JSONString);
		}

		[System.Serializable]
		public class QueryContext
		{
			public string originalQuery;
		}

		[System.Serializable]
		public class About
		{
			public string name;
		}

		[System.Serializable]
		public class License
		{
			public string name;
			public string url;
		}

		[System.Serializable]
		public class SnippetAttribution
		{
			public License license;
			public string licenseNotice;
		}

		[System.Serializable]
		public class Value
		{
			public string id;
			public string name;
			public string url;
			public List<About> about;
			public bool isFamilyFriendly;
			public string displayUrl;
			public string snippet;
			public SnippetAttribution snippetAttribution;
			public DateTime dateLastCrawled;
			public string language;
			public bool isNavigational;
		}

		[System.Serializable]
		public class WebPages
		{
			public string webSearchUrl;
			public int totalEstimatedMatches;
			public List<Value> value;
		}

		[System.Serializable]
		public class Value2
		{
			public string id;
		}

		[System.Serializable]
		public class Item
		{
			public string answerType;
			public int resultIndex;
			public Value2 value;
		}

		[System.Serializable]
		public class Mainline
		{
			public List<Item> items;
		}

		[System.Serializable]
		public class RankingResponse
		{
			public Mainline mainline;
		}

		[System.Serializable]
		public class RootObject
		{
			public string _type;
			public QueryContext queryContext;
			public WebPages webPages;
			public RankingResponse rankingResponse;
		}
	

		void Awake()
		{

		}

		public string getLinkResult(string query) 
		{
			int count = 2;
			string returnedUrl;
			DownloadHandlerBuffer downloader = new DownloadHandlerBuffer ();
			string uriQuery = bsHosturi + "?q=" + Uri.EscapeDataString (query) + "&answerCount=2" + "&count=2" + "&responseFilter=webpages";
			Debug.Log (uriQuery);
			using (var request = new UnityWebRequest())
			{
				request.downloadHandler = downloader;
				request.url = uriQuery;
				request.method = UnityWebRequest.kHttpVerbGET;
				// Set the content type header
						
				request.SetRequestHeader("Ocp-Apim-Subscription-Key", subscriptionKey);
				request.SetRequestHeader ("Accept", "application/json");
				Debug.Log ("Sending Web Search Request");
				request.SendWebRequest ();
				float startTime = Time.time;
				if (request.isNetworkError || request.isHttpError) {
					Debug.Log (request.error);
					Debug.Log (request.downloadedBytes.ToString ());
				} 
				else {
					while (request.isDone == false) {
						if (Time.time - startTime > 10) {
							Debug.Log ("API TIMEOUT");
							break;
						}

					}
					//Debug.Log (request.ToString ());
					if (request.isDone) {
						string jsonData = System.Text.Encoding.UTF8.GetString (request.downloadHandler.data, 0, request.downloadHandler.data.Length);
						Debug.Log (jsonData);
						RootObject rootObject = BingSearchHandler.getRootObject (jsonData);
						returnedUrl = rootObject.webPages.value [0].url;
						Debug.Log (returnedUrl);
						this.url = returnedUrl;
						return returnedUrl;
						//string firstResultUrl = rootObject.Demo; //.webPages.value [0].url;
						//Debug.Log ("First Result" + firstResultUrl);
					}


				}

			}
			Debug.Log ("Done with no URL");
			return "no Url";

		}

		public string getFirstSearchedUrl(string query) {
			StartCoroutine (getLinkResult (query));
			return this.url;
		}


		public void testGetLinkResult() {
			string query = "moose";
			StartCoroutine(getLinkResult (query));

		}

	}
}