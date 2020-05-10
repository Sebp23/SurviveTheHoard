using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCollisionTracker : MonoBehaviour
{
    private Score scoreScript;

    // Start is called before the first frame update
    void Start()
    {
        //find the Score script, so we can manipulate score from this script
        scoreScript = GameObject.Find("Score").GetComponent<Score>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision other)
    {
        //check to see if bullet object hit enemy object
        if (other.gameObject.CompareTag("Enemy"))
        {
            //add 5 to the score
            scoreScript.timer += 5;

            //destroy enemy and bullet object
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
        //if bullet misses
        else
        {
            StartCoroutine(DestroyBullet());
        }
    }

    IEnumerator DestroyBullet()
    {
        //destroy bullet object after 3 seconds
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }
}
