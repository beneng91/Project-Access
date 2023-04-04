using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectTrigger : MonoBehaviour
{
    public KeyCode triggerObject;
    public GameObject raycastObject;
    public GameObject keyItem;
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
                //Play Animation
                //objectHit.collider.gameObject.GetComponent<Animator>();

                objectHit.transform.gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
                Debug.Log("Destructible Triggered");
            }

            if (objectHit.collider.gameObject.layer == layerKeyDestructible)
            {
                //objectHit.collider.gameObject.GetComponent<Animator>();
                objectHit.transform.gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
                //keyItem.SetActive(true);
                Debug.Log("Key Destructible Triggered");
            }

            if (objectHit.collider.gameObject.layer == layerKey)
            {
                Debug.Log("Key Triggered");
                //keyAcquired = true;
                //keyItem.SetActive(false);
            }

            if (objectHit.collider.gameObject.layer == layerInteractable)
            {
                //objectHit.collider.gameObject.GetComponent<Animator>();
                Debug.Log("Interactable Triggered");
            }
        }
    }
}
