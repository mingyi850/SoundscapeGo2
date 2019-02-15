using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
	public RectTransform sideMenu, sideMenuButton, markersPanel;
	public Button homeButton;
	public GameObject menuButtonObject;
	public Transform sidePanel, topPanel, markersPanelTransform;
	private int menuState = 0;

	public void ChangeToHomeScene() {
		SceneManager.LoadScene ("Destination Select");
	}

	public void menuButtonClicked() {
		if (menuState == 0)
			SideMenuSlideOut();
		else if (menuState == 1)
			HideSideMenu();
		else if (menuState == 2)
			HideAllFromMarkersPanel();
	}

	public void SideMenuSlideOut() {
		menuState = 1;
		sideMenu.DOAnchorPos(new Vector2(0, 0), 0.25f);
		markersPanel.DOAnchorPos(new Vector2(-800, 0), 0.25f);
		sideMenuButton.DOAnchorPos(new Vector2(0, 585), 0.25f);
		menuButtonObject.transform.SetParent(sidePanel);
		homeButton.gameObject.SetActive(false);
	}

	public void MarkersPanelSlideOut() {
		menuState = 2;
		markersPanel.DOAnchorPos(Vector2.zero, 0.25f);
		sideMenuButton.DOAnchorPos(new Vector2(0, 585), 0.25f);
		menuButtonObject.transform.SetParent(markersPanelTransform);
	}

	public void HideSideMenu() {
		sideMenu.DOAnchorPos(new Vector2(-800, 0), 0.25f);
		menuButtonObject.transform.SetParent(topPanel);
		sideMenuButton.DOAnchorPos(new Vector2(-637.05f, 0), 0.25f);
		homeButton.gameObject.SetActive(true);
		menuState = 0;
	}

	public void HideMarkersPanel() {
		menuButtonObject.transform.SetParent(sideMenu);
		markersPanel.DOAnchorPos(new Vector2(-800, 0), 0.25f);
		menuState = 1;
	}

	public void HideAllFromMarkersPanel() {
		markersPanel.DOAnchorPos(new Vector2(-1600, 0), 0.25f);
		sideMenu.DOAnchorPos(new Vector2(-800, 0), 0.25f);
		sideMenuButton.DOAnchorPos(new Vector2(-637.05f, 0), 0.25f);
		menuButtonObject.transform.SetParent(topPanel);
		homeButton.gameObject.SetActive(true);
		menuState = 0;
	}
}
