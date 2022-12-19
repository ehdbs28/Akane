using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoSingleton<PlayerController>
{
    [SerializeField] private float _speed = 5;
    [SerializeField] private Transform _weaponTrm;

    private PlayerAttack playerAttack;
    private Animator animator;
    private Rigidbody2D rb;
    private Vector2 moveDir = Vector2.zero;

    private bool _isDying;
    public bool IsDying
    {
        get => _isDying;
        set => _isDying = value;
    }

    public float hp = 100f;

    private void Awake()
    {
        animator = transform.Find("PlayerSprite").GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        playerAttack = GetComponent<PlayerAttack>();
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
        // if (!playerAttack.IsAttack)
        // {
        //     if (h < 0)
        //     {
        //         _weaponTrm.position = new Vector3(transform.position.x + 0.7f, transform.position.y + 0.5f, 0);
        //     }
        //     else if (h > 0)
        //     {
        //         _weaponTrm.position = new Vector3(transform.position.x - 0.7f, transform.position.y + 0.5f, 0);
        //     }
        //     else
        //     {
        //         return;
        //     }
        // }
        // else
        // {
        //     return;
        // }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        //공격 튕기는거 테스트 용임
        if(other.CompareTag("Bullet")){
            BigBullet bigBullet = other.GetComponent<BigBullet>();
            
            if(bigBullet != null){
                if(bigBullet.Turn == BounceTurn.Player){
                    bigBullet.Bounce();
                }
            }
            
        }
    }

    public void Slow(){
        StartCoroutine(Slowly());
    }

    IEnumerator Slowly(){
        _speed = 3;
        yield return new WaitForSeconds(1f);
        _speed = 5;
    }
}
