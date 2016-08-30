using UnityEngine;
using System.Collections;

public class ButtonArtDB : MonoBehaviour {

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

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public Sprite returnSprite(int spriteNum) {
        switch (spriteNum) {
            case KeyDictionary.BUTTONSPRITES.HighlightGreenMoveCraneGreen:
                {
                    return GreenMoveHighlightSprite;
                }
            case KeyDictionary.BUTTONSPRITES.HighlightRedMoveCraneGreen:
                {
                    break;
                }
            case KeyDictionary.BUTTONSPRITES.NormalMoveCraneGreen:
                {
                    return GreenMoveNormalSprite;
                }

            case KeyDictionary.BUTTONSPRITES.HighlightGreenMoveCraneRed:
                {
                    return RedMoveHighlightSprite;
                }
            case KeyDictionary.BUTTONSPRITES.HighlightRedMoveCraneRed:
                {
                    
                    break;
                }
            case KeyDictionary.BUTTONSPRITES.NormalMoveCraneRed:
                {
                    return RedMoveNormalSprite;
                }

            case KeyDictionary.BUTTONSPRITES.HighlightGreenSwitch:
                {
                    return SwitchHighlightSprite;
                }
            case KeyDictionary.BUTTONSPRITES.HighlightRedSwitch:
                {
                    break;
                }
            case KeyDictionary.BUTTONSPRITES.NormalSwitch:
                {
                    return SwitchNormalSprite;
                }

            case KeyDictionary.BUTTONSPRITES.HighlightGreenMark:
                {
                    return MarkHighlightSprite;
                }
            case KeyDictionary.BUTTONSPRITES.HighlightRedMark:
                {
                    
                    break;
                }
            case KeyDictionary.BUTTONSPRITES.NormalMark:
                {
                    return MarkNormalSprite;
                }
            default: return normalSprite;
        }
        return normalSprite;
    }
}
