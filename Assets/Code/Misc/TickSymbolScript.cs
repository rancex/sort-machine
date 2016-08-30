using UnityEngine;
using System.Collections;

public class TickSymbolScript : MonoBehaviour {

    public SpriteRenderer rend;

	// Use this for initialization
	void Start () {
        rend = this.GetComponent<SpriteRenderer>();
        rend.color = Color.black;
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void switchColor() {
        rend.color = Color.green;
    }
}
