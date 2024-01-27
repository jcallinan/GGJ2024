using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void StartQuest()
    {
        // Replace "GameScene" with the name of your game scene
        SceneManager.LoadScene("Start_Quest");
    }
    public void StartMML()
    {
        // Replace "GameScene" with the name of your game scene
        SceneManager.LoadScene("Start_MML");
    }
}
