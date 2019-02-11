using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystal : Pickable {

    public GameManager gameManager;
    public List<ObjectIdentifier> resettingObjects;

    public override void Start()
    {
        base.Start();
        if (!gameManager)
        {
            gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        }
    }

    public override void Pick(Wand p)
    {
        base.Pick(p);
        print("Hit " + name);
        Drop(p);
        ResetObjects(); // Reset related objects in corresponding area
        gameManager.SaveGame();  // Save games
    }

    public override void Drop(Wand p)
    {
        base.Drop(p);
        p.pickable = null;
    }

    public void ResetObjects ()
    {
        foreach (ObjectIdentifier ob in resettingObjects)
        {
            ob.SendMessage("OnReset", SendMessageOptions.DontRequireReceiver);
        }
    }
}