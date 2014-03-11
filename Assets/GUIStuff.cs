using UnityEngine;
using System.Collections;

public class GUIStuff : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        	
	}

    void OnGUI()
    {
        int bubblePoints = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().getBubblePoints();
        GUI.Box(new Rect(10, 10, 100, 90), "Points");
        GUI.Box(new Rect(20, 40, 80, 20), bubblePoints.ToString());
    }
	
	// Update is called once per frame
	void Update () {
        	
	}
}
