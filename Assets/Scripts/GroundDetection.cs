using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundDetection : MonoBehaviour
{
    public GameObject detectionObject;
    private int layerConcrete;
    private int layerGrass;
    private int layerWood;
    private bool isWalking;

    private void Awake()
    {
        isWalking = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        layerConcrete = LayerMask.NameToLayer("Concrete");
        layerGrass = LayerMask.NameToLayer("Grass");
        layerWood = LayerMask.NameToLayer("Wood");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            isWalking = true;
        }
        else
        {
            isWalking = false;
        }
        DetectGround();
        
    }

    private void DetectGround()
    {
        RaycastHit objectHit;
        if (Physics.Raycast(detectionObject.transform.position, Vector3.down, out objectHit, 1f))
        {
            if (objectHit.collider.gameObject.layer == layerConcrete) //Concrete layer
            {   
                if (isWalking == true)
                {
                    //play audio here
                }
            }

            if (objectHit.collider.gameObject.layer == layerGrass) //Grass layer
            {
                if (isWalking == true)
                {
                    //play audio here
                }
            }

            if (objectHit.collider.gameObject.layer == layerWood) //Wood layer
            {
                if (isWalking == true)
                {
                    //play audio here
                }
            }
        }
        else
        {
            Debug.Log("Not Hit");
        }
    }
}
