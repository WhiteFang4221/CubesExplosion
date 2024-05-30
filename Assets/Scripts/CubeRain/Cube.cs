using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
[RequireComponent (typeof(Rigidbody))]
public class Cube : MonoBehaviour
{
    [SerializeField] private Renderer _renderComponent;
    [SerializeField] private Rigidbody _rigidbody;

    private CubePool _cubePooler;
    private float _minLifeTime = 2f;
    private float _maxLifeTime = 6f;
    private bool _isPlatformTouch = false;

    private void OnEnable()
    {
        _rigidbody.velocity = Vector3.zero;
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

    public void Initialize(CubePool pooler)
    {
        _cubePooler = pooler;
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
