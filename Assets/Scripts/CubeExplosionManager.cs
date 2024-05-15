using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class CubeExplosionManager : MonoBehaviour
{
    [SerializeField] private GameObject _cubePrefab;
    [SerializeField] private float _explosionRadius;
    [SerializeField] private float _explosionForce;

    public Action CubeCLicked;

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
            CheckClickedObject();
        }
    }

    private void CheckClickedObject()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            ExplosiveCube explosiveCube = hit.collider.gameObject.GetComponent<ExplosiveCube>();

            if (explosiveCube != null)
            {

                ExplodeCube(explosiveCube.gameObject);
            }
        }
    }

    private void ExplodeCube(GameObject cube)
    {
        if (IsShouldSpawnCubes() == true)
        {
            CreateNewCubes(cube);
            _currentSpawnChance/=_differenceScaleCubes;
        }

        foreach(Rigidbody explodableObject in GetExplodableObjects(cube))
        {
            explodableObject.AddExplosionForce(_explosionForce, cube.transform.position, _explosionRadius);
        }

        Destroy(cube);
    }

    private void CreateNewCubes(GameObject oldCube)
    {
        int numberCubes = Random.Range(_minCubes, _maxCubes);

        for(int i = 1; i <= numberCubes; i++)
        {
            GameObject newCube = Instantiate(_cubePrefab, oldCube.transform.position, Quaternion.identity);
            newCube.transform.localScale = oldCube.transform.localScale / (float)Math.Pow(_differenceScaleCubes, i);
        }
    }

    private List<Rigidbody> GetExplodableObjects(GameObject explosiveCube)
    {
        Collider[] hits = Physics.OverlapSphere(explosiveCube.transform.position, _explosionRadius);

        List<Rigidbody> cubes = new List<Rigidbody>();

        foreach(Collider hit in hits)
        {
            if (hit.attachedRigidbody != null)
            {
                cubes.Add(hit.attachedRigidbody);
            }
        }

        return cubes;
    }

    private bool IsShouldSpawnCubes()
    {
        return (Random.Range(0, _maxSpawnChance) <= _currentSpawnChance);
    }
}
