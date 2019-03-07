using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

namespace ButtonSelect
{
	public class ButtonSelector : MonoBehaviour
	{

		private string lastClickedID = "";
		// Start is called before the first frame update
		void Start()
		{
		
		}

		// Update is called once per frame
		void Update()
		{

		}

		public void setLastClickedId(string buttonID)
		{
			lastClickedID = buttonID;
		}

		public string getLastClicked()
		{
			return lastClickedID;
		}

	}
}
