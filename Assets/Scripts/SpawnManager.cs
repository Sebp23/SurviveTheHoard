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
        if (collisionTrackerScript.gameOver == false)
        {
            for (int i = 0; i < numberOfEnemies; i++)
            {
                //determine which obstacle prefab will spawn
                int spawnIndex = Random.Range(0, spawnPositions.Length);
                float spawnPosX = spawnPositions[spawnIndex].transform.position.x;
                float spawnPosZ = spawnPositions[spawnIndex].transform.position.z;
                float spawnPosY = spawnPositions[spawnIndex].transform.position.y;

                Vector3 spawnPosition = new Vector3(spawnPosX, spawnPosY, spawnPosZ);

                //spawn the obstacle
                Instantiate(enemyObject, spawnPosition, enemyObject.transform.rotation);
            }

        }
    }

    private void SpawnPowerups()
    {
        if (collisionTrackerScript.gameOver == false)
        {
            for (int i = 0; i < numberOfPowerups; i++)
            {
                //determine which obstacle prefab will spawn
                float spawnPosX = Random.Range(-playerControlScript.xRange, playerControlScript.xRange);
                float spawnPosY = GameObject.Find("Player").transform.position.y;
                float spawnPosZ = Random.Range(-playerControlScript.zRange, playerControlScript.zRange);

                Vector3 spawnPosition = new Vector3(spawnPosX, spawnPosY, spawnPosZ);

                //spawn the obstacle
                Instantiate(powerupObject, spawnPosition, powerupObject.transform.rotation);
            }

        }
    }

    private void IncreaseEnemySpawnNumber()
    {
        numberOfEnemies++;
    }
}
