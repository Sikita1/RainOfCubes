using System.Collections;
using UnityEngine;

public class Spawner : ObjectPool
{
    [SerializeField] private Cube _prefab;

    [SerializeField] private float _secondBetweenSpawn;

    [SerializeField] private Transform _leftZone;
    [SerializeField] private Transform _rightZone;
    [SerializeField] private Transform _upZone;
    [SerializeField] private Transform _downZone;

    private float _elepsedTime = 0;
    private float _delay = 1f;

    private Coroutine _coroutine;

    private void Start()
    {
        Initialize(_prefab);
    }

    private void Update()
    {
        _elepsedTime += Time.deltaTime;
        _coroutine = StartCoroutine(CreateObject());
    }

    private IEnumerator CreateObject()
    {
        if (_elepsedTime >= _secondBetweenSpawn)
            if (TryGetObject(out Cube cube))
            {
                _elepsedTime = 0;
                SetCube(cube);
            }

        yield return new WaitForSeconds(_delay);
    }

    private void SetCube(Cube cube)
    {
        cube.SetDefaultColor();
        cube.gameObject.SetActive(true);
        cube.transform.position = GetRandomPosition();
    }

    private Vector3 GetRandomPosition()
    {
        float positionX = Random.Range(_leftZone.position.x, _rightZone.position.x);
        float positionZ = Random.Range(_upZone.position.z, _downZone.position.z);

        return new Vector3(positionX, _leftZone.position.y, positionZ);
    }

    private void OnCollisionEnter(Collision collision)
    {
        collision.gameObject.GetComponent<Cube>().Initialize();
    }
}