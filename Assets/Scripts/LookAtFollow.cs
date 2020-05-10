using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtFollow : MonoBehaviour
{
    [SerializeField]
    private Transform enemyTarget;
    //[SerializeField]
    //private float enemySpeed = 3.0f;

    private CollisionTracker collisionTrackerScript;
    private IncreaseSpeed increaseSpeedScript;

    // Start is called before the first frame update
    void Start()
    {
        //get necessary scripts
        collisionTrackerScript = GameObject.Find("Player").GetComponent<CollisionTracker>();
        increaseSpeedScript = GameObject.Find("SpawnManager").GetComponent<IncreaseSpeed>();

        //get the target to follow
        enemyTarget = GameObject.Find("Player").transform;

    }

    // Update is called once per frame
    void Update()
    {
        //look at the position of the player (enemyTarget)
        transform.LookAt(enemyTarget.position);
        //move the object towards enemyTarget
        transform.Translate(0.0f, 0.0f, increaseSpeedScript.speed * Time.deltaTime);
    }
}
