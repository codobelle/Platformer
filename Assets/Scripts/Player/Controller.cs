using System.Collections;
using UnityEngine;

public class Controller : MonoBehaviour
{
    [HideInInspector]
    public int direction;
    [SerializeField]
    private InputManager InputManager;
    [SerializeField]
    [Range(1,4)]
    private int speed = 1, runSpeed = 1;
    [SerializeField]
    private float jumpForce = 1;
    [SerializeField]
    private float doubleJumpForce = 1;

    private int leftDirection = -1, rightDirection = 1;
    private bool isMoving = false;
    private bool isGrounded = true;
    private bool isJumping = false;
    private int jumpCounter = 0, maxJumps = 2;
    private int playerSpeed;
    private int dashSpeed;
    private float groundSmashSpeed;
    private int dashCoefficient = 2;
    private float groundSmashCoefficient = 4f;
    private float waitForGroundSmash = 0.35f;

    private Rigidbody rigidBody;
    private Animator animator;
    private float distanceToTheGround;
    private float maxRaycastDistance;

    // Use this for initialization
    private void Start()
    {
        direction = 1;
        playerSpeed = speed;
        dashSpeed = speed / dashCoefficient;
        groundSmashSpeed = speed * groundSmashCoefficient;
        rigidBody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        distanceToTheGround = GetComponent<BoxCollider>().bounds.extents.y;
        maxRaycastDistance = GetComponent<BoxCollider>().bounds.extents.x;
    }

    private void Update()
    {
        isGrounded = Physics.Raycast(new Vector3(transform.position.x - maxRaycastDistance, 
            transform.position.y, transform.position.z), 
            Vector3.down, distanceToTheGround + 0.1f) 
            || Physics.Raycast(transform.position, Vector3.down, distanceToTheGround + 0.1f) 
            ||  Physics.Raycast(new Vector3(transform.position.x + maxRaycastDistance, 
            transform.position.y, transform.position.z), 
            Vector3.down, distanceToTheGround + 0.1f);
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        isMoving = rigidBody.velocity != Vector3.zero ? true : false;
        //check is moving left
        if (InputManager.IsLeft() && isGrounded)
        {
            MovePlayer(leftDirection);
        }
        //check is moving right
        if (InputManager.IsRight() && isGrounded)
        {
            MovePlayer(rightDirection);
        }
        //check is jump or double jump
        if (InputManager.IsJump() && jumpCounter < maxJumps)
        {
            if (isGrounded)
            {
                Jump(jumpForce);
            }
            else
            {
                Jump(doubleJumpForce);
            }
            jumpCounter++;
            isGrounded = false;
            isJumping = true;
        }
        //check is runing
        if (InputManager.IsRun() && isMoving)
        {
            playerSpeed = runSpeed;
        }
        else
        {
            playerSpeed = speed;
        }

        if (!isMoving && !isJumping && isGrounded)
        {
            animator.SetTrigger("Idle");
        }

        if (isJumping)
        {
            animator.SetTrigger("Jump");
        }
    }

    // check if player entered platform
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(Constants.groundTag))
        {
            isJumping = false;
            rigidBody.mass = 1;
            rigidBody.velocity = Vector3.zero;
            jumpCounter = 0;
        }
    }
    
    // player Dash
    public void Dash()
    {
        isGrounded = false;
        rigidBody.MovePosition(new Vector3(rigidBody.position.x + dashSpeed * direction,
            rigidBody.position.y, rigidBody.position.z));
    }

    public void GroundSmash()
    {
        if (!isGrounded)
        {
            StartCoroutine(GroundSmashIEnumerator());
        }
    }

    private IEnumerator GroundSmashIEnumerator()
    {
        rigidBody.useGravity = false;
        yield return new WaitForSeconds(waitForGroundSmash);
        rigidBody.useGravity = true;
        rigidBody.AddForce(Vector3.down * groundSmashSpeed, ForceMode.Impulse);
    }

    // player movement
    private void MovePlayer(int direction)
    {
        animator.SetTrigger("Walk");
        this.direction = direction;
        if (direction < 0)
        {
            transform.eulerAngles = new Vector3(0, -90, 0);
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 90, 0);
        }
        rigidBody.velocity = new Vector3(playerSpeed * direction, rigidBody.velocity.y, rigidBody.velocity.z);
    }

    // player jump
    private void Jump(float jumpingForce)
    {
        rigidBody.AddForce(transform.up * playerSpeed * jumpingForce, ForceMode.Impulse);
    }
}
