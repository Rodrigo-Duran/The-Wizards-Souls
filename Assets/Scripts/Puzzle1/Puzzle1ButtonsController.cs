using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle1ButtonsController : MonoBehaviour
{

    [SerializeField] GameController gameController;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (gameObject.tag == "PuzzleButton1")
        {
            if (gameController._button1WasPressed)
            {
                StartCoroutine(PressingButton());
            }
        }
        else if (gameObject.tag == "PuzzleButton2")
        {
            if (gameController._button2WasPressed)
            {
                StartCoroutine(PressingButton());
            }
        }
        else if (gameObject.tag == "PuzzleButton3")
        {
            if (gameController._button3WasPressed)
            {
                StartCoroutine(PressingButton());
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Magic")
        {
            if (gameObject.tag == "PuzzleButton1")
            {
                gameController._button1WasPressed = true;
                gameController._howManyButtonsArePressed += 1;
            }
            else if (gameObject.tag == "PuzzleButton2")
            {
                gameController._button2WasPressed = true;
                gameController._howManyButtonsArePressed += 1;
            }
            else if (gameObject.tag == "PuzzleButton3")
            {
                gameController._button3WasPressed = true;
                gameController._howManyButtonsArePressed += 1;
            }

            if (!gameController._puzzleStarted)
            {
                gameController._puzzleStarted = true;
            }
        }
    }

    IEnumerator PressingButton()
    {
        Debug.Log("BUTTON PRESSED");
        animator.Play("ButtonPressed");
        yield return new WaitForSeconds(0.5f);
        gameObject.SetActive(false);
    }
}
