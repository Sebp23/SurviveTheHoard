using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyObject;
    [SerializeField]
    private int numberOfEnemies = 1;

    [SerializeField]
    private GameObject[] spawnPositions;
    
    private float startDelay = 2.5f;
    private float spawnDelay = 2f;

    private CollisionTracker collisionTrackerScript;

    // Start is called before the first frame update
    void Start()
    {
        //get the CollisionTracker script
        collisionTrackerScript = GameObject.Find("Player").GetComponent<CollisionTracker>();

        InvokeRepeating("SpawnEnemies", startDelay, spawnDelay);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SpawnEnemies()
    {
        if (collisionTrackerScript.gameOver == false)
        {
            for(int i = 0; i < numberOfEnemies; i++)
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
}
