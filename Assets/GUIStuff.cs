using UnityEngine;
using System.Collections;

public class GUIStuff : MonoBehaviour {


	PlayerStats stats;
	// Use this for initialization
	void Start ()
    {
		stats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
	}

    void OnGUI()
    {
        int bubblePoints = stats.getBubblePoints();
		int streakLength = stats.getStreakLength();

        GUI.Box(new Rect(10, 10, 100, 60), "Points");

        GUI.Box(new Rect(20, 40, 80, 20), bubblePoints.ToString());

		GUI.Box(new Rect(10, 75, 100, 60), "Streak");
        GUI.Box(new Rect(20, 105, 80, 20), streakLength.ToString());
    }
	
	// Update is called once per frame
	void Update () {
        	
	}
}
