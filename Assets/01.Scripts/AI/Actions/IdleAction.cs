using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleAction : AIAction
{
    [SerializeField] private float _moveSpeed;

    private SpriteRenderer _spriteRenderer;

    private Vector2 _moveDir;

    private void Start() {
        _spriteRenderer = _brain.GetComponent<SpriteRenderer>();
    }

    public override void TakeAction()
    {
        _moveDir = PlayerController.Instance.transform.position - transform.position;
        _brain.Rigid.velocity = _moveDir.normalized * _moveSpeed;
        _spriteRenderer.flipX = _moveDir.x < 0;
    }
}