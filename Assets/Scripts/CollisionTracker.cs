using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionTracker : MonoBehaviour
{
    private float powerupStrength = 30.0f;

    public bool gameOver = false;
    public bool hasPowerup = false;

    private PlayerController playerControlScript;

    // Start is called before the first frame update
    void Start()
    {
        //get player controller script
        playerControlScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        //check to see if the player has hit an enemy, and the powerup status is false
        if (collision.gameObject.CompareTag("Enemy") && hasPowerup == false)
        {
            //set game over to true
            gameOver = true;
            //call enumerator that loads GameOver scene
            StartCoroutine(GameOverScene());
            Debug.Log("Game Over!");
        }

        //if the player hits and enemy and their powerup status is true
        if (collision.gameObject.CompareTag("Enemy") && hasPowerup)
        {
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
        //powerup lasts 7 seconds
        yield return new WaitForSeconds(7);
        //set player color back to original color after powerup has ended
        playerControlScript.playerRenderer.material.color = playerControlScript.playerColor;
        //set powerup status to false
        hasPowerup = false;
    }

    IEnumerator GameOverScene()
    {
        //wait for 3 seconds before loading GameOver scene
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("GameOver");
        Debug.Log("Scene Loaded!");
    }
}
