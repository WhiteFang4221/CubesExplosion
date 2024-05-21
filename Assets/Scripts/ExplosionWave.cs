using System.Collections.Generic;
using UnityEngine;

public class ExplosionWave : MonoBehaviour
{
    [SerializeField] private float _explosionRadius;
    [SerializeField] private float _explosionForce;
    [SerializeField] private CubeSpawner _cubeSpawner;

    private void OnEnable()
    {
        _cubeSpawner.NewCubesSpawned += ExplodeCube;
    }

    private void OnDisable()
    {
        _cubeSpawner.NewCubesSpawned -= ExplodeCube;
    }

    private void ExplodeCube(ExplosiveCube explosiveCube, List<Rigidbody> newCubes)
    {
        foreach(Rigidbody cube in newCubes)
        {
            cube.AddExplosionForce(_explosionForce, explosiveCube.transform.position, _explosionRadius);
        }
    }
}
