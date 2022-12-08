using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Vector3 followPos;
    public int followDelay;
    public Transform parent;
    public Queue<Vector3> parentPos;
    private PlayerAttack _attackScript;
    private void Awake()
    {
        parentPos = new Queue<Vector3>();
    }

    private void Update()
    {
        Watch();
        Movement();
    }

    private void Watch()
    {
        parentPos.Enqueue(parent.position);

        if(parentPos.Count > followDelay)
            followPos = parentPos.Dequeue();
    }

    void Movement()
    {
        float h = Mathf.Lerp(transform.position.x, followPos.x, 0.5f);
        float v = Mathf.Lerp(transform.position.y, followPos.y, 0.5f);

        Vector2 moveDir;
        moveDir = new Vector2(h, v);
        transform.position = moveDir;
    }
}
