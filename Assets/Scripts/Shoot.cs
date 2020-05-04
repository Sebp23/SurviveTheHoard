﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    [SerializeField]
    private GameObject bullet;

    [SerializeField]
    private Camera mainCamera;

    [SerializeField]
    private float bulletsRemaining;

    private Rigidbody playerRB;


    // Start is called before the first frame update
    void Start()
    {
        //bulletRB = GetComponent<Rigidbody>();

        mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();

        playerRB = GameObject.Find("Player").GetComponent<Rigidbody>();
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

            if(bulletsRemaining > 0)
            {
                if (Input.GetMouseButtonDown(0))
                {

                    playerRB.isKinematic = true;

                    GameObject bulletFired = Instantiate(bullet, transform.position, Quaternion.identity);

                    bulletFired.transform.LookAt(new Vector3(pointToLook.x, transform.position.y, pointToLook.z));

                    bulletFired.GetComponent<Rigidbody>().AddForce(bulletFired.transform.forward * 100);

                    bulletsRemaining--;

                    playerRB.isKinematic = false;


                }
            }

            if(bulletsRemaining == 0)
            {
                if (Input.GetKeyDown(KeyCode.R))
                {
                    StartCoroutine(Reload());
                }
            }
        }

        
    }

    IEnumerator Reload()
    {
        Debug.Log("Reloading...");
        yield return new WaitForSeconds(3);
        bulletsRemaining += 5;
        Debug.Log($"Reloaded! Capacity: {bulletsRemaining}");
    }
}
