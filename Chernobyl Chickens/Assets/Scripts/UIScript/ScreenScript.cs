using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ScreenScript : MonoBehaviour
{
    public List<Map> maps = new List<Map>();

    public Map currentMap;
    public string currentMapName;
    public Image currentMapImage;
    
    GameObject curSel;
    public string curSelName;
    public string selMapName;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        curSel = EventSystem.current.currentSelectedGameObject;
        selMapName = curSel.name;
        if (currentMapName != selMapName)
        {
            setScreenScene();
        }
        
    }

    void setScreenScene()
    {

        for (int i = 0; i < maps.Count; i++)
        {
            if (maps[i].name == selMapName)
            {
                Debug.Log("newscreeenn");
                currentMap = maps[i];

                break;
            }
        }

        currentMapName = currentMap.mapRef;
        currentMapImage.sprite = currentMap.mapIcon;
    }
}
