using UnityEngine;
using System.Collections;

public class camerascript : MonoBehaviour {

	Quaternion q;
	Vector3 origPos;
	// Use this for initialization
	void Start () {

		Quaternion q = transform.rotation;
		origPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        var playerpos = GameObject.FindGameObjectWithTag("Player").transform.position;
		transform.position = new Vector3(playerpos.x, playerpos.y, origPos.z);
		transform.rotation = q;
	}
}
