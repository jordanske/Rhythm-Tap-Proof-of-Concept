using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TrackController : MonoBehaviour {

    //Track Note Prefab
    public TrackNoteController trackNotePrefab;

    //List containing trackNotes instances of this track
	private List<TrackNoteController> trackNotes = new List<TrackNoteController>();

	void Start () {
        SpriteRenderer sr = gameObject.GetComponent<SpriteRenderer>();
        transform.localScale = new Vector2(TrackManager.trackWidth / sr.sprite.bounds.size.x, GameManager.cameraDimensions.y / sr.sprite.bounds.size.y);
    }

    public void spawnTrackNote () {
		//trackNotes.Add(Instantiate(trackNote, transform.position, transform.rotation) as TrackNoteController);
		TrackNoteController newNote = Instantiate(trackNotePrefab, new Vector3(transform.position.x, (GameManager.cameraDimensions.y / 2) + TrackManager.hitbarHeight, -20), transform.rotation) as TrackNoteController;
		newNote.parentTrack = gameObject;
		trackNotes.Add(newNote);
    }
		
	void update() {

    }

    void OnMouseDown() {
        foreach (TrackNoteController trackNote in trackNotes) {
            float hitrate = trackNote.hitRate();
            
            if(Mathf.Abs(hitrate) <= 100) {
                float perc = (100 - Mathf.Abs(hitrate));
                Debug.Log("HIT");
                break;
            } else if(hitrate < -100) {
                Debug.Log("MISS");
                break;
            }

        }
    }

    public void destroyNote(GameObject note) {
        Destroy(note);
        trackNotes.RemoveAt(0);
    }
}
