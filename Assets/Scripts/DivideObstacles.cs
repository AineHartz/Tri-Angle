using UnityEngine;

public class DivideObstacles : MonoBehaviour
{
    public GameObject obstaclePrefab;
    public int splitCount = 0;
    public int maxSplits = 3;

    public int baseHP = 12;
    public int currentHP = 12;
    public float minSpawnX;
    public float maxSpawnX;
    public float minSpawnY;
    public float maxSpawnY;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHP = baseHP; 
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("currentHP" + currentHP);
    }

    void OnTriggerEnter2D(Collider2D other){
        if (other.CompareTag("Projectile")){
             Destroy(other.gameObject);
             Destroy(gameObject);

            if (splitCount < maxSplits){
                SpawnNewObstacles();
            }
            
             
            

            
            // Debug.Log("baseHP" + baseHP);
        }
    }

    void SpawnNewObstacles(){
        for (int i = 0; i < 2; i++){
            Vector3 spawnPosition = transform.position + new Vector3(Random.Range(minSpawnX, maxSpawnX), Random.Range(minSpawnY, maxSpawnY), 0);
            GameObject newObstacle = Instantiate(obstaclePrefab, spawnPosition, Quaternion.identity);
            newObstacle.GetComponent<DivideObstacles>().splitCount = this.splitCount + 1; 
            newObstacle.GetComponent<DivideObstacles>().currentHP = currentHP/2;

            
        }
    }

    
}
