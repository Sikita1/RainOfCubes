using System.Collections.Generic;
using UnityEngine;


public class Destruction : MonoBehaviour
{
    [SerializeField] private float _fissionExplosionRadius;
    [SerializeField] private float _fussionExplosionForce;

    [SerializeField] private Bomb _unit;

    private void OnEnable()
    {
        _unit.Obliterating += OnObliterating;
    }

    private void OnDisable()
    {
        _unit.Obliterating -= OnObliterating;
    }

    public void Explode(List<Bomb> units)
    {
        units
            .ForEach(rigidbody => rigidbody
            .AddExplosion(_fussionExplosionForce, transform.position, _fissionExplosionRadius));
    }

    public void OnObliterating()
    {
        foreach (Rigidbody explodableObject in GetExplodableObjects())
            explodableObject.AddExplosionForce(_unit.CurrentForceDestruction,
                                               transform.position,
                                               _unit.CurrentRadiusDestruction);
    }

    private IEnumerable<Rigidbody> GetExplodableObjects()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, _unit.CurrentRadiusDestruction);

        List<Rigidbody> objects = new();

        foreach (Collider hit in hits)
            if (hit.attachedRigidbody != null)
                objects.Add(hit.attachedRigidbody);

        return objects;
    }
}
