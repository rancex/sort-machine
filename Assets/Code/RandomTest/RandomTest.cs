using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class RandomTest : MonoBehaviour {

    public int testObjectAmount = 5;
    public int testAmount = 30;

	// Use this for initialization
	void Start () {
        testRandomness();
	}

    void testRandomness() {
        List<int> numberList = new List<int>();

        for (int i = 0; i < testAmount; i++) {
            numberList = Shuffler.generateRandomList(testObjectAmount);

            string numberString = "";

            foreach(int number in numberList) {
                numberString = numberString + " " + number;
            }

            Debug.Log(numberString);
        }
    }
}
