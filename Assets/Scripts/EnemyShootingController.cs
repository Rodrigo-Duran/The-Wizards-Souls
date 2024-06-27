using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShootingController : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private GameObject magic;
    [SerializeField] private Transform magicTransform;

    private EnemyController enemyController;
    private bool canShoot;
    private float timer;
    private float timeBetweenShooting;

    // Awake 
    void Awake()
    {
        enemyController = GetComponentInParent<EnemyController>();
        canShoot = true;
        timer = 0f;
        timeBetweenShooting = 4f;
    }

    // Update
    void Update()
    {

        Vector3 rotation = player.position - transform.position;

        float rotationInZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, 0, rotationInZ);

        if (!canShoot)
        {
            timer += Time.deltaTime;
            if (timer > timeBetweenShooting)
            {
                canShoot = true;
                timer = 0f;
            }

        }

        if (enemyController._playerIsClose && canShoot)
        {
            canShoot = false;
            Instantiate(magic, magicTransform.position, Quaternion.identity);
        }

    }
}
