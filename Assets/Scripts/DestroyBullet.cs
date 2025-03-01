using UnityEngine;

public class DestroyBullet : MonoBehaviour
{
    void OnBecameInvisible()
    {
        Destroy(gameObject);        
    }
}
