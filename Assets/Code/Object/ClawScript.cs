using UnityEngine;
using System.Collections;

public class ClawScript : MonoBehaviour {

    public GameObject outlineEffect;

    public int clawNumber;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void highlightClaw(int newClawNumber) {

        

        clawNumber = newClawNumber;

        OutlineEffect[] oleList = GameObject.Find("Main Camera").GetComponents<OutlineEffect>();//.outlineRenderers.Add(outlineEffect.GetComponent<Renderer>());


        bool found = false;

        foreach (OutlineEffect ole in oleList) {

            if (found == false) {
                if (ole.outlineRenderers.Count <= 0) {
                    ole.outlineRenderers.Add(outlineEffect.GetComponent<Renderer>());
                    if (clawNumber == 1) {
                        ole.lineColor0 = Color.red;
                        ole.lineColor1 = Color.red;
                        ole.lineColor2 = Color.red;
                    }
                    else if (clawNumber == 2) {
                        ole.lineColor0 = Color.green;
                        ole.lineColor1 = Color.green;
                        ole.lineColor2 = Color.green;
                    }
                    found = true;
                }
            }
        }

        
    }
}
