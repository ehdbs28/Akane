using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour, IDamageable
{
    [SerializeField] private float _maxHP;
    private float _currentHP;

    public bool IsStun {get; set;}
    public bool IsDie {get; set;}

    private void Awake() {
        _currentHP = _maxHP;
    }

    public void OnDamage(float damage)
    {
        _currentHP -= damage;
        if(_currentHP <= 0){
            OnDie();
        }
    }

    public void OnDie(){
        IsDie = true;
        Debug.Log("죽음");
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Bullet")){
            BigBullet bigBullet = other.GetComponent<BigBullet>();
            
            if(bigBullet != null){
                if(bigBullet.Turn == BounceTurn.Boss){
                    bigBullet.Bounce();
                }
            }
            
        }
    }
}
