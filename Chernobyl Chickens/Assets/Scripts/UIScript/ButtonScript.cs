using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonScript : MonoBehaviour
{

    public string ButtonAction;
    public string CurrentScene;
    public string SceneToLoad;
    public GameObject highlight;
    public bool toggleHL;
    //public string Function;
    PersistentGameManagerScript GameManager;
    //Button thisButton;
    void Start()
    {
        GameManager = GameObject.FindWithTag("GameManager").GetComponent<PersistentGameManagerScript>();
        //thisButton = GetComponent<Button>();

        //thisButton.onClick.AddListener(GameManager.);
    }

    public void HLToggle()
    {
        Debug.Log("Toggle");
        toggleHL = !toggleHL;
        highlight.SetActive(toggleHL);
    }


    public void ButtonLoad()
    {

        object[] tempArray = new object[2];
        tempArray[0] = SceneToLoad;
        tempArray[1] = CurrentScene;

        GameManager.SendMessage(ButtonAction, tempArray);
    }

    //TEST
    //public void ButtonClick()
    //{
    //    Debug.Log("Clicked");
    //}
}
