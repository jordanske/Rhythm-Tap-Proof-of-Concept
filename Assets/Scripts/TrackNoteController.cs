﻿using UnityEngine;
using System.Collections;

public class TrackNoteController : MonoBehaviour {

    public float speed;
	public TrackManager trackmanager;

	// Use this for initialization
	void Start () {
        gameObject.transform.localScale = new Vector3(TrackManager.trackWidth, 1, 0);
	}
	
	// Update is called once per frame
	void Update () {
        gameObject.transform.position += new Vector3(0, -speed * Time.deltaTime, 0);
		if (gameObject.transform.position.y <= -TrackManager.stageDimensions.y) {
			Destroy (gameObject);
			Debug.Log("Miss!");

		}
	}
}
