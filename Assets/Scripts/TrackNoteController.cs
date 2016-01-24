using UnityEngine;
using System.Collections;

public class TrackNoteController : MonoBehaviour {
    
    public bool hit = false;
    public bool missed = false;

    void Start () {

    }

    public void reset(float x) {
        transform.position = new Vector3(x, (GameManager.cameraDimensions.y / 2) + TrackManager.hitbarHeight, 0);
        gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f); // Transparency reset
        hit = false;
        missed = false;
    }

	void Update () {
        transform.position += new Vector3(0, -TrackManager.trackSpeed * Time.deltaTime, 0);
        
        float hitrate = hitRate();
        if(hitrate > 100 && !hit && !missed) {
            TrackManager.current.onNoteMiss(gameObject);
            missed = true;
        }

        if (transform.position.y + (transform.localScale.y / 2) <= (-GameManager.cameraDimensions.y / 2)) {
            gameObject.SetActive(false);
        }
	}

    public float hitRate() {        
        float relativePosition = TrackManager.hitbar.transform.position.y - transform.position.y;
        float percentage = relativePosition / TrackManager.hitbarHeight * 100;

        return percentage;
    }

    void Destroy() {
        gameObject.SetActive(false);
    }

}