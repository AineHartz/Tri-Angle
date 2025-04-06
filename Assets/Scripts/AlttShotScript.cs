using UnityEngine;

public class AlttShotScript : MonoBehaviour
{
    private float speed;
    private float damage = 1f;

    void Start()
    {
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
