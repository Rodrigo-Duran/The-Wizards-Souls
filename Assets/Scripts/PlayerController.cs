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
    [SerializeField] private float playerSpeed;
    [SerializeField] private float jumpForce;
    private bool isRunning;
    private float playerActualSpeed;
    private bool canJump;
    private bool isJumping;

    // Awake
    void Awake()
    {
        playerRB = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        isRunning = false;
        playerActualSpeed = playerSpeed;
        canJump = true;
        isJumping = false;
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

            //CHECANDO SE ELE PODE PULAR
            if (canJump)
            {
                isJumping = true;
                playerRB.velocity = new Vector2(horizontalMove, jumpForce);
                animator.SetInteger("transition", 4);
            }
            
        }

        //SE O JOGADOR MANTER APERTADO O SHIFT ESQUERDO
        if (Input.GetKey(KeyCode.LeftShift))
        {
            isRunning = true;
            playerActualSpeed = playerSpeed * 3;
        }
        //SE O JOGADOR NÃO TIVER APERTANDO O SHIFT ESQUERDO
        else
        {
            isRunning = false;
            playerActualSpeed = playerSpeed;
        }

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

        playerRB.velocity = new Vector2(horizontalMove * playerActualSpeed, playerRB.velocity.y);

    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            canJump = false;
            Debug.Log("EXIT COLLISION WITH GROUND");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            canJump = true;
            isJumping = false;
            Debug.Log("ENTER COLLISION WITH GROUND");
        }
    }
}
