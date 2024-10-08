using System.Collections;
using UnityEngine;

public class SpawnerCube : ObjectPool
{
    [SerializeField] private Cube _prefab;

    [SerializeField] private Transform _leftZone;
    [SerializeField] private Transform _rightZone;
    [SerializeField] private Transform _upZone;
    [SerializeField] private Transform _downZone;

    private float _delay = 1f;

    private Coroutine _coroutine;

    private bool _isOpen = true;

    private void Start()
    {
        Initialize(_prefab);
        _coroutine = StartCoroutine(CreateObject());
    }

    private IEnumerator CreateObject()
    {
        WaitForSeconds wait = new WaitForSeconds(_delay);

        while (_isOpen)
        {
            if (TryGetObject(out Cube cube))
                SetCube(cube);

            yield return wait;
        }
    }

    private void SetCube(Cube cube)
    {
        cube.SetDefaultColor();
        cube.gameObject.SetActive(true);
        cube.transform.position = GetRandomPosition();
        cube.AuthorizeColorChange();
        cube.StartLifeCircle();
    }

    private Vector3 GetRandomPosition()
    {
        float positionX = Random.Range(_leftZone.position.x, _rightZone.position.x);
        float positionZ = Random.Range(_upZone.position.z, _downZone.position.z);

        return new Vector3(positionX, _leftZone.position.y, positionZ);
    }
}
