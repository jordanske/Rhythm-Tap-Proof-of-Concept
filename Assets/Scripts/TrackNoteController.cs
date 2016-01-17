using UnityEngine;
using System.Collections;

public class TrackNoteController : MonoBehaviour {


    public float speed;
	public float hitbarY;
	public float hitPercent;

	private GameObject ParentTrack;
	public GameObject parentTrack{
		get{return ParentTrack;}
		set{ParentTrack = value;}
	}
		
	void Start () {
        gameObject.transform.localScale = new Vector3(TrackManager.trackWidth, 1, 0);
		hitbarY = -TrackManager.stageDimensions.y*0.70f;
		//print(hitbarY);
	}

	void Update () {
        gameObject.transform.position += new Vector3(0, -speed * Time.deltaTime, 0);
		if (gameObject.transform.position.y <= -TrackManager.stageDimensions.y*1.05) {
			// didn't click on note
			Destroy (gameObject);
		}
	}

	// Clicked on note
	void OnMouseDown(){
		hitPercent = (gameObject.transform.position.y / hitbarY) * 100;
		if (hitPercent > 100) {hitPercent = 200 - hitPercent;}

		if (gameObject.transform.position.y < hitbarY * 0.5f && gameObject.transform.position.y > hitbarY * 1.5f) {
			print (" HIT! "+ hitPercent + "%");
			Destroy (gameObject);
		} else {
			print (" MISS! "+ hitPercent + "%");
			Destroy (gameObject);
		}

	}
}