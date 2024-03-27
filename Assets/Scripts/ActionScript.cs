using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionScript : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;




    public void Activate()
    {
        if(rb != null)
        {
            if (rb.isKinematic == false )
            {
                rb.isKinematic = true;
            }
            else if (rb.isKinematic)
            {
                rb.isKinematic = false;
            }
        }
    }





}
