using UnityEngine;
using UnityEngine.UI;

public class ThirdPersonController_V2 : MonoBehaviour
{
    public static ThirdPersonController_V2 instance;

    public enum PlayerView { FirstPerson, ThirdPerson };
    public PlayerView playerView;

    public enum ControlsType { KeyBoardControl, SwipeControl };
    public ControlsType controlsType;

    public Transform cameraTransform, cameraParentTransform;
    public CharacterController characterController;
    public Transform circleImage;

    public float cameraSensitivity;
    public float moveSpeed, jumpSpeed, gravity;
    public float moveInputDeadZone;

    [Range(0, 1f)]
    public float StartAnimTime = 0.3f;
    [Range(0, 1f)]
    public float StopAnimTime = 0.15f;

    public bool isControllingEnabled;
    public bool isRotation;

    [Header("Jump Button")]
    public Button jumpBtn;

    [Header("Player View Data")]
    [Header("First Person Transforms")]
    [SerializeField] Vector3 cameraPosition_FP;

    [Header("Third Person Transforms")]
    [SerializeField] Vector3 cameraPosition_TP;

    [Header("Player Data")]
    public GameObject[] headParts;
    public GameObject cameraCollider;

    internal Animator anim;

    int leftFingerId, rightFingerId;
    float halfScreenWidth;

    Vector2 lookInput;
    float cameraPitch, cameraPitch_H;

    Vector2 moveTouchStartPosition;
    Vector2 moveInput;
    float animSpeed; 

    bool isJump, isAtGround;
    Vector3 jumpDirection, iniPos;
    //CharacterAudioHandler characterAudioHandler;

    //private void Awake()
    //{
    //    instance = this;
    //}

    private void OnEnable()
    {
        instance = this;
        StopRotation();
        jumpBtn.onClick.AddListener(() => Click_Jump());
    }
    private void OnDisable()
    {
        StopRotation();
    }
    // Start is called before the first frame update
    void Start()
    {
#if UNITY_EDITOR || UNITY_WEBGL
        controlsType = ControlsType.KeyBoardControl;
#else
        controlsType = ControlsType.SwipeControl;
#endif
        SetPlayerView();

        isAtGround = true;
        //characterAudioHandler = GetComponent<BNW.CharacterAudioHandler>();

        leftFingerId = -1;
        rightFingerId = -1;
        halfScreenWidth = Screen.width / 2;
        moveInputDeadZone = Mathf.Pow(Screen.height / moveInputDeadZone, 2);

        anim = this.GetComponent<Animator>();
        iniPos = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (controlsType == ControlsType.KeyBoardControl)
        {
            if (isControllingEnabled)
            {
                MoveUsingKeys();
                LookUsingKeys();

                if (Input.GetKeyDown(KeyCode.Space))
                {
                    isJump = true;
                }
                JumpControls();
            }
            else if (isRotation)
            {
                LookUsingKeys();
            }
            //else if (isMovement)
            //{
            //    MoveUsingKeys();

            //    if (Input.GetKeyDown(KeyCode.Space))
            //    {
            //        isJump = true;
            //    }
            //    JumpControls();
            //}
            else
            {
                animSpeed = 0;
                anim.SetFloat("Blend", animSpeed, StopAnimTime, Time.deltaTime);
                //SoundHandler.instance.StopSound("PlayerRunning");
                circleImage.gameObject.SetActive(false);
                leftFingerId = -1;
                rightFingerId = -1;
                //Jump
                jumpDirection.x = 0;
                jumpDirection.y = 0;
            }
        }
        else if (controlsType == ControlsType.SwipeControl)
        {
            GetTouchInput();

            if (isControllingEnabled)
            {
                if (leftFingerId != -1)
                {
                    Move();
                }
                else
                {
                    animSpeed = 0;
                    anim.SetFloat("Blend", animSpeed, StopAnimTime, Time.deltaTime);
                    //characterAudioHandler.StopSound();
                    circleImage.gameObject.SetActive(false);
                    //Jump
                    jumpDirection.x = 0;
                    jumpDirection.y = 0;
                }

                if (rightFingerId != -1)
                {
                    LookAround();
                }

                JumpControls();

            }
            else if (isRotation)
            {
                if (rightFingerId != -1)
                {
                    LookAround();
                }
            }
            else
            {
                animSpeed = 0;
                anim.SetFloat("Blend", animSpeed, StopAnimTime, Time.deltaTime);
                //characterAudioHandler.StopSound();
                circleImage.gameObject.SetActive(false);
                //Jump
                jumpDirection.x = 0;
                jumpDirection.y = 0;
            }
        }
    }

    void GetTouchInput()
    {
        for (int i = 0; i < Input.touchCount; i++)
        {
            Touch _touch = Input.GetTouch(i);

            switch (_touch.phase)
            {
                case TouchPhase.Began:
                    if (_touch.position.x < halfScreenWidth && leftFingerId == -1)
                    {
                        leftFingerId = _touch.fingerId;
                        moveTouchStartPosition = _touch.position;
                        circleImage.gameObject.SetActive(true);
                        circleImage.position = new Vector3(_touch.position.x, _touch.position.y, transform.position.z);
                    }
                    else if (_touch.position.x > halfScreenWidth && rightFingerId == -1)
                    {
                        rightFingerId = _touch.fingerId;
                    }
                    break;
                case TouchPhase.Moved:
                    if (_touch.fingerId == rightFingerId)
                    {
                        lookInput = _touch.deltaPosition * cameraSensitivity * Time.deltaTime;
                    }
                    else if (_touch.fingerId == leftFingerId)
                    {
                        moveInput = _touch.position - moveTouchStartPosition;

                        circleImage.gameObject.SetActive(true);
                        circleImage.position = new Vector3(_touch.position.x, _touch.position.y, transform.position.z);

                    }
                    break;
                case TouchPhase.Ended:
                case TouchPhase.Canceled:
                    circleImage.gameObject.SetActive(false);
                    if (_touch.fingerId == leftFingerId)
                    {
                        leftFingerId = -1;
                        animSpeed = 0;
                    }
                    else if (_touch.fingerId == rightFingerId)
                    {
                        rightFingerId = -1;
                    }
                    break;
                case TouchPhase.Stationary:
                    if (_touch.fingerId == rightFingerId)
                    {
                        lookInput = Vector2.zero;
                    }
                    break;
            }
        }
    }

    public void StopRotation()
    {
        //isControllingEnabled = false;
        leftFingerId = -1;
        rightFingerId = -1;
        lookInput = Vector2.zero;
        moveInput = Vector2.zero;
        moveTouchStartPosition = Vector2.zero;
        animSpeed = 0;
        Move();
    }

    void LookAround()
    {
        //Vertical
        cameraPitch = Mathf.Clamp(cameraPitch - lookInput.y, -90f, 90f);
        cameraTransform.localRotation = Quaternion.Euler(cameraPitch, 0, 0);

        //Horizontal
        if (isFirstPerson())
        {
            if(cameraParentTransform.localEulerAngles.y >= -70 && cameraParentTransform.localEulerAngles.y <= 80)
            {
                cameraParentTransform.Rotate(transform.up, lookInput.x);
            }
            else
            {
                cameraPitch_H = Mathf.Clamp(cameraParentTransform.localEulerAngles.y, -70, 80);
                transform.eulerAngles = new Vector3(0, cameraParentTransform.eulerAngles.y - cameraPitch_H, 0);
                cameraParentTransform.localEulerAngles = new Vector3(0, cameraPitch_H, 0);
            }
        }
        else
        {
            cameraParentTransform.Rotate(transform.up, lookInput.x);
        }
    }

    void Move()
    {
        if (moveInput.sqrMagnitude <= moveInputDeadZone)
        {
            Debug.Log("not move dir");
            return;
        }

        if (cameraParentTransform.localEulerAngles != Vector3.zero)
        {
            transform.eulerAngles = cameraParentTransform.eulerAngles;
            cameraParentTransform.localEulerAngles = Vector3.zero;
        }

        Vector2 moveDir = moveInput.normalized * moveSpeed * Time.deltaTime;
        characterController.Move(transform.right * moveDir.x + transform.forward * moveDir.y);

        Vector2 normalizeMoveInput = moveInput;
        normalizeMoveInput.Normalize();
        animSpeed = normalizeMoveInput.sqrMagnitude;
        anim.SetFloat("Blend", animSpeed, StartAnimTime, Time.deltaTime);
        //characterAudioHandler.PlayRunningSound();

        Debug.Log("Move dir");
        //jump
        jumpDirection.x = moveDir.x;
        jumpDirection.y = moveDir.y;

        RealCamera_V2.instance.isPlayerMoved = true;
    }

    public void StandPlayer()
    {
        anim.SetTrigger("stand");
    }
    //public void StopMovement()
    //{
    //    moveInput = Vector2.zero;
    //    lookInput = Vector2.zero;
    //    animSpeed = 0;
    //}

#region Jump
    void Click_Jump()
    {
        isJump = true;
    }

    void JumpControls()
    {
        if (characterController.isGrounded)
        {
            isAtGround = true;
            iniPos = transform.localPosition;
        }

        //Jump
        if (isJump && isAtGround)
        {
            isJump = false;
            isAtGround = false;
            if (transform.localPosition.y <= iniPos.y + 0.1f)
            {
                jumpDirection.z = jumpSpeed;
                anim.SetTrigger("jump");
            }
        }
        jumpDirection.z -= gravity * Time.deltaTime;
        characterController.Move(transform.right * jumpDirection.x + transform.forward * jumpDirection.y + transform.up * jumpDirection.z);
    }
#endregion

#region PlayerView 
    public void ChangePlayerView(bool isFirstPerson)
    {
        if (isFirstPerson)
        {
            playerView = PlayerView.FirstPerson;
        }
        else
        {
            playerView = PlayerView.ThirdPerson;
        }

        SetPlayerView();
    }

    void SetPlayerView()
    {
        if (isFirstPerson())
        {
            Debug.Log("FP- " + cameraPosition_FP);
            cameraTransform.localPosition = cameraPosition_FP;
            Debug.Log("Cam FP- " + cameraTransform.localPosition);
            SetPlayerHead(false);
        }
        else
        {
            cameraTransform.localPosition = cameraPosition_TP;
            SetPlayerHead(true);
        }
    }

    //To enable/disable player head based player view
    void SetPlayerHead(bool status)
    {
        foreach (var item in headParts)
        {
            item.SetActive(status);
        }
        cameraCollider.SetActive(status);
    }

    internal bool isFirstPerson()
    {
        return (playerView == PlayerView.FirstPerson);
    }

#endregion

#region Pc move controlls

    void MoveUsingKeys()
    {
        Vector2 movementAxis = Vector2.zero;

        if (!Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.DownArrow) && !Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow))
        {
            movementAxis = Vector2.zero;
        }
        else
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                movementAxis = new Vector2(movementAxis.x, 1);
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                movementAxis = new Vector2(movementAxis.x, -1);
            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                movementAxis = new Vector2(-1, movementAxis.y);
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                movementAxis = new Vector2(1, movementAxis.y);
            }
        }

        if (movementAxis.y != 0 || movementAxis.x != 0)
        {
            //Vector2 moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

            

            Vector2 moveDir = movementAxis.normalized * moveSpeed * 2 * Time.deltaTime; 
            characterController.Move(transform.right * moveDir.x + transform.forward * moveDir.y);

            //Vector3 moveFace = transform.right * moveDir.x + transform.forward * moveDir.y;
            //Debug.Log("moveFace- " + moveFace);

            //float ang = Vector3.Angle(transform.position, moveDir); 
            //transform.eulerAngles = new Vector3(0, ang, 0);

            if (cameraParentTransform.localEulerAngles != Vector3.zero)
            {
                transform.eulerAngles = cameraParentTransform.eulerAngles;
                cameraParentTransform.localEulerAngles = Vector3.zero;
            }

            Vector2 normalizeMoveInput = movementAxis;
            normalizeMoveInput.Normalize();
            animSpeed = normalizeMoveInput.sqrMagnitude * 2;
            anim.SetFloat("Blend", animSpeed, StartAnimTime, Time.deltaTime);
            //characterAudioHandler.PlayRunningSound();

            jumpDirection.x = moveDir.x;
            jumpDirection.y = moveDir.y;

            RealCamera_V2.instance.isPlayerMoved = true;
        }
        else
        {
            animSpeed = 0;
            anim.SetFloat("Blend", animSpeed, StopAnimTime, Time.deltaTime);
            //characterAudioHandler.StopSound();
            circleImage.gameObject.SetActive(false);
            leftFingerId = -1;
            rightFingerId = -1;
            //Jump
            jumpDirection.x = 0;
            jumpDirection.y = 0;
        }
    }

    void LookUsingKeys()
    {
        GetMouseMovement();

        Vector2 rotationAxis = Vector2.zero;

        if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
        {
            rotationAxis = Vector2.zero;
        }
        else
        {
            if (Input.GetKey(KeyCode.W))
            {
                rotationAxis = new Vector2(rotationAxis.x, 1);
            }
            if (Input.GetKey(KeyCode.S))
            {
                rotationAxis = new Vector2(rotationAxis.x, -1);
            }
            if (Input.GetKey(KeyCode.A))
            {
                rotationAxis = new Vector2(-1, rotationAxis.y);
            }
            if (Input.GetKey(KeyCode.D))
            {
                rotationAxis = new Vector2(1, rotationAxis.y);
            }
        }

        if (rotationAxis.x != 0 || rotationAxis.y != 0)
        {
            lookInput = new Vector2(lookInput.x + rotationAxis.x, lookInput.y + rotationAxis.y) * cameraSensitivity / 4;
        }

        //Vertical
        cameraPitch = Mathf.Clamp(cameraPitch - lookInput.y, -90f, 90f);
        cameraTransform.localRotation = Quaternion.Euler(cameraPitch, 0, 0);

        //Horizontal
        cameraParentTransform.Rotate(transform.up, lookInput.x);
    }

    void GetMouseMovement()
    {
        if (Input.GetMouseButton(1))
        {
            lookInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y")) * cameraSensitivity;
        }
        else
        {
            lookInput = Vector2.zero;
        }
    }

#endregion
}
