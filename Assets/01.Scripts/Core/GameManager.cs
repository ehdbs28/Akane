using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    [SerializeField] private List<Poolable> _poolList;

    private void Awake() {
        foreach(Poolable poolable in _poolList){
            PoolManager.Instance.CreatePool(poolable, transform);
        }

        MaterialManager.Instance = new GameObject(nameof(MaterialManager)).AddComponent<MaterialManager>();
        MaterialManager.Instance.transform.SetParent(transform.parent);

        UIManager.Instance = transform.parent.Find(nameof(UIManager)).GetComponent<UIManager>();
        
        CameraManager.Instance = new GameObject(nameof(CameraManager)).AddComponent<CameraManager>();
        CameraManager.Instance.transform.SetParent(transform.parent);
    }
}
