using System.Collections.Generic;
using UnityEngine;

public class ExplosiveCube : MonoBehaviour
{
    [SerializeField] private float _explosionRadius;
    [SerializeField] private float _explosionForce;
    [SerializeField] private GameObject _cubePrefab;
    [SerializeField] private LayerMask _newCubesLayer;

    private int _minNewCubes = 2;
    private int _maxNewCubes = 7;
    private float _maxChance = 101;
    private float _SpawnChance;

    private void Start()
    {
        _SpawnChance = _maxChance;
    }
    private void OnMouseUpAsButton()
    {
        float SpawnOrNot = Random.Range(0, _maxChance);

        if (SpawnOrNot <= _SpawnChance)
        {
            CreateNewCubes();
            Explode();
        }

        Destroy(gameObject);
        _SpawnChance /= 2;
    }

    private void Explode()
    {
        foreach (Rigidbody explodableObject in GetExplodableObjects())
        {
            if (explodableObject)
            explodableObject.AddExplosionForce(_explosionForce, transform.position, _explosionRadius);
        }
    }

    private void CreateNewCubes()
    {
        int RandomCubesCount = Random.Range(_minNewCubes, _maxNewCubes);

        for (int i = 1; i <= RandomCubesCount; i++)
        {
            GameObject cube = Instantiate(_cubePrefab, transform.position, Quaternion.identity);
            cube.transform.localScale /= i*2;
        }
    }

    private List<Rigidbody> GetExplodableObjects()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, _explosionRadius, _newCubesLayer);

        List<Rigidbody> cubes = new List<Rigidbody>();

        foreach (Collider hit in hits)
        {
            if(hit.attachedRigidbody != null)
            {
                cubes.Add(hit.attachedRigidbody);
            }
        }

        return cubes;
    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawSphere(transform.position, _explosionRadius);
    //}
}
