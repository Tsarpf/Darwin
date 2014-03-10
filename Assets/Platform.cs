using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets
{
    class Platform
    {
        Vector3 position;
        Texture2D texture;
        GameObject platform;
        
        public Platform(Vector3 position, Texture2D texture)
        {
            //this.position = new Vector3(position.x, position.y, position.z);
            this.position = position;
            this.texture = texture;

            //platform = GameObject.CreatePrimitive(PrimitiveType.Cube);
            
            GameObject prefab = (GameObject)Resources.Load("PlatformPrefab");
            platform = (GameObject)GameObject.Instantiate(prefab);

            platform.name = "Platform";

            //var collider = platform.GetComponent<CapsuleCollider>();
            //collider.isTrigger = true;
            platform.AddComponent("PlatformCollider");

            platform.transform.position = position;
            //platform.transform.Rotate(new Vector3(90, 0, 0));

            //platform.renderer.material.mainTextureScale = new Vector2(0.5f, 1f);
            platform.renderer.material.mainTexture = texture;
        }
    }
}
