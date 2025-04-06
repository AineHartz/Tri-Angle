using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 10f;
    private float damage = 4f; 

    void Update()
    {
        transform.position += transform.up * speed * Time.deltaTime;
    }

    //When obstacles get hit, they can use this getter to see how hurt they are.
    float  getDamage()
    {
        return damage;
    }
}
