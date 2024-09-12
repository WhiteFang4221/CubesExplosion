using UnityEngine;

public class BombSpawner : Spawner<Bomb>
{
    private void Awake()
    {
        InitializeView();
    }

    public void SpawnBombAtPosition(Transform transform)
    {
        Bomb createdObject = (Bomb)ObjectPool.Get(transform);
        createdObject.Destroyed += DestroyObject;
        ViewCount.UpdateSpawned(++SpawnedCount);
        ViewCount.UpdateInstantiated(ObjectPool.InstantiatedCount);
        ViewCount.UpdateActive(ObjectPool.ActiveCount);
    }
}
