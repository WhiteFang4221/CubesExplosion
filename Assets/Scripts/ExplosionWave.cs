using System.Collections.Generic;
using UnityEngine;

public class ExplosionWave : MonoBehaviour
{
    [SerializeField] private float _basedExplosionRadius = 6;
    [SerializeField] private float _basedExplosionForce = 300;
    [SerializeField] private CubeSpawner _cubeSpawner;

    private void OnEnable()
    {
        _cubeSpawner.NewCubesSpawned += ExplodeCube;
        _cubeSpawner.CubeDisappeared += PushObjects;
    }

    private void OnDisable()
    {
        _cubeSpawner.NewCubesSpawned -= ExplodeCube;
        _cubeSpawner.CubeDisappeared -= PushObjects;
    }

    private void ExplodeCube(ExplosiveCube explosiveCube, List<Rigidbody> newCubes)
    {
        float scale = explosiveCube.transform.localScale.magnitude;
        float ExplosionForce = _basedExplosionForce / scale;
        float ExplosionRadius = _basedExplosionRadius / scale;

        foreach (Rigidbody cube in newCubes)
        {
            cube.AddExplosionForce(ExplosionForce, explosiveCube.transform.position, ExplosionRadius);
        }
    }

    private void PushObjects(ExplosiveCube explosiveCube)
    {
        Collider[] hits = Physics.OverlapSphere(explosiveCube.transform.position, _basedExplosionRadius);

        List<Rigidbody> objects = new List<Rigidbody>();

        foreach (Collider hit in hits)
        {
            if (hit.attachedRigidbody != null)
            {
                objects.Add(hit.attachedRigidbody);
            }
        }

        ExplodeCube(explosiveCube, objects);
    }
}