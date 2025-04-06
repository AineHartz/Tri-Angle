using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SpawnObstacle : MonoBehaviour
{
    public Transform[] spawnPoints;
    //public MoveObstacles[] obstaclePrefabs;
    public GameObject[] obstaclePrefabs;
    private float waitTime;
    private bool spawned;
    private MoveObstacles movement;
    //private int obstacleNum;

    private void spawnObstacle() {
        int randomSpawn = Random.Range(0, spawnPoints.Length);
        int randomObstacle = Random.Range(0, obstaclePrefabs.Length);
        //GameObject chosenObstacle = obstaclePrefabs[randomObstacle].obstaclePrefab;
        //obstaclePrefabs[randomObstacle].setSpawn(randomSpawn);
        GameObject chosenObstacle = obstaclePrefabs[randomObstacle];
        movement = chosenObstacle.GetComponent<MoveObstacles> ();
        movement.setSpawn(randomSpawn);
        chosenObstacle = Instantiate(chosenObstacle, spawnPoints[randomSpawn].position, spawnPoints[randomSpawn].rotation) as GameObject;

        spawned = true;
        waitTime = 4.0f;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spawned = false;
        waitTime = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        waitTime = waitTime - Time.deltaTime;
        if (waitTime < 0) {
            spawned = false;
        }
        if (spawned == false) {
            spawnObstacle();
        }
    }
}
