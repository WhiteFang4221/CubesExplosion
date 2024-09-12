using System.Collections.Generic;
using UnityEngine;

public abstract class Pool<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] private T _prefab;
    [SerializeField] private int _poolCapacity = 1;
    [SerializeField] private Transform _parentTransform;
    
    private Queue<T> _pool = new Queue<T>();
    public int ActiveCount { get; private set; } = 0;
    public int InstantiatedCount { get; private set; } = 0;

    public T Get(Transform transform)
    {
        if (_pool.Count == 0)
        {
            ExpandPool();
        }

        T entity = _pool.Dequeue();
        entity.gameObject.SetActive(true);
        ActiveCount++;
        entity.transform.position = transform.position;

        return entity;
    }

    public void Return(T entity)
    {
        entity.gameObject.SetActive(false);
        ActiveCount--;
        _pool.Enqueue(entity);
    }

    private void ExpandPool()
    {
        T entity = Instantiate(_prefab, _parentTransform);
        _pool.Enqueue(entity);
        InstantiatedCount++;
    }
}
