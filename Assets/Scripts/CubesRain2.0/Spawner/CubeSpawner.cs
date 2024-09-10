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

    public event Action CubeSpawned;


    private void Awake()
    {
        _spawnArea = GetComponent<BoxCollider>();
    }

    private void Start()
    {
        _spawnArea = GetComponent<BoxCollider>();
        StartCoroutine(SpawnCubesCoroutine());
    }

    private IEnumerator SpawnCubesCoroutine()
    {
        {
            while (true)
            {
                Vector3 spawnAreaSize = _spawnArea.size;
                Vector3 spawnAreaCenter = transform.position;

                float randomX = UnityEngine.Random.Range(-spawnAreaSize.x / _dividerSpawnArea, spawnAreaSize.x / _dividerSpawnArea);
                float randomZ = UnityEngine.Random.Range(-spawnAreaSize.z / _dividerSpawnArea, spawnAreaSize.z / _dividerSpawnArea);
                Vector3 spawnPosition = spawnAreaCenter + new Vector3(randomX, 0, randomZ);

                Cube cube = ObjectPool.Get(transform);
                CubeSpawned?.Invoke();
                cube.LifeTimeOver += ReturnCube;
                cube.transform.position = spawnPosition;
                cube.SetBombSpawner(_bombSpawner);

                yield return _spawnDelay;
            }
        }
    }

    private void ReturnCube(Cube cube)
    {
        ObjectPool.Return(cube);
        cube.LifeTimeOver -= ReturnCube;
    }
}
