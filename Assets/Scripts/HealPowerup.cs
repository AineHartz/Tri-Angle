using UnityEngine;

public class HealPowerup : MonoBehaviour
{
    public float lifeTimer;

    private void OnTriggerEnter2D(UnityEngine.Collider2D collision)
    {
        if(collision.gameObject.tag=="Player")
        {
            collision.gameObject.GetComponent<SpaceshipController>().heal();
            Destroy(gameObject);
        }
    }

    public void setTimer() {
        lifeTimer = 7.5f;
    }

    void Update() {
        lifeTimer = lifeTimer - Time.deltaTime;
        if(lifeTimer < 0) {
            Destroy(gameObject);
        }
    }
}
