﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class CharacterSelectorScript : MonoBehaviour
{
    public List<Character> characters = new List<Character>();
    //public GameObject charCellPrefab;
    public Character CurrentCharacter;
    public int CharacterSelectNum;
    //public bool Active;
    public GameObject CharacterCell;
    public GameObject Player;
    public GameObject Portrait;
    public int CharacterCount;
    public int CharacterVal;
    void Start()
    {
        foreach(Character character in characters)
        {
            CharacterCount += 1;
            //CharacterToggle(character);
            //Debug.Log(character);
           // Active = false;
        }
        CurrentCharacter = characters[0];
        CharacterVal = 0;
        
    }

    void Update()
    {
        Player = GameObject.Find("Player" + CharacterSelectNum);
        if (Player !=null)
        {
            CharacterCell.SetActive(true);
        }
        Image icon = Portrait.transform.GetComponent<Image>();
        icon.sprite = CurrentCharacter.characterSprite;
    }

    private void CharacterToggleLeft()
    {
        Debug.Log("Left");
        if (CharacterVal == 0)
        {
            CharacterVal = 4;
            CurrentCharacter = characters[CharacterVal];
        }
        else
        {
            CharacterVal -= 1;
            CurrentCharacter = characters[CharacterVal];
        }

    }
    private void CharacterToggleRight()
    {
        Debug.Log("Right");
        if (CharacterVal == 4)
        {
            CharacterVal = 0;
            CurrentCharacter = characters[CharacterVal];
        }
        else
        {
            CharacterVal += 1;
            CurrentCharacter = characters[CharacterVal];
        }
    }

    void SelectChar()
    {
        Player.SendMessage("SetCharacter", CurrentCharacter);
        Debug.Log(CurrentCharacter);
    }
}
