using UnityEngine;
using System.Collections;

public class TickSymbolScript : MonoBehaviour {

    public SpriteRenderer rend;

    public Sprite LEDOff;
    public Sprite LEDOn;

	// Use this for initialization
	void Start () {
        rend = this.GetComponent<SpriteRenderer>();
        rend.sprite = LEDOff;
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void switchColor() {
        rend.sprite = LEDOn;
    }
}
