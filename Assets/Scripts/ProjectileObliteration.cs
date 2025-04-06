using UnityEngine;

public class ProjectileObliteration : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Projectile")
        {
            Destroy(collision.gameObject);
        }
    }
}
