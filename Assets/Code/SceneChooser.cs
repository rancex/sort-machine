using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public static class SceneChooser{

    public static void startBubbleSort() {
        PlayerPrefs.SetInt("sorttype", KeyDictionary.SORTTYPE.BUBBLESORT);
        SceneManager.LoadScene(KeyDictionary.SCENES.BUBBLESORT);
    }

    public static void startSelectionSort() {
        PlayerPrefs.SetInt("sorttype", KeyDictionary.SORTTYPE.SELECTIONSORT);
        SceneManager.LoadScene(KeyDictionary.SCENES.SELECTIONSORT);
    }

    public static void startInsertionSort() {
        PlayerPrefs.SetInt("sorttype", KeyDictionary.SORTTYPE.INSERTIONSORT);
        SceneManager.LoadScene(KeyDictionary.SCENES.INSERTIONSORT);
    }

    public static void startShellSort() {
        PlayerPrefs.SetInt("sorttype", KeyDictionary.SORTTYPE.SHELLSORT);
        SceneManager.LoadScene(KeyDictionary.SCENES.SHELLSORT);
    }

    public static void startHeapSort() {
        PlayerPrefs.SetInt("sorttype", KeyDictionary.SORTTYPE.HEAPSORT);
        SceneManager.LoadScene(KeyDictionary.SCENES.HEAPSORT);
    }

    public static void startMergeSort() {
        PlayerPrefs.SetInt("sorttype", KeyDictionary.SORTTYPE.MERGESORT);
        SceneManager.LoadScene(KeyDictionary.SCENES.MERGESORT);
    }

    public static void startQuickSort() {
        PlayerPrefs.SetInt("sorttype", KeyDictionary.SORTTYPE.QUICKSORT);
        SceneManager.LoadScene(KeyDictionary.SCENES.QUICKSORT);
    }

    public static void returnMainMenu() {
        PlayerPrefs.SetInt("sorttype", 0);
        SceneManager.LoadScene(KeyDictionary.SCENES.MAINMENU);
    }

    public static void replayStage() {
        int sorttype = PlayerPrefs.GetInt("sorttype", 0);
        switch (sorttype) {
            case KeyDictionary.SORTTYPE.BUBBLESORT:
                {
                    startBubbleSort();
                    break;
                }
            case KeyDictionary.SORTTYPE.SELECTIONSORT:
                {
                    startSelectionSort();
                    break;
                }
            case KeyDictionary.SORTTYPE.INSERTIONSORT:
                {
                    startInsertionSort();
                    break;
                }
            case KeyDictionary.SORTTYPE.SHELLSORT:
                {
                    startShellSort();
                    break;
                }
            case KeyDictionary.SORTTYPE.MERGESORT:
                {
                    startMergeSort();
                    break;
                }
            case KeyDictionary.SORTTYPE.HEAPSORT:
                {
                    startHeapSort();
                    break;
                }
            case KeyDictionary.SORTTYPE.QUICKSORT:
                {
                    startQuickSort();
                    break;
                }
        }
    }
}
