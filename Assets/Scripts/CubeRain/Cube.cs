using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class Cube : MonoBehaviour
{
    private Renderer _renderComponent;
    private CubePool _cubePooler;
    private float _minLifeTime = 2f;
    private float _maxLifeTime = 6f;
    private bool _isPlatformTouch = false;

    public void Initialize(CubePool pooler)
    {
        _cubePooler = pooler;
    }

    private void Start()
    {
        _renderComponent = GetComponent<Renderer>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Platform platform) && _isPlatformTouch == false)
        {
            _renderComponent.material.color = Random.ColorHSV();
            StartCoroutine(DestroyAfterDelay());
            _isPlatformTouch = true;
        }
    }

    private IEnumerator DestroyAfterDelay()
    {
        float randomLifetime = Random.Range(_minLifeTime, _maxLifeTime);
        yield return new WaitForSeconds(randomLifetime);

        _cubePooler.ReturnCubePool().Release(this);
        _renderComponent.material.color = Color.white;
        _isPlatformTouch = false;
    }
}
