using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle1PlatformController : MonoBehaviour
{
    
    [SerializeField] Puzzle1Controller puzzle1Controller;
    [SerializeField] GameObject startingPoint;
    [SerializeField] GameObject endPoint;
    [SerializeField] List<Sprite> spritesList;

    //Platform Attributes
    [SerializeField] private float speed = 1f;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D platformRB;
    private Vector2 direction;
    [SerializeField] public bool platformIsReady; //TESTE

    // Awake
    void Awake()
    {
        platformRB = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        direction.x = 0f;
        direction.y = 0f;
        platformIsReady = true;
    }

    // Update
    void Update()
    {
        if (puzzle1Controller._puzzleCompleted)
        {
            direction.y = 1f;
            platformIsReady = false;
        }

        switch (puzzle1Controller._buttonsPressed)
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
        platformRB.velocity = new Vector2(direction.x, direction.y).normalized * speed;
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
            platformIsReady = true;
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
        yield return new WaitForSeconds(2f);
        Debug.Log("Platform going down");
        direction.y = -1f;
        puzzle1Controller.StartPuzzle();
    }

}
