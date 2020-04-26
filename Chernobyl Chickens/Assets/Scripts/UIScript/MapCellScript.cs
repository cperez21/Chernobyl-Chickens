using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MapCellScript : MonoBehaviour
{
    //sets gamemanager and script
    public GameObject GameManager;
    PersistentGameManagerScript GameManagerScript;


    public string sceneToLoad;
    public Map mapInfo;
    public Image icon;
    public Text text;



    void Start()
    {
        GameManager = GameObject.FindWithTag("GameManager");
        GameManagerScript = GameManager.GetComponent<PersistentGameManagerScript>();
        icon.sprite = mapInfo.mapIcon;
        sceneToLoad = mapInfo.sceneToLoad;
        text.text = mapInfo.mapName;
    }

    public void LoadScene()
    {
        GameManagerScript.SendMessage("ChangeScene", sceneToLoad);
    }

}
