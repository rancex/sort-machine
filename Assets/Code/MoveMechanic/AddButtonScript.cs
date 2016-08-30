using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AddButtonScript : MonoBehaviour {

    public int moveType;
    public GameObject spriteManager;
    /*
    public Sprite normalSprite;
    public Sprite highlightedSpriteRed;
    public Sprite highlightedSpriteGreen;

    public Sprite GreenMoveNormalSprite;
    public Sprite GreenMoveHighlightSprite;

    public Sprite RedMoveNormalSprite;
    public Sprite RedMoveHighlightSprite;

    public Sprite SwitchNormalSprite;
    public Sprite SwitchHighlightSprite;

    public Sprite MarkNormalSprite;
    public Sprite MarkHighlightSprite;
    */

    // Use this for initialization
    void Start () {
        this.transform.SetParent(GameObject.Find("ControllerGeneral").GetComponent<Transform>(),false);
        
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void updateMovetype(int newMoveType) {
        moveType = newMoveType;

        spriteManager = GameObject.Find("SpriteManager");

        switch (moveType) {
            case KeyDictionary.MOVETYPES.MOVECRANEGREEN:
                {
                    this.GetComponent<Image>().sprite = spriteManager.GetComponent<ButtonArtDB>().returnSprite(KeyDictionary.BUTTONSPRITES.NormalMoveCraneGreen);
                    this.GetComponent<Button>().onClick.AddListener(delegate { GameObject.Find("ProgramMoveManager").GetComponent<ProgrammableMove>().addMoveGreenButton(); });
                    break;
                }
            case KeyDictionary.MOVETYPES.MOVECRANERED:
                {

                    this.GetComponent<Image>().sprite = spriteManager.GetComponent<ButtonArtDB>().returnSprite(KeyDictionary.BUTTONSPRITES.NormalMoveCraneRed);
                    this.GetComponent<Button>().onClick.AddListener(delegate { GameObject.Find("ProgramMoveManager").GetComponent<ProgrammableMove>().addMoveRedButton(); });
                    break;
                }
            case KeyDictionary.MOVETYPES.MOVECRANEREDLEFT:
                {
                    this.transform.localScale = new Vector3((-this.transform.localScale.x), this.transform.localScale.y, this.transform.localScale.z);
                    this.GetComponent<Image>().sprite = spriteManager.GetComponent<ButtonArtDB>().returnSprite(KeyDictionary.BUTTONSPRITES.NormalMoveCraneRed);
                    this.GetComponent<Button>().onClick.AddListener(delegate { GameObject.Find("ProgramMoveManager").GetComponent<ProgrammableMove>().addMoveRedButton(); });
                    break;
                }
            case KeyDictionary.MOVETYPES.SWITCHOBJECT:
                {
                    this.GetComponent<Image>().sprite = spriteManager.GetComponent<ButtonArtDB>().returnSprite(KeyDictionary.BUTTONSPRITES.NormalSwitch);
                    this.GetComponent<Button>().onClick.AddListener(delegate { GameObject.Find("ProgramMoveManager").GetComponent<ProgrammableMove>().addSwitchButton(); });
                    break;
                }
            case KeyDictionary.MOVETYPES.MARK:
                {
                    this.GetComponent<Image>().sprite = spriteManager.GetComponent<ButtonArtDB>().returnSprite(KeyDictionary.BUTTONSPRITES.NormalMark);
                    this.GetComponent<Button>().onClick.AddListener(delegate { GameObject.Find("ProgramMoveManager").GetComponent<ProgrammableMove>().addMarkButton(); });
                    break;
                }
        }
    }
}
