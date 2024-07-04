using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    [SerializeField] private Transform environment;

    // Update
    void Update()
    {
        //o ambiente de fundo seguir a camera
        environment.transform.position = transform.position;
    }
}
