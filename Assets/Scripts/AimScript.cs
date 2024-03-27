using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class AimScript : MonoBehaviour
{

    public Transform lookPoint;
    public Transform lookpointBase;
    public Transform cam;

    public float sensX;
    public float sensY;

    public LayerMask invis;

    public Transform orientation;

    float yRotation = 0f;
    float xRotation = 0f;

    [SerializeField] private float lookRange;
    [SerializeField] private float holdDist;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }


    void LateUpdate()
    {
       float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
       float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

        yRotation += mouseX;

        xRotation -= mouseY;

        xRotation = Mathf.Clamp(xRotation, -90f, 90f);


        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0f);
        orientation.rotation = Quaternion.Euler(0f, yRotation, 0f);

        RaycastHit hit;

        if(Physics.Raycast(cam.position, cam.forward, out hit, lookRange, ~invis))
        {
           
                lookPoint.transform.position = hit.point;
            


        }
        else
        {
            lookPoint.transform.position = lookpointBase.transform.position;
        }


    }
}
