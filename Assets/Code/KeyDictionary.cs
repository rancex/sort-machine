using UnityEngine;
using System.Collections;

public static class KeyDictionary{

    public static class SORTTYPE {
        public const int BUBBLESORT = 1;
        public const int SELECTIONSORT = 2;
        public const int INSERTIONSORT = 3;
        public const int SHELLSORT = 4;
        public const int HEAPSORT = 5;
        public const int MERGESORT = 6;
        public const int QUICKSORT = 7;
    }

    public static class SCENES {
        public const string BUBBLESORT = "BubbleSort";
        public const string SELECTIONSORT = "SelectionSort";
        public const string INSERTIONSORT = "InsertionSort";
        public const string SHELLSORT = "ShellSort";
        public const string HEAPSORT = "HeapSort";
        public const string MERGESORT = "MergeSort";
        public const string QUICKSORT = "QuickSort2";
        public const string MAINMENU = "mainmenu";
        public const string ARCADE = "ArcadeStage";
    }

    public static class TREECHILDDIRECTION {
        public const int LEFT = 1;
        public const int RIGHT = 2;
    }

    public static class MOVETYPES {
        public const int MOVECRANEGREEN = 1;
        public const int MOVECRANERED = 2;
        public const int MOVECRANEREDLEFT = 6;
        public const int SWITCHOBJECT = 3;
        public const int MARK = 4;
        public const int GRABOBJECT = 5;
    }

    public static class BUTTONSPRITES {
        public const int HighlightGreenMoveCraneGreen = 1;
        public const int HighlightRedMoveCraneGreen = 2;
        public const int NormalMoveCraneGreen = 3;

        public const int HighlightGreenMoveCraneRed = 4;
        public const int HighlightRedMoveCraneRed = 5;
        public const int NormalMoveCraneRed = 6;

        public const int HighlightGreenSwitch= 7;
        public const int HighlightRedSwitch = 8;
        public const int NormalSwitch = 9;

        public const int HighlightGreenMark = 10;
        public const int HighlightRedMark = 11;
        public const int NormalMark = 12;
    }
}
