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
        //bulletRB = GetComponent<Rigidbody>();

        mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();

        playerRB = GameObject.Find("Player").GetComponent<Rigidbody>();

        collisionTrackerScript = GameObject.Find("Player").GetComponent<CollisionTracker>();
    }

    // Update is called once per frame
    void Update()
    {
        //CREDIT TO CHRIS REARDON FOR HELPING WITH BELOW CODE

        //A Ray (line) named CameraRay is cast from camera to where mouse is
        Ray cameraRay = mainCamera.ScreenPointToRay(Input.mousePosition);

        // !!! Futher Explantion Needed !!!
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);

        //Float initalized for the following if loop
        float rayLength;


        if (collisionTrackerScript.gameOver == false)
        {
            //Following If Loop
            //!!! Sets rayLength to however long the ray is from cameraRay to groundPlane !!!
            if (groundPlane.Raycast(cameraRay, out rayLength))
            {
                // Gets a vector3 pos where the RayLength crosses (gives the orientation for the player)
                Vector3 pointToLook = cameraRay.GetPoint(rayLength);

                //Debug to see casted ray 
                Debug.DrawLine(cameraRay.origin, pointToLook, Color.red);

                //Faces the point
                transform.LookAt(new Vector3(pointToLook.x, transform.position.y, pointToLook.z));

                if (bulletsRemaining > 0)
                {

                    if (Input.GetMouseButtonDown(0))
                    {


                        playerRB.isKinematic = true;

                        GameObject bulletFired = Instantiate(bullet, transform.position, Quaternion.identity);

                        bulletFired.transform.LookAt(new Vector3(pointToLook.x, transform.position.y, pointToLook.z));

                        bulletFired.GetComponent<Rigidbody>().AddForce(bulletFired.transform.forward * 100);

                        bulletsRemaining--;

                        Debug.Log($"Bullets Remaining: {bulletsRemaining}");

                        playerRB.isKinematic = false;


                    }
                }

                if (bulletsRemaining == 0)
                {

                    if (Input.GetKeyDown(KeyCode.R))
                    {
                        reloading = true;
                        StartCoroutine(Reload());
                    }
                }


            }
        }
    }

    IEnumerator Reload()
    {
        
        Debug.Log("Reloading...");
        yield return new WaitForSeconds(3);
        if(bulletsRemaining == 0)
        {
            bulletsRemaining += 5;
            reloading = false;
            Debug.Log($"Reloaded! Bullets Remaining: {bulletsRemaining}");
        }
    }
}

