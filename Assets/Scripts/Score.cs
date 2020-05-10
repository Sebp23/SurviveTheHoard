using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public float timer = 0.0f;
    [SerializeField]
    private Text scoreCount;

    public int score;

    // Start is called before the first frame update
    void Start()
    {
        //determines the position of the score on the HUD
        transform.position = Camera.main.ViewportToWorldPoint(new Vector3(3.1f, 0, -850f));
    }

    // Update is called once per frame
    void Update()
    {
        AddSecond();
    }

    private void AddSecond()
    {
        //add to the score each second
        //the score = amount of seconds survived
        timer += Time.deltaTime;
        score = Mathf.RoundToInt(timer);
        scoreCount.text = ($"Score: {score.ToString()}");
    }
}
