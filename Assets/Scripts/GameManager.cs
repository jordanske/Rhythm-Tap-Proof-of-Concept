using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    public static GameManager current;

    //Track Manager Prefab
    public TrackManager trackManagerPrefab;

    //Easy to access Track Manager
    public static TrackManager trackManager;

    public static Vector2 cameraDimensions;

    //Object Pooler Prefab
    public static ObjectPooler ObjectPooler;
    public ObjectPooler objectPooler;

    //Current experience of the player
    private static int experience;

    //Calculates the current level of the player based on current experience
    public static int Level {
        get {
            return (int) (Mathf.Floor(25 + Mathf.Sqrt(625 + 100 * experience)) / 50);
        }
        //Level is not settable
    }
    
    //Current notes of the player, currency in this game
    private static int notes;

    //Combo modifier
    public static float combo;

    public static int notesPerTap;
    public static int experiencePerTap;

    public static TextAsset SongList;
    public TextAsset songList;

    //Temporary
    public static bool pause;
    public bool Pause;

    void Awake () {
        current = this;
    }

    void Start () {
        ObjectPooler = objectPooler;
        SongList = songList;
        experience = 0;
        notes = 0;
        combo = 1.00f;
        notesPerTap = 1;
        experiencePerTap = 1;
        TrackManager.trackCount = 3;
        
        cameraDimensions = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height)) - Camera.main.ScreenToWorldPoint(Vector2.zero);
        
        trackManager = Instantiate(trackManagerPrefab, Vector2.zero, Quaternion.identity) as TrackManager;

        SongLoader.initialize();
    }

    void Update () {
        pause = Pause;
    }

    public static void addExperience(int amount) {
        if(amount > 0) {
            experience += (int) (amount * (GameManager.combo * 2f));
        }
    }

    public static void addNotes(int amount) {
        if(amount > 0) {
            notes += (int) (amount * (GameManager.combo * 1f));
        }
    }

    //Temporary
    void OnGUI() {
        GUI.Box(new Rect(10, 10, 100, 20), "Notes: " + notes.ToString());
        GUI.Box(new Rect(10, 32, 100, 20), "Experience: " + experience.ToString());
        GUI.Box(new Rect(10, 54, 100, 20), "Level: " + Level.ToString());
        GUI.Box(new Rect(10, 76, 100, 20), "Combo: " + combo.ToString());
        
        GUI.Box(new Rect(10, 100, 100, 20), "trackSpeed: " + TrackManager.trackSpeed.ToString());
        GUI.Box(new Rect(10, 122, 100, 20), "ttnNote: " + TrackManager.TimeTillNextNote.ToString());
    }
}
