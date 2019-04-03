using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PicturePuzzleTrigger : Trigger {

    public PicturePuzzle puzzle;
    public Crystal crystal;

    protected PuzzleSelectionBlock block;

    public override void OnReset()
    {
        base.OnReset();
        if (puzzle)
        {
            puzzle.SendMessage("OnReset");
        }
        deactivateTriggees();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!block)
        {
            PuzzleSelectionBlock b = collision.collider.GetComponent<PuzzleSelectionBlock>();
            if (b && puzzle)
            {
                if (b.answer == puzzle.correctAnswer)
                {
                    activateTriggees();
                }
                else
                {
                    crystal.Pick(GameObject.FindWithTag("Player").GetComponent<Wand>());
                }
            }
        }
    }
}
