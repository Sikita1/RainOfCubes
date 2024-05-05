using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class Cube : MonoBehaviour
{
    private float _minLifetime = 2f;
    private float _maxLifetime = 5f;
    private bool _isChangedColor;

    private Renderer _renderer;

    private Color _defaultColor;
    private Coroutine _coroutine;

    private float _lifetime => Random.Range(_minLifetime, _maxLifetime);

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _defaultColor = Color.white;
    }

    private void Start()
    {
        _isChangedColor = false;
    }

    public void StartLifeCycle()
    {
        if (_isChangedColor == false)
            SetColor(CreateRandomColor());

        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(Disappear());
    }

    public void SetDefaultColor() =>
        SetColor(_defaultColor);

    public void AuthorizeColorChange() =>
        _isChangedColor = false;

    private void Die() =>
        gameObject.SetActive(false);

    private IEnumerator Disappear()
    {
        yield return new WaitForSeconds(_lifetime);

        Die();
    }

    private Color CreateRandomColor() =>
        new Color(Random.value, Random.value, Random.value);

    private void SetColor(Color color)
    {
        _isChangedColor = true;
        _renderer.material.color = color;
    }
}
