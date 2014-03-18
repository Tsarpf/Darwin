using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;


public class GrassSceneTest : MonoBehaviour {

	// Use this for initialization
	void Start ()
	{

		Dictionary<string, string> textures = new Dictionary<string, string>();
        //textures["floorTexture"] = "WaterPlain0017_9_S";
        textures["floorTexture"] = "Grass0133_9_S";
		textures["bubbleTexture"] = "Fruit0034_L";
        textures["platformTexture"] = "OrnamentBorder0228_2_S";
        textures["leaf"] = "grass_project/single_black_and_white";

		Assets.Level level = new Assets.Level(textures, 10);
	}

	void Update () {
	
	}
}   