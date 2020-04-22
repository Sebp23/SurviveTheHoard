using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionTracker : MonoBehaviour
{
    public bool gameOver = false;
    
    private Rigidbody enemyRB;

    // Start is called before the first frame update
    void Start()
    {
        enemyRB = GameObject.Find("Enemy").GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            enemyRB.isKinematic = true;
            
            //set game over to true
            gameOver = true;
            Debug.Log("Game Over!");
        }
    }
}
