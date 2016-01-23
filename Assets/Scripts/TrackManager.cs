using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TrackManager : MonoBehaviour {

    public static TrackManager current;

    //Track Prefab
    public TrackController trackPrefab;

    //The hitbar
    public GameObject hitbarPrefab;
    public static GameObject hitbar;
    public static float hitbarHeight;

    //The width of one track
    public static float trackWidth;

    //Number of track to spawn?
    public static int trackCount;

    //List containing all track instances
    private static List<TrackController> tracks = new List<TrackController>();

    //Current active tracks
    public int activeTracks {
        get {
            return tracks.Count;
        }
    }
    
    public float TimeTillNextNote;

    private int temp = 0;

    void Awake() {
        current = this;
    }

    void Start () {
        updateTracks();
        setHitbar();

        StartCoroutine(spawnNotes());
    }
    
    IEnumerator spawnNotes () {
        while(true) {
            if (!GameManager.pause) { //Temporary
                if (tracks.Count > 0) {
                    tracks[0].spawnTrackNote();
                }
            }
            yield return new WaitForSeconds(TimeTillNextNote);
        }
    }

    void Update () {

	}

    private void setHitbar() {
        hitbar = Instantiate(hitbarPrefab, new Vector3(0, (-GameManager.cameraDimensions.y*0.7f) /2, -2), Quaternion.identity) as GameObject;
        Transform hitbarTr = hitbar.GetComponent<Transform>();
        SpriteRenderer hitbarSR = hitbar.GetComponent<SpriteRenderer>();
        //Full width: GameManager.cameraDimensions.x
        hitbarTr.localScale = new Vector2((float) (trackWidth * trackCount) / hitbarSR.sprite.bounds.size.x, TrackManager.trackWidth);
        hitbarHeight = hitbar.transform.localScale.y * hitbar.GetComponent<SpriteRenderer>().sprite.bounds.size.y;
    }

    private void updateTracks() {
        trackWidth = GameManager.cameraDimensions.x / 4;
        float trackPadding = (GameManager.cameraDimensions.x - (trackWidth * trackCount)) / 2;
        for (int i = 0; i < trackCount; i++) {
            float trackX = (trackPadding + (i * trackWidth)) - (GameManager.cameraDimensions.x/2) + (trackWidth / 2);
            tracks.Add(Instantiate(trackPrefab, new Vector3(trackX, 0, 0), Quaternion.identity) as TrackController);
        }
    }

    public void onNoteHit(GameObject note, float hitPerc) {
        note.GetComponent<TrackNoteController>().hit = true;
        Debug.Log("HIT");
    }

    public void onNoteMiss(GameObject note) {
        Debug.Log("Miss");
    }
}
