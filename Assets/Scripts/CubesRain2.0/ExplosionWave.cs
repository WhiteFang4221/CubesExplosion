using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Bomb))]  
public class ExplosionWave : MonoBehaviour
{
    [SerializeField] private float _explosionRadius = 6;
    [SerializeField] private float _explosionForce = 300;
    private Bomb _bomb;

    private void Awake()
    {
        _bomb = GetComponent<Bomb>();
    }
    //private void OnEnable()
    //{
    //    _bomb.Exploded += PushAllCubes;
    //}

    //private void OnDisable()
    //{
    //    _bomb.Exploded -= PushAllCubes;
    //}

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

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(transform.position, _explosionRadius);
    }
}
