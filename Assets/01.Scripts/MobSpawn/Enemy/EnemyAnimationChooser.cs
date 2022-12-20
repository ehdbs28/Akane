using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class EnemyAnimationChooser : MonoBehaviour
{
    public EnemyBase enemy;
    
    public RuntimeAnimatorController[] anim;
    public Sprite[] sprites;
    SpriteRenderer sprite;
    Animator animator;
    private void Awake(){
        enemy = GetComponentInParent<EnemyBase>();
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    public void Chooser(){
        if(enemy.enemy.isFar){
            sprite.sprite = sprites[1];
            animator.runtimeAnimatorController = anim[1];
        }
        else{
            sprite.sprite = sprites[0];
            animator.runtimeAnimatorController = anim[0];
        }
    }
}
