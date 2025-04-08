using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public class DivideObstacles : MonoBehaviour
{
    public GameObject obstaclePrefab;
    // public GameObject healthBar;
    // public Sprite[] healthImages;
    // public SpriteRenderer spriteRenderer;
    // public int splitCount = 0;
    // public int maxSplits = 2;

    public int baseHP = 12;

    public int currentHP;
    public float minSpawnX;
    public float maxSpawnX;
    public float minSpawnY;
    public float maxSpawnY;

    public int asteroidSize;

    public GameObject middleAsteroid;
    public GameObject smallAsteroid;
    private bool canBeHit;

    public int baseScore = 300;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(WaitALittle());
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Projectile")){
            // if(canBeHit == false){
            //     return;
            // }
            // currentHP = baseHP/2;

            currentHP -= other.gameObject.GetComponent<RealProjectile>().getDamage();

            Destroy(other.gameObject);
            
            if(currentHP <= 0)
            {
                if(asteroidSize == 3)
                {
                    GameObject asteroid1 = Instantiate(middleAsteroid, transform.position +  new Vector3(Random.Range(minSpawnX, maxSpawnX), Random.Range(minSpawnY, maxSpawnY), 0), transform.rotation);
                    GameObject asteroid2 = Instantiate(middleAsteroid, transform.position, transform.rotation);

                    asteroid1.GetComponent<DivideObstacles>().currentHP /= 2;
                    asteroid2.GetComponent<DivideObstacles>().currentHP /= 2;
                    asteroid1.GetComponent<DivideObstacles>().baseScore -= 100;
                    asteroid2.GetComponent<DivideObstacles>().baseScore -= 100;
                }

                else if(asteroidSize == 2)
                {
                    GameObject asteroid1 = Instantiate(smallAsteroid, transform.position  + new Vector3(Random.Range(minSpawnX, maxSpawnX), Random.Range(minSpawnY, maxSpawnY), 0), transform.rotation);
                    GameObject asteroid2 = Instantiate(smallAsteroid, transform.position, transform.rotation);

                    asteroid1.GetComponent<DivideObstacles>().currentHP /= 4;
                    asteroid2.GetComponent<DivideObstacles>().currentHP /= 4;
                    asteroid1.GetComponent<DivideObstacles>().baseScore -= 100;
                    asteroid2.GetComponent<DivideObstacles>().baseScore -= 100;
                }

                else if(asteroidSize == 1)
                {
            
                }

                GameManager.addScore(baseScore);
                Destroy(gameObject);

            }

            
        }
    }

    IEnumerator WaitALittle()
{
    yield return new WaitForSeconds(0.1f);
    canBeHit = true;
}


    // //make collider wait a frame so it doesn't instantiate small asteroid immediately after a medium asteroid is spawned
    // IEnumerator DisableColliderForAFrame()
    // {
    //     Collider2D col = GetComponent<Collider2D>();
    //     if (col != null)
    //     {
    //         col.enabled = false;
    //         yield return null; 
    //         col.enabled = true;
    //     }
    // }

}


