using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BounceTurn{
    Player,
    Boss
}

public class BigBullet : BossBullet
{
    [SerializeField] private int _bounceCount;

    private BounceTurn _turn;
    public BounceTurn Turn => _turn;

    private Transform _player;
    private Transform _boss;

    private void Start() {
        _player = GameObject.Find("Player").transform;
        _boss = GameObject.Find("Boss").transform;

        _bounceCount = Random.Range(3, 6);
        Debug.Log(_bounceCount);
    }

    ///<summary>
    /// 현제 타겟의 방향으로 총알의 이동방향을 틀어줌
    ///</summary>
    public void Bounce(){
        if(_bounceCount != 0){
            _bounceCount--;
            Vector3 dir = Vector3.zero;

            if(_turn == BounceTurn.Player){
                dir = _boss.position - _player.position;
            }
            else{
                dir = _player.position - _boss.position;
            }

            SetVelocity(dir.normalized);
            TurnChange();
        }
        else{
            //보스 스턴 추가 해야댐
            DestroyBullet();
        }
    }

    private void TurnChange(){
        transform.localScale = transform.localScale - Vector3.zero * 0.3f;

        if(_turn == BounceTurn.Player){
            _turn = BounceTurn.Boss;
        }
        else{
            _turn = BounceTurn.Player;
        }
    }
}
