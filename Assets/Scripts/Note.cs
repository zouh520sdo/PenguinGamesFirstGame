using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class Paragraph
{
    public bool noNeedCondition;
    public int needDiaryAmount;
    public int hasGua;
    public int foundThisDiary;
    [TextArea]
    public List<string> texts;
    public List<AudioClip> audioOvers;
    public List<DialogueOption> options;
    public UnityEvent onComplete;
    
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
    [TextArea]
    public List<string> paragraph;
    public bool canStartParagraph = true;
    public bool canRepeat;
    public bool inThisNote;
    public bool finished;
    protected int paragraphIndex;
    protected bool originalFinished;
    protected bool originalCanParagraph;
    protected bool originalCanRepeat;
    protected bool originalInThisNote;
    protected bool originalIsInOption;


    public virtual void OnReset()
    {
        paragraphIndex = -1;
        finished = originalFinished;
        canStartParagraph = originalCanParagraph;
        canRepeat = originalCanRepeat;
        inThisNote = originalInThisNote;
        isInOption = originalIsInOption;
    }

    // Use this for initialization
    public virtual void Start () {
        paragraphIndex = -1;
        originalFinished = finished;
        originalCanParagraph = canStartParagraph;
        originalCanRepeat = canRepeat;
        originalInThisNote = inThisNote;
        originalIsInOption = isInOption;
    }

    // Update is called once per frame
    public virtual void Update () {
		
	}

    public virtual string nextLine()
    {
        inThisNote = true;
        if (paragraphIndex >= paragraph.Count-1)
        {
            paragraphIndex = -1;
            finished = true;
            inThisNote = false;
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

    public virtual void setFinished(bool can)
    {
        finished = can;
        originalFinished = can;
    }

    public virtual void setCanRepeatHard(bool can)
    {
        canRepeat = can;
        originalCanRepeat = can;
    }
}
