using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shoot : MonoBehaviour
{
    [SerializeField]
    private GameObject bullet;
    [SerializeField]
    private GameObject shootFrom;

    [SerializeField]
    private Camera mainCamera;

    [SerializeField]
    private AudioClip shotFired;
    [SerializeField]
    private AudioClip beginReload;
    [SerializeField]
    private AudioClip reloadSound;
    [SerializeField]
    private AudioClip noAmmo;


    private AudioSource playerAudio;

    private GameObject[] bulletCountArray;

    public int bulletsRemaining;
    public bool reloading = false;
    public bool canFire = true;

    private Rigidbody playerRB;
    private CollisionTracker collisionTrackerScript;
    private Animator playerAnimation;


    // Start is called before the first frame update
    void Start()
    {
        //get main camera component
        mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();

        //get players rigidbody
        playerRB = GameObject.Find("Player").GetComponent<Rigidbody>();

        //get the animator of the player
        playerAnimation = GameObject.Find("Player").GetComponent<Animator>();

        //get the audio component
        playerAudio = GameObject.Find("Player").GetComponent<AudioSource>();

        //get CollisionTracker script
        collisionTrackerScript = GameObject.Find("Player").GetComponent<CollisionTracker>();

        //get the object bullets come from
        shootFrom = GameObject.Find("ShootFrom");

        //get all of the bullet images
        bulletCountArray = new GameObject[]
        {
            GameObject.Find("BulletCount1"),
            GameObject.Find("BulletCount2"),
            GameObject.Find("BulletCount3"),
            GameObject.Find("BulletCount4"),
            GameObject.Find("BulletCount5"),
            GameObject.Find("BulletCount6"),
            GameObject.Find("BulletCount7"),
            GameObject.Find("BulletCount8"),
            GameObject.Find("BulletCount9"),
            GameObject.Find("BulletCount10"),
        };
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
                if (bulletsRemaining > 0 && canFire)
                {
                    //if left mouse is clicked
                    if (Input.GetMouseButtonDown(0))
                    {

                        //set the player as kinematic, so they dont fly back
                        playerRB.isKinematic = true;

                        //play shotFired audio
                        playerAudio.PlayOneShot(shotFired, 0.2f);

                        //play shooting animation
                        playerAnimation.SetTrigger("shoot");

                        //spawn bullet
                        GameObject bulletFired = Instantiate(bullet, shootFrom.transform.position, Quaternion.identity);

                        bulletFired.transform.LookAt(new Vector3(pointToLook.x, shootFrom.transform.position.y, pointToLook.z));

                        //add force to the bullet so that it moves
                        bulletFired.GetComponent<Rigidbody>().AddForce(bulletFired.transform.forward * 100);

                        //remove one bullet from ammo
                        bulletsRemaining--;

                        Debug.Log($"Bullets Remaining: {bulletsRemaining}");

                        //set player kinematic status back to false
                        playerRB.isKinematic = false;


                    }
                }

                //player can reload if magazine has less than 10 bullets
                if (bulletsRemaining < 10 && reloading == false)
                {

                    CheckAmmo();

                    //if player presses R
                    if (Input.GetKeyDown(KeyCode.R))
                    {

                        //player cannot shoot while reloading
                        canFire = false;

                        SetAmmoDeactive();

                        //start reload process
                        reloading = true;
                        //playerAudio.PlayOneShot(beginReload, 1.5f);
                        StartCoroutine(Reload());
                    }
                }

                if (bulletsRemaining <= 0)
                {
                    if (Input.GetMouseButtonDown(0) && reloading == false)
                    {
                        playerAudio.PlayOneShot(noAmmo, 2.0f);
                    }
                }


            }
        }
    }

    void CheckAmmo()
    {
        if (bulletsRemaining == 9)
        {
            bulletCountArray[9].SetActive(false);
        }
        if (bulletsRemaining == 8)
        {
            bulletCountArray[8].SetActive(false);
        }
        if (bulletsRemaining == 7)
        {
            bulletCountArray[7].SetActive(false);
        }
        if (bulletsRemaining == 6)
        {
            bulletCountArray[6].SetActive(false);
        }
        if (bulletsRemaining == 5)
        {
            bulletCountArray[5].SetActive(false);
        }
        if (bulletsRemaining == 4)
        {
            bulletCountArray[4].SetActive(false);
        }
        if (bulletsRemaining == 3)
        {
            bulletCountArray[3].SetActive(false);
        }
        if (bulletsRemaining == 2)
        {
            bulletCountArray[2].SetActive(false);
        }
        if (bulletsRemaining == 1)
        {
            bulletCountArray[1].SetActive(false);
        }
        if (bulletsRemaining == 0)
        {
            bulletCountArray[0].SetActive(false);
        }
    }

    //TODO make it so that it shows each bullet becoming reactivated individually over 2 seconds total
    IEnumerator SetAmmoActive()
    {
        foreach(GameObject bullet in bulletCountArray)
        {
            bullet.SetActive(true);
            yield return new WaitForSeconds(0.5f);
        }
    }

    void SetAmmoDeactive()
    {
        foreach (GameObject bullet in bulletCountArray)
        {
            bullet.SetActive(false);
        }
    }

    IEnumerator Reload()
    {
        //wait two seconds for reload
        Debug.Log("Reloading...");
        //refills ammo
        if (bulletsRemaining < 10)
        {
            foreach (GameObject bullet in bulletCountArray)
            {
                yield return new WaitForSeconds(0.2f);
                bullet.SetActive(true);
                playerAudio.PlayOneShot(beginReload, 1.5f);
            }
            int bulletsNeeded = 10 - bulletsRemaining;
            playerAudio.PlayOneShot(reloadSound, 1.0f);
            //StartCoroutine(SetAmmoActive());
            bulletsRemaining += bulletsNeeded;
            //end reloading process
            reloading = false;
            canFire = true;
            Debug.Log($"Reloaded! Bullets Remaining: {bulletsRemaining}");
        }
    }
}