using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBall : MonoBehaviour
{
    
    [SerializeField] private float speed = 2f;

    private float lerpTime = 0;
    private int ran;
    private int speedRan;
    private float sizeRan;
    private Vector3 spawnStart;
    private Vector3 This_ob;

    private float size_up = 0;
    private float size_max = 3;

    ObjectSpawner objectSpawner;

    private void Awake()
    {
        objectSpawner = FindObjectOfType<ObjectSpawner>();
    }
    private void Start()
    {
        This_ob = transform.localScale;
        sizeRan = Random.Range(0, 0.0007f);
        ran = Random.Range(0, 3);
        speedRan = Random.Range(2, 7);
        float X_Pos = Random.Range(-5, 4);
        if (ran==2)
        {
            transform.position = new Vector3(X_Pos, transform.position.y,transform.position.z);
        }
        spawnStart = transform.position;
    }
    private void Update()
    {
        lerpTime += Time.deltaTime * speed;

        Ball_size();

        //Move_wall();
        //Debug.Log(speedRan);
        //LeftSin();
        //while (true)
        //{
        //    switch (ran)
        //    {
        //        case 0:
        //            LeftSin();
        //            continue;
        //        case 1:
        //            RightSin();
        //            continue;
        //        case 2:
        //            Sin();
        //            continue;
        //        case 3:
        //            Move_wall();
        //            continue;
               
        //    }
        //}
       

        if (ran == 0)
        {
            LeftSin();
        }
        else if (ran == 1)
        {
            RightSin();
        }
        else if (ran==2)
        {
            Move_wall();
        }
        else if (ran==3)
        {
            Sin();
        }
        TakeIn();
    }
    private void Ball_size()
    {
        if (size_up <= size_max)
        {
            size_up += sizeRan;

            float Y;        
           
            if (sizeRan > 0.0004f && speedRan < 3)
            {
                Y = transform.position.y + (sizeRan - 0.00012f);
            }
            else
            {
                 Y = transform.position.y + sizeRan;
            }
            transform.localScale = new Vector3(1 + size_up, 1 + size_up, 1 + size_up);
            
            transform.position = new Vector3(transform.position.x, Y, transform.position.z);
            transform.GetChild(0).localScale = transform.localScale;
        }
    }

    
    private void LeftSin()
    {
        float x = Mathf.Sin(lerpTime) * speed;
        float z = -lerpTime * speedRan;
        
        transform.position = new Vector3(x - 2f, transform.position.y, z+spawnStart.z); ;
    }

    private void RightSin()
    {
        float x = -Mathf.Sin(lerpTime) * speed;
        float z = -lerpTime * speedRan;
        transform.position = new Vector3(x+2f, transform.position.y, z + spawnStart.z);
    }
    private void Sin()
    {
        float x = Mathf.Sin(lerpTime) * 4.2f;
        float z = -lerpTime * 2f;
        transform.position = new Vector3(x -0.6f, transform.position.y, z + spawnStart.z);
    }
    private void Move_wall()
    {
        transform.Translate(Vector3.back * (speedRan + 5) * Time.deltaTime, 0);
    }
    private void TakeIn()
    {
        if (transform.position.z < -16)
        {
            objectSpawner.TakeInObject(this.gameObject);
            transform.position = spawnStart;
            transform.localScale = This_ob;
            size_up = 0;
        }
    }


}
