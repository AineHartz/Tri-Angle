using UnityEngine;

public class ImmunityPowerup : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag=="Player")
        {

            collision.gameObject.GetComponent<SpaceshipController>().setImmune();
            Destroy(gameObject);
        }
    }
}
