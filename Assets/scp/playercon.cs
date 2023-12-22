using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playercon : MonoBehaviour
{
    private CharacterController characterController;
    [SerializeField] private float playerSpeed = 5f;
    [SerializeField] private Camera followCamera;

    [SerializeField] private float rotationSpeed = 10f;

    private Vector3 playerVeclocaty;
    [SerializeField] private float gravityvalue = -13f;

    public bool groundedPlayer;
    [SerializeField] private float jumpHeight = 2.5f;

    public Animator animator;
    public static playercon instance;
    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        {
            switch (chekwinner.instance.isWinner)
            {
                case true:
                    animator.SetBool("victory", chekwinner.instance.isWinner);
                    break;

                case false:
                    Movement();
                    break;
            }
            
        }

    void Movement()
        {
            groundedPlayer = characterController.isGrounded;
            if (characterController.isGrounded && playerVeclocaty.y < -2f)
            {
                playerVeclocaty.y = -1f;
            }

            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");

            Vector3 movementInput = Quaternion.Euler(0, followCamera.transform.eulerAngles.y, 0) * new Vector3(horizontalInput, 0, verticalInput);
            Vector3 movementDirection = movementInput.normalized;

            characterController.Move(movementDirection * playerSpeed * Time.deltaTime);

            if (movementDirection != Vector3.zero)
            {
                Quaternion desiredRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
                transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, rotationSpeed * Time.deltaTime);
            }

            if (Input.GetButtonDown("Jump") && groundedPlayer)
            {
                playerVeclocaty.y += Mathf.Sqrt(jumpHeight * -3f * gravityvalue);
                animator.SetTrigger("jumping");
            }

            playerVeclocaty.y += gravityvalue * Time.deltaTime;
            characterController.Move(playerVeclocaty * Time.deltaTime);

            animator.SetFloat("speed", Mathf.Abs(movementDirection.x) + Mathf.Abs(movementDirection.z));
            animator.SetBool("Ground", characterController.isGrounded);
        }
    }
}
