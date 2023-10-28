using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{

    [SerializeField] private int object_count = 10;

    [SerializeField] private List<GameObject> object_prefabs;
    [SerializeField] private float spawn_time = 10f;

    private Vector3 pool_position = new Vector3(0, -25f, 0);
    private Queue<GameObject> object_queue;

    [Header("생성 위치")]
    [SerializeField] private float pos_x = 0.0f;
    [SerializeField] private float pos_y = 0.5f;
    [SerializeField] private float pos_z = 30f;
    [SerializeField] private float range = 2.5f;


    GameObject ob;

    private void Awake()
    {
        ob = GetComponent<GameObject>();

        object_queue = new Queue<GameObject>();

        Instantiate_ob();
        
        StartCoroutine(SpawnObject());
    }
    private void Instantiate_ob()
    {
        for (int i = 0; i < object_count; i++)
        {
            Ran_Prefeb();
            ob.SetActive(false);
            object_queue.Enqueue(ob);
        }
    }
    private void Ran_Prefeb()
    {
        int ran = Random.Range(0, object_prefabs.Count);

        ob = Instantiate(object_prefabs[ran]);

    }

    public void TakeInObject(GameObject ob)
    {
        ob.transform.position = pool_position;
        object_queue.Enqueue(ob);
        if (ob.activeSelf)
        {
            ob.SetActive(false);
        }
    }

    public void TakeOutObject() //Vector3 position
    {
        if (object_count < 0)
        {
            return;
        }
        GameObject ob = object_queue.Dequeue();
        if (!ob.activeSelf)
        {
            ob.SetActive(true);
        }
        //ob.transform.position = position;
    }

    private IEnumerator SpawnObject()
    {
        WaitForSeconds wfs = new WaitForSeconds(spawn_time);

        while (true)
        {
            //float position_x = Random.Range(pos_x - range, pos_x + range);
            //Vector3 position = new Vector3(position_x, pos_y, pos_z);
            TakeOutObject();//position
            yield return wfs;
        }
    }
}
