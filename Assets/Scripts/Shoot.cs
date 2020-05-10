using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shoot : MonoBehaviour
{
    [SerializeField]
    private GameObject bullet;

    [SerializeField]
    private Camera mainCamera;

    public float bulletsRemaining;
    public bool reloading = false;

    private Rigidbody playerRB;
    private CollisionTracker collisionTrackerScript;


    // Start is called before the first frame update
    void Start()
    {
        //get main camera component
        mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();

        //get players rigidbody
        playerRB = GameObject.Find("Player").GetComponent<Rigidbody>();

        //get CollisionTracker script
        collisionTrackerScript = GameObject.Find("Player").GetComponent<CollisionTracker>();
    }

    // Update is called once per frame
    void Update()
    {
        //CREDIT TO CHRIS REARDON FOR HELPING WITH BELOW CODE

        //A Ray (line) named CameraRay is cast from camera to where mouse is
        Ray cameraRay = mainCamera.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);

        //Float initalized for the following if loop
        float rayLength;


        if (collisionTrackerScript.gameOver == false)
        {
            //Sets rayLength to however long the ray is from cameraRay to groundPlane
            if (groundPlane.Raycast(cameraRay, out rayLength))
            {
                // Gets a vector3 pos where the RayLength crosses (gives the orientation for the player)
                Vector3 pointToLook = cameraRay.GetPoint(rayLength);

                //Debug to see casted ray 
                Debug.DrawLine(cameraRay.origin, pointToLook, Color.red);

                //Faces the point
                transform.LookAt(new Vector3(pointToLook.x, transform.position.y, pointToLook.z));

                //check if there are bullets left
                if (bulletsRemaining > 0)
                {
                    //if left mouse is clicked
                    if (Input.GetMouseButtonDown(0))
                    {

                        //set the player as kinematic, so they dont fly back
                        playerRB.isKinematic = true;

                        //spawn bullet
                        GameObject bulletFired = Instantiate(bullet, transform.position, Quaternion.identity);

                        bulletFired.transform.LookAt(new Vector3(pointToLook.x, transform.position.y, pointToLook.z));

                        //add force to the bullet so that it moves
                        bulletFired.GetComponent<Rigidbody>().AddForce(bulletFired.transform.forward * 100);

                        //remove one bullet from ammo
                        bulletsRemaining--;

                        Debug.Log($"Bullets Remaining: {bulletsRemaining}");

                        //set player kinematic status back to false
                        playerRB.isKinematic = false;


                    }
                }

                //if there are no bullets
                if (bulletsRemaining == 0)
                {
                    //if player presses R
                    if (Input.GetKeyDown(KeyCode.R))
                    {
                        //start reload process
                        reloading = true;
                        StartCoroutine(Reload());
                    }
                }


            }
        }
    }

    IEnumerator Reload()
    {
        //wait two seconds for reload
        Debug.Log("Reloading...");
        yield return new WaitForSeconds(2);
        //add 10 bullets to ammo, only if there are no bullets remaining
        if(bulletsRemaining == 0)
        {
            bulletsRemaining += 10;
            //end reloading process
            reloading = false;
            Debug.Log($"Reloaded! Bullets Remaining: {bulletsRemaining}");
        }
    }
}

