using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCcollider : MonoBehaviour
{
    public bool isNPC = false;

    public bool isFruitTree = false;

    public GameObject interactable;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("NPC"))
        {
            isNPC = true;
            interactable = collision.gameObject;
        }

        if (collision.CompareTag("FruitTree"))
        {
            isFruitTree = true;
            interactable = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("NPC"))
        {
            isNPC = false;
            interactable = null;
        }

        if (collision.CompareTag("FruitTree"))
        {
            isFruitTree = false;
            interactable = null;
        }
    }
}
