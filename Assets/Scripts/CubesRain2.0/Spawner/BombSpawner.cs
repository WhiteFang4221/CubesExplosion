using System;
using UnityEngine;

public class BombSpawner : Spawner<Bomb>
{
    public event Action BombSpawned;
    public void SpawnBombAtPosition(Transform transform)
    {
        Bomb createdObject = ObjectPool.Get(transform);
        BombSpawned?.Invoke();
    }
}
