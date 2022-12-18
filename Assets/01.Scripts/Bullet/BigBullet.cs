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

    private int[] _bounceCnt = {4, 6, 8};

    public bool IsBulletDisable {get; private set;} = false;

    protected override void Awake()
    {
        base.Awake();
        _player = GameObject.Find("Player").transform;
        _boss = GameObject.Find("Boss").transform;
    }

    private void OnEnable() {
        _bounceCount = _bounceCnt[Random.Range(0, _bounceCnt.Length)];
        _turn = BounceTurn.Boss;
        transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
        IsBulletDisable = false;
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
            IsBulletDisable = true;
            if(_turn == BounceTurn.Player){ //hit Player

            }
            else{ //hit Boss
                Boss boss = _boss.GetComponent<Boss>();
                boss.IsStun = true;
            }
            DestroyBullet();
        }
    }

    private void TurnChange(){
        transform.localScale = transform.localScale - Vector3.one * 0.03f;

        if(_turn == BounceTurn.Player){
            _turn = BounceTurn.Boss;
        }
        else{
            _turn = BounceTurn.Player;
        }
    }
}
