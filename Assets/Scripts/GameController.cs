using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    [SerializeField] private Transform environment;
    [SerializeField] GameObject button1;
    [SerializeField] GameObject button2;
    [SerializeField] GameObject button3;

    private bool button1WasPressed;
    private bool button2WasPressed;
    private bool button3WasPressed;
    private bool puzzleStarted;
    private bool puzzleCompleted;

    private int howManyButtonsArePressed;

    public bool _button1WasPressed
    {
        get { return button1WasPressed; }
        set { button1WasPressed = value; }
    }

    public bool _button2WasPressed
    {
        get { return button2WasPressed; }
        set { button2WasPressed = value; }
    }

    public bool _button3WasPressed
    {
        get { return button3WasPressed; }
        set { button3WasPressed = value; }
    }

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

    public int _howManyButtonsArePressed
    {
        get { return howManyButtonsArePressed; }
        set { howManyButtonsArePressed = value; }
    }

    // Awake
    void Awake()
    {
        button1WasPressed = false;
        button2WasPressed = false;
        button3WasPressed = false;
        puzzleStarted = false;
        puzzleCompleted = false;
        howManyButtonsArePressed = 0;
    }

    // Update
    void Update()
    {
        environment.transform.position = transform.position;

        if (puzzleStarted)
        {
            StartCoroutine(RestartingPuzzle());
        }

        if (button1WasPressed && button2WasPressed && button3WasPressed)
        {
            StopCoroutine(RestartingPuzzle());
            //Debug.Log("PUZZLE COMPLETED");
            puzzleCompleted = true;
            //puzzleStarted = false;
        }
    }

    public IEnumerator RestartingPuzzle()
    {
        yield return new WaitForSeconds(10f);
        puzzleStarted = false;
        Debug.Log("PUZZLE STARTED: " + puzzleStarted);
        puzzleCompleted = false;
        button1WasPressed = false;
        button2WasPressed = false;
        button3WasPressed = false;
        if (!button1.activeInHierarchy)button1.SetActive(true);
        if (!button2.activeInHierarchy)button2.SetActive(true);
        if (!button3.activeInHierarchy) button3.SetActive(true);
        Debug.Log("PUZZLE RESTARTED");
    }
}
