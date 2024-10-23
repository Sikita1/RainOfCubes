using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class ObjectPooll<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] private GameObject _container;
    [SerializeField] private int _capacity;

    public event Action<int> FacilitiesCreated;

    private int _objects—reated = 0;

    private List<T> _pool = new List<T>();

    public void Initialize(T prefab)
    {
        for (int i = 0; i < _capacity; i++)
        {
            var spawned = Instantiate(prefab, _container.transform);
            spawned.gameObject.SetActive(false);

            _objects—reated++;
            FacilitiesCreated?.Invoke(_objects—reated);

            _pool.Add(spawned);
        }
    }

    public bool TryGetObject(out T result)
    {
        result = _pool.FirstOrDefault(unit => unit.gameObject.activeSelf == false);

        return result != null;
    }
}
