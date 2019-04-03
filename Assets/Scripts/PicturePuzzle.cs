using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Answer
{
    Red,
    Blue
}

public class PicturePuzzle : MonoBehaviour {

    public List<PuzzlePicture> pictures;
    public Answer correctAnswer;

    protected bool originalIsActive;

    public void Start()
    {
        pictures.AddRange(GetComponentsInChildren<PuzzlePicture>());
        InitializePuzzle();
    }

    public void OnReset()
    {
        InitializePuzzle();
    }

    public void Update()
    {

    }

    public void InitializePuzzle ()
    {
        if (pictures.Count >= 2)
        {
            int haha = Random.Range(1, 7);

            switch (haha)
            {
                // All red
                case 1:
                    pictures[0].isRed = true;
                    pictures[1].isRed = true;
                    correctAnswer = Answer.Red;
                    break;
                // you blue, other red
                case 2:
                    pictures[0].isRed = true;
                    pictures[1].isRed = true;
                    correctAnswer = Answer.Blue;
                    break;
                // You red, one red, and one blue
                case 3:
                    pictures[0].isRed = true;
                    pictures[1].isRed = false;
                    correctAnswer = Answer.Red;
                    break;
                // You red, one blue, and one red
                case 4:
                    pictures[0].isRed = false;
                    pictures[1].isRed = true;
                    correctAnswer = Answer.Red;
                    break;
                // You Blue, one red, and one blue
                case 5:
                    pictures[0].isRed = true;
                    pictures[1].isRed = false;
                    correctAnswer = Answer.Blue;
                    break;
                // You blue, one blue, and one red
                case 6:
                    pictures[0].isRed = false;
                    pictures[1].isRed = true;
                    correctAnswer = Answer.Blue;
                    break;

                // !All blue
                case 7:
                    pictures[0].isRed = false;
                    pictures[1].isRed = false;
                    correctAnswer = Answer.Blue;
                    break;

                // !You red, other blue
                case 8:
                    pictures[0].isRed = false;
                    pictures[1].isRed = false;
                    correctAnswer = Answer.Red;
                    break;
            }

            pictures[0].isHandsUp = (pictures[1].isRed && correctAnswer == Answer.Red);
            pictures[1].isHandsUp = (pictures[0].isRed && correctAnswer == Answer.Red);

            pictures[0].SetNote();
            pictures[1].SetNote();
        }
    }
}
