using UnityEngine;
using System.Collections;

public class BubbleCollider : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

	void OnTriggerEnter(Collider other)
	{
		Destroy(gameObject);
	}

	
	// Update is called once per frame
	void Update () {
	
	}
}
