using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets
{
	class Bubble
	{
		Vector3 position;
		GameObject bubble;
		Texture2D texture;

		public Bubble(Vector3 position, Texture2D texture)
		{
			//this.position = new Vector3(position.x, position.y, position.z);
			this.position = position;
			this.texture = texture;


            GameObject prefab = (GameObject)Resources.Load("BubblePrefab");
            bubble = (GameObject)GameObject.Instantiate(prefab);

            bubble.name = "Bubble";

            bubble.renderer.material.shader = Shader.Find("Transparent/Cutout/Diffuse");

            bubble.transform.position = position;

			//bubble.renderer.material.mainTextureScale = new Vector2(0.5f, 1f);
			bubble.renderer.material.mainTexture = texture;
		}
	}
}
