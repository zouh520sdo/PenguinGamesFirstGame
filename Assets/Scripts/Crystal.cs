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

    public bool isFullCharged;
    public float chargedHeight;
    public float unchargedHeight;
    protected Vector3 targetPos;

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
        originalPos = transform.localPosition;
        targetPos = transform.localPosition;
    }

    public override void Update()
    {
        base.Update();

        if (isFullCharged)
        {
            targetPos.y = chargedHeight;
            Vector3 target = targetPos + new Vector3(0, 0.5f * Mathf.Sin(3f * Time.time), 0);
            transform.localPosition = Vector3.Lerp(transform.localPosition, target, 3f * Time.deltaTime);
            transform.RotateAround(transform.position, Vector3.up, rotateSpeed * Time.deltaTime);
        }
        else
        {
            targetPos.y = unchargedHeight;

            if (Vector3.Distance(transform.localPosition, targetPos) > 0.01f)
            {
                transform.localPosition = Vector3.Lerp(transform.localPosition, targetPos, 3f * Time.deltaTime);
                transform.RotateAround(transform.position, Vector3.up, (transform.localPosition.y - unchargedHeight) / (chargedHeight - unchargedHeight) * rotateSpeed * Time.deltaTime);
            }
            else
            {
                transform.localPosition = targetPos;
            }
        }
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
            if (ob)
            {
                ob.SendMessage("OnReset", SendMessageOptions.DontRequireReceiver);
            }
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