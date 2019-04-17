using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogKwarkTrigger : Trigger {

    public bool isActive;
    public OpeningEveNote note;
    public GameManager gameManager;
    public FrogPicture frogPicture;

    public override void Start()
    {
        base.Start();
        if (!gameManager)
        {
            gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        }
    }

    public override void Update()
    {
        base.Update();
        if (!isActive)
        {
            int foundThisDiary = PlayerPrefs.GetInt("FoundThisDiary");
            if (foundThisDiary != 0)
            {
                // Found this level's diary
                gameManager.StartNote(note);
                isActive = true;
            }
        }
    }
}
