using UnityEngine;
using System.Collections;

public class TrackNoteController : MonoBehaviour {


    public float speed;
	private GameObject ParentTrack;
	public GameObject parentTrack{
		get{return ParentTrack;}
		set{ParentTrack = value;}
	}
		
	void Start () {
        gameObject.transform.localScale = new Vector3(TrackManager.trackWidth, 1, 0);
	}

	void Update () {
        gameObject.transform.position += new Vector3(0, -speed * Time.deltaTime, 0);
		if (gameObject.transform.position.y <= -TrackManager.stageDimensions.y*1.05) {
			Destroy (gameObject);
			//Debug.Log("Miss!");
		}
	}

	// Click on note
	void OnMouseDown(){
		print(" HIT!");
		Destroy (gameObject);
	}
}