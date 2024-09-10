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
    }

    private void Start()
    {
        _spawnArea = GetComponent<BoxCollider>();

        if (_spawnArea.isTrigger == false)
        {
            _spawnArea.isTrigger = true;
        }

        StartCoroutine(SpawnCubesCoroutine());
    }

    private IEnumerator SpawnCubesCoroutine()
    {
        {
            while (true)
            {
                Vector3 spawnAreaSize = _spawnArea.size;
                Vector3 spawnAreaCenter = transform.position;

                float randomX = Random.Range(-spawnAreaSize.x / _dividerSpawnArea, spawnAreaSize.x / _dividerSpawnArea);
                float randomZ = Random.Range(-spawnAreaSize.z / _dividerSpawnArea, spawnAreaSize.z / _dividerSpawnArea);
                Vector3 spawnPosition = spawnAreaCenter + new Vector3(randomX, 0, randomZ);

                Cube createdObject = _pool.ReturnObjectPool().Get();
                createdObject.transform.position = spawnPosition;
                createdObject.SetBombSpawner(_bombSpawner);

                yield return _spawnDelay;
            }
        }
    }
}
