using System.Collections.Generic;
using UnityEngine;

public class Destruction : MonoBehaviour
{
    [SerializeField] private Bomb _unit;

    private void OnEnable()
    {
        _unit.Obliterating += OnObliterating;
    }

    private void OnDisable()
    {
        _unit.Obliterating -= OnObliterating;
    }

    public void OnObliterating()
    {
        foreach (Rigidbody explodableObject in GetExplodableObjects())
            explodableObject.AddExplosionForce(_unit.ForceDestruction,
                                               transform.position,
                                               _unit.RadiusDestruction);
    }

    private IEnumerable<Rigidbody> GetExplodableObjects()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, _unit.RadiusDestruction);

        List<Rigidbody> objects = new();

        foreach (Collider hit in hits)
            if (hit.attachedRigidbody != null)
                objects.Add(hit.attachedRigidbody);

        return objects;
    }
}
