using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AIController : MonoBehaviour
{
    public static AIController instance;
   // public float moveSpeed = 5f;
    public State state;
    public Transform cameraTransform, cameraParentTransform;
    private CharacterController characterController;
    private Animator anim;

    private bool isJump = false;
    private bool isAtGround = true;
    private Vector3 iniPos;
    private Vector3 jumpDirection = Vector3.zero;
    private Vector3 moveDirection = Vector3.zero;
    private float jumpSpeed = 3f;
    private float gravity = 9.81f;
    public float moveInputDeadZone;
    public float moveSpeed = 3f;
    private float runSpeed = 10f;
    Vector2 moveInput;
    public Animator ani;

    public enum State
    {
        Idle,
        left,
        right,
        Stop,
        Jump,
        Move
    }

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();

        rb = GetComponent<Rigidbody>();
        InvokeRepeating("Jump", 0f, jumpInterval);
    }

    private void Update()
    {
        switch (state)
        {
            case State.Idle:
                Debug.Log("State Idle");
                break;

            case State.left:
                moveleft();
                break;

            case State.right:
                moveright();
                break;

            case State.Stop:
                stop();
                break;
            case State.Move:
                MoveCharacter();                
                break;

            case State.Jump:
                JumpCharacter();
                break;
        }
    }
   
    private void MoveCharacter()
    {
        float movement = moveSpeed * Time.deltaTime;

        anim.SetBool("walk", true);
        transform.Translate(Vector3.forward * movement);
        Debug.Log("move");
    }


    public void  moveleft()
    {
        float movement = moveSpeed * Time.deltaTime;
        anim.SetBool("walk", true);
        transform.Translate(Vector3.left * movement);
        Debug.Log("move left");
    }

    public void moveright()
    {
        float movement = moveSpeed * Time.deltaTime;
        anim.SetBool("walk", true);
        transform.Translate(Vector3.right * movement);
        Debug.Log("move right");
    }

    public void stop()
    {
       
        float movement = moveSpeed * Time.deltaTime;
        moveSpeed = 0f;
        anim.SetBool("walk", false);
        transform.Translate(Vector3.forward * movement);

        Debug.Log("stop");


    }
    public float jumpForce = 10f;
    public float jumpInterval = 2f;
    private Rigidbody rb;
    private float jumpMaxHeight;

    private void JumpCharacter()
    {
        Debug.Log("JumpCharacter");
        anim.SetBool("jump", true);
        if (transform.position.y <= jumpMaxHeight)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

    }
       
 
    public void RunCharacter()
    {

    }
}

