using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


namespace Assets
{
	class Level 
	{
		Floor floor;
		Dictionary<string, Texture2D> textures;
		List<Bubble> bubbles;

		public Level(Dictionary<string, string> textureLocations, int mapLength)
		{
			this.textures = loadTextures(textureLocations);
			bubbles = new List<Bubble>();

			Vector3 scaleMultiplier = new Vector3(250, 25, 1);
			floor = new Floor(mapLength, textures["floorTexture"], scaleMultiplier);

			Vector3[] upperVertices = floor.getUpperVertices();
			List<Vector3> floorBubblePositions = new List<Vector3>();

			for (int i = 0; i < upperVertices.Length; i+=3)
			{
				Vector3 pos = new Vector3(upperVertices[i].x * scaleMultiplier.x, upperVertices[i].y * scaleMultiplier.y, upperVertices[i].z * scaleMultiplier.z);
				//floorBubblePositions.Add(upperVertices[i]);
				floorBubblePositions.Add(pos);
			}

			int bubbleHeightFromGround = 1;
			createBubbles(floorBubblePositions.ToArray(), bubbleHeightFromGround, textures["bubbleTexture"]);
		}

		private Dictionary<string, Texture2D> loadTextures(Dictionary<string, string> textures)
		{
			Dictionary<string, Texture2D> texs = new Dictionary<string, Texture2D>();

			foreach (KeyValuePair<string, string> kvp in textures)
			{
				Texture2D tex = Resources.Load(kvp.Value) as Texture2D;
				texs[kvp.Key] = tex;
			}

			return texs;
		}

		private void createBubbles(Vector3[] positions, int height, Texture2D tex)
        {
			Bubble bubble;
			Debug.Log("length: " + positions.Length);
            for (int i = 0; i < positions.Length; i++)
            {
                Vector3 pos = new Vector3(positions[i].x, positions[i].y + height, positions[i].z);
				Debug.Log(pos);
                bubble = new Bubble(pos, tex);
				bubbles.Add(bubble);
				//createBubble(positions[i], height);

            }
        }
	}
}
