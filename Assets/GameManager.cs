using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int CurrentStepInQuest;
    public string[] questSteps;
    public GameObject[] characterPrefabs;
    public GameObject currentCharacter;
    // Start is called before the first frame update
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        CurrentStepInQuest = 0;
        
    }
    public void AdvanceQuest()
    {
        if (CurrentStepInQuest < questSteps.Length - 1)
        {
            CurrentStepInQuest++;
            // Execute the logic for the new quest step
            ProcessQuestStep(CurrentStepInQuest);
        }
    }
    // Function to check if something is funny - as in two GameObjects have the same tag
    public bool IsThisFunny(GameObject obj1, GameObject obj2)
    {
        return obj1.tag == obj2.tag;
    }
    // Function to spawn a random character
    public void SpawnRandomCharacter()
    {
        if (characterPrefabs.Length == 0)
        {
            Debug.LogWarning("No character prefabs assigned.");
            return;
        }

        int randomIndex = Random.Range(0, characterPrefabs.Length);
        GameObject randomCharacter = characterPrefabs[randomIndex];
        GameManager.Instance.currentCharacter = randomCharacter;
        Instantiate(randomCharacter, Vector3.zero, Quaternion.identity); // Spawns at position (0,0,0)
    }
    // Function to check for the next quest step or reset
    public void CheckForNextQuestStepOrReset(int currentStep, string userChoice)
    {
        if (questSteps[currentStep+1] == userChoice)
        {
            // Valid choice, move to the next step
            CurrentStepInQuest++;
            ProcessQuestStep(CurrentStepInQuest);
        }
        else
        {
            // Invalid choice, reset the game
            Debug.Log("Invalid choice. Resetting the game.");
            ResetGame();
        }
    }
    private void ResetGame()
    {
        CurrentStepInQuest = 0;
        SceneManager.LoadScene(questSteps[0]);
    }
    private void ProcessQuestStep(int stepIndex)
    {
        // Check if the step index is within bounds of the array
        if (stepIndex >= 0 && stepIndex < questSteps.Length)
        {
            // Load the scene specified in questSteps[stepIndex]
            SceneManager.LoadScene(questSteps[stepIndex]);
        }
        else
        {
            Debug.LogError("Quest step index is out of bounds.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
