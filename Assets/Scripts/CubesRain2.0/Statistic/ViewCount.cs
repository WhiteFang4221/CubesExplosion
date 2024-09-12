using TMPro;
using UnityEngine;

[RequireComponent(typeof(Spawner<>))]
public abstract class ViewCount<T> : MonoBehaviour, IView where T : PoolableObject<T>
{
    private Spawner<T> _spawner;
    [SerializeField] private TextMeshProUGUI _spawnedText;
    [SerializeField] private TextMeshProUGUI _instantiatedText;
    [SerializeField] private TextMeshProUGUI _activeText;

    private string _initialSpawnedText = "Заспавнено: ";
    private string _initialInstantiatedText = "Создано: ";
    private string _initialActiveText = "Активно: ";

    private void Awake()
    {
        _spawner = GetComponent<Spawner<T>>();
    }

    private void OnEnable()
    {
        _spawner.SpawnUpdated += UpdateSpawned;
        _spawner.InstantiateUpdated += UpdateInstantiated;
        _spawner.ActiveUpdated += UpdateActive;
    }

    private void OnDisable()
    {
        _spawner.SpawnUpdated -= UpdateSpawned;
        _spawner.InstantiateUpdated -= UpdateInstantiated;
        _spawner.ActiveUpdated -= UpdateActive;
    }

    public void UpdateSpawned(ulong count)
    {
        _spawnedText.text = $"{_initialSpawnedText} {count}";
    }

    public void UpdateInstantiated(ulong count)
    {
        _instantiatedText.text = $"{_initialInstantiatedText} {count}";
    }

    public void UpdateActive(ulong count)
    {
        _activeText.text = $"{_initialActiveText} {count}";
    }
}
