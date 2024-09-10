using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class Pool<T> : MonoBehaviour where T : MonoBehaviour, IInitializable
{
    [SerializeField] private T _prefab;
    [SerializeField] private int _poolCapacity = 1;
    [SerializeField] private Transform _parentTransform;

    public event Action Created;
    public event Action Activated;
    public event Action Disabled;

    private Queue<T> _pool = new Queue<T>();

    private void Awake()
    {
        for (int i = 0; i < _poolCapacity; i++)
        {
            ExpandPool();
        }

        foreach (T entity in _pool)
        {
            entity.gameObject.SetActive(false);
            entity.Initialize(this);
        }
    }

    public T Get(Transform transform)
    {
        if (_pool.Count == 0)
        {
            ExpandPool();
        }

        T entity = _pool.Dequeue();
        entity.gameObject.SetActive(true);
        Activated?.Invoke();
        entity.transform.position = transform.position;

        return entity;
    }

    public void Return(T entity)
    {
        entity.gameObject.SetActive(false);
        Disabled?.Invoke();
        _pool.Enqueue(entity);
    }

    private void ExpandPool()
    {
        T entity = Instantiate(_prefab, _parentTransform);
        Created?.Invoke();
        entity.Initialize(this);
        _pool.Enqueue(entity);
    }
}
