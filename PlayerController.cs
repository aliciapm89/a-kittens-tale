using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    GameManager manager;

    ArrowController arrowController;

    PlayerInput playerInput;

    Rigidbody2D rb2d;

    Vector2 movement;
    Vector2 jumpOffset;

    BoxCollider2D coll2D;

    Animator anim;

    public bool isRunning = false;

    public bool jumping = false;
    public bool jumpingToPos = false;

    public bool isInArea1 = false;
    public bool isInArea2 = false;

    public bool isHit1;
    public bool isHit2;

    public bool speaking = false;
    bool selecting = false;

    public bool isMom1 = false;
    public bool isEmo1 = false;
    public bool isEmo2 = false;

    Vector2 startPos;
    Vector2 jumpPos;

    float currentYpos;
    float jumpSpeed;

    float currentJumpTime;
    float jumpTime = .4f;

    void Start()
    {
        manager = GameObject.Find("Manager").GetComponent<GameManager>();
        coll2D = transform.Find("Player_Sprite").GetComponent<BoxCollider2D>();
        arrowController = transform.Find("Arrow").GetComponent<ArrowController>();

        playerInput = GetComponent<PlayerInput>();
        rb2d = GetComponent<Rigidbody2D>();
        anim = transform.Find("Player_Sprite").GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        movement = playerInput.actions["Movement"].ReadValue<Vector2>();

        if (!speaking && !selecting)
            Move();
        SelectCheck();
    }

    void Update()
    {
        //Move();
    }

    void Move()
    {
        if (jumping)
        {
            if (jumpSpeed <= -manager.PlayerJumpForce)
            {
                jumping = false;
                rb2d.MovePosition(new Vector2(rb2d.transform.position.x, currentYpos));
                jumpOffset = Vector2.zero;
            }
            else
            {
                jumpOffset = new Vector2(0, jumpSpeed);
                jumpSpeed -= manager.PlayerJumpGravityScale;
            }
        }
        else
        {
            jumpOffset = Vector2.zero;
        }

        if (isRunning)
        {
            rb2d.MovePosition(rb2d.position + jumpOffset + movement * manager.PlayerRunSpeed);
            anim.speed = 2;
        }
        else
        {
            rb2d.MovePosition(rb2d.position + jumpOffset + movement * manager.PlayerWalkSpeed);
            anim.speed = 1;
        }

        var dir = Vector3.zero;

        if (movement.x != 0 || movement.y != 0)
        {
            anim.SetFloat("X", movement.x);
            anim.SetFloat("Y", movement.y);

            anim.SetBool("IsWalking", true);
        }
        else
        {
            anim.SetBool("IsWalking", false);
        }

        dir.x = anim.GetFloat("X");
        dir.y = anim.GetFloat("Y");

        RaycastHit2D hit1 = Physics2D.Raycast(transform.position, dir, 1.3f, LayerMask.GetMask("jump1"));
        RaycastHit2D hit2 = Physics2D.Raycast(transform.position, dir, 1.3f, LayerMask.GetMask("jump2"));

        if (hit1.collider != null)
            isHit1 = true;
        else
            isHit1 = false;

        if (hit2.collider != null)
            isHit2 = true;
        else
            isHit2 = false;

        if (isHit1 && !isHit2)
        {
            Debug.DrawRay(transform.position, dir * 1.3f, Color.red);
        }
        else if (!isHit1 && isHit2)
        {
            Debug.DrawRay(transform.position, dir * 1.3f, Color.yellow);
        }
        else if (isHit1 && isHit2)
        {
            Debug.DrawRay(transform.position, dir * 1.3f, Color.blue);
        }
        else
        {
            Debug.DrawRay(transform.position, dir * 1.3f, Color.black);
        }
    }

    void SelectCheck() //Se llama desde Update. Checkea si el jugador debe de tomar una opcion
    {
        if (!speaking && isMom1 && selecting) //Se ejecuta si el jugador tiene que decidir una opcion con la madre
        {
            selecting = true;
            manager.MotherSelection1.gameObject.SetActive(true);
        }

        if (!speaking && isEmo1 && selecting)
        {
            selecting = true;
            manager.EmoSelection1.gameObject.SetActive(true);
        }
    }

    public void SelectionStatus(bool value)
    {
        selecting = value;
    }

    public void Run(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.started && !speaking && !selecting)
        {
            isRunning = true;
        }
        if (callbackContext.canceled && !speaking && !selecting)
        {
            isRunning = false;
        }
    }

    public void Jump(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.started && !speaking && !selecting)
        {
            if (isInArea1 && isHit2)
            {
                if (!jumping)
                {
                    currentYpos = transform.Find("Player_Sprite").transform.position.y;
                    jumpSpeed = manager.PlayerJumpForce;
                    rb2d.MovePosition(new Vector2(rb2d.transform.position.x, rb2d.transform.position.y + .1f));
                    jumping = true;
                    StartCoroutine(JumpToPos());
                }
            }

            if (isInArea2 && isHit1 && !speaking && !selecting)
            {
                if (!jumping)
                {
                    currentYpos = transform.Find("Player_Sprite").transform.position.y;
                    jumpSpeed = manager.PlayerJumpForce;
                    rb2d.MovePosition(new Vector2(rb2d.transform.position.x, rb2d.transform.position.y + .1f));
                    jumping = true;
                    StartCoroutine(JumpToPos());
                }
            }

            if (!jumping && !speaking && !selecting)
            {
                currentYpos = transform.Find("Player_Sprite").transform.position.y;
                jumpSpeed = manager.PlayerJumpForce;
                rb2d.MovePosition(new Vector2(rb2d.transform.position.x, rb2d.transform.position.y + .1f));
                jumping = true;
            }
        }
    }

    //En desuso

    IEnumerator JumpToPos()
    {
        anim.SetTrigger("jump");
        coll2D.enabled = false;
        startPos = transform.position;
        jumpingToPos = true;
        currentJumpTime = 0;

        while (currentJumpTime < jumpTime && jumpingToPos)
        {
            float perc = currentJumpTime / jumpTime;
            currentJumpTime += Time.deltaTime;
            transform.position = Vector2.Lerp(startPos, jumpPos, perc);

            if (currentJumpTime >= jumpTime)
            {
                currentJumpTime = jumpTime;
                jumpingToPos = false;
                coll2D.enabled = true;
                anim.SetTrigger("noJump");
                jumpOffset = Vector2.zero;
            }

            yield return null;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("jumpArea1"))
        {
            isInArea1 = true;
            jumpPos = new Vector2(collision.transform.parent.Find("Jump_Position_2").position.x, collision.transform.parent.Find("Jump_Position_2").position.y);
        }

        if (collision.gameObject.CompareTag("jumpArea2"))
        {
            isInArea2 = true;
            jumpPos = new Vector2(collision.transform.parent.Find("Jump_Position_1").position.x, collision.transform.parent.Find("Jump_Position_1").position.y);
        }

        if (collision.gameObject.CompareTag("ChildColl") && manager.childFound)
        {
            manager.ComponentMove("Child", manager.ChildNewPosition);
        }

        if (collision.CompareTag("FruitArea") && manager.emoMissionStarted && !manager.fruitFound)
        {
            arrowController.ActiveValue(false);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("jumpArea1"))
        {
            isInArea1 = false;
        }

        if (collision.gameObject.CompareTag("jumpArea2"))
        {
            isInArea2 = false;
        }

        if (collision.CompareTag("FruitArea") && manager.emoMissionStarted && !manager.fruitFound)
        {
            arrowController.ActiveValue(true);
        }
    }

    public void Interact(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.started)
        {
            print("start");

            NPCcollider nCollider = transform.Find("Player_Sprite").transform.Find("Interaction_Detector").GetComponent<NPCcollider>();

            //Mira si el objeto interactuable es un NPC y si el juador no esta hablando
            if (nCollider.isNPC && !speaking)
            {
                nCollider.interactable.GetComponent<NPC>().ConvoStart();
            }

            if (nCollider.isFruitTree && manager.emoMissionStarted)
            {
                nCollider.interactable.transform.Find("Fruta").GetComponent<Fruit>().FruitFall();
            }
        }
    }

    public void Pause(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.started)
            manager.ReturnToMenu();
    }

    public void DisableSelections(int num)
    {
        //1: isMom1
        //2: isEmo1
        //3: isEmo2
        if (num == 1)
            isMom1 = false;
        if (num == 2)
            isEmo1 = false;
        if (num == 3)
            isEmo2 = false;
    }
}
