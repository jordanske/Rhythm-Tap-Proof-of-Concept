using UnityEngine;
using System.Collections;

public class TrackNoteController : MonoBehaviour {
    
    public bool hit = false;
    public bool missed = false;

    public int semitone;

    void Start () {

    }

    public void reset(float x, int Semitone) {
        semitone = Semitone;
        transform.position = new Vector3(x, (GameManager.cameraDimensions.y / 2) + TrackManager.hitbarHeight, 0);
        gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f); // Transparency reset
        hit = false;
        missed = false;
    }

	void Update () {
        transform.position += new Vector3(0, -TrackManager.trackSpeed * Time.deltaTime, 0);
        
        float hitrate = hitRate();
        if(hitrate > 100 && !hit && !missed) {
            //TrackManager.current.onNoteMiss(gameObject);
            TrackManager.current.onNoteHit(gameObject, 100);
            missed = true;
        }

        if (transform.position.y + (transform.localScale.y / 2) <= (-GameManager.cameraDimensions.y / 2)) {
            if(!GetComponent<AudioSource>().isPlaying)
                gameObject.SetActive(false);
        }
	}

    public float hitRate() {        
        float relativePosition = TrackManager.hitbar.transform.position.y - transform.position.y;
        float percentage = relativePosition / TrackManager.hitbarHeight * 100;

        return percentage;
    }
    
    public void playTone() {
        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.pitch = Mathf.Pow(2, semitone / 12.0f);
        audioSource.Play();
    }

    void Destroy() {
        gameObject.SetActive(false);
    }

}