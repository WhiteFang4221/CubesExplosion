using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Camera))]
public class CubeSpawner : MonoBehaviour
{
    [SerializeField] private ExplosiveCube _cubePrefab;
    [SerializeField] private CubeSearcher _cubeSearcher;
   
    public event Action<ExplosiveCube, List<Rigidbody>> NewCubesSpawned;

    private int _minCubes = 2;
    private int _maxCubes = 7;
    private float _differenceScaleCubes = 2;
    private float _maxSplitChance = 101;
    private int _reducingSpawnChance = 2;

    private void OnEnable()
    {
        _cubeSearcher.CubeFounded += TrySpawnCubes;
    }

    private void OnDisable()
    {
        _cubeSearcher.CubeFounded -= TrySpawnCubes;
    }

    private void CreateNewCubes(ExplosiveCube explosiveCube, float splitChance)
    {
        int numberCubes = Random.Range(_minCubes, _maxCubes);
        List<Rigidbody> cubes = new List<Rigidbody>();

        for(int i = 1; i <= numberCubes; i++)
        {
            ExplosiveCube newCube = Instantiate(_cubePrefab, explosiveCube.transform.position, Quaternion.identity);
            newCube.InitializeSplitChance(splitChance);
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
        if(Random.Range(0, _maxSplitChance) <= explosiveCube.SplitChance)
        {
            CreateNewCubes(explosiveCube, explosiveCube.SplitChance);
        }    
    }
}
