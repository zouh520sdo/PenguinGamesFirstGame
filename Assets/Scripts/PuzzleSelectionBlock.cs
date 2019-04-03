using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleSelectionBlock : MonoBehaviour {

    public Answer answer;

    protected Answer originalAnswer;
    protected Vector3 originalPos;
    protected Quaternion originalRot;

    private void Start()
    {
        originalAnswer = answer;
        originalPos = transform.position;
        originalRot = transform.rotation;
    }

    public void OnReset()
    {
        answer = originalAnswer;
        transform.position = originalPos;
        transform.rotation = originalRot;
    }

}
