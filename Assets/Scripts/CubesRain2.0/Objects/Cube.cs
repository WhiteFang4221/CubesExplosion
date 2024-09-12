using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
[RequireComponent(typeof(Rigidbody))]
public class Cube : PoolableObject<Cube>
{
    private Renderer _renderComponent;
    private Rigidbody _rigidbody;
    private float _minLifeTime = 2f;
    private float _maxLifeTime = 6f;
    private bool _isPlatformTouch = false;

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
        if (_isPlatformTouch == false && collision.gameObject.TryGetComponent(out Platform platform))
        {
            _isPlatformTouch = true;
            _renderComponent.material.color = UnityEngine.Random.ColorHSV();
            StartCoroutine(DestroyAfterDelay());
        }
    }

    private IEnumerator DestroyAfterDelay()
    {
        float randomLifetime = UnityEngine.Random.Range(_minLifeTime, _maxLifeTime);
        yield return new WaitForSeconds(randomLifetime);

        Disable();
        _renderComponent.material.color = Color.white;
    }
}
