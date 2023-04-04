using System.Collections;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private Animator animator;

    public float moveSpeed = 4f;
    
    //interaction components

    
    // Start is called before the first frame update
    void Start()
    {
        //movement components
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        

    }

    // Update is called once per frame
    void Update()
    {
        Move();
        

    }



    private void Move()
    {
        //movement inputs
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        
        //direction normalized
        Vector3 dir = new Vector3(horizontal, 0f, vertical).normalized;
        Vector3 velocity = moveSpeed * Time.deltaTime * dir;
        
        /*sprint key maybe?
        if (Input.GetButton("Sprint"))
        {
                
        }*/
        
        //check for movements
        if (dir.magnitude >= 0.1f)
        {
            //look direction
            transform.rotation = Quaternion.LookRotation(dir);
            
            //Movement
            controller.Move(velocity);
        }
        
        //animation
        animator.SetFloat("Speed", velocity.magnitude);
        
    }
}
