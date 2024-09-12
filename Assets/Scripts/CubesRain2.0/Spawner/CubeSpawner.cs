using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]

public class CubeSpawner : Spawner<Cube>
{
    [SerializeField] private BombSpawner _bombSpawner;
    private BoxCollider _spawnArea;
    private WaitForSeconds _spawnDelay = new WaitForSeconds(1f);
    private float _dividerSpawnArea = 2f;

    public event Action<BombSpawner> CubeSpawned;

    protected void Awake()
    {
        _spawnArea = GetComponent<BoxCollider>();
    }

    private void Start()
    {
        StartCoroutine(SpawnCubesCoroutine());
    }

    private IEnumerator SpawnCubesCoroutine()
    {
        while (true)
        {
            SpawnObject(GetSpawnArea());
            CubeSpawned?.Invoke(_bombSpawner);
            yield return _spawnDelay;
        }
    }

    protected override void OnObjectRelease(Cube obj)
    {
        _bombSpawner.SpawnObject(obj.transform.position);
    }

    private Vector3 GetSpawnArea()
    {
        Vector3 spawnAreaSize = _spawnArea.size;
        Vector3 spawnAreaCenter = transform.position;
        float randomX = UnityEngine.Random.Range(-spawnAreaSize.x / _dividerSpawnArea, spawnAreaSize.x / _dividerSpawnArea);
        float randomZ = UnityEngine.Random.Range(-spawnAreaSize.z / _dividerSpawnArea, spawnAreaSize.z / _dividerSpawnArea);
        return spawnAreaCenter + new Vector3(randomX, 0, randomZ);
    }
}
