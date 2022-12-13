using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : Poolable
{
    protected ParticleSystem _particleSystem;

    protected virtual void Awake(){
        _particleSystem = GetComponent<ParticleSystem>();
    }

    public void SetPosition(Vector2 pos){
        transform.position = new Vector3(pos.x, pos.y, 0);
    }

    public void SetRotation(Vector3 rot){
        transform.localRotation = Quaternion.Euler(rot);
    }

    public virtual void Play(){
        _particleSystem.Play();
    }

    public void SetDisable(){
        PoolManager.Instance.Push(this);
    }

    protected virtual void OnParticleSystemStopped() {
        SetDisable();
    }

    public override void Reset()
    {
        
    }
}
