using UnityEngine;

[RequireComponent (typeof(IView))]
public abstract class Spawner<T> : MonoBehaviour where T: MonoBehaviour, ISpawnable
{
    [SerializeField] protected Pool<T> ObjectPool;
    [SerializeField] protected IView ViewCount;

    protected int SpawnedCount = 0;

    protected void DestroyObject(ISpawnable spawnableObject)
    {
        ObjectPool.Return((T)spawnableObject);
        ViewCount.UpdateActive(ObjectPool.ActiveCount);
        spawnableObject.Destroyed -= DestroyObject;
    }

    protected void InitializeView()
    {
        ViewCount = GetComponent<IView>();
    }
}