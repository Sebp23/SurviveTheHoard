using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        if (collision.gameObject.CompareTag("Enemy") && hasPowerup == false)
        {
            //set game over to true
            gameOver = true;
            Debug.Log("Game Over!");
        }

        if (collision.gameObject.CompareTag("Enemy") && hasPowerup)
        {
            Rigidbody enemyRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = (collision.gameObject.transform.position - transform.position);

            Debug.Log($"Player collided with {collision.gameObject} with powerup set to {hasPowerup}");

            enemyRigidbody.AddForce(awayFromPlayer * powerupStrength, ForceMode.Impulse);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Powerup") && gameOver == false)
        {
            hasPowerup = true;
            Destroy(other.gameObject);
            playerControlScript.playerRenderer.material.color = Color.green;
            StartCoroutine(PowerupCountdownRoutine());
        }
    }

    IEnumerator PowerupCountdownRoutine()
    {
        yield return new WaitForSeconds(7);
        playerControlScript.playerRenderer.material.color = playerControlScript.playerColor;
        hasPowerup = false;
    }
}
