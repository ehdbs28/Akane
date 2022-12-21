using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolingParticle : Poolable
{
    protected ParticleSystem _particleSystem;

    protected virtual void Awake(){
        _particleSystem = GetComponent<ParticleSystem>();
    }

    ///<summary>
    /// Set position this object
    ///</summary>
    public void SetPosition(Vector2 pos){
        transform.position = new Vector3(pos.x, pos.y, 0);
    }

    ///<summary>
    /// Set rotation this object
    ///</summary>
    public void SetRotation(Vector3 rot){
        transform.localRotation = Quaternion.Euler(rot);
    }

    public virtual void Play(){
        _particleSystem.Play();
    }

    public virtual void Play(float waitTime){
        _particleSystem.Play();
        Invoke("SetDisable", waitTime);
    }

    ///<summary>
    ///push to pull
    ///</summary>
    public void SetDisable(){
        _particleSystem.Stop();
        PoolManager.Instance.Push(this);
    }

    protected virtual void OnParticleSystemStopped() {
        SetDisable();
    }

    public override void Reset()
    {

    }
}
