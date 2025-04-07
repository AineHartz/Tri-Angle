using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public class DivideObstacles : MonoBehaviour
{
    public GameObject obstaclePrefab;
    // public GameObject healthBar;
    // public Sprite[] healthImages;
    // public SpriteRenderer spriteRenderer;
    public int splitCount = 0;
    public int maxSplits = 2;

    public int baseHP = 12;
    public int currentHP;
    public float minSpawnX;
    public float maxSpawnX;
    public float minSpawnY;
    public float maxSpawnY;

    public int asteroidSize;

    public GameObject middleAsteroid;
    public GameObject smallAsteroid;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(DisableColliderForAFrame());
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Projectile")){
             Destroy(other.gameObject);
            

            if(asteroidSize == 3){
                Instantiate(middleAsteroid, transform.position +  new Vector3(Random.Range(minSpawnX, maxSpawnX), Random.Range(minSpawnY, maxSpawnY), 0), transform.rotation);
                Instantiate(middleAsteroid, transform.position, transform.rotation);
            }

            else if(asteroidSize == 2){
                Instantiate(smallAsteroid, transform.position  + new Vector3(Random.Range(minSpawnX, maxSpawnX), Random.Range(minSpawnY, maxSpawnY), 0), transform.rotation);
                Instantiate(smallAsteroid, transform.position, transform.rotation);
            }

            else if(asteroidSize == 1){

            }
           
            Destroy(gameObject);
            

            
        }
    }


    //make collider wait a frame so it doesn't instantiate small asteroid immediately after a medium asteroid is spawned
    IEnumerator DisableColliderForAFrame()
    {
        Collider2D col = GetComponent<Collider2D>();
        if (col != null)
        {
            col.enabled = false;
            yield return null; 
            col.enabled = true;
        }
    }

}


