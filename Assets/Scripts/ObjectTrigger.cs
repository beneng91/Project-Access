using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ObjectTrigger : MonoBehaviour
{
    public KeyCode triggerObject;
    public GameObject raycastObject;
    public GameObject keyItem;
    public GameObject keyDoor;
    public GameObject heldWeapon;
    private int layerBDestroy;
    private int layerBKeyDestroy;
    private int layerCDestroy;
    private int layerCKeyDestroy;
    private int layerKey;
    private int layerInteractable;
    private bool keyAcquired;

    //sound stuff
    public AudioClip[] audioSound;
    public AudioSource audioSource;

    //animation stuff
    public Animator animator;

    //cooldown stuff
    public bool attackCooldown;
    public float attackCooldownTimer;
    private float attackTimer;

    // Start is called before the first frame update
    void Start()
    {
        layerBDestroy = LayerMask.NameToLayer("BDestroy");
        layerBKeyDestroy = LayerMask.NameToLayer("BKeyDestroy");
        layerCDestroy = LayerMask.NameToLayer("CDestroy");
        layerCKeyDestroy = LayerMask.NameToLayer("CKeyDestroy");
        layerKey = LayerMask.NameToLayer("Key");
        layerInteractable = LayerMask.NameToLayer("Interactable");
        keyAcquired = false;

        //bug fixing?
        attackTimer = attackCooldownTimer;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(triggerObject))
        {
            TriggerObject();
        }

        if (attackCooldown == true)
        {
            attackTimer -= Time.deltaTime;

            if (attackTimer <= 0)
            {
                attackCooldown = false;
                attackTimer = attackCooldownTimer;
                animator.ResetTrigger("Attack");
            }

        }
    }

    private void TriggerObject()
    {
        Vector3 fwd = raycastObject.transform.TransformDirection(Vector3.forward);
        Debug.DrawRay(raycastObject.transform.position, fwd * 1.5f, Color.green);
        RaycastHit objectHit;
        
        if (Physics.Raycast(raycastObject.transform.position, fwd, out objectHit, 1.5f))
        {
            if (objectHit.collider.gameObject.layer == layerBDestroy && attackCooldown == false) //Destroy barrel, nothing else
            {
                objectHit.transform.gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
                Debug.Log("Destructible Triggered");
                //Trigger destroyed barrel
                objectHit.transform.GetChild(1).gameObject.SetActive(true);
                objectHit.transform.GetChild(0).gameObject.SetActive(false);

                //sound stuff
                audioSource.clip = audioSound[Random.Range(0, 2)];
                audioSource.PlayOneShot(audioSource.clip);

                //animation stuff
                animator.SetTrigger("Attack");

                //cooldown stuff
                attackCooldown = true;               
            }

            if (objectHit.collider.gameObject.layer == layerBKeyDestroy && attackCooldown == false) //Destroy barrel, spawn key
            {
                objectHit.transform.gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
                Debug.Log("Key Destructible Triggered");
                //Trigger destroyed barrel
                objectHit.transform.GetChild(0).gameObject.SetActive(false);
                objectHit.transform.GetChild(1).gameObject.SetActive(true);
                

                //sound stuff
                audioSource.clip = audioSound[4];
                audioSource.PlayOneShot(audioSource.clip);

                //animation stuff
                animator.SetTrigger("Attack");

                //cooldown stuff
                attackCooldown = true;

                keyItem.SetActive(true);
            }

            if (objectHit.collider.gameObject.layer == layerCDestroy && attackCooldown == false) //Destroy crate, nothing else
            {
                objectHit.transform.gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
                Debug.Log("Destructible Triggered");
                //Trigger destroyed crate
                objectHit.transform.GetChild(1).gameObject.SetActive(true);
                objectHit.transform.GetChild(0).gameObject.SetActive(false);

                //sound stuff
                audioSource.clip = audioSound[Random.Range(2, 4)];
                audioSource.PlayOneShot(audioSource.clip);

                //animation stuff
                animator.SetTrigger("Attack");

                //cooldown stuff
                attackCooldown = true;
            }

            if (objectHit.collider.gameObject.layer == layerCKeyDestroy) //Destroy crate, spawn key
            {
                objectHit.transform.gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
                Debug.Log("Key Destructible Triggered");
                //Trigger destroyed crate
                objectHit.transform.GetChild(1).gameObject.SetActive(true);
                objectHit.transform.GetChild(0).gameObject.SetActive(false);

                keyItem.SetActive(true);
            }

            if (objectHit.collider.gameObject.layer == layerKey)
            {
                Debug.Log("Key Triggered");
                keyAcquired = true;
                keyItem.SetActive(false);
                heldWeapon.SetActive(true);
            }

            if (objectHit.collider.gameObject.layer == layerInteractable)
            {
                Debug.Log("Interactable Triggered");
                if (keyAcquired == true)
                {
                    Debug.Log("Key Interactable Triggered");
                    keyDoor.SetActive(false);
                    audioSource.clip = audioSound[5];
                    audioSource.PlayOneShot(audioSource.clip);
                }
            }
        }
    }
}
