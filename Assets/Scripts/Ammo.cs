using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ammo : MonoBehaviour
{
    private Shoot shootScript;

    public Text ammoCount;

    // Start is called before the first frame update
    void Start()
    {
        //determines the position of the score on the HUD
        transform.position = Camera.main.ViewportToWorldPoint(new Vector3(4.1f, 0, -800f));

        shootScript = GameObject.Find("Player").GetComponent<Shoot>();
    }

    // Update is called once per frame
    void Update()
    {
        //if player is not reloading, and has ammo
        if(shootScript.reloading == false && shootScript.bulletsRemaining > 0)
        {
            //print amount of ammo player has remaining
            ammoCount.text = ($"Ammo: {shootScript.bulletsRemaining.ToString()}");
        }
        //if the player is not reloading and has no more ammo
        if(shootScript.reloading == false && shootScript.bulletsRemaining == 0)
        {
            //tell the player they need to reload
            ammoCount.text = ("Press 'R' to reload...");
        }
        //if player is currently reloading
        if (shootScript.reloading)
        {
            //print "Reloading..." to inform player that they are reloading
            ammoCount.text = ("Reloading...");
        }
    }
}
