using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle1Controller : MonoBehaviour
{
    //Button1
    [SerializeField] private GameObject button1;
    [SerializeField] public Puzzle1ButtonsController button1Controller;

    //Button2
    [SerializeField] private GameObject button2;
    [SerializeField] public Puzzle1ButtonsController button2Controller;

    //Button3
    [SerializeField] private GameObject button3;
    [SerializeField] public Puzzle1ButtonsController button3Controller;

    //AllButtons
    [SerializeField] Sprite initialButtonsSprite;

    //Puzzle Attributes
    private bool puzzleStarted;
    private bool puzzleCompleted;
    private int buttonsPressed;
    private bool canRestart;

    public bool _puzzleStarted
    {
        get { return puzzleStarted; }
        set { puzzleStarted = value; }
    }

    public bool _puzzleCompleted
    {
        get { return puzzleCompleted; }
        set { puzzleCompleted = value; }
    }

    public int _buttonsPressed
    {
        get { return buttonsPressed; }
        set { buttonsPressed = value; }
    }

    // Awake
    void Awake()
    {
        StartPuzzle();
    }

    // Update
    void Update()
    {
        if (puzzleStarted)
        {
            if (canRestart) 
            {
                canRestart = false;
                StartCoroutine(RestartingPuzzle());
            }
        }

        if (buttonsPressed == 3)
        {
            puzzleCompleted = true;
        }
    }

    public IEnumerator RestartingPuzzle()
    {
        //Espera 10 segundos para reiniciar o puzzle
        Debug.Log("RESTARTANDO PUZZLE EM 10 SEGUNDOS");

        yield return new WaitForSeconds(10f);

        //Após os 10 segundos
        Debug.Log("RESTARTANDO PUZZLE 0 SEGUNDOS");
        
        
        //Puzzle não começou
        puzzleStarted = false;
        //Puzzle não completou
        puzzleCompleted = false;

        //Todos os botões voltam ao normal
        if(button1Controller.isPressed) button1Controller.animator.Play("ButtonBackToNormal");
        button1Controller.isPressed = false;

        if (button2Controller.isPressed) button2Controller.animator.Play("ButtonBackToNormal");
        button2Controller.isPressed = false;

        if (button3Controller.isPressed) button3Controller.animator.Play("ButtonBackToNormal");
        button3Controller.isPressed = false;

        //Botões pressionados vai pra 0
        buttonsPressed = 0;
        //Puzzle pode recomeçar
        canRestart = true;

        Debug.Log("PUZZLE RESTARTED"); 
        
    }

    public void StartPuzzle()
    {
        button1Controller.isPressed = false;
        button2Controller.isPressed = false;
        button3Controller.isPressed = false;
        puzzleStarted = false;
        puzzleCompleted = false;
        buttonsPressed = 0;
        canRestart = true;
    }
}
