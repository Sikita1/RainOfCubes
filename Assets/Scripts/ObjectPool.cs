using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private GameObject _container;
    [SerializeField] private int _capacity;

    private List<Cube> _pool = new List<Cube>();

    protected void Initialize(Cube prefab)
    {
        for (int i = 0; i < _capacity; i++)
        {
            Cube spawned = Instantiate(prefab, _container.transform);
            spawned.gameObject.SetActive(false);

            _pool.Add(spawned);
        }
    }

    protected bool TryGetObject(out Cube result)
    {
        result = _pool.FirstOrDefault(unit => unit.gameObject.activeSelf == false);

        return result != null;
    }
}
