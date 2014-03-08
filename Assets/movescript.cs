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
			transform.rigidbody2D.AddForce(new Vector2(10, 0));
		}
		if (Input.GetKey(KeyCode.LeftArrow))
		{
			transform.rigidbody2D.AddForce(new Vector2(-10, 0));
		}
		if (Input.GetKey(KeyCode.DownArrow))
		{
			//transform.rigidbody.AddForce(new Vector3(10, 0, 0));
		}
	}

    void OnCollisionEnter2D(Collision2D collision)
    {
		if (Input.GetKeyDown(KeyCode.UpArrow))
		{
            //rigidbody2D.AddForce(new Vector2(0, 250));
            rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, 10f);
		}
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        //Debug.Log(collision.contacts[0].normal);
        float maxVelocity = 5f;

        if (rigidbody2D.velocity.x < maxVelocity)
        {
            //Debug.Log("adding powah");
            transform.rigidbody2D.AddForce(new Vector2(15, 0));
        }

		if (Input.GetKeyDown(KeyCode.UpArrow))
		{
            bool anyJumpable = false;
            foreach (ContactPoint2D contact in collision.contacts)
            {
                if (contact.normal.x > -0.91)
                {
                    anyJumpable = true;
                }
            }
            //rigidbody2D.AddForce(new Vector2(0, 250));
            if(anyJumpable) rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, 10f);

		}

        //foreach (ContactPoint2D contact in collision.contacts)
        //{
        //    print(contact.collider.name + " hit " + contact.otherCollider.name);
        //    Debug.DrawRay(contact.point, contact.normal, Color.white);
        //}
    }
    /*
    void OnCollisionStay(Collision coll)
    {
        coll.contacts

    }
    */

}
