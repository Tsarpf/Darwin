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
<<<<<<< HEAD
        int bubblePoints = stats.getBubblePoints();
		int streakLength = stats.getStreakLength();

        GUI.Box(new Rect(10, 10, 100, 60), "Points");

=======
        int bubblePoints = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().getBubblePoints();
        GUI.Box(new Rect(10, 10, 100, 90), "Points");
>>>>>>> 0f8ecc1f0327df2ea171f0f513e296573d5076a9
        GUI.Box(new Rect(20, 40, 80, 20), bubblePoints.ToString());

		GUI.Box(new Rect(10, 75, 100, 60), "Streak");
        GUI.Box(new Rect(20, 105, 80, 20), streakLength.ToString());
    }
	
	// Update is called once per frame
	void Update () {
        	
	}
}
