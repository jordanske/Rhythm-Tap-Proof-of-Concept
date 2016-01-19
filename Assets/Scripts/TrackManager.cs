using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TrackManager : MonoBehaviour {

    //Track Prefab
    public TrackController trackPrefab;

    //The hitbar
    public GameObject hitbarPrefab;
    public static GameObject hitbar;

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
    
    public float baseCooldown;
    private float currentCooldown; //coroutine ?

    private int temp = 0;

	void Start () {
        setHitbar();
        updateTracks();
    }

    void Update () {
        if (tracks.Count <= 0) return;
        currentCooldown -= Time.deltaTime;
	    if(currentCooldown <= 0) {
            currentCooldown = baseCooldown;
            temp = temp >= activeTracks-1 ? 0 : temp + 1 ;
            tracks[temp].spawnTrackNote();

        }
	}

    private void setHitbar() {
        hitbar = Instantiate(hitbarPrefab, Vector2.zero, Quaternion.identity) as GameObject;
        Transform hitbarTr = hitbar.GetComponent<Transform>();
        SpriteRenderer hitbarSR = hitbar.GetComponent<SpriteRenderer>();

        var width = hitbarSR.sprite.bounds.size.x;

        SpriteRenderer test = trackPrefab.trackNotePrefab.GetComponent<SpriteRenderer>();

        Debug.Log(width);
        Debug.Log(test.sprite.bounds.size.x);

        hitbarTr.localScale = new Vector2((float) GameManager.cameraDimensions.x / width, hitbarTr.localScale.y);
            //GameManager.cameraDimensions.y camera hoogte
    }

    private void updateTracks() {
        trackWidth = GameManager.cameraDimensions.x / 4;
        float trackPadding = (GameManager.cameraDimensions.x - (trackWidth * trackCount)) / 2;
        for (int i = 0; i < trackCount; i++) {
            float trackX = (trackPadding + (i * trackWidth)) - (GameManager.cameraDimensions.x/2) + (trackWidth / 2);
            tracks.Add(Instantiate(trackPrefab, new Vector2(trackX, (GameManager.cameraDimensions.y/2) * 1.05f), Quaternion.identity) as TrackController);
        }
        currentCooldown = baseCooldown;
    }
}
