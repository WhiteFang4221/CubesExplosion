using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
[RequireComponent(typeof(Rigidbody))]
public class Cube : MonoBehaviour, IInitializable
{
    private Renderer _renderComponent;
    private Rigidbody _rigidbody;
    private CubePool _cubePooler;
    private BombSpawner _bombSpawner;
    private float _minLifeTime = 2f;
    private float _maxLifeTime = 6f;
    private bool _isPlatformTouch = false;

    public event Action<Cube> LifeTimeOver;

    private void OnEnable()
    {
        _isPlatformTouch = false;
        _rigidbody.velocity = Vector3.zero;
    }

    private void Awake()
    {
        _renderComponent = GetComponent<Renderer>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Platform platform) && _isPlatformTouch == false)
        {
            _isPlatformTouch = true;
            _renderComponent.material.color = UnityEngine.Random.ColorHSV();
            StartCoroutine(DestroyAfterDelay());
        }
    }

    public void Initialize(object pool)
    {
        _cubePooler = (CubePool)pool;
    }

    public void SetBombSpawner(BombSpawner bombSpawner)
    {
        _bombSpawner = bombSpawner;
    }

    private IEnumerator DestroyAfterDelay()
    {
        float randomLifetime = UnityEngine.Random.Range(_minLifeTime, _maxLifeTime);
        yield return new WaitForSeconds(randomLifetime);

        LifeTimeOver?.Invoke(this);

        if (_bombSpawner != null)
        {
            _bombSpawner.SpawnBombAtPosition(transform);
        }

        _renderComponent.material.color = Color.white;
    }
}
