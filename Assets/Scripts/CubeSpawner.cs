using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Camera))]
public class CubeSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _cubePrefab;
    
    public event Action<ExplosiveCube, List<Rigidbody>> NewCubesSpawned;

    private Camera _camera;
    private int _minCubes = 2;
    private int _maxCubes = 7;
    private float _differenceScaleCubes = 2;
    private float _maxSpawnChance = 101;
    private float _currentSpawnChance;
    private int _reducingSpawnChance = 2;

    private void Start()
    {
        _camera = GetComponent<Camera>();
        _currentSpawnChance = _maxSpawnChance;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            IdentifyExplosiveCube();
        }
    }

    private void IdentifyExplosiveCube()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            ExplosiveCube explosiveCube = hit.collider.GetComponent<ExplosiveCube>();

            if (explosiveCube != null)
            {
                Destroy(explosiveCube.gameObject);
                TrySpawnCubes(explosiveCube);
            }
        }
    }

    private void CreateNewCubes(ExplosiveCube explosiveCube)
    {
        int numberCubes = Random.Range(_minCubes, _maxCubes);
        List<Rigidbody> cubes = new List<Rigidbody>();

        for(int i = 1; i <= numberCubes; i++)
        {
            GameObject newCube = Instantiate(_cubePrefab, explosiveCube.transform.position, Quaternion.identity);
            newCube.transform.localScale = explosiveCube.transform.localScale / _differenceScaleCubes;
            
            if (newCube.TryGetComponent(out Rigidbody cubeRigidbody))
            {
                cubes.Add(cubeRigidbody);
            }
        }
            
        NewCubesSpawned?.Invoke(explosiveCube, cubes);
    }

    private void TrySpawnCubes(ExplosiveCube explosiveCube)
    {
        if(Random.Range(0, _maxSpawnChance) <= _currentSpawnChance)
        {
            CreateNewCubes(explosiveCube);
            _currentSpawnChance/= _reducingSpawnChance;
        }    
    }
}
