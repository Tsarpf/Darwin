using UnityEngine;
using System.Collections;

public class PlayerStats : MonoBehaviour {

    int bubblePoints;
    float playerSpeed;
    float playerSpeedForce;

    float maxJumpTime;
    float jumpSpeed;

    bool streaking;
    int streakLength;
    float streakLastBubbleTime;

    float streakMaxInterval = 1;
    
	void Start () {
        bubblePoints = 0;
        maxJumpTime = 0.35f;
        jumpSpeed = 6f;
        streaking = false;
        streakLength = 0;
        streakLastBubbleTime = 0;
	}

	void Update () {
        if (Time.time - streakLastBubbleTime > streakMaxInterval)
        {
            streakLength = 0;
            streaking = false;
        }
	}

    void OnTriggerEnter2D(Collider2D collider)
    {
        //Debug.Log("Ebin: '" + collider.gameObject.name + "'");
        switch (collider.gameObject.name)
        {
            case "Bubble":
                poppedBubble();
                break;
            case "jotain":
                break;
            default:
                break;
        }
    }

    private void poppedBubble()
    {

        if (streaking)
        {
            streakLength++;
        }
        else
        {
            bubblePoints++;
            streaking = true;
            streakLength = 1;
        }

		if (streakLength >= 5)
		{
			//moar points
			bubblePoints += 2;
		}

        if (bubblePoints >= 50)
        {

        }
        else if (bubblePoints >= 25)
        {

        }
        else if (bubblePoints >= 10)
        {

        }
        else if (bubblePoints >= 3)
        {

        }


        streakLastBubbleTime = Time.time;
    }

	public float getPlayerSpeedCap()
	{
		return playerSpeed;
	}

	public float getPlayerHorizontalMoveForce()
	{
		return playerSpeedForce;
	}

	public int getStreakLength()
	{
		return streakLength;
	}

    public float getJumpSpeed()
    {
        return jumpSpeed;
    }

    public float getMaxJumpTime()
    {
        return maxJumpTime;
    }

    public float getPlayerSpeedForce()
    {
        return playerSpeedForce;
    }

    public int getBubblePoints()
    {
        return bubblePoints;
    }
	
}
