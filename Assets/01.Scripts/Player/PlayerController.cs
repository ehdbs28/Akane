using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _speed = 5;
    [SerializeField] private Transform _weaponTrm;

    private Animator animator;
    private Rigidbody2D rb;
    private Vector2 moveDir = Vector2.zero;

    private void Awake()
    {
        animator = transform.Find("PlayerSprite").GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Movement();
    }

    private void Movement()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        moveDir = new Vector2(h, v);

        //animator value set
        animator.SetBool("IsMove", moveDir.magnitude > 0);
        animator.SetFloat("X_Input", moveDir.x);
        animator.SetFloat("Y_Input", moveDir.y);

        rb.velocity = moveDir.normalized * _speed;
        if (h < 0)
        {
            _weaponTrm.position = new Vector3(transform.position.x+0.7f, transform.position.y+0.5f, 0);
        }
        else if(h > 0)
        {
            _weaponTrm.position = new Vector3(transform.position.x-0.7f, transform.position.y+0.5f, 0);
        }
        else
        {
            return;
        }
        
    }
}