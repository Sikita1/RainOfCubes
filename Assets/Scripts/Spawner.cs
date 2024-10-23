using UnityEngine;
using UnityEngine.Events;

public class Spawner<T> : ObjectPooll<T> where T : MonoBehaviour
{
    [SerializeField] private T _prefab;

    public event UnityAction<int> GeneratedObjects;
    public event UnityAction<int> ActiveObjects;

    public int Generated { get; private set; } = 0;
    public int Active { get; private set; } = 0;

    private void Start()
    {
        Initialize(_prefab);
    }

    public void ObjectCreated()
    {
        Generated++;
        GeneratedObjects?.Invoke(Generated);
    }

    public void ObjectActivated()
    {
        Active++;
        ActiveObjects?.Invoke(Active);
    }

    public void ObjectDeactivated()
    {
        Active--;
        ActiveObjects?.Invoke(Active);
    }

}