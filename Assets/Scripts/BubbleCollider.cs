﻿using UnityEngine;
using System.Collections;

public class BubbleCollider : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

	void OnTriggerEnter2D(Collider2D other)
	{
        //Destroy(gameObject);

        Assets.Bubble.Pop(gameObject);

        //BroadcastMessage("ebin", parametrit); //Tämä tehdään täällä

        /*
        void onEbin(parametrit)
        {
            //sijaitsen pisteenlaskuluokassa jou.
        }
        */

        //Todo: Add points or whatever.
	}

	
	// Update is called once per frame
	void Update () {
	
	}
}
