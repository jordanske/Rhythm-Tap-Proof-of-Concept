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
        transform.localScale = new Vector2(TrackManager.trackWidth, TrackManager.trackWidth);
    }

	void Update () {
        float tspeed = GameManager.pause ? 0 : speed ;
        transform.position += new Vector3(0, -tspeed * Time.deltaTime, 0);
        
        if (transform.position.y + (transform.localScale.y / 2) <= (-GameManager.cameraDimensions.y / 2)) {
            // didn't click on note
            //Destroy (gameObject);
            ParentTrack.GetComponent<TrackController>().destroyNote(gameObject);
        }
	}

    public float hitRate() {        
        float relativePosition = TrackManager.hitbar.transform.position.y - transform.position.y;
        //float hitbarHeight = TrackManager.hitbar.transform.localScale.y * TrackManager.hitbar.GetComponent<SpriteRenderer>().sprite.bounds.size.y;
        float percentage = relativePosition / TrackManager.hitbarHeight * 100;

        return percentage;
    }

	// Clicked on note
	/*void OnMouseDown(){
		hitPercent = (gameObject.transform.position.y / hitbarY) * 100;
		if (hitPercent > 100) {hitPercent = 200 - hitPercent;}

		if (gameObject.transform.position.y < hitbarY * 0.5f && gameObject.transform.position.y > hitbarY * 1.5f) {
			print (" HIT! "+ hitPercent + "%");
			Destroy (gameObject);
		} else {
			print (" MISS! "+ hitPercent + "%");
			Destroy (gameObject);
		}

	}*/
}