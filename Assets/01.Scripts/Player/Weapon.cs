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

    private Animator _anim;

    [field:SerializeField]public bool IsAttack {get; set;}

    private void Awake()
    {
        parentPos = new Queue<Vector3>();
        _anim = GetComponent<Animator>();
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
        if(!IsAttack){
            float h = Mathf.Lerp(transform.position.x, followPos.x, 0.5f);
            float v = Mathf.Lerp(transform.position.y, followPos.y, 0.5f);

            Vector2 moveDir;
            moveDir = new Vector2(h, v);
            transform.position = moveDir;
            transform.rotation = Quaternion.AngleAxis(0f, Vector3.forward);
        }
    }

    public void Attack(Vector2 dir){
        float rotation = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.AngleAxis(rotation - 180f, Vector3.forward);
        transform.position = dir.normalized;
        Debug.Log(transform.position);
        _anim.Play("WeaponSlash");
    }
}
