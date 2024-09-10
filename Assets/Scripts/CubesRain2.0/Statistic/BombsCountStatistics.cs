using UnityEngine;

[RequireComponent (typeof(Spawner<Bomb>))]
public class BombsCountStatistics : CountStatistics
{
    [SerializeField] private Pool<Bomb> _pool;
    private BombSpawner _spawner;

    private void Awake()
    {
        _spawner = GetComponent<BombSpawner>();
    }

    private void OnEnable()
    {
        _pool.Created += UpdateInstantiated;
        _pool.Activated += IncreaseActive;
        _pool.Disabled += DecreaseActive;
        _spawner.BombSpawned += UpdateSpawned;
    }

    private void OnDisable()
    {
        _pool.Created -= UpdateInstantiated;
        _pool.Activated -= IncreaseActive;
        _pool.Disabled -= DecreaseActive;
        _spawner.BombSpawned -= UpdateSpawned;
    }
}
