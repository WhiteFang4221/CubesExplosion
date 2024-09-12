using UnityEngine;

public class BombSpawner : Spawner<Bomb>
{
    public void SpawnBombAtPosition(Vector3 vector)
    {
        SpawnObject(vector);
    }
}