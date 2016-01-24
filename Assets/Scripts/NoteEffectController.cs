using UnityEngine;
using System.Collections;

public class NoteEffectController : MonoBehaviour {

    public Sprite sprite_perfect;
    public Sprite sprite_good;
    public Sprite sprite_bad;
    public Sprite sprite_miss;

    private SpriteRenderer spriteRenderer;

    void Awake () {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    public void setSprite (Vector3 position, float hitPerc) {
        transform.position = new Vector3(position.x, position.y, 0);

        if (hitPerc > 0.90f) {
            spriteRenderer.sprite = sprite_perfect;
        } else if(hitPerc > 0.50f) {
            spriteRenderer.sprite = sprite_good;
        } else if(hitPerc > 0f) {
            spriteRenderer.sprite = sprite_bad;
        } else {
            spriteRenderer.sprite = sprite_miss;
        }

        Invoke("Destroy", 1f);
    }

    void Destroy() {
        gameObject.SetActive(false);
    }
	
	void Update () {
	
	}
}
