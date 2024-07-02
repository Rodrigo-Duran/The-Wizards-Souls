using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle1ButtonsController : MonoBehaviour
{

    [SerializeField] Puzzle1Controller puzzleController;
    [SerializeField] private Puzzle1PlatformController platformController;
    [SerializeField] public bool isPressed; //PUBLIC PRA TESTE

    //public SpriteRenderer spriteRenderer;
    public Animator animator;

    private void Awake()
    {
        //spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        isPressed = false;
    }

    private void Update()
    { 

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Magic")
        {
            if(!isPressed && platformController.platformIsReady) StartCoroutine(PressingButton());
            if(!puzzleController._puzzleStarted && platformController.platformIsReady) puzzleController._puzzleStarted = true;
        }
    }

    IEnumerator PressingButton()
    {
        isPressed = true;
        puzzleController._buttonsPressed += 1;
        //Debug.Log("BUTTON PRESSED");
        animator.Play("ButtonPressed");
        yield return new WaitForSeconds(0.5f);
        //gameObject.SetActive(false);
    }
}
