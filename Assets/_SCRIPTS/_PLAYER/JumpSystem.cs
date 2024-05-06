using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//comentareio del 6 de mayo
//me gustan las patatas fritas
public class JumpSystem : MonoBehaviour
{
    PlayerController player;

    private void Awake()
    {
        player = transform.parent.GetComponent<PlayerController>();
    }

    public void JumpEnd()
    {
        player.jumping = false;
    }
}
