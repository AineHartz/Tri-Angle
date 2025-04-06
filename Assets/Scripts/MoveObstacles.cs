using UnityEngine;

public class MoveObstacles : MonoBehaviour
{
    public GameObject obstaclePrefab;
    private Vector2 currentPosition; 
    public float obstacleSpeed = 5.0f; 
    private Rigidbody2D body; 
    public int spawnPoint = 0;
    private Vector2 targetPosition;
 
    public void setSpawn(int spawnNum) { 
        spawnPoint = spawnNum + 1; 
    } 
    // Start is called once before the first execution of Update after the MonoBehaviour is created 
    void Awake()
    { 
        body = gameObject.GetComponent<Rigidbody2D>(); 
        currentPosition = body.position;
        setTarget(spawnPoint);
    } 
 
    // Update is called once per frame 
    void Update() 
    { 
         obstacleMovement();
    }

    //x max = 18
    //x min = -17.5
    //y max = 8
    //y min = -7.5
    private void setTarget(int spawnNum) {
        float randomY = 0.0f;
        float randomX = 0.0f;
        if(spawnNum == 1) {
            randomX = Random.Range(-12.0f, 18.0f);
            if(randomX == 18.0) {
                randomY = Random.Range(-7.5f, 3.0f);
            } else {
                randomY = -7.5f;
            }
            targetPosition = new Vector2(randomX, randomY);

        }
        if(spawnNum == 2) {
            randomX = Random.Range(-17.5f, 11.5f);
            if(randomX == -17.5) {
                randomY = Random.Range(-7.5f, 3.0f);
            } else {
                randomY = -7.5f;
            }
            targetPosition = new Vector2(randomX, randomY);

        }
        if(spawnNum == 3) {
            randomX = Random.Range(-17.5f, 7.5f);
            randomY = Random.Range(-7.5f, 8.0f);
            Vector2 targetPosition = new Vector2(randomX, randomY);

        }
        if(spawnNum == 4) {
            randomX = Random.Range(-17.5f, 11.5f);
            if(randomX == -17.5) {
                randomY = Random.Range(-3.0f, 8.0f);
            } else {
                randomY = 8.0f;
            }
            Vector2 targetPosition = new Vector2(randomX, randomY);

        }
        if(spawnNum == 5) {
            randomX = Random.Range(-12.0f, 18.0f);
            if(randomX == 18.0) {
                randomY = Random.Range(-3.0f, 8.0f);
            } else {
                randomY = 8.0f;
            }
            Vector2 targetPosition = new Vector2(randomX, randomY);

        }
        if(spawnNum == 6) {
            randomX = Random.Range(-7.0f, 18.0f);
            randomY = Random.Range(-7.5f, 8.0f);
            Vector2 targetPosition = new Vector2(randomX, randomY);

        }
    }
 

    private void obstacleMovement() {
        Vector2 newPosition = Vector2.MoveTowards(currentPosition, targetPosition, obstacleSpeed);
        body.MovePosition(newPosition);
        Debug.Log(newPosition);
    } 
}