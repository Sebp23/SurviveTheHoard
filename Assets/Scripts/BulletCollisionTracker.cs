using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCollisionTracker : MonoBehaviour
{
    private Score scoreScript;

    [SerializeField]
    private AudioClip bulletHit;

    private AudioSource bulletAudio;


    // Start is called before the first frame update
    void Start()
    {
        //find the Score script, so we can manipulate score from this script
        scoreScript = GameObject.Find("Score").GetComponent<Score>();

        //get the audio component
        bulletAudio = GameObject.Find("Player").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        //check to see if bullet object hit enemy object
        if (other.gameObject.CompareTag("Enemy"))
        {
            //play the bulletHit sound
            bulletAudio.PlayOneShot(bulletHit, 0.4f);

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