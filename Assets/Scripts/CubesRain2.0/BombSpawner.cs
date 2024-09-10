using UnityEngine;

public class BombSpawner : Spawner<Bomb>
{
    public void SpawnBombAtPosition(Vector3 position)
    {
        Bomb createdObject = _pool.ReturnObjectPool().Get();
        createdObject.transform.position = position;
    }
}
