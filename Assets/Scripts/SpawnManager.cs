using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyObject;
    [SerializeField]
    private GameObject powerupObject;
    [SerializeField]
    private int numberOfEnemies = 1;
    [SerializeField]
    private int numberOfPowerups = 1;

    [SerializeField]
    private GameObject[] spawnPositions;

    private float enemyStartDelay = 2.5f;
    private float enemySpawnDelay = 2f;

    private float powerupStartDelay = 5f;
    private float powerupSpawnDelay = 15f;

    private float enemyNumberStartDelay = 5f;
    private float enemyNumberSpawnDelay = 10f;

    private CollisionTracker collisionTrackerScript;
    private PlayerController playerControlScript;

    // Start is called before the first frame update
    void Start()
    {
        //get player controller script
        playerControlScript = GameObject.Find("Player").GetComponent<PlayerController>();
        //get the CollisionTracker script
        collisionTrackerScript = GameObject.Find("Player").GetComponent<CollisionTracker>();

        //invoke the SpawnEnemies, SpawnPowerups, and IncreaseEnemySpawnNumber repeatedly with their respective repeat times.
        InvokeRepeating("SpawnEnemies", enemyStartDelay, enemySpawnDelay);
        InvokeRepeating("SpawnPowerups", powerupStartDelay, powerupSpawnDelay);
        InvokeRepeating("IncreaseEnemySpawnNumber", enemyNumberStartDelay, enemyNumberSpawnDelay);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SpawnEnemies()
    {
        //make sure the game is not over
        if (collisionTrackerScript.gameOver == false)
        {
            for (int i = 0; i < numberOfEnemies; i++)
            {
                //determine the x, y, and z positions for the spawn of the enemy, using the spawnPositions array
                int spawnIndex = Random.Range(0, spawnPositions.Length);
                float spawnPosX = spawnPositions[spawnIndex].transform.position.x;
                float spawnPosZ = spawnPositions[spawnIndex].transform.position.z;
                float spawnPosY = spawnPositions[spawnIndex].transform.position.y;

                //define spawn position
                Vector3 spawnPosition = new Vector3(spawnPosX, spawnPosY, spawnPosZ);

                //spawn the obstacle
                Instantiate(enemyObject, spawnPosition, enemyObject.transform.rotation);
            }

        }
    }

    private void SpawnPowerups()
    {
        //make sure game is not over
        if (collisionTrackerScript.gameOver == false)
        {
            for (int i = 0; i < numberOfPowerups; i++)
            {
                //Determine the x, y, and z, positions for the spawn of powerup
                float spawnPosX = Random.Range(-playerControlScript.xRange, playerControlScript.xRange);
                float spawnPosY = GameObject.Find("Player").transform.position.y;
                float spawnPosZ = Random.Range(-playerControlScript.zRange, playerControlScript.zRange);

                //define spawn position
                Vector3 spawnPosition = new Vector3(spawnPosX, spawnPosY, spawnPosZ);

                //spawn the powerup
                Instantiate(powerupObject, spawnPosition, powerupObject.transform.rotation);
            }

        }
    }

    private void IncreaseEnemySpawnNumber()
    {
        //increase the amount of enemies to be spawned
        numberOfEnemies++;
    }
}
