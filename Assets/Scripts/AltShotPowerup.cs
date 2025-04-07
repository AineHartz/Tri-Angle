using UnityEngine;

public class AltShotPowerup : MonoBehaviour
{
    public float lifeTimer;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag=="Player")
        {
            //colision.ShipController.setAlt();
            collision.gameObject.GetComponent<SpaceshipController>().setAlt();
            Destroy(gameObject);
        }
    }

    public void setTimer() {
        lifeTimer = 5.0f;
    }

    void Update() {
        lifeTimer = lifeTimer - Time.deltaTime;
        if(lifeTimer < 0) {
            Destroy(gameObject);
        }
    }
}
