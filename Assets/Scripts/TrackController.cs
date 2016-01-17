using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TrackController : MonoBehaviour {

    public TrackNoteController trackNote;
	private List<TrackNoteController> trackNotes = new List<TrackNoteController>();

	void Start () {
	
	}

    public void spawnTrackNote() {
		trackNotes.Add(Instantiate(trackNote, transform.position, transform.rotation) as TrackNoteController);
		foreach (TrackNoteController o in trackNotes) {
			print(o);
		}
    }
		

	void Update () {
		foreach (TrackNoteController o in trackNotes) {
			if (o == null) {
				trackNotes.Remove (o);

			}
		}
	}
}
