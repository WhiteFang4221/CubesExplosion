using System.Collections.Generic;
using UnityEngine;

namespace ExplosionCubes
{
    public class ExplosionWave : MonoBehaviour
    {
        [SerializeField] private float _basedExplosionRadius = 6;
        [SerializeField] private float _basedExplosionForce = 300;
        [SerializeField] private CubeSpawner _cubeSpawner;

        //private void OnEnable()
        //{
        //    _cubeSpawner.NewCubesSpawned += PushNewCubes;
        //    _cubeSpawner.CubeDisappeared += PushAllCubes;
        //}

        //private void OnDisable()
        //{
        //    _cubeSpawner.NewCubesSpawned -= PushNewCubes;
        //    _cubeSpawner.CubeDisappeared -= PushAllCubes;
        //}

        private void PushNewCubes(ExplosiveCube explosiveCube, List<Rigidbody> newCubes)
        {
            foreach (Rigidbody cube in newCubes)
            {
                cube.AddExplosionForce(_basedExplosionForce, explosiveCube.transform.position, _basedExplosionRadius);
            }
        }

        private void PushAllCubes(ExplosiveCube explosiveCube)
        {
            float scale = explosiveCube.transform.localScale.magnitude;
            float ExplosionForce = _basedExplosionForce / scale;
            float ExplosionRadius = _basedExplosionRadius / scale;

            Collider[] hits = Physics.OverlapSphere(explosiveCube.transform.position, _basedExplosionRadius);
            List<Rigidbody> cubes = new List<Rigidbody>();

            foreach (Collider hit in hits)
            {
                if (hit.attachedRigidbody != null)
                {
                    cubes.Add(hit.attachedRigidbody);
                }
            }

            foreach (Rigidbody cube in cubes)
            {
                cube.AddExplosionForce(ExplosionForce, explosiveCube.transform.position, ExplosionRadius);
            }
        }
    }
}
