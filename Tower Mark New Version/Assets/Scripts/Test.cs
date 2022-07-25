using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField] private GameObject target;
    [SerializeField] private LayerMask layer;
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitt;
        if (Physics.Raycast(ray, out hitt, 100, layer))
        {
            target = hitt.collider.gameObject;
        }
        else
        {
            target = null;
        }

    }
}
