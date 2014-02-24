using UnityEngine;
using System.Collections;

public class movescript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector3(transform.position.x, transform.position.y, 0);
		if (Input.GetKey(KeyCode.RightArrow))
		{
			transform.rigidbody.AddForce(new Vector3(10, 0, 0));
		}
		if (Input.GetKey(KeyCode.LeftArrow))
		{
			transform.rigidbody.AddForce(new Vector3(-10, 0, 0));
		}
		if (Input.GetKeyDown(KeyCode.UpArrow))
		{
			transform.rigidbody.AddForce(new Vector3(0, 250, 0));
		}
		if (Input.GetKey(KeyCode.DownArrow))
		{
			//transform.rigidbody.AddForce(new Vector3(10, 0, 0));
		}
	}
}
