using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingEveNote : Note {

    public AudioSource audioSource;
    public List<AudioClip> voiceOvers;

    public int diaryAmountToTrigger;
    public List<string> secretParagraphs;
    public List<AudioClip> secretVoiceOvers;

    public List<Paragraph> primaryParagraghs;

    protected Paragraph tempParagraph;
    protected List<string> tempTexts;
    protected List<AudioClip> tempVoiceOvers;
    protected List<DialogueOption> tempOptions;

    public override void Start()
    {
        base.Start();
        if (!audioSource)
        {
            audioSource = GetComponent<AudioSource>();
        }
    }

    public virtual void MakeSelection(int optionIndex)
    {
        DialogueOption d = tempOptions[optionIndex];
        tempParagraph = d.paragraph;
        tempTexts = d.paragraph.texts;
        tempVoiceOvers = d.paragraph.audioOvers;
        tempOptions = d.paragraph.options;
        isInOption = false;
        // Hide selection UI
        nextLine();
    }

    public override string nextLine()
    {

        int hasGua = PlayerPrefs.GetInt("HasGua");
        int foundThisDiary = PlayerPrefs.GetInt("FoundThisDiary");
        int diaryCount = PlayerPrefs.GetInt("DiaryAmount");

        if (primaryParagraghs.Count > 0)
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

                if (tempOptions.Count == 0) // If no options
                {
                    finished = true;
                    
                    return "";
                }
                else // if have options
                {
                    // Show UI options for player to selection
                    // ....
                    isInOption = true;
                    return "";
                }
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
