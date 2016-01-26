using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TrackController : MonoBehaviour {

    //Track Note Prefab
    public GameObject trackNotePrefab;

    //Object Pooler containing Track Notes
    private ObjectPooler trackNotesPooler;

	void Start () {
        SpriteRenderer sr = gameObject.GetComponent<SpriteRenderer>();
        transform.localScale = new Vector2(TrackManager.trackWidth / sr.sprite.bounds.size.x, GameManager.cameraDimensions.y / sr.sprite.bounds.size.y);
        trackNotePrefab.transform.localScale = new Vector2(TrackManager.trackWidth, TrackManager.trackWidth);

        trackNotesPooler = Instantiate(GameManager.ObjectPooler) as ObjectPooler;
        trackNotesPooler.initialize(trackNotePrefab, 4, true);
    }

    public void spawnTrackNote () {
        if(trackNotesPooler) { 
            GameObject newNote = trackNotesPooler.getObject();
            if(newNote) {
                //newNote.transform.position = new Vector3(transform.position.x, (GameManager.cameraDimensions.y / 2) + TrackManager.hitbarHeight, -4);
                newNote.GetComponent<TrackNoteController>().reset(transform.position.x, (int)Random.Range(-10f, 10f));
                newNote.SetActive(true);
            }
        }
    }
		
	void update () {

    }

    void OnMouseDown () {
        //TODO: Enumeration function ? idk, google
        List<GameObject> trackNotes = trackNotesPooler.PooledObjects;
        GameObject closest = null;
        float lastHitrate = -10000;
        for(int i = 0; i < trackNotes.Count; i++) {
            if(trackNotes[i].activeInHierarchy) {
                TrackNoteController trackNote = trackNotes[i].GetComponent<TrackNoteController>();
                float hitrate = trackNote.hitRate();
                
                if(hitrate < 100 && !trackNote.hit) { //Ignore notes below the hitbar
                    if(hitrate > lastHitrate) {
                        closest = trackNotes[i];
                        lastHitrate = hitrate;
                    }
                }
            }
        }

        if(closest) {
            if (Mathf.Abs(lastHitrate) <= 100) {
                float hitPerc = (100 - Mathf.Abs(lastHitrate)) / 100;
                TrackManager.current.onNoteHit(closest, hitPerc);
                return;
            }
        }

        TrackManager.current.onNoteMiss(null);
        //TrackManager.current.onNoteMiss(closest);
    }
    
}
