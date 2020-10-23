using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float verticalInput;
    public float horizontalInput;
    public float zRange;
    public float xRange;

    public static float playerSpeed = 10.0f;

    public Color playerColor;
    public Renderer playerRenderer;

    private CollisionTracker collisionTrackerScript;
    private Animator playerAnimation;

    // Start is called before the first frame update
    void Start()
    {
        //get the z-axis boundary position from the PlayerZBoundary object
        zRange = GameObject.Find("ZBoundary").transform.position.z;

        //get the x-axis boundary position from the PlayerXBoundary object
        xRange = GameObject.Find("XBoundary").transform.position.x;

        //get the CollisionTracker script
        collisionTrackerScript = GameObject.Find("Player").GetComponent<CollisionTracker>();

        //get the animator for the player
        playerAnimation = GetComponent<Animator>();

        //get the original color of player object
        playerColor = GameObject.Find("Player").GetComponent<Renderer>().material.color;

        //get the rendering component of player object, so we can change color with powerups
        playerRenderer = GameObject.Find("Player").GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (collisionTrackerScript.gameOver == false)
        {
            //check to make sure player stays inbounds
            if (transform.position.z < -zRange)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, -zRange);
            }

            if (transform.position.z > zRange)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, zRange);
            }

            if (transform.position.x < -xRange)
            {
                transform.position = new Vector3(-xRange, transform.position.y, transform.position.z);
            }

            if (transform.position.x > xRange)
            {
                transform.position = new Vector3(xRange, transform.position.y, transform.position.z);
            }

            //assign control to horizontalInput to move player up/down
            verticalInput = Input.GetAxis("Vertical");
            //assign control to the horizontalInput to move player left/right
            horizontalInput = Input.GetAxis("Horizontal");

            //move player up/down
            transform.Translate(Vector3.forward * verticalInput * Time.deltaTime * playerSpeed, Space.World);
            //move player left/right
            transform.Translate(Vector3.right * horizontalInput * Time.deltaTime * playerSpeed, Space.World);

            if (horizontalInput >= 0 || horizontalInput <= 0 || verticalInput >= 0 || verticalInput <= 0)
            {
                playerAnimation.SetBool("isMoving", true);
            }
            if (horizontalInput == 0 && verticalInput == 0)
            {
                playerAnimation.SetBool("isMoving", false);
            }

        }

    }
}