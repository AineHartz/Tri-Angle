using UnityEngine;

public class DivideObstacles : MonoBehaviour
{
    public GameObject obstaclePrefab;
    public int splitCount = 0;
    public int maxSplits = 3;
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
        
    }

    void OnTriggerEnter2D(Collider2D other){
        if (other.CompareTag("Laser")){
            // Destroy(other.gameObject);

            if (splitCount < maxSplits){
                SpawnNewObstacles();
            }
            
            Destroy(gameObject); 
        }
    }

    void SpawnNewObstacles(){
        for (int i = 0; i < 2; i++){
            Vector3 spawnPosition = transform.position + new Vector3(Random.Range(minSpawnX, maxSpawnX), Random.Range(minSpawnY, maxSpawnY), 0);
            GameObject newObstacle = Instantiate(obstaclePrefab, spawnPosition, Quaternion.identity);
            newObstacle.GetComponent<DivideObstacles>().splitCount = this.splitCount + 1; 
        }
    }
}
