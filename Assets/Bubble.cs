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

            bubble = GameObject.CreatePrimitive(PrimitiveType.Cylinder);

			var collider = bubble.GetComponent<CapsuleCollider>();
			collider.isTrigger = true;
			bubble.AddComponent("BubbleCollider");

            bubble.transform.position = position;
			//bubble.transform.rotation = new Quaternion(90, bubble.transform.rotation.y, bubble.transform.rotation.z, bubble.transform.rotation.w);
			bubble.transform.Rotate(new Vector3(90, 0, 0));

			//bubble.renderer.material.mainTextureScale = new Vector2(0.5f, 1f);
			bubble.renderer.material.mainTexture = texture;
		}
	}
}
