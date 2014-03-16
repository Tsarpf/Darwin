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
        Color32 averageColor; 

		public Bubble(Vector3 position, Texture2D texture, Color32 averageColor)
		{

            this.averageColor = averageColor;
			this.position = position;
			this.texture = texture;


            GameObject prefab = (GameObject)Resources.Load("BubblePrefab");
            bubble = (GameObject)GameObject.Instantiate(prefab);

            bubble.name = "Bubble";

            bubble.renderer.material.shader = Shader.Find("Transparent/Cutout/Diffuse");

            var ps = bubble.GetComponent<ParticleSystem>();
            //ps.startColor = (Color)averageColor;
            float r = Mathf.Lerp(0, 1, (averageColor.r) / 255f);
            //Debug.Log(averageColor.r / 255f);
            float g = Mathf.Lerp(0, 1, (averageColor.g) / 255f);
            float b = Mathf.Lerp(0, 1, (averageColor.b) / 255f);
            //Debug.Log(string.Format("r: '{0}' g: '{1}' b: '{2}'",r,g,b));
            Color color = new Color(r, g, b, 1);
            //Debug.Log(color);
            ps.startColor = color;
            //Debug.Log(ps.startColor);   

            //bubble.particleSystem.Play();
            //bubble.particleSystem.enableEmission = true;
            

            bubble.transform.position = position;

			//bubble.renderer.material.mainTextureScale = new Vector2(0.5f, 1f);
			bubble.renderer.material.mainTexture = texture;
		}

        public static void Pop(GameObject bubble)
        {
            //bubble.AddComponent<ParticleSystem>();
            bubble.renderer.enabled = false;
            var ps = bubble.GetComponent<ParticleSystem>();
            ps.enableEmission = true;
            ps.Play();
            //ps.particleEmitter.emit = true;
            //ps.enableEmission = true;
            //ps.Play();
            //ps.gravityModifier = 1f;
            //ps.startColor = 
            //ps.particleEmitter.Emit(10);
            GameObject.Destroy(bubble, 1);
        }

	}
}
