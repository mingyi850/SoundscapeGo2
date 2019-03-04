using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEditor;

namespace VoiceSearchTermsNS
{
	enum VoiceAction
	{
		AROUND,
		AHEAD,
		MYLOCATION,
		OPTIONS,
		HOME
			
	};

	class VoiceSearchTerms : MonoBehaviour
	{
		
		public Dictionary<string, int> VoiceCommandLookupTable;

		private string[] aroundMeSearchTerms = { "around", "near", "nearby" };
		private string[] aheadOfMeSearchTerms = { "ahead", "front" };
		private string[] myLocationSearchTerms = { "location", "current" };
		private string[] optionsSearchTerms = { "settings", "options", "help" };
		private string[] homeSearchTerms = { "home", "search", "new", "back" };

		void Awake()
		{
			VoiceCommandLookupTable = new Dictionary<string, int>();
			addToDictionary(aroundMeSearchTerms, VoiceAction.AROUND);
			addToDictionary(aheadOfMeSearchTerms, VoiceAction.AHEAD);
			addToDictionary(myLocationSearchTerms, VoiceAction.MYLOCATION);
			addToDictionary(optionsSearchTerms, VoiceAction.OPTIONS);
			addToDictionary(homeSearchTerms, VoiceAction.HOME);

		}

		void addToDictionary(string[] array, VoiceAction action)
		{
			for (int x = 0; x < array.Length; x++)
			{
				VoiceCommandLookupTable.Add(array[x], (int)action);
			}
		}

	}
}

