using UnityEngine;
using UnityEngine.Pool;

public class CubePooler : MonoBehaviour
{
    [SerializeField] private GameObject _cubePrefab;
    [SerializeField] private Transform _parentTransform;
    [SerializeField] private int _poolCapacity = 1;
    [SerializeField] private int _poolMaxSize = 10;


    public ObjectPool<GameObject> CubePool;

    private void Awake()
    {
        CubePool = new ObjectPool<GameObject>(
            createFunc: () => CreateCube(),
            actionOnGet: (cube) => ActionOnGet(cube),
            actionOnRelease: (cube) => cube.SetActive(false),
            actionOnDestroy: (cube) => Destroy(cube),
            collectionCheck: true,
            defaultCapacity: _poolCapacity,
            maxSize: _poolMaxSize);
    }

    private GameObject CreateCube()
    {
        GameObject cube = Instantiate(_cubePrefab, _parentTransform);

        if (cube.TryGetComponent(out Cube cubeComponent))
        {
            cubeComponent.Initialize(this);
        }

        return cube;
    }

    private void ActionOnGet(GameObject cube)
    {
        cube.SetActive(true);
        cube.TryGetComponent(out Rigidbody rb);
        rb.velocity = Vector3.zero;
    }
}
