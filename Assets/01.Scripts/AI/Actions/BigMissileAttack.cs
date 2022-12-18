using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigMissileAttack : AIAction
{
    [ColorUsage(true, true)][SerializeField] private Color _bulletColor;
    private float _waitTime = 0;

    public override void Reset()
    {
        IsPlayAction = false;
        _brain.Rigid.velocity = Vector2.zero;
        _waitTime = 1f;

        _brain.Animator.SetBool("IsSkill", true);
        _brain.Animator.SetInteger("Pattern", 1);
        _brain.Animator.SetFloat("WaitTime", _waitTime);
    }

    public override void TakeAction()
    {
        if(IsPlayAction) return;
        
        Vector3 originPos = _brain.transform.position;
        Vector3 targetDir = _brain.Player.position - originPos; 

        if(_waitTime >= 0){
            _waitTime -= Time.deltaTime;
            _brain.Animator.SetFloat("WaitTime", _waitTime);
        }
        else{
            BigBullet bullet = PoolManager.Instance.Pop("BigBullet") as BigBullet;
            bullet.transform.position = originPos;
            bullet.SetBulletColor(_bulletColor);

            bullet.Bounce();

            IsPlayAction = true;
        }
    }
}
