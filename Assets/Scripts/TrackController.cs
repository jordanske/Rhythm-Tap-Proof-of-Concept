using UnityEngine;
using System.Collections;

public class TrackController : MonoBehaviour {

    public TrackNoteController trackNote;
	
	void Start () {
	
	}

    public void spawnTrackNote() {
        Instantiate(trackNote, transform.position, transform.rotation);
    }
	
}
