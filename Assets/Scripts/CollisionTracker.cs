using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionTracker : MonoBehaviour
{
    private float powerupStrength = 30.0f;

    public bool gameOver = false;
    public bool hasPowerup = false;
    public bool powerupEnding = false;

    [SerializeField]
    private AudioClip getsPowerup;
    [SerializeField]
    private AudioClip knockEnemy;
    [SerializeField]
    private AudioClip hitPlayer;

    private AudioClip music;

    private AudioSource colliderAudio;
    private AudioSource cameraAudio;

    //private Color variableColor;

    private PlayerController playerControlScript;
    private LookAtFollow enemyControlScript;
    private Rigidbody enemyRB;
    private Animator playerAnimation;

    // Start is called before the first frame update
    void Start()
    {
        //get player controller script
        playerControlScript = GameObject.Find("Player").GetComponent<PlayerController>();

        //get the animator of the player
        playerAnimation = GameObject.Find("Player").GetComponent<Animator>();

        //get the audio component
        colliderAudio = GameObject.Find("Player").GetComponent<AudioSource>();

        cameraAudio = GameObject.Find("Main Camera").GetComponent<AudioSource>();
        music = GameObject.Find("Main Camera").GetComponent<AudioClip>();

        //variableColor = playerControlScript.playerRenderer.material.color;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        enemyControlScript = collision.gameObject.GetComponent<LookAtFollow>();

        //check to see if the player has hit an enemy, and the powerup status is false
        if (collision.gameObject.CompareTag("Enemy") && hasPowerup == false && gameOver == false)
        {
            //set game over to true
            gameOver = true;
            playerAnimation.SetBool("isMoving", false);

            cameraAudio.Stop();
            colliderAudio.PlayOneShot(hitPlayer, 3.0f);
            //call enumerator that loads GameOver scene
            StartCoroutine(GameOverScene());
            Debug.Log("Game Over!");
        }

        //if the player hits and enemy and their powerup status is true
        if (collision.gameObject.CompareTag("Enemy") && hasPowerup)
        {
            colliderAudio.PlayOneShot(knockEnemy, 3.0f);

            //get the rigidbody of the enemy
            Rigidbody enemyRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = (collision.gameObject.transform.position - transform.position);

            Debug.Log($"Player collided with {collision.gameObject} with powerup set to {hasPowerup}");
            //knock enemies away from player
            enemyRigidbody.AddForce(awayFromPlayer * powerupStrength, ForceMode.Impulse);

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //make sure the game isnt over when player enters a powerup trigger
        if (other.CompareTag("Powerup") && gameOver == false)
        {
            //set powerup status to true
            hasPowerup = true;

            colliderAudio.PlayOneShot(getsPowerup, 1.0f);

            //destroy powerup object
            Destroy(other.gameObject);
            //change player color to green
            playerControlScript.playerRenderer.material.color = Color.green;
            //enumerator that has powerup effect and determines how long it will last
            StartCoroutine(PowerupCountdownRoutine());
        }
    }

    IEnumerator PowerupCountdownRoutine()
    {
        //enemyRB = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Rigidbody>();
        //powerup lasts 7 seconds
        yield return new WaitForSeconds(5);

        //color flashes three times to indicate powerup ending
        //playerControlScript.playerRenderer.material.color = Color.Lerp(Color.green, playerControlScript.playerColor, Time.time);
        //yield return new WaitForSeconds(0.25f);
        //playerControlScript.playerRenderer.material.color = Color.Lerp(playerControlScript.playerColor, Color.green, Time.time);
        //yield return new WaitForSeconds(0.25f);
        //playerControlScript.playerRenderer.material.color = Color.Lerp(Color.green, playerControlScript.playerColor, Time.time);
        //yield return new WaitForSeconds(0.25f);
        //playerControlScript.playerRenderer.material.color = Color.Lerp(playerControlScript.playerColor, Color.green, Time.time);
        //yield return new WaitForSeconds(0.25f);
        //playerControlScript.playerRenderer.material.color = Color.Lerp(Color.green, playerControlScript.playerColor, Time.time);
        //yield return new WaitForSeconds(0.25f);
        //playerControlScript.playerRenderer.material.color = Color.Lerp(playerControlScript.playerColor, Color.green, Time.time);
        //yield return new WaitForSeconds(0.25f);
        //playerControlScript.playerRenderer.material.color = Color.Lerp(Color.green, playerControlScript.playerColor, Time.time);
        //yield return new WaitForSeconds(0.25f);
        //playerControlScript.playerRenderer.material.color = Color.Lerp(playerControlScript.playerColor, Color.green, Time.time);
        //yield return new WaitForSeconds(0.25f);
        //TODO I hate this ^. This is disgusting. Find a better way to do this (╯°□°）╯︵ ┻━┻

        int i = 0;
        while (i < 6)
        {
            playerControlScript.playerRenderer.material.color = Color.Lerp(Color.green, playerControlScript.playerColor, Time.time);
            yield return new WaitForSeconds(0.25f);
            playerControlScript.playerRenderer.material.color = Color.Lerp(playerControlScript.playerColor, Color.green, Time.time);
            yield return new WaitForSeconds(0.25f);
            i++;
        }
        //this ^ is much better ┬─┬ ノ( ゜-゜ノ)

        //set player color back to original color after powerup has ended
        playerControlScript.playerRenderer.material.color = playerControlScript.playerColor;
        //set powerup status to false
        hasPowerup = false;
    }

    IEnumerator GameOverScene()
    {
        //wait for 3 seconds before loading GameOver scene
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("GameOver");
        Debug.Log("Scene Loaded!");
    }
}