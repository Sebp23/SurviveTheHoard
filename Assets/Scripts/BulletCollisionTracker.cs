using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCollisionTracker : MonoBehaviour
{
    private Score scoreScript;

    // Start is called before the first frame update
    void Start()
    {
        scoreScript = GameObject.Find("Score").GetComponent<Score>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            scoreScript.timer += 5;

            Destroy(other.gameObject);
            Destroy(gameObject);
        }
        else
        {
            StartCoroutine(DestroyBullet());
        }
    }

    IEnumerator DestroyBullet()
    {
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }
}
