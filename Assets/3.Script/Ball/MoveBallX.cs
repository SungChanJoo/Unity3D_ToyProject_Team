using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBallX : MonoBehaviour
{
    

   
    private int speedRan;
    
    private void Start()
    {
        
        speedRan = Random.Range(5, 20);
        float X_Pos = Random.Range(-6, 0);
        Debug.Log(speedRan);
        transform.position =new Vector3 (transform.position.x + X_Pos,transform.position.y,transform.position.z);

    }
    private void Update()
    {
        Move_wall();
    }

    private void Move_wall()
    {
        transform.Translate(Vector3.back * speedRan * Time.deltaTime, 0);

        
    }
}
