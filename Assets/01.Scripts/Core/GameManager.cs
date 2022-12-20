using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    [SerializeField] private List<Poolable> _poolList;

    public bool IsGameStop {get => IsGameStop; set => GameStop(value); }

    public AudioSource PlayerSource {get; private set;}
    public AudioSource BossSource {get; private set;}

    public AudioClip BossPhase1BGM {get; private set;}
    public AudioClip BossPhase2BGM {get; private set;}

    private void Awake() {
        foreach(Poolable poolable in _poolList){
            PoolManager.Instance.CreatePool(poolable, transform);
        }

        MaterialManager.Instance = new GameObject(nameof(MaterialManager)).AddComponent<MaterialManager>();
        MaterialManager.Instance.transform.SetParent(transform.parent);

        UIManager.Instance = transform.parent.Find(nameof(UIManager)).GetComponent<UIManager>();
        
        CameraManager.Instance = new GameObject(nameof(CameraManager)).AddComponent<CameraManager>();
        CameraManager.Instance.transform.SetParent(transform.parent);

        TimeScaleManager.Instance = new GameObject(nameof(TimeScaleManager)).AddComponent<TimeScaleManager>();
        TimeScaleManager.Instance.transform.SetParent(transform.parent);

        SoundManager.Instance = transform.parent.Find(nameof(SoundManager)).GetComponent<SoundManager>();

        PlayerSource = GameObject.Find("Player").GetComponent<AudioSource>();
        BossSource = GameObject.Find("Boss").GetComponent<AudioSource>();

        BossPhase1BGM = Resources.Load<AudioClip>("AudioClip/BGM/BossPhase1BGM");
        BossPhase2BGM = Resources.Load<AudioClip>("AudioClip/BGM/BossPhase2BGM");
    }

    private void GameStop(bool gameStop){
        Time.timeScale = (gameStop) ? 0f : 1f;
    }
}
