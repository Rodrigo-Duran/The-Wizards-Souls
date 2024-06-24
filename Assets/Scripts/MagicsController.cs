using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicsController : MonoBehaviour
{

    private Camera mainCamera;
    private Vector3 mousePosition;
    private Rigidbody2D magicRB;
    private float speed;

    // Awake
    void Awake()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        magicRB = GetComponent<Rigidbody2D>();
        speed = 4f;
        mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);

        Vector3 direction = mousePosition - transform.position;
        Vector3 rotation = transform.position - mousePosition;
        magicRB.velocity = new Vector2 (direction.x, direction.y).normalized * speed;
        float rotationInZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotationInZ);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Physics2D.IgnoreCollision(gameObject.GetComponent<BoxCollider2D>(), collision.gameObject.GetComponent<BoxCollider2D>());
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Physics2D.IgnoreCollision(gameObject.GetComponent<BoxCollider2D>(), collision.gameObject.GetComponent<BoxCollider2D>());
        }
        else
        {
            Destroy(gameObject);
        }
        
    }

}
