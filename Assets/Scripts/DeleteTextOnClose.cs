using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteTextOnClose : MonoBehaviour
{

	public void deleteAllPoiText()
	{
		poiPanel[] poiPanelsList = gameObject.GetComponentsInChildren<poiPanel>();
		foreach (poiPanel panel in poiPanelsList)
		{
			panel.DeleteAllContent();
		}
	}
}
