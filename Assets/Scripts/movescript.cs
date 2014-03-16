using UnityEngine;
using System.Collections;

public class movescript : MonoBehaviour {

	// Use this for initialization
    Quaternion q;
    //Vector3 eulerAngles;

    bool playerOnGround = false;
    bool playerJumping = false;
    float jumpStartTime = 0;
    PlayerStats stats;

	void Start () {
        stats = gameObject.GetComponent<PlayerStats>();
        q = transform.rotation;
        //eulerAngles = transform.eulerAngles;
	}
	
	// Update is called once per frame
	void Update () {
        float jumpSpeed = stats.getJumpSpeed();
        float maxJumpTime = stats.getMaxJumpTime();

        if (playerJumping)
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                float currentTime = Time.time;
                if (currentTime < jumpStartTime + maxJumpTime)
                {
                    
                    rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, jumpSpeed);
                    //rigidbody2D.AddForce(new Vector2(0, jumpSpeed));
                }
                else
                {
                    playerJumping = false;
                }
            }
        }

        if (Input.GetKey(KeyCode.UpArrow) && playerOnGround)
        {
            playerJumping = true;
            jumpStartTime = Time.time;
            //rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, jumpSpeed
            if (rigidbody2D.velocity.y > 0)
            {
                rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, rigidbody2D.velocity.y + 3f);
            }
            else
            {
                rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, jumpSpeed);
            }
            //rigidbody2D.AddForce(new Vector2(0, jumpSpeed));
            playerOnGround = false;
        }

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

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Terrain" || collision.gameObject.name == "Platform")
        {
            playerOnGround = false;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Terrain" || collision.gameObject.name == "Platform")
        {
            bool anyJumpable = false;
            foreach (ContactPoint2D contact in collision.contacts)
            {
                if (contact.normal.x > -0.91)
                {
                    anyJumpable = true;
                }
            }
           if(anyJumpable) playerOnGround = true;
        }
        //if (Input.GetKeyDown(KeyCode.UpArrow))
        //if (Input.GetKey(KeyCode.UpArrow))
        //{
        //    //rigidbody2D.AddForce(new Vector2(0, 250));
        //    rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, 10f);
        //}
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        //Debug.Log(collision.contacts[0].normal);
        float maxVelocity = stats.getPlayerSpeedCap();
		float horizontalForce = stats.getPlayerHorizontalMoveForce();

        if (rigidbody2D.velocity.x < maxVelocity)
        {
            //Debug.Log("adding powah");
            transform.rigidbody2D.AddForce(new Vector2(horizontalForce, 0));
        }

        /*
        //if (Input.GetKeyDown(KeyCode.UpArrow))
		if (Input.GetKey(KeyCode.UpArrow))
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
        */

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
