using UnityEngine;
using System.Collections;

public class movescript : MonoBehaviour {

	// Use this for initialization
    Quaternion q;
    Vector3 eulerAngles;
	void Start () {
        q = transform.rotation;
        eulerAngles = transform.eulerAngles;
	}
	
	// Update is called once per frame
	void Update () {
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z);

		if (Input.GetKey(KeyCode.RightArrow))
		{
			transform.rigidbody2D.AddForce(new Vector3(10, 0, 0));
		}
		if (Input.GetKey(KeyCode.LeftArrow))
		{
			transform.rigidbody2D.AddForce(new Vector3(-10, 0, 0));
		}
		if (Input.GetKeyDown(KeyCode.UpArrow))
		{
			transform.rigidbody2D.AddForce(new Vector3(0, 250, 0));
		}
		if (Input.GetKey(KeyCode.DownArrow))
		{
			//transform.rigidbody.AddForce(new Vector3(10, 0, 0));
		}
	}
}
