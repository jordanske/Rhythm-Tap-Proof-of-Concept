using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
    
    //Track Manager Prefab
    public TrackManager trackManagerPrefab;

    //Easy to access Track Manager
    public static TrackManager trackManager;

    public static Vector2 cameraDimensions;

    //Current experience of the player
    private static int experience;
    public int Experience {
        get {
            return experience;
        }
        set {
            experience = value;
        }
    }

    //Calculates the current level of the player based on current experience
    public int Level {
        get {
            return (int) (Mathf.Floor(25 + Mathf.Sqrt(625 + 100 * experience)) / 50);
        }
        //Level is not settable
    }

    //Friendly reminder : notes != trackNotes !
    //Current notes of the player, currency in this game
    private static int notes;
    public int Notes {
        get {
            return notes;
        }
        set {
            notes = value;
        }
    }

    void Start () {
        experience = 0;
        notes = 0;
        TrackManager.trackCount = 3;
        
        cameraDimensions = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height)) - Camera.main.ScreenToWorldPoint(Vector2.zero);
        
        trackManager = Instantiate(trackManagerPrefab, Vector2.zero, Quaternion.identity) as TrackManager;
        
    }

    void Update () {

    }
}
