using UnityEngine;
using System.Collections;

public class PlayerPoints : MonoBehaviour {

    int bubblePoints;
	// Use this for initialization
	void Start () {
        bubblePoints = 0;	
	}

    void OnTriggerEnter2D(Collider2D collider)
    {
        //Debug.Log("Ebin: '" + collider.gameObject.name + "'");
        switch (collider.gameObject.name)
        {
            case "Bubble":
                bubblePoints++;
                break;
            case "jotain":
                break;
            default:
                break;
        }
    }

    public int getBubblePoints()
    {
        return bubblePoints;
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
