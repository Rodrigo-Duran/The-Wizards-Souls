using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnPointController : MonoBehaviour
{
    [SerializeField] private List<Transform> patrolPoints = new List<Transform>();
    [SerializeField] private int spawnPointNumber;
    public int enemiesAlive;
    public bool canSpawnAnEnemy;
    [SerializeField] private GameObject enemy;
    public bool playerExitArea = false;

    private void Start()
    {
        enemiesAlive = 0;
        canSpawnAnEnemy = true;
    }

    private void Update()
    {
        if (canSpawnAnEnemy && enemiesAlive <= 0)
        {
            canSpawnAnEnemy = false;
            StartCoroutine("InstantiateEnemy");
        }
    }

    public List<Transform> GetPatrolPoints()
    {
        return patrolPoints;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameController.spawnPoints[spawnPointNumber] = true;
            Debug.Log("Player enter the area");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameController.spawnPoints[spawnPointNumber] = false;
            playerExitArea = true;
            Debug.Log("Player exit the area");
        }
    }

    IEnumerator InstantiateEnemy()
    {
        enemy.GetComponent<EnemyController>().patrolPoints = patrolPoints;
        enemy.GetComponent<EnemyController>().spawnPointNumber = spawnPointNumber;
        enemy.GetComponent<EnemyController>().spawnPointController = this;
        yield return new WaitForSeconds(3f);
        Instantiate(enemy, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
        enemiesAlive++;
    }
}
