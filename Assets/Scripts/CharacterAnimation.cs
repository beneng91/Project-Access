using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimation : MonoBehaviour
{
    [SerializeField] Transform hand;

    private Animator animator;
    private UnityEngine.CharacterController controller;
    private Vector3 moveDirection = Vector3.zero;

    public GameObject chainsaw;

    public RuntimeAnimatorController unarmed;
    public RuntimeAnimatorController chainsawArmed;

    public float speed = 10f;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        controller = GetComponent<UnityEngine.CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        // Get movement input from the horizontal and vertical axes
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Calculate the move direction based on the input
        moveDirection = new Vector3(horizontal, 0, vertical);
        moveDirection = transform.TransformDirection(moveDirection);
        moveDirection *= speed * Time.deltaTime;

        // Apply the move direction to the character controller
        controller.Move(moveDirection);

        // Update the animator with the movement input
        animator.SetFloat("Horizontal", horizontal);
        animator.SetFloat("Vertical", vertical);

        // Check if the character is moving
        bool isMoving = moveDirection.magnitude > 0;

        // Update the animator with the movement state
        animator.SetBool("IsMoving", isMoving);

        if (chainsaw.activeSelf)
        {
            animator.runtimeAnimatorController = chainsawArmed as RuntimeAnimatorController;
            transform.SetParent(hand);
        }
    }
}
