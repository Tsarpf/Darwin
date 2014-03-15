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

    TextStuff textStuff;


    
	void Start () {
        bubblePoints = 0;
        maxJumpTime = 0.35f;
        jumpSpeed = 6f;
        streaking = false;
        streakLength = 0;
        streakLastBubbleTime = 0;
		playerSpeedForce = 10f;
   		playerSpeed = 3f;
        textStuff = GameObject.FindGameObjectWithTag("WorldGameObject").GetComponent<TextStuff>();
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
		int points = 1;

        if (streaking)
        {
            streakLength++;
        }
        else
        {
            streaking = true;
            streakLength = 1;
        }

        if (streakLength >= 5)
        {
            textStuff.showTextMoving("STREAKING!");
        }

		if (streakLength >= 5 && streakLength < 10)
		{
			//double points
			points++;
		}
		else if (streakLength >= 10 && streakLength < 20)
		{
            //triple
			points += 2;
		}
		else if (streakLength >= 20)
		{
            //quadruple
			points += 3;
		}

		bubblePoints += points;

        //Todo: add powerups/speedups/etc here. ie: make changes to the player speed stuff
		if (bubblePoints >= 100)
		{
			playerSpeed = 20f;
			playerSpeedForce = 45f;
		}
		else if (bubblePoints >= 50)
		{
			playerSpeed = 15f;
			playerSpeedForce = 25;
			jumpSpeed = 10f;
		}
		else if (bubblePoints >= 25)
		{
			playerSpeed = 10f;
			playerSpeedForce = 20f;
			jumpSpeed = 8f;
		}
		else if (bubblePoints >= 10)
		{
			playerSpeedForce = 15f;
			playerSpeed = 5f;
		}
		else if (bubblePoints >= 3)
		{
			playerSpeedForce = 12.5f;
			playerSpeed = 4f;
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
