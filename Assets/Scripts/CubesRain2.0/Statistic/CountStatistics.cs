using TMPro;
using UnityEngine;

public abstract class CountStatistics: MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _spawnedText;
    [SerializeField] private TextMeshProUGUI _instantiatedText;
    [SerializeField] private TextMeshProUGUI _activeText;

    private string _initialSpawnedText = "Заспавнено: ";
    private string _initialInstantiatedText = "Создано: ";
    private string _initialActiveText = "Активно: ";

    private int _totalSpawned;
    private int _totalInstantiated;
    private int _activeOnScene;


    private void  UpdateUI()
    {
        _spawnedText.text = $"{_initialSpawnedText} {_totalSpawned}";
        _instantiatedText.text = $"{_initialInstantiatedText} {_totalInstantiated}";
        _activeText.text = $"{_initialActiveText} {_activeOnScene}";
    }

    protected void UpdateSpawned()
    {
        _totalSpawned++;
        UpdateUI();
    }

    protected void UpdateInstantiated()
    {
        _totalInstantiated++;
        UpdateUI();
    }

    protected void  IncreaseActive()
    {
        _activeOnScene++;
        UpdateUI();
    }

    protected void DecreaseActive()
    {
        _activeOnScene--;
        UpdateUI();
    }
}
