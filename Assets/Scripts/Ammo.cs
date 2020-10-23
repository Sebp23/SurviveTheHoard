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

        shootScript = GameObject.Find("Player").GetComponent<Shoot>();
    }

    // Update is called once per frame
    void Update()
    {
        //if the player is not reloading and has no more ammo
        if (shootScript.reloading == false && shootScript.bulletsRemaining == 0)
        {
            ammoCount.color = Color.yellow;
            //tell the player they need to reload
            ammoCount.text = ("Press 'R' to reload...");
        }
        //if player is currently reloading
        if (shootScript.reloading)
        {
            ammoCount.color = Color.green;
            //print "Reloading..." to inform player that they are reloading
            ammoCount.text = (" ");
        }
    }
}
