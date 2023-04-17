using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectTrigger : MonoBehaviour
{
    public KeyCode triggerObject;
    public GameObject raycastObject;
    public GameObject keyItem;
    public GameObject keyDoor;
    public GameObject destroyedBarrel;
    public GameObject destroyedCrate;
    public GameObject heldWeapon;
    private int layerBDestroy;
    private int layerBKeyDestroy;
    private int layerCDestroy;
    private int layerCKeyDestroy;
    private int layerKey;
    private int layerInteractable;
    private bool keyAcquired;

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
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(triggerObject))
        {
            TriggerObject();
        }
    }

    private void TriggerObject()
    {
        Vector3 fwd = raycastObject.transform.TransformDirection(Vector3.forward);
        Debug.DrawRay(raycastObject.transform.position, fwd * 1.5f, Color.green);
        RaycastHit objectHit;
        
        if (Physics.Raycast(raycastObject.transform.position, fwd, out objectHit, 1.5f))
        {
            if (objectHit.collider.gameObject.layer == layerBDestroy) //Destroy barrel, nothing else
            {
                Debug.Log("Destructible Triggered");
                //Trigger destroyed barrel
                Instantiate(destroyedBarrel, objectHit.transform.position, objectHit.transform.rotation);
                objectHit.collider.gameObject.SetActive(false);

                objectHit.transform.gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
            }

            if (objectHit.collider.gameObject.layer == layerBKeyDestroy) //Destroy barrel, spawn key
            {
                Debug.Log("Key Destructible Triggered");
                //Trigger destroyed barrel
                Instantiate(destroyedBarrel, objectHit.transform.position, objectHit.transform.rotation);
                objectHit.collider.gameObject.SetActive(false);

                keyItem.SetActive(true);
                objectHit.transform.gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
            }

            if (objectHit.collider.gameObject.layer == layerCDestroy) //Destroy crate, nothing else
            {
                Debug.Log("Destructible Triggered");
                //Trigger destroyed crate
                Instantiate(destroyedCrate, objectHit.transform.position, objectHit.transform.rotation);
                objectHit.collider.gameObject.SetActive(false);

                objectHit.transform.gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
            }

            if (objectHit.collider.gameObject.layer == layerCKeyDestroy) //Destroy crate, spawn key
            {
                Debug.Log("Key Destructible Triggered");
                //Trigger destroyed crate
                Instantiate(destroyedCrate, objectHit.transform.position, objectHit.transform.rotation);
                objectHit.collider.gameObject.SetActive(false);

                keyItem.SetActive(true);
                objectHit.transform.gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
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
                    //objectHit.collider.gameObject.GetComponent<Animator>();
                    //or
                    keyDoor.SetActive(false);
                }
            }
        }
    }
}
