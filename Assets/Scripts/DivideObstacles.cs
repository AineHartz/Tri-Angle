using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public class DivideObstacles : MonoBehaviour
{
    public GameObject obstaclePrefab;
    // public GameObject healthBar;
    public Sprite[] healthImages;
    public SpriteRenderer spriteRenderer;
    public int splitCount = 0;
    public int maxSplits = 2;

    public int baseHP = 12;
    public int currentHP;
    public float minSpawnX;
    public float maxSpawnX;
    public float minSpawnY;
    public float maxSpawnY;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("currentHP" + currentHP);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Projectile")){
             Destroy(other.gameObject);
            

            if (splitCount < maxSplits - 1){
                SpawnNewObstacles();
            }
            
            Destroy(gameObject);
            

            
            // Debug.Log("baseHP" + baseHP);
        }
    }

    void SpawnNewObstacles(){
        for (int i = 0; i < 2; i++){
            
            Vector3 spawnPosition = transform.position + new Vector3(Random.Range(minSpawnX, maxSpawnX), Random.Range(minSpawnY, maxSpawnY), 0);
            GameObject newObstacle = Instantiate(obstaclePrefab, spawnPosition, Quaternion.identity);
            DivideObstacles newObstaclePrefab = newObstacle.GetComponent<DivideObstacles>();
            newObstaclePrefab.splitCount = this.splitCount + 1; 
            newObstaclePrefab.currentHP /= 2;
            getImage(newObstacle, newObstaclePrefab.currentHP);
           
        }
    }

    void getImage(GameObject targetObstacle, int currentHPP){
        
        // Image healthIcon = healthBar.GetComponent<Image>()
       SpriteRenderer healthIcon = targetObstacle.GetComponentInChildren<SpriteRenderer>();

    if (currentHP == 12)
    {
        spriteRenderer.sprite = healthImages[0];
    }
    else if (currentHP == 6)
    {
        spriteRenderer.sprite = healthImages[1];
    }
    else if (currentHP == 3)
    {
        spriteRenderer.sprite = healthImages[2];
    }

    spriteRenderer.transform.localScale = new Vector3(.2f, .2f, .2f);
    spriteRenderer.transform.position = new Vector3(0,1,0);
        }
    }


