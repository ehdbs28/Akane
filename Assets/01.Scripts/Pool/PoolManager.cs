using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoSingleton<PoolManager>
{
    private Dictionary<string, Pool<Poolable>> _pools = new Dictionary<string, Pool<Poolable>>();

    private Transform _trmParent;

    public void CreatePool(Poolable prefab, Transform parent, int cnt = 10)
    {
        _trmParent = parent;
        Pool<Poolable> pool = new Pool<Poolable>(prefab, _trmParent, cnt);
        _pools.Add(prefab.gameObject.name, pool);
    }

    public Poolable Pop(string prefabName)
    {
        if (_pools.ContainsKey(prefabName) == false)
        {
            Debug.LogError("Prefab doesnt exist on poolList");
            return null;
        }

        Poolable item = _pools[prefabName].Pop();
        item.Reset();
        return item;
    }

    public void Push(Poolable obj)
    {
        _pools[obj.name].Push(obj);
    }
}
