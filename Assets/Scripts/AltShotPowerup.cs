using UnityEngine;

public class AltShotPowerup : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag=="Player")
        {
            //colision.ShipController.setAlt();
            collision.gameObject.GetComponent<SpaceshipController>().setAlt();
            Destroy(gameObject);
        }
    }
}
