using UnityEngine;
using System.Collections;

public class ClawScript : MonoBehaviour {

    public GameObject spotLight;

    public GameObject outlineEffect;

    public int clawNumber;

    public Coroutine cor;

    public Coroutine animationCor;

    public Animator anim;

    public GameObject Outline;

    public GameObject clawSub;

    //state 1 = isMoving
    //state 2 = isMovingDown
    //state 3 = isPinching
    //state 4 = isGoingUp
    public int state = 0;

    public bool isPlayingAnimation = false;

    public int targetObjNum;

    public Vector3 originalTargetBoxPosition;

    public Vector3 targetBoxPosition;

    public GameObject nowObj;

    public int newNumber;

	// Use this for initialization
	void Start () {
        anim = this.GetComponent<Animator>();
    }

    bool isPlaying = false;

	// Update is called once per frame
	void Update () {
        if (isPlaying == true) 
        {
            if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !anim.IsInTransition(0)) {
                isPlaying = false;
                StartCoroutine(waitForAnimation(0.5f));
            }   
        }
        this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, -2.0f);
    }

    IEnumerator waitForAnimation(float sec) {
        //isPlaying = false;
        yield return new WaitForSeconds(sec);

        //after going up
        if (state == 4) {
            
            moveItemToObjectPosition();
        }

        else {
            nextAnimation();
        }
    }

    //Manage pinching animations
    public void pinchItem(int nowPos,int newTargetObjNum) {

        Outline.SetActive(false);

        targetObjNum = newTargetObjNum;
        
        /*
        targetObj = GameObject.Find("GameManager").GetComponent<GameManager>().carList[nowPos];

        Vector3 targetBoxPosition = targetObj.transform.position;

        originalTargetBoxPosition = new Vector3(targetBoxPosition.x, targetBoxPosition.y, targetBoxPosition.z);
        */

        nowObj = GameObject.Find("GameManager").GetComponent<GameManager>().carList[nowPos];

        targetBoxPosition = GameObject.Find("GameManager").GetComponent<GameManager>().carList[newTargetObjNum].transform.position;
    
        originalTargetBoxPosition = new Vector3(nowObj.transform.position.x, nowObj.transform.position.y, nowObj.transform.position.z);

        isPlayingAnimation = true;

        Outline.SetActive(false);
        state = 2;
        anim.SetInteger("state", state);
        isPlaying = true;
        //animationCor = StartCoroutine(waitForAnimationToFinish());
    }

    public void nextAnimation() {

        if (animationCor != null) {
            StopCoroutine(animationCor);
            animationCor = null;
        }

        if (state == 1) {
            state = 0;
            Debug.Log("justmove");
            GameObject.Find("ProgramMoveManager").GetComponent<ProgrammableMove>().afterMovement(this.gameObject);
        }

        else if (state == 8) {
            state = 0;
            GameObject.Find("ProgramMoveManager").GetComponent<ProgrammableMove>().afterSingleMovement();
        }

        else {
            state++;

            if (state == 4 || state == 5) {
                //targetObj.transform.position = new Vector3(clawSub.transform.position.x, clawSub.transform.position.y - 1, targetObj.transform.position.z);
                nowObj.transform.parent = clawSub.transform;
            }

            if (state == 5) {
                if (GameObject.Find("GameManager").GetComponent<GameManager>().sortType == KeyDictionary.SORTTYPE.INSERTIONSORT) {

                    GameObject.Find("GameManager").GetComponent<GameManager>().moveItems();
                }
            }

            else if (state == 7) {
                nowObj.transform.parent = null;
            }

            if (state > 7) state = 0;
            anim.SetInteger("state", state);

            if (state != 0) isPlaying = true;
            else {
                isPlaying = false;
                isPlayingAnimation = false;
                Outline.SetActive(true);
                GameObject.Find("GameManager").GetComponent<GameManager>().afterSwitch(this.gameObject, targetObjNum);
            }
        }
        

        //if (state !=4)
        //animationCor = StartCoroutine(waitForAnimationToFinish());
        
    }

    //single = check if only one crane is moving
    public void moveItemToObjectNum(int objNum,bool single) {

        Vector3 targetPos = GameObject.Find("GameManager").GetComponent<GameManager>().carList[objNum].transform.position;

        if (single == true) {
            state = 8;
        }
        else {
            state = 1;
        }

        cor = StartCoroutine(smoothMoveToTrigger(1.0f, targetPos));
    }

    public void moveItemToObjectPosition() {

        
        cor = StartCoroutine(smoothMoveToTrigger(1.0f, targetBoxPosition));
    }

    IEnumerator smoothMoveToTrigger(float time, Vector3 targetPos) {

        float elapsedTime = 0;

        while (elapsedTime < time) {
            this.transform.position = new Vector3(Mathf.Lerp(transform.position.x, targetPos.x, (elapsedTime / time)),
                                                  transform.position.y,
                                                  -4.0f);
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        nextAnimation();
    }

    IEnumerator waitForAnimationToFinish() {

        //Debug.Log(anim.GetCurrentAnimatorClipInfo(0).Length);

        while (true) {
            
            if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > anim.GetCurrentAnimatorStateInfo(0).length && !anim.IsInTransition(0)) {
                nextAnimation();
                yield return null;
            }
            else yield return null;
            //Debug.Log(anim.GetCurrentAnimatorStateInfo(0).normalizedTime);
        }
        
        /*
        if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !anim.IsInTransition(0)) {
        nextAnimation();
        yield return new WaitForFixedUpdate();
        }
        */
        /*
        while (true) {

            if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !anim.IsInTransition(0)) {
                nextAnimation();
                yield return new WaitForFixedUpdate();
                break;
            }
            */
            /*
            //Debug.Log(anim.GetCurrentAnimatorStateInfo(0).name
            if (state == 2) {
                
                if (anim.GetCurrentAnimatorStateInfo(0).IsName("PinchAnimation")) {
                    if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !anim.IsInTransition(0)) {
                        yield return new WaitForSeconds(0.1f);
                        nextAnimation();
                        break;
                    }
                }
                else {
                    Debug.Log("a");
                    yield return new WaitForSeconds(0.1f);
                    break;
                }
            }
            else if (state == 3) {
                if (anim.GetCurrentAnimatorStateInfo(0).IsName("ClosePinch")) {
                    if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !anim.IsInTransition(0)) {
                        yield return new WaitForSeconds(0.1f);
                        nextAnimation();
                        break;
                    }
                }
                else {
                    yield return new WaitForSeconds(0.1f);
                    break;
                }
            }
            else {
                yield return new WaitForSeconds(0.1f);
                break;
            }
            */
    }

    public void highlightClaw(int newClawNumber) {

        clawNumber = newClawNumber;

        OutlineEffect[] oleList = GameObject.Find("Camera").GetComponents<OutlineEffect>();//.outlineRenderers.Add(outlineEffect.GetComponent<Renderer>());


        bool found = false;

        foreach (OutlineEffect ole in oleList) {

            if (found == false) {
                if (ole.outlineRenderers.Count <= 0) {
                    if (spotLight.activeSelf == true) {
                        ole.outlineRenderers.Add(spotLight.GetComponent<Renderer>());
                        if (clawNumber == 1) {
                            ole.lineColor0 = Color.white;
                            ole.lineColor1 = Color.white;
                            ole.lineColor2 = Color.white;
                        }
                    }
                    else {
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
                    }
                    found = true;
                }
            }
        }   
    }

    public void changeSpriteToSpotlight() {

        disableAllRenderer(this.gameObject);

        /*
        Transform[] sr = this.gameObject.GetComponentsInChildren<Transform>();

        for (int i = 0; i < sr.Length; i++) {
            sr[i].gameObject.GetComponent<SpriteRenderer>().enabled = false;

            Transform[] sr2 = sr[i].gameObject.GetComponentsInChildren<Transform>();
            if (sr2.Length > 0) {
                for (int j = 0; j < sr2.Length; j++) {
                    sr2[j].gameObject.GetComponentInChildren<Transform>();
                }
            }
        }
        */

        spotLight.SetActive(true);
    }

    public void disableAllRenderer(GameObject g) {
        Transform[] sr = g.GetComponentsInChildren<Transform>();

        Debug.Log(sr.Length);
        
        for (int i = 0; i < sr.Length; i++) {
            SpriteRenderer srObj = sr[i].gameObject.GetComponent<SpriteRenderer>();

            if (srObj != null) srObj.enabled = false;
                
        }

        Outline.SetActive(false);
    }
}
