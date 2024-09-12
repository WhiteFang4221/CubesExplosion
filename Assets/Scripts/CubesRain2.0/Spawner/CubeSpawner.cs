using System.Collections;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]

public class CubeSpawner : Spawner<Cube>
{
    [SerializeField] private BombSpawner _bombSpawner;
    private BoxCollider _spawnArea;
    private WaitForSeconds _spawnDelay = new WaitForSeconds(1f);
    private float _dividerSpawnArea = 2f;

    private void Awake()
    {
        _spawnArea = GetComponent<BoxCollider>();
        InitializeView();
    }

    private void Start()
    {
        StartCoroutine(SpawnCubesCoroutine());
    }

    private IEnumerator SpawnCubesCoroutine()
    {
        {
            while (true)
            {
                Cube cube = (Cube)ObjectPool.Get(transform);
                cube.transform.position = GetSpawnArea();
                cube.SetBombSpawner(_bombSpawner);
                cube.Destroyed += DestroyObject;
                ViewCount.UpdateSpawned(++SpawnedCount);
                ViewCount.UpdateInstantiated(ObjectPool.InstantiatedCount);
                ViewCount.UpdateActive(ObjectPool.ActiveCount);
                yield return _spawnDelay;
            }
        }
    }

    private Vector3 GetSpawnArea()
    {
        Vector3 spawnAreaSize = _spawnArea.size;
        Vector3 spawnAreaCenter = transform.position;
        float randomX = Random.Range(-spawnAreaSize.x / _dividerSpawnArea, spawnAreaSize.x / _dividerSpawnArea);
        float randomZ = Random.Range(-spawnAreaSize.z / _dividerSpawnArea, spawnAreaSize.z / _dividerSpawnArea);
        return spawnAreaCenter + new Vector3(randomX, 0, randomZ);
    }
}
