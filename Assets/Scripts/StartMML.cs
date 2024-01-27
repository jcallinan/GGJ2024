using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMML : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.SpawnRandomCharacter();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
