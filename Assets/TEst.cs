using UnityEngine;
using System.Collections.Generic;
using LibNoise;
using System;

public class TEst : MonoBehaviour {

	// Use this for initialization

	Vector3[] height;

	void Start ()
	{

		Dictionary<string, string> textures = new Dictionary<string, string>();
		textures["floorTexture"] = "WaterPlain0017_9_S";
		textures["bubbleTexture"] = "Fruit0034_L";

		Assets.Level level = new Assets.Level(textures, 250);
	}
	void Update () {
	
	}
}
