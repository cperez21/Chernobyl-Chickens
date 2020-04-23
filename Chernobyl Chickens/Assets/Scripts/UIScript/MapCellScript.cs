using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MapCellScript : MonoBehaviour
{
    public string sceneToLoad;
    public Map mapInfo;
    public Image icon;
    public Text text;

    void Start()
    {
        icon.sprite = mapInfo.mapIcon;
        sceneToLoad = mapInfo.sceneToLoad;
        text.text = mapInfo.mapName;
    }

    public void LoadScene()
    {
        Debug.Log("click");
        //SceneManager.LoadScene(sceneToLoad, LoadSceneMode.Single);
    }

}
