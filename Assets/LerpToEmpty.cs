using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpToEmpty : MonoBehaviour
{

    [SerializeField] private Transform EmptyToLerpTo;

    [SerializeField] private float LerpSpeed;



    void LateUpdate()
    {
        

        transform.position = EmptyToLerpTo.position;
        transform.rotation = Quaternion.Lerp(transform.rotation, EmptyToLerpTo.rotation, LerpSpeed * Time.fixedDeltaTime);
    }
}
