using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuController : MonoBehaviour
{
    public bool isPlay;
    public bool isTutorial;
    public bool isQuit;
    public bool isCredits;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnMouseUp()
    {
        if (isPlay)
        {
            //Load the main game scene if "Play" is clicked
            SceneManager.LoadScene("GameLevel");
            SceneManager.SetActiveScene(SceneManager.GetSceneByName("GameLevel"));
            Debug.Log("Scene Loaded!");
        }
        if (isTutorial)
        {
            //Load the Tutorial scene if "How to Play" is clicked
            SceneManager.LoadScene("Tutorial");
            SceneManager.SetActiveScene(SceneManager.GetSceneByName("Tutorial"));
            Debug.Log("Scene Loaded!");
        }
        if (isCredits)
        {
            //load the credits scene of "Credits" is clicked
            SceneManager.LoadScene("Credits");
            SceneManager.SetActiveScene(SceneManager.GetSceneByName("Credits"));
            Debug.Log("Scene Loaded!");
        }
        if (isQuit)
        {
            //quit the application if "Quit" is clicked
            Application.Quit();
            Debug.Log("Scene Loaded!");
        }
    }
}
