using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogSecretTrigger : Trigger {

    public bool isActive;
    public OpeningEveNote note;
    public GameManager gameManager;

    public override void Start()
    {
        base.Start();
        note = GetComponent<OpeningEveNote>();
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
                isActive = true;
                gameManager.StartNote(note);
            }
        }
    }
}
