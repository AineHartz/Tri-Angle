using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SpawnObstacle : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject[] obstaclePrefabs;
    private float waitTime;
    private bool spawned;
    //private int obstacleNum;

    private void spawnObstacle() {
        int randomSpawn = Random.Range(0, spawnPoints.Length);
        int randomObstacle = Random.Range(0, obstaclePrefabs.Length);
        GameObject chosenObstacle = obstaclePrefabs[randomObstacle];
        //chosenObstacle = Instantiate(chosenObstacle, randomSpawn.position, randomSpawn.rotation) as GameObject;
        spawned = true;
        waitTime = 7.5f;
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
