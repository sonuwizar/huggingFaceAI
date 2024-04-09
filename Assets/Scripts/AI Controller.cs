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
    private float jumpSpeed = 5f;
    private float gravity = 9.81f;
    public float moveInputDeadZone;
    public float moveSpeed = 5f;
    private float runSpeed = 10f;
    Vector2 moveInput;

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

        Debug.Log("Characteremove");
        transform.Translate(Vector3.forward * movement);
        Debug.Log("Characteremove");
    }


    public void  moveleft()
    {
        float movement = moveSpeed * Time.deltaTime;
        transform.Translate(Vector3.left * movement);
        Debug.Log("Characteremove");
    }

    public void moveright()
    {
        float movement = moveSpeed * Time.deltaTime;
        transform.Translate(Vector3.right * movement);
        Debug.Log("Characteremove");
    }

    public void stop()
    {
       
        float movement = moveSpeed * Time.deltaTime;
        moveSpeed = 0f;
        // Move the object along the Z-axis
        transform.Translate(Vector3.forward * movement);

        Debug.Log("Characteremove");


    }
    public float jumpForce = 10f;
    public float jumpInterval = 2f;
    private Rigidbody rb;
    private void JumpCharacter()
    {
        Debug.Log("JumpCharacter");
        rb.AddForce(new Vector3(0f, jumpForce),ForceMode.Impulse);
       
    }
       // 
        //if (characterController.isGrounded)
        //{
        //    isAtGround = true;
        //    iniPos = transform.localPosition;
        //}

        //if (isJump && isAtGround)
        //{
        //    isJump = false;
        //    isAtGround = false;
        //    if (transform.localPosition.y <= iniPos.y + 0.1f)
        //    {
        //        jumpDirection.z = jumpSpeed;
        //        anim.SetTrigger("jump");
        //    }
        //}
        //jumpDirection.z -= gravity * Time.deltaTime;
        //characterController.Move(transform.right * jumpDirection.x + transform.forward * jumpDirection.y + transform.up * jumpDirection.z);
 
    public void RunCharacter()
    {

    }
}

