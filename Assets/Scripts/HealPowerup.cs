using UnityEngine;

public class HealPowerup : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
        {
            if(collision.gameObject.tag=="Player")
            {

                collision.gameObject.GetComponent<SpaceshipController>().heal();
            }
        }
}
