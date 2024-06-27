using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private Transform LeftPoint;
    [SerializeField] private Transform RightPoint;
    [SerializeField] private Transform player;
    [SerializeField] private float enemySpeed = 3f;
    [SerializeField] private float enemyLife = 30f;
    [SerializeField] private GameObject soul;
   


    private Rigidbody2D enemyRB;
    private Animator animator;
    private HealthBarController healthBarController;

    private Vector2 direction;
    private bool playerIsClose;
    private bool enemyIsDead;
    private bool enemyIsTakingAHit;
    private float enemyCurrentLife;

    public bool _playerIsClose
    {
        get { return playerIsClose; }
        set { playerIsClose = value; }
    }

    // Awake
    void Awake()
    {
        enemyRB = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        healthBarController = GetComponentInChildren<HealthBarController>();

        direction.x = LeftPoint.position.x - transform.position.x;
        playerIsClose = false;
        enemyIsDead = false;
        enemyIsTakingAHit = false;
        enemyCurrentLife = enemyLife;
    }

    // Update
    void Update()
    {

        if (playerIsClose) direction.x = 0;
        if (enemyCurrentLife <= 0)
        {
            enemyIsDead = true;
            direction.x = 0;
        }

        if (direction.x != 0 && !enemyIsDead && !enemyIsTakingAHit)
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
        else if (direction.x == 0 && !enemyIsDead && !enemyIsTakingAHit)
        {
            animator.SetInteger("transition", 1);
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
        else if (enemyIsTakingAHit)
        {
            animator.SetInteger("transition", 4);
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
        Debug.Log("Enemy hit by magic");
        enemyCurrentLife -= 10;
        healthBarController.UpdateHealthBarValue(enemyCurrentLife, enemyLife);
        enemyIsTakingAHit = true;
        yield return new WaitForSeconds(0.3f);
        enemyIsTakingAHit = false;
    }

    IEnumerator Death()
    {
        Debug.Log("ENEMY DIED");
        yield return new WaitForSeconds(2.5f);
        Debug.Log(transform.position);
        Instantiate(soul, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

}
