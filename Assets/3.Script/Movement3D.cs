using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement3D : MonoBehaviour
{
    [SerializeField] private float move_speed = 10f;
    [SerializeField] private Vector3 move_direction = Vector3.zero;
    // Update is called once per frame
    void Update()
    {
        transform.position += move_direction * move_speed * Time.deltaTime;
    }

    public void MoveTo(Vector3 direction)
    {
        move_direction = direction;
    }
}
