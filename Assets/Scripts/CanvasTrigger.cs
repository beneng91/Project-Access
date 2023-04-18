using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;

public class CanvasTrigger : MonoBehaviour
{
    public GameObject raycastObject;
    public GameObject clearE;
    private int layerBDestroy;
    private int layerBKeyDestroy;
    private int layerCDestroy;
    private int layerCKeyDestroy;
    private int layerKey;
    private int layerInteractable;



    // Start is called before the first frame update
    void Start()
    {
        layerBDestroy = LayerMask.NameToLayer("BDestroy");
        layerBKeyDestroy = LayerMask.NameToLayer("BKeyDestroy");
        layerCDestroy = LayerMask.NameToLayer("CDestroy");
        layerCKeyDestroy = LayerMask.NameToLayer("CKeyDestroy");
        layerKey = LayerMask.NameToLayer("Key");
        layerInteractable = LayerMask.NameToLayer("Interactable");

    }

    // Update is called once per frame
    void Update()
    {
        TriggerUI();
    }

    private void TriggerUI()
    {
        Vector3 fwd = raycastObject.transform.TransformDirection(Vector3.forward);
        Debug.DrawRay(raycastObject.transform.position, fwd * 1.5f, Color.green);
        RaycastHit objectHit;

        if (Physics.Raycast(raycastObject.transform.position, fwd, out objectHit, 1.5f))
        {
            if (objectHit.collider.gameObject.layer == layerBDestroy) //Destroy barrel, nothing else
            {
                //Debug.Log("Hit");
                clearE.SetActive(true);
            }
            else if (objectHit.collider.gameObject.layer == layerBKeyDestroy) //Destroy barrel, spawn key
            {
                //Debug.Log("Hit");
                clearE.SetActive(true);
            }
            else if (objectHit.collider.gameObject.layer == layerCDestroy) //Destroy crate, nothing else
            {
                //Debug.Log("Hit");
                clearE.SetActive(true);
            }
            else if (objectHit.collider.gameObject.layer == layerCKeyDestroy) //Destroy crate, spawn key
            {
                //Debug.Log("Hit");
                clearE.SetActive(true);
            }
            else if (objectHit.collider.gameObject.layer == layerKey)
            {
                //Debug.Log("Hit");
                clearE.SetActive(true);
            }
            else if (objectHit.collider.gameObject.layer == layerInteractable)
            {
                //Debug.Log("Hit");
                clearE.SetActive(true);
            }
            
        }
        else
        {
            //Debug.Log("Not Hit");
            clearE.SetActive(false);
        }
    }
}
