using System.Collections;
using UnityEngine;

public abstract class Spawner<T> : MonoBehaviour where T: MonoBehaviour, IInitializable
{
    [SerializeField] protected T _prefab;
    [SerializeField] protected Pool<T> _pool;
}
