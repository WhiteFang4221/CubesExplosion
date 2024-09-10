using UnityEngine;
using UnityEngine.Pool;

public abstract class Pool<T> : MonoBehaviour where T : MonoBehaviour, IInitializable
{
    [SerializeField] private T _prefab;
    [SerializeField] private Transform _parentTransform;
    [SerializeField] private int _poolCapacity = 1;
    [SerializeField] private int _poolMaxSize = 10;

    private ObjectPool<T> _pool;

    private void Awake()
    {
        _pool = new ObjectPool<T>(
            createFunc: CreateCube,
            actionOnGet: PullOut,
            actionOnRelease: (createdObject) => createdObject.gameObject.SetActive(false),
            actionOnDestroy: (createdObject) => Object.Destroy(createdObject),
            collectionCheck: true,
            defaultCapacity: _poolCapacity,
            maxSize: _poolMaxSize);
    }

    public ObjectPool<T> ReturnObjectPool()
    {
        return _pool;
    }

    private T CreateCube()
    {
        T createdObject = Instantiate(_prefab, _parentTransform);
        createdObject.Initialize(this);

        return createdObject;
    }

    private void PullOut(T obj)
    {
        obj.gameObject.SetActive(true);
    }
}
