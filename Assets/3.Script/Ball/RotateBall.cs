using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateBall : MonoBehaviour
{
    [SerializeField] private bool rotate;    
    [SerializeField] private float rotationSpeed;

    private void Update()
    {
        if (rotate)
            transform.Rotate(Vector3.left * rotationSpeed * Time.deltaTime, Space.World);
    }
}
