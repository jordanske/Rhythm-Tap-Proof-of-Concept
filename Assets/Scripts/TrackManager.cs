using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TrackManager : MonoBehaviour {

    public TrackController trackPrefab;
    public int trackCount;
    public int maxTracks;
	public static Vector2 stageDimensions;
    public static float trackWidth;
    private static List<TrackController> tracks = new List<TrackController>();
    public int activeTracks {
        get {
            return tracks.Count;
        }
    }
    
    public float baseCooldown;
    private float currentCooldown;

    private int temp = 0;

	// Use this for initialization
	void Start () {
        stageDimensions = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        float screenWidth = stageDimensions.x * 2;
        trackWidth = screenWidth / 4;
        float trackPadding = (screenWidth - (trackWidth * maxTracks)) / 2;
        for (int i = 0; i < maxTracks; i++) {
            float trackX = (trackPadding + (i * trackWidth)) - stageDimensions.x + (trackWidth / 2);
            
            tracks.Add(Instantiate(trackPrefab, new Vector2(trackX, stageDimensions.y), Quaternion.identity) as TrackController);

        }
	
        currentCooldown = baseCooldown;

    }
	
	// Update is called once per frame
	void Update () {
        currentCooldown -= Time.deltaTime;
	    if(currentCooldown <= 0) {
            currentCooldown = baseCooldown;
            temp = temp >= activeTracks-1 ? 0 : temp + 1 ;
            tracks[temp].spawnTrackNote();
        }
	
	}
}
