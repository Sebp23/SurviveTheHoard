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
        if(shootScript.reloading == false && shootScript.bulletsRemaining > 0)
        {
            ammoCount.text = ($"Ammo: {shootScript.bulletsRemaining.ToString()}");
        }
        if(shootScript.reloading == false && shootScript.bulletsRemaining == 0)
        {
            ammoCount.text = ("Press 'R' to reload...");
        }
        if (shootScript.reloading)
        {
            ammoCount.text = ("Reloading...");
        }
    }
}
