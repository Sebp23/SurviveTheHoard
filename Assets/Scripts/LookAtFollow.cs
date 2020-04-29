using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtFollow : MonoBehaviour
{
    [SerializeField]
    private Transform enemyTarget;
    private float enemySpeed = 5.0f;

    private CollisionTracker collisionTrackerScript;

    // Start is called before the first frame update
    void Start()
    {
        collisionTrackerScript = GameObject.Find("Player").GetComponent<CollisionTracker>();
    }

    // Update is called once per frame
    void Update()
    {
        if(collisionTrackerScript.gameOver == false)
        {
            transform.LookAt(enemyTarget.position);
            transform.Translate(0.0f, 0.0f, enemySpeed * Time.deltaTime);
        }
        
    }
}
