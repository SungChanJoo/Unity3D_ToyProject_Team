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
    
    private float size_up = 0;
    private float size_max = 3;
    private void Start()
    {
        sizeRan = Random.Range(0, 0.0007f);
        ran = Random.Range(0, 3);
        speedRan = Random.Range(2, 7);
        spawnStart = transform.position;
    }
    private void Update()
    {
        lerpTime += Time.deltaTime * speed;

        if (size_up<=size_max)
        {
            size_up += sizeRan;
            
           transform.localScale = new Vector3(1+size_up, 1+size_up,1+ size_up);
            float Y = transform.position.y + sizeRan;
            transform.position = new Vector3(transform.position.x, Y, transform.position.z);
            transform.GetChild(0).localScale = transform.localScale;
        }

        //Move_wall();
        //Debug.Log(speedRan);
        //LeftSin();
        

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


}
