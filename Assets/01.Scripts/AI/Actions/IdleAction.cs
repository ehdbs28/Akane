using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleAction : AIAction
{
    [System.Serializable]
    struct MapData{
        public float MaxPos_X;
        public float MinPos_X;
    }

    [SerializeField] private float _moveSpeed;
    [SerializeField] private MapData _mapData;

    private Rigidbody2D _rigid;
    private Vector2 _moveDir;

    private void Start() {
        _moveDir = Vector2.right;
        _rigid = _brain.GetComponent<Rigidbody2D>();
    }

    public override void TakeAction()
    {
        _rigid.position += _moveDir * _moveSpeed * Time.deltaTime;
        if(_rigid.position.x >= _mapData.MaxPos_X || _rigid.position.x <= _mapData.MinPos_X){
            _moveDir *= -1f;
        }
    }
}
