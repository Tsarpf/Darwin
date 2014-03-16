using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using LibNoise;

namespace Assets
{
	class Floor
	{
		Vector3[] upperVertices;
		Vector3[] allVertices;
		Texture2D texture;
		GameObject floorGameObject;
		MeshData meshData;


		public Floor(int length, Texture2D texture, Vector3 scaleMultiplier)
		{
			this.texture = texture;

			float multiplier = scaleMultiplier.x;
			//Vector3 scaleMultiplier = new Vector3(250, 25, 1);
			
			int blockCount = (length - 1) / 2;
            int vertexCount = blockCount * 5;
            int indexCount = blockCount * 9;
			upperVertices = generateUpperVertices(length, multiplier);

			meshData = generateMeshData(vertexCount, indexCount);

			floorGameObject = initializeGroundFloor(meshData, scaleMultiplier, texture);
		}

        public Vector3[] getUpperVertices()
		{
			return upperVertices;
		}

		private struct MeshData
		{
			public Vector3[] vertices;
			public int[] indices;
			public List<Vector2> UVs;
		}

		private MeshData generateMeshData(int vertexCount, int indexCount)
		{
			MeshData meshData = new MeshData();

			meshData.vertices = new Vector3[vertexCount];
			meshData.indices = new int[indexCount];
			meshData.UVs = new List<Vector2>();

            
			//Debug.Log(string.Format("upperVerticescount: '{0}' idxs: '{1}' vertexes: '{2}'", upperVertices.Length, idxs.Length, meshData.vertices.Length));

            int upperVerticesIdx = 0;
            int indexIdx = 0;
            for (int vertexIdx = -1; vertexIdx < meshData.vertices.Length - 1;)
            {

                //var asdf = meshData.vertices[vertexIdx + 1];
                //var fdsa = upperVertices[upperVerticesIdx];
                meshData.vertices[++vertexIdx] = lowerVertex(upperVertices[upperVerticesIdx]);
				//Debug.Log(meshData.vertices[vertexIdx]);
                meshData.indices[indexIdx++] = vertexIdx;

                meshData.UVs.Add(new Vector2(0, 0)); 

                meshData.vertices[++vertexIdx] = upperVertices[upperVerticesIdx];
				//Debug.Log(meshData.vertices[vertexIdx]);
                meshData.indices[indexIdx++] = vertexIdx;
                //meshData.UVs.Add(new Vector2(0, 1)); 
                meshData.UVs.Add(new Vector2(0, meshData.vertices[vertexIdx].y)); 

				//var asdf = meshData.vertices[vertexIdx + 1];
				//var fdsa = upperVertices[upperVerticesIdx + 1];
                meshData.vertices[++vertexIdx] = upperVertices[++upperVerticesIdx];
				//Debug.Log(meshData.vertices[vertexIdx]);
                meshData.indices[indexIdx++] = vertexIdx;
                //meshData.UVs.Add(new Vector2(0.5f, 1)); 
                meshData.UVs.Add(new Vector2(0.5f, meshData.vertices[vertexIdx].y)); 



                //second
                meshData.indices[indexIdx++] = vertexIdx - 2;

                meshData.indices[indexIdx++] = vertexIdx;

                meshData.vertices[++vertexIdx] = lowerVertex(upperVertices[++upperVerticesIdx]);
                //Debug.Log(meshData.vertices[vertexIdx]);
                meshData.indices[indexIdx++] = vertexIdx;
                    meshData.UVs.Add(new Vector2(1, 0)); 

                //third
                meshData.indices[indexIdx++] = vertexIdx;
                meshData.indices[indexIdx++] = vertexIdx - 1;

                meshData.vertices[++vertexIdx] = upperVertices[upperVerticesIdx];
                //Debug.Log(meshData.vertices[vertexIdx]);
                meshData.indices[indexIdx++] = vertexIdx;
                //meshData.UVs.Add(new Vector2(1, 1));
                meshData.UVs.Add(new Vector2(1, meshData.vertices[vertexIdx].y)); 

			    //Debug.Log(string.Format("indexIdx: '{0}' vertexIdx: '{1}' heightIdx: '{2}', uvcount: '{3}'", indexIdx, vertexIdx, heightIdx, meshData.UVs.Count));
                //vertexIdx--;
            }

			return meshData;
		}

        private GameObject initializeGroundFloor(MeshData meshData, Vector3 scaleMultiplier, Texture2D texture)
        {
            GameObject block = new GameObject();
			block.transform.localScale = new Vector3(block.transform.localScale.x * scaleMultiplier.x,
				                                     block.transform.localScale.y * scaleMultiplier.y,
													 block.transform.localScale.z * scaleMultiplier.z);

            block.name = "Terrain";

            Mesh mesh = new Mesh();

            //Debug.Log(meshData.vertices.Length + " " + meshData.UVs.Count);

			mesh.vertices = meshData.vertices;
			mesh.triangles = meshData.indices;
			mesh.uv = meshData.UVs.ToArray();

            block.AddComponent("MeshFilter");
            block.AddComponent("MeshRenderer");
            //block.AddComponent("MeshCollider");
            //block.AddComponent("PolygonCollider2D");
            block.AddComponent("EdgeCollider2D");

            block.renderer.material.mainTextureScale = new Vector2(0.5f, 1f);
			block.renderer.material.mainTexture = texture;

            var meshFilter = block.GetComponent<MeshFilter>();
            meshFilter.mesh = mesh;
            meshFilter.mesh.RecalculateNormals();

            //var meshCollider = block.GetComponent<MeshCollider>();
            //meshCollider.sharedMesh = mesh;
            var edgeCollider = block.GetComponent<EdgeCollider2D>();

            //Vector2[] verts = new Vector2[meshData.vertices.Length];
            //for(int i = 0; i < meshData.vertices.Length; i++)
            //{
            //    verts[i] = new Vector2(meshData.vertices[i].x, meshData.vertices[i].y); 
            //}

            Vector2[] verts = new Vector2[upperVertices.Length];
            for (int i = 0; i < upperVertices.Length; i++)
            {
                verts[i] = new Vector2(upperVertices[i].x, upperVertices[i].y);
            }

            edgeCollider.points = verts;

			//meshCollider.collider.bounds.SetMinMax(new Vector3(50, 50, 50), new Vector3(100, 100, 100));

            //var collider = block.GetComponent<PolygonCollider2D>();
            //collider.collider2D.

			return block;
        }

        private Vector3[] generateUpperVertices(int upperVertCount, float frequencyMultiplier)
        {
            //block count == (height - 1) / 3
            //vertex count per block == 5
            //index count per block == 9

            int heightCount = upperVertCount; //height count should be (divisable by three) + 1
            Vector3[] height = new Vector3[heightCount]; //*2 + 1


            Perlin perlin = new Perlin();
            perlin.Seed = DateTime.Now.Millisecond;
            float x = 0;
            float multiplier = frequencyMultiplier;

            for (int i = 0; i < height.Length; i++)
            {
                float value = (float)perlin.GetValue(x + 0.005f * i, 0, 0);
                value += 1;
                value = value / 2;
                value += 0.3f;
                height[i] = new Vector3(x, value, 0);
                x += 1.5f / (multiplier + 500);
            }

            return height;
        }
		
		private Vector3 lowerVertex(Vector3 upper)
        {
            return new Vector3(upper.x, 0, 0);
        }
	}
}
