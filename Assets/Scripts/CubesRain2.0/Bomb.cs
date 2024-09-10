using System;
using System.Collections;
using System.Drawing;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class Bomb : MonoBehaviour, IInitializable
{
    private BombPool _bombPool;
    private Renderer _renderer;
    private float _minLifeTime = 2f;
    private float _maxLifeTime = 6f;
    private float _randomLifetime;

    private Coroutine _LifeTimeCoroutine;

    public event Action<Transform> Exploded;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
    }

    private void OnEnable()
    {
        UnityEngine.Color color = _renderer.material.color;
        color.a = 1f;
    }

    private void Start()
    {
       
        _randomLifetime = UnityEngine.Random.Range(_minLifeTime, _maxLifeTime);
        _LifeTimeCoroutine = StartCoroutine(LifeTimeCoroutine());
    }

    public void Initialize(object pooler)
    {
        _bombPool = (BombPool)pooler;
    }

    private IEnumerator LifeTimeCoroutine()
    {
        StartCoroutine(FadeOutCoroutine());
        yield return new WaitForSeconds(_randomLifetime);
        Exploded?.Invoke(transform);
        _bombPool.ReturnObjectPool().Release(this);
    }

    private IEnumerator FadeOutCoroutine()
    {
        float elapsedTime = 0f;

        UnityEngine.Color color = _renderer.material.color;

        while (elapsedTime <= _randomLifetime)
        {
            float normalizedTime = elapsedTime / _randomLifetime;
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Lerp(1f, 0f, normalizedTime);
            _renderer.material.color = color;

            yield return null;
        }
    }
}
