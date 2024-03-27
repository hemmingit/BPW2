using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class Interaction : MonoBehaviour
{

    public Transform camPos;

    public float interactRange;

    private ActionScript actionScript;

    private bool isHolding = false;


    private Rigidbody heldObjectRb;

    [SerializeField] private Transform holder;
    [SerializeField] private float smoothSpeed;
    [SerializeField] private GameObject empty;

    [SerializeField] private Phasing phasing;

    // Update is called once per frame
    void FixedUpdate()
    {
        RaycastHit hit;

        if (Physics.Raycast(camPos.position, camPos.forward, out hit, interactRange ))
        {
            if (hit.collider.CompareTag("Holdable"))
            {
             if(Input.GetKey(KeyCode.E) && !isHolding)
                {
                    Grab(hit);
                }
             
            }
            else
            {
                Release();
            }
        }
     

        // RAYCAST ENDS

        if (heldObjectRb != null)
        {
            heldObjectRb.position = Vector3.Lerp(heldObjectRb.position, holder.position, smoothSpeed * Time.deltaTime);
            heldObjectRb.rotation = Quaternion.Lerp(heldObjectRb.rotation, holder.rotation, smoothSpeed * Time.deltaTime);
        }



    }


    public void Release()
    {
        heldObjectRb.useGravity = true;
        heldObjectRb = null;
        
        isHolding = false;
    }


    public void Grab(RaycastHit hit)
    {
        heldObjectRb = hit.collider.gameObject.GetComponent<Rigidbody>();
        heldObjectRb.useGravity = false;
        isHolding = true;
    }

}

