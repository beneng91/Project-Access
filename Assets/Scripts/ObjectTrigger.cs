using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectTrigger : MonoBehaviour
{
    public KeyCode triggerObject;
    public GameObject raycastObject;
    public GameObject keyItem;
    //public GameObject playerPos;
    private int layerDestructible;
    private int layerKey;
    private int layerKeyDestructible;
    private int layerInteractable;
    private bool isDestroyed;
    private bool keyAcquired;

    // Start is called before the first frame update
    void Start()
    {
        layerDestructible = LayerMask.NameToLayer("Destructible");
        layerKey = LayerMask.NameToLayer("Key");
        layerKeyDestructible = LayerMask.NameToLayer("KeyDestructible");
        layerInteractable = LayerMask.NameToLayer("layerInteractable");
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
        Debug.DrawRay(raycastObject.transform.position, fwd * 5, Color.green);
        RaycastHit objectHit;
        if (Physics.Raycast(raycastObject.transform.position, fwd, out objectHit, 5))
        {
            if (objectHit.collider.gameObject.layer == layerDestructible)
            {
                Debug.Log("Destructible Triggered");
                //Play Animation
                //objectHit.collider.gameObject.GetComponent<Animator>();

                objectHit.transform.gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
            }

            if (objectHit.collider.gameObject.layer == layerKeyDestructible)
            {
                Debug.Log("Key Destructible Triggered");
                //objectHit.collider.gameObject.GetComponent<Animator>();
                //keyItem.SetActive(true);
                objectHit.transform.gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
            }

            if (objectHit.collider.gameObject.layer == layerKey)
            {
                Debug.Log("Key Triggered");
                //keyAcquired = true;
                //keyItem.SetActive(false);
            }

            if (objectHit.collider.gameObject.layer == layerInteractable)
            {
                Debug.Log("Interactable Triggered");
                //objectHit.collider.gameObject.GetComponent<Animator>();
            }
        }
    }
}