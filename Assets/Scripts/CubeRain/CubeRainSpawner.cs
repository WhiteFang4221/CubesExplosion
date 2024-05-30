using System.Collections;
using UnityEngine;

[RequireComponent (typeof(BoxCollider))]
public class CubeRainSpawner : MonoBehaviour
{
    [SerializeField] private CubePool _cubePooler;
    [SerializeField] private Cube _cube;
    
    private WaitForSeconds _spawnDelay = new WaitForSeconds(1f);
    private BoxCollider _spawnArea;
    private float _dividerSpawnArea = 2f;

    private void Start()
    {
        _spawnArea = GetComponent<BoxCollider>();

        if (_spawnArea.isTrigger == false)
        {
            _spawnArea.isTrigger = true;
        }

        StartCoroutine(SpawnCubes());
    }

    private IEnumerator SpawnCubes()
    {
        while (true)
        {
            Vector3 spawnAreaSize = _spawnArea.size;
            Vector3 spawnAreaCenter = transform.position;

            float randomX = Random.Range(-spawnAreaSize.x / _dividerSpawnArea, spawnAreaSize.x / _dividerSpawnArea);
            float randomZ = Random.Range(-spawnAreaSize.z / _dividerSpawnArea, spawnAreaSize.z / _dividerSpawnArea);
            Vector3 spawnPosition = spawnAreaCenter + new Vector3(randomX, 0, randomZ);

            Cube cube = _cubePooler.ReturnCubePool().Get();
            cube.transform.position = spawnPosition;

            yield return _spawnDelay;
        }
    }
}
