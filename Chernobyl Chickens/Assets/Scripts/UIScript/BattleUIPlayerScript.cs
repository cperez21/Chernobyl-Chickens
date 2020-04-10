using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleUIPlayerScript : MonoBehaviour
{
    public int Playernum;
    public Character PlayerCharacterSerializable;
    public GameObject Portrait;
    public GameObject BattleUIPlayerCell;

    //Calls PlayerInput Object, which exists in PersistentScene
    public GameObject Player;

    // Start is called before the first frame update
    void Start()
    {
        BattleUIPlayerCell.name = "PlayerCell" + Playernum;

    }

    // Update is called once per frame
    void Update()
    {
        if (Player == null)
        {
            Player = GameObject.Find("Player" + Playernum);
            if (Player != null)
            {
                Player.SendMessage("SendCharacter");
            }
        }

        if (Player != null)
        {
            BattleUIPlayerCell.SetActive(true);
        }

        Image icon = Portrait.transform.GetComponent<Image>();
        icon.sprite = PlayerCharacterSerializable.characterSprite;
    }

    void SetCharacterUI(Character character)
    {
        PlayerCharacterSerializable = character;

    }
}
