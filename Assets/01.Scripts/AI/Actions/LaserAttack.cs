using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserAttack : AIAction
{
    [SerializeField] private float _delayTime = 1f;

    private float _currentTime = 0f;

    public override void Reset()
    {
        _brain.Rigid.velocity = Vector2.zero;
        _currentTime = _delayTime;

        _brain.Animator.SetBool("IsSkill", false);
        _brain.Animator.SetBool("IsSkill", true);
        _brain.Animator.SetInteger("Pattern", 1);
    }

    public override void TakeAction()
    {
        _currentTime -= Time.deltaTime;

        if(_currentTime <= 0f){
            Vector3 originPos = _brain.transform.position;
            Vector3 targetPos = _brain.Player.position - originPos;

            
        }
    }
}
