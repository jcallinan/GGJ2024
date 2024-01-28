using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetGame : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    { 
        
    }
    void ResetIt()
    {
        // Reload the current scene
        SceneManager.LoadScene("Start_Screen");

        // If you have other reset logic, implement it here
        // For example, resetting scores, player position, etc.
    }
    // Update is called once per frame
    void Update()
    {
        // Check if the Escape key is pressed
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Reset the game
            ResetIt();
        }
    }
}
