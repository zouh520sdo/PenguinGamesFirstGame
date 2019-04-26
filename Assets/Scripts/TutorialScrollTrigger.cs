using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialScrollTrigger : Trigger {

    public bool isActive;
    public OpeningEveNote evesIntroduction;
    public Note scrollNote;
    public GameManager gameManager;
    public Wand wand;

    public override void Start()
    {
        base.Start();
        if (!scrollNote)
        {
            scrollNote = GetComponent<Note>();
        }
        if (!gameManager)
        {
            gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        }
        if (!wand)
        {
            wand = GameObject.FindGameObjectWithTag("Player").GetComponent<Wand>();
        }
    }

    public override void Update()
    {
        base.Update();

        if (!isActive)
        {
            if (scrollNote.getIsFinished())
            {
                isActive = true;
                gameManager.StartNote(evesIntroduction);
                StartCoroutine(ShowHandForSeconds(4.5f));
            }
        }
    }

    public IEnumerator ShowHandForSeconds(float time)
    {
        wand.ShowHand();
        yield return new WaitForSeconds(time);
        wand.HideHand();
    }
}
