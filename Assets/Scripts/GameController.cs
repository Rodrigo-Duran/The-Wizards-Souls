using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    [SerializeField] private Transform environment;

    // Awake
    void Awake()
    {
        
    }

    // Update
    void Update()
    {
        environment.transform.position = transform.position; 
    }
}
