using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour {

    public string note;
    public List<string> paragraph;
    public bool canStartParagraph = true;
    protected bool finished;
    protected int paragraphIndex;
    protected bool originalFinished;
    protected bool originalCanParagraph;


    public void OnReset()
    {
        finished = originalFinished;
        canStartParagraph = originalCanParagraph;
    }

	// Use this for initialization
	void Start () {
        paragraphIndex = -1;
        originalFinished = finished;
        originalCanParagraph = canStartParagraph;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public string nextLine()
    {
        if (paragraphIndex >= paragraph.Count-1)
        {
            paragraphIndex = -1;
            finished = true;
            return "";
        }
        else
        {
            paragraphIndex++;
            return paragraph[paragraphIndex];
        }
    }

    public bool getIsFinished()
    {
        return finished;
    }
}
