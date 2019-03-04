using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SampleListButton : MonoBehaviour
{
    public Button button;
    public Text nameLabel;

    private MarkerScrollList scrollList;
    private ListOfMarkers list;
    private GameObject UIManagerObject;

    // Start is called before the first frame update
    void Start()
    {
        button.onClick.AddListener (HandleClick);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Setup(ListOfMarkers newList, MarkerScrollList currentScrollList, GameObject UIManager)
    {
        list = newList;
        nameLabel.text = list.listName;
        scrollList = currentScrollList;
        UIManagerObject = UIManager;
    }

    public void HandleClick()
    {
        UIManagerObject.GetComponent<UIManager>().ListButtonClicked();
    }

}
