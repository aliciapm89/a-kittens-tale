using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeColor : MonoBehaviour
{
#pragma warning disable 0108

    GameManager manager;

    SpriteRenderer renderer;

    void Start()
    {
        manager = GameObject.Find("Manager").GetComponent<GameManager>();

        renderer = GetComponent<SpriteRenderer>();

        int rand = UnityEngine.Random.Range(0, 5);

        if (rand == 0 || rand == 1)
        {
            renderer.sprite = manager.GreenTree;
        }
        if (rand == 2 || rand == 3)
        {
            renderer.sprite = manager.DarkTree;
        }
        if (rand == 4)
        {
            renderer.sprite = manager.BrownTree;
        }
    }

    void Update()
    {
        
    }
}
