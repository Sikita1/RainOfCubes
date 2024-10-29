using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

[RequireComponent(typeof(Renderer))]
[RequireComponent(typeof(Rigidbody))]
public class Bomb : MonoBehaviour
{
    [SerializeField] private Color _defaultColor;

    private float _minLifetime = 2f;
    private float _maxLifetime = 5f;

    private WaitForSeconds _wait;
    private Renderer _renderer;
    private Rigidbody _rigidbody;

    private float Lifetime =>
        Random.Range(_minLifetime, _maxLifetime);

    public float RadiusDestruction { get; private set; } = 20f;
    public float ForceDestruction { get; private set; } = 300f;

    public event UnityAction Obliterating;
    public event UnityAction<Bomb> Dead;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _rigidbody = GetComponent<Rigidbody>();

        _wait = new WaitForSeconds(Lifetime);
    }

    public void SetDefaultColor() =>
        _renderer.material.color = _defaultColor;

    public void StartLifeTime() =>
        StartCoroutine(Disappear());

    public void AddExplosion(Vector3 position) =>
        _rigidbody.AddExplosionForce(ForceDestruction, position, RadiusDestruction);

    private IEnumerator Disappear()
    {
        _renderer.material.DOFade(0, Lifetime);

        yield return _wait;

        Die();
    }

    private void Die()
    {
        Obliterating?.Invoke();
        Dead?.Invoke(this);
        gameObject.SetActive(false);
    }
}
