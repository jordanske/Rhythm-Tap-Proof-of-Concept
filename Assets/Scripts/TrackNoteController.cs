using UnityEngine;
using System.Collections;

public class TrackNoteController : MonoBehaviour {
    
    public float speed;
    public bool hit = false;
    public bool missed = false;

    void Start () {

    }

    public void reset(float x) {
        transform.position = new Vector3(x, (GameManager.cameraDimensions.y / 2) + TrackManager.hitbarHeight, -4);
        hit = false;
        missed = false;
    }

	void Update () {
        float tspeed = GameManager.pause ? 0 : speed ; //Temporary
        transform.position += new Vector3(0, -tspeed * Time.deltaTime, 0);
        
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
    
}