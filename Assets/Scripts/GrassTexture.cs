using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
	class GrassTexture
	{
        float density;
        int height;
        int width;
        int stepLength;
		Texture2D grass;
        GameObject go;
        Texture2D leaf;
        Color32 color;
		public GrassTexture(Texture2D leaf, int stepLength, float density, int height, int width, Color32 color)
		{
            this.color = color;
            this.density = density;
            this.stepLength = stepLength;
            this.height = height;
            this.width = width;
            this.leaf = leaf;

            List<Texture2D> layers = new List<Texture2D>();
            int layerCount = 3;
            for(int i = 0; i < layerCount; i++)
            {
                Debug.Log("creating layer");
                layers.Add(createTextureLayer());
            }

                Debug.Log("creating final texture");
            grass = createFinalTexture(layers);


            Debug.Log("creating object");
            createObject();
            grass.Apply();
		}

        private Texture2D createTextureLayer()
        {
			System.Random random = new System.Random(DateTime.Now.Millisecond);
            Texture2D singleLeaf = new Texture2D(leaf.width, leaf.height);
            Texture2D layer = createNewEmptyTexture(width, height);

			for (int x = 0; x < width - leaf.width; x += stepLength)
			{
				float val = random.Next(101) / 100f;
				if (val > density)
				{
					continue; //No leaf for this x position
				}

                singleLeaf = leaf;

				int inv = random.Next(0, 2); //2 because range is exclusive
				int ups = random.Next(0, 2); //2 because range is exclusive
				if (inv == 0) singleLeaf = invertX(singleLeaf);

				if (ups == 0) singleLeaf = invertY(singleLeaf);

                float resizeMultiplier = random.Next(30, 101);
                resizeMultiplier /= 100f;

                singleLeaf = resizeLeaf(singleLeaf, resizeMultiplier);

				//int orientation = random.Next(-20, 20);
                //todo: leaf random orientation
		        //TODO: rotate using something similar to http://forum.unity3d.com/threads/23904-Rotate-a-texture-with-an-arbitrary-angle?p=159007&viewfull=1#post159007
                

                //int yMin = singleLeaf.height;
                int yMin = 0;
                int yMax = height - singleLeaf.height;
                int y = random.Next(yMin, yMax + 1);
                Color32[] colors = singleLeaf.GetPixels32();
                //grass.SetPixels(x, y, singleLeaf.width, singleLeaf.height, colors);
                setOnlyAlphaPositivePixels(x, y, singleLeaf.width, singleLeaf.height, colors, layer);
                layer.Apply();
                //Debug.Log(string.Format("x: {0} y: {1} blockwidth: {2} blockheight: {3}", x, y, singleLeaf.width, singleLeaf.height));
			}

            layer.Apply();

            return layer;

        }

        private Texture2D createFinalTexture(List<Texture2D> layers)
        {
            Texture2D grass = createNewEmptyTexture(width, height);

            float maxContrastDifference = 0.3f;

            float contrastStep = maxContrastDifference / (float)(layers.Count);

            /*
            int idx = 0;
            for (float i = 0; i < maxContrastDifference; i += contrastStep)
            {
                Color[] colors = layers[idx].GetPixels();

                if(i != 0) decreaseContrast(layers.Count i, colors);

                setOnlyAlphaPositivePixels(0, 0, width, height, colors, grass);

                idx++;
            }
            */
            float currentContrastChange = maxContrastDifference;
            for(int i = 0; i < layers.Count; i++)
            {
                Color32[] colors = layers[i].GetPixels32();

                colors = decreaseContrast(currentContrastChange, colors);

                setOnlyAlphaPositivePixels(0,0, width, height, colors, grass);
                grass.Apply();

                currentContrastChange -= contrastStep;
            }

            return grass;
        }

        private Color32[] decreaseContrast(float howMuch, Color32[] colors)
        {
            float multiplier = 1f - howMuch; 

            for (int i = 0; i < colors.Length; i++)
            {
                byte r = Convert.ToByte(colors[i].r * multiplier);
                byte g = Convert.ToByte(colors[i].g * multiplier);
                byte b = Convert.ToByte(colors[i].b * multiplier);
                byte a = colors[i].a;
                colors[i] = new Color32(r, g, b, a);
            }

            return colors;
        }

        private void setOnlyAlphaPositivePixels(int x, int y, int blockWidth, int blockHeight, Color32[] colors, Texture2D target)
        {
            for (int i = x; i < blockWidth + x; i++)
            {
                for (int j = y; j < blockHeight + y; j++)
                {
                    Color col = colors[getIdx(i - x, j - y, blockWidth)];
                    if (col.a != 0)
                    {
                        target.SetPixel(i, j, col);
                    }
                }
            }
        }

        private Texture2D createNewEmptyTexture(int width, int height)
        {
            Texture2D newTexture = new Texture2D(width, height);
            Color32[] colors = new Color32[width * height];
            for(int i = 0; i < width*height; i++)
            {
                colors[i] = new Color32(0, 0, 0, 0);
            }

            newTexture.SetPixels32(colors);
            return newTexture;
        }

        private Texture2D resizeLeaf(Texture2D singleLeaf, float multiplier)
        {
            //leaf.Resize((int)(leaf.width * multiplier), (int)(leaf.height * multiplier));
            //leaf.Apply();
            /*
            	var newTex = Instantiate (tex);
	renderer.material.mainTexture = newTex;
	TextureScale.Bilinear (newTex, tex.width*2, tex.height*2);
            */

            Texture2D newTex = Texture2D.Instantiate(singleLeaf) as Texture2D;
            


            TextureScale.Bilinear(newTex, (int)(singleLeaf.width * multiplier), (int)(singleLeaf.height * multiplier));
            return newTex;
        }

        private void createObject()
        {
            go = new GameObject("Grass");
            go.transform.position = new Vector3(-5, 0, 0);
            //go.transform.position = position;
            //go.AddComponent<
            //go.AddComponent("MeshFilter");
            //go.AddComponent("MeshRenderer");
            go.AddComponent<SpriteRenderer>();
            var spriteRenderer = go.GetComponent<SpriteRenderer>();
            Rect rect = new Rect(0, 0, grass.width, grass.height);
            Sprite sprite = Sprite.Create(grass, rect, new Vector2(0, 0));

            spriteRenderer.sprite = sprite;

            spriteRenderer.material = new Material(Shader.Find("Sprites/Default"));
            spriteRenderer.color = color;
        }

		public Texture2D getTexture()
		{
			return grass;
		}

        public GameObject getGameObject()
        {
            return go;
        }

        private Texture2D invertX(Texture2D original)
		{
			Texture2D newTexture = new Texture2D(original.width, original.height);
            Color32[] origPixels =  original.GetPixels32();
			Color32[] newPixels = new Color32[origPixels.Length];
			for (int x = 0; x < original.width; x++)
			{
				for (int y = 0; y < original.height; y++)
				{
					newPixels[getIdx(x, y, original.width)] = origPixels[getIdx(original.width - x - 1, y, original.width)];
				}
			}

			newTexture.SetPixels32(newPixels);
            //newTexture.Apply();

			return newTexture;
		}

        private Texture2D invertY(Texture2D original)
		{
			Texture2D newTexture = new Texture2D(original.width, original.height);
            Color32[] origPixels =  original.GetPixels32();
			Color32[] newPixels = new Color32[origPixels.Length];
			for (int x = 0; x < original.width; x++)
			{
				for (int y = 0; y < original.height; y++)
				{
					newPixels[getIdx(x, y, original.width)] = origPixels[getIdx(x, original.height - y - 1, original.width)];
				}
			}

			newTexture.SetPixels32(newPixels);
            //newTexture.Apply();

			return newTexture;
		}

		private int getIdx(int x, int y, int width)
		{
			return y * width + x;
		}
	}
}
