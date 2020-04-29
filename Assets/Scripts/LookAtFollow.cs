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
        collisionTrackerScript = GameObject.Find("Player").GetComponent<CollisionTracker>();
        increaseSpeedScript = GameObject.Find("SpawnManager").GetComponent<IncreaseSpeed>();

        enemyTarget = GameObject.Find("Player").transform;

    }

    // Update is called once per frame
    void Update()
    {
        if(collisionTrackerScript.gameOver == false)
        {
            transform.LookAt(enemyTarget.position);
            transform.Translate(0.0f, 0.0f, increaseSpeedScript.speed * Time.deltaTime);
        }
        
    }
}
