using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    [SerializeField] private float playerSpeed = 4f;
    [SerializeField] private float jumpForce = 8f;
    private float horizontalMove;
    private bool isRunning;
    private float playerActualSpeed;
    private bool canJump;
    private bool isJumping;
    private bool isFalling;
    private bool isAttacking;
    private bool isVulnerable = false;

    public bool _isAttacking
    {
        get { return isAttacking; }
        set { isAttacking = value; }
    }

    //Player Attributes
    [SerializeField] private int maxHealth = 5;
    private Vector2 initialPosition;
    private int actualHealth;
    private bool playerIsDead;
    private bool isTakingDamage;

    public bool _playerIsDead
    {
        get { return playerIsDead; }
        set { playerIsDead = value; }
    }

    public bool _isTakingDamage
    {
        get { return isTakingDamage; }
        set { isTakingDamage = value; }
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
        initialPosition = transform.position;
        actualHealth = maxHealth;
        playerIsDead = false;
        isTakingDamage = false;

    }

    private void Update()
    {
        if (GameController.gameIsOn) {
            //HUD
            UpdateHealthImage();

            //GAME
            //CHECANDO SE PLAYER ESTA MORTO
            if (actualHealth <= 0)
            {
                playerIsDead = true;
                StartCoroutine("Death");
            }


            //PEGANDO INPUT APENAS HORIZONTAL
            if (!playerIsDead) horizontalMove = Input.GetAxisRaw("Horizontal");

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

            // Andando
            if (horizontalMove != 0 && !isRunning && !isJumping && !playerIsDead && !isTakingDamage && !isAttacking && !isFalling)
            {
                animator.SetInteger("transition", 2);
            }
            // Correndo
            else if (horizontalMove != 0 && isRunning && !isJumping && !playerIsDead && !isTakingDamage && !isAttacking && !isFalling)
            {
                animator.SetInteger("transition", 3);
            }
            // Parado
            else if (horizontalMove == 0 && !isJumping && !playerIsDead && !isTakingDamage && !isAttacking && !isFalling)
            {
                animator.SetInteger("transition", 1);
            }
            // Caindo
            else if (isFalling && !playerIsDead && !isTakingDamage && !isAttacking)
            {
                animator.SetInteger("transition", 5);
            }
            // Tomando dano
            /*else if (isTakingDamage && !playerIsDead && !isAttacking)
            {
                animator.SetInteger("transition", 6);
            }*/
            // Atacando
            else if (isAttacking && !playerIsDead)
            {
                animator.SetInteger("transition", 8);
            }
            // Morrendo
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
            if (Input.GetKey(KeyCode.LeftShift) && !isJumping)
            {
                //ele está correndo, consequentemente aumenta-se sua velocidade
                isRunning = true;
                playerActualSpeed = playerSpeed * 3;
            }
            //SE O JOGADOR NÃO TIVER APERTANDO O SHIFT ESQUERDO
            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                //ele não está correndo, portanto retorna a velocidade padrão
                isRunning = false;
                playerActualSpeed = playerSpeed;
            }

            // Tomando dano
            if (isTakingDamage && !playerIsDead && !isVulnerable)
            {
                isVulnerable = true;
                StartCoroutine("TakeDamage");
            }
        }
    }

    // FixedUpdate
    void FixedUpdate()
    {
        //Movendo o Rigidbody pela velocidade dele
        playerRB.velocity = new Vector2(horizontalMove * playerActualSpeed , playerRB.velocity.y);

    }

    #endregion

    #region CollisionsHandler
    private void OnCollisionExit2D(Collision2D collision)
    {
        //Saindo da colisão com o chão
        if (collision.gameObject.CompareTag("Platforms"))
        {
            //Debug.Log("EXIT COLLISION WITH GROUND");

            //não pode pular
            canJump = false;
            
            //Se ele não estiver pulando, então ele está caindo
            if (!isJumping)
            {
                isFalling = true;
                Debug.Log("IS FALLING");
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Entrando em colisão com o chão
        if (collision.gameObject.CompareTag("Platforms"))
        {
            //ele pode pular, não esta mais pulando e nem caindo
            canJump = true;
            isJumping = false;
            isFalling = false;
            //Debug.Log("ENTER COLLISION WITH GROUND");
        }

        if (collision.gameObject.CompareTag("Lava"))
        {
            isTakingDamage = true;
            transform.position = initialPosition;
        }

        if (collision.gameObject.CompareTag("Soul"))
        {
            GameController.numberOfSoulsCollected++;
            Destroy(collision.gameObject);
        }
    }

  /*  private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "EnemyMagic")
        {
            if (!playerIsDead)
            {
                StartCoroutine(TakeDamage());
            }
        }
    }*/

    #endregion

    #region CombatHandler

    IEnumerator TakeDamage()
    {
        Debug.Log("Player was hitted by an Enemy ");
        //perde 1 coração de vida
        actualHealth -= 1;
        //esta tomando dano
        //isTakingDamage = true;
        //espera até a animação tocar - tempo da animação é de 0.3f
        animator.SetInteger("transition", 6);
        yield return new WaitForSeconds(1f);
        //não esta mais tomando dano
        isTakingDamage = false;
        isVulnerable = false;
        //Debug.Log(isTakingDamage);
    }

    IEnumerator Death()
    {
        playerIsDead = true;
        animator.SetInteger("transition", 7);
        yield return new WaitForSeconds(1f);
        GameController.playerDied = true;
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
