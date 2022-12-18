using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : IDamageable
{
    [SerializeField] private float _maxHP;
    private float _currentHP;

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

    public void OnDie()
    {
        IsDie = true;
        Debug.Log("죽음");
    }
}
