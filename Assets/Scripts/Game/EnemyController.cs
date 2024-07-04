using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    //
    [SerializeField] private Transform LeftPoint;
    [SerializeField] private Transform RightPoint;
    [SerializeField] private Transform player;
    [SerializeField] private float enemySpeed = 3f;
    [SerializeField] private float enemyLife = 30f;
    [SerializeField] private GameObject soul;
    [SerializeField] private Transform soulTransform;

    //Components
    private Rigidbody2D enemyRB;
    private Animator animator;
    private HealthBarController healthBarController;
    private BoxCollider2D boxCollider;

    private Vector2 direction;
    private bool playerIsClose;
    private bool enemyIsDead;
    private bool enemyIsTakingAHit;
    private bool enemyIsAttacking;
    private float enemyCurrentLife;

    public bool _playerIsClose
    {
        get { return playerIsClose; }
        set { playerIsClose = value; }
    }

    public bool _enemyIsDead
    {
        get { return enemyIsDead; }
        set { enemyIsDead = value; }
    }

    public bool _enemyIsAttacking
    {
        get { return enemyIsAttacking; }
        set { enemyIsAttacking = value; }
    }

    // Awake
    void Awake()
    {
        enemyRB = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        healthBarController = GetComponentInChildren<HealthBarController>();
        boxCollider = GetComponent<BoxCollider2D>();

        direction.x = LeftPoint.position.x - transform.position.x;
        playerIsClose = false;
        enemyIsDead = false;
        enemyIsTakingAHit = false;
        enemyCurrentLife = enemyLife;
        enemyIsAttacking = false;
    }

    // Update
    void Update()
    {

        if (playerIsClose)
        {
            direction.x = 0;
            float playerPosition = player.position.x - transform.position.x;
            if (playerPosition < 0)
            {
                transform.eulerAngles = new Vector3(0, 180, 0);
            }
            else if (playerPosition > 0)
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
            }
        }
        if (enemyCurrentLife <= 0)
        {
            enemyIsDead = true;
            enemyRB.gravityScale = 0f;
            boxCollider.enabled = false; 
            direction.x = 0;
            direction.y = 0;
        }

        if (direction.x != 0 && !enemyIsDead && !enemyIsTakingAHit && !enemyIsAttacking)
        {
            animator.SetInteger("transition", 5);

            if (direction.x < 0)
            {
                transform.eulerAngles = new Vector3(0, 180, 0);
            }
            else if (direction.x > 0)
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
            }
        }
        else if (direction.x == 0 && !enemyIsDead && !enemyIsTakingAHit && !enemyIsAttacking)
        {
            animator.SetInteger("transition", 1);
        }
        else if (enemyIsTakingAHit && !enemyIsDead && !enemyIsAttacking)
        {
            animator.SetInteger("transition", 4);
        }
        else if (enemyIsAttacking && !enemyIsDead)
        {
            animator.SetInteger("transition", 3);
        }
        else if (enemyIsDead)
        {
            animator.SetInteger("transition", 2);
            StartCoroutine(Death());
        }
    }

    //FixedUpdate
    private void FixedUpdate()
    {
        
        if(!playerIsClose) enemyRB.velocity = new Vector2(direction.x, direction.y).normalized * enemySpeed;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "LeftPoint")
        {
            direction.x = RightPoint.position.x - transform.position.x;
        }
        if (collision.gameObject.tag == "RightPoint")
        {
            direction.x = LeftPoint.position.x - transform.position.x;
        }
        if (collision.gameObject.tag == "Magic")
        {
            if (!enemyIsDead)
            {
                StartCoroutine(takeDamage());
            }
        }
    }

    IEnumerator takeDamage()
    {
        //Debug.Log("TAKING THE HIT");
        Debug.Log("Enemy was hitted by Player's magic");
        enemyCurrentLife -= 10;
        healthBarController.UpdateHealthBarValue(enemyCurrentLife, enemyLife);
        enemyIsTakingAHit = true;
        yield return new WaitForSeconds(0.3f);
        enemyIsTakingAHit = false;
    }

    IEnumerator Death()
    {
        //Debug.Log("ENEMY DIED");
        yield return new WaitForSeconds(2.5f);
        //Debug.Log(transform.position);
        Instantiate(soul, soulTransform.position, Quaternion.identity);
        //Debug.Log(soul.transform.position);
        Destroy(gameObject);
    }

}
