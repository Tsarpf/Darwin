using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
	class GrassTexture
	{
		Texture2D singleLeaf;
		float length;
		Texture2D grass;
		public GrassTexture(Texture2D leaf, float stepLength, float density, float height, float width)
		{
			singleLeaf = leaf;
			length = 0;
			System.Random random = new System.Random(DateTime.Now.Millisecond);

            /*
			for (float x = 0; x < width; x += stepLength)
			{
				float val = random.Next(101) / 100f;
				if (val > density)
				{
					continue;
				}

				bool inverted;
				bool upsideDown;

				int inv = random.Next(0, 2); //2 because range is exclusive
				int ups = random.Next(0, 2); //2 because range is exclusive
				if (inv == 0) inverted = true;
				else inverted = false;

				if (ups == 0) upsideDown = true;
				else upsideDown = false;

				int orientation = random.Next(-20, 20);

                //newLeaf.
			}
            */

				Texture2D newLeaf = leaf;
				grass = invertY(leaf);
				grass = invertX(grass);
				//grass = leaf;
		}

		public Texture2D getTexture()
		{
			return grass;
		}

		//TODO: rotate using something similar to http://forum.unity3d.com/threads/23904-Rotate-a-texture-with-an-arbitrary-angle?p=159007&viewfull=1#post159007
		//private Texture2D rotate(Texture2D tex, int degrees)
		//{
		//	Vector2 pivot = new Vector2(tex.width / 2, tex.height / 2);
		//	GUIUtility.RotateAroundPivot(degrees, pivot);
		//}

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
			newTexture.Apply();

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
			newTexture.Apply();

            /*
			for (int i = 0; i < origPixels.Length; i+= 100)
			{
			    Debug.Log(newPixels[i]);
			}
            */

			return newTexture;
		}

		private int getIdx(int x, int y, int width)
		{
			return y * width + x;
		}


		public void generate()
		{
		}
	}
}
