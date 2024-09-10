using UnityEngine;

public abstract class Spawner<T> : MonoBehaviour where T: MonoBehaviour, IInitializable
{
    [SerializeField] protected Pool<T> ObjectPool;
}
