using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 10f;
    //how long the projectile should exist for
    public float lifetime = 1.5f;

    private float damage = 2f; 

    void Start()
    {
        //projectile destroys itself after a bit to stop it from existing forever on miss
        Destroy(gameObject, lifetime);
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
