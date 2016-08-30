using UnityEngine;
using System.Collections;

public class ArcadeStageObject : MonoBehaviour {

    public TextMesh txtNumber;
    public TextMesh txtSort;
    public SpriteRenderer spriteRender;

    public Sprite circleNormal;
    public Sprite circleGlowGreen;
    public Sprite circleGlowRed;

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void changeNumber(int stageNum) {
        txtNumber.text = stageNum.ToString();
    }

    public void changeName(int sortType) {
        string target = "empty";
        switch (sortType) {
            case KeyDictionary.SORTTYPE.BUBBLESORT:
                {
                    target = "Bubble\nSort";
                    break;
                }
            case KeyDictionary.SORTTYPE.SELECTIONSORT:
                {
                    target = "Selection\nSort";
                    break;
                }
            case KeyDictionary.SORTTYPE.INSERTIONSORT:
                {
                    target = "Insertion\nSort";
                    break;
                }
            case KeyDictionary.SORTTYPE.SHELLSORT:
                {
                    target = "Shell\nSort";
                    break;
                }
            case KeyDictionary.SORTTYPE.HEAPSORT:
                {
                    target = "Heap\nSort";
                    break;
                }
            case KeyDictionary.SORTTYPE.MERGESORT:
                {
                    target = "Merge\nSort";
                    break;
                }
            case KeyDictionary.SORTTYPE.QUICKSORT:
                {
                    target = "Quick\nSort";
                    break;
                }
        }

        txtSort.text = target;
    }

    public void changeSprite(int spriteNum) {
        if (spriteNum == 1) spriteRender.sprite = circleNormal;
        else if (spriteNum == 2) spriteRender.sprite = circleGlowGreen;
        else if (spriteNum == 3) spriteRender.sprite = circleGlowRed;
    }
}
