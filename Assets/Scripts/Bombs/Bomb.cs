using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

[RequireComponent(typeof(Renderer))]
[RequireComponent(typeof(Rigidbody))]
public class Bomb : MonoBehaviour
{
    private float _minLifetime = 2f;
    private float _maxLifetime = 5f;

    private WaitForSeconds _wait;

    private Color _originalColor;

    private float Lifetime => Random.Range(_minLifetime, _maxLifetime);

    public float CurrentRadiusDestruction { get; private set; } = 20f;
    public float CurrentForceDestruction { get; private set; } = 300f;

    private Renderer _renderer;
    private Rigidbody _rigidbody;

    public event UnityAction Obliterating;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _rigidbody = GetComponent<Rigidbody>();

        _originalColor = _renderer.material.color;
    }

    private void Start()
    {
        _wait = new WaitForSeconds(Lifetime);
        StartCoroutine(Disappear());
    }

    private IEnumerator Disappear()
    {
        _renderer.material.DOFade(0, Lifetime);

        yield return _wait;

        Die();
    }

    public void AddExplosion(float force, Vector3 position, float radius) =>
        _rigidbody.AddExplosionForce(force, position, radius);

    private void Die()
    {
        Obliterating?.Invoke();
        gameObject.SetActive(false);
    }
}
