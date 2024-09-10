using UnityEngine;

[RequireComponent(typeof(Spawner<Cube>))]
public class CubesCountStatistics : CountStatistics
{
    [SerializeField] private Pool<Cube> _pool;
    private CubeSpawner _spawner;

    private void Awake()
    {
        _spawner = GetComponent<CubeSpawner>();
    }

    private void OnEnable()
    {
        _pool.Created += UpdateInstantiated;
        _pool.Activated += IncreaseActive;
        _pool.Disabled += DecreaseActive;
        _spawner.CubeSpawned += UpdateSpawned;
    }

    private void OnDisable()
    {
        _pool.Created -= UpdateInstantiated;
        _pool.Activated -= IncreaseActive;
        _pool.Disabled -= DecreaseActive;
        _spawner.CubeSpawned -= UpdateSpawned;
    }
}
