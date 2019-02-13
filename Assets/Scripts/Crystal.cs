using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Crystal : Pickable {

    public Image blackScreen;
    public GameManager gameManager;
    public List<ObjectIdentifier> resettingObjects;

    public float fadeOutDuration;
    public float fadeInDuration;

    protected float rotateSpeed;
    protected Vector3 originalPos;

    public override void Start()
    {
        base.Start();
        if (!gameManager)
        {
            gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        }

        rotateSpeed = 60f;
        originalPos = transform.position;
    }

    public override void Update()
    {
        base.Update();

        transform.RotateAround(transform.position, Vector3.up, rotateSpeed * Time.deltaTime);
        transform.position = originalPos + new Vector3(0, 0.3f * Mathf.Sin( 3f * Time.time), 0);
    }

    public override void Pick(Wand p)
    {
        base.Pick(p);
        print("Hit " + name);
        Drop(p);

        // Start to black out, save, and then fade in to game
        StartCoroutine(FadeOutToBlack());
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

    IEnumerator FadeOutToBlack()
    {
        float timestamp = Time.time;
        Color tempColor = blackScreen.color;
        while (Time.time - timestamp < fadeOutDuration)
        {
            tempColor.a = (Time.time - timestamp) / fadeOutDuration;
            blackScreen.color = tempColor;
            yield return null;
        }
        tempColor.a = 1;
        blackScreen.color = tempColor;

        ResetObjects(); // Reset related objects in corresponding area
        gameManager.SaveGame();  // Save games

        StartCoroutine(FadeInToGame());
    }

    IEnumerator FadeInToGame()
    {
        float timestamp = Time.time;
        Color tempColor = blackScreen.color;
        while (Time.time - timestamp < fadeInDuration)
        {
            tempColor.a = 1 -  (Time.time - timestamp) / fadeInDuration;
            blackScreen.color = tempColor;
            yield return null;
        }
        tempColor.a = 0;
        blackScreen.color = tempColor;
    }
}