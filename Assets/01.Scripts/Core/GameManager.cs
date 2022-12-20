using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    [SerializeField] private List<Poolable> _poolList;

    public bool IsGameStop {get => IsGameStop; set => GameStop(value); }

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
    }

    private void GameStop(bool gameStop){
        Time.timeScale = (gameStop) ? 0f : 1f;
    }
}
