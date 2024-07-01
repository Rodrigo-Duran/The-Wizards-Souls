using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingController : MonoBehaviour
{

    [SerializeField] private Camera mainCamera;
    [SerializeField] private GameObject magic;
    [SerializeField] private Transform magicTransform;

    private PlayerController playerController;
    private Vector3 mousePosition;
    private bool canShoot;
    private float timer;
    private float timeBetweenShooting;

    // Awake 
    void Awake()
    {
        playerController = GetComponentInParent<PlayerController>();
        canShoot = true;
        timer = 0f;
        timeBetweenShooting = 0.6f;
    }

    // Update
    void Update()
    {
        mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        //Debug.Log(mousePosition);
        
        Vector3 rotation = mousePosition - transform.position;

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

        if (Input.GetMouseButton(0) && canShoot)
        {
            /*canShoot = false;
            Instantiate(magic, magicTransform.position, Quaternion.identity);*/
            StartCoroutine(Attacking());

        }

    }

    IEnumerator Attacking()
    {
        canShoot = false;
        playerController._isAttacking = true;
        yield return new WaitForSeconds(0.3f);
        Instantiate(magic, magicTransform.position, Quaternion.identity);
        yield return new WaitForSeconds(0.2f);
        playerController._isAttacking = false;
    }
}
