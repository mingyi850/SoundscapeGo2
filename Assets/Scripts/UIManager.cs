using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
	public RectTransform sideMenu;

	public void ChangeToHomeScene() {
		SceneManager.LoadScene ("Destination Select");
	}

	public void SideMenuSlideOut() {
		sideMenu.DOAnchorPos(new Vector2(-200, 0), 0.25f);
	}
}
