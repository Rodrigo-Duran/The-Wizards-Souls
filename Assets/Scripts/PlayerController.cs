using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Components
    private Rigidbody2D playerRB;
    private Animator animator;

    //Movement
    private float horizontalMove;
    [SerializeField] private float playerSpeed = 5f;
    [SerializeField] private float jumpForce = 9f;
    private bool isRunning;
    private float playerActualSpeed;
    private bool canJump;
    private bool isJumping;
    private bool isFalling;

    [SerializeField] private EnemyController enemy;

    // Awake
    void Awake()
    {
        playerRB = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        isRunning = false;
        playerActualSpeed = playerSpeed;
        canJump = true;
        isJumping = false;
        isFalling = false;
    }

    private void Update()
    {
        //PEGANDO INPUT APENAS HORIZONTAL
        horizontalMove = Input.GetAxisRaw("Horizontal");

        //ROTACIONANDO O PLAYER PRO LADO DO INPUT
        if (horizontalMove > 0)
        {
            transform.eulerAngles = new Vector2(0, 0);
        }
        else if (horizontalMove < 0)
        {
            transform.eulerAngles = new Vector2(0, 180);
        }

        //ANIMAÇÕES

        //Andando
        if (horizontalMove != 0 && !isRunning && !isJumping)
        {
            animator.SetInteger("transition", 2);
        }
        //Correndo
        else if (horizontalMove != 0 && isRunning && !isJumping)
        {
            animator.SetInteger("transition", 3);
        }
        else if (horizontalMove == 0 && !isJumping)
        {
            animator.SetInteger("transition", 1);
        }

        //SE O JOGADOR APERTAR ESPAÇO
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //SE ELE PUDER PULAR
            if (canJump)
            {
                //ele está pulando
                isJumping = true;
                //movendo o rigidbody dele pela velocidade para cima
                playerRB.velocity = new Vector2(horizontalMove, jumpForce);
                //tocando animação de pulo
                animator.SetInteger("transition", 4);
            }
            
        }

        //SE O JOGADOR ESTIVER CAINDO
        if (isFalling)
        {
            animator.SetInteger("transition", 5);
        }

        //SE O JOGADOR MANTER APERTADO O SHIFT ESQUERDO
        if (Input.GetKey(KeyCode.LeftShift))
        {
            //ele está correndo, consequentemente aumenta-se sua velocidade
            isRunning = true;
            playerActualSpeed = playerSpeed * 3;
        }
        //SE O JOGADOR NÃO TIVER APERTANDO O SHIFT ESQUERDO
        else
        {
            //ele não está correndo, portanto retorna a velocidade padrão
            isRunning = false;
            playerActualSpeed = playerSpeed;
        }

        //PAUSE

        //SE O JOGADOR APERTAR ESC
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //PAUSAR JOGO
            Time.timeScale = 0f;
        }

        //SE O JOGADOR APERTAR M
        if (Input.GetKeyDown(KeyCode.M))
        {
            //RETOMAR JOGO
            Time.timeScale = 1f;
        }
    }

    // FixedUpdate
    void FixedUpdate()
    {
        //Movendo o Rigidbody pela velocidade dele
        playerRB.velocity = new Vector2(horizontalMove * playerActualSpeed, playerRB.velocity.y);

    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        //Saindo da colisão com o chão
        if (collision.gameObject.tag == "Platforms")
        {
            Debug.Log("EXIT COLLISION WITH GROUND");

            //não pode pular
            canJump = false;
            
            //Se ele não estiver pulando, então ele está caindo
            if (!isJumping)
            {
                isFalling = true;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Entrando em colisão com o chão
        if (collision.gameObject.tag == "Platforms")
        {
            //ele pode pular, não esta mais pulando e nem caindo
            canJump = true;
            isJumping = false;
            isFalling = false;
            Debug.Log("ENTER COLLISION WITH GROUND");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PointCloseToEnemy")
        {
            enemy._playerIsClose = true;
        }
    }
}
