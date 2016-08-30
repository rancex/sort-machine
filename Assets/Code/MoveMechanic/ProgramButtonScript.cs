using UnityEngine;
using System.Collections;

public class ProgramButtonScript : MonoBehaviour {

    public GameObject programMoveManager;

    public GameObject spriteManager;
    


    public int moveType;
    //public TextMesh moveText;

    //represent the index number on movelist
    public int indexNumber;

	// Use this for initialization
	void Start () {
        programMoveManager = GameObject.Find("ProgramMoveManager");
        
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnMouseDown() {

        programMoveManager.GetComponent<ProgrammableMove>().removeMove(indexNumber);


        /*
        switch (moveType) {
            case KeyDictionary.MOVETYPES.MOVECRANE:
                {
                    
                    break;
                }
            case KeyDictionary.MOVETYPES.SWITCHOBJECT:
                {
                    break;
                }
        }
        */
        //programMoveManager.GetComponent<ProgrammableMove>().
    }

    public void setMoveIndex(int moveIndex) {
        indexNumber = moveIndex;
    }

    public void setMoveType(int newMoveType) {
        moveType = newMoveType;
        setSprite();
    }

    //also doubles as dehighlight condition
    public void setSprite() {

        spriteManager = GameObject.Find("SpriteManager");

        switch (moveType) {
            case KeyDictionary.MOVETYPES.MOVECRANEGREEN:
                {
                    
                    this.GetComponent<SpriteRenderer>().sprite = spriteManager.GetComponent<ButtonArtDB>().returnSprite(KeyDictionary.BUTTONSPRITES.NormalMoveCraneGreen);
                    break;
                }
            case KeyDictionary.MOVETYPES.MOVECRANERED:
                {
                    this.GetComponent<SpriteRenderer>().sprite = spriteManager.GetComponent<ButtonArtDB>().returnSprite(KeyDictionary.BUTTONSPRITES.NormalMoveCraneRed);
                    break;
                }
            case KeyDictionary.MOVETYPES.MOVECRANEREDLEFT:
                {
                    this.transform.localScale = new Vector3(-(this.transform.localScale.x), this.transform.localScale.y, this.transform.localScale.z);
                    this.GetComponent<SpriteRenderer>().sprite = spriteManager.GetComponent<ButtonArtDB>().returnSprite(KeyDictionary.BUTTONSPRITES.NormalMoveCraneRed);
                    break;
                }
            case KeyDictionary.MOVETYPES.SWITCHOBJECT:
                {
                    this.GetComponent<SpriteRenderer>().sprite = spriteManager.GetComponent<ButtonArtDB>().returnSprite(KeyDictionary.BUTTONSPRITES.NormalSwitch);
                    break;
                }
            case KeyDictionary.MOVETYPES.MARK:
                {
                    this.GetComponent<SpriteRenderer>().sprite = spriteManager.GetComponent<ButtonArtDB>().returnSprite(KeyDictionary.BUTTONSPRITES.NormalMark);

                    break;
                }
        }
    }


    //if correct, green, if wrong red
    public void highlightSprite(bool correct) {

        /*
        if(correct) this.GetComponent<SpriteRenderer>().sprite = highlightedSpriteGreen;
        else this.GetComponent<SpriteRenderer>().sprite = highlightedSpriteRed;
        */

        switch (moveType) {
            case KeyDictionary.MOVETYPES.MOVECRANEGREEN:
                {
                    this.GetComponent<SpriteRenderer>().sprite = spriteManager.GetComponent<ButtonArtDB>().returnSprite(KeyDictionary.BUTTONSPRITES.HighlightGreenMoveCraneGreen);
                    break;
                }
            case KeyDictionary.MOVETYPES.MOVECRANERED:
                {
                    this.GetComponent<SpriteRenderer>().sprite = spriteManager.GetComponent<ButtonArtDB>().returnSprite(KeyDictionary.BUTTONSPRITES.HighlightGreenMoveCraneRed);
                    break;
                }
            case KeyDictionary.MOVETYPES.SWITCHOBJECT:
                {
                    this.GetComponent<SpriteRenderer>().sprite = spriteManager.GetComponent<ButtonArtDB>().returnSprite(KeyDictionary.BUTTONSPRITES.HighlightGreenSwitch);
                    break;
                }
        }
    }
    
    public void dehighlightSprite() {
        setSprite();
        /*
        switch (moveType) {
            case KeyDictionary.MOVETYPES.MOVECRANEGREEN:
                {
                    this.GetComponent<SpriteRenderer>().sprite = GreenMoveNormalSprite;
                    break;
                }
            case KeyDictionary.MOVETYPES.MOVECRANERED:
                {
                    this.GetComponent<SpriteRenderer>().sprite = RedMoveNormalSprite;
                    break;
                }
            case KeyDictionary.MOVETYPES.SWITCHOBJECT:
                {
                    this.GetComponent<SpriteRenderer>().sprite = SwitchNormalSprite;
                    break;
                }
        }
        */
    }
}
