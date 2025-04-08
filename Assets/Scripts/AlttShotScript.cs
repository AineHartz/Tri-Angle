using UnityEngine;

public class AlttShotScript : MonoBehaviour, RealProjectile
{
    private float speed;
    private int damage = 2;

    void Start()
    {
        speed = Random.Range(8f, 12f);
    }

    void Update()
    {
        transform.position += transform.up * speed * Time.deltaTime;
    }

    //When obstacles get hit, they can use this getter to see how hurt they are.
    public int getDamage()
    {
        return damage;
    }
}
