using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonScript : MonoBehaviour
{

    public string ButtonAction;
    public string SceneToLoad;
    public GameObject CanvasActive;
    public GameObject CanvasDeactive;
    public GameObject highlight;
    //public bool toggleHL;
    //public string Function;
    PersistentGameManagerScript GameManager;
    //Button thisButton;
    void Start()
    {
        GameManager = GameObject.FindWithTag("GameManager").GetComponent<PersistentGameManagerScript>();
        //thisButton = GetComponent<Button>();

        //thisButton.onClick.AddListener(GameManager.);
    }

    //public void HLToggle()
    //{
    //    Debug.Log("Toggle");
    //    toggleHL = !toggleHL;
    //    highlight.SetActive(toggleHL);
    //}


    public void ButtonLoad()
    {
        GameManager.SendMessage(ButtonAction, SceneToLoad);
    }

    public void CanvasToggle()
    {
        CanvasActive.SetActive(true);
        CanvasDeactive.SetActive(true);
    }

    //TEST
    //public void ButtonClick()
    //{
    //    Debug.Log("Clicked");
    //}
}
