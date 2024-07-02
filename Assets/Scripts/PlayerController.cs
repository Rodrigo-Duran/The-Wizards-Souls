using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    #region Variables
    //Components
    private Rigidbody2D playerRB;
    private Animator animator;
    [SerializeField] Transform playerRotatePoint;

    //Movement
    [SerializeField] private float playerSpeed = 5f;
    [SerializeField] private float jumpForce = 9f;
    private float horizontalMove;
    private bool isRunning;
    private float playerActualSpeed;
    private bool canJump;
    private bool isJumping;
    private bool isFalling;
    private bool isAttacking;

    public bool _isAttacking
    {
        get { return isAttacking; }
        set { isAttacking = value; }
    }

    //Enemy
    [SerializeField] private EnemyController enemy;

    //Player Attributes
    [SerializeField] private int maxHealth = 5;
    private int actualHealth;
    private bool playerIsDead;
    private bool isTakingDamage;

    public bool _playerIsDead
    {
        get { return playerIsDead; }
        set { playerIsDead = value; }
    }

    //HUD
    [SerializeField] Image healthBarImage;
    [SerializeField] List<Sprite> healthBarImagesList;

    #endregion

    #region MainMethods
    // Awake
    void Awake()
    {
        //Components
        playerRB = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        //Movement
        isRunning = false;
        playerActualSpeed = playerSpeed;
        canJump = true;
        isJumping = false;
        isFalling = false;
        isAttacking = false;

        //Player Attributes
        actualHealth = maxHealth;
        playerIsDead = false;
        isTakingDamage = false;

    }

    private void Update()
    {

        //HUD
        UpdateHealthImage();


        //GAME

        //CHECANDO SE PLAYER ESTA MORTO
        if (actualHealth <= 0) playerIsDead = true;

        //PEGANDO INPUT APENAS HORIZONTAL
        horizontalMove = Input.GetAxisRaw("Horizontal");

        //ROTACIONANDO O PLAYER PRO LADO DO INPUT CASO ELE ESTIVER MOVIMENTANDO
        if (horizontalMove > 0)
        {
            transform.eulerAngles = new Vector2(0, 0);
        }
        else if (horizontalMove < 0)
        {
            transform.eulerAngles = new Vector2(0, 180);
        }
        //ROTACIONANDO O PLAYER PRO LADO DO MOUSE INPUT CASO ELE ESTIVER PARADO
        else if (horizontalMove == 0)
        {
            if (playerRotatePoint.rotation.z < 0.75f && playerRotatePoint.rotation.z > -0.7f) {
                transform.eulerAngles = new Vector2(0, 0);
            }
            else
            {
                transform.eulerAngles = new Vector2(0, 180);
            }
        }

        //ANIMAÇÕES

        //Andando
        if (horizontalMove != 0 && !isRunning && !isJumping && !playerIsDead && !isTakingDamage && !isAttacking)
        {
            animator.SetInteger("transition", 2);
        }
        //Correndo
        else if (horizontalMove != 0 && isRunning && !isJumping && !playerIsDead && !isTakingDamage && !isAttacking)
        {
            animator.SetInteger("transition", 3);
        }
        //Parado
        else if (horizontalMove == 0 && !isJumping && !playerIsDead && !isTakingDamage && !isAttacking)
        {
            animator.SetInteger("transition", 1);
        }
        //SE O JOGADOR ESTIVER CAINDO, NÃO ESTIVER MORTO E NÃO ESTIVER TOMANDO DANO
        else if (isFalling && !playerIsDead && !isTakingDamage && !isAttacking)
        {
            animator.SetInteger("transition", 5);
        }
        //SE O JOGADOR ESTIVER TOMANDO DANO E NÃO ESTIVER MORTO
        else if (isTakingDamage && !playerIsDead && !isAttacking)
        {
            animator.SetInteger("transition", 6);
        }
        else if (isAttacking && !playerIsDead)
        {
            animator.SetInteger("transition", 8);
        }
        //SE O JOGADOR MORRER
        else if (playerIsDead)
        {
            animator.SetInteger("transition", 7);
        }

        //SE O JOGADOR APERTAR ESPAÇO
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //SE ELE PUDER PULAR, NÃO ESTIVER MORTO E NÃO ESTIVER TOMANDO DANO
            if (canJump && !playerIsDead && !isTakingDamage && !isAttacking)
            {
                //ele está pulando
                isJumping = true;
                //movendo o rigidbody dele pela velocidade para cima
                playerRB.velocity = new Vector2(horizontalMove, jumpForce);
                //tocando animação de pulo
                animator.SetInteger("transition", 4);
            }
            
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

    #endregion

    #region CollisionsHandler
    private void OnCollisionExit2D(Collision2D collision)
    {
        //Saindo da colisão com o chão
        if (collision.gameObject.tag == "Platforms")
        {
            //Debug.Log("EXIT COLLISION WITH GROUND");

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
            //Debug.Log("ENTER COLLISION WITH GROUND");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PointCloseToEnemy")
        {
            enemy._playerIsClose = true;
        }
        if (collision.gameObject.tag == "EnemyMagic")
        {
            if (!playerIsDead)
            {
                StartCoroutine(TakeDamage());
            }
        }
    }

    #endregion

    #region CombatHandler

    IEnumerator TakeDamage()
    {
        Debug.Log("Player was hitted by Enemy magic");
        //perde 1 coração de vida
        actualHealth -= 1;
        //esta tomando dano
        isTakingDamage = true;
        //espera até a animação tocar - tempo da animação é de 0.3f
        //animator.SetInteger("transition", 6);
        yield return new WaitForSeconds(0.4f);
        //não esta mais tomando dano
        isTakingDamage = false;
        Debug.Log(isTakingDamage);
    }

    #endregion

    #region HUDHandler

    void UpdateHealthImage()
    {
        switch (actualHealth)
        {
            case 0:
                healthBarImage.sprite = healthBarImagesList[0];
                break;

            case 1:
                healthBarImage.sprite = healthBarImagesList[1];
                break;

            case 2:
                healthBarImage.sprite = healthBarImagesList[2];
                break;

            case 3:
                healthBarImage.sprite = healthBarImagesList[3];
                break;

            case 4:
                healthBarImage.sprite = healthBarImagesList[4];
                break;

            case 5:
                healthBarImage.sprite = healthBarImagesList[5];
                break;
        }
    }

    #endregion
}
