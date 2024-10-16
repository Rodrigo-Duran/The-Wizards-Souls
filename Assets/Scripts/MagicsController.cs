using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MagicsController : MonoBehaviour
{

    private Camera mainCamera;
    private Transform player;

    private Animator animator;
    private Rigidbody2D magicRB;
    private ShootingController shootingController;

    private Vector3 mousePosition;
    private float speed;
    private float playerInitialPositionX;
    private float playerInitialPositionY;
    private Vector3 direction;
    private Vector3 rotation;

    // Awake
    void Awake()
    {
        //pegando componentes
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        magicRB = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        shootingController = GameObject.FindWithTag("PlayerRotatePoint").GetComponent<ShootingController>();
        //mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mousePosition = shootingController.mousePositionInShooting;

        //setando velocidade da magia pra 8
        speed = 16f;

        //setando a posicao x e y do player no momento da criação da magia
        playerInitialPositionX = player.position.x;
        playerInitialPositionY = player.position.y;

        //---------------------

        //direção e rotação que a magia tem que seguir
        //se ela for solta pelo player
        if (gameObject.tag == "Magic")
        {
            direction = mousePosition - transform.position;
            rotation = mousePosition - transform.position;
        }
        else if (gameObject.tag == "EnemyMagic")
        {
            direction = player.transform.position - transform.position;
            rotation = player.transform.position - transform.position;
        }
        

        //----------------------

        //movendo a magia pelo seu rigidbody pra certa direção e em certa rotação
        magicRB.velocity = new Vector2 (direction.x, direction.y).normalized * speed;
        float rotationInZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotationInZ);

    }

    //Update
    private void Update()
    {
        //atualizando por frame a distancia entre a magia e a posicao inicial do player
        float distanceX = gameObject.transform.position.x - playerInitialPositionX;
        float distanceY = gameObject.transform.position.y - playerInitialPositionY;

        //se essa distancia ultrapassar 50 pra qualquer lado, destuir o objeto
        if (distanceX > 50 ||  distanceY > 50 || distanceX < -50 || distanceY < -50)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //se a magia for solta pelo player
        if (gameObject.tag == "Magic")
        {
            if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Magic") || collision.gameObject.CompareTag("CameraBounds") || collision.gameObject.CompareTag("EnemySpawnPoint"))
            {

                //ignorar colisao com player
                Physics2D.IgnoreCollision(gameObject.GetComponent<PolygonCollider2D>(), collision.gameObject.GetComponent<PolygonCollider2D>());
            }
            else
            {
                //caso não colida com esses objetos, explodir a magia
                StartCoroutine(Exploding());
            }
        }
        /*//se a magia for solta pelo inimigo
        else if (gameObject.tag == "EnemyMagic")
        {
            if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("EnemyMagic") || collision.gameObject.CompareTag("CameraBounds") || collision.gameObject.CompareTag("EnemySpawnPoint"))
            {
                //ignorar colisao com inimigo
                Physics2D.IgnoreCollision(gameObject.GetComponent<PolygonCollider2D>(), collision.gameObject.GetComponent<PolygonCollider2D>());
            }
            else
            {
                //caso não colida com esses objetos, explodir a magia
                StartCoroutine(Exploding());
            }
        }*/
        
    }

    IEnumerator Exploding()
    {
        magicRB.velocity = new Vector2(0,0).normalized;
        animator.Play("Exploding");
        yield return new WaitForSeconds(0.6f);
        Destroy(gameObject);
    }

}
