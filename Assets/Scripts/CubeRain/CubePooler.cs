using UnityEngine;
using UnityEngine.Pool;

public class CubePooler : MonoBehaviour
{
    [SerializeField] private Cube _cubePrefab;
    [SerializeField] private Transform _parentTransform;
    [SerializeField] private int _poolCapacity = 1;
    [SerializeField] private int _poolMaxSize = 10;

    public ObjectPool<Cube> CubePool;

    private void Awake()
    {
        CubePool = new ObjectPool<Cube>(
            createFunc: CreateCube,
            actionOnGet:  ActionOnGet,
            actionOnRelease: (cube) => cube.gameObject.SetActive(false),
            actionOnDestroy: Destroy,
            collectionCheck: true,
            defaultCapacity: _poolCapacity,
            maxSize: _poolMaxSize);
    }

    private Cube CreateCube()
    {
        Cube cube = Instantiate(_cubePrefab, _parentTransform);

        if (cube.TryGetComponent(out Cube cubeComponent))
        {
            cubeComponent.Initialize(this);
        }

        return cube;
    }

    private void ActionOnGet(Cube cube)
    {
        cube.gameObject.SetActive(true);
        cube.TryGetComponent(out Rigidbody rb);
        rb.velocity = Vector3.zero;
    }
}
