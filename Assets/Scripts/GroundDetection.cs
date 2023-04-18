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
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            isWalking = true;
        }
        else
        {
            isWalking = false;
            //audioSource.Stop();
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
                    audioSource.clip = audioSound[0];
                    audioSource.Play();
                }
                else
                {
                    audioSource.Stop();
                }
            }

            if (objectHit.collider.gameObject.layer == layerGrass) //Grass layer
            {
                if (isWalking == true)
                {
                    audioSource.clip = audioSound[1];
                    audioSource.Play();
                }
                else
                {
                    audioSource.Stop();
                }
            }

            if (objectHit.collider.gameObject.layer == layerWood) //Wood layer
            {
                if (isWalking == true)
                {
                    audioSource.clip = audioSound[2];
                    audioSource.Play();
                }
                else
                {
                    audioSource.Stop();
                }
            }
        }
        else
        {
            Debug.Log("Not Hit");
        }
    }
}
