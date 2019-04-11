using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingEveNote : Note {

    public AudioSource audioSource;
    public List<AudioClip> voiceOvers;

    public override void Start()
    {
        base.Start();
        if (!audioSource)
        {
            audioSource = GetComponent<AudioSource>();
        }
    }

    public override string nextLine()
    {
        if (paragraphIndex >= paragraph.Count - 1)
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
                if (voiceOvers.Count > paragraphIndex)
                {
                    audioSource.clip = voiceOvers[paragraphIndex];
                    if (audioSource.clip)
                    {
                        audioSource.Play();
                    }
                }
            }
            return paragraph[paragraphIndex];
        }
    }
}
