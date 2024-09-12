using TMPro;
using UnityEngine;

public abstract class ViewCount<T> : MonoBehaviour, IView where T : ISpawnable
{
    [SerializeField] private TextMeshProUGUI _spawnedText;
    [SerializeField] private TextMeshProUGUI _instantiatedText;
    [SerializeField] private TextMeshProUGUI _activeText;

    private string _initialSpawnedText = "Заспавнено: ";
    private string _initialInstantiatedText = "Создано: ";
    private string _initialActiveText = "Активно: ";

    public void UpdateSpawned(int count)
    {
        _spawnedText.text = $"{_initialSpawnedText} {count}";
    }

    public void UpdateInstantiated(int count)
    {
        _instantiatedText.text = $"{_initialInstantiatedText} {count}";
    }

    public void UpdateActive(int count)
    {
        _activeText.text = $"{_initialActiveText} {count}";
    }
}
