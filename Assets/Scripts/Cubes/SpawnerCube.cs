using System.Collections;
using UnityEngine;

public class SpawnerCube : Spawner<Cube>
{
    [SerializeField] private SpawnerBomb _bomb;
    [SerializeField] private Cube _cube;

    [SerializeField] private Transform _leftZone;
    [SerializeField] private Transform _rightZone;
    [SerializeField] private Transform _upZone;
    [SerializeField] private Transform _downZone;

    private WaitForSeconds _wait;

    private float _delay = 1f;

    private bool _isOpen = true;

    private void Awake()
    {
        _wait = new WaitForSeconds(_delay);
        StartCoroutine(CreateObject());
    }

    private void OnDied(Cube cube)
    {
        _bomb.CreateObject(cube.transform.position);
        ObjectDeactivated();
        cube.Died -= OnDied;
    }

    private IEnumerator CreateObject()
    {
        while (_isOpen)
        {
            if (TryGetObject(out Cube cube))
            {
                SetCube(cube);
                ObjectCreated();
                ObjectActivated();
            }

            yield return _wait;
        }
    }

    private void SetCube(Cube cube)
    {
        cube.SetDefaultColor();
        cube.gameObject.SetActive(true);
        cube.transform.position = GetRandomPosition();
        cube.AuthorizeColorChange();
        cube.StartLifeCircle();
        cube.Died += OnDied;
    }

    private Vector3 GetRandomPosition()
    {
        float positionX = Random.Range(_leftZone.position.x, _rightZone.position.x);
        float positionZ = Random.Range(_upZone.position.z, _downZone.position.z);

        return new Vector3(positionX, _leftZone.position.y, positionZ);
    }
}
