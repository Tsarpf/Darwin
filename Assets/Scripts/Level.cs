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
        Color32 bubbleAverageColor;
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
            bubbleAverageColor = averageColorFromTexture(textures["bubbleTexture"]);
            createGroundBubbles(floorBubblePositions.ToArray(), bubbleHeightFromGround, textures["bubbleTexture"]);

            List<Vector3> platformPositions = generatePlatformPositions(scaleMultiplier);
            float platformMinimumHeight = 2.5f;
            createPlatforms(platformPositions.ToArray(), platformMinimumHeight, textures["platformTexture"]);

			createGrass(floor.getUpperVertices()[0]);
		}

        private void createGrass(Vector3 position)
		{
			Assets.Scripts.GrassTexture grass = new Assets.Scripts.GrassTexture(textures["leaf"], 0.1f, 0.9f, 2f, 2f);
			GameObject go = new GameObject("grass");
			go.transform.position = new Vector3(-5,0,0);
			//go.transform.position = position;
            //go.AddComponent<
			//go.AddComponent("MeshFilter");
			//go.AddComponent("MeshRenderer");
			go.AddComponent<SpriteRenderer>();
			var spriteRenderer = go.GetComponent<SpriteRenderer>();
            Texture2D asdf = grass.getTexture();
            Rect rect = new Rect(0,0,asdf.width,asdf.height);
			Sprite sprite = Sprite.Create(grass.getTexture(), rect, new Vector2(0, 0));

            spriteRenderer.sprite = sprite;

			spriteRenderer.material = new Material(Shader.Find("Sprites/Diffuse"));

		}

        private Color32 averageColorFromTexture(Texture2D tex)
        {
            Color32[] texColors = tex.GetPixels32();
            int total = texColors.Length;
            float r = 0;

            float g = 0;

            float b = 0;
            for (int i = 0; i < total; i++)
            {
                r += texColors[i].r;
                g += texColors[i].g;
                b += texColors[i].b;
            }

            return new Color32((byte)(r / total), (byte)(g / total), (byte)(b / total), 0);
        }
        private List<Vector3> generatePlatformPositions(Vector3 scaleMultiplier)
        {
            Vector3[] positionsUnTranslated = floor.getUpperVertices();
            Vector3[] positions = new Vector3[positionsUnTranslated.Length];
            for (int i = 0; i < positions.Length; i++)
            {
                positions[i] = new Vector3(positionsUnTranslated[i].x * scaleMultiplier.x - 2   , positionsUnTranslated[i].y * scaleMultiplier.y, positionsUnTranslated[i].z * scaleMultiplier.z);
            }

            List<Vector3> platformPositions = new List<Vector3>();

            float platformStartChance = 0.2f; //should "chance" be "likelihood"?
            float platformContinueChance = 0.50f;
            bool platformOnGoing = false;

            System.Random random = new System.Random();

            for (int i = 0; i < positions.Length; i += 5)
            {
                float rand = (float)random.Next(101) / 100;
                if (!platformOnGoing)
                {
                    if (rand < platformStartChance)
                    {
                        platformPositions.Add(positions[i]);
                        platformOnGoing = true;
                    }
                }
                else if(platformOnGoing)
                {
                    if (rand < platformContinueChance)
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
                bubble = new Bubble(pos, tex, bubbleAverageColor);
				bubbles.Add(bubble);
				//createBubble(positions[i], height);

            }
        }

        private void createPlatforms(Vector3[] positions,  float minimumHeight, Texture2D tex)
        {
            Platform platform; 

            System.Random random = new System.Random();
            for(int i = 0; i < positions.Length; i++)
            {
                float height = random.Next(101) / 100; //in range of 0 to 5
                Vector3 pos = new Vector3(positions[i].x, positions[i].y + minimumHeight + height, positions[i].z);

                platform = new Platform(pos, tex);
                platforms.Add(platform);
            }

            int platformBubblePyramidSize = 3;
            for (int i = 0; i < platforms.Count; i++)
            {
                createPlatformBubbles(platforms[i].getPosition(), platformBubblePyramidSize, textures["bubbleTexture"]);
            }
        }


        private void createPlatformBubbles(Vector3 position, int width, Texture2D tex)
        {
            position = new Vector3(position.x - 2, position.y + 1, position.z);
            for (int j = 0; j < width; j++)
            {
                for (int i = width - j; i > 0; i--)
                {
                    //Bubble bubble = new Bubble(
                    Vector2 pos = new Vector2(i + j/2f + position.x, j + position.y);
                    Bubble bubble = new Bubble(pos, tex, bubbleAverageColor);
                    bubbles.Add(bubble);
                      //
                     ///
                    /////
                }
            }
            //for (int y = width; y > 0; y--)
            //{
            //    for (int x = width; x > 0; x--)
            //    {
                    
            //    }
            //}


        }
	}
}
