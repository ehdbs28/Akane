using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastingEffect : PoolingParticle
{
    [SerializeField] private float _stopMoveTime = 0.25f;

    private WaitForSeconds ws_Drop;
    private WaitForSeconds ws_Disable;

    private Coroutine co;

    protected override void Awake()
    {
        ws_Drop = new WaitForSeconds(_stopMoveTime);
        base.Awake();
    }

    public override void Play()
    {
        var main = _particleSystem.main;
        main.simulationSpeed = 1f;

        ws_Disable = new WaitForSeconds(main.startLifetime.constantMax);

        var renderer = _particleSystem.GetComponent<ParticleSystemRenderer>();

        _particleSystem.Play();

        if(co != null){
            StopCoroutine(co);
        }

        co = StartCoroutine(DisableTimer());
    }

    private IEnumerator DisableTimer(){
        yield return ws_Drop;

        var main = _particleSystem.main;
        main.simulationSpeed = 0f;

        var renderer = _particleSystem.GetComponent<ParticleSystemRenderer>();

        yield return ws_Disable;

        OnParticleSystemStopped();
    }
}
