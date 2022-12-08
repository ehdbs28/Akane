using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleAction : AIAction
{
    [SerializeField] private float _moveSpeed;

    private Rigidbody2D _rigid;
    private SpriteRenderer _spriteRenderer;

    private Vector2 _moveDir;

    private void Start() {
        _rigid = _brain.GetComponent<Rigidbody2D>();
        _spriteRenderer = _brain.GetComponent<SpriteRenderer>();
    }

    public override void TakeAction()
    {
        _moveDir = PlayerController.Instance.transform.position - transform.position;
        _rigid.velocity = _moveDir.normalized * _moveSpeed;
        _spriteRenderer.flipX = _moveDir.x < 0;
    }
}