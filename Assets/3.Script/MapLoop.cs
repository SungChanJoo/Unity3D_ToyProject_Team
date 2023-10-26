using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapLoop : MonoBehaviour
{
    [SerializeField] private float Speed = 10f;
    
    

    private void Update()
    {
        transform.Translate(Vector3.back * Speed * Time.deltaTime);

        if (transform.position.z<-15)
        {
            Vector3 set = new Vector3(0, 0, 90);
            transform.position = transform.position + set;
        }
    }


}
