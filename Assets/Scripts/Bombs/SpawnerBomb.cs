using UnityEngine;

public class SpawnerBomb : Spawner<Bomb>
{
    public void CreateObject(Vector3 position)
    {
        if (TryGetObject(out Bomb bomb))
        {
            SetBomb(bomb, position);
            ObjectCreated();
            ObjectActivated();
        }
    }

    private void SetBomb(Bomb bomb, Vector3 position)
    {
        bomb.transform.position = position;
        bomb.gameObject.SetActive(true);
        bomb.SetDefaultColor();
        bomb.StartLifeTime();
        bomb.Dead += OnDead;
    }

    private void OnDead(Bomb bomb)
    {
        ObjectDeactivated();
        bomb.Dead -= OnDead;
    }
}
