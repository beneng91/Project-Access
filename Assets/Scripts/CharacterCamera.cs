using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCamera : MonoBehaviour
{
    public float cameraDistance = 5f;
    public float cameraHeight = 2f;
    public float lookSpeed = 2f;
    public float cameraOffset = 0.8f;

    private Transform cameraTransform;
    private Vector2 rotation = Vector2.zero;


    // Start is called before the first frame update
    void Start()
    {
        cameraTransform = transform.GetChild(0);
    }

    // Update is called once per frame
    void Update()
    {
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
