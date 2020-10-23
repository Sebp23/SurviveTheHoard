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
    private GameObject player;
    [SerializeField]
    private int numberOfEnemies = 1;
    [SerializeField]
    private int numberOfBigEnemies = 1;
    [SerializeField]
    private int numberOfPowerups = 1;

    [SerializeField]
    private GameObject[] spawnPositions;

    private float normalEnemyStartDelay = 2.5f;
    private float normalEnemySpawnDelay = 2f;

    private float bigEnemyStartDelay = 15f;
    private float bigEnemySpawnDelay = 12.5f;

    private float powerupStartDelay = 5f;
    private float powerupSpawnDelay = 15f;

    private float enemyNumberStartDelay = 5f;
    private float enemyNumberSpawnDelay = 10f;

    private Vector3 playerPosition;

    private CollisionTracker collisionTrackerScript;
    private PlayerController playerControlScript;
    private Collider enemyCollider;

    // Start is called before the first frame update
    void Start()
    {
        //get player controller script
        playerControlScript = GameObject.Find("Player").GetComponent<PlayerController>();

        //get the player object
        player = GameObject.Find("Player");

        //get the CollisionTracker script
        collisionTrackerScript = GameObject.Find("Player").GetComponent<CollisionTracker>();

        //invoke the SpawnEnemies, SpawnPowerups, and IncreaseEnemySpawnNumber repeatedly with their respective repeat times.
        InvokeRepeating("SpawnNormalEnemies", normalEnemyStartDelay, normalEnemySpawnDelay);
        InvokeRepeating("SpawnBigEnemies", bigEnemyStartDelay, bigEnemySpawnDelay);
        InvokeRepeating("SpawnPowerups", powerupStartDelay, powerupSpawnDelay);
        InvokeRepeating("IncreaseEnemySpawnNumber", enemyNumberStartDelay, enemyNumberSpawnDelay);
    }

    // Update is called once per frame
    void Update()
    {
        playerPosition = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z);
    }

    private void SpawnNormalEnemies()
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

                float distance = Vector3.Distance(playerPosition, spawnPosition);
                Debug.Log(spawnPosition + ":" + distance);

                if(distance >= 7)
                {
                    //spawn the obstacle
                    Instantiate(enemyObject, spawnPosition, enemyObject.transform.rotation);
                }
            }

        }
    }

    private void SpawnBigEnemies()
    {
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

                float distance = Vector3.Distance(playerPosition, spawnPosition);
                Debug.Log(spawnPosition + ":" + distance);

                if (distance >= 7)
                {
                    //spawn the obstacle
                    Instantiate(enemyObject, spawnPosition, enemyObject.transform.rotation);
                }
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

    private void IncreaseBigEnemySpawnNumber()
    {
        //increase the amount of enemies to be spawned
        numberOfEnemies++;
    }
}