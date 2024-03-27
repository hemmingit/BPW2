using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorPadding : MonoBehaviour
{

    [SerializeField] private Transform holderHolder;


    private void Update()
    {
        var xPos = holderHolder.position.x;
        var yPos = Mathf.Clamp(holderHolder.position.y, 0.5f, 1000f);
        var zPos = holderHolder.position.z;

        transform.rotation = holderHolder.transform.rotation;

        transform.position = new Vector3(xPos, yPos, zPos);





    }
}
