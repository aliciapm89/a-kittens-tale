using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour
{
    GameManager manager;
    MeshRenderer meshRenderer;
    Rigidbody rb;

    public Transform target;

    void Start()
    {
        manager = GameObject.Find("Manager").GetComponent<GameManager>();
        meshRenderer = GetComponent<MeshRenderer>();
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        rb.transform.LookAt(target);
    }

    public void ActiveValue(bool value)
    {
        meshRenderer.enabled = value;
    }

    public void SetTarget(Transform targ)
    {
        target.position = targ.position - new Vector3(0, 0, - 1);
    }
}
