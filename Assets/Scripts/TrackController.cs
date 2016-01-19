using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TrackController : MonoBehaviour {

    //Track Note Prefab
    public TrackNoteController trackNotePrefab;

    //List containing trackNotes instances of this track
	private List<TrackNoteController> trackNotes = new List<TrackNoteController>();

	void Start () {
	    
	}

    public void spawnTrackNote () {
		//trackNotes.Add(Instantiate(trackNote, transform.position, transform.rotation) as TrackNoteController);
		TrackNoteController newNote = Instantiate(trackNotePrefab, transform.position, transform.rotation) as TrackNoteController;
		newNote.parentTrack = gameObject;
		trackNotes.Add(newNote);
    }
		
	void update(){
		foreach (TrackNoteController i in trackNotes) {
			if (i == null) {
				print(i);
				trackNotes.Remove(i);
			}
		}
	}
}
