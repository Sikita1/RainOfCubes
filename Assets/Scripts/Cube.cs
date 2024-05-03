using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class Cube : MonoBehaviour
{
    private float _minLifetime = 2f;
    private float _maxLifetime = 5f;

    private Renderer _renderer;

    private Color _defaultColor;
    private Coroutine _coroutine;

    private float _lifetime => Random.Range(_minLifetime, _maxLifetime);

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _defaultColor = Color.white;
    }

    public void Initialize()
    {
        SetColor(CreateRandomColor());

        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(Disappear());
    }

    public void SetDefaultColor() =>
        SetColor(_defaultColor);

    private void Die() =>
        gameObject.SetActive(false);

    private IEnumerator Disappear()
    {
        yield return new WaitForSeconds(_lifetime);

        Die();
    }

    private Color CreateRandomColor() =>
        new Color(Random.value, Random.value, Random.value);

    private Color SetColor(Color color) =>
        _renderer.material.color = color;
}
