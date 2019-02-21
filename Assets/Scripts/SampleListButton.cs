using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SampleListButton : MonoBehaviour
{
    public Button button;
    public Text nameLabel;
    private MarkerScrollList scrollList;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Setup(ListOfMarkers newList, MarkerScrollList currentScrollList)
    {
        nameLabel.text = newList.listName;
        scrollList = currentScrollList;
    }
}
