using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtFollow : MonoBehaviour
{
    [SerializeField]
    private Transform enemyTarget;
    public bool touchingGround = false; 

    private CollisionTracker collisionTrackerScript;
    private IncreaseSpeed increaseSpeedScript;
    private PlayerController playerControlScript;
    private Rigidbody enemyRB;

    // Start is called before the first frame update
    void Start()
    {
        //get necessary scripts
        collisionTrackerScript = GameObject.Find("Player").GetComponent<CollisionTracker>();
        playerControlScript = GameObject.Find("Player").GetComponent<PlayerController>();
        increaseSpeedScript = GameObject.Find("SpawnManager").GetComponent<IncreaseSpeed>();

        enemyRB = gameObject.GetComponent<Rigidbody>();


        //get the target to follow
        enemyTarget = GameObject.Find("EnemyLookAt").transform;

        //stops enemies from flying away on spawn
        if (gameObject.transform.position.y != enemyTarget.transform.position.y)
        {
            enemyRB.velocity = Vector3.zero;
            //gameObject.transform.position = new Vector3(gameObject.transform.position.x, enemyTarget.transform.position.y, gameObject.transform.position.z);
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, 0.5f, gameObject.transform.position.z);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(collisionTrackerScript.hasPowerup == false && collisionTrackerScript.gameOver == false)
        {
            //make sure the enemy doesnt fly away unless the powerup is on. helps prevent enemies from flying away on spawn
            if (gameObject.transform.position.y != enemyTarget.transform.position.y)
            {
                enemyRB.velocity = Vector3.zero;
                //gameObject.transform.position = new Vector3(gameObject.transform.position.x, enemyTarget.transform.position.y, gameObject.transform.position.z);
                gameObject.transform.position = new Vector3(gameObject.transform.position.x, 0.5f, gameObject.transform.position.z);
            }
            //check if enemy is outside the bounds, destroy if they are, and the powerup is not on. This also helps optimization and prevents enemies from z00ming
            if (gameObject.transform.position.z < -playerControlScript.zRange)
            {
                Destroy(gameObject);
            }
            if (gameObject.transform.position.z > playerControlScript.zRange)
            {
                Destroy(gameObject);
            }
            if (gameObject.transform.position.x < -playerControlScript.xRange)
            {
                Destroy(gameObject);
            }
            if (gameObject.transform.position.x > playerControlScript.xRange)
            {
                Destroy(gameObject);
            }
        }

        //look at the position of the player (enemyTarget)
        transform.LookAt(enemyTarget.position);
        //move the object towards enemyTarget
        transform.Translate(0.0f, 0.0f, increaseSpeedScript.speed * Time.deltaTime);
    }
}