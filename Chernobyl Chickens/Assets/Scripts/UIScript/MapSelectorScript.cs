using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MapSelectorScript : MonoBehaviour
{
    public List<Map> maps = new List<Map>();
    public GameObject mapCellPrefab;


    void Start()
    {

        foreach (Map map in maps)
        {
            SpawnMapCell(map);
            
        }

    }



    private void SpawnMapCell(Map map)
    {
        GameObject mapCell = Instantiate(mapCellPrefab, transform);
        mapCell.name = map.mapName;
        Text name = mapCell.transform.GetComponentInChildren<Text>();
        name.text = map.mapName;
        MapCellScript MCScript = mapCell.transform.GetComponent<MapCellScript>();
        MCScript.sceneToLoad = map.sceneToLoad;

        Image icon = mapCell.transform.Find("Icon").GetComponent<Image>();
        icon.sprite = map.mapSprite;
    }


}
