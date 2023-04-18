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
    private bool concreteTriggered = false;
    private bool grassTriggered = false;
    private bool woodTriggered = false;


    //Sound stuff
    public AudioClip[] audioSound;
    public AudioSource audioSource;

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
        DetectGround();
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            if (isWalking == false)
            {
                isWalking = true;
                audioSource.Play();
            }
            
        }
        else
        {
            if (isWalking == true)
            {
                isWalking = false;
                audioSource.Stop();
            }
            
        }
    }

    private void DetectGround()
    {
        RaycastHit objectHit;
        if (Physics.Raycast(detectionObject.transform.position, Vector3.down, out objectHit, 1f))
        {
            if (objectHit.collider.gameObject.layer == layerConcrete && concreteTriggered == false) //Concrete layer
            {
                concreteTriggered = true;
                if (audioSource.clip != audioSound[0])
                {
                    audioSource.clip = audioSound[0];
                    if (isWalking == true)
                    {
                        audioSource.Play();
                    }
                }
                grassTriggered = false;
                woodTriggered = false;
            }
            
            if (objectHit.collider.gameObject.layer == layerGrass && grassTriggered == false) //Grass layer
            {
                grassTriggered = true;
                if (audioSource.clip != audioSound[1])
                {
                    audioSource.clip = audioSound[1];
                    if (isWalking == true)
                    {
                        audioSource.Play();
                    }
                }
                concreteTriggered = false;
                woodTriggered = false;
            }
            
            if (objectHit.collider.gameObject.layer == layerWood && woodTriggered == false) //Wood layer
            {
                woodTriggered = true;
                if (audioSource.clip != audioSound[2])
                {
                    audioSource.clip = audioSound[2];
                    if (isWalking == true)
                    {
                        audioSource.Play();
                    }
                }
                concreteTriggered = false;
                grassTriggered = false;
            }
        }
    }
}
