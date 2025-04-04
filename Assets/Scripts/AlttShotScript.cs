using UnityEngine;

public class AlttShotScript : MonoBehaviour
{
    private float speed;
    //how long the projectile should exist for
    public float lifetime = 1.5f;

    private float damage = 1f;

    void Start()
    {
        //projectile destroys itself after a bit to stop it from existing forever on miss
        Destroy(gameObject, lifetime);
        speed = Random.Range(8f, 12f);
    }

    void Update()
    {
        transform.position += transform.up * speed * Time.deltaTime;
    }

    //When obstacles get hit, they can use this getter to see how hurt they are.
    float getDamage()
    {
        return damage;
    }
}
