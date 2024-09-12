using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Bomb))]  
public class ExplosionWave : MonoBehaviour, IExplosion
{
    [SerializeField] private float _explosionRadius = 6;
    [SerializeField] private float _explosionForce = 300;

    public void ExecuteExplosion(Transform explodePosition)
    {
        PushAllCubes(explodePosition);
    }

    private void PushAllCubes(Transform explodePosition)
    {
        Collider[] hits = Physics.OverlapSphere(explodePosition.transform.position, _explosionRadius);
        List<Rigidbody> rigidbodies = new List<Rigidbody>();

        foreach (Collider hit in hits)
        {
            if (hit.attachedRigidbody != null)
            {
                rigidbodies.Add(hit.attachedRigidbody);
            }
        }

        foreach (Rigidbody rigidbody in rigidbodies)
        {
            rigidbody.AddExplosionForce(_explosionForce, explodePosition.transform.position, _explosionRadius);
        }
    }
}
