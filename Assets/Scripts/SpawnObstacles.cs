using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SpawnObstacle : MonoBehaviour
{
    public Transform[] spawnPoints;
    //public MoveObstacles[] obstaclePrefabs;
    public GameObject[] obstaclePrefabs;
    public GameObject[] powerupPrefabs;
    private float powerupWait;
    private float waitTime;
    private bool powerupSpawned;
    private bool spawned;
    private MoveObstacles movement;
    //private int obstacleNum;
    private AltShotPowerup lifeTimerShot;
    private HealPowerup lifeTimerHeal;
    private ImmunityPowerup lifeTimerImmune;

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
        waitTime = 3.0f;
    }

    private void spawnPowerup() {
        int randomNum = Random.Range(1,7);
        float randomX = Random.Range(-8.35f, 8.35f);
        float randomY = Random.Range(-4.5f, 3f);
        if(randomNum == 1) {
            GameObject chosenPowerup = powerupPrefabs[0];
            chosenPowerup = Instantiate(chosenPowerup, new Vector2(randomX, randomY), Quaternion.identity);
            lifeTimerShot = chosenPowerup.GetComponent<AltShotPowerup>();
            lifeTimerShot.setTimer();
        }
        if(randomNum == 2 || randomNum == 3) {
            GameObject chosenPowerup = powerupPrefabs[1];
            chosenPowerup = Instantiate(chosenPowerup, new Vector2(randomX, randomY), Quaternion.identity);
            lifeTimerHeal = chosenPowerup.GetComponent<HealPowerup>();
            lifeTimerHeal.setTimer();
        }
        if(randomNum == 4) {
            GameObject chosenPowerup = powerupPrefabs[2];
            chosenPowerup = Instantiate(chosenPowerup, new Vector2(randomX, randomY), Quaternion.identity);
            lifeTimerImmune = chosenPowerup.GetComponent<ImmunityPowerup>();
            lifeTimerImmune.setTimer();
        }
        powerupSpawned = true;
        powerupWait = Random.Range(6.0f, 12.0f);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spawned = false;
        waitTime = 0.0f;
        powerupSpawned = true;
        powerupWait = 10.0f;
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

        powerupWait = powerupWait - Time.deltaTime;
        if (powerupWait < 0) {
            powerupSpawned = false;
        }
        if (powerupSpawned == false) {
            spawnPowerup();
        }
    }
}
