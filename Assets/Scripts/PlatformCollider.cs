using UnityEngine;
using System.Collections;

public class PlatformCollider : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

	void OnTriggerEnter(Collider other)
	{
		//Destroy(gameObject);
        Debug.Log("Hit platform");
        //Todo: Add points or whatever.
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
