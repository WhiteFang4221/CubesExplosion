using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Renderer), typeof(ExplosionWave))]
public class Bomb : PoolableObject<Bomb>
{
    private Renderer _renderer;
    private Rigidbody _rigidbody;
    private IExplosion _explosionWave;

    private float _minLifeTime = 2f;
    private float _maxLifeTime = 6f;
    private float _randomLifetime;
    private Coroutine _lifeTimeCoroutine;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _rigidbody = GetComponent<Rigidbody>();
        _explosionWave = GetComponent<IExplosion>();
    }

    private void OnEnable()
    {  
        _rigidbody.velocity = Vector3.zero;
        _randomLifetime = UnityEngine.Random.Range(_minLifeTime, _maxLifeTime);
        StartLifeTimeCoroutine();
    }

    private IEnumerator LifeTimeCoroutine()
    {
        yield return (StartCoroutine(FadeOutCoroutine()));
        Disable();
        _explosionWave.ExecuteExplosion(transform);
    }

    private IEnumerator FadeOutCoroutine()
    {
        float elapsedTime = 0f;

        Color color = _renderer.material.color;

        while (elapsedTime <= _randomLifetime)
        {
            float normalizedTime = elapsedTime / _randomLifetime;
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Lerp(1f, 0f, normalizedTime);
            _renderer.material.color = color;

            yield return null;
        }
    }

    private void StartLifeTimeCoroutine()
    {
        if(_lifeTimeCoroutine != null)
        {
            StopCoroutine(_lifeTimeCoroutine);
        }

        _lifeTimeCoroutine = StartCoroutine(LifeTimeCoroutine());
    }
}
