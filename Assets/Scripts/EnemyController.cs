using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [SerializeField] GameObject soul;
    [SerializeField] Transform player;
    NavMeshAgent agent;
    public List<Transform> patrolPoints;
    public int spawnPointNumber;
    public EnemySpawnPointController spawnPointController;
    private Animator animator;
    private Rigidbody2D rb;
    private SliderController sliderController;
    private int enemyLife = 5;
    private int maxEnemyLife;
    private bool enemyIsAlive = true;
    private bool isAttacking = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        agent = GetComponent<NavMeshAgent>();
        sliderController = GetComponentInChildren<SliderController>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        player = GameObject.FindWithTag("Player").transform;
        maxEnemyLife = enemyLife;
        StartCoroutine("Patrol");
    }

    private void Update()
    {

        if (enemyIsAlive)
        {
            // Follow player
            if (GameController.spawnPoints[spawnPointNumber] && !isAttacking)
            {
                agent.SetDestination(player.position);
            }

            // Animations
            if (!isAttacking) 
            {
                if (agent.velocity.sqrMagnitude > 0)
                {
                    animator.SetInteger("transition", 2);
                }
                else
                {
                    animator.SetInteger("transition", 1);
                }
            }

            // Rotation
            if (agent.velocity.x < 0)
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }
            else if (agent.velocity.x > 0)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
        }

        if (spawnPointController.playerExitArea)
        {
            spawnPointController.playerExitArea = false;
            agent.SetDestination(patrolPoints[Random.Range(0, patrolPoints.Count)].position);
        }

        // Death
        if (enemyIsAlive && enemyLife <= 0)
        {
            enemyIsAlive = false;
            StartCoroutine("Death");
        }

    }

    IEnumerator Patrol()
    {
        Transform nextPoint = patrolPoints[Random.Range(0, patrolPoints.Count)];
        agent.SetDestination(nextPoint.position);
        yield return new WaitForSeconds(Random.Range(7f, 10f));
        if (enemyIsAlive)StartCoroutine("Patrol");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Magic"))
        {
            enemyLife--;
            sliderController.UpdateSliderValue(enemyLife, maxEnemyLife);
            animator.SetInteger("transition", 4);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !isAttacking)
        {
            isAttacking = true;
            StartCoroutine("Attack");
        }
    }

    IEnumerator Death()
    {
        agent.SetDestination(transform.position);
        animator.SetInteger("transition", 5);
        yield return new WaitForSeconds(1f);
        spawnPointController.enemiesAlive--;
        spawnPointController.canSpawnAnEnemy = true;
        Instantiate(soul, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    IEnumerator Attack()
    {
        agent.SetDestination(transform.position);
        animator.SetInteger("transition", 3);
        player.gameObject.GetComponent<PlayerController>()._isTakingDamage = true;
        yield return new WaitForSeconds(0.5f);
        isAttacking = false;
    }
}
