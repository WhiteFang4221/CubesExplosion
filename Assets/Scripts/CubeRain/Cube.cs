using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class Cube : MonoBehaviour
{
    [SerializeField] private LayerMask _platformLayer;

    private CubePooler _cubePooler;

    private float _minLifeTime = 2f;
    private float _maxLifeTime = 6f;
    private bool _isPlatformTouch = false;
    private Renderer _cubeRenderComponent;

    public void Initialize(CubePooler pooler)
    {
        this._cubePooler = pooler;
    }

    private void Start()
    {
        _cubeRenderComponent = GetComponent<Renderer>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        var hitLayerMaxk = 1 << collision.gameObject.layer;

        if ((_platformLayer == hitLayerMaxk) && _isPlatformTouch == false)
        {
            _cubeRenderComponent.material.color = Random.ColorHSV();
            StartCoroutine(DestroyAfterDelay());
            _isPlatformTouch = true;
        }
    }

    private IEnumerator DestroyAfterDelay()
    {
        float randomLifetime = Random.Range(_minLifeTime, _maxLifeTime);
        yield return new WaitForSeconds(randomLifetime);

        _cubePooler.CubePool.Release(gameObject);
        _cubeRenderComponent.material.color = Color.white;
        _isPlatformTouch = false;

    }
}
