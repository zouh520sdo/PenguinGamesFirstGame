using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogSecretTrigger : Trigger {

    public bool isActive;
    public OpeningEveNote note;
    public GameManager gameManager;
    public FrogPicture frogPicture;

    public OpeningEveNote normalNote1;
    public OpeningEveNote normalNote2;
    public OpeningEveNote doFavorNote;
    public OpeningEveNote needExlixir;
    public OpeningEveNote gotExlixir;

    public OpeningEveNote frogHint;

    public bool isStart;
    public Animator frogAnim;


    public override void Start()
    {
        base.Start();
        if (!note)
        {
            note = GetComponent<OpeningEveNote>();
        }
        if (!gameManager)
        {
            gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        }
    }

    public override void Update()
    {
        base.Update();

        if (!isActive && isStart)
        {
            if (normalNote1.finished)
            {
                note = normalNote2;
            }
            if (normalNote2.finished)
            {
                note = doFavorNote;
            }
            if (doFavorNote.optionIndexMade == 0 && doFavorNote.finished && frogPicture.isActive)
            {
                note = gotExlixir;
                frogPicture.FrogFlyToWand();
            }
            if (doFavorNote.optionIndexMade == 0 && doFavorNote.finished && !frogPicture.isActive)
            {
                note = needExlixir;
            }
            if (doFavorNote.optionIndexMade == 1 && doFavorNote.finished) // Refuse
            {
                isActive = true;
                frogAnim.ResetTrigger("openeyes");
                frogAnim.SetTrigger("sleep");
            }

            if (!note.inThisNote) {
                if (note == normalNote1)
                {
                    gameManager.StartNote(note);
                    frogAnim.ResetTrigger("sleep");
                    frogAnim.SetTrigger("openeyes");
                }
                else if (note == gotExlixir || note == needExlixir)
                {
                    isActive = true;
                    frogPicture.TriggerSecretDialogue();
                    gameManager.StartNote(note);
                }
                else 
                {
                    gameManager.StartNote(note);
                }
            }
        }
    }
}
