using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseSpeed : MonoBehaviour
{
    public float speed = 3.0f;
    public float addedSpeed;
    private float speedIncreaseStartDelay = 10f;
    private float speedIncreaseInterval = 10f;

    private CollisionTracker collisionTrackerScript;
    // Start is called before the first frame update
    void Start()
    {
        //get the CollisionTracker script
        collisionTrackerScript = GameObject.Find("Player").GetComponent<CollisionTracker>();

        //execute the method SpeedIncrease() after 10 seconds, then repeat it every 10 seconds
        InvokeRepeating("SpeedIncrease", speedIncreaseStartDelay, speedIncreaseInterval);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SpeedIncrease()
    {
        if(collisionTrackerScript.gameOver == false)
        {
            //determine how much speed will be added to the total speed
            //addedSpeed = 2f;
            //add 5 to the total speed
            speed += addedSpeed;

            Debug.Log($"Speed increased: {speed}");
        }
    }
}
