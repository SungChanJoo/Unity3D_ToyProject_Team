using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBallX : MonoBehaviour
{
       
    private int speedRan;
    private Vector3 spawnStart;
    private Vector3 This_ob;

    ObjectSpawner objectSpawner;


    private void Awake()
    {
        objectSpawner = FindObjectOfType<ObjectSpawner>();
        

    }
    private void Start()
    {
        This_ob = transform.localScale;
        spawnStart = transform.position;
        speedRan = Random.Range(5, 20);
        float X_Pos = Random.Range(-4, 0);
        Debug.Log(speedRan);
        transform.position =new Vector3 (transform.position.x + X_Pos,transform.position.y,transform.position.z);

    }
    private void Update()
    {
        Move_wall();
        TakeIn();
    }
    
    private void Move_wall()
    {
        transform.Translate(Vector3.back * speedRan * Time.deltaTime, 0);
        
    }
    private void TakeIn()
    {
        if (transform.position.z<-16)
        {
            objectSpawner.TakeInObject(this.gameObject);
            transform.position = spawnStart;
            transform.localScale = This_ob;
        }
    }
}
