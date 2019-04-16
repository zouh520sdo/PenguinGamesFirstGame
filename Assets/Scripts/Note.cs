using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Paragraph
{
    public bool noNeedCondition;
    public int needDiaryAmount;
    public int hasGua;
    public int foundThisDiary;
    public List<string> texts;
    public List<AudioClip> audioOvers;
    public List<DialogueOption> options;
}

[System.Serializable]
public class DialogueOption
{
    public string content;
    public Paragraph paragraph;
}

public delegate string MakeSelectionDelegate(int optionIndex);

public class Note : MonoBehaviour {

    public string note;
    public bool isInOption;
    public List<string> paragraph;
    public bool canStartParagraph = true;
    public bool canRepeat;
    public bool finished;
    protected int paragraphIndex;
    protected bool originalFinished;
    protected bool originalCanParagraph;
    protected bool originalCanRepeat;


    public virtual void OnReset()
    {
        finished = originalFinished;
        canStartParagraph = originalCanParagraph;
        canRepeat = originalCanRepeat;
    }

    // Use this for initialization
    public virtual void Start () {
        paragraphIndex = -1;
        originalFinished = finished;
        originalCanParagraph = canStartParagraph;
        originalCanRepeat = canRepeat;
    }

    // Update is called once per frame
    public virtual void Update () {
		
	}

    public virtual string nextLine()
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

    public virtual bool getIsFinished()
    {
        return finished;
    }
}
