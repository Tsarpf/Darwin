using UnityEngine;
using System.Collections;

public class TextStuff : MonoBehaviour {

    Transform cameraTransform;
    GameObject cameraGO;
	// Use this for initialization
	void Start () {
        cameraTransform = GameObject.FindGameObjectWithTag("MainCamera").transform;
        cameraGO = GameObject.FindGameObjectWithTag("MainCamera");
        
	}
	
	// Update is called once per frame
	void Update () {
        //transform.position = new Vector3(cameraTransform.position.x, cameraTransform.position.y, transform.position.z);
	}

    public void showTextMoving(string text)
    {
        GameObject textGameObject = createNewText(text);

        //Fade(textGameObject);
        StartCoroutine(Fade(textGameObject));
        StartCoroutine(MoveWhileVisible(textGameObject,cameraGO));
        //MoveWhileVisible(textGameObject, cameraGO);

        
    }

    public void showTextStatic(string text, Vector2 position)
    {
        GameObject textGameObject = createNewText(text);
        textGameObject.transform.position = position;
        StartCoroutine(Fade(textGameObject));
    }

    private GameObject createNewText(string text, string font = "Arial")
    {
        /*
        //GameObject textGameObject = new GameObject("TextThingy");
        textGameObject.AddComponent<TextMesh>();
        var textComponent = textGameObject.GetComponent<TextMesh>();
        textComponent.text = text;
        textComponent.font = new Font(font);
        */


        /////////// 
        GameObject prefab = (GameObject)Resources.Load("TextTest");
        GameObject textGameObject = (GameObject)GameObject.Instantiate(prefab);
        var component = textGameObject.GetComponent<TextMesh>();
        component.text = text;
        ///////////

        return textGameObject;
    }

    /*
     *             GameObject prefab = (GameObject)Resources.Load("BubblePrefab");
            bubble = (GameObject)GameObject.Instantiate(prefab)

         var wallTxt : TextMesh = Instantiate(textMeshPrefab, Vector3.up*10, Quaternion.identity);
    wallTxt.text = "Hello World";
     * 
     * 
     * 
     var wallTxt = new GameObject("TextField");
wallTxt.AddComponent(TextMesh);
wallTxt.AddComponent(MeshRenderer);
var meshRender: MeshRenderer = wallTxt.GetComponent(MeshRenderer);
var material: Material = meshRender.material;
meshRender.material = Resources.Load("Arial", Material);
wallTxt.GetComponent (TextMesh).text = "Hello world";
var myFont : Font = Resources.Load("Arial",Font);
wallTxt.GetComponent(TextMesh).font = myFont;
     * */

    IEnumerator MoveWhileVisible(GameObject followerGO, GameObject gameObjectToFollow)
    {
        while (followerGO.renderer.material.color.a > 0)
        {
            followerGO.transform.position = new Vector3(gameObjectToFollow.transform.position.x,
                gameObjectToFollow.transform.position.y, followerGO.transform.position.z);
            yield return null;
        }
    }

    IEnumerator Fade(GameObject go)
    {
        for (float f = 1f; f >= 0; f -= 0.01f)
        {
            Color c = go.renderer.material.color;
            c.a = f;
            go.renderer.material.color = c;
            yield return null;
        }
    }


}
