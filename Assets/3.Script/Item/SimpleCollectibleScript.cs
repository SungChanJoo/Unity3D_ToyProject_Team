using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SimpleCollectibleScript : MonoBehaviour {

	public enum CollectibleTypes {NoType, Type1, Type2, Type3, Type4, Type5}; 

	public CollectibleTypes CollectibleType; 

	public bool rotate; 

	public float rotationSpeed;

	public AudioClip collectSound;

	public GameObject collectEffect;

	ObjectSpawner objectSpawner;

	private Vector3 spawnStart;
	private Vector3 This_ob;

	void Start () 
	{
		objectSpawner = FindObjectOfType<ObjectSpawner>();
		This_ob = transform.localScale;
		spawnStart = transform.position;
	}
	
	// Update is called once per frame
	void Update () 
	{

		if (rotate)
			transform.Rotate (Vector3.up * rotationSpeed * Time.deltaTime, Space.World);

	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player") {
			Collect ();
		}
	}

	public void Collect()
	{
		if(collectSound)
			AudioSource.PlayClipAtPoint(collectSound, transform.position);
		if(collectEffect)
			Instantiate(collectEffect, transform.position, Quaternion.identity);

		

		if (CollectibleType == CollectibleTypes.NoType) 
		{

			//Add in code here;

			Debug.Log ("Do NoType Command");
		}
		if (CollectibleType == CollectibleTypes.Type1) 
		{

			//Add in code here;

			Debug.Log ("Do NoType Command");
		}

		TakeIn();

		
	}
	private void TakeIn()
	{
		
			objectSpawner.TakeInObject(this.gameObject);
			transform.position = spawnStart;
			transform.localScale = This_ob;
		
	}
}
