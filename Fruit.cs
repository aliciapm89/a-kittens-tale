using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour
{
    GameManager manager;

    PlayerController player;

    [Header("Fruit Type")]
    public bool Apple;
    public bool Orange;
    public bool Pear;

    ItemPickup ItemPickup;

    Vector3 startPos, endPos;

    bool falling = false;
    bool hasFallen = false;

    float t;
    float startTime, endTime;

    void Start()
    {
        manager = GameObject.Find("Manager").GetComponent<GameManager>();
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        ItemPickup = GetComponent<ItemPickup>();
    }

    void Update()
    {
        if (falling && !hasFallen)
        {
            t += Time.deltaTime / .5f;
            transform.position = Vector3.Lerp(startPos, endPos, t);
        }

        if (t > 1)
        {
            falling = false;
            hasFallen = true;
        }
    }

    public void FruitFall()
    {
        falling = true;
        startTime = Time.time;
        endTime = Time.time + 1;
        startPos = transform.position;
        endPos = transform.position - new Vector3(0, 4.26f, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && hasFallen)
        {
            if (Apple)
            {
                manager.hasApple = true;
                player.isEmo1 = false;
                player.isEmo2 = true;
            }
            if (Pear)
            {
                manager.hasPear = true;
                player.isEmo1 = false;
                player.isEmo2 = true;
            }
            if (Orange)
            {
                manager.hasOrange = true;
                player.isEmo1 = false;
                player.isEmo2 = true;
            }

            manager.fruitFound = true;
            ItemPickup.Pickup();
        }
    }
}
