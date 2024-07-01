using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle1PlatformController : MonoBehaviour
{

    [SerializeField] GameController gameController;
    [SerializeField] private float speed = 1f;
    [SerializeField] GameObject startingPoint;
    [SerializeField] GameObject endPoint;
    [SerializeField] List<Sprite> spritesList;
    
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rigidbody;
    private Vector2 direction;

    // Awake
    void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        direction.x = 0f;
        direction.y = 0f;
    }

    // Update
    void Update()
    {
        if (gameController._puzzleCompleted)
        {
            direction.y = 1f;
        }

        switch (gameController._howManyButtonsArePressed)
        {
            case 0:
                spriteRenderer.sprite = spritesList[0];
                break;

            case 1:
                spriteRenderer.sprite = spritesList[1];
                break;

            case 2:
                spriteRenderer.sprite = spritesList[2];
                break;

            case 3:
                spriteRenderer.sprite = spritesList[3];
                break;
        }
    }

    //FixedUpdate
    private void FixedUpdate()
    {
        rigidbody.velocity = new Vector2(direction.x, direction.y).normalized * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == endPoint)
        {
            Debug.Log("Platform touchs end point");
            direction.y = 0;
            StartCoroutine(PlatformGoingDown());
        }
        if (collision.gameObject == startingPoint)
        {
            Debug.Log("Platform touchs starting point");
            direction.y = 0f;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Platforms")
        {
            Physics2D.IgnoreCollision(gameObject.GetComponent<BoxCollider2D>(), collision.gameObject.GetComponent<BoxCollider2D>());
        }
    }

    IEnumerator PlatformGoingDown()
    {
        gameController._button1WasPressed = false;
        gameController._button2WasPressed = false;
        gameController._button3WasPressed = false;
        yield return new WaitForSeconds(2f);
        Debug.Log("Platform going down");
        direction.y = -1f;
        StartCoroutine(gameController.RestartingPuzzle());
    }

}
