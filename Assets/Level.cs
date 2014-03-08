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
        List<Platform> platforms;

		public Level(Dictionary<string, string> textureLocations, int mapLength)
		{
			this.textures = loadTextures(textureLocations);
			bubbles = new List<Bubble>();
            platforms = new List<Platform>();

			Vector3 scaleMultiplier = new Vector3(7500, 100, 1);
			floor = new Floor(mapLength, textures["floorTexture"], scaleMultiplier);

			Vector3[] upperVertices = floor.getUpperVertices();
			List<Vector3> floorBubblePositions = new List<Vector3>();

			for (int i = 0; i < upperVertices.Length; i+=5)
			{
				Vector3 pos = new Vector3(upperVertices[i].x * scaleMultiplier.x, upperVertices[i].y * scaleMultiplier.y, upperVertices[i].z * scaleMultiplier.z);
				//floorBubblePositions.Add(upperVertices[i]);
				floorBubblePositions.Add(pos);
			}

			int bubbleHeightFromGround = 1;
			createGroundBubbles(floorBubblePositions.ToArray(), bubbleHeightFromGround, textures["bubbleTexture"]);

            List<Vector3> platformPositions = generatePlatformPositions(scaleMultiplier);

            int platformMinimumHeight = 1;
            createPlatforms(platformPositions.ToArray(), platformMinimumHeight, textures["platformTexture"]);
		}

        private List<Vector3> generatePlatformPositions(Vector3 scaleMultiplier)
        {
            Vector3[] positionsUnTranslated = floor.getUpperVertices();
            Vector3[] positions = new Vector3[positionsUnTranslated.Length];
            for (int i = 0; i < positions.Length; i++)
            {
                positions[i] = new Vector3(positionsUnTranslated[i].x * scaleMultiplier.x, positionsUnTranslated[i].y * scaleMultiplier.y, positionsUnTranslated[i].z * scaleMultiplier.z);
            }

            List<Vector3> platformPositions = new List<Vector3>();

            float platformStartChance = 0.2f; //should "chance" be "likelihood"?
            float platformContinueChance = 0.8f;
            bool platformOnGoing = false;

            System.Random random = new System.Random();

            for (int i = 0; i < positions.Length; i += 5)
            {
                float rand = (float)random.Next(101) / 100;
                //Debug.Log(rand);
                if (!platformOnGoing)
                {
                    if (rand > platformStartChance)
                    {
                        platformPositions.Add(positions[i]);
                        platformOnGoing = true;
                    }
                }
                else if(platformOnGoing)
                {
                    if (rand > platformContinueChance)
                    {
                        platformPositions.Add(positions[i]);
                    }
                    else
                    {
                        platformOnGoing = false;
                    }
                }
            }

            return platformPositions;
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

		private void createGroundBubbles(Vector3[] positions, int height, Texture2D tex)
        {
			Bubble bubble;
            for (int i = 0; i < positions.Length; i++)
            {
                Vector3 pos = new Vector3(positions[i].x, positions[i].y + height, positions[i].z);
				//Debug.Log(pos);
                bubble = new Bubble(pos, tex);
				bubbles.Add(bubble);
				//createBubble(positions[i], height);

            }
        }

        private void createPlatforms(Vector3[] positions, int minimumHeight, Texture2D tex)
        {
            Platform platform; 

            System.Random random = new System.Random();
            for(int i = 0; i < positions.Length; i++)
            {
                float height = random.Next(101) / 40; //in range of 0 to 5
                Vector3 pos = new Vector3(positions[i].x, positions[i].y + minimumHeight + height, positions[i].z);
                //Debug.Log(pos);
                platform = new Platform(pos, tex);
                platforms.Add(platform);
            }
        }
	}
}
