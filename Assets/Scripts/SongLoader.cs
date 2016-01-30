using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public static class SongLoader {

    private static JSONObject JSON;
    private static string version;
    private static List<JSONObject> songList;

    private static JSONObject currentSong;
    private static List<JSONObject> currentSongNotes;
    private static int currentNoteIndex;
    private static int currentNoteLastIndex;

    public static int currentSongId {
        get {
            return (int) currentSong.GetField("id").n;
        }
    }

    public static string currentSongName {
        get {
            return currentSong.GetField("name").str;
        }
    }

    public static int currentSongLength {
        get {
            return (int)currentSong.GetField("length").n;
        }
    }

    public static void initialize() {
        JSON = new JSONObject(GameManager.SongList.text);
        version = JSON.GetField("version").str;
        songList = JSON.GetField("songs").list;

        nextSong();
    }

    private static void nextSong() {
        currentSong = songList[Random.Range(0, songList.Count - 1)];
        currentSongNotes = currentSong.GetField("notes").list;
        currentNoteIndex = 0;
        currentNoteLastIndex = currentSongNotes.Count-1;
    }

    public static SongNote nextNote() {
        if (currentNoteIndex >= currentNoteLastIndex) nextSong();
        currentNoteIndex++;
        return new SongNote(currentSongNotes[currentNoteIndex-1]);
    }
	
}
