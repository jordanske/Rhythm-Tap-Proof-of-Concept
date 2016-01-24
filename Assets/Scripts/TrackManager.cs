using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TrackManager : MonoBehaviour {

    public static TrackManager current;

    //Track Prefab
    public TrackController trackPrefab;

    public GameObject noteEffectPrefab;

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

    public static float trackSpeed {
        get {
            return GameManager.pause ? 0 : (GameManager.combo * 3); //Temporary
            //return GameManager.combo * 2;
        }
    }
    
    public static float TimeTillNextNote {
        get {
            //Hoe hoger speed, hoe lager dit. Hoe berekenen ? xD

            return trackSpeed / 2;
        }
    }


    private ObjectPooler noteEffectsPooler;

    void Awake() {
        current = this;
    }

    void Start () {
        noteEffectsPooler = Instantiate(GameManager.ObjectPooler) as ObjectPooler;
        noteEffectsPooler.initialize(noteEffectPrefab, 4, true);

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

            yield return new WaitForSeconds(TimeTillNextNote * 2f);
        }
    }

    void Update () {

	}

    private void setHitbar() {
        hitbar = Instantiate(hitbarPrefab, new Vector3(0, (-GameManager.cameraDimensions.y*0.7f) /2, 0), Quaternion.identity) as GameObject;
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
		note.GetComponent<SpriteRenderer>().color = new Color(0f, 1f, 0f, 0.4f); // Transparency on hit
		Debug.Log("HIT " + hitPerc);
          
        GameManager.combo += (0.02f * (1f + (0.1f * GameManager.combo)));

        GameManager.addNotes((int) Mathf.Ceil(GameManager.notesPerTap * hitPerc));
        GameManager.addExperience((int) Mathf.Ceil(GameManager.experiencePerTap * hitPerc));

        if (noteEffectsPooler) {
            GameObject noteEffect = noteEffectsPooler.getObject();
            if (noteEffect) {
                noteEffect.GetComponent<NoteEffectController>().setSprite(Camera.main.ScreenToWorldPoint(Input.mousePosition), hitPerc);
                noteEffect.SetActive(true);
            }
        }
    }

    public void onNoteMiss(GameObject note) {
        GameManager.combo = ((GameManager.combo - 1) * 0.5f) + 1;
        Debug.Log("MISU");
        
        if (noteEffectsPooler) {
            GameObject noteEffect = noteEffectsPooler.getObject();
            if (noteEffect) {
                Vector3 position = note ? note.transform.position : Camera.main.ScreenToWorldPoint(Input.mousePosition);
                noteEffect.GetComponent<NoteEffectController>().setSprite(position, 0);
                noteEffect.SetActive(true);
            }
        }
    }
}
