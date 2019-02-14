using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
	public RectTransform sideMenu, sideMenuButton;
	public Button homeButton;
	public GameObject homeButtonObject;
	public Transform sidePanel, topPanel;
	private int menuState = 0;

	public void ChangeToHomeScene() {
		SceneManager.LoadScene ("Destination Select");
	}

	public void menuButtonClicked() {
		if (menuState == 0)
			SideMenuSlideOut();
		else if (menuState == 1)
			HideSideMenu();
	}

	public void SideMenuSlideOut() {
		sideMenu.DOAnchorPos(new Vector2(0, 0), 0.25f);
		sideMenuButton.DOAnchorPos(new Vector2(0, 0), 0.25f);
		homeButtonObject.transform.SetParent(sidePanel);
		homeButton.gameObject.SetActive(false);
		menuState = 1;
	}

	public void HideSideMenu() {
		sideMenu.DOAnchorPos(new Vector2(-800, 0), 0.25f);
		homeButtonObject.transform.SetParent(topPanel);
		sideMenuButton.DOAnchorPos(new Vector2(-637.05f, 0), 0.25f);
		homeButton.gameObject.SetActive(true);
		menuState = 0;
	}
}
