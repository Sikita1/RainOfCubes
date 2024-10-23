using TMPro;
using UnityEngine;

public class SpawnerView<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] private Spawner<T> _spawner;

    [SerializeField] private TMP_Text _spawnedCount;
    [SerializeField] private TMP_Text _createdCount;
    [SerializeField] private TMP_Text _activeCount;

    private void OnEnable()
    {
        _spawner.FacilitiesCreated += OnUpdateData;
        _spawner.GeneratedObjects += OnGeneratedObjects;
        _spawner.ActiveObjects += OnActiveObjects;
    }
    private void OnDisable()
    {
        _spawner.FacilitiesCreated -= OnUpdateData;
        _spawner.GeneratedObjects -= OnGeneratedObjects;
        _spawner.ActiveObjects += OnActiveObjects;
    }

    private void OnActiveObjects(int count) =>
        _activeCount.text = $"�������: {count}";

    private void OnGeneratedObjects(int count) =>
        _createdCount.text = $"��������: {count}";

    private void OnUpdateData(int currrentCount) =>
        _spawnedCount.text = $"�����: {currrentCount}";
}
