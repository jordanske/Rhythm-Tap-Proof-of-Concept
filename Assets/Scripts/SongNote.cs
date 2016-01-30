using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SongNote {
    private JSONObject Note;

    public List<int> notes {
        get {
            List<int> n = new List<int>();
            foreach(JSONObject tone in Note.GetField("tones").list) {
                n.Add((int) tone.n);
            }
            return n;
        }
    }

    public float length {
        get {
            return Note.GetField("length").n;
        }
    }

    public SongNote(JSONObject note) {
        Note = note;
    }

}
