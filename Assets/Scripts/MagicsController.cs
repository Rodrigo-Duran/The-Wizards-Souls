using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicsController : MonoBehaviour
{

    private Camera mainCamera;
    private Vector3 mousePosition;
    private Rigidbody2D magicRB;
    private float speed;
    private Transform player;
    private float playerInitialPositionX;
    private float playerInitialPositionY;

    // Awake
    void Awake()
    {
        //pegando componentes
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        magicRB = GetComponent<Rigidbody2D>();
        mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);

        //setando velocidade da magia pra 8
        speed = 8f;

        //setando a posicao x e y do player no momento da criação da magia
        playerInitialPositionX = player.position.x;
        playerInitialPositionY = player.position.y;

        //direção e rotação que a magia tem que seguir
        Vector3 direction = mousePosition - transform.position;
        Vector3 rotation = transform.position - mousePosition;
        
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
        
        if (collision.gameObject.tag == "Player")
        {
            //ignorar colisao com player
            Physics2D.IgnoreCollision(gameObject.GetComponent<BoxCollider2D>(), collision.gameObject.GetComponent<BoxCollider2D>());
        }
        else
        {
            //caso não colida com o player, destruir o objeto
            Destroy(gameObject);
        }
    }


}
