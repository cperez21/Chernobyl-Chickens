using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class CharacterSelectorScript : MonoBehaviour
{
    public List<Character> characters = new List<Character>();
    public GameObject charCellPrefab;
    
    void Start()
    {
        foreach(Character character in characters)
        {
            SpawnCharacterCell(character);
            Debug.Log(character);
        }
    }

    private void SpawnCharacterCell(Character character)
    {
        GameObject charCell = Instantiate(charCellPrefab, transform);
        charCell.name = character.characterName;
        Text name = charCell.transform.GetComponentInChildren<Text>();
        Image icon = charCell.transform.GetComponent<Image>();

        icon.sprite = character.characterSprite;
        name.text = character.characterName;
    }
    
}
