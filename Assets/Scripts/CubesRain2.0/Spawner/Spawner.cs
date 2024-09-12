using System;
using UnityEngine;

[RequireComponent (typeof(IView))]
public abstract class Spawner<T> : MonoBehaviour where T: PoolableObject<T>
{
    [SerializeField] private Pool<T> _objectPool;

    private ulong _spawnedCount = 0;
    private ulong _activeCount = 0;

    public event Action<ulong> SpawnUpdated;
    public event Action<ulong> InstantiateUpdated;
    public event Action<ulong> ActiveUpdated;

    public virtual void SpawnObject(Vector3 vector)
    {
        T createdObject = _objectPool.Get(vector);
        _spawnedCount++;
        _activeCount++;
        createdObject.Disabled += DestroyObject;
        SpawnUpdated?.Invoke(_spawnedCount);
        InstantiateUpdated?.Invoke(_objectPool.InstantiatedCount);
        ActiveUpdated?.Invoke(_activeCount);
    }

    private void DestroyObject(T spawnableObject)
    {
        _objectPool.Return((T)spawnableObject);
        spawnableObject.Disabled -= DestroyObject;
        OnObjectRelease(spawnableObject);
        _activeCount--;
        ActiveUpdated?.Invoke(_activeCount);
    }

    protected virtual void OnObjectRelease(T obj)
    {
    }
}