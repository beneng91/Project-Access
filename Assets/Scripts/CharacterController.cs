using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public float speed = 10f;
    public float cameraDistance = 5f;
    public float cameraHeight = 2f;
    public float lookSpeed = 2f;
    public float cameraOffset = 0.8f;

    private UnityEngine.CharacterController controller;
    private Transform cameraTransform;
    private Vector3 moveDirection = Vector3.zero;
    private Vector2 rotation = Vector2.zero;

    private void Start()
    {
        controller = GetComponent<UnityEngine.CharacterController>();
        cameraTransform = transform.GetChild(0);
    }

    private void Update()
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

        // Get mouse input to control the camera rotation
        rotation.y += Input.GetAxis("Mouse X") * lookSpeed;
        rotation.x -= Input.GetAxis("Mouse Y") * lookSpeed;

        // Clamp the camera rotation to prevent the camera from flipping over
        rotation.x = Mathf.Clamp(rotation.x, -15f, 30f);

        // Apply the camera rotation to the camera transform
        cameraTransform.localRotation = Quaternion.Euler(rotation.x, 0, 0);
        transform.localRotation = Quaternion.Euler(0, rotation.y, 0);

        // Update the camera position to keep it centered behind the player
        Vector3 cameraPosition = transform.position - transform.forward * cameraDistance + transform.up * cameraHeight + transform.right * cameraOffset;
        cameraTransform.position = cameraPosition;
    }
}
