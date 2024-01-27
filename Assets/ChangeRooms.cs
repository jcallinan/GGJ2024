using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeRooms : MonoBehaviour
{ 
    
    // The scene name to load, set this in the Inspector
    public string sceneToLoad;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.tag);
        // Check if the collider is the player
       // if (other.CompareTag("Player"))
       // {
            // Load the scene
            SceneManager.LoadScene(sceneToLoad);
      //  }
    }
}
