using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingEveNote : Note {

    public AudioSource audioSource;
    public List<AudioClip> voiceOvers;

    public int diaryAmountToTrigger;
    [TextArea]
    public List<string> secretParagraphs;
    public List<AudioClip> secretVoiceOvers;

    public List<Paragraph> primaryParagraghs;

    protected Paragraph tempParagraph;
    protected List<string> tempTexts;
    protected List<AudioClip> tempVoiceOvers;
    protected List<DialogueOption> tempOptions;
    public Wand wand;

    public int optionIndexMade;

    public override void Start()
    {
        base.Start();
        if (!audioSource)
        {
            audioSource = GetComponent<AudioSource>();
        }
        if (!wand)
        {
            wand = GameObject.FindGameObjectWithTag("Player").GetComponent<Wand>();
        }
    }

    public virtual string MakeSelection(int optionIndex)
    {
        optionIndexMade = optionIndex;
        DialogueOption d = tempOptions[optionIndex];
        tempParagraph = d.paragraph;
        tempTexts = d.paragraph.texts;
        tempVoiceOvers = d.paragraph.audioOvers;
        tempOptions = d.paragraph.options;
        isInOption = false;
        paragraphIndex = -1;
        // Hide selection UI

        wand.HideDialogueSelection();
        return nextLine();
    }

    public void InvokeOnComplete()
    {
        if (tempParagraph.onComplete != null)
        {
            print("Invoke on complete.");
            tempParagraph.onComplete.Invoke();
        }
    }

    public override string nextLine()
    {
        int hasGua = PlayerPrefs.GetInt("HasGua");
        int foundThisDiary = PlayerPrefs.GetInt("FoundThisDiary");
        int diaryCount = PlayerPrefs.GetInt("DiaryAmount");
        inThisNote = true;

        if (primaryParagraghs.Count > 0)  // Using primary paragraphs
        {
            if (tempParagraph == null)
            {
                foreach (Paragraph p in primaryParagraghs)
                {
                    if (p.noNeedCondition || (hasGua == p.hasGua && foundThisDiary == p.foundThisDiary && diaryCount >= p.needDiaryAmount))
                    {
                        tempParagraph = p;
                        tempTexts = p.texts;
                        tempVoiceOvers = p.audioOvers;
                        tempOptions = p.options;
                        break;
                    }
                }
            }

            if (paragraphIndex >= tempTexts.Count - 1)
            {
                paragraphIndex = -1;
                if (audioSource)
                {
                    audioSource.Stop();
                    audioSource.clip = null;
                }
                finished = true;
                return "";
            }
            else
            {
                paragraphIndex++;
                if (audioSource)
                {
                    audioSource.Stop();
                    if (tempVoiceOvers.Count > paragraphIndex)
                    {
                        audioSource.clip = tempVoiceOvers[paragraphIndex];
                        if (audioSource.clip)
                        {
                            audioSource.Play();
                        }
                    }
                }

                if (paragraphIndex >= tempTexts.Count - 1 && tempOptions.Count != 0) // if have options
                {
                    // Show UI options for player to selection
                    // ....
                    wand.ShowDialogueSelection(MakeSelection, tempOptions);
                    isInOption = true;
                }
                return tempTexts[paragraphIndex];
            }
        }
        else  // If not using primary paragraph
        {
            if (secretParagraphs.Count > 0 && diaryCount >= diaryAmountToTrigger)
            {
                tempTexts = secretParagraphs;
                tempVoiceOvers = secretVoiceOvers;
            }
            else
            {
                tempTexts = paragraph;
                tempVoiceOvers = voiceOvers;
            }

            if (paragraphIndex >= tempTexts.Count - 1)
            {
                paragraphIndex = -1;
                finished = true;
                if (audioSource)
                {
                    audioSource.Stop();
                    audioSource.clip = null;
                }
                return "";
            }
            else
            {
                paragraphIndex++;
                if (audioSource)
                {
                    audioSource.Stop();
                    if (tempVoiceOvers.Count > paragraphIndex)
                    {
                        audioSource.clip = tempVoiceOvers[paragraphIndex];
                        if (audioSource.clip)
                        {
                            audioSource.Play();
                        }
                    }
                }
                return tempTexts[paragraphIndex];
            }
        }
    }
}
