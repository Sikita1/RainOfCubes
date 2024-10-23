using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        collision.gameObject.GetComponent<Cube>()?.ChangedColor();
    }
}