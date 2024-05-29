using System.Collections;
using UnityEngine;

[RequireComponent (typeof(BoxCollider))]
public class CubeRain : MonoBehaviour
{
    [SerializeField] private CubePooler _cubePooler;
    [SerializeField] private Cube _cube;
    
    private Coroutine _spawnCoroutine;
    private BoxCollider _spawnArea;
    private WaitForSeconds _spawnDelay = new WaitForSeconds(1f);

    private void Start()
    {
        _spawnArea = GetComponent<BoxCollider>();

        if (_spawnArea.isTrigger == false)
        {
            _spawnArea.isTrigger = true;
        }

       _spawnCoroutine = StartCoroutine(SpawnCubes());
    }

    private void OnDisable()
    {
        if (_spawnCoroutine != null)
        {
            StopCoroutine(_spawnCoroutine);
        }
    }


    private IEnumerator SpawnCubes()
    {
        while (true)
        {
            Vector3 spawnAreaSize = _spawnArea.size;
            Vector3 spawnAreaCenter = transform.position;

            float randomX = Random.Range(-spawnAreaSize.x / 2, spawnAreaSize.x / 2);
            float randomZ = Random.Range(-spawnAreaSize.z / 2, spawnAreaSize.z / 2);
            Vector3 spawnPosition = spawnAreaCenter + new Vector3(randomX, 0, randomZ);

            Cube cube = _cubePooler.CubePool.Get();
            cube.transform.position = spawnPosition;

            yield return _spawnDelay;
        }
    }
}
