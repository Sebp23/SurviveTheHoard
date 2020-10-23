using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsController : MonoBehaviour
{
    public bool isMainMenu;

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
        if (isMainMenu)
        {
            //Load the start menu scene if "Main Menu" is clicked
            SceneManager.LoadScene("StartMenu");
            Debug.Log("Scene Loaded!");
        }
    }
}