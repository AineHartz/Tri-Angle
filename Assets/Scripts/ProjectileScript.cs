using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 10f;
    private int damage = 4; 

    void Update()
    {
        transform.position += transform.up * speed * Time.deltaTime;
    }

    //When obstacles get hit, they can use this getter to see how hurt they are.
    public int  getDamage()
    {
        return damage;
    }
}
