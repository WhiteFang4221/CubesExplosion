using UnityEngine;
using UnityEngine.Pool;


public class CubePool : MonoBehaviour
{
    [SerializeField] private Cube _cubePrefab;
    [SerializeField] private Transform _parentTransform;
    [SerializeField] private int _poolCapacity = 1;
    [SerializeField] private int _poolMaxSize = 10;

    private ObjectPool<Cube> _cubePool;

    private void Awake()
    {
        _cubePool = new ObjectPool<Cube>(
            createFunc: CreateCube,
            actionOnGet:  PullOut,
            actionOnRelease: (cube) => cube.gameObject.SetActive(false),
            actionOnDestroy: Destroy,
            collectionCheck: true,
            defaultCapacity: _poolCapacity,
            maxSize: _poolMaxSize);
    }

    public ObjectPool<Cube> ReturnCubePool()
    {
        return _cubePool;
    }

    private Cube CreateCube()
    {
        Cube cube = Instantiate(_cubePrefab, _parentTransform);
        cube.Initialize(this);

        return cube;
    }

    private void PullOut(Cube cube)
    {
        cube.gameObject.SetActive(true);
    }
}
