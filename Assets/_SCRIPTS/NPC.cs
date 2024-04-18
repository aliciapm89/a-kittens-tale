using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    GameManager manager;

    PlayerController player;

    SpriteRenderer SpriteRend;

    [Header("NPC Character")]
    public bool Mom;
    public bool Child;
    public bool Emo;

    void Start()
    {
        manager = GameObject.Find("Manager").GetComponent<GameManager>();
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        SpriteRend = GetComponent<SpriteRenderer>();

        if (Child)
        {
            SpriteRend.enabled = false;
        }
    }

    void Update()
    {
        
    }

    //Se llama desde PlayerController en la funcion Interact
    public void ConvoStart()
    {

        print("true");
        if (Mom)
        {
            if (!manager.motherMissionStarted)
            {
                manager.motherMissionStarted = true;
                manager.ChildNPC.GetComponent<SpriteRenderer>().enabled = true;
                manager.StartConvo(manager.MotherConvoStart);
                player.isMom1 = true;
                player.SelectionStatus(true);
            }
            else if (!manager.ChildFound)
            {
                manager.StartConvo(manager.MotherActiveConvo);
            }
            else if (manager.ChildFound && !manager.motherMissionCompleted)
            {
                manager.motherMissionCompleted = true;
                manager.StartConvo(manager.MotherConvoEnd);
            }
        }

        if (Child)
        {
            if (manager.motherMissionStarted && !manager.childFound)
            {
                manager.childFound = true;
                manager.StartConvo(manager.ChildFound);
            }
        }

        if (Emo)
        {
            if (!manager.emoMissionStarted)
            {
                manager.emoMissionStarted = true;
                manager.StartConvo(manager.EmoConvoStart);
                player.isEmo1 = true;
                player.SelectionStatus(true);
            }
            else if (!manager.fruitFound)
            {
                manager.StartConvo(manager.EmoConvoActive);
            }
            else if (!manager.emoMissionCompleted)
            {
                manager.emoMissionCompleted = true;
                manager.FruitSelectorUI.SetActive(true);
                manager.SelectFruit();
                player.SelectionStatus(true);
            }
        }
    }
}
