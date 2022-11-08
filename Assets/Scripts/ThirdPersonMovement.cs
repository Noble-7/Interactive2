using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class ThirdPersonMovement : MonoBehaviour
{

    [SerializeField]
    private InputActionReference movementControl;
    [SerializeField]
    private InputActionReference jumpControl;
    [SerializeField]
    private InputActionReference colorControl;
    [SerializeField]
    private InputActionReference shootControl;
    [SerializeField]
    private InputActionReference mousePosControl;
    [SerializeField]
    private InputActionReference sprintControl;
    [SerializeField]
    private float playerSpeed = 7.5f;
    [SerializeField]
    private float jumpHeight = 0.1f;
    [SerializeField]
    private float gravityValue = -9.81f;
    [SerializeField]
    private float rotationSpeed = 4.00f;

    private CharacterController controller;

    [SerializeField]
    private Vector3 playerVelocity;

    [SerializeField]
    private bool groundedPlayer;

    [Header("Parenting")]
    public Transform parent;

    //[SerializeField]
    //private ParticleSystem poof;


    private Transform cameraMain;
    private Animator animator;
    private bool usedDoubleJump;
    [SerializeField]
    private bool isSprinting;



    [SerializeField]
    public Material[] colours;

    public GameObject bulletRef;
    public Camera characterCamera;
    public Vector3 clickPosition;


    private void OnEnable()
    {
        movementControl.action.Enable();
        jumpControl.action.Enable();
        colorControl.action.Enable();
        shootControl.action.Enable();
        mousePosControl.action.Enable();
        sprintControl.action.Enable();



    }

    private void OnDisable()
    {
        movementControl.action.Disable();
        jumpControl.action.Disable();
        colorControl.action.Disable();
        shootControl.action.Disable();
        mousePosControl.action.Disable();
        sprintControl.action.Disable();

    }

    private void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
        cameraMain = Camera.main.transform;
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        

        playerVelocity.y += gravityValue * Time.deltaTime;

        if (groundedPlayer)
        {
            playerVelocity.y = -1.0f;
        }

        Vector2 movement = movementControl.action.ReadValue<Vector2>();

        Vector3 move = new Vector3(movement.x, 0, movement.y);

        move = cameraMain.forward * move.z + cameraMain.right * move.x;
        move.y = 0.0f;

        controller.Move(move * Time.deltaTime * playerSpeed);




        // Changes the height position of the player..
        if (jumpControl.action.triggered && groundedPlayer)
        {          
              playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
                

            
        }

        if(jumpControl.action.triggered && !groundedPlayer)
        {
            if (!usedDoubleJump)
            {
                playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
                usedDoubleJump = true;
            }
        }

        if (jumpControl.action.WasReleasedThisFrame())
        {
            playerVelocity.y = -1.0f;
        }

        if (sprintControl.action.triggered)
        {
            isSprinting = true;
            playerSpeed = 10.0f;
            //Debug.Log("Pressed sprint");
        }
        if (sprintControl.action.WasReleasedThisFrame())
        {
            isSprinting = false;
            playerSpeed = 7.5f;
            //Debug.Log("Released sprint");

        }
        if (isSprinting)
        {
            playerSpeed = 10.0f;
            //Debug.Log("Holding sprint");
        }

        //if (colorControl.action.triggered)
        //{


        //    //gameObject.GetComponentInChildren<Renderer>().material = colours[0];
        //    Debug.Log("Color Changed");

        //    switch (currentIndex)
        //    {
        //        case 0:
        //            gameObject.GetComponentInChildren<Renderer>().material = colours[1];
        //            ParticleSystem poof = GetComponentInChildren<ParticleSystem>();
        //            ParticleSystem.EmitParams emitOverride = new ParticleSystem.EmitParams();
        //            emitOverride.startLifetime = 10f;
        //            poof.Emit(emitOverride, 20);

        //            currentIndex = 1;
        //            return;
        //        case 1:
        //            gameObject.GetComponentInChildren<Renderer>().material = colours[2];
        //            ParticleSystem poof2 = GetComponentInChildren<ParticleSystem>();
        //            ParticleSystem.EmitParams emitOverride2 = new ParticleSystem.EmitParams();
        //            emitOverride2.startLifetime = 10f;
        //            poof2.Emit(emitOverride2, 20);

        //            currentIndex = 2;
        //            return;
        //        case 2:
        //            gameObject.GetComponentInChildren<Renderer>().material = colours[0];
        //            ParticleSystem poof3 = GetComponentInChildren<ParticleSystem>();
        //            ParticleSystem.EmitParams emitOverride3 = new ParticleSystem.EmitParams();
        //            emitOverride3.startLifetime = 10f;
        //            poof3.Emit(emitOverride3, 20);

        //            currentIndex = 0;
        //            return;


        //    }

        //}

 
        controller.Move(playerVelocity * Time.deltaTime);

        if(movement != Vector2.zero)
        {
            float targetAngle = Mathf.Atan2(movement.x, movement.y) * Mathf.Rad2Deg + cameraMain.transform.eulerAngles.y;
            Quaternion rotation = Quaternion.Euler(0.0f, targetAngle, 0.0f);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * rotationSpeed);
            //animator.SetInteger("animState", 1);
            //if(sprintControl.action.IsPressed())
            //{
            //    animator.SetInteger("animState", 3);

            //}
        }
        else
        {
            
             //animator.SetInteger("animState", 0);
            
        }

        if (!groundedPlayer)
        {
            //animator.SetInteger("animState", 2);
        }

        if(usedDoubleJump == true && groundedPlayer)
        {
            usedDoubleJump = false;
        }

        groundedPlayer = controller.isGrounded;


    }


}